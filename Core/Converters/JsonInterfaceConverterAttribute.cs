using System;
using System.Text.Json.Serialization;

namespace morphological_image_processing_wpf.Core.Converters
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class JsonInterfaceConverterAttribute : JsonConverterAttribute
    {
        public JsonInterfaceConverterAttribute(Type converterType)
            : base(converterType)
        {
        }
    }
}
