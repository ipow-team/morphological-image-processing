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
            this.DataContext = this;
        }

        public void SetBeforeImage(string path)
        {
            SetBeforeImage(new BitmapImage(new Uri(path)));
        }

        public void SetBeforeImage(BitmapImage image)
        {
            SetImage(BeforeImage, image);
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
                var encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(imageSource as BitmapSource));
                encoder.Save(memStream);
                memStream.Flush();
                bitmap =  Bitmap.FromStream(memStream) as Bitmap;
            }
            if(bitmap == null)
            {
                throw new Exception("Error loading image from source");
            }
            return bitmap;
        }

        private void SetImage(System.Windows.Controls.Image imageComponent, BitmapImage image)
        {
            imageComponent.Source = image;
        }

        public void SetAfterImageFromBitmap(Bitmap bitmap)
        {
            BitmapImage bitmapImage = GetBitMapImageFromBitMap(bitmap);
            SetAfterImage(bitmapImage);
        }

        private BitmapImage GetBitMapImageFromBitMap(Bitmap bitmap)
        {
            BitmapImage bitmapImage;
            using (var memStream = new MemoryStream())
            {
                new Bitmap(bitmap).Save(memStream, bitmap.RawFormat);
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

        public void SetAfterImage(BitmapImage image)
        {
            SetImage(AfterImage, image);
        }
    }
}
