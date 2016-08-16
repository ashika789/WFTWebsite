using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFTCloud
{
    public static class ObjectExtensionMethods
    {
        public static bool IsNotNull(this object param)
        {
            return !(param == null);
        }
    }
}