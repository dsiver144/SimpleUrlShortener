using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace Infrastructure.Repositories
{
    internal class ShortedUrlRepository : IShortedUrlRepository
    {
        readonly AppDbContext _dbContext;

        public ShortedUrlRepository(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<ShortedUrl?> GetShortedUrlByCode(string code)
        {
            return await _dbContext.ShortedUrls.FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task<string> AddShortedUrl(ShortedUrlDTO shortedUrlDTO)
        {
            var existingUrl = await _dbContext.ShortedUrls.FirstOrDefaultAsync(x => x.OriginalUrl == shortedUrlDTO.OriginalUrl);
            if (existingUrl != null)
            {
                throw new UrlExistedException("Existed url: " + existingUrl.Code);
            }
            var shortedUrl = ShortedUrl.Create(shortedUrlDTO);
            _dbContext.ShortedUrls.Add(shortedUrl);
            await _dbContext.SaveChangesAsync();
            return shortedUrl.Code;
        }
    }
}
