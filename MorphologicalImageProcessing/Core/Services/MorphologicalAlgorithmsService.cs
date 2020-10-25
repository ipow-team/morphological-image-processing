using MorphologicalImageProcessing.Core.Algorithms;
using System.Collections.Generic;
namespace MorphologicalImageProcessing.Core.Services
{
    class MorphologicalAlgorithmsService
    {
        private static IList<Algorithm> _algoritms = new List<Algorithm>() {
            new Dilatation(),
            new Erosion()
        };

        public IList<Algorithm> GetAllAlgorithms()
        {
            return _algoritms;
        }
    }
}
