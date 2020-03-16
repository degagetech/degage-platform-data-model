using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Degage.DataModel
{
    /// <summary>
    /// 数据操作过滤动作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FilterAction<T> : DataAction where T : class
    {
        /// <summary>
        /// 表示此数据动作的类型
        /// </summary>
        public override DataActionType ActionType { get; internal set; } = DataActionType.Filter;

        /// <summary>
        /// 用于提供筛选依据的条件表达式
        /// </summary>
        public Expression<Func<T, Boolean>> Condition { get; protected set; }

        /// <summary>
        /// 表示此过滤器与其他过滤器的组合类型
        /// </summary>
        public FilterCombinationType CombinationType { get; internal set; }

        internal FilterAction(Expression<Func<T, Boolean>> condition, FilterCombinationType combinationType = FilterCombinationType.AndAlso,DataAction lastAction=null)
        {
            this.Condition = condition;
            this.CombinationType = combinationType;
        }

        public FilterAction<T> AndAlso(Expression<Func<T, Boolean>> condition)
        {
            FilterAction<T> action = new FilterAction<T>(condition, FilterCombinationType.AndAlso);
            this.Next = action;
            return action;
        }

        public FilterAction<T> OrElse(Expression<Func<T, Boolean>> condition)
        {
            FilterAction<T> action = new FilterAction<T>(condition, FilterCombinationType.OrElse);
            this.Next = action;
            return action;
        }

    }

    /// <summary>
    /// 数据操作过滤器的可能的组合类型的值
    /// </summary>
    public enum FilterCombinationType
    {
       /// <summary>
       /// 并且
       /// </summary>
        AndAlso,
        /// <summary>
        /// 或者
        /// </summary>
        OrElse
    }
}
