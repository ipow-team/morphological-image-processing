using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace morphological_image_processing_wpf.View.Components.AlgorithmsSelection.Configurations.CustomProperties
{
    /// <summary>
    /// Interaction logic for KernelComponent.xaml
    /// </summary>
    public partial class KernelComponent : UserControl
    {
        private static readonly Tuple<int, int> DEFAULT_SHAPE = Tuple.Create(5, 5);

        private PositionedRadioButton _selectedCenter;
        private readonly ISet<PositionedCheckBox> _kernelShape = new HashSet<PositionedCheckBox>();

        private readonly List<Action<Tuple<int, int>>> _onSelectedCenterChangedActions = new List<Action<Tuple<int, int>>>();
        private readonly List<Action<ISet<Tuple<int, int>>>> _onKernelShapeChangedActions = new List<Action<ISet<Tuple<int, int>>>>();

        private PositionedRadioButtonGrid _centralPointGrid;
        private PositionedCheckBoxGrid _kernelShapeGrid;

        // TODO: Implement validation for minimum kernel size and minimum center
        public KernelComponent()
        {
            InitializeComponent();
            _centralPointGrid = GetPositionedRadioButtonGrid(DEFAULT_SHAPE);
            _kernelShapeGrid = GetPositionedKernelShapeGrid(DEFAULT_SHAPE, _centralPointGrid);
        }

        private PositionedRadioButtonGrid GetPositionedRadioButtonGrid(Tuple<int, int> shape)
        {
            Action<PositionedRadioButton> onCenterPointChangedAction = GetOnCenterPointChangedAction();
            PositionedRadioButtonGrid radioButtonGrid = new PositionedRadioButtonGrid(CentralPointGrid, shape);
            radioButtonGrid.GetElements().ForEach(element => element.GetElement().IsEnabled = false);
            radioButtonGrid.GetElements().ForEach(radioButton => radioButton.OnClick(onCenterPointChangedAction));
            return radioButtonGrid;
        }

        private Action<PositionedRadioButton> GetOnCenterPointChangedAction()
        {
            return positionedRadioElement => SetSelectedCenter(positionedRadioElement);
        }

        private PositionedCheckBoxGrid GetPositionedKernelShapeGrid(Tuple<int, int> shape, PositionedRadioButtonGrid positionedRadioButtonGrid)
        {
            Action<PositionedCheckBox> onCheckboxClickedAction = GetOnKernelShapeChangedAction(positionedRadioButtonGrid);
            PositionedCheckBoxGrid checkBoxGrid = new PositionedCheckBoxGrid(KernelShapeGrid, shape);
            checkBoxGrid.GetElements().ForEach(checkBox => checkBox.OnClick(onCheckboxClickedAction));
            return checkBoxGrid;
        }

        private Action<PositionedCheckBox> GetOnKernelShapeChangedAction(PositionedRadioButtonGrid positionedRadioButtonGrid)
        {
            return positionedCheckBox =>
            {
                CheckBox checkBox = positionedCheckBox.GetElement();
                if (checkBox.IsChecked.GetValueOrDefault(false))
                {
                    AddShapePosition(positionedCheckBox);
                }
                else
                {
                    RemoveShapePosition(positionedCheckBox);
                }
            };
        }

        public Tuple<int, int> GetCenter()
        {
            if(_selectedCenter == null)
            {
                return null;
            }

            return Tuple.Create(_selectedCenter.GetXPosition(), _selectedCenter.GetYPosition());
        }

        public HashSet<Tuple<int, int>> GetShape()
        {
            HashSet<Tuple<int, int>> shape = new HashSet<Tuple<int, int>>();
            foreach (PositionedCheckBox checkbox in _kernelShape)
            {
                shape.Add(Tuple.Create(checkbox.GetXPosition(), checkbox.GetYPosition()));
            }
            return shape;
        }

        public void SetCenter(Tuple<int, int> center)
        {
            SetSelectedCenter(_centralPointGrid.GetElement(center.Item1, center.Item2));
        }

        private void SetSelectedCenter(PositionedRadioButton center)
        {
            if (_selectedCenter == null || !_selectedCenter.Equals(center))
            {
                _centralPointGrid.GetElements().ForEach(element => element.GetElement().IsChecked = false);
                _selectedCenter = center;
                _selectedCenter.GetElement().IsChecked = true;
            }
            _onSelectedCenterChangedActions.ForEach(action => action.Invoke(GetCenter()));
        }

        public void SetShape(ISet<Tuple<int, int>> shape)
        {
            _kernelShapeGrid.GetElements().ForEach(element => RemoveShapePosition(element));
            foreach (Tuple<int, int> position in shape)
            {
                PositionedCheckBox shapePosition = _kernelShapeGrid.GetElement(position.Item1, position.Item2);
                AddShapePosition(shapePosition);
            }
        }

        private void AddShapePosition(PositionedCheckBox positionedCheckBox)
        {
            RadioButton radioButton = _centralPointGrid.GetElement(positionedCheckBox.GetXPosition(), positionedCheckBox.GetYPosition()).GetElement();
            radioButton.IsEnabled = true;
            positionedCheckBox.GetElement().IsChecked = true;
            _kernelShape.Add(positionedCheckBox);
            _onKernelShapeChangedActions.ForEach(action => action.Invoke(GetShape()));
        }

        private void RemoveShapePosition(PositionedCheckBox positionedCheckBox)
        {
            PositionedRadioButton positionedRadioButton = _centralPointGrid.GetElement(positionedCheckBox.GetXPosition(), positionedCheckBox.GetYPosition());
            RadioButton radioButton = positionedRadioButton.GetElement();
            radioButton.IsEnabled = false;
            if(_selectedCenter != null && positionedRadioButton != null && positionedRadioButton.Equals(_selectedCenter))
            {
                SetSelectedCenter(null);
            }
            positionedCheckBox.GetElement().IsChecked = false;
            _kernelShape.Remove(positionedCheckBox);
            _onKernelShapeChangedActions.ForEach(action => action.Invoke(GetShape()));
        }

        public void AddOnCenterChangedAction(Action<Tuple<int, int>> onSelectedCenterChangedAction)
        {
            _onSelectedCenterChangedActions.Add(onSelectedCenterChangedAction);
        }

        public void AddOnKernelShapeChangedAction(Action<ISet<Tuple<int, int>>> onKernelShapeChangedAction)
        {
            _onKernelShapeChangedActions.Add(onKernelShapeChangedAction);
        }
    }

    class PositionedCheckBoxGrid : AbstractToggleButtonGrid<PositionedCheckBox>
    {
        public PositionedCheckBoxGrid(Grid grid, Tuple<int, int> shape) : base(grid, shape)
        {
            // empty
        }

        protected override List<PositionedCheckBox> GetPositionedElements(Tuple<int, int> gridShape)
        {
            List<PositionedCheckBox> positionedCheckBoxes = new List<PositionedCheckBox>();
            for (int i = 0; i < gridShape.Item1; i++)
            {
                for (int j = 0; j < gridShape.Item2; j++)
                {
                    PositionedCheckBox checkBox = new PositionedCheckBox(i, j);
                    positionedCheckBoxes.Add(checkBox);
                }
            }
            return positionedCheckBoxes;
        }
    }

    class PositionedCheckBox : IPositionedUIElement<CheckBox>
    {
        private readonly int _XPosition;
        private readonly int _YPosition;
        private readonly CheckBox _checkBox;
        private Action<PositionedCheckBox> _onClickAction;

        public PositionedCheckBox(int XPosition, int YPosition)
        {
            _XPosition = XPosition;
            _YPosition = YPosition;
            _checkBox = new CheckBox();
            _checkBox.Checked += CheckBox_CheckedChanged;
            _checkBox.Unchecked += CheckBox_CheckedChanged;
        }

        public override bool Equals(object obj)
        {
            return obj is PositionedCheckBox box &&
                   _XPosition == box._XPosition &&
                   _YPosition == box._YPosition;
        }

        public CheckBox GetElement()
        {
            return _checkBox;
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

        public bool IsSelected()
        {
            return _checkBox.IsChecked.GetValueOrDefault(false);
        }

        public void OnClick(Action<PositionedCheckBox> onClickAction)
        {
            _onClickAction = onClickAction;
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if(_onClickAction != null)
            {
                _onClickAction.Invoke(this);
            }
        }
    }

    class PositionedRadioButtonGrid : AbstractToggleButtonGrid<PositionedRadioButton>
    {
        public PositionedRadioButtonGrid(Grid grid, Tuple<int, int> shape) : base(grid, shape)
        {
            // empty
        }

        protected override List<PositionedRadioButton> GetPositionedElements(Tuple<int, int> gridShape)
        {
            List<PositionedRadioButton> positionedRadioButtons = new List<PositionedRadioButton>();
            for (int i = 0; i < gridShape.Item1; i++)
            {
                for (int j = 0; j < gridShape.Item2; j++)
                {
                    PositionedRadioButton radioButton = new PositionedRadioButton(i, j);
                    positionedRadioButtons.Add(radioButton);
                }
            }
            return positionedRadioButtons;
        }
    }

    class PositionedRadioButton : IPositionedUIElement<RadioButton>
    {
        private readonly int _XPosition;
        private readonly int _YPosition;
        private readonly RadioButton _radioButton;
        private Action<PositionedRadioButton> _onClickAction;

        public PositionedRadioButton(int XPosition, int YPosition)
        {
            _XPosition = XPosition;
            _YPosition = YPosition;
            _radioButton = new RadioButton();
            _radioButton.Checked += RadioButton_CheckedChanged;
            _radioButton.Unchecked += RadioButton_CheckedChanged;
        }

        public RadioButton GetElement()
        {
            return _radioButton;
        }

        public int GetXPosition()
        {
            return _XPosition;
        }

        public int GetYPosition()
        {
            return _YPosition;
        }

        public bool IsSelected()
        {
            return _radioButton.IsChecked.GetValueOrDefault(false);
        }

        public void OnClick(Action<PositionedRadioButton> onClickAction)
        {
            _onClickAction = onClickAction;
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_onClickAction != null)
            {
                _onClickAction.Invoke(this);
            }
        }
    }

    abstract class AbstractToggleButtonGrid<T> where T: IPositionedUIElement<UIElement>
    {
        private readonly Grid _grid;
        private readonly Tuple<int, int> _shape;
        private readonly List<T> _elements;

        public AbstractToggleButtonGrid(Grid grid, Tuple<int, int> shape)
        {
            _grid = grid;
            _shape = shape;
            _elements = new List<T>();
            PopulateKernelShapeGrid(grid, shape);
        }

        private void PopulateKernelShapeGrid(Grid grid, Tuple<int, int> shape)
        {
            List<T> positionedCheckBoxes = GetPositionedElements(shape);
            InitializeGrid(grid, shape);
            positionedCheckBoxes.ForEach(checkBox => AddCheckBoxToGrid(grid, checkBox));
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

        private void AddCheckBoxToGrid(Grid grid, T positionedElement)
        {
            UIElement element = positionedElement.GetElement();
            Grid.SetColumn(element, positionedElement.GetXPosition());
            Grid.SetRow(element, positionedElement.GetYPosition());
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
            foreach(T element in _elements) 
            {
                if (element.GetXPosition().Equals(XPosition) && element.GetYPosition().Equals(YPosition))
                {
                    return element;
                }
            }

            throw new KeyNotFoundException($"No element on position X: {XPosition}, Y: {YPosition}");
        }
    }

    interface IPositionedUIElement<out T> where T: UIElement
    {
        public int GetXPosition();

        public int GetYPosition();

        public T GetElement();
    }
}
