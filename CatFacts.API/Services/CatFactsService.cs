
using CatFacts.API.Models.Domain;
using CatFacts.API.Models.Dtos;
using CatFacts.API.Repositories;

namespace CatFacts.API.Services
{
    public class CatFactsService(ICatFactsRepository catFactsRepository, IHttpClientFactory httpClientFactory) : ICatFactsService
    {
        public readonly string apiName = "NinjaApi";

        public async Task<CatFactDto?> GetNewCatFact()
        {
            var httpClient = httpClientFactory.CreateClient(apiName);
            var response = await httpClient.GetAsync("https://catfact.ninja/fact");
            var newCatFactDto = await response.Content.ReadFromJsonAsync<AddRequestCatFactDto>();

            if (newCatFactDto is null)
            {
                return null;
            }
            var catFact = new CatFact
            {
                Id = Guid.NewGuid(),
                Fact = newCatFactDto.Fact,
                Length = newCatFactDto.Length,
            };

            await catFactsRepository.AddCatFactToFile(catFact);
            var catFactDto = new CatFactDto
            {
                Id = catFact.Id,
                Fact = catFact.Fact,
                Length = catFact.Length,
            };
            return catFactDto;
        }

        public async Task<CatFactDto?> DeleteCatFact(Guid id)
        {
            var catFact = await catFactsRepository.DeleteCatFactFromFile(id);
            if (catFact is null)
            {
                return null;
            }
            var catFactDto = new CatFactDto
            {
                Id = catFact.Id,
                Fact = catFact.Fact,
                Length = catFact.Length,
            };

            return catFactDto;
        }

        public async Task<List<CatFactDto>> GetAllCatFacts()
        {
            var catFacts = await catFactsRepository.GetAllCatFactsFromFile();
            List<CatFactDto> catFactDtos = catFacts.Select(cf => new CatFactDto
            {
                Id = cf.Id,
                Fact = cf.Fact,
                Length = cf.Length,
            }).ToList();

            return catFactDtos;
        }
    }
}
