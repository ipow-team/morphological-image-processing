using System;
using System.Collections.Generic;
using System.Text;

namespace morphological_image_processing_wpf.Core.Generator
{
    public interface IGeneratorConfiguration { }

    class GeneratorConfiguration : IGeneratorConfiguration
    {
        public int NumberOfShapes { get; set; } = 3;
    }
}
