using morphological_image_processing_wpf.Core.Algorithms;
using System;
using System.Diagnostics;

namespace morphological_image_processing_wpf.Core.ChangesDetection
{
    class NoiseDetection
    {
        double brightnessThreshold;

        public NoiseDetection(double brightnessThreshold)
        {
            this.brightnessThreshold = brightnessThreshold;
        }


        public bool detectNoise(DirectBitmap image)
        {
            int noiseCounter = 0;

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    bool isPixelBlack;

                    if (image.GetPixel(i, j).GetBrightness() < brightnessThreshold)
                    {
                        isPixelBlack = true;
                    } else
                    {
                        isPixelBlack = false;
                    }

                    if (CheckNeighboorhoud(i, j, image.Width, image.Height, image, isPixelBlack))
                        noiseCounter++;
                }
            }

            if (noiseCounter >= 25)
                return true;
            else
                return false;
        }

        private bool CheckNeighboorhoud(int coorX, int coorY, int imageWidth, int imageHeight, DirectBitmap image, bool isPixelBlack)
        {
            bool isNoise = true;

            for (int i = Math.Max(coorX - 1, 0); i <= Math.Min(coorX + 1, imageWidth - 1); i++)
            {
                for (int j = Math.Max(coorY - 1, 0); j <= Math.Min(coorY + 1, imageHeight - 1); j++)
                {
                    if ((i != coorX || j != coorY) && ((isPixelBlack && image.GetPixel(i, j).GetBrightness() < brightnessThreshold) || (!isPixelBlack && image.GetPixel(i, j).GetBrightness() >= brightnessThreshold)))
                    {
                        isNoise = false;
                        i = imageWidth;
                        j = imageHeight;
                    }
                }
            }
            
            return isNoise;
        }
    }
}
