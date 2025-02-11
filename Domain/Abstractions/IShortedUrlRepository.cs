using Domain.Entities;

namespace Domain.Abstractions
{
    public interface IShortedUrlRepository
    {
        Task<string> AddShortedUrl(ShortedUrlDTO shortedUrlDTO);
        public Task<ShortedUrl?> GetShortedUrlByCode(string code);
    }
}
