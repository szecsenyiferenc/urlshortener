using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Services
{
    public interface IUrlService
    {
        Task<string> GetUrl(string guid);
        Task<string> AddUrl(string url);
    }
}
