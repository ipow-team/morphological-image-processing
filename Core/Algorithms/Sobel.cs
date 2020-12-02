using MorphologicalImageProcessing.Core.Algorithms;
using System.Drawing;

namespace morphological_image_processing_wpf.Core.Algorithms
{
    class Sobel : ConvolutionMorphologicalAlgorithm<EmptyMorphologicalAlgorithmConfiguration>
    {
        protected override Bitmap Apply(Bitmap image, EmptyMorphologicalAlgorithmConfiguration configuration)
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
