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
            await RunAlgorithm(beforeImage, selectedAlgorithm, currentConfiguration);
        }

        private async Task RunAlgorithm(Bitmap beforeImage, IAlgorithm selectedAlgorithm, IMorphologicalAlgorithmConfiguration currentConfiguration)
        {
            StartProcessingButton.IsEnabled = false;
            LoadImageButton.IsEnabled = false;
            GenerateImageButton.IsEnabled = false;
            Bitmap afterImage = await Task.Run(() =>
            {
                return selectedAlgorithm.Apply(beforeImage, currentConfiguration);
            });
            SideBySideImagesComponent.SetAfterImageFromBitmap(afterImage);
            StartProcessingButton.IsEnabled = true;
            LoadImageButton.IsEnabled = true;
            GenerateImageButton.IsEnabled = true;
        }

        private void ShowErrorDialog(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error message", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void GenerateImageBtnClick(object sender, RoutedEventArgs e)
        {
            ImageGenerator imageGenerator = new ImageGenerator(500, 500);
            SideBySideImagesComponent.SetBeforeImage(imageGenerator.GeneratePicture(3, 5, 10));
        }
    }
}
