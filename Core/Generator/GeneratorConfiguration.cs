namespace morphological_image_processing_wpf.Core.Generator
{
    public interface IGeneratorConfiguration {}

    public class GeneratorConfiguration : IGeneratorConfiguration
    {
        public int NumberOfShapes { get; set; } = 3;
        public int MaxNumberOfEdges { get; set; } = 5;
        public int MaxStrokeThickness { get; set; } = 10;

        public void SetValuesFrom(GeneratorConfiguration configuration)
        {
            NumberOfShapes = configuration.NumberOfShapes;
            MaxNumberOfEdges = configuration.MaxNumberOfEdges;
            MaxStrokeThickness = configuration.MaxStrokeThickness;
        }
    }
}
