using morphological_image_processing_wpf.Core.Algorithms.Filters;

namespace morphological_image_processing_wpf.Core.Algorithms.Advanced.Filters
{
    class Sobel5x5x4 : IBaseFilter
    {
        private static double[,] _baseKernel = new double[,]
        {
           {   -5,  -4,  0,   4,  5,  },
           {   -8, -10,  0,  10,  8,  },
           {  -10, -20,  0,  20, 10,  },
           {   -8, -10,  0,  10,  8,  },
           {   -5,  -4,  0,   4,  5,  }
        };

        private static double _rotation = 90;

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
            return "Sobel5x5x4";
        }

        public double GetRotation()
        {
            return _rotation;
        }
    }
}
