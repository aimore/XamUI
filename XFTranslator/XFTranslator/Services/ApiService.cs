using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;
using XFTranslator.Models;

namespace XFTranslator.Services
{
    public static class ApiService
    {
        const string baseUrl = "https://systran-systran-platform-for-language-processing-v1.p.rapidapi.com";
        const string googleDictionaryUrl = "https://googledictionaryapi.eu-gb.mybluemix.net";
        //public static IApiService apiService;
        const string apiKey = "c84ba4d8d3msh43bb25e7dc95402p14b8d4jsnbaadbea38fb9";
        const string keyName = "X-RapidAPI-Key";
        private static readonly HttpClient _httpClient = new HttpClient();

        public static IApiService GetApiService()
        {
            var apiService = RestService.For<IApiService>(baseUrl);
            return apiService;
        }

        public async static Task<object> GetWordTranslation(string word)
        {
            object responseModel = null;
            var url = $"/translation/text/translate?source=auto&target=de&input={word}";
            if (_httpClient?.BaseAddress == null)
            {
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                _httpClient.BaseAddress = new Uri(baseUrl);
                _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", apiKey);
            }
            var response = await _httpClient.GetAsync(url);
            using (HttpContent content = response.Content)
            {
                var output = await content.ReadAsStringAsync();
                bool hasResponseSucceeded = (response?.StatusCode == System.Net.HttpStatusCode.OK);
                responseModel = hasResponseSucceeded ? JsonConvert.DeserializeObject<TranslationModel>(output) as object : JsonConvert.DeserializeObject<ApiErrorResponseModel>(output) as object;
                if (responseModel is ApiErrorResponseModel)
                    await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("ERROR", ((ApiErrorResponseModel)responseModel)?.Error?.Message, "cancel");
            }

            return responseModel;
        }

        public async static Task<Models.WordDefinitionModel> GetWordDefinitions(string word)
        {
            WordDefinitionModel responseModel = null;
            using (HttpClient client = new HttpClient())
            {
                var url = $"/?define={word}";
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
                client.BaseAddress = new Uri(googleDictionaryUrl);
                //client.DefaultRequestHeaders.Add("X-RapidAPI-Key", apiKey);
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    using (HttpContent content = response.Content)
                    {
                        var output = await content.ReadAsStringAsync();
                        if(response?.StatusCode == System.Net.HttpStatusCode.OK)
                            responseModel = JsonConvert.DeserializeObject<WordDefinitionModel>(output);
                        if (responseModel == null)
                            await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("ERROR", "No definitions for this word!", "cancel");
                    }
                }
                return responseModel;
            }
        }
    }
}