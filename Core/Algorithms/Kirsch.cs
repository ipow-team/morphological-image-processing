using MorphologicalImageProcessing.Core.Algorithms;
using System.Drawing;

namespace morphological_image_processing_wpf.Core.Algorithms
{
    class Kirsch : ConvolutionMorphologicalAlgorithm<EmptyMorphologicalAlgorithmConfiguration>
    {
        protected override Bitmap Apply(Bitmap image, EmptyMorphologicalAlgorithmConfiguration configuration)
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
