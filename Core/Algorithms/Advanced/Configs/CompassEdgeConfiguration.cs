using morphological_image_processing_wpf.Core.Algorithms.Advanced.Filters;
using morphological_image_processing_wpf.Core.Algorithms.Filters;
using MorphologicalImageProcessing.Core.Algorithms;
using System.Collections.Generic;

namespace morphological_image_processing_wpf.Core.Algorithms.Advanced.Configs
{
    class CompassEdgeConfiguration : IMorphologicalAlgorithmConfiguration
    {
        private static readonly Dictionary<string, IBaseFilter> _filters = GetAllBaseFiltersByName();

        public IBaseFilter SelectedFilter { get; set; } = new Prewit3x3x4();

        public List<IBaseFilter> GetDefaultFilters() {
            List<IBaseFilter> result = new List<IBaseFilter>();
            result.AddRange(_filters.Values);
            return result;
        }

        public void SetValuesFrom(IMorphologicalAlgorithmConfiguration other)
        {
            if (typeof(CompassEdgeConfiguration).IsAssignableFrom(other.GetType()))
            {
                SetValuesFrom((CompassEdgeConfiguration)other);
            }
        }

        public void SetValuesFrom(CompassEdgeConfiguration other)
        {
            SelectedFilter = other.SelectedFilter;
        }

        private static IBaseFilter GetFilterByName(string name)
        {
            return _filters[name];
        }

        private static Dictionary<string, IBaseFilter> GetAllBaseFiltersByName()
        {
            Dictionary<string, IBaseFilter> result = new Dictionary<string, IBaseFilter>();
            List<IBaseFilter> baseFilters = GetAllBaseFilters();
            baseFilters.ForEach(filter => result.Add(filter.GetName(), filter));
            return result;
        }

        private static List<IBaseFilter> GetAllBaseFilters()
        {
            return new List<IBaseFilter>() {
                new Prewit3x3x4(),
                new Prewit3x3x8(),
                new Prewitt5x5x4(),
                new Sobel3x3x4(),
                new Sobel3x3x8(),
                new Sobel5x5x4(),
                new Kirsch3x3x4(),
                new Kirsch3x3x8()
            };
        }
    }
}
