using CatFacts.API.Models.Domain;

namespace CatFacts.API.Repositories
{
    public class FileCatFactsRepository : ICatFactsRepository
    {
        private readonly string _path = $"{AppDomain.CurrentDomain.BaseDirectory}/catFacts.txt";
        public async Task AddCatFactToFile(CatFact catFact)
        {
            using FileStream fs = File.Open(_path, FileMode.Append);
            using StreamWriter sw = new(fs);

            await sw.WriteLineAsync($"{catFact.Id}\t{catFact.Fact}\t{catFact.Length}");
        }

        public async Task<CatFact?> DeleteCatFactFromFile(Guid id)
        {
            FileStream fs = File.Open(_path, FileMode.OpenOrCreate);
            StreamReader sr = new(fs);

            string line;
            List<CatFact> catFacts = [];
            List<CatFact> deletedCatFacts = [];
            while (sr.EndOfStream == false)
            {
                line = await sr.ReadLineAsync();
                string[] fields = line.Split('\t');

                if (id == new Guid(fields[0]))
                {
                    var foundCatFact = new CatFact
                    {
                        Id = new Guid(fields[0]),
                        Fact = fields[1],
                        Length = int.Parse(fields[2])
                    };
                    deletedCatFacts.Add(foundCatFact);
                    continue;
                }

                var catFact = new CatFact
                {
                    Id = new Guid(fields[0]),
                    Fact = fields[1],
                    Length = int.Parse(fields[2])
                };
                catFacts.Add(catFact);
            }
            fs.Close();
            sr.Close();

            using FileStream fsw = File.Open(_path, FileMode.Create);
            using StreamWriter sww = new(fsw);

            catFacts.ForEach(async (cf) => await sww.WriteLineAsync($"{cf.Id}\t{cf.Fact}\t{cf.Length}"));

            if (deletedCatFacts.Count < 1)
            {
                return null;
            }
            return deletedCatFacts[0];
        }

        public async Task<List<CatFact>> GetAllCatFactsFromFile()
        {
            using FileStream fs = File.Open(_path, FileMode.OpenOrCreate);
            using StreamReader sr = new(fs);

            string line;
            List<CatFact> catFacts = [];
            while(sr.EndOfStream == false)
            {
                line = await sr.ReadLineAsync();
                string[] fields = line.Split('\t');

                var catFact = new CatFact
                {
                    Id = new Guid(fields[0]),
                    Fact = fields[1],
                    Length = int.Parse(fields[2])
                };
                catFacts.Add(catFact);
            }
            return catFacts;
        }
    }
}
