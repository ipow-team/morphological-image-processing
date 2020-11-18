using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using morphological_image_processing_wpf.Core.Generator;
using MorphologicalImageProcessing.Core.Algorithms;

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

        private void StartProcessingButton_Click(object sender, RoutedEventArgs e)
        {
            Bitmap beforeImage = SideBySideImagesComponent.GetBeforeImage();
            IAlgorithm selectedAlgorithm = AlgorithmSelectionComponent.GetSelectedAlgorithm();
            IMorphologicalAlgorithmConfiguration currentConfiguration = AlgorithmSelectionComponent.GetCurrentConfiguration();
            if (beforeImage == null)
            {
                ShowErrorDialog("You have to load an image");
            } else if (selectedAlgorithm == null)
            {
                ShowErrorDialog("You have to select an algorithm");
            } else if (currentConfiguration == null) {
                ShowErrorDialog("Algorithm has to have atleast empty configuration.");
            } else
            {
                StartProcessingButton.IsEnabled = false;
                LoadImageButton.IsEnabled = false;
                Task.Run(() =>
                {
                    App.Current.Dispatcher.Invoke(() => RunAlgorithm(beforeImage, selectedAlgorithm, currentConfiguration));
                });
            }
        }

        private void RunAlgorithm(Bitmap beforeImage, IAlgorithm selectedAlgorithm, IMorphologicalAlgorithmConfiguration currentConfiguration)
        {
            Bitmap afterImage = selectedAlgorithm.Apply(beforeImage, currentConfiguration);
            SideBySideImagesComponent.SetAfterImageFromBitmap(afterImage);
            StartProcessingButton.IsEnabled = true;
            LoadImageButton.IsEnabled = true;
        }

        private void ShowErrorDialog(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error message", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void GenerateImageBtnClick(object sender, RoutedEventArgs e)
        {
            ImageGenerator generator = new ImageGenerator(250, 250);
            SideBySideImagesComponent.SetBeforeImage(generator.GeneratePicture(5, 10, 10));
        }
    }
}
