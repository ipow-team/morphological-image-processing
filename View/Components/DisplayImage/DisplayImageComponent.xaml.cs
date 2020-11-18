using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace morphological_image_processing_wpf.View.Components.DisplayImage
{
    /// <summary>
    /// Interaction logic for DisplayImageComponent.xaml
    /// </summary>
    public partial class DisplayImageComponent : UserControl
    {
        public DisplayImageComponent()
        {
            InitializeComponent();
            DataContext = this;
            BeforeImageDownload.Visibility = System.Windows.Visibility.Hidden;
            AfterImageDownload.Visibility = System.Windows.Visibility.Hidden;
        }

        public void SetBeforeImage(string path)
        {
            SetBeforeImage(new BitmapImage(new Uri(path)));
        }

        public void SetBeforeImage(BitmapSource imageSource)
        {
            if(imageSource == null)
            {
                BeforeImageDownload.Visibility = System.Windows.Visibility.Hidden;
            } else
            {
                BeforeImageDownload.Visibility = System.Windows.Visibility.Visible;
            }
            SetImage(BeforeImage, imageSource);
        }

        private void SetImage(System.Windows.Controls.Image imageComponent, BitmapSource imageSource)
        {
            imageComponent.Source = imageSource;
        }

        public Bitmap GetBeforeImage()
        {
            return GetImage(BeforeImage);
        }


        private Bitmap GetImage(System.Windows.Controls.Image imageComponent)
        {
            if(imageComponent.Source == null)
                return null;
            return GetImageFromSource(imageComponent.Source);
        }

        private Bitmap GetImageFromSource(ImageSource imageSource)
        {
            Bitmap bitmap;
            using (var memStream = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                BitmapSource source = imageSource as BitmapSource;
                encoder.Frames.Add(BitmapFrame.Create(source));
                encoder.Save(memStream);
                memStream.Flush();
                bitmap = new Bitmap(memStream);
            }
            if(bitmap == null)
            {
                throw new Exception("Error loading image from source");
            }
            return bitmap;
        }

        public void SetAfterImageFromBitmap(Bitmap bitmap)
        {
            BitmapImage bitmapImage = GetBitMapImageFromBitMap(bitmap);
            SetAfterImage(bitmapImage);
        }

        public void SetAfterImage(BitmapSource imageSource)
        {
            if (imageSource == null)
            {
                AfterImageDownload.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                AfterImageDownload.Visibility = System.Windows.Visibility.Visible;
            }
            SetImage(AfterImage, imageSource);
        }

        private BitmapImage GetBitMapImageFromBitMap(Bitmap bitmap)
        {
            BitmapImage bitmapImage;
            using (var memStream = new MemoryStream())
            {
                new Bitmap(bitmap).Save(memStream, ImageFormat.Png);
                memStream.Position = 0;

                bitmapImage = new BitmapImage();

                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memStream;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

            }
            if(bitmapImage == null)
            {
                throw new Exception("Error converting Bitmap to BitmapImage");
            }
            return bitmapImage;
        }

        private void BeforeImageDownload_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SetImageToDisk(BeforeImage.Source);
        }

        private void SetImageToDisk(ImageSource source)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Title = "Save image to disk",
                Filter = "Png file (*.png)|*.png",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using(var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(source as BitmapSource));
                    encoder.Save(fileStream);
                }
            }
        }

        private void AfterImageDownload_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SetImageToDisk(AfterImage.Source);
        }
    }
}
