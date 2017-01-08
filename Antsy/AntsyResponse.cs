using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Antsy
{
    public class AntsyResponse : HttpResponse
    {
        private readonly HttpResponse _response;

        public AntsyResponse(HttpResponse response)
        {
            _response = response;
        }

        public void Json(object content)
        {
            _response.ContentType = "application/json";

            string contentStr;
            if (content == null)
            {
                _response.StatusCode = 204;
                return;
            }
            else if (content is string)
            {
                contentStr = (string)content;
                if (contentStr.Length == 0)
                {
                    _response.StatusCode = 204;
                    return;
                }
            }
            else
            {
                contentStr = JsonConvert.SerializeObject(content);
            }

            var bytes = Encoding.UTF8.GetBytes(contentStr);

            _response.StatusCode = 200;
            _response.ContentLength = bytes.Length;
            _response.Body.Write(bytes, 0, bytes.Length);
        }

        public void Text(string text)
        {
            _response.ContentType = "text/plain";

            if (string.IsNullOrEmpty(text))
            {
                _response.StatusCode = 204;
                return;
            }

            var bytes = Encoding.UTF8.GetBytes(text);

            _response.StatusCode = 200;
            _response.ContentLength = bytes.Length;
            _response.Body.Write(bytes, 0, bytes.Length);
        }

        public void Html(string html)
        {
            _response.ContentType = "text/html";

            if (string.IsNullOrEmpty(html))
            {
                _response.StatusCode = 204;
                return;
            }

            var bytes = Encoding.UTF8.GetBytes(html);

            _response.StatusCode = 200;
            _response.ContentLength = bytes.Length;
            _response.Body.Write(bytes, 0, bytes.Length);
        }

        public override Stream Body
        {
            get
            {
                return _response.Body;
            }
            set
            {
                _response.Body = value;
            }
        }

        public override long? ContentLength
        {
            get
            {
                return _response.ContentLength;
            }
            set
            {
                _response.ContentLength = value;
            }
        }

        public override string ContentType
        {
            get
            {
                return _response.ContentType;
            }
            set
            {
                _response.ContentType = value;
            }
        }

        public override IResponseCookies Cookies
        {
            get
            {
                return _response.Cookies;
            }
        }

        public override bool HasStarted
        {
            get
            {
                return _response.HasStarted;
            }
        }

        public override IHeaderDictionary Headers
        {
            get
            {
                return _response.Headers;
            }
        }

        public override HttpContext HttpContext
        {
            get
            {
                return _response.HttpContext;
            }
        }

        public override int StatusCode
        {
            get
            {
                return _response.StatusCode;
            }
            set
            {
                _response.StatusCode = value;
            }
        }

        public override void OnCompleted(Func<object, Task> callback, object state)
        {
            _response.OnCompleted(callback, state);
        }

        public override void OnStarting(Func<object, Task> callback, object state)
        {
            _response.OnStarting(callback, state);
        }

        public override void Redirect(string location, bool permanent)
        {
            _response.Redirect(location, permanent);
        }
    }
}
