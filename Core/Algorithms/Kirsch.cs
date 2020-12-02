using MorphologicalImageProcessing.Core.Algorithms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace morphological_image_processing_wpf.Core.Algorithms
{
    class Kirsch : MorphologicalAlgorithm<DefaultMorphologicalAlgorithmConfiguration>
    {
        protected override Bitmap Apply(Bitmap image, DefaultMorphologicalAlgorithmConfiguration configuration)
        {
            return DrawEdges(image);
        }

        public override string GetName()
        {
            return "Kirsch";
        }

        private Bitmap DrawEdges(Bitmap original)
        {
            return Convolution(original,
                               new double[,]
                                { {  5,  5,  5, },
                                  { -3,  0, -3, },
                                  { -3, -3, -3, }, },
                               new double[,]
                                { { 5, -3, -3, },
                                  { 5,  0, -3, },
                                  { 5, -3, -3, }, },
                               1.0, 0, true);
        }
    }
}
