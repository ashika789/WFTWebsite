using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WFTCloud.DataAccess;

namespace WFTCloud
{
    public class GeneralReusableMethods
    {
        public static cgxwftcloudEntities objDBContext = new cgxwftcloudEntities();

        public static string GetStatusString(int statusID)
        {
            var status = objDBContext.RecordStatus.FirstOrDefault(rs => rs.RecordStatusID == statusID);
            return status != null ? status.RecordStatusDesc : "Unknown Status";
        }

        public static string GetCourseName(string crsid)
        {
            int CourseID = Convert.ToInt32(crsid);
            var curse = objDBContext.CourseDetails.FirstOrDefault(a => a.CourseID == CourseID);
            if (curse != null)
                return curse.CourseName;
            else
                return "";
        }
        
        public static string GetStatusString(string statusIDstr)
        {
            int statusID = int.Parse(statusIDstr);
            var status = objDBContext.RecordStatus.FirstOrDefault(rs => rs.RecordStatusID == statusID);
            return status != null ? status.RecordStatusDesc : "Unknown Status";
        }

        public static string GetCRMIssueStatusString(string statusIDstr)
        {
            int statusID = int.Parse(statusIDstr);
            var status = objDBContext.CRMRequestStatus.FirstOrDefault(rs => rs.CRMRequestStatusID == statusID);
            return status != null ? status.CRMRequestStatusDesc : "Unknown Status";
        }

        public static string GetStatusForRefund(string Refund)
        {
            bool status = Convert.ToBoolean(Refund);
            if (status == true)
                return "Yes";
            else
                return "No";
        }

        public static string GetActivateDeactivateIcon(string statusID)
        {
            if (statusID == "1")
                return "icon-check-empty bigger-130";
            else
                return "icon-check bigger-130";
        }

        public static string GetActivateDeactivateAction(string statusID)
        {
            if (statusID == "1")
                return "deactivate";
            else
                return "activate";
        }

        public static string GetCrmIssueType(string Type)
        {
            if (Type == "1")
                return "Technical";
            else
                return "Sales";
        }

        public static string GetIndexDataToolTip(string IndexType)
        {
            if (IndexType == "1")
                return "Preview Certificate Image/Pdf";
            else if (IndexType == "2")
                return "Preview Video";
            else if (IndexType == "3")
                return "Preview Partner Logo";
            else if (IndexType == "4")
                return "Preview Client Logo";
            else if (IndexType == "5")
                return "Preview Brochure Pdf";
            else if (IndexType == "6")
                return "Preview WhitePaper Pdf";
            else
                return "Preview";
        }

        public static string GetIndexDataClass(string IndexType)
        {
            if (IndexType == "1")
                return "icon-file-text bigger-130";
            else if (IndexType == "2")
                return "icon-facetime-video bigger-130";
            else if (IndexType == "3")
                return "icon-picture bigger-130";
            else if (IndexType == "4")
                return "icon-picture bigger-130";
            else if (IndexType == "5")
                return "icon-file bigger-130";
            else if (IndexType == "6")
                return "icon-file bigger-130";
            else
                return "icon-picture bigger-130";
        }

        public static string GetIndexDataIconColor(string IndexType)
        {
            if (IndexType == "1")
                return "primary";
            else if (IndexType == "2")
                return "orange";
            else if (IndexType == "3")
                return "green";
            else if (IndexType == "4")
                return "purple";
            else if (IndexType == "5")
                return "pink";
            else if (IndexType == "6")
                return "grey";
            else
                return "warning";
        }

    }
}