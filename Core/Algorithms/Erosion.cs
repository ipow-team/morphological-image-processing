using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

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
