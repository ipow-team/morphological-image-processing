using System.Drawing;

namespace MorphologicalImageProcessing.Core.Algorithms
{
    class Closing : MorphologicalAlgorithm<DefaultMorphologicalAlgorithmConfiguration>
    {
        private Erosion _erosion = new Erosion();
        private Dilatation _dilatation = new Dilatation();

        protected override Bitmap Apply(Bitmap image, DefaultMorphologicalAlgorithmConfiguration configuration)
        {
            return DrawEdges(image, configuration);
        }

        public override string GetName()
        {
            return "Closing";
        }

        private Bitmap DrawEdges(Bitmap image, DefaultMorphologicalAlgorithmConfiguration configuration)
        {
            image = _dilatation.Apply(image, configuration);
            image = _erosion.Apply(image, configuration);
            return image;
        }
    }
}
