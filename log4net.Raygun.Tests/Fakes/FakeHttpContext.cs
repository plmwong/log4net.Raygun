﻿using System.IO;
using System.Web;

namespace log4net.Raygun.Tests.Fakes
{
	public class FakeHttpContext : IHttpContext
	{
		private readonly HttpApplication _httpApplication;

		public static FakeHttpContext For(HttpApplication httpApplication)
		{
			return new FakeHttpContext(httpApplication);
		}

		private FakeHttpContext(HttpApplication httpApplication)
		{
			_httpApplication = httpApplication;
		}

	    public HttpContext Instance
	    {
	        get { return new HttpContext(new HttpRequest("foo", "http://bar", "baz"), new HttpResponse(new StringWriter())); }
	    }

	    public HttpApplication ApplicationInstance
		{
			get
			{
				return _httpApplication;
			}
		}
	}
}

