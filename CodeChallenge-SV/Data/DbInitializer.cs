using CodeChallenge_SV.Constants;
using Newtonsoft.Json;

namespace CodeChallenge_SV.Data
{
    public class DbInitializer
    {
        public static void Initialize(SearchContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
            dbContext.Database.EnsureCreated();

            if (dbContext.Buildings.Any() || dbContext.Groups.Any() || dbContext.Locks.Any() || dbContext.Media.Any()) return;

            var data = JsonConvert.DeserializeObject<SearchDataHolder>(File.ReadAllText(FilePath.Path));
            if (data != null)
            {
                dbContext.Buildings.AddRange(data.Buildings);
                dbContext.Groups.AddRange(data.Groups);
                dbContext.Locks.AddRange(data.Locks);
                dbContext.Media.AddRange(data.Media);
                dbContext.SaveChanges();
            }
        }
    }
}
