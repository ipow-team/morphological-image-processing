using System;
using System.Drawing;

namespace MorphologicalImageProcessing.Core.Algorithms
{
    class Dilatation : MorphologicalAlgorithm<DilatationConfiguration>
    {
        protected override Image Apply(Image image, DilatationConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            return "Dilatation";
        }
    }

    class DilatationConfiguration : MorphologicalAlgorithmConfiguration
    {

    }

}
