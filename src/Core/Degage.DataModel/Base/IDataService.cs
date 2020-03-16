using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Degage.DataModel
{
    /// <summary>
    /// 提供对特定数据资源的访问功能
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// 是否支持事务操作
        /// </summary>
        public Boolean SuppoertedTransaction { get; }

        IDataActionResult Run(IDataAction action);

        IDataActionResult<T> Run<T>(IDataAction action) where T : class;
    }
}
