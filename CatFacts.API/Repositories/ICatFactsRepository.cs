using CatFacts.API.Models.Domain;

namespace CatFacts.API.Repositories
{
    public interface ICatFactsRepository
    {
        Task AddCatFactToFile(CatFact catFact);
        Task<List<CatFact>> GetAllCatFactsFromFile();
        Task<CatFact?> DeleteCatFactFromFile(Guid id);
    }
}
