using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFTCloud
{
    /// <summary>
    /// All keys whose value comes from database table must be declared here.
    /// </summary>
    public class DBKeys
    {
        public const int RecordStatus_Active = 1;
        public const int RecordStatus_Inactive = 0;
        public const int RecordStatus_Delete = -1;
        public const int CRMRequestStatus_Closed = 2;

        public const string Role_Administrator = "Admin";
        public const string Role_SuperAdministrator = "Super Admin";
        public const string Role_PersonalUser = "Personal User";
        public const string Role_BusinessUser = "Business User";
        public const string Role_EnterpriseUser = "Enterprise User";

        public const int Admin_Email = 1;
        public const int Sales_Email = 2;
        public const int Technical_Email = 3;
        public const int HOURS_PER_MONTH = 6;
        public const int DAYS_PER_MONTH = 7;
        public const int Support_Email = 10;
        public const int Maintenance_Email = 11;
        public const int SAPBasis_Email = 12;
    }
}