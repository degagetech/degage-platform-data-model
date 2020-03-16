using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Degage.DataModel
{
    public interface IDataActionParameter
    {
        public String Name { get; set; }
        public Object Value { get; set; }

    }
}

