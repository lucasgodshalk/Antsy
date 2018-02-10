using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Antsy
{
  public class AntsyRequest : HttpRequest
  {
    private readonly HttpRequest _request;

    public AntsyRequest(HttpRequest request)
    {
      _request = request;
    }

    /// <summary>
    /// Treats the request body as a json payload and deserializes to the specified type.
    /// </summary>
    public T BodyJson<T>()
    {
      using (var sr = new StreamReader(Body, Encoding.UTF8))
      {
        using (var jr = new JsonTextReader(sr))
        {
          var serializer = new JsonSerializer();
          return serializer.Deserialize<T>(jr);
        }
      }
    }

    /// <summary>
    /// Treats the request body as a text payload and returns the body as a string.
    /// </summary>
    public string BodyText()
    {
      using (var sr = new StreamReader(Body, Encoding.UTF8))
      {
        return sr.ReadToEnd();
      }
    }

    #region HttpRequest
    /// <summary>
    /// Gets or sets the RequestBody stream.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the Content=Length header.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the Content-Type header.
    /// </summary>
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

    /// <summary>
    /// Gets the collection of Cookies for this request.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the request body as a form.
    /// </summary>
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

    /// <summary>
    /// Checks the content-type header for form types.
    /// </summary>
    public override bool HasFormContentType
    {
      get
      {
        return _request.HasFormContentType;
      }
    }

    /// <summary>
    /// Gets the request headers.
    /// </summary>
    public override IHeaderDictionary Headers
    {
      get
      {
        return _request.Headers;
      }
    }

    /// <summary>
    /// Gets or sets the Host header. May include the port.
    /// </summary>
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

    /// <summary>
    /// Gets the <see cref="Microsoft.AspNetCore.Http.HttpContext"/> for this request.
    /// </summary>
    public override HttpContext HttpContext
    {
      get
      {
        return _request.HttpContext;
      }
    }

    /// <summary>
    /// Returns true if the RequestScheme is https.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the HTTP method.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the request path from RequestPath.
    /// </summary>
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

    /// <summary>
    /// Gets or sets RequestPathBase
    /// </summary>
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

    /// <summary>
    /// Gets or sets RequestProtocol
    /// </summary>
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

    /// <summary>
    /// Gets the query value collection parsed from <see cref="QueryString" />
    /// </summary>
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

    /// <summary>
    /// Gets or sets the raw query string used to create the query collection in <see cref="Query"/>.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the HTTP request scheme.
    /// </summary>
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

    public async override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
      return await _request.ReadFormAsync(cancellationToken);
    }
    #endregion
  }
}
