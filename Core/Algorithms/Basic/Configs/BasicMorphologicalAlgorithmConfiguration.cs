using System;
using System.Collections.Generic;
using Color = System.Drawing.Color;

namespace MorphologicalImageProcessing.Core.Algorithms
{
    class BasicMorphologicalAlgorithmConfiguration : IMorphologicalAlgorithmConfiguration
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
            if (typeof(BasicMorphologicalAlgorithmConfiguration).IsAssignableFrom(other.GetType()))
            {
                SetValuesFrom((BasicMorphologicalAlgorithmConfiguration)other);
            }
            
        }

        public void SetValuesFrom(BasicMorphologicalAlgorithmConfiguration other)
        {
            BrightnessThreshold = other.BrightnessThreshold;
            LineColor = other.LineColor;
            StructuralElementPoints = other._structuralElementShape;
            Center = other.Center;
        }
    }
}
