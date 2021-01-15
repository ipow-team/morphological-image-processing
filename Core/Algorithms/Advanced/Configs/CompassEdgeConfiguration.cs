using morphological_image_processing_wpf.Core.Algorithms.Advanced.Filters;
using morphological_image_processing_wpf.Core.Algorithms.Filters;
using MorphologicalImageProcessing.Core.Algorithms;
using System.Collections.Generic;

namespace morphological_image_processing_wpf.Core.Algorithms.Advanced.Configs
{
    public class CompassEdgeConfiguration : IMorphologicalAlgorithmConfiguration
    {
        private static Dictionary<string, IBaseFilter> _filters = GetAllBaseFiltersByName();
        private IBaseFilter _selectedFilter;

        public IBaseFilter SelectedFilter
        {
            set
            {
                _selectedFilter = value;
            }
        }

        public string SelectedFilterName {
            get
            {
                return _selectedFilter == null ? null : _selectedFilter.GetName();
            }
            set
            {
                _selectedFilter = _filters[value];
            }
        }

        public CustomFilter CustomFilterConfig
        {
            get
            {
                if (_selectedFilter == null || !_selectedFilter.IsMutable())
                    return null;
                return new CustomFilter(_selectedFilter.GetBaseKernel(), _selectedFilter.GetRotation(), _selectedFilter.GetGridSize());
            }
            set
            {
                if(value != null && value.GetName() == "Custom")
                {
                    CustomFilter customFilter = new CustomFilter(value.GetBaseKernel(), value.GetRotation(), value.GetGridSize());
                    if (_filters.ContainsKey(customFilter.GetName()))
                        _filters.Remove(customFilter.GetName());
                    _filters.Add(customFilter.GetName(), customFilter);
                    _selectedFilter = customFilter;
                }
            }
        }

        private CompassEdgeConfiguration()
        {
        }
        private CompassEdgeConfiguration(Dictionary<string, IBaseFilter> filtersByName, IBaseFilter selectedFilter)
        {
            _selectedFilter = selectedFilter;
        }
        public static CompassEdgeConfiguration create()
        {
            return new CompassEdgeConfiguration(GetAllBaseFiltersByName(), new Prewit3x3x4());
        }

        public IBaseFilter GetSelectedFilter()
        {
            return _selectedFilter;
        }

        public IList<IBaseFilter> GetDefaultFilters() {
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
            SelectedFilter = other._selectedFilter;
            SelectedFilterName = other.SelectedFilterName;
            CustomFilterConfig = other.CustomFilterConfig;
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
                new Kirsch3x3x8(),
                CustomFilter.create()
            };
        }
    }
}
