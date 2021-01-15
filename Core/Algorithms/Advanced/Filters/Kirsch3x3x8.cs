using morphological_image_processing_wpf.Core.Algorithms.Filters;

namespace morphological_image_processing_wpf.Core.Algorithms.Advanced.Filters
{
    class Kirsch3x3x8 : IBaseFilter
    {
        private static double[,] _baseKernel = new double[,]
        {
           {  -3, -3,  5,  },
           {  -3,  0,  5,  },
           {  -3, -3,  5,  }
        };

        private static double _rotation = 45;

        private static GridSizes _gridSize = GridSizes.Small;

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
            return "Kirsch3x3x8";
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
