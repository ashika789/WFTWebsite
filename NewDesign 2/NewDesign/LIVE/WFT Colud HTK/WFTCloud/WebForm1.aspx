<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WFTCloud.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script  type="text/javascript" src="/paypal-button.min.js"></script>
    <style type="text/css">
        .paypal-button button:after {
    content: " ";
    position: absolute;
    width: 98%;
    height: 60%;
    border-radius: 40px 40px 38px 38px;
    top: 0px;
    left: 0px;
    background: -moz-linear-gradient(center top , rgb(254, 254, 254) 0%, rgb(254, 217, 148) 100%) repeat scroll 0% 0% transparent;
    z-index: -1;
    transform: translateX(1%);
}
.paypal-button button:before {
    content: " ";
    position: absolute;
    width: 100%;
    height: 100%;
    border-radius: 11px;
    top: 0px;
    left: 0px;
    background: -moz-linear-gradient(center top , rgb(255, 170, 0) 0%, rgb(255, 170, 0) 80%, rgb(255, 248, 252) 100%) repeat scroll 0% 0% transparent;
    z-index: -2;
}
element {
}
.paypal-button button.large {
    padding: 4px 19px;
    font-size: 14px;
}
.paypal-button button {
    white-space: nowrap;
    overflow: hidden;
    border-radius: 13px;
    font-family: "Arial",bold,italic;
    font-weight: bold;
    font-style: italic;
    border: 1px solid rgb(255, 168, 35);
    color: rgb(14, 49, 104);
    background: none repeat scroll 0% 0% rgb(255, 168, 35);
    position: relative;
    text-shadow: 0px 1px 0px rgba(255, 255, 255, 0.5);
    cursor: pointer;
    z-index: 0;
}
    </style>
</head>
<body>
<form style="opacity: 1;" target="_top" class="paypal-button" action="https://www.sandbox.paypal.com/cgi-bin/webscr" method="post">
    <input value="subscribe" name="button" type="hidden" />
    <input value="hthirukkumaran-facilitator@gmail.com" name="business" type="hidden" />
    <input value="SAP Is Retail" name="item_name" type="hidden" />
    <input value="5" name="amount" type="hidden" />
    <input value="USD" name="currency_code" type="hidden" />
    <input value="1" name="p3" type="hidden" />
    <input value="M" name="t3" type="hidden" />
    <input value="http://sapcloud.creagx.com/index.aspx" name="notify_url" type="hidden" />
    <input value="www.sandbox" name="env" type="hidden" />
    <input value="_xclick-subscriptions" name="cmd" type="hidden" />
    <input value="5" name="a3" type="hidden" />
    <input value="JavaScriptButton_subscribe" name="bn" type="hidden" />
    <button class="paypal-button large" type="submit">PayPal</button>
</form>
</body>
</html>
