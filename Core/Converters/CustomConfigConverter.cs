
using morphological_image_processing_wpf.Core.Algorithms.Advanced.Filters;
using morphological_image_processing_wpf.Core.Algorithms.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace morphological_image_processing_wpf.Core.Converters
{
    public class CustomConfigConverter : CustomCreationConverter<CustomFilter>
    {
        public override CustomFilter Create(Type objectType)
        {
            return new CustomFilter();
        }

        public override bool CanWrite { get { return false; } }
        public override bool CanRead { get { return true; } }
        public CustomConfigConverter() { }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                JObject jObject = JObject.Load(reader);
                CustomFilter target = Create(objectType);
                target.Rotation = jObject["Rotation"].Value<double>();
                int gridSize = jObject["GridSize"].Value<int>();
                target.GridSize = (GridSizes)gridSize;
                target.BaseKernel = jObject["BaseKernel"].ToObject<List<GridElement>>();
                return target;
            } catch (Exception e)
            {
                return null;
            }
        }
    }
}
