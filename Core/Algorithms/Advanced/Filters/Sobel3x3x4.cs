using morphological_image_processing_wpf.Core.Algorithms.Filters;

namespace morphological_image_processing_wpf.Core.Algorithms.Advanced.Filters
{
    class Sobel3x3x4 : IBaseFilter
    {
        private static readonly double[,] _baseKernel = new double[,]
        {
           {  -1,  0,  1,  },
           {  -2,  0,  2,  },
           {  -1,  0,  1,  }
        };

        private static readonly double _rotation = 90;

        public double[,] GetBaseKernel()
        {
            return _baseKernel;
        }

        public double[,,] GetKernel()
        {
            return IBaseFilter.RotateMatrix(_baseKernel, _rotation);
        }

        public string GetName()
        {
            return "Sobel3x3x4";
        }

        public double GetRotation()
        {
            return _rotation;
        }

        public GridSizes GetGridSize()
        {
            return GridSizes.Small;
        }
    }
}
