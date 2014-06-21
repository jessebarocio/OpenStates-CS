using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStatesApi
{
    public class OpenStatesHttpException : Exception
    {
        public OpenStatesHttpException() { }
        public OpenStatesHttpException(string message) : base(message) { }
        public OpenStatesHttpException(string message, Exception inner) : base(message, inner) { }
    }
}
