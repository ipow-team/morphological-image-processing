using System;
using System.Drawing;

namespace MorphologicalImageProcessing.Core.Algorithms
{
    class Dilatation : MorphologicalAlgorithm<EmptyMorphologicalAlgorithmConfiguration>
    {
        protected override Bitmap Apply(Bitmap image, EmptyMorphologicalAlgorithmConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            return "Dilatation";
        }
    }
}
