using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WFTCloud.ResuableRoutines;

namespace WFTCloud.User
{
    public partial class press_release_staticcontent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    WFTCloud.DataAccess.cgxwftcloudEntities objDBContext = new DataAccess.cgxwftcloudEntities();
                    string Preview = Request.QueryString[QueryStringKeys.Preview];
                    if (Preview == null)
                    {
                        if (Request.QueryString[QueryStringKeys.PressReleaseID].IsValid())
                        {
                            int PressReleaseID = int.Parse(Request.QueryString[QueryStringKeys.PressReleaseID]);
                            var Press = objDBContext.PressReleases.FirstOrDefault(pr => pr.PressReleaseID == PressReleaseID);
                            lblPressReleaseHeader.Text = Press.PressReleaseHeader;
                            lblDate.Text = Convert.ToDateTime(Press.PressReleaseDate).ToString("MMMM dd, yyyy");
                            lblCompanyDescription.Text = Press.CompanyDescription;
                            lblPlaceName.Text = Press.PlaceName + " " + Convert.ToDateTime(Press.PressReleaseDate).ToString("MMMM dd, yyyy");
                            lblPressReleaseContent.Text = HttpUtility.HtmlDecode(Press.PressReleaseContent);
                            if (Press.ActualPRLink != null && Press.ActualPRLink.Replace(" ","") != "")
                            {
                                ActualPRLink.HRef = Press.ActualPRLink.Trim();
                                ActualPRLink.Visible = true;
                            }
                            else
                            {
                                ActualPRLink.Visible = false;
                            }
                            if (Press.CaptionContent != null && Press.CaptionContent != "")
                            {
                                pnlQuote.Visible = true;
                                lblCaption.Text = Press.CaptionContent;
                            }
                            else
                            {
                                pnlQuote.Visible = false;
                            }
                            if (Press.VideoURL != null && Press.VideoURL != "")
                            {
                                pnlVideo.Visible = true;
                                iframe.Attributes["src"] = Press.VideoURL;
                            }
                            else
                            {
                                pnlVideo.Visible = false;
                            }
                            if (Press.ImagePath != null && Press.ImagePath != "")
                            {
                                pnlImage.Visible = true;
                                Image.Attributes["src"] = Press.ImagePath;
                            }
                            else
                            {
                                pnlImage.Visible = false;
                            }
                        }
                    }
                    else
                    {
                        if (Request.QueryString[QueryStringKeys.PreviewID].IsValid())
                        {
                            int PreviewID = int.Parse(Request.QueryString[QueryStringKeys.PreviewID]);
                            var Press = objDBContext.PressReleases.FirstOrDefault(pr => pr.PressReleaseID == PreviewID);
                            lblPressReleaseHeader.Text = Press.PreviewPressReleaseHeader;
                            lblDate.Text = Convert.ToDateTime(Press.PreviewPressReleaseDate).ToString("MMMM dd, yyyy");
                            lblCompanyDescription.Text = Press.PreviewCompanyDescription;
                            lblPlaceName.Text = Press.PlaceName + " " + Convert.ToDateTime(Press.PreviewPressReleaseDate).ToString("MMMM dd, yyyy");
                            lblPressReleaseContent.Text = (Press.PreviewPressReleaseContent);
                            if (Press.ActualPRLink != null && Press.ActualPRLink.Replace(" ", "") != "")
                            {
                                ActualPRLink.HRef = Press.ActualPRLink.Trim();
                                ActualPRLink.Visible = true;
                            }
                            else
                            {
                                ActualPRLink.Visible = false;
                            }
                            if (Press.PreviewCaptionContent != null)
                            {
                                pnlQuote.Visible = true;
                                lblCaption.Text = Press.PreviewCaptionContent;
                            }
                            else
                            {
                                pnlQuote.Visible = false;
                            }
                            if (Press.PreviewVideoURL != null && Press.PreviewVideoURL != "")
                            {
                                pnlVideo.Visible = true;
                                iframe.Attributes["src"] = Press.PreviewVideoURL;
                            }
                            else
                            {
                                pnlVideo.Visible = false;
                            }
                            if (Press.ImagePath != null && Press.ImagePath != "")
                            {
                                pnlImage.Visible = true;
                                Image.Attributes["src"] = Press.ImagePath;
                            }
                            else
                            {
                                pnlImage.Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                ReusableRoutines.LogException(this.GetType().ToString(), System.Reflection.MethodBase.GetCurrentMethod().Name, Ex.Message, Ex.StackTrace, DateTime.Now);
            }
        }
    }
}