using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenStatesApi
{
    public class OpenStatesException : Exception
    {
        public OpenStatesException() { }
        public OpenStatesException( string message ) : base( message ) { }
        public OpenStatesException( string message, Exception inner ) : base( message, inner ) { }
    }
}
