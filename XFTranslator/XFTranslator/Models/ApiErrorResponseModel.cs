using System;
namespace XFTranslator.Models
{

    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class ApiErrorResponseModel
    {
        [JsonProperty("error")]
        public Error Error { get; set; }
    }

    public partial class Error
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }

        [JsonProperty("statusCode")]
        public long StatusCode { get; set; }
    }

    public partial class Info
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("statusCode")]
        public long StatusCode { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }
    }
}