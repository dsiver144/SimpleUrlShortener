using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (await _dbContext.ShortedUrls.AnyAsync(x => x.OriginalUrl == shortedUrlDTO.OriginalUrl))
            {
                throw new UrlExistedException("ShortedUrl already exists");
            }
            var shortedUrl = ShortedUrl.Create(shortedUrlDTO);
            _dbContext.ShortedUrls.Add(shortedUrl);
            await _dbContext.SaveChangesAsync();
            return shortedUrl.Code;
        }
    }
}
