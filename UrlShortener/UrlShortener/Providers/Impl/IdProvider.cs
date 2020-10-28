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
        private const int START_INDEX = 0;
        private readonly INumericBaseConverter _numericBaseConverter;
        private int _counter;

        public IdProvider(INumericBaseConverter numericBaseConverter)
        {
            _counter = START_INDEX;
            _numericBaseConverter = numericBaseConverter;
        }

        public void Init(string startId)
        {
            if (startId != null)
            {
                int id = _numericBaseConverter.BaseToInt(startId);

                _counter = id;
            }

        }

        public string GetId()
        {
            int current = Interlocked.Increment(ref _counter);

            string id = _numericBaseConverter.IntToBase(current);

            return id;
        }

    }
}
