using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Test.Custom_Exception
{

    public class CustomeException : Exception
    {
        private readonly HttpStatusCode StatusCode;
        //private readonly HttpStatusCode message;

        public HttpStatusCode StatusCode1 => StatusCode;

        public CustomeException(HttpStatusCode statusCode, string message, Exception ex)
            : base(message, ex)
        {
            this.StatusCode = statusCode;
        }
        public CustomeException(HttpStatusCode statusCode, string message)
            : base(message)
        {
            this.StatusCode = statusCode;
        }
        public CustomeException(HttpStatusCode statusCode)
        {
            this.StatusCode = statusCode;
        }

    

    }
}