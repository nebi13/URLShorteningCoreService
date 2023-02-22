using System.Threading.Tasks;
using URLShorteningCoreService.Models;

namespace URLShorteningCoreService.Services
{
    public interface IURLShorteningService
    {
        ShortUrlModel GetById(string id);

        Task<ShortUrlModel> GetByPathAsync(string path);

        Task<ShortUrlModel> GetByOriginalUrlAsync(string originalUrl);

        Task<string> SaveAsync(ShortUrlModel shortUrl);
    }
}
