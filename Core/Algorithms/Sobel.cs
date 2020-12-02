using MorphologicalImageProcessing.Core.Algorithms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace morphological_image_processing_wpf.Core.Algorithms
{
    class Sobel : MorphologicalAlgorithm<DefaultMorphologicalAlgorithmConfiguration>
    {
        protected override Bitmap Apply(Bitmap image, DefaultMorphologicalAlgorithmConfiguration configuration)
        {
            return DrawEdges(image);
        }

        public override string GetName()
        {
            return "Sobel";
        }

        private Bitmap DrawEdges(Bitmap original)
        {
            return Convolution(original, 
                               new double[,]
                                { { -1,  0,  1, },
                                  { -2,  0,  2, },
                                  { -1,  0,  1, }, },
                               new double[,]
                                { {  1,  2,  1, },
                                  {  0,  0,  0, },
                                  { -1, -2, -1, }, }, 
                               1.0, 0, true);
        }
    }
}
