using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CatFacts.API.Models.Dtos;
using CatFacts.API.Services;

namespace CatFacts.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatFactsController(ICatFactsService catFactsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCatFacts()
        {
            var catFactDtos = await catFactsService.GetAllCatFacts();
            if (catFactDtos.Count < 1)
            {
                return NotFound();
            }
            return Ok(catFactDtos);
        }

        [HttpGet]
        [Route("newCatFact")]
        public async Task<IActionResult> GetAndSaveNewCatFact()
        {
            var catFactDto = await catFactsService.GetNewCatFact();
            if (catFactDto is null) 
            {
                return NotFound();
            }
            return Ok(catFactDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteCatFact([FromRoute] Guid id)
        {
            var deletedCatFactDto = await catFactsService.DeleteCatFact(id);
            if (deletedCatFactDto is null)
            {
                return NotFound();
            }
            return Ok(deletedCatFactDto);
        }
    }
}
