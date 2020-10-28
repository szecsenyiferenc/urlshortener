using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Utils
{
    public interface INumericBaseConverter
    {
        public string IntToBase(int value);

        public int BaseToInt(string number);

    }
}
