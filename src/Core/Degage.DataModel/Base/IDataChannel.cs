using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Degage.DataModel
{
    /// <summary>
    ///表示与数据资源的连接通道
    /// </summary>
    public interface IDataChannel : IDisposable
    {
        /// <summary>
        /// 打开数据通道，建立与数据资源的连接
        /// </summary>
        /// <param name="enabledTransaction">是否启用事务性,若关联的数据资源不支持事务性，则此参数无效</param>
        /// <returns>若打开成功返回 True，否则返回 Flase</returns>
        public Boolean Open(IDataService context, Boolean enabledTransaction);

        /// <summary>
        /// <see cref="Open(bool)"/> 的异步方法
        /// </summary>
        public Task<Boolean> OpenAsync(IDataService context,Boolean enabledTransaction);


        public void Push(IDataAction action);
        public Boolean Commit();
        public Task<Boolean> CommitAsync();


        public IDataActionResult Run(IDataAction action);
        public Task<IDataActionResult> RunAsync(IDataAction action);

        public IDataActionResult<T> Run<T>(IDataAction<T> action) where T:class;
        public Task<IDataActionResult<T>> RunAsync<T>(IDataAction<T> action) where T : class;

        public void Close();
        public Task<Boolean> CloseAsync();
    }
}
