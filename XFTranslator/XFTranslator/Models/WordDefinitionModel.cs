
namespace XFTranslator.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class WordDefinitionModel
    {
        [JsonProperty("word")]
        public string Word { get; set; }

        [JsonProperty("phonetic")]
        public string[] Phonetic { get; set; }

        [JsonProperty("meaning")]
        public Meaning Meaning { get; set; }
    }

    public partial class Meaning
    {
        [JsonProperty("noun, adjective, verb, adverb, pronoun, determiner, conjunction")]
        public Value[] Values { get; set; }
    }

    public partial class Value
    {
        [JsonProperty("definition", NullValueHandling = NullValueHandling.Ignore)]
        public string Definition { get; set; }

        [JsonProperty("example", NullValueHandling = NullValueHandling.Ignore)]
        public string Example { get; set; }

        [JsonProperty("synonyms", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Synonyms { get; set; }
    }
}