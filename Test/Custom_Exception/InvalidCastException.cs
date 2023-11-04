using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Custom_Exception
{
    
   public class InvalidCastException : CustomeException
    {
        public InvalidCastException(String message) : base(System.Net.HttpStatusCode.InternalServerError, message)
        {

        }
    }
}