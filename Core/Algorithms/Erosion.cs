using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MorphologicalImageProcessing.Core.Algorithms
{
    class Erosion : MorphologicalAlgorithm<ErosionConfiguration>
    {

        protected override Image Apply(Image image, ErosionConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public override string GetName()
        {
            return "Erosion";
        }
    }

    class ErosionConfiguration : MorphologicalAlgorithmConfiguration
    {

    }

}
