using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Antsy
{

    public class AntsyRequest : HttpRequest
    {
        private readonly HttpRequest _request;

        public AntsyRequest(HttpRequest request)
        {
            _request = request;
        }

        public override Stream Body
        {
            get
            {
                return _request.Body;
            }

            set
            {
                _request.Body = value;
            }
        }

        public override long? ContentLength
        {
            get
            {
                return _request.ContentLength;
            }

            set
            {
                _request.ContentLength = value;
            }
        }

        public override string ContentType
        {
            get
            {
                return _request.ContentType;
            }

            set
            {
                _request.ContentType = value;
            }
        }

        public override IRequestCookieCollection Cookies
        {
            get
            {
                return _request.Cookies;
            }

            set
            {
                _request.Cookies = value;
            }
        }

        public override IFormCollection Form
        {
            get
            {
                return _request.Form;
            }

            set
            {
                _request.Form = value;
            }
        }

        public override bool HasFormContentType
        {
            get
            {
                return _request.HasFormContentType;
            }
        }

        public override IHeaderDictionary Headers
        {
            get
            {
                return _request.Headers;
            }
        }

        public override HostString Host
        {
            get
            {
                return _request.Host;
            }

            set
            {
                _request.Host = value;
            }
        }

        public override HttpContext HttpContext
        {
            get
            {
                return _request.HttpContext;
            }
        }

        public override bool IsHttps
        {
            get
            {
                return _request.IsHttps;
            }
            set
            {
                _request.IsHttps = value;
            }
        }

        public override string Method
        {
            get
            {
                return _request.Method;
            }
            set
            {
                _request.Method = value;
            }
        }

        public override PathString Path
        {
            get
            {
                return _request.Path;
            }
            set
            {
                _request.Path = value;
            }
        }

        public override PathString PathBase
        {
            get
            {
                return _request.PathBase;
            }
            set
            {
                _request.PathBase = value;
            }
        }

        public override string Protocol
        {
            get
            {
                return _request.Protocol;
            }
            set
            {
                _request.Protocol = value;
            }
        }

        public override IQueryCollection Query
        {
            get
            {
                return _request.Query;
            }
            set
            {
                _request.Query = value;
            }
        }

        public override QueryString QueryString
        {
            get
            {
                return _request.QueryString;
            }
            set
            {
                _request.QueryString = value;
            }
        }

        public override string Scheme
        {
            get
            {
                return _request.Scheme;
            }
            set
            {
                _request.Scheme = value;
            }
        }

        public override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}
