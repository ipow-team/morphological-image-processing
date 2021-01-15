using MorphologicalImageProcessing.Core.Algorithms;
using System;

namespace morphological_image_processing_wpf.Core.Converters
{
    public class ConfigContainer : BaseConfigContainer
    {
        public IMorphologicalAlgorithmConfiguration value { get; set; }

        public ConfigContainer(IMorphologicalAlgorithmConfiguration config)
        {
            value = config;
            type = config.GetType().FullName;
        }
    }

    public class BaseConfigContainer
    {
        public String type { get; set; }
    }
}
