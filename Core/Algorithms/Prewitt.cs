using MorphologicalImageProcessing.Core.Algorithms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace morphological_image_processing_wpf.Core.Algorithms
{
    class Prewitt : MorphologicalAlgorithm<DefaultMorphologicalAlgorithmConfiguration>
    {
        protected override Bitmap Apply(Bitmap image, DefaultMorphologicalAlgorithmConfiguration configuration)
        {
            return DrawEdges(image);
        }

        public override string GetName()
        {
            return "Prewitt";
        }

        private Bitmap DrawEdges(Bitmap original)
        {
            return Convolution(original,
                               new double[,]
                                { { -1,  0,  1, },
                                  { -1,  0,  1, },
                                  { -1,  0,  1, }, },
                               new double[,]
                                { {  1,  1,  1, },
                                  {  0,  0,  0, },
                                  { -1, -1, -1, }, },
                               1.0, 0, true);
        }
    }
}
