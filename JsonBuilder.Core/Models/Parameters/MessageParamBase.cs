using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JsonBuilder.Core.Models.Parameters
{
    public abstract class MessageParamBase
    {
        // 将当前参数的所有属性转换为 Dictionary<string, object>
        public virtual IDictionary<string, object> ToDictionary()
        {
            return GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead)
                .ToDictionary(p => p.Name.ToLower(), p => p.GetValue(this));
        }
    }
}
