using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Degage.DataModel
{
    public abstract class DataAction
    {
        /// <summary>
        /// 表示此数据动作的类型
        /// </summary>
        public abstract DataActionType ActionType { get; internal set; }

        protected DataAction()
        {

        }
            
        protected DataAction(DataAction lastAction) : this()
        {
            this.Contact(lastAction);
        }

        /// <summary>
        /// 与指定的上一个数据动作建立关联
        /// </summary>
        /// <param name="lastAction"></param>
        protected virtual void Contact(DataAction lastAction)
        {
            this.Last = lastAction;
            if (lastAction != null)
            {
                this.Head = lastAction.Head;
            }
        }

        protected virtual void ContactNext(DataAction nextAction)
        {
            this.Next = nextAction;
        }

        /// <summary>
        /// 断开与当前下一个数据动作的关联
        /// </summary>
        public virtual void BreakNext()
        {
            this.Next = null;
        }

        /// <summary>
        /// 与此数据动作相关的头动作
        /// </summary>
        public DataAction Head { get; protected set; }

        /// <summary>
        /// 与此数据动作相关的下一个动作
        /// </summary>
        public DataAction Next { get; protected set; }

        /// <summary>
        /// 与此数据动作相关的上一个动作
        /// </summary>
        public DataAction Last { get; protected set; }


        /// <summary>
        /// 将两个数据动作进行关联，返回右边的数据动作的引用
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static DataAction operator +(DataAction left, DataAction right)
        {
            right.Contact(left);
            return right;
        }

        /// <summary>
        /// 将两个数据动作断开关联，返回左边的数据动作的引用
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static DataAction operator -(DataAction left, DataAction right)
        {
            left.BreakNext();
            return left;
        }
    }
}
