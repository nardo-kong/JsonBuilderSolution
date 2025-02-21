using JsonBuilder.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonBuilder.Core.Utilities
{
    public class ParameterValidation
    {
        public static bool ValidateAllParameters(
            MessageBase messageBase,
            out Dictionary<string, object> validatedParameters,
            out IList<string> errorMessages
        )
        {
            errorMessages = new List<string>();
            validatedParameters = new Dictionary<string, object>();

            bool result = true;
            result &= ValidateMessage(messageBase, "", validatedParameters, errorMessages);

            return result;
            //return false;
        }

        private static bool ValidateMessage(
            MessageBase message,
            string parentPath,
            Dictionary<string, object> validatedParameters,
            IList<string> errorMessages
        )
        {
            bool isValid = true;

            var parameters = message.Parameters?.ToDictionary();

            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    string key = $"{parentPath}{param.Key}";
                    var value = param.Value;

                    // 检查值是否为空
                    if (!IsValidValue(value))
                    {
                        errorMessages.Add($"Parameter '{key}' is required but was empty.");
                        isValid = false;
                    }
                    else
                    {
                        validatedParameters[key] = value;
                        Debug.WriteLine($"Parameter '{key}' is valid.");
                    }
                }
            }

            // 递归校验子消息
            foreach (var nested in message.NestedMessages)
            {
                isValid &= ValidateMessage(nested, parentPath + "Nested:", validatedParameters, errorMessages);
            }

            return isValid;
        }

        private static bool IsValidValue(object value)
        {
            if (value == null)
                return false;
            
            switch (value)
            {
                case string s when string.IsNullOrWhiteSpace(s):
                    return false;
                case ICollection collection when collection.Count == 0:
                    return false;
                case IEnumerable enumerable:
                    foreach (var item in enumerable)
                    {
                        if (item != null)
                            return true;
                    }
                    return false;
                case INotifyDataErrorInfo _:
                    throw new NotSupportedException("Nested INotifyDataErrorInfo is not supported.");
                default:
                    return true;
            }
        }
    }
}
