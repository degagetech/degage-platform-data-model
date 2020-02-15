﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Degage.DataModel
{

    public interface IDataActionResult
    {
        /// <summary>
        /// 获取操作返回结果的原始对象
        /// </summary>
        public Object Original { get; }
    
    }
}
