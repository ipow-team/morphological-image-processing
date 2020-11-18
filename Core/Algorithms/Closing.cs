using System.Drawing;

namespace MorphologicalImageProcessing.Core.Algorithms
{
    class Closing : MorphologicalAlgorithm<DefaultMorphologicalAlgorithmConfiguration>
    {
        protected override Bitmap Apply(Bitmap image, DefaultMorphologicalAlgorithmConfiguration configuration)
        {
            return DrawEdges(image, configuration);
        }

        public override string GetName()
        {
            return "Closing";
        }

        public Bitmap DrawEdges(Bitmap image, DefaultMorphologicalAlgorithmConfiguration configuration)
        {
            Erosion erosion = new Erosion();
            Dilatation dilatation = new Dilatation();
            image = dilatation.DrawEdges(image, configuration);
            image = erosion.DrawEdges(image, configuration);

            return image;
        }
    }
}
