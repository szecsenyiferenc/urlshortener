using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Repository
{
    public interface IUrlRepository
    {
        string GetUrl(string guid);
        string AddUrl(string url);
        void Save();
    }
}
