namespace ToDoer.API.Infrastructure.Data.Serialization
{
    using System;
    using Newtonsoft.Json;

    public class StringEnumConverter : Newtonsoft.Json.Converters.StringEnumConverter
    {
        public override object ReadJson(
            JsonReader reader,
            Type objectType,
            object existingValue,
            JsonSerializer serializer)
        {
            try
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch (JsonSerializationException)
            {
                return existingValue;
            }
        }
    }
}