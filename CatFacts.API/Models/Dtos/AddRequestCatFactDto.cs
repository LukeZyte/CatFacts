namespace CatFacts.API.Models.Dtos
{
    public class AddRequestCatFactDto
    {
        public required string Fact { get; set; }
        public int Length { get; set; }
    }
}
