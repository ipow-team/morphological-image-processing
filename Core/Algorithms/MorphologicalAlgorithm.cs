﻿using System;
using System.Drawing;

namespace MorphologicalImageProcessing.Core.Algorithms
{
    public interface IAlgorithm
    {
        String GetName();

        Bitmap Apply(Bitmap image, IMorphologicalAlgorithmConfiguration configuration);

        Bitmap Apply(Bitmap image, IMorphologicalAlgorithmConfiguration configuration, Action<Bitmap> stepCallback);

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
        protected abstract Bitmap Apply(Bitmap image, T configuration, Action<Bitmap> stepCallback);

        public abstract String GetName();


        public Bitmap Apply(Bitmap image, IMorphologicalAlgorithmConfiguration configuration)
        {
            return Apply(image, configuration, null);
        }

        public Bitmap Apply(Bitmap image, IMorphologicalAlgorithmConfiguration configuration, Action<Bitmap> stepCallback)
        {
            return Apply(image, (T)configuration, stepCallback);
        }

        public Type GetConfigurationClass() {
            return typeof(T);
        }
    }

    public interface IMorphologicalAlgorithmConfiguration { }

    class DefaultMorphologicalAlgorithmConfiguration: IMorphologicalAlgorithmConfiguration
    {
        public int BoxSize { get; set; } = 3;
        public int MinBoxSize { get; } = 2;
        public int MaxBoxSize { get; } = 10;

        public Color LineColor { get; set; } = Color.Red;
    }

    class EmptyMorphologicalAlgorithmConfiguration: IMorphologicalAlgorithmConfiguration
    {

    }
}
