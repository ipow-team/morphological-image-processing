using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;

namespace morphological_image_processing_wpf.Core.Converters
{
    public class ColorJsonConverter : CustomCreationConverter<Color>
    {
        public override bool CanWrite { get { return false; } }
        public override bool CanRead { get { return true; } }
        public ColorJsonConverter() { }
        public override Color Create(Type objectType)
        {
            return new Color();
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            Color target = Create(objectType);
            target = Color.FromArgb(jObject["A"].Value<Int32>(), jObject["R"].Value<Int32>(), jObject["G"].Value<Int32>(), jObject["B"].Value<Int32>());
            return target;
        }
    }
}
