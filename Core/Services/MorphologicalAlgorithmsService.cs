using MorphologicalImageProcessing.Core.Algorithms;
using System.Collections.Generic;
namespace MorphologicalImageProcessing.Core.Services
{
    class MorphologicalAlgorithmsService
    {
        private static IList<IAlgorithm> _algoritms = new List<IAlgorithm>() {
            new Dilatation(),
            new Erosion()
        };

        public IList<IAlgorithm> GetAllAlgorithms()
        {
            return _algoritms;
        }
    }
}
