using MorphologicalImageProcessing.Core.Algorithms;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace morphological_image_processing_wpf.Core.Converters
{
    class MorphologicalAlgorithmConfigurationJsonConverter : JsonConverter<IMorphologicalAlgorithmConfiguration>
    {
        public override IMorphologicalAlgorithmConfiguration Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, IMorphologicalAlgorithmConfiguration value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                JsonSerializer.Serialize(writer, (IMorphologicalAlgorithmConfiguration)null, options);
            }
            else
            {
                var type = value.GetType();
                JsonSerializer.Serialize(writer, value, type, options);
            }
        }
    }
}
