using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Repository.Impl
{
    public class UrlRepository : IUrlRepository
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly TimeSpan _expiration = TimeSpan.FromDays(1);

        public UrlRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task AddUrl(string key, string value)
        {
            var db = _connectionMultiplexer.GetDatabase();
            await db.StringSetAsync(key, value, _expiration);
        }

        public async Task<string> GetUrl(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.StringGetAsync(key);
        }

    }
}
