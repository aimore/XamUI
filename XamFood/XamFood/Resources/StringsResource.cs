namespace XamFood.Resources
{
    using System.Collections.Generic;
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class AppResource
    {
        [JsonProperty("appName")]
        public string AppName { get; set; }

        [JsonProperty("isPremium")]
        public bool IsPremium { get; set; }

        [JsonProperty("styles")]
        public Styles Styles { get; set; }

        [JsonProperty("home")]
        public Home Home { get; set; }
    }

    public class Home
    {
        [JsonProperty("promoSectionTitle")]
        public string PromoSectionTitle { get; set; }

        [JsonProperty("categorySectionTitle")]
        public string CategorySectionTitle { get; set; }

        [JsonProperty("searchPlaceholder")]
        public string SearchPlaceholder { get; set; }

        [JsonProperty("promoCards")]
        public List<PromoCard> PromoCards { get; set; }

        [JsonProperty("categoryCards")]
        public List<CategoryCard> CategoryCards { get; set; }
    }

    public class CategoryCard
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }

    public class PromoCard
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("rigthWidget")]
        public string RigthWidget { get; set; }

        [JsonProperty("leftWidget")]
        public string LeftWidget { get; set; }
    }

    public class Styles
    {
        [JsonProperty("backgroundColor")]
        public string BackgroundColor { get; set; }

        [JsonProperty("promoTitleColor")]
        public string PromoTitleColor { get; set; }

        [JsonProperty("tabBarColor")]
        public string TabBarColor { get; set; }

        [JsonProperty("categoryTitleColor")]
        public string CategoryTitleColor { get; set; }

        [JsonProperty("descriptionColor")]
        public string DescriptionColor { get; set; }

        [JsonProperty("rightWidgetColor")]
        public string RightWidgetColor { get; set; }

        [JsonProperty("leftWidgetColor")]
        public string LeftWidgetColor { get; set; }

        [JsonProperty("categoryCardColor")]
        public string CategoryCardColor { get; set; }

        [JsonProperty("promoCardColor")]
        public string PromoCardColor { get; set; }
    }

    public partial class AppResource
    {
        public static AppResource FromJson(string json) => JsonConvert.DeserializeObject<AppResource>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this AppResource self) => JsonConvert.SerializeObject(self, Converter.Settings);
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
