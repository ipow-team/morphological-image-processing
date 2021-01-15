using morphological_image_processing_wpf.Core.Algorithms.Advanced.Configs;
using morphological_image_processing_wpf.Core.Algorithms.Advanced.Convolutions;
using MorphologicalImageProcessing.Core.Algorithms;
using System.Drawing;

namespace morphological_image_processing_wpf.Core.Algorithms.Advanced
{
    class CompassEdgeDetection : MorphologicalAlgorithm<CompassEdgeConfiguration>
    {
        private readonly IConvolution _convolution = new CompassConvolution();

        public override string GetName()
        {
            return "Compass Edge Detection";
        }

        protected override Bitmap Apply(Bitmap image, CompassEdgeConfiguration configuration)
        {
            if (configuration.GetSelectedFilter() == null)
                return image;
            return _convolution.Apply(image, configuration.GetSelectedFilter());
        }
    }
}
