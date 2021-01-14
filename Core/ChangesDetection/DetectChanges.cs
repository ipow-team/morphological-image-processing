using MorphologicalImageProcessing.Core.Algorithms;
using System.Drawing;

namespace morphological_image_processing_wpf.Core.ChangesDetection
{
    class DetectChanges : MorphologicalAlgorithm<EmptyMorphologicalAlgorithmConfiguration>
    {
        public override string GetName()
        {
            return "Detect changes";
        }

        protected override Bitmap Apply(Bitmap image, EmptyMorphologicalAlgorithmConfiguration configuration)
        {
            ImageSplit imageSplit = new ImageSplit(image);
            return imageSplit.LookForShapes()[0].Bitmap;
        }
    }
}
