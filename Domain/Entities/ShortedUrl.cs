using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ShortedUrl
    {
        public Guid Guid { get; set; }
        public string OriginalUrl { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public static ShortedUrl Create(ShortedUrlDTO shortedUrlDTO)
        {
            return new ShortedUrl
            {
                Guid = Guid.NewGuid(),
                OriginalUrl = shortedUrlDTO.OriginalUrl,
                Code = Guid.NewGuid().ToString().Substring(0, 6),
                CreatedAt = DateTime.Now
            };
        }
    }
}
