using morphological_image_processing_wpf.Core.Algorithms;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MorphologicalImageProcessing.Core.Algorithms
{
    class Dilatation : MorphologicalAlgorithm<DefaultMorphologicalAlgorithmConfiguration>
    {
        protected override Bitmap Apply(Bitmap image, DefaultMorphologicalAlgorithmConfiguration configuration)
        {
            return DrawEdges(image, configuration);
        }

        public override string GetName()
        {
            return "Dilatation";
        }

        private  Bitmap DrawEdges(Bitmap original, DefaultMorphologicalAlgorithmConfiguration configuration)
        {
            DirectBitmap edges = new DirectBitmap(original);
            DirectBitmap image = new DirectBitmap(original);

            for (int i = 0; i < edges.Width; i++)
            {
                for (int j = 0; j < edges.Height; j++)
                {
                    Boolean isEdge = false;
                    List<Tuple<int, int>> pointsToCheck = CalculatePointsToCheck(configuration.StructuralElementPoints, configuration.Center);

                    foreach (Tuple<int, int> point in pointsToCheck)
                    {
                        int x = i - point.Item1;
                        int y = j - point.Item2;

                        if(x >= 0 && x < edges.Width && y >= 0 && y < edges.Height && image.GetPixel(x, y).GetBrightness() < configuration.BrightnessThreshold) {
                            isEdge = true;
                        }
                    }
                    
                    if (isEdge && image.GetPixel(i, j).GetBrightness() > configuration.BrightnessThreshold)
                    {
                        edges.SetPixel(i, j, configuration.LineColor);
                    }
                }
            }
            return edges.Bitmap;
        }
    }
}
