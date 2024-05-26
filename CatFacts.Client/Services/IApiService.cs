using CatFacts.Client.Models;

namespace CatFacts.Client.Services
{
    public interface IApiService
    {
        Task<List<CatFact>?> GetCatFactsFromApi();
        Task<CatFact?> GetNewCatFact();
        Task<CatFact?> DeleteCatFact(Guid id);
    }
}
