using System;
using System.Windows.Controls;

namespace MorphologicalImageProcessing.Core.Algorithms
{
    class Erosion : MorphologicalAlgorithm<DefaultMorphologicalAlgorithmConfiguration>
    {

        protected override Image Apply(Image image, DefaultMorphologicalAlgorithmConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            return "Erosion";
        }
    }
}
