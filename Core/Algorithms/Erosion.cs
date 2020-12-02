using morphological_image_processing_wpf.Core.Algorithms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace MorphologicalImageProcessing.Core.Algorithms
{
    class Erosion : MorphologicalAlgorithm<DefaultMorphologicalAlgorithmConfiguration>
    {
        protected override Bitmap Apply(Bitmap image, DefaultMorphologicalAlgorithmConfiguration configuration)
        {
            return DrawEdges(image, configuration);
        }

        public override string GetName()
        {
            return "Erosion";
        }

        private Bitmap DrawEdges(Bitmap original, DefaultMorphologicalAlgorithmConfiguration configuration)
        {
            DirectBitmap edges = new DirectBitmap(original);
            DirectBitmap image = new DirectBitmap(original);

            for (int i = 0; i < edges.Width; i++)
            {
                for (int j = 0; j < edges.Height; j++)
                {
                    Boolean is_edge = false;
                    List<Tuple<int, int>> pointsToCheck = CalculatePointsToCheck(configuration.getStructuralElementConfiguration(), Tuple.Create(1, 1));

                    foreach(Tuple<int, int> point in pointsToCheck)
                    {
                        int x = i - point.Item1;
                        int y = j - point.Item2;

                        if(x >= 0 && x < edges.Width && y >= 0 && y < edges.Height && image.GetPixel(x, y).GetBrightness() < configuration.BrightnessThreshold) {
                            is_edge = true;
                        }
                    }
                    
                    if (is_edge && image.GetPixel(i, j).GetBrightness() > configuration.BrightnessThreshold)
                    {
                        edges.SetPixel(i, j, configuration.LineColor);
                    }
                }
            }
            return edges.Bitmap;
        }
        private List<Tuple<int, int>> CalculatePointsToCheck(List<Tuple<int, int>> points, Tuple<int, int> centralPoint) {
            List<Tuple<int, int>> newPoints = new List<Tuple<int, int>>();

            foreach (Tuple<int, int> point in points) 
            {
                newPoints.Add(Tuple.Create(point.Item1 - centralPoint.Item1, point.Item2 - centralPoint.Item2));
            }

            return newPoints;
        }

    }
}
