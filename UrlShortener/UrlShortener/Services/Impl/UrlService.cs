using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Providers;
using UrlShortener.Repository;

namespace UrlShortener.Services.Impl
{
    public class UrlService : IUrlService
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IIdProvider _idProvider;

        public UrlService(IUrlRepository urlRepository, IIdProvider idProvider)
        {
            _urlRepository = urlRepository;
            _idProvider = idProvider;
        }

        public async Task<string> AddUrl(string url)
        {
            var guid = _idProvider.GetId();
            await _urlRepository.AddUrl(guid, url);
            return guid;
        }

        public Task<string> GetUrl(string guid)
        {
            return _urlRepository.GetUrl(guid);
        }
    }
}
