// # Namespaces  
using System;      
using System.Collections.Generic;   
// # NuGet Install          
// Visual Studio 2012 and 2010 Command:  
// Install-Package PayPalMerchantSDK        
// Visual Studio 2005 and 2008 (NuGet.exe) Command:   
// install PayPalMerchantSDK      
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;

// # Sample for CreateRecurringPaymentsProfile API  
// The CreateRecurringPaymentsProfile API operation creates a recurring
// payments profile.
// You must invoke the CreateRecurringPaymentsProfile API operation for each
// profile you want to create. The API operation creates a profile and an
// associated billing agreement.
// `Note:
// There is a one-to-one correspondence between billing agreements and
// recurring payments profiles. To associate a recurring payments profile
// with its billing agreement, you must ensure that the description in the
// recurring payments profile matches the description of a billing
// agreement. For version 54.0 and later, use SetExpressCheckout to initiate
// creation of a billing agreement.`
// This sample code uses Merchant .NET SDK to make API call. You can
// download the SDKs [here](https://github.com/paypal/sdk-packages/tree/gh-pages/merchant-sdk/dotnet)
public class CreateRecurringPaymentsProfileCommand
{
    // # Static constructor for configuration setting
    static CreateRecurringPaymentsProfileCommand()
    {
        // Load the log4net configuration settings from Web.config or App.config    
        //log4net.Config.XmlConfigurator.Configure();
    }

