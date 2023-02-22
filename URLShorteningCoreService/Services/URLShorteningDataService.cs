using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;
using URLShorteningCoreService.Models;

namespace URLShorteningCoreService.Services
{
    public class URLShorteningDataService
    {
        private readonly IMongoCollection<ShortUrlModel> _URLShorteningDataCollection;

        public URLShorteningDataService(IURLShorteningDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _URLShorteningDataCollection = database.GetCollection<ShortUrlModel>(settings.URLShorteningCollectionName);
        }

        public async Task<ShortUrlModel?> GetAsync(string id) =>
            await _URLShorteningDataCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(ShortUrlModel ShortUrl) =>
            await _URLShorteningDataCollection.InsertOneAsync(ShortUrl);

        public async Task<ShortUrlModel?> FindAsync(string path) =>
            await _URLShorteningDataCollection.Find(x => x.OriginalUrl.Contains(path)).FirstOrDefaultAsync();

    }
}
