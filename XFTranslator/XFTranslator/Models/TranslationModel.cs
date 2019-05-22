namespace XFTranslator.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class TranslationModel
    {
        [JsonProperty("outputs")]
        public List<Output> Outputs { get; set; }
    }

    public partial class Output
    {
        [JsonProperty("output")]
        public string OutputOutput { get; set; }

        [JsonIgnore]
        public Stats Stats { get; set; }

        [JsonProperty("detectedLanguage")]
        public string DetectedLanguage { get; set; }

        [JsonIgnore]
        public double DetectedLanguageConfidence { get; set; }
    }

    public partial class Stats
    {
        [JsonProperty("elapsed_time")]
        public long ElapsedTime { get; set; }

        [JsonIgnore]
        public long NbCharacters { get; set; }

        [JsonIgnore]
        public long NbTokens { get; set; }

        [JsonIgnore]
        public long NbTus { get; set; }

        [JsonIgnore]
        public long NbTusFailed { get; set; }
    }

    public partial class TranslationModel
    {
        public static TranslationModel FromJson(string json) => JsonConvert.DeserializeObject<TranslationModel>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this TranslationModel self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}