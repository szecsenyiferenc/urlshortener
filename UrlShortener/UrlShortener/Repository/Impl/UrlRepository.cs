using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Repository.Impl
{
    public class UrlRepository : IUrlRepository
    {
        private const string LAST_KEY = "lastkey";
        private readonly IConnectionMultiplexer _connectionMultiplexer;
        private readonly TimeSpan _expiration = TimeSpan.FromDays(1);

        public UrlRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task AddUrl(string key, string value)
        {
            var db = _connectionMultiplexer.GetDatabase();
            ITransaction transacton = db.CreateTransaction();
            Task<bool> setKey = transacton.StringSetAsync(key, value, _expiration);
            Task<bool> setLastKey = transacton.StringSetAsync(LAST_KEY, key);
            if (await transacton.ExecuteAsync()) 
            {
                await setKey;
                await setLastKey;
            }
        }

        public async Task<string> GetUrl(string key)
        {
            var db = _connectionMultiplexer.GetDatabase();
            return await db.StringGetAsync(key);
        }

        public string GetLastId()
        {
            var db = _connectionMultiplexer.GetDatabase();
            return db.StringGet(LAST_KEY);
        }

    }
}
