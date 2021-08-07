using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Antsy
{
  /// <summary>
  /// the outgoing http response
  /// </summary>
  public class AntsyResponse : HttpResponse
  {
    private readonly HttpResponse _response;

    /// <summary>
    /// Creates a new response based on an existing <see cref="HttpResponse"/>
    /// </summary>
    public AntsyResponse(HttpResponse response)
    {
      _response = response;
    }

    /// <summary>
    /// Formats a response as json. The content can either be a string or an object.
    /// </summary>
    public void Json(object content)
    {
      if (content == null)
        throw new ArgumentNullException(nameof(content));

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
        contentStr = JsonConvert.SerializeObject(content);

      var bytes = Encoding.UTF8.GetBytes(contentStr);

      _response.StatusCode = 200;
      _response.ContentLength = bytes.Length;
      _response.Body.Write(bytes, 0, bytes.Length);
    }

    /// <summary>
    /// Formats the response as text.
    /// </summary>
    public void SendText(string text)
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

    /// <summary>
    /// Formats the response as html. Can either be an html string or filepath.
    /// </summary>
    public void SendHtml(string html)
    {
      _response.ContentType = "text/html";

      if (string.IsNullOrEmpty(html))
      {
        _response.StatusCode = 204;
        return;
      }

      byte[] bytes;
      if (File.Exists(html))
        bytes = File.ReadAllBytes(html);
      else
        bytes = Encoding.UTF8.GetBytes(html);

      _response.StatusCode = 200;
      _response.ContentLength = bytes.Length;
      _response.Body.Write(bytes, 0, bytes.Length);
    }

    /// <summary>
    /// Formats the response as a file to download.
    /// </summary>
    public void Download(string filename, Stream filecontent)
    {
      _response.StatusCode = 200;
      _response.ContentType = "application/octet-stream";
      _response.Headers.Add("Content-Disposition", "attachment; filename=" + filename);
      _response.ContentLength = filecontent.Length;
      filecontent.CopyTo(_response.Body);
    }

    /// <summary>
    /// Formats the response as a file to download.
    /// </summary>
    public void Download(string filepath)
    {
      FileInfo fileInfo = new FileInfo(Path.Combine(Directory.GetCurrentDirectory(), filepath));
      if (fileInfo.Exists)
      {
        using (var fs = System.IO.File.OpenRead(fileInfo.FullName))
        {
          Download(fileInfo.Name, fs);
        }
      }
      else
        throw new FileNotFoundException(fileInfo.FullName);
    }

    #region HttpResponse
    /// <summary>
    /// Gets or sets the response body.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the value of the Content-Length response header.
    /// </summary>
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

    /// <summary>
    /// Gets or sets the value for the Content-Type response header.
    /// </summary>
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

    /// <summary>
    /// Gets an object that can be used to manage cookies for this response.
    /// </summary>
    public override IResponseCookies Cookies
    {
      get
      {
        return _response.Cookies;
      }
    }

    /// <summary>
    /// Gets a value indicating whether response headers have been sent to the client.
    /// </summary>
    public override bool HasStarted
    {
      get
      {
        return _response.HasStarted;
      }
    }

    /// <summary>
    /// Gets the response headers.
    /// </summary>
    public override IHeaderDictionary Headers
    {
      get
      {
        return _response.Headers;
      }
    }

    /// <summary>
    /// Gets the <see cref="Microsoft.AspNetCore.Http.HttpContext"/> for this response.
    /// </summary>
    public override HttpContext HttpContext
    {
      get
      {
        return _response.HttpContext;
      }
    }

    /// <summary>
    /// Gets or sets the HTTP status code.
    /// </summary>
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

    /// <summary>
    /// Adds a delegate to be invoked after the response has finished being sent to the client.
    /// </summary>
    /// <param name="callback">The delegate to invoke. Supplies the state object as the argument.</param>
    /// <param name="state">A state object to capture and pass back to the delegate.</param>
    public override void OnCompleted(Func<object, Task> callback, object state)
    {
      _response.OnCompleted(callback, state);
    }

    /// <summary>
    /// Adds a delegate to be invoked just before response headers will be sent to the client.
    /// </summary>
    /// <param name="callback">The delegate to invoke. Supplies the state object as the argument.</param>
    /// <param name="state">A state object to capture and pass back to the delegate.</param>
    public override void OnStarting(Func<object, Task> callback, object state)
    {
      _response.OnStarting(callback, state);
    }

    /// <summary>
    /// Returns a redirect response (HTTP 301 or HTTP 302) to the client.
    /// </summary>
    /// <param name="location">The url to redirect the client to.</param>
    /// <param name="permanent">True if redirect is permanent (301).</param>
    public override void Redirect(string location, bool permanent)
    {
      _response.Redirect(location, permanent);
    }
    #endregion

  }
}
