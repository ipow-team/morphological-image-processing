using System;
using System.Drawing;

namespace MorphologicalImageProcessing.Core.Algorithms
{
    class Dilatation : MorphologicalAlgorithm<EmptyMorphologicalAlgorithmConfiguration>
    {
        protected override Image Apply(Image image, EmptyMorphologicalAlgorithmConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            return "Dilatation";
        }
    }
}
