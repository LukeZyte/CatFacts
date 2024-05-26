using CatFacts.Client.Models;
using Newtonsoft.Json;

namespace CatFacts.Client.Services
{
    public class ApiService(IHttpClientFactory httpClientFactory) : IApiService
    {
        private const string apiName = "CatFactsApi";

        public async Task<List<CatFact>?> GetCatFactsFromApi()
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient(apiName);
                var response = await httpClient.GetAsync("/api/catfacts/");
                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<CatFact>>(responseBody) ?? [];
                }
                return [];
            } catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<CatFact?> GetNewCatFact()
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient(apiName);
                var response = await httpClient.GetAsync("/api/catfacts/newcatfact");

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    CatFact? catFact = JsonConvert.DeserializeObject<CatFact>(responseBody);
                    return catFact;
                }
                return null;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<CatFact?> DeleteCatFact(Guid id)
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient(apiName);
                var response = await httpClient.DeleteAsync($"/api/catfacts/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseBody);
                    CatFact? deletedCatFact = JsonConvert.DeserializeObject<CatFact>(responseBody);
                    return deletedCatFact;
                }
                return null;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}
