using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Degage.DataModel
{


    public interface IDataActionResult<T> : IDataActionResult where T : class
    {
        /// <summary>
        /// 获取操作结果对应的第一个对象，若无则抛出异常，
        /// </summary>
        public T First { get; }
        /// <summary>
        /// 获取操作结果对应的第一个对象，若无则返回空引用
        /// </summary>
        public T FirstOrDefault { get; }
        /// <summary>
        /// 获取包含所有操作结果的对象链表，若操作结果为空则返回一个元素个数为零的链表对象
        /// </summary>
        public IList<T> List { get; }
    }
}
