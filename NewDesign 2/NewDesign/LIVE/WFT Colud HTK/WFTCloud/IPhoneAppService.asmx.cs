using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WFTCloud.DataAccess;
using WFTCloud.ResuableRoutines;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Configuration;

namespace WFTCloud
{
    /// <summary>
    /// Summary description for IPhoneAppService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class IPhoneAppService : System.Web.Services.WebService
    {
        cgxwftcloudEntities ObjDb = new cgxwftcloudEntities();

        //For Log in
        [WebMethod]
        public string Login(string UserName,string Password)
        {
            try
            {
                MembershipUser MSU = Membership.GetUser(UserName);
                if (MSU.IsLockedOut)
                    MSU.UnlockUser();
                if (Membership.ValidateUser(UserName,Password))
                {
                    string RoleName = Roles.GetRolesForUser(UserName).First();
                    if (RoleName == DBKeys.Role_Administrator || RoleName == DBKeys.Role_SuperAdministrator)
                      return "Success";
                    else
                      return "Failed";
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                return "Error" + Ex.ToString();
            }
        }

        //Top get Course details by giving courseid
        [WebMethod]
        public string GetCourseDetailsById(int ID)
        {
            try
            {
                string CourseName = ObjDb.CourseDetails.FirstOrDefault(a => a.CourseID == ID).Description;
                return CourseName;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                return "Error" + Ex.ToString();
            }
        }

        //Top get Course opportunities details by giving courseid
        [WebMethod]
        public string GetCourseOpportunitiesDetailsById(int ID)
        {
            try
            {
                string Opportunities = ObjDb.CourseDetails.FirstOrDefault(a => a.CourseID == ID).Opportunities;
                return Opportunities;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                return "Error" + Ex.ToString();
            }
        }

        //Top save iPad app images to Uploaded content path
        [WebMethod]
        public string UploadediPadApplicationImages(byte[] imgByte)
        {
            try
            {
                if (imgByte != null)
                {
                    string ImageName = DateTime.Now.ToString("ddMMyyyymmssfff");
                    string ImageFullPath = Server.MapPath(ConfigurationManager.AppSettings["UploadeContent"] + "/UploadedContents/IpadImages/");
                    
                    MemoryStream ms = new MemoryStream(imgByte);
                    Bitmap img1 = new Bitmap(ms);

                    if (!Directory.Exists(ImageFullPath))
                    { Directory.CreateDirectory(ImageFullPath); }
                    string a = ImageFullPath + ImageName + ".jpg";
                    if (File.Exists(a))
                    {
                        File.Delete(a);
                    }
                    img1.Save(a, ImageFormat.Jpeg);
                    a = "/UploadedContents/IpadImages/" + ImageName + ".jpg";
                    return a;
                }
                else
                {
                    return "Image Byte is Null";
                }
            }
            catch (Exception ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message, ex.StackTrace, DateTime.Now);
                //DeleteReimbursements(ReimburesementID);
                return "Failed";
            }
        }


        //To get all course details
        [WebMethod]
        public List<CourseDetail> GetCourseList()
        {
            try
            {
                return ObjDb.CourseDetails.Where(a => a.Status == 1).ToList();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                return null;
            }
        }

        //To get all WFT Video details
        [WebMethod]
        public List<IndexData> GetWFTVideoList()
        {
            try
            {
                return ObjDb.IndexDatas.Where(Idx => Idx.RecordStatus == 1 && Idx.IndexDataTypeID == 2).OrderBy(Idx => Idx.Priority).ToList();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                return null;
            }
        }

        //To get all WFT Banner details
        [WebMethod]
        public List<pr_GetRandomBannerImages_Result> GetWFTBannersList()
        {
            try
            {
                List<pr_GetRandomBannerImages_Result> bannersList = new List<pr_GetRandomBannerImages_Result>();
                bannersList = ObjDb.pr_GetRandomBannerImages(true).OrderBy(a => a.ImgPriority).ToList();
                int bannerCount = int.Parse(ConfigurationManager.AppSettings["BannerCount"]);

                if (bannersList.Count() < bannerCount)
                {
                    int nonmand = bannerCount - bannersList.Count();
                    List<pr_GetRandomBannerImages_Result> bannersNonMandatory = new List<pr_GetRandomBannerImages_Result>();
                    bannersNonMandatory = ObjDb.pr_GetRandomBannerImages(false).Take(nonmand).ToList();
                    for (int i = 0; i < nonmand; i++)
                    { bannersList.Add(bannersNonMandatory[i]); }
                }
                return bannersList.OrderBy(bnr => bnr.ImgPriority).ToList();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                return null;
            }
        }

        //To get the names for(How did u know about us)
        [WebMethod]
        public List<KnowAboutU> GetKnowAboutUsList()
        {
            try
            {
                return ObjDb.KnowAboutUs.ToList();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                return null;
            }
        }

        //To get all WFT Traing details
        [WebMethod]
        public List<vwManageTrainingDetail> GetWFTVisitorTrainingDetailsList()
        {
            try
            {
                var output = ObjDb.vwManageTrainingDetails.Where(Idx => Idx.Status == 1 ).OrderBy(obj => obj.FirstName).OrderByDescending(a=>a.TrainnerID).ToList();
                return  output;
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                return null;
            }
        }

        [WebMethod]
        public List<VisitorsDetail> GetWFTVisitorsList()
        {
            try
            {
                return ObjDb.VisitorsDetails.Where(Idx => Idx.Status == 1).OrderBy(obj => obj.FirstName).OrderByDescending(a=>a.CreatedOn).ToList();
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                return null;
            }
        }

        //To Add new training details in to the table
        [WebMethod]
        public string AddNewTrainingDetails(
            string VisitorID,
            string TrainnerID,
            string FirstName,
            string LastName, 
            string Email, 
            string PhoneNumber, 
            string TemporaryAddress, 
            string PermanentAddress, 
            string CourseOfInterest, 
            string KnowAboutUs, 
            string Others,
            string UGCollege, 
            string UGPercentage, 
            string PGCollege, 
            string PGPercentage, 
            string OQCollege, 
            string OQPercentage, 
            string YearOfExperience, 
            string CurrentCompany, 
            string TechnologyCurrentlyWork, 
            string TimeIn, 
            string TimeOut, 
            string Feedback,
             string ImgPath)
        {
            try
            {
                int VisitID = 0;
                int TrainID = 0;
                if (VisitorID.Replace(" ", "") == "")
                {
                    VisitorsDetail VD = new VisitorsDetail();
                    VD.FirstName = FirstName;
                    VD.LastName = LastName;
                    VD.Email = Email;
                    VD.PhoneNumber = PhoneNumber;
                    if (ImgPath == "")
                        VD.ImgPath = null;
                    else
                        VD.ImgPath = ImgPath;
                    VD.Status = 1;
                    VD.CreatedOn = DateTime.Now;
                    ObjDb.VisitorsDetails.AddObject(VD);
                    ObjDb.SaveChanges();
                    VisitID = ObjDb.VisitorsDetails.Max(a => a.VisitorID);
                }
                if(VisitID == 0)
                    VisitID = Convert.ToInt32(VisitorID);

                if (TrainnerID.Replace(" ", "") == "")
                {
                    TrainingDetail TD = new TrainingDetail();
                    TD.FirstName = FirstName;
                    TD.LastName = LastName;
                    TD.Email = Email;
                    TD.PhoneNumber = PhoneNumber;
                    TD.TemporaryAddress = TemporaryAddress;
                    TD.PermanentAddress = PermanentAddress;
                    TD.CourseOfInterest = Convert.ToInt32(CourseOfInterest);
                    TD.KnowAboutUs = Convert.ToInt32(KnowAboutUs);
                    TD.Others = Others;
                    TD.UGCollege = UGCollege;
                    TD.UGPercentage = Convert.ToDecimal(UGPercentage);
                    if (PGCollege != "")
                    {
                        TD.PGCollege = PGCollege;
                    }
                    if (PGPercentage != "")
                    {
                        TD.PGPercentage = Convert.ToDecimal(PGPercentage);
                    }
                    if (OQCollege != "")
                    {
                        TD.OQCollege = OQCollege;
                    }
                    if (OQPercentage != "")
                    {
                        TD.OQPercentage = Convert.ToDecimal(OQPercentage);
                    }
                    TD.YearOfExperience = YearOfExperience;
                    TD.CurrentCompany = CurrentCompany;
                    TD.TechnologyCurrentlyWork = TechnologyCurrentlyWork;
                    TD.TimeIn = Convert.ToDateTime(TimeIn);
                    TD.TimeOut = Convert.ToDateTime(TimeOut);
                    TD.Feedback = Feedback;
                    TD.CreatedOn = DateTime.Now;
                    TD.Status = 1;
                    if (ImgPath != "")
                        TD.ImgPath = ImgPath;

                    TD.VisitorID = VisitID;
                    ObjDb.TrainingDetails.AddObject(TD);
                    ObjDb.SaveChanges();
                    TrainID = ObjDb.TrainingDetails.Max(a => a.TrainnerID);
                }
                else
                {
                    TrainID = Convert.ToInt32(TrainnerID);
                    var TD = ObjDb.TrainingDetails.FirstOrDefault(a => a.TrainnerID == TrainID);
                    if (TD != null)
                    {
                        TD.FirstName = FirstName;
                        TD.LastName = LastName;
                        TD.Email = Email;
                        TD.PhoneNumber = PhoneNumber;
                        TD.TemporaryAddress = TemporaryAddress;
                        TD.PermanentAddress = PermanentAddress;
                        TD.CourseOfInterest = Convert.ToInt32(CourseOfInterest);
                        TD.KnowAboutUs = Convert.ToInt32(KnowAboutUs);
                        TD.Others = Others;
                        TD.UGCollege = UGCollege;
                        TD.UGPercentage = Convert.ToDecimal(UGPercentage);
                        if (PGCollege != "")
                        {
                            TD.PGCollege = PGCollege;
                        }
                        if (PGPercentage != "")
                        {
                            TD.PGPercentage = Convert.ToDecimal(PGPercentage);
                        }
                        if (OQCollege != "")
                        {
                            TD.OQCollege = OQCollege;
                        }
                        if (OQPercentage != "")
                        {
                            TD.OQPercentage = Convert.ToDecimal(OQPercentage);
                        }
                        TD.YearOfExperience = YearOfExperience;
                        TD.CurrentCompany = CurrentCompany;
                        TD.TechnologyCurrentlyWork = TechnologyCurrentlyWork;
                        TD.TimeIn = Convert.ToDateTime(TimeIn);
                        TD.TimeOut = Convert.ToDateTime(TimeOut);
                        TD.Feedback = Feedback;
                        TD.LastModifiedOn = DateTime.Now;
                        TD.Status = 1;
                        TD.VisitorID = VisitID;
                        if (ImgPath != "")
                            TD.ImgPath = ImgPath;
                        ObjDb.SaveChanges();
                    }
                }

                var VDs = ObjDb.VisitorsDetails.FirstOrDefault(a => a.VisitorID == VisitID);
                if (VDs != null)
                {
                    VDs.TrainnerID = TrainID;
                    ObjDb.SaveChanges();
                }

                
                return "Success";
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                return "Error" + Ex.ToString();
            }
        }

        //To Add New Visitors Details
        [WebMethod]
        public string AddNewVisitorsDetails(string FirstName, string LastName, string Email, string PhoneNumber, string ImgPath,string OrgNewsLettersub)
        {
            try
            {
                VisitorsDetail VD=new VisitorsDetail();
                VD.FirstName=FirstName;
                VD.LastName=LastName;
                VD.Email=Email;
                VD.PhoneNumber=PhoneNumber;
                VD.ImgPath = ImgPath;
                try
                {
                    if (Convert.ToInt32(OrgNewsLettersub) != 1)
                        VD.Status = 0;
                    else
                        VD.Status = 1;
                }
                catch (Exception)
                {
                }
                finally
                {
                    VD.Status = 1;
                }
                VD.CreatedOn=DateTime.Now;
                VD.TrainnerID = null;
                ObjDb.VisitorsDetails.AddObject(VD);
                ObjDb.SaveChanges();
                return "Success";
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
                return "Error" +Ex.ToString();
            }
        }


        [WebMethod]
        public List<SocialTweet> GetSocialTweets()
        {
            List<SocialTweet> socialtweets = new List<SocialTweet>();
            socialtweets = ObjDb.SocialTweets.OrderByDescending(a => a.CreatedDate).Take(5).ToList() ;
            return socialtweets;
        }
    }
}
