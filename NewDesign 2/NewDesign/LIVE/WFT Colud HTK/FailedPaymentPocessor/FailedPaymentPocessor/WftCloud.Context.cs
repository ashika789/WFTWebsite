﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PaymentProcessor
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class cgxwftcloudEntities1 : DbContext
    {
        public cgxwftcloudEntities1()
            : base("name=cgxwftcloudEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<AdminPageAccess> AdminPageAccesses { get; set; }
        public DbSet<AdminPage> AdminPages { get; set; }
        public DbSet<AllAutomatedPayment> AllAutomatedPayments { get; set; }
        public DbSet<AutomatedPayment> AutomatedPayments { get; set; }
        public DbSet<AutomatedPaymentTransaction> AutomatedPaymentTransactions { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<CouponType> CouponTypes { get; set; }
        public DbSet<CourseDetail> CourseDetails { get; set; }
        public DbSet<CreditCardDetail> CreditCardDetails { get; set; }
        public DbSet<CRMIssueType> CRMIssueTypes { get; set; }
        public DbSet<CRMRequestNote> CRMRequestNotes { get; set; }
        public DbSet<CRMRequest> CRMRequests { get; set; }
        public DbSet<CRMRequestStatu> CRMRequestStatus { get; set; }
        public DbSet<CustomerPaymentProfile> CustomerPaymentProfiles { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<ExLog> ExLogs { get; set; }
        public DbSet<FAQCategoryType> FAQCategoryTypes { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<FAQType> FAQTypes { get; set; }
        public DbSet<IndexData> IndexDatas { get; set; }
        public DbSet<IndexDataType> IndexDataTypes { get; set; }
        public DbSet<KnowAboutU> KnowAboutUs { get; set; }
        public DbSet<NewsLetterSignUp> NewsLetterSignUps { get; set; }
        public DbSet<RecordStatu> RecordStatus { get; set; }
        public DbSet<ServiceCatalog> ServiceCatalogs { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<ServiceProvision> ServiceProvisions { get; set; }
        public DbSet<SitePagesAndContent> SitePagesAndContents { get; set; }
        public DbSet<SocialTweet> SocialTweets { get; set; }
        public DbSet<SurveyChoice> SurveyChoices { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<TrainingDetail> TrainingDetails { get; set; }
        public DbSet<UserCart> UserCarts { get; set; }
        public DbSet<UserOrderDetail> UserOrderDetails { get; set; }
        public DbSet<UserOrder> UserOrders { get; set; }
        public DbSet<UserPaymentTransaction> UserPaymentTransactions { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserRefundPayment> UserRefundPayments { get; set; }
        public DbSet<UserServicesInvoice> UserServicesInvoices { get; set; }
        public DbSet<UserSubscribedService> UserSubscribedServices { get; set; }
        public DbSet<UserSurveyRespons> UserSurveyResponses { get; set; }
        public DbSet<VisitorsDetail> VisitorsDetails { get; set; }
        public DbSet<WftCloudResource> WftCloudResources { get; set; }
        public DbSet<WFTCloudUserType> WFTCloudUserTypes { get; set; }
        public DbSet<WftSetting> WftSettings { get; set; }
    
        public virtual int pr_UpdatePaymentResponse(string customerProfileID, string paymentProfileID, Nullable<decimal> returnAmount, Nullable<int> returnApproved, string returnAuthorizationCode, string returnCardNumber, string returnAuthMessage, string returnResponseCode, string returnTransactionID, string invoiceNumber, Nullable<int> userProfileID)
        {
            var customerProfileIDParameter = customerProfileID != null ?
                new ObjectParameter("CustomerProfileID", customerProfileID) :
                new ObjectParameter("CustomerProfileID", typeof(string));
    
            var paymentProfileIDParameter = paymentProfileID != null ?
                new ObjectParameter("PaymentProfileID", paymentProfileID) :
                new ObjectParameter("PaymentProfileID", typeof(string));
    
            var returnAmountParameter = returnAmount.HasValue ?
                new ObjectParameter("ReturnAmount", returnAmount) :
                new ObjectParameter("ReturnAmount", typeof(decimal));
    
            var returnApprovedParameter = returnApproved.HasValue ?
                new ObjectParameter("ReturnApproved", returnApproved) :
                new ObjectParameter("ReturnApproved", typeof(int));
    
            var returnAuthorizationCodeParameter = returnAuthorizationCode != null ?
                new ObjectParameter("ReturnAuthorizationCode", returnAuthorizationCode) :
                new ObjectParameter("ReturnAuthorizationCode", typeof(string));
    
            var returnCardNumberParameter = returnCardNumber != null ?
                new ObjectParameter("ReturnCardNumber", returnCardNumber) :
                new ObjectParameter("ReturnCardNumber", typeof(string));
    
            var returnAuthMessageParameter = returnAuthMessage != null ?
                new ObjectParameter("ReturnAuthMessage", returnAuthMessage) :
                new ObjectParameter("ReturnAuthMessage", typeof(string));
    
            var returnResponseCodeParameter = returnResponseCode != null ?
                new ObjectParameter("ReturnResponseCode", returnResponseCode) :
                new ObjectParameter("ReturnResponseCode", typeof(string));
    
            var returnTransactionIDParameter = returnTransactionID != null ?
                new ObjectParameter("ReturnTransactionID", returnTransactionID) :
                new ObjectParameter("ReturnTransactionID", typeof(string));
    
            var invoiceNumberParameter = invoiceNumber != null ?
                new ObjectParameter("InvoiceNumber", invoiceNumber) :
                new ObjectParameter("InvoiceNumber", typeof(string));
    
            var userProfileIDParameter = userProfileID.HasValue ?
                new ObjectParameter("UserProfileID", userProfileID) :
                new ObjectParameter("UserProfileID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("pr_UpdatePaymentResponse", customerProfileIDParameter, paymentProfileIDParameter, returnAmountParameter, returnApprovedParameter, returnAuthorizationCodeParameter, returnCardNumberParameter, returnAuthMessageParameter, returnResponseCodeParameter, returnTransactionIDParameter, invoiceNumberParameter, userProfileIDParameter);
        }
    }
}
