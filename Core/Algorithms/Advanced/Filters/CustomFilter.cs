using morphological_image_processing_wpf.Core.Algorithms.Filters;
using System.Collections.Generic;

namespace morphological_image_processing_wpf.Core.Algorithms.Advanced.Filters
{
    public class CustomFilter : IBaseFilter
    {
        private double[,] _baseKernel = new double[,] { };

        public CustomFilter(double[,] baseKernel, double rotation, GridSizes gridSize)
        {
            _baseKernel = baseKernel;
            Rotation = rotation;
            GridSize = gridSize;
        }

        public CustomFilter()
        {
        }

        public static CustomFilter create()
        {
            double[,] defaultKernel = new double[,]
            {
               {  0, 0,  0, },
               {  0, 0,  0, },
               {  0, 0,  0, }
            };
            return new CustomFilter(defaultKernel, 0, GridSizes.Small);
        }

        public List<GridElement> BaseKernel
        {
            get
            {
                List<GridElement> gridElementList = new List<GridElement>();

                for(int i = 0; i < _baseKernel.GetLength(0); i++)
                {
                    for(int j = 0; j < _baseKernel.GetLength(1); j++)
                    {
                        gridElementList.Add(new GridElement(_baseKernel[i, j], i, j));
                    }
                }

                return gridElementList;
            }
            set
            {
                if (value == null)
                    return;

                int x = 0;
                int y = 0;

                for(int i = 0; i < value.Count; i++)
                {
                    if (value[i].XPos > x)
                        x = value[i].XPos;

                    if (value[i].YPos > y)
                        y = value[i].XPos;
                }

                double[,] baseKernel = new double[x + 1, y + 1];
                value.ForEach(element => baseKernel[element.XPos, element.YPos] = element.Value);
                _baseKernel = baseKernel;
            }
        }

        public double[,] GetBaseKernel()
        {
            return _baseKernel;
        }

        public double[,,] GetKernel()
        {
            return IBaseFilter.RotateMatrix(_baseKernel, Rotation);
        }

        public string GetName()
        {
            return "Custom";
        }

        public double Rotation { get; set; }

        public double GetRotation()
        {
            return Rotation;
        }

        public bool IsMutable()
        {
            return true;
        }

        public GridSizes GetGridSize()
        {
            return GridSize;
        }

        public GridSizes GridSize { get; set; }
    }

    public class GridElement
    {
        public double Value { get; }
        public int XPos { get; }
        public int YPos { get; }

        public GridElement(double Value, int XPos, int YPos)
        {
            this.Value = Value;
            this.XPos = XPos;
            this.YPos = YPos;
        }
    }
}
