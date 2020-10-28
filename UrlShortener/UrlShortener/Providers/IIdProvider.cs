using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Providers
{
    public interface IIdProvider
    {
        void Init(string startId);
        string GetId();
    }
}
