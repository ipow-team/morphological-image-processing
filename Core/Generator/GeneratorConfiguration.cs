namespace morphological_image_processing_wpf.Core.Generator
{
    public interface IGeneratorConfiguration {}

    public class GeneratorConfiguration : IGeneratorConfiguration
    {
        public int NumberOfShapes { get; set; } = 3;

        public void SetValuesFrom(GeneratorConfiguration configuration)
        {
            NumberOfShapes = configuration.NumberOfShapes;
        }
    }
}
