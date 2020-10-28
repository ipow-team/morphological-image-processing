using System;
using System.Drawing;

namespace MorphologicalImageProcessing.Core.Algorithms
{
    class Erosion : MorphologicalAlgorithm<DefaultMorphologicalAlgorithmConfiguration>
    {
        private Bitmap image;
        private DefaultMorphologicalAlgorithmConfiguration configuration;

        protected override Bitmap Apply(Bitmap image, DefaultMorphologicalAlgorithmConfiguration configuration)
        {
            this.image = image;
            this.configuration = configuration;

            return this.image;
        }

        public override string GetName()
        {
            return "Erosion";
        }

        public Bitmap DrawEdges()
        {
            Bitmap edges = new Bitmap(image.Width, image.Height);
            int boxSize = configuration.BoxSize;

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
                        edges.SetPixel(i, j, Color.Black);
                    }
                }
            }
            return edges;
        }
    }
}
