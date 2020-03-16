using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Degage.DataModel
{
    /// <summary>
    ///表示与多个数据资源建立的连接
    /// </summary>
    public interface IMultiDataChannel :IDataChannel, IDisposable
    {
        public Boolean OpenMulti(params IDataService[] contexts);

        /// <summary>
        /// <see cref="Open(bool)"/> 的异步方法
        /// </summary>
        public Task<Boolean> OpenMultiAsync(params IDataService[] contexts);
    }
}
