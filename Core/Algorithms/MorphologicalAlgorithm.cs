using System;
using System.Collections.Generic;
using System.Drawing;
using Color = System.Drawing.Color;

namespace MorphologicalImageProcessing.Core.Algorithms
{
    public interface IAlgorithm
    {
        String GetName();

        Bitmap Apply(Bitmap image, IMorphologicalAlgorithmConfiguration configuration);

        Type GetConfigurationClass();

        public String Name
        {
            get
            {
                return GetName();
            }
        }
    }

    abstract class MorphologicalAlgorithm<T> : IAlgorithm where T : IMorphologicalAlgorithmConfiguration
    {
        protected abstract Bitmap Apply(Bitmap image, T configuration);

        public abstract String GetName();

        public Bitmap Apply(Bitmap image, IMorphologicalAlgorithmConfiguration configuration)
        {
            return Apply(image, (T)configuration);
        }

        public Type GetConfigurationClass() {
            return typeof(T);
        }
    }

    public interface IMorphologicalAlgorithmConfiguration { }

    class DefaultMorphologicalAlgorithmConfiguration : IMorphologicalAlgorithmConfiguration
    {
        public int BoxSize { get; set; } = 3;
        public int MinBoxSize { get; } = 2;
        public int MaxBoxSize { get; } = 10;

        public List<DataGridRow> StructuralElementDataGrid { get; set; } = new List<DataGridRow>() { new DataGridRow(), new DataGridRow(), new DataGridRow(), new DataGridRow(), new DataGridRow()};

        public double BrightnessThreshold { get; set; } = 0.02;

        public Color LineColor { get; set; } = Color.Red;

        public List<Tuple<int, int>> getStructuralElementConfiguration()
        {
            List<Tuple<int, int>> StructuralElementPoints = new List<Tuple<int, int>>();
            for(int i = 0; i < StructuralElementDataGrid.Count; i++)
            {
                if (StructuralElementDataGrid[i].Column1)
                {
                    StructuralElementPoints.Add(Tuple.Create(i, 0));
                }
                if (StructuralElementDataGrid[i].Column2)
                {
                    StructuralElementPoints.Add(Tuple.Create(i, 1));
                }
                if (StructuralElementDataGrid[i].Column3)
                {
                    StructuralElementPoints.Add(Tuple.Create(i, 2));
                }
                if (StructuralElementDataGrid[i].Column4)
                {
                    StructuralElementPoints.Add(Tuple.Create(i, 3));
                }
                if (StructuralElementDataGrid[i].Column5)
                {
                    StructuralElementPoints.Add(Tuple.Create(i, 4));
                }
            }

            return StructuralElementPoints;
        }

    }

    class DataGridRow
    {
        public bool Column1 { get; set; }
        public bool Column2 { get; set; }
        public bool Column3 { get; set; }
        public bool Column4 { get; set; }
        public bool Column5 { get; set; }
    }

    class EmptyMorphologicalAlgorithmConfiguration: IMorphologicalAlgorithmConfiguration
    {

    }
}
