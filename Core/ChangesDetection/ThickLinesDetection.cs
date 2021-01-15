using morphological_image_processing_wpf.Core.Algorithms;
using System;

namespace morphological_image_processing_wpf.Core.ChangesDetection
{
    class ThickLinesDetection
    {
        readonly double brightnessThreshold;

        public ThickLinesDetection(double brightnessThreshold)
        {
            this.brightnessThreshold = brightnessThreshold;
        }


        public bool detectThickLines(DirectBitmap image)
        {
            int imageWidth = image.Width;
            int imageHeight = image.Height;
            bool isThick = false;

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    if (image.GetPixel(i, j).GetBrightness() < brightnessThreshold && CheckNeighboorhoud(i, j, imageWidth, imageHeight, image))
                    {
                        isThick = true;
                        i = imageWidth;
                        j = imageHeight;
                    }
                }
            }

            return isThick;
        }


        private bool CheckNeighboorhoud(int coorX, int coorY, int imageWidth, int imageHeight, DirectBitmap image)
        {
            bool isThick = false;

            if (!(coorX == 0 || coorY == 0 || coorX == imageWidth - 1 || coorY == imageHeight - 1))
            { 
                if ((image.GetPixel(coorX - 1, coorY).GetBrightness() < brightnessThreshold && image.GetPixel(coorX + 1, coorY).GetBrightness() < brightnessThreshold)
                    && (image.GetPixel(coorX, coorY + 1).GetBrightness() < brightnessThreshold && image.GetPixel(coorX, coorY - 1).GetBrightness() < brightnessThreshold))
                    isThick = true;
            }
            return isThick;
        }
    }
}
