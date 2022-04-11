namespace IntegrationTests
{
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;


    public sealed class Deserialize<T>
    {
        public static T FromJson(string json) => JsonConvert.DeserializeObject<T>(json, IntegrationTests.Converter.Settings);
    }


    //public static class Serialize<K>

    //{
    //    public static string ToJson(K self) => JsonConvert.SerializeObject(self, IntegrationTests.Converter.Settings);
    //}

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
