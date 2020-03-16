using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Degage.DataModel
{
    public interface IDataAction<T>:IDataAction where T:class
    {
       
    }
}
