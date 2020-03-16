using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Degage.DataModel
{
    public class SearchAction<T> : DataAction where T : class
    {
        /// <summary>
        /// 表示此数据动作的类型
        /// </summary>
        public override DataActionType ActionType { get; internal set; } = DataActionType.Search;

        /// <summary>
        /// 用于提供搜索依据的条件表达式
        /// </summary>
        public Expression<Func<T, Boolean>> Condition { get; protected set; }


        internal SearchAction(Expression<Func<T, Boolean>> condition)
        {
            this.Condition = condition;
        }

       
        public FilterAction<T> Filter(Expression<Func<T, Boolean>> condition)
        {
            FilterAction<T> action = new FilterAction<T>(condition, FilterCombinationType.AndAlso);
            this.ContactNext(action);
            return action;
        }

        public FilterAction<T> OrElseFilter(Expression<Func<T, Boolean>> condition)
        {
            FilterAction<T> action = new FilterAction<T>(condition, FilterCombinationType.OrElse);
            this.ContactNext(action);
            return action;
        }


    }
}
