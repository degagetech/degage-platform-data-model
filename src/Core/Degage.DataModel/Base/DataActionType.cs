using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Degage.DataModel
{
    /// <summary>
    /// 表示数据操作类型的可能值
    /// </summary>
    public enum DataActionType
    {
        /// <summary>
        /// 通常用于关系型数据库的自定义SQL执行
        /// </summary>
        CustomSQL = 0x0001,

        /// <summary>
        /// 数据搜索，通常带有条件
        /// </summary>
        Search = 0x0002,

        /// <summary>
        /// 数据筛选，通常与其他数据动作组合使用
        /// </summary>
        Filter=0x0003,

        /// <summary>
        /// 限制获取数据的数量，通常与其他数据动作组合使用
        /// </summary>
        Limit = 0x0004,

        /// <summary>
        /// 数据统计操作，以统计满足条件的数据的条数
        /// </summary>
        Count=0x0005,

        /// <summary>
        /// 数据添加
        /// </summary>
        Add = 0x1001,

        /// <summary>
        /// 批量添加数据
        /// </summary>
        AddRange = 0x1002,


        /// <summary>
        /// 数据更新，通常带有更新条件，以及需要更新的值
        /// </summary>
        Update = 0x2001,

        /// <summary>
        /// 数据移除，通产带有移除的条件
        /// </summary>
        Remove = 0x3001

    }
}
