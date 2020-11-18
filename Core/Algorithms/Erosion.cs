using morphological_image_processing_wpf.Core.Algorithms;
using System;
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

        public Bitmap DrawEdges(Bitmap image, DefaultMorphologicalAlgorithmConfiguration configuration)
        {
            DirectBitmap edges = new DirectBitmap(image2);
            DirectBitmap image = new DirectBitmap(image2);
            int boxSize = 2 * (configuration.BoxSize) + 1;

            for (int i = 0; i < edges.Width; i++)
            {
                for (int j = 0; j < edges.Height; j++)
                {
                    Boolean is_edge = false;
                    for (int k = i - (boxSize - 1) / 2; k <= i + (boxSize - 1) / 2; k++)
                    {
                        if (k > 0 && k < edges.Width)
                        {
                            for (int l = j - (boxSize - 1) / 2; l <= j + (boxSize - 1) / 2; l++)
                            {
                                if (l > 0 && l < edges.Height)
                                {
                                    if (image.GetPixel(k, l).GetBrightness() < 0.02)
                                    {
                                        is_edge = true;
                                    }
                                }
                            }
                        }
                    }
                    if (is_edge && image.GetPixel(i, j).GetBrightness() > 0.02)
                    {
                        edges.SetPixel(i, j, configuration.LineColor);
                    }
                }
            }
            return edges.Bitmap;
        }
    }
}
