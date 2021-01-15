using morphological_image_processing_wpf.Core.Algorithms.Advanced.Configs;
using morphological_image_processing_wpf.Core.Algorithms.Advanced.Filters;
using morphological_image_processing_wpf.Core.Algorithms.Filters;
using MorphologicalImageProcessing.Core.Algorithms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace morphological_image_processing_wpf.View.Components.AlgorithmsSelection.Configurations
{
    /// <summary>
    /// Interaction logic for CompassEdgeConfigurationComponent.xaml
    /// </summary>
    partial class CompassEdgeConfigurationComponent : UserControl, IConfigurationComponent
    {
        private readonly CompassEdgeConfiguration _compassConfiguration = CompassEdgeConfiguration.create();

        private readonly FilterSelectionViewModel _filterSelectionViewModel;

        private readonly PositionedTextBoxGrid _positiontedTextBoxGrid;

        public CompassEdgeConfigurationComponent()
        {
            InitializeComponent();
            _filterSelectionViewModel = new FilterSelectionViewModel(
                _compassConfiguration,
                SetFilter,
                SetGridSize
            );
            ConfigureGridSizeSelectionComboBox();
            _positiontedTextBoxGrid = GetPositionedTextBoxGrid();
            ConfigureFilterSelectionComboBox();
            SetFilter(_filterSelectionViewModel.SelectedFilter);
        }

        private PositionedTextBoxGrid GetPositionedTextBoxGrid()
        {
            GridSizes selectedGridSize = ((GridSizeEntry)GridSizeComboBox.SelectedItem).GridSize;
            PositionedTextBoxGrid positionedTextBoxGrid = new PositionedTextBoxGrid(TextBoxGrid, GetShape(selectedGridSize), "0");
            return positionedTextBoxGrid;
        }

        Tuple<int, int> GetShape(GridSizes gridSizes)
        {
            switch(gridSizes)
            {
                case GridSizes.Big:
                    return Tuple.Create(5, 5);
                case GridSizes.Small:
                default:
                    return Tuple.Create(3, 3);
            }
        }

        private void SetFilter(IBaseFilter filter)
        {
            FilterEntry filterEntry = _filterSelectionViewModel.findByFilterName(filter.GetName());
            MethodComboBox.SelectedItem = filterEntry;
            RotationSlider.Value = filter.GetRotation();
            GridSizeComboBox.SelectedItem = _filterSelectionViewModel.findByGridSize(filter.GetGridSize());
            PopulateKernelFromFilter(_positiontedTextBoxGrid, filter);
            if (!filter.IsMutable())
            {
                TextBoxGrid.IsEnabled = false;
                GridSizeComboBox.IsEnabled = false;
                RotationSlider.IsEnabled = false;
            }
            else
            {
                TextBoxGrid.IsEnabled = true;
                GridSizeComboBox.IsEnabled = true;
                RotationSlider.IsEnabled = true;
            }
        }

        private void PopulateKernelFromFilter(PositionedTextBoxGrid textBoxGrid, IBaseFilter filter)
        {
            double[,] baseKernel = filter.GetBaseKernel();
            for(int i = 0; i < baseKernel.GetLength(0); i++)
            {
                for(int j = 0; j < baseKernel.GetLength(1); j++)
                {
                    string stringValue = baseKernel[i, j].ToString();
                    textBoxGrid.GetElement(i, j).SetValue(stringValue);
                }
            }
        }

        private void SetGridSize(GridSizes gridSizes)
        {

        }

        private void ConfigureFilterSelectionComboBox()
        {
            MethodComboBox.ItemsSource = _filterSelectionViewModel.FilterEntries;
            MethodComboBox.DisplayMemberPath = "Name";
            if (_filterSelectionViewModel.FilterEntries.Count > 0)
            {
                MethodComboBox.SelectedIndex = 0;
            }
        }

        private void ConfigureGridSizeSelectionComboBox()
        {
            GridSizeComboBox.ItemsSource = _filterSelectionViewModel.GridSizeEntries;
            GridSizeComboBox.DisplayMemberPath = "Description";
            if (_filterSelectionViewModel.GridSizeEntries.Count > 0)
            {
                GridSizeComboBox.SelectedIndex = 0;
            }
        }

        public IMorphologicalAlgorithmConfiguration GetConfiguration()
        {
            if(_filterSelectionViewModel.SelectedFilter.IsMutable())
            {
                double[,] kernel = GetKernel();
                double precision = GetCurrentRotation();
                GridSizes gridSize = _filterSelectionViewModel.SelectedGridSize;
                _compassConfiguration.SelectedFilter = new CustomFilter(kernel, precision, gridSize);
                return _compassConfiguration;
            }
            _compassConfiguration.SelectedFilter = _filterSelectionViewModel.SelectedFilter;
            return _compassConfiguration;
        }

        private double[,] GetKernel()
        {
            Tuple<int, int> gridShape = GetShape(_filterSelectionViewModel.SelectedGridSize);
            double[,] result = new double[gridShape.Item1, gridShape.Item2];

            for(int i = 0; i < gridShape.Item1; i++)
            {
                for(int j = 0; j < gridShape.Item2; j++)
                {
                    string value = _positiontedTextBoxGrid.GetElement(i, j).GetElement().Text;
                    result[i, j] = double.Parse(value);
                }
            }

            return result;
        }

        public Control GetControlComponent()
        {
            return this;
        }

        public void SetValuesFrom(IMorphologicalAlgorithmConfiguration other)
        {
            if (typeof(CompassEdgeConfiguration).IsAssignableFrom(other.GetType()))
            {
                SetValuesFrom((CompassEdgeConfiguration)other);
            }
        }

        private void SetValuesFrom(CompassEdgeConfiguration other)
        {
            _compassConfiguration.SetValuesFrom(other);
            SetFilter(other.GetSelectedFilter());
        }

        private void MethodComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(MethodComboBox.SelectedIndex != -1)
            {
                _filterSelectionViewModel.SelectedFilterEntry = (FilterEntry)MethodComboBox.SelectedItem;
            }
        }

        private void GridSizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(GridSizeComboBox.SelectedIndex != -1)
            {
                GridSizeEntry gridSizeEntry = ((GridSizeEntry)GridSizeComboBox.SelectedItem);
                _filterSelectionViewModel.SelectedGridSizeEntry = gridSizeEntry;
                if (_positiontedTextBoxGrid != null)
                {
                    _positiontedTextBoxGrid.Resize(GetShape(gridSizeEntry.GridSize));
                }
            }
        }

        private void RotationSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RotationSliderValue.Text = GetCurrentRotation().ToString();
        }

        private int GetCurrentRotation()
        {
            return ((int)Math.Max(0, Math.Min(90, RotationSlider.Value)));
        }
    }

    public class FilterSelectionViewModel
    {
        private readonly Action<IBaseFilter> _onFilterChangedAction;
        private readonly Action<GridSizes> _onGridSizeChangedAction;
        private FilterEntry _selectedFilter;
        private GridSizeEntry _selectedGridSize;

        public FilterSelectionViewModel(CompassEdgeConfiguration configuration, 
                                        Action<IBaseFilter> onFilterChangedAction, 
                                        Action<GridSizes> onGridSizeChangedAction)
        {
            IList<FilterEntry> filterEntries =
                configuration.GetDefaultFilters()
                .Select(filter => new FilterEntry(filter)).ToList();
            IList<GridSizeEntry> gridSizeEntries = Enum.GetValues(typeof(GridSizes)).Cast<GridSizes>()
                .Select(gridSize => new GridSizeEntry(gridSize)).ToList();
            FilterEntries = new ObservableCollection<FilterEntry>(filterEntries);
            GridSizeEntries = new ObservableCollection<GridSizeEntry>(gridSizeEntries);
            _onFilterChangedAction = onFilterChangedAction;
            _onGridSizeChangedAction = onGridSizeChangedAction;
        }

        public ObservableCollection<FilterEntry> FilterEntries { get;  }

        public ObservableCollection<GridSizeEntry> GridSizeEntries { get; }

        public FilterEntry SelectedFilterEntry
        {
            get { return _selectedFilter; }
            set
            {
                if (_selectedFilter == value)
                    return;
                _selectedFilter = value;
                _onFilterChangedAction.Invoke(_selectedFilter.Filter);
            }
        }

        public GridSizeEntry SelectedGridSizeEntry
        {
            get { return _selectedGridSize; }
            set
            {
                if (_selectedGridSize == value)
                    return;
                _selectedGridSize = value;
                _onGridSizeChangedAction.Invoke(_selectedGridSize.GridSize);
            }
        }

        public IBaseFilter SelectedFilter
        {
            get
            {
                if (SelectedFilterEntry == null)
                    throw new Exception("You have to select a filter");
                return SelectedFilterEntry.Filter;
            }
        }

        public GridSizes SelectedGridSize
        {
            get
            {
                if (SelectedGridSizeEntry == null)
                    throw new Exception("You have to select a grid size");
                return SelectedGridSizeEntry.GridSize;
            }
        }

        public FilterEntry findByFilterName(String filterName)
        {
            for (int i = 0; i < FilterEntries.Count; i++)
            {
                if (FilterEntries[i].Name == filterName)
                {
                    return FilterEntries[i];
                }
            }
            throw new Exception("Unknown Filter: " + filterName);
        }

        public GridSizeEntry findByGridSize(GridSizes gridSize)
        {
            for (int i = 0; i < GridSizeEntries.Count; i++)
            {
                if (GridSizeEntries[i].GridSize == gridSize)
                {
                    return GridSizeEntries[i];
                }
            }
            throw new Exception("Unknown Grid Size");
        }
    }
    public class GridSizeEntry
    {
        public string Description { get; }
        public GridSizes GridSize { get; set; }

        public GridSizeEntry(GridSizes gridSize)
        {
            Description = gridSize.ToDescriptionString();
            GridSize = gridSize;
        }

        public override string ToString()
        {
            return Description;
        }
    }
    public class FilterEntry
    {
        public string Name { get; }

        public IBaseFilter Filter { get; set; }

        public FilterEntry(IBaseFilter filter)
        {
            Name = filter.GetName();
            Filter = filter;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    class PositionedTextBox : IPositionedUIElement<TextBox>
    {
        private readonly int _XPosition;
        private readonly int _YPosition;
        private readonly TextBox _textBox;
        private Action<PositionedTextBox> _onClickAction;

        public PositionedTextBox(int XPosition, int YPosition, string defaultValue)
        {
            _XPosition = XPosition;
            _YPosition = YPosition;
            _textBox = new TextBox();
            _textBox.Text = defaultValue;
            _textBox.TextChanged += TextBox_TextChanged;
        }

        public override bool Equals(object obj)
        {
            return obj is PositionedTextBox box &&
                   _XPosition == box._XPosition &&
                   _YPosition == box._YPosition;
        }

        public TextBox GetElement()
        {
            return _textBox;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_XPosition, _YPosition);
        }

        public int GetXPosition()
        {
            return _XPosition;
        }

        public int GetYPosition()
        {
            return _YPosition;
        }

        public void SetValue(string value)
        {
            _textBox.Text = value;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (_onClickAction != null)
            {
                _onClickAction.Invoke(this);
            }
        }
    }

    class PositionedTextBoxGrid : AbstractTextBoxGrid<PositionedTextBox>
    {
        private string _defaultValue;

        public PositionedTextBoxGrid(Grid grid, Tuple<int, int> shape, string defaultValue) : base(grid, shape)
        {
            _defaultValue = defaultValue;
        }

        protected override List<PositionedTextBox> GetPositionedElements(Tuple<int, int> gridShape)
        {
            List<PositionedTextBox> positionedTextBoxes = new List<PositionedTextBox>();
            for (int i = 0; i < gridShape.Item1; i++)
            {
                for (int j = 0; j < gridShape.Item2; j++)
                {
                    PositionedTextBox textBox = new PositionedTextBox(i, j, _defaultValue);
                    positionedTextBoxes.Add(textBox);
                }
            }
            return positionedTextBoxes;
        }
    }

    abstract class AbstractTextBoxGrid<T> where T : IPositionedUIElement<UIElement>
    {
        private readonly Grid _grid;
        private Tuple<int, int> _shape;
        private List<T> _elements;

        public AbstractTextBoxGrid(Grid grid, Tuple<int, int> shape)
        {
            _grid = grid;
            _shape = shape;
            _elements = new List<T>();
            PopulateKernelShapeGrid(grid, shape);
        }

        private void PopulateKernelShapeGrid(Grid grid, Tuple<int, int> shape)
        {
            List<T> positionedTextBoxes = GetPositionedElements(shape);
            InitializeGrid(grid, shape);
            positionedTextBoxes.ForEach(checkBox => AddTextBoxToGrid(grid, checkBox));
        }

        public void Resize(Tuple<int, int> newShape)
        {
            _shape = newShape;
            _grid.Children.Clear();
            _grid.ColumnDefinitions.Clear();
            _grid.RowDefinitions.Clear();
            _elements.Clear();
            PopulateKernelShapeGrid(_grid, _shape);
        }

        private void InitializeGrid(Grid grid, Tuple<int, int> gridShape)
        {
            for (int i = 0; i < gridShape.Item1; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < gridShape.Item2; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
        }

        protected abstract List<T> GetPositionedElements(Tuple<int, int> gridShape);

        private void AddTextBoxToGrid(Grid grid, T positionedElement)
        {
            UIElement element = positionedElement.GetElement();
            Grid.SetColumn(element, positionedElement.GetYPosition());
            Grid.SetRow(element, positionedElement.GetXPosition());
            grid.Children.Add(element);
            _elements.Add(positionedElement);
        }

        public Grid GetGrid()
        {
            return _grid;
        }

        public int GetWidth()
        {
            return _shape.Item1;
        }

        public int GetHeight()
        {
            return _shape.Item2;
        }

        public List<T> GetElements()
        {
            return _elements;
        }

        public T GetElement(int XPosition, int YPosition)
        {
            foreach (T element in _elements)
            {
                if (element.GetXPosition().Equals(XPosition) && element.GetYPosition().Equals(YPosition))
                {
                    return element;
                }
            }

            throw new KeyNotFoundException($"No element on position X: {XPosition}, Y: {YPosition}");
        }
    }

    interface IPositionedUIElement<out T> where T : UIElement
    {
        public int GetXPosition();

        public int GetYPosition();

        public T GetElement();
    }
}
