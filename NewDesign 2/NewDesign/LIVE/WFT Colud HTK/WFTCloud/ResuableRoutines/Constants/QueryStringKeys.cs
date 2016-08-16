using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WFTCloud
{
    /// <summary>
    /// All keys used to access query string keys must be listed here.
    /// </summary>
    public class QueryStringKeys
    {
        public const string Delete = "delete";
        public const string Activate = "activate";
        public const string Deactivate = "deactivate";
        public const string Pagination = "pagination";
        public const string ShowPage = "showpage";

        //Key used by category screen.
        public const string EditCategoryID = "editcategoryid";
        //Key used by User profile screen.
        public const string ViewUserProfileID = "viewuserprofileid";

        public const string EditUser = "edituser";
        //key used by edit subscribed service.
        public const string EditSubscribedService = "SubscribedServiceid";
        //key used by edit subscribed service.
        public const string UserServiceProvision = "UserServiceProvisionid";
        //key used by edit manage crm issues.
        public const string EditCrmIssues = "crmrequestid";
        //key used for tab controls in user service screen
        public const string ShowView = "showview";

        

		//key used for repeater
        public const string CheckRepeater = "userid";
        //key used for add new services
        public const string GetAddNewService = "addnewserviceid";

        public const string ChangePassword = "changepassword";

        public const string SendEmail = "sendemailid";


		public const string UserProfileID = "userprofileid";

        public const string PageAccess = "pageaccess";

        public const string EditService = "editservice";

        public const string ServiceID = "serviceid";

        public const string EditCoupon = "editcoupon";
        public const string CouponID = "couponid";
        public const string EditFAQ = "editfaqid";
        public const string EditCMS = "editcmsid";
        public const string EditIndexData = "editindexdataid";
        public const string EditWftCloudResource = "editWftCloudid";
        public const string EditBanner = "editbannerid";
        public const string EditServiceProvision = "editserviceprovisionid";
        public const string EditTrainingDetails = "edittrainingdetailid";
        public const string EditCourseDetails = "editcoursedetailid";
        public const string EditVisitorDetails = "editvisitordetails";
        public const string LoadRefund = "refundid";
        public const string EditPaymentDetails = "editPaymentdetailsid";
        public const string EditCustomerCRM = "editcustomercrmid";
        public const string EditTestimonials = "editTestimonialsid";
        public const string AccessAccount = "accessaccountid";
        public const string PressReleaseID = "pressreleaseid";
        public const string EditPressRelease = "editpressreleaseid";
        public const string PreviewID = "previewid";
        public const string AdminID = "adminmembershipid";
        public const string Preview = "view";
    }
}