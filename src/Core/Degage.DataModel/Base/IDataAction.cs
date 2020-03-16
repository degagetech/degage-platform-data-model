using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Degage.DataModel
{
    public interface IDataAction
    {
        /// <summary>
        /// 获取此操作位于整个操作树中的层级值
        /// </summary>
        Int32 Level { get; }
        /// <summary>
        /// 获取此操作关联的 “上一个（通常这会是此操作上一级的一个操作）” 操作
        /// </summary>
        IDataAction PrevAction { get; }

        /// <summary>
        /// 获取此操作所在的同级的操作链
        /// </summary>
        IList<IDataAction> Chain { get; }

        /// <summary>
        /// 此操作表示的公式
        /// </summary>
        String Formula { get; }
    }


}
