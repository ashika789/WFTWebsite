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

// # Sample for GetExpressCheckoutDetails API   
// The GetExpressCheckoutDetails API operation obtains information about
// an Express Checkout transaction.
// This sample code uses Merchant .NET SDK to make API call. You can
// download the SDKs [here](https://github.com/paypal/sdk-packages/tree/gh-pages/merchant-sdk/dotnet)
public class GetExpressCheckoutDetailsCommand
{
    // # Static constructor for configuration setting
    static GetExpressCheckoutDetailsCommand()
    {
        // Load the log4net configuration settings from Web.config or App.config    
        //log4net.Config.XmlConfigurator.Configure();
    }

    // # GetExpressCheckout API Operation
    // The GetExpressCheckoutDetails API operation obtains information about an Express Checkout transaction
    public GetExpressCheckoutDetailsResponseType GetExpressCheckoutDetailsAPIOperation(string strToken)
    {
        // Create the GetExpressCheckoutDetailsResponseType object
        GetExpressCheckoutDetailsResponseType responseGetExpressCheckoutDetailsResponseType = new GetExpressCheckoutDetailsResponseType();

        try
        {
            // Create the GetExpressCheckoutDetailsReq object
            GetExpressCheckoutDetailsReq getExpressCheckoutDetails = new GetExpressCheckoutDetailsReq();

            // A timestamped token, the value of which was returned by `SetExpressCheckout` response
            GetExpressCheckoutDetailsRequestType getExpressCheckoutDetailsRequest = new GetExpressCheckoutDetailsRequestType(strToken);
            getExpressCheckoutDetails.GetExpressCheckoutDetailsRequest = getExpressCheckoutDetailsRequest;

            // Create the service wrapper object to make the API call
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService();

            // # API call
            // Invoke the GetExpressCheckoutDetails method in service wrapper object
            responseGetExpressCheckoutDetailsResponseType = service.GetExpressCheckoutDetails(getExpressCheckoutDetails);

            if (responseGetExpressCheckoutDetailsResponseType != null)
            {
                // Response envelope acknowledgement
                string acknowledgement = "GetExpressCheckoutDetails API Operation - ";
                acknowledgement += responseGetExpressCheckoutDetailsResponseType.Ack.ToString();
                //logger.Info(acknowledgement + "\n");
                //Console.WriteLine(acknowledgement + "\n");

                // # Success values
                if (responseGetExpressCheckoutDetailsResponseType.Ack.ToString().Trim().ToUpper().Equals("SUCCESS"))
                {
                    // Unique PayPal Customer Account identification number. This
                    // value will be null unless you authorize the payment by
                    // redirecting to PayPal after `SetExpressCheckout` call.
                    //logger.Info("Payer ID : " + responseGetExpressCheckoutDetailsResponseType.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerID + "\n");
                    //Console.WriteLine("Payer ID : " + responseGetExpressCheckoutDetailsResponseType.GetExpressCheckoutDetailsResponseDetails.PayerInfo.PayerID + "\n");

                }
                // # Error Values
                else
                {
                    List<ErrorType> errorMessages = responseGetExpressCheckoutDetailsResponseType.Errors;
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
            throw ex;
            // Log the exception message       
            //logger.Debug("Error Message : " + ex.Message);
            //Console.WriteLine("Error Message : " + ex.Message);
        }
        return responseGetExpressCheckoutDetailsResponseType;
    }

    
}


