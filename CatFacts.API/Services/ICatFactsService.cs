using CatFacts.API.Models.Dtos;
using CatFacts.API.Models.Domain;

namespace CatFacts.API.Services
{
    public interface ICatFactsService
    {
        Task<List<CatFactDto>> GetAllCatFacts();
        Task<CatFactDto?> GetNewCatFact();
        Task<CatFactDto?> DeleteCatFact(Guid id);
    }
}
