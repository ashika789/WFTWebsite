using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFTCloud
{
    /// <summary>
    /// Extension Methods for String DataType
    /// </summary>
    public static class StringExtensionMethods
    {
        public static bool IsValid(this String str)
        {
            return !string.IsNullOrEmpty(str);
        }
    }
}