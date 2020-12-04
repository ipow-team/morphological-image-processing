using morphological_image_processing_wpf.Core.Converters;
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

        protected List<Tuple<int, int>> CalculatePointsToCheck(ISet<Tuple<int, int>> points, Tuple<int, int> centralPoint)
        {
            List<Tuple<int, int>> newPoints = new List<Tuple<int, int>>();

            foreach (Tuple<int, int> point in points)
            {
                newPoints.Add(Tuple.Create(point.Item1 - centralPoint.Item1, point.Item2 - centralPoint.Item2));
            }

            return newPoints;
        }
    }

    [JsonInterfaceConverter(typeof(MorphologicalAlgorithmConfigurationJsonConverter))]
    public interface IMorphologicalAlgorithmConfiguration {
        void SetValuesFrom(IMorphologicalAlgorithmConfiguration other);
    }

    class DefaultMorphologicalAlgorithmConfiguration : IMorphologicalAlgorithmConfiguration
    {
        public double BrightnessThreshold { get; set; } = 0.02;

        public Color LineColor { get; set; } = Color.Red;

        private readonly ISet<Tuple<int, int>> _structuralElementShape = new HashSet<Tuple<int, int>>()
        {
            Tuple.Create(0, 0), Tuple.Create(0, 1), Tuple.Create(0, 2),
            Tuple.Create(1, 0), Tuple.Create(1, 1), Tuple.Create(1, 2),
            Tuple.Create(2, 0), Tuple.Create(2, 1), Tuple.Create(2, 2)
        };

        public ISet<Tuple<int, int>> StructuralElementPoints
        {
            get
            {
                return _structuralElementShape;
            }
            set
            {
                _structuralElementShape.Clear();
                _structuralElementShape.UnionWith(value);
            }
        }

        public Tuple<int, int> Center { get; set; } = Tuple.Create(1, 1);

        public void SetValuesFrom(IMorphologicalAlgorithmConfiguration other)
        {
            if (typeof(DefaultMorphologicalAlgorithmConfiguration).IsAssignableFrom(other.GetType()))
            {
                SetValuesFrom((DefaultMorphologicalAlgorithmConfiguration)other);
            }
            
        }

        public void SetValuesFrom(DefaultMorphologicalAlgorithmConfiguration other)
        {
            BrightnessThreshold = other.BrightnessThreshold;
            LineColor = other.LineColor;
            StructuralElementPoints = other._structuralElementShape;
            Center = other.Center;
        }
    }

    class EmptyMorphologicalAlgorithmConfiguration: IMorphologicalAlgorithmConfiguration
    {
        public void SetValuesFrom(IMorphologicalAlgorithmConfiguration other)
        {
            // No neeed, empty configuration
        }
    }
}
