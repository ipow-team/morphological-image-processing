using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a picture";
            openFileDialog.Filter =   "All supported graphics|*.jpg;*.jpeg;*.png|" +
                                      "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                      "Portable Network Graphic (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                DisplayImageComponent.SetBeforeImage(openFileDialog.FileName);
                DisplayImageComponent.SetAfterImage(null);
            }
        }

        private void StartProcessingButton_Click(object sender, RoutedEventArgs e)
        {
            Bitmap beforeImage = DisplayImageComponent.GetBeforeImage();
            IAlgorithm selectedAlgorithm = AlgorithmSelectionComponent.GetSelectedAlgorithm();
            IMorphologicalAlgorithmConfiguration currentConfiguration = AlgorithmSelectionComponent.getCurrentConfiguration();
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
                //Bitmap afterImage = selectedAlgorithm.Apply(beforeImage, currentConfiguration);
                Bitmap afterImage = DisplayImageComponent.GetBeforeImage();
                DisplayImageComponent.SetAfterImageFromBitmap(afterImage);
            }
        }

        private void ShowErrorDialog(string errorMessage)
        {
            MessageBox.Show(errorMessage, "OK", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        // Ustawiamy obrazek super
        // Process -> zgarnij Algorytm, Konfiguracje
        // Zgarnij załadowany obrazek / Jak nie ma to rzuć coś tam
        // Jak jest -> wrzuć do algorytmu asynchronicznie
        // Na callbacku -> ustaw AfterImage
    }
}
