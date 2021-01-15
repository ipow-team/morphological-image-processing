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

    public class DefaultMorphologicalAlgorithmConfiguration : IMorphologicalAlgorithmConfiguration
    {
        public double BrightnessThreshold { get; set; }

        public Color LineColor { get; set; }

        private readonly ISet<Tuple<int, int>> _structuralElementShape;

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

        public Tuple<int, int> Center { get; set; }

        private static ISet<Tuple<int, int>> DefaultStructuralElementShape()
        {
            ISet<Tuple<int, int>> points = new HashSet<Tuple<int, int>>();

            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 3; j++)
                {
                    points.Add(Tuple.Create(i, j));
                }
            }

            return points;
        }

        private DefaultMorphologicalAlgorithmConfiguration()
        {
            _structuralElementShape = new HashSet<Tuple<int, int>>();
        }

        private DefaultMorphologicalAlgorithmConfiguration(double brightnessThreshold, Color lineColor, Tuple<int, int> center, ISet<Tuple<int, int>> shape)
        {
            BrightnessThreshold = brightnessThreshold;
            LineColor = lineColor;
            Center = center;
            _structuralElementShape = shape;
        }

        public static DefaultMorphologicalAlgorithmConfiguration create()
        {
            return new DefaultMorphologicalAlgorithmConfiguration(0.02, Color.Red, Tuple.Create(1, 1), DefaultStructuralElementShape());
        }

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
