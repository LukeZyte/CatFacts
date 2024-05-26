namespace CatFacts.API.Models.Domain
{
    public class CatFact
    {
        public Guid Id { get; set; }
        public required string Fact { get; set; }
        public int Length {  get; set; }
    }
}