    // # CreateRecurringPaymentsProfile API Operation
    // The CreateRecurringPaymentsProfile API operation creates a recurring payments profile.
    // You must invoke the CreateRecurringPaymentsProfile API operation for each profile you want to create. 
    // The API operation creates a profile and an associated billing agreement. 
    // Note: 
    // There is a one-to-one correspondence between billing agreements and recurring payments profiles. 
    // To associate a recurring payments profile with its billing agreement, 
    // you must ensure that the description in the recurring payments profile matches the description of a billing agreement. 
    // For version 54.0 and later, use SetExpressCheckout to initiate creation of a billing agreement.
    public CreateRecurringPaymentsProfileResponseType CreateRecurringPaymentsProfileAPIOperation(string strToken, DateTime SubscriptionStartDt, decimal SubscriptionAmt, string RecurringDesc, int BillingPeriod )
    {
        // Create the CreateRecurringPaymentsProfileResponseType object
        CreateRecurringPaymentsProfileResponseType responseCreateRecurringPaymentsProfileResponseType = new CreateRecurringPaymentsProfileResponseType();

        try
        {
            // Create the CreateRecurringPaymentsProfileReq object
            CreateRecurringPaymentsProfileReq createRecurringPaymentsProfile = new CreateRecurringPaymentsProfileReq();

            // Create the CreateRecurringPaymentsProfileRequestType object
            CreateRecurringPaymentsProfileRequestType createRecurringPaymentsProfileRequest = new CreateRecurringPaymentsProfileRequestType();

            // You can include up to 10 recurring payments profiles per request. The
            // order of the profile details must match the order of the billing
            // agreement details specified in the SetExpressCheckout request which
            // takes mandatory argument:
            // 
            // * `billing start date` - The date when billing for this profile begins.
            // `Note:
            // The profile may take up to 24 hours for activation.`
            //Sample date format "2013-12-31T13:01:19+00:00"
            RecurringPaymentsProfileDetailsType recurringPaymentsProfileDetails
                = new RecurringPaymentsProfileDetailsType(SubscriptionStartDt.ToString("yyyy-MM-dd") + "T13:01:19+00:00");

            // Billing amount for each billing cycle during this payment period.
            // This amount does not include shipping and tax amounts.
            // `Note:
            // All amounts in the CreateRecurringPaymentsProfile request must have
            // the same currency.`
            BasicAmountType billingAmount = new BasicAmountType(CurrencyCodeType.USD, SubscriptionAmt.ToString());

            // Regular payment period for this schedule which takes mandatory
            // params:
            //  
            // * `Billing Period` - Unit for billing during this subscription period. It is one of the
            // following values:
            //  * Day
            //  * Week
            //  * SemiMonth
            //  * Month
            //  * Year
            //  For SemiMonth, billing is done on the 1st and 15th of each month.
            //  `Note:
            //  The combination of BillingPeriod and BillingFrequency cannot exceed
            //  one year.`
            // * `Billing Frequency` - Number of billing periods that make up one billing cycle.
            // The combination of billing frequency and billing period must be less
            // than or equal to one year. For example, if the billing cycle is
            // Month, the maximum value for billing frequency is 12. Similarly, if
            // the billing cycle is Week, the maximum value for billing frequency is
            // 52.
            // `Note:
            // If the billing period is SemiMonth, the billing frequency must be 1.`
            // * `Billing Amount`
            BillingPeriodDetailsType paymentPeriod = new BillingPeriodDetailsType(BillingPeriodType.MONTH, 1, billingAmount);
            
            // Describes the recurring payments schedule, including the regular
            // payment period, whether there is a trial period, and the number of
            // payments that can fail before a profile is suspended which takes
            // mandatory params:
            //  
            // * `Description` - Description of the recurring payment.
            // `Note:
            // You must ensure that this field matches the corresponding billing
            // agreement description included in the SetExpressCheckout request.`
            // * `Payment Period`
            ScheduleDetailsType scheduleDetails = new ScheduleDetailsType(RecurringDesc, paymentPeriod);
            scheduleDetails.PaymentPeriod.TotalBillingCycles = BillingPeriod;
            // `CreateRecurringPaymentsProfileRequestDetailsType` which takes
            // mandatory params:
            //      
            // * `Recurring Payments Profile Details`
            // * `Schedule Details`
            CreateRecurringPaymentsProfileRequestDetailsType createRecurringPaymentsProfileRequestDetails
                = new CreateRecurringPaymentsProfileRequestDetailsType(recurringPaymentsProfileDetails, scheduleDetails);

            // Either EC token or a credit card number is required.If you include
            // both token and credit card number, the token is used and credit card number is
            // ignored
            // In case of setting EC token,
            // `createRecurringPaymentsProfileRequestDetails.Token = "EC-5KH01765D1724703R";`
            // A timestamped token, the value of which was returned in the response
            // to the first call to SetExpressCheckout. Call
            // CreateRecurringPaymentsProfile once for each billing
            // agreement included in SetExpressCheckout request and use the same
            // token for each call. Each CreateRecurringPaymentsProfile request
            // creates a single recurring payments profile.
            // `Note:
            // Tokens expire after approximately 3 hours.`

            // Credit card information for recurring payments using direct payments.
            //CreditCardDetailsType creditCard = new CreditCardDetailsType();

            // Type of credit card. For UK, only Maestro, MasterCard, Discover, and
            // Visa are allowable. For Canada, only MasterCard and Visa are
            // allowable and Interac debit cards are not supported. It is one of the
            // following values:
            //  
            // * Visa
            // * MasterCard
            // * Discover
            // * Amex
            // * Solo
            // * Switch
            // * Maestro: See note.
            // `Note:
            // If the credit card type is Maestro, you must set currencyId to GBP.
            // In addition, you must specify either StartMonth and StartYear or
            // IssueNumber.`
            //creditCard.CreditCardType = CreditCardTypeType.VISA;

            // Credit Card Number
            //creditCard.CreditCardNumber = "4442662639546634";

            // Credit Card Expiration Month
            //creditCard.ExpMonth = Convert.ToInt32("12");

            // Credit Card Expiration Year
            //creditCard.ExpYear = Convert.ToInt32("2016");
            //createRecurringPaymentsProfileRequestDetails.CreditCard = creditCard;

            createRecurringPaymentsProfileRequestDetails.Token = strToken;

            createRecurringPaymentsProfileRequest.CreateRecurringPaymentsProfileRequestDetails
                = createRecurringPaymentsProfileRequestDetails;

            createRecurringPaymentsProfile.CreateRecurringPaymentsProfileRequest = createRecurringPaymentsProfileRequest;

            // # Create the service wrapper object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();


            // # API call
            // Invoke the CreateRecurringPaymentsProfile method
            responseCreateRecurringPaymentsProfileResponseType
                = service.CreateRecurringPaymentsProfile(createRecurringPaymentsProfile);

            if (responseCreateRecurringPaymentsProfileResponseType != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "CreateRecurringPaymentsProfile API Operation - ";
                acknowledgement += responseCreateRecurringPaymentsProfileResponseType.Ack.ToString();
                //logger.Info(acknowledgement + "\n");
                //Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseCreateRecurringPaymentsProfileResponseType.Ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // A unique identifier for future reference to the details of this recurring payment
                    //logger.Info("Profile ID : " + responseCreateRecurringPaymentsProfileResponseType.CreateRecurringPaymentsProfileResponseDetails.ProfileID + "\n");
                    //Console.WriteLine("Profile ID : " + responseCreateRecurringPaymentsProfileResponseType.CreateRecurringPaymentsProfileResponseDetails.ProfileID + "\n");
                }
                // # Error Values           
                else
                {
                    List<ErrorType> errorMessages = responseCreateRecurringPaymentsProfileResponseType.Errors;
                    foreach (ErrorType error in errorMessages)
                    {
                        //logger.Debug("API Error Message : " + error.LongMessage);
                        //Console.WriteLine("API Error Message : " + error.LongMessage + "\n");
                    }
                }
            }
        }
        // # Exception log    
        catch (System.Exception ex)
        {
            // Log the exception message       
            //logger.Debug("Error Message : " + ex.Message);
            //Console.WriteLine("Error Message : " + ex.Message);
        }
        return responseCreateRecurringPaymentsProfileResponseType;
    }

    
}

