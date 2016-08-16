<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LaunchService.aspx.cs" Inherits="WFTCloud.Customer.LaunchService" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
<%--<script type="text/javascript">    $(function () { $(document.getElementById("TextBox1")).bind("contextmenu", function () { return false }); });</script>--%>

        <%--<script type="text/javascript">

            var message = "Function Disabled!";

            ///////////////////////////////////
            function clickIE4() {
                if (event.button == 2) {
                    alert(message);
                    return false;
                }
            }

            function clickNS4(e) {
                if (document.layers || document.getElementById && !document.all) {
                    if (e.which == 2 || e.which == 3) {
                        alert(message);
                        return false;
                    }
                }
            }

            if (document.layers) {
                document.captureEvents(Event.MOUSEDOWN);
                document.onmousedown = clickNS4;
            }
            else if (document.all && !document.getElementById) {
                document.onmousedown = clickIE4;
            }

            document.oncontextmenu = new Function("alert(message);return false")

            // --> 
        </script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <iframe runat="server" id="iframe" allowfullscreen="" frameborder="0"  style="height:1000px; width:100%;" webkitallowfullscreen=""  wmode="transparent"></iframe>
        </div>
    </form>
</body>
</html>
