using System;
using System.Drawing;

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

    class DefaultMorphologicalAlgorithmConfiguration: IMorphologicalAlgorithmConfiguration
    {
        public int BoxSize { get; set; } = 3;
    }

    class EmptyMorphologicalAlgorithmConfiguration: IMorphologicalAlgorithmConfiguration
    {

    }
}
