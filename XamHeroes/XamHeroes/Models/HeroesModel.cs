

namespace XamHeroes.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Heroes
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonIgnore]
        [JsonProperty("powerstats")]
        public Powerstats Powerstats { get; set; }

        [JsonProperty("appearance")]
        public Appearance Appearance { get; set; }

        [JsonProperty("biography")]
        public Biography Biography { get; set; }

        [JsonIgnore]
        [JsonProperty("work")]
        public Work Work { get; set; }

        [JsonIgnore]
        [JsonProperty("connections")]
        public Connections Connections { get; set; }


        [JsonIgnore]
        public string ImageSource
        {
            get
            {
                return $"https://cdn.rawgit.com/akabab/superhero-api/0.2.0/api/images/md/{Slug}.jpg";
            }
        }

        [JsonIgnore]
        [JsonProperty("images")]
        public Images Images { get; set; }
    }

    public partial class Appearance
    {
        [JsonProperty("gender")]
        public Gender Gender { get; set; }

        [JsonProperty("race")]
        public string Race { get; set; }

        [JsonProperty("height")]
        public string[] Height { get; set; }

        [JsonProperty("weight")]
        public string[] Weight { get; set; }

        [JsonProperty("eyeColor")]
        public string EyeColor { get; set; }

        [JsonProperty("hairColor")]
        public string HairColor { get; set; }
    }

    public partial class Biography
    {
        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("alterEgos")]
        public string AlterEgos { get; set; }

        [JsonProperty("aliases")]
        public string[] Aliases { get; set; }

        [JsonProperty("placeOfBirth")]
        public string PlaceOfBirth { get; set; }

        [JsonProperty("firstAppearance")]
        public string FirstAppearance { get; set; }

        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("alignment")]
        public Alignment Alignment { get; set; }
    }

    public partial class Connections
    {
        [JsonProperty("groupAffiliation")]
        public string GroupAffiliation { get; set; }

        [JsonProperty("relatives")]
        public string Relatives { get; set; }
    }

    public partial class Images
    {
        [JsonIgnore]
        [JsonProperty("xs")]
        public string Xs { get; set; }

        [JsonIgnore]
        [JsonProperty("sm")]
        public string Sm { get; set; }

        [JsonProperty("md")]
        public string Medium { get; set; }

        [JsonProperty("lg")]
        public string Large { get; set; }
    }

    public partial class Powerstats
    {
        [JsonProperty("intelligence")]
        public long Intelligence { get; set; }

        [JsonProperty("strength")]
        public long Strength { get; set; }

        [JsonProperty("speed")]
        public long Speed { get; set; }

        [JsonProperty("durability")]
        public long Durability { get; set; }

        [JsonProperty("power")]
        public long Power { get; set; }

        [JsonProperty("combat")]
        public long Combat { get; set; }
    }

    public partial class Work
    {
        [JsonProperty("occupation")]
        public string Occupation { get; set; }

        [JsonProperty("base")]
        public string Base { get; set; }
    }

    public enum Gender { Empty, Female, Male };

    public enum Alignment { Bad, Empty, Good, Neutral };

    public partial class Heroes
    {
        public static Heroes[] FromJson(string json) => JsonConvert.DeserializeObject<Heroes[]>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Heroes[] self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public static class Converter
    {
        public static JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                GenderConverter.Singleton,
                AlignmentConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class GenderConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(Gender) || objectType == typeof(Gender?);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "-":
                    return Gender.Empty;
                case "Female":
                    return Gender.Female;
                case "Male":
                    return Gender.Male;
            }
            throw new Exception("Cannot unmarshal type Gender");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Gender)untypedValue;
            switch (value)
            {
                case Gender.Empty:
                    serializer.Serialize(writer, "-");
                    return;
                case Gender.Female:
                    serializer.Serialize(writer, "Female");
                    return;
                case Gender.Male:
                    serializer.Serialize(writer, "Male");
                    return;
            }
            throw new Exception("Cannot marshal type Gender");
        }

        public static readonly GenderConverter Singleton = new GenderConverter();
    }

    internal class AlignmentConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(Alignment) || objectType == typeof(Alignment?);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "-":
                    return Alignment.Empty;
                case "bad":
                    return Alignment.Bad;
                case "good":
                    return Alignment.Good;
                case "neutral":
                    return Alignment.Neutral;
            }
            throw new Exception("Cannot unmarshal type Alignment");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Alignment)untypedValue;
            switch (value)
            {
                case Alignment.Empty:
                    serializer.Serialize(writer, "-");
                    return;
                case Alignment.Bad:
                    serializer.Serialize(writer, "bad");
                    return;
                case Alignment.Good:
                    serializer.Serialize(writer, "good");
                    return;
                case Alignment.Neutral:
                    serializer.Serialize(writer, "neutral");
                    return;
            }
            throw new Exception("Cannot marshal type Alignment");
        }

        public static readonly AlignmentConverter Singleton = new AlignmentConverter();
    }
}
