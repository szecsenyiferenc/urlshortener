using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Repository
{
    public interface IUrlRepository
    {
        Task<string> GetUrl(string key);
        Task AddUrl(string key, string value);
        string GetLastId();
    }
}
