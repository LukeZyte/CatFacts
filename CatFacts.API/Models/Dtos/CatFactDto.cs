namespace CatFacts.API.Models.Dtos
{
    public class CatFactDto
    {
        public Guid Id { get; set; }
        public required string Fact { get; set; }
        public int Length { get; set; }
    }
}
