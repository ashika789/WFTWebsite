//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class UserSurveyRespons
    {
        public int UserSurveyResponseID { get; set; }
        public int SurveyQuestionID { get; set; }
        public string SurveyAnswer { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.Guid CreatedBy { get; set; }
    }
}