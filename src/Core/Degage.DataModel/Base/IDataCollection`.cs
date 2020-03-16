using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Degage.DataModel
{
    /// <summary>
    /// 表示一个相同结构的数据的集合
    /// </summary>
    public interface IDataCollection<T>:IDataCollection where T : class
    {
        /// <summary>
        /// 将指定的数据对象添加到集合中
        /// </summary>
        /// <param name="obj">需要被添加的数据对象</param>
        IDataAction<T> Add(T obj);
        /// <summary>
        /// 将一批数据对象添加至集合中
        /// </summary>
        /// <param name="objs">需要被添加的数据对象的可枚举集合</param>
        IDataAction<T> AddRange(IEnumerable<T> objs);

        IDataAction<T> Search(Expression<Func<T,Boolean>> condition=null);

        IDataAction<T> Remove(Expression<Func<T, Boolean>> condition = null);

    }

}
