using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Refit;
using XFTranslator.Models;

namespace XFTranslator.Services
{
    [Headers("X-RapidAPI-Key: c84ba4d8d3msh43bb25e7dc95402p14b8d4jsnbaadbea38fb9")]
    public interface IApiService
    {
        [Get("/translation/text/translate?source=auto&target=de&input={inputValue}")]
        Task<TranslationModel> GetTranslation(string inputValue);
    }
}