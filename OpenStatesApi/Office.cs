using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenStatesApi
{
    public class Office
    {
        public OfficeType Type { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }
}
