using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;
using XamHeroes.Configurations;

namespace XamHeroes.Services
{

    public class APIService
    {
        private readonly HttpClient _httpClient;
        private readonly IHeroesApi _theHeroesApi;
        private static WebClient _webClient;
        public APIService()
        {
            _httpClient = new HttpClient();
            _theHeroesApi = RestService.For<IHeroesApi>(WebAPIConstants.BaseUrl,
            new RefitSettings
            {
                ContentSerializer = new JsonContentSerializer(Models.Converter.Settings)
            });
        }

        public async Task<List<Models.Heroes>> GetAllHeroes()
        {
            List<Models.Heroes> response = null;
            try
            {
                response = await _theHeroesApi.GetHeroes();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error {_theHeroesApi}:  {ex}");
            }
            finally 
            {
                _httpClient.CancelPendingRequests();
                _httpClient.Dispose();
            }
            return response;
            //return await _theHeroesApi.GetHeroes().ConfigureAwait(false);
        }

        //public static List<Test.Heroes> GetAllHeroesWebClient() 
        //{
        //    _webClient = new WebClient();
        //    string s = _webClient.DownloadString(new Uri(WebAPIConstants.BaseUrl + "/all.json"));
        //    List <Test.Heroes> heroes = new List<Test.Heroes>(new Test.Heroes[30]);
        //    JsonConvert.PopulateObject(s, heroes);
        //    return heroes;
        //}
    }

    public interface IHeroesApi
    {
        [Get("/all.json")]
        Task<List<Models.Heroes>> GetHeroes();
    }
}