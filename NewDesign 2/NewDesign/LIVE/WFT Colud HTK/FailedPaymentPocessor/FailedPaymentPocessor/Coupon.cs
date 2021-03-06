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
    
    public partial class Coupon
    {
        public int CouponID { get; set; }
        public string CouponName { get; set; }
        public string CouponCode { get; set; }
        public int CouponTypeID { get; set; }
        public decimal Discount { get; set; }
        public Nullable<bool> IsUsed { get; set; }
        public Nullable<int> UserCount { get; set; }
        public Nullable<int> ValidityInDays { get; set; }
        public Nullable<System.DateTime> ValidityDate { get; set; }
        public Nullable<System.Guid> ForUser { get; set; }
        public Nullable<int> ForServiceID { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<System.Guid> ModifiedBy { get; set; }
        public int RecordStatus { get; set; }
        public Nullable<long> CouponCount { get; set; }
    }
}
