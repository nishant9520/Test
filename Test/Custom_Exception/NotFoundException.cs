using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test.Custom_Exception
{

    public class NotFoundException : CustomeException
    {
        public NotFoundException(String message) : base(System.Net.HttpStatusCode.NotFound, message)
        {

        }
    }
}