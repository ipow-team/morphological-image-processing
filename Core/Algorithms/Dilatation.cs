using System;
using System.Drawing;

namespace MorphologicalImageProcessing.Core.Algorithms
{
    class Dilatation : MorphologicalAlgorithm<DefaultMorphologicalAlgorithmConfiguration>
    {
        protected override Image Apply(Image image, DefaultMorphologicalAlgorithmConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            return "Dilatation";
        }
    }
}
