using MorphologicalImageProcessing.Core.Algorithms;
using System.Collections.Generic;
namespace MorphologicalImageProcessing.Core.Services
{
    public class MorphologicalAlgorithmsService
    {
        private static readonly IList<IAlgorithm> _algoritms = new List<IAlgorithm>() {
            new Dilatation(),
            new Erosion()
        };

        public IList<IAlgorithm> GetAllAlgorithms()
        {
            return _algoritms;
        }
    }
}
