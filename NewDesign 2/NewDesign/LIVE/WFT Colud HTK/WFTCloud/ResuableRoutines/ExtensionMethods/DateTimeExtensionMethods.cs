using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFTCloud.ResuableRoutines.ExtensionMethods
{
    /// <summary>
    /// ExtensionMethods for DateTime.
    /// </summary>
    public static class DateTimeExtensionMethods
    {
        public static bool IsToday(this DateTime dtParam)
        {
            return dtParam.Date == DateTime.Today;
        }
    }
}