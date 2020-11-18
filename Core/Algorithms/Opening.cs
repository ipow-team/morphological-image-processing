using System.Drawing;

namespace MorphologicalImageProcessing.Core.Algorithms
{
    class Opening : MorphologicalAlgorithm<DefaultMorphologicalAlgorithmConfiguration>
    {
        protected override Bitmap Apply(Bitmap image, DefaultMorphologicalAlgorithmConfiguration configuration)
        {
            return DrawEdges(image, configuration);
        }

        public override string GetName()
        {
            return "Opening";
        }

        public Bitmap DrawEdges(Bitmap image, DefaultMorphologicalAlgorithmConfiguration configuration)
        {
            Erosion erosion = new Erosion();
            Dilatation dilatation = new Dilatation();
            image = erosion.DrawEdges(image, configuration);
            image = dilatation.DrawEdges(image, configuration);

            return image;
        }
    }
}
