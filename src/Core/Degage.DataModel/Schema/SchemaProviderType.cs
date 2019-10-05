using System;
using System.Collections.Generic;
using System.Text;

namespace Degage.DataModel.Schema
{
    /// <summary>
    /// Schema 信息提供器类型的集合
    /// </summary>
    public enum SchemaProviderType
    {
        /// <summary>
        /// 文件加载
        /// </summary>
        File = 1,
        /// <summary>
        /// 元数据特性
        /// </summary>
        Attribute = 2,
        /// <summary>
        /// 结合类型信息自动推导
        /// </summary>
        AutoDeduce = 3
    }
}
