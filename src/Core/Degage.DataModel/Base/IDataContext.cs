using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Degage.DataModel
{
    /// <summary>
    /// 与各类数据源交互的桥梁
    /// </summary>
    public interface IDataContext
    {
        IDataCollection<T> GetDataCollection<T>() where T : class;
        IDataActionResult ExecuteAction(IDataAction action);
        IDataActionResult<T> ExecuteAction<T>(IDataAction action) where T : class;
        IDataCollection GetDataCollect();
    }
}
