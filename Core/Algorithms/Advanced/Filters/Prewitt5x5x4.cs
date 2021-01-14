using morphological_image_processing_wpf.Core.Algorithms.Filters;

namespace morphological_image_processing_wpf.Core.Algorithms.Advanced.Filters
{
    class Prewitt5x5x4 : IBaseFilter
    {
        private static double[,] _baseKernel = new double[,]
         { {  -2, -1,  0,  1, 2,  },
           {  -2, -1,  0,  1, 2,  },
           {  -2, -1,  0,  1, 2,  },
           {  -2, -1,  0,  1, 2,  },
           {  -2, -1,  0,  1, 2,  }, };

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
            return "Prewitt5x5x4";
        }

        public double GetRotation()
        {
            return _rotation;
        }
    }
}
