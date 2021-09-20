namespace ToDoer.API.Infrastructure.Data.Serialization
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class JsonConstants
    {
        static JsonConstants()
        {
            SerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DefaultValueHandling = DefaultValueHandling.Populate,
                Formatting = Formatting.Indented
            };
            SerializerSettings.Converters.Add(new StringEnumConverter());
        }

        public static JsonSerializerSettings SerializerSettings { get; }

        public static void ApplyTo(JsonSerializerSettings settings)
        {
            if (settings != null)
            {
                settings.ContractResolver = SerializerSettings.ContractResolver;
                settings.DefaultValueHandling = SerializerSettings.DefaultValueHandling;
                settings.Formatting = SerializerSettings.Formatting;
                settings.Converters.Add(new StringEnumConverter());
            }
        }
    }
}