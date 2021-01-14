using morphological_image_processing_wpf.Core.Algorithms.Filters;

namespace morphological_image_processing_wpf.Core.Algorithms.Advanced.Filters
{
    class CustomFilter : IBaseFilter
    {
        private double[,] _baseKernel;
        private double _rotation;

        CustomFilter(double[,] baseKernel, double rotation)
        {
            _baseKernel = baseKernel;
            _rotation = rotation;
        }

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
            return "Custom";
        }

        public double GetRotation()
        {
            return _rotation;
        }
    }
}
