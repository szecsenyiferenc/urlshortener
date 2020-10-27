using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Validators.Impl
{
    public class UrlValidator : IUrlValidator
    {
        public bool Validate(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute);
        }
    }
}
