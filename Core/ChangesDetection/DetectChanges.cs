using morphological_image_processing_wpf.Core.Algorithms;
using System.Collections.Generic;
using System.Drawing;

namespace morphological_image_processing_wpf.Core.ChangesDetection
{
    class DetectChanges 
    {
        readonly double brightnessThreshold = 0.5;

        public string Apply(Bitmap image)
        {
            ImageSplit imageSplit = new ImageSplit(image);
            List <DirectBitmap> shapes = imageSplit.LookForShapes();
            HolesDetection holesDetection = new HolesDetection(brightnessThreshold);
            NoiseDetection noiseDetection = new NoiseDetection(brightnessThreshold);
            ThickLinesDetection thickLinesDetection = new ThickLinesDetection(brightnessThreshold);
            ThinLinesDetection thinLinesDetection = new ThinLinesDetection(brightnessThreshold);

            bool hasNoise = noiseDetection.detectNoise(new DirectBitmap(image));

            if (hasNoise)
            {
                return "Possible closing. Others operators probably not possible.";
            }
            else
            {
                bool hasThinLine = false;

                foreach (DirectBitmap shape in shapes)
                {
                    if (thinLinesDetection.detectThinLines(shape))
                    {
                        hasThinLine = true;
                        break;
                    }
                }

                bool hasOnlyThickLines = true;

                foreach (DirectBitmap shape in shapes)
                {
                    if (!thickLinesDetection.detectThickLines(shape))
                    {
                        hasOnlyThickLines = false;
                        break;
                    }
                }

                bool hasHoles = false;

                foreach (DirectBitmap shape in shapes)
                {
                    if (holesDetection.detectHoles(shape))
                    {
                        hasHoles = true;
                        break;
                    }
                }

                string result;

                if (hasThinLine)
                {
                    if (hasOnlyThickLines || !hasHoles)
                        result = "Possible closing. Others operators probably not possible.";
                    else
                        result = "Possible erosion.";

                } else if (hasOnlyThickLines)
                {
                    if (hasHoles)
                        result = "Dilatation or opening possible.";
                    else
                        result = "Dilatation possible.";
                } else
                {
                    result = "Hard to recognize image features";
                }

                return result;
            }
        }
    }
}
