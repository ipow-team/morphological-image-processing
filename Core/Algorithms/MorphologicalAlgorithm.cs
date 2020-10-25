using System;
using System.Drawing;

namespace MorphologicalImageProcessing.Core.Algorithms
{
    interface Algorithm
    {
        String GetName();

        Image Apply(Image image, MorphologicalAlgorithmConfiguration configuration);

        public String Name
        {
            get
            {
                return GetName();
            }
        }
    }

    abstract class MorphologicalAlgorithm<T> : Algorithm where T : MorphologicalAlgorithmConfiguration
    {
        protected abstract Image Apply(Image image, T configuration);

        public abstract String GetName();

        public Image Apply(Image image, MorphologicalAlgorithmConfiguration configuration)
        {
            return Apply(image, (T)configuration);
        }

        public Type GetConfigurationClass() {
            return GetType().GetGenericTypeDefinition();
        }
    }

    interface MorphologicalAlgorithmConfiguration { }

    class DefaultMorphologicalAlgorithmConfiguration: MorphologicalAlgorithmConfiguration
    {
        public int BoxSize { get; set; } = 3;
    }
}
