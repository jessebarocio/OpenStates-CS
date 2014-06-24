using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OpenStatesApi
{
    public class OpenStatesHttpException : OpenStatesException
    {
        public HttpStatusCode StatusCode { get; set; }

        public OpenStatesHttpException() { }
        public OpenStatesHttpException(string message) : base(message) { }
        public OpenStatesHttpException(string message, Exception inner) : base(message, inner) { }
    }
}
