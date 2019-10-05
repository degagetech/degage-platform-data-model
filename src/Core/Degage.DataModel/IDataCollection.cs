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
    public interface IDataCollection<T> where T : class
    {
        /// <summary>
        /// 将指定的数据对象添加到集合中
        /// </summary>
        /// <param name="obj">需要被添加的数据对象</param>
        void Add(T obj);
        /// <summary>
        /// 将一批数据对象添加至集合中
        /// </summary>
        /// <param name="objs">需要被添加的数据对象的可枚举集合</param>
        void AddRange(IEnumerable<T> objs);

        /// <summary>
        /// 从集合中移除数据对象
        /// </summary>
        IDataFilter<T> Remove();
        void Get();

        IDataFilter<T> Save();
    }
    /// <summary>
    /// 表示对数据集合操作的结果信息
    /// </summary>
    public interface IOperationResult<T> where T : class
    {
        /// <summary>
        /// 获取操作影响记录的计数
        /// </summary>
        public Int32 EffectCount { get; }
        /// <summary>
        /// 获取操作返回结果的原始对象
        /// </summary>
        public Object Original { get; }
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
    public interface IDataOperator<T> where T : class
    {
        void Execute();
        IOperationResult<T> Result { get; }
    }
    public interface IDataFilter<T>: IDataOperator<T> where T : class
    {
        /// <summary>
        /// 通过指定的表达式过滤数据
        /// </summary>
        /// <param name="predicate"></param>
        void Filtering(Expression<Func<T, Boolean>> predicate);

    }
}
