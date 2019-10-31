using System;
using System.ComponentModel;

namespace XamHeroes.Configurations
{
    public static class WebAPIConstants
    {
        public enum APICallType 
        {
            [Description("/all.json")]
            All = 1,
         }
        public static string BaseUrl = "https://cdn.rawgit.com/akabab/superhero-api/0.2.0/api";
    }
}
