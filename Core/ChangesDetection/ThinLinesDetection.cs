using morphological_image_processing_wpf.Core.Algorithms;
using System;

namespace morphological_image_processing_wpf.Core.ChangesDetection
{
    class ThinLinesDetection
    {
        readonly double brightnessThreshold;

        public ThinLinesDetection(double brightnessThreshold)
        {
            this.brightnessThreshold = brightnessThreshold;
        }

        public bool detectThinLines(DirectBitmap image)
        {
            int imageWidth = image.Width;
            int imageHeight = image.Height;
            bool hasThinLine = false;

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    if(image.GetPixel(i, j).GetBrightness() < brightnessThreshold && CheckNeighboorhoud(i, j, imageWidth, imageHeight, image))
                    {
                        hasThinLine = true;
                        i = imageWidth;
                        j = imageHeight;
                    }
                }
            }

            return hasThinLine;
        }


        private bool CheckNeighboorhoud(int coorX, int coorY, int imageWidth, int imageHeight, DirectBitmap image)
        {
            bool hasThinLine = false;

            if ((coorX == 0 && coorY == 0) || (coorX == imageWidth - 1 && coorY == 0) || (coorX == 0 && coorY == imageHeight - 1) || (coorX == imageWidth - 1 && coorY == imageWidth - 1))
            {
                for (int i = Math.Max(coorX - 1, 0); i <= Math.Min(coorX + 1, imageWidth - 1); i++)
                {
                    for (int j = Math.Max(coorY - 1, 0); j <= Math.Min(coorY + 1, imageHeight - 1); j++)
                    {
                        if(image.GetPixel(i, j).GetBrightness() > brightnessThreshold)
                        {
                            hasThinLine = true;
                            i = imageWidth;
                            j = imageHeight;
                        }
                    }
                }
            }
            else if (!(coorX == 0 || coorY == 0 || coorX == imageWidth - 1 || coorY == imageHeight - 1))
            {
                if ((image.GetPixel(coorX - 1, coorY).GetBrightness() > brightnessThreshold && image.GetPixel(coorX + 1, coorY).GetBrightness() > brightnessThreshold) 
                    || (image.GetPixel(coorX, coorY + 1).GetBrightness() > brightnessThreshold && image.GetPixel(coorX, coorY - 1).GetBrightness() > brightnessThreshold))
                    hasThinLine = true;
            }
            return hasThinLine;
        }
    }
}
