using System;
using XamFood.Resources;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace XamFood.Services
{
    public static class AppResourceService
    {

        public static AppResource TextResource { get; set; }

        private static readonly HttpClient Client = new HttpClient();
        private static readonly string _stringJsonUrl = @"https://gist.githubusercontent.com/aimore/7898b9cd7e8dfce587949f7e6c4c0ca3/raw/4511c2aa3a26b59d21d3c023389d9693b4356342/strings.json";
        public async static Task<bool> GetAppText()
        {
            var httpResponseMessage = await Client.GetAsync(_stringJsonUrl);
            try
            {
                httpResponseMessage.EnsureSuccessStatusCode();
                string jsonString = await httpResponseMessage.Content.ReadAsStringAsync();
                TextResource = JsonConvert.DeserializeObject<AppResource>(jsonString);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }    
        }

        public async static Task<Models.HomeModel> GetHomeContent()
        {
            throw new NotImplementedException();
        }
    }
}
namespace XamFood
{
    public static class HomeUIConstants
    {
        public static string PromoTitleLabel = "Trending Restaurants";
        public static string PromoLinkLabel = "See all (43)";
        public static string CategoryTitleLabel = "Category";
        public static string CategoryLinkLabel = "See all (9)";
    }
}