using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLShorteningCoreService.Models;

namespace URLShorteningCoreService.Services
{
    public class URLShorteningService: IURLShorteningService
    {
        private readonly URLShorteningDataService _URLShorteningDataService;

        public URLShorteningService(URLShorteningDataService URLShorteningDataService)
        {
            _URLShorteningDataService = URLShorteningDataService;
        }

        public ShortUrlModel GetById(string id)
        {
            return _URLShorteningDataService.GetAsync(id).Result;
        }

        public async Task<string> SaveAsync(ShortUrlModel shortUrl)
        {
            await _URLShorteningDataService.CreateAsync(shortUrl);

            return shortUrl.Id;
        }

        public async Task<ShortUrlModel> GetByPathAsync(string path)
        {
            return await _URLShorteningDataService.FindAsync(path);
        }

        public async Task<ShortUrlModel> GetByOriginalUrlAsync(string originalUrl)
        {
            return await _URLShorteningDataService.FindAsync(originalUrl);
        }
    }
}
