using System;
using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using morphological_image_processing_wpf.Core.Algorithms.Advanced.Configs;
using morphological_image_processing_wpf.Core.Converters;
using morphological_image_processing_wpf.Core.Generator;
using MorphologicalImageProcessing.Core.Algorithms;
using Newtonsoft.Json;

namespace morphological_image_processing_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadImageBtnClick(object sender, RoutedEventArgs e) 
        {
            LoadImageLoadDialog();
        }

        private void LoadImageLoadDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a picture",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                         "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                         "Portable Network Graphic (*.png)|*.png"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                SideBySideImagesComponent.SetBeforeImage(openFileDialog.FileName);
                SideBySideImagesComponent.SetAfterImage(null);
            }
        }

        private async void StartProcessingButton_Click(object sender, RoutedEventArgs e)
        {
            Bitmap beforeImage = SideBySideImagesComponent.GetBeforeImage();
            IAlgorithm selectedAlgorithm = AlgorithmSelectionComponent.GetSelectedAlgorithm();
            IMorphologicalAlgorithmConfiguration currentConfiguration = AlgorithmSelectionComponent.GetCurrentConfiguration();
            if (beforeImage == null)
            {
                ShowErrorDialog("You have to load an image");
                return;
            } else if (selectedAlgorithm == null)
            {
                ShowErrorDialog("You have to select an algorithm");
                return;
            } else if (currentConfiguration == null) {
                ShowErrorDialog("Algorithm has to have atleast empty configuration.");
                return;
            }
            try
            {
                await RunAlgorithm(beforeImage, selectedAlgorithm, currentConfiguration);
            } 
            catch (Exception ex)
            {
                string cause = "Exception encountered while running algorithm: " + ex.Message;
                MessageBox.Show(cause, "Exception encountered", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
            finally
            {
                StartProcessingButton.IsEnabled = true;
                LoadImageButton.IsEnabled = true;
                GenerateImageButton.IsEnabled = true;
            }
        }

        private async void DetectAlgorithmBtnClick(object sender, RoutedEventArgs e)
        {
            Bitmap beforeImage = SideBySideImagesComponent.GetBeforeImage();
            if (beforeImage == null)
            {
                ShowErrorDialog("You have to load an image");
                return;
            }
            await RunDetection(beforeImage);
        }

        private async Task RunDetection(Bitmap image)
        {
            StartProcessingButton.IsEnabled = false;
            LoadImageButton.IsEnabled = false;
            GenerateImageButton.IsEnabled = false;
            DetectAlgorithmButton.IsEnabled = false;
            DetectChanges detection = new DetectChanges();
            String result = await Task.Run(() =>
            {
                return detection.Apply(image);
            });
            AlgorithmDetectionTextBox.Text = result;
            DetectAlgorithmButton.IsEnabled = true;
            StartProcessingButton.IsEnabled = true;
            LoadImageButton.IsEnabled = true;
            GenerateImageButton.IsEnabled = true;
        }

        private async Task RunAlgorithm(Bitmap beforeImage, IAlgorithm selectedAlgorithm, IMorphologicalAlgorithmConfiguration currentConfiguration)
        {
            StartProcessingButton.IsEnabled = false;
            LoadImageButton.IsEnabled = false;
            GenerateImageButton.IsEnabled = false;
            DetectAlgorithmButton.IsEnabled = false;
            Bitmap afterImage = await Task.Run(() =>
            {
                return selectedAlgorithm.Apply(beforeImage, currentConfiguration);
            });
            SideBySideImagesComponent.SetAfterImageFromBitmap(afterImage);
        }

        private void ShowErrorDialog(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error message", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void GenerateImageBtnClick(object sender, RoutedEventArgs e)
        {
            ImageGenerator imageGenerator = new ImageGenerator(500, 500);
            SideBySideImagesComponent.SetBeforeImage(imageGenerator.GeneratePicture((GeneratorConfiguration) ImageGeneratorConfigurationComponent.GetConfiguration()));
        }

        private void LoadConfiguratioButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select configuration file",
                Filter = "Json file (*.json)|*.json"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                //SideBySideImagesComponent.SetBeforeImage(openFileDialog.FileName);
                //SideBySideImagesComponent.SetAfterImage(null);
                using (StreamReader r = new StreamReader(openFileDialog.FileName)) {
                    var jsonString = r.ReadToEnd();
                    BaseConfiguration baseConfiguration = JsonConvert.DeserializeObject<BaseConfiguration>(jsonString);
                    ApplyConfiguration(baseConfiguration, jsonString);
                };
            }
        }

        // TODO: This is just a workaround. It's necessary to finda a better solution
        private void ApplyConfiguration(BaseConfiguration baseConfiguration, string jsonString)
        {
            var algorithm = AlgorithmSelectionComponent.GetAlgorithmByName(baseConfiguration.Algorithm);
            AlgorithmSelectionComponent.SetSelectedAlgorithm(baseConfiguration.Algorithm);
            ImageGeneratorConfigurationComponent.SetFromExternalConfiguration(baseConfiguration.GeneratorConfig);

            switch(baseConfiguration.ConfigClass)
            {
                case "DefaultMorphologicalAlgorithmConfiguration":
                    ApplyConfiguration<DefaultMorphologicalAlgorithmConfiguration>(algorithm, jsonString);
                    break;
                case "EmptyMorphologicalAlgorithmConfiguration":
                    ApplyConfiguration<EmptyMorphologicalAlgorithmConfiguration>(algorithm, jsonString);
                    break;
                case "CompassEdgeConfiguration":
                    ApplyConfiguration<CompassEdgeConfiguration>(algorithm, jsonString);
                    break;
                default:
                    throw new Exception("Unknown configuration type");
            }
        }

        private void ApplyConfiguration<T>(IAlgorithm algorithm, string jsonString) where T : IMorphologicalAlgorithmConfiguration
        {
            Configuration<T> config = JsonConvert.DeserializeObject<Configuration<T>>(jsonString, new JsonConverter[] { new ColorJsonConverter(), new CustomConfigConverter() });
            IMorphologicalAlgorithmConfiguration deserializedConfig = config.Config;
            AlgorithmSelectionComponent.SetCurrentConfiguration(algorithm, deserializedConfig);
            GeneratorConfiguration generatorConfiguration = config.GeneratorConfig;
            ImageGeneratorConfigurationComponent.SetFromExternalConfiguration(generatorConfiguration);
        }

        private void SaveConfiguratioButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Title = "Save configuration to disk",
                Filter = "Json file (*.json)|*.json",
                FilterIndex = 1,
                RestoreDirectory = true
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                var config = AlgorithmSelectionComponent.GetCurrentConfiguration();
                var configToSave = new Configuration<IMorphologicalAlgorithmConfiguration>()
                {
                    Algorithm = AlgorithmSelectionComponent.GetSelectedAlgorithm().Name,
                    Config = config,
                    ConfigClass = config.GetType().Name,
                    GeneratorConfig = (GeneratorConfiguration)ImageGeneratorConfigurationComponent.GetConfiguration()
                };
                var options = new JsonSerializerOptions()
                {
                    WriteIndented = true,
                    MaxDepth = 10
                };
                var jsonString = System.Text.Json.JsonSerializer.Serialize(configToSave, options);
                File.WriteAllText(saveFileDialog.FileName, jsonString);
            }
        }
    }

    public class Configuration<T> : BaseConfiguration where T: IMorphologicalAlgorithmConfiguration
    {
        public T Config { get; set; }
    }   

    public class BaseConfiguration
    {
        public string Algorithm { get; set; }
        
        public string ConfigClass { get; set; }

        public GeneratorConfiguration GeneratorConfig { get; set; }
    }
}
