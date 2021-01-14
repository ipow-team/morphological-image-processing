using morphological_image_processing_wpf.Core.Algorithms.Filters;
using System.Drawing;

namespace morphological_image_processing_wpf.Core.Algorithms.Advanced.Convolutions
{
    interface IConvolution
    {
        Bitmap Apply(Bitmap originalBitmap, IBaseFilter filter, bool convertToGrayScale = false)
        {
            return Apply(originalBitmap, filter.GetKernel(), convertToGrayScale);
        }

        Bitmap Apply(Bitmap originalBitmap, double[,,] filterMatrix, bool convertToGrayScale = false);
    }
}
