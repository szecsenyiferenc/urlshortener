using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UrlShortener.Utils;

namespace UrlShortener.Providers.Impl
{
    public class IdProvider : IIdProvider
    {
        private readonly INumbericBaseConverter _numbericBaseConverter;
        private int _counter;

        public IdProvider(INumbericBaseConverter numbericBaseConverter)
        {
            _counter = 1;
            _numbericBaseConverter = numbericBaseConverter;
        }

        public string GetId()
        {
            int current = Interlocked.Increment(ref _counter);

            string id = _numbericBaseConverter.IntToBase(current).PadLeft(6, '0');

            return id;
        }
    }
}
