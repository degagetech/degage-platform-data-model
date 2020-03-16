using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Degage.DataModel
{
    /// <summary>
    /// 存储 <see cref="IDataAction"/> 的执行结果信息
    /// </summary>
    public interface IDataActionResult
    {
        /// <summary>
        /// 获取操作返回结果的原始对象
        /// </summary>
        public Object Original { get; }

    }
}
