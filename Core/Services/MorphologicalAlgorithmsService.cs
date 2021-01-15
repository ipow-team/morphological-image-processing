using morphological_image_processing_wpf.Core.Algorithms;
using morphological_image_processing_wpf.Core.ChangesDetection;
using MorphologicalImageProcessing.Core.Algorithms;
using System.Collections.Generic;
namespace MorphologicalImageProcessing.Core.Services
{
    public class MorphologicalAlgorithmsService
    {
        private static readonly IList<IAlgorithm> _algoritms = new List<IAlgorithm>() {
            new Dilatation(),
            new Erosion(),
            new Opening(),
            new Closing(),
            new Sobel(),
            new Prewitt(),
            new Kirsch()
        };

        public IList<IAlgorithm> GetAllAlgorithms()
        {
            return _algoritms;
        }
    }
}
