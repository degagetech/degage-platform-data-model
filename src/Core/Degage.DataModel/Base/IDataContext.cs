using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Degage.DataModel
{
    /// <summary>
    /// 表示对指定数据存储环境的引用
    /// </summary>
    public interface IDataContext
    {
        IDataActionResult Run(IDataAction action);
        IDataActionResult<T> Run<T>(IDataAction action) where T : class;
    }
}
