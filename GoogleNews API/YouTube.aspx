<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YouTube.aspx.cs" Inherits="YouTube_API.YouTubeSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>YouTube TryIt</title>
    <script type="text/javascript" src="Scripts/jquery-2.1.0.min.js"></script>
    <script type="text/javascript">
        // Function for GetVideos using Ajax Post Method
        function GetVideos() {
            // Create Ajax Post Method
            $.ajax({
                type: "POST", // Ajax Mehod
                url: "YouTube.aspx/GetVideos",  // Page URL / Method name
                data: "{'topic':'" + document.getElementById("input").value + "'}", // Pass Parameters
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (data) { // Function call on success
                    $("#DivVideos").empty(); // Set Div Empty

                    // Set For loop for binding Div Row wise
                    for (var i = 0; i < data.d.length; i++) {
                        // Design Div Dynamically using append
                        $("#DivVideos").append(
                            "<a target=\"_blank\" href=\"http://www.youtube.com/watch?v=" + data.d[i].id + "\">" + data.d[i].title + "</a><br /><blockquote>"
                            + data.d[i].description + "</blockquote><br /><br />"
                        );
                    }
                },
                error: function (result) {
                    // Function call on failure or error
                    alert(result.d);
                }
            });

            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table>
            <tr>
                <td align="center" class="style1">
                    <h3>YouTube API TryIt</h3>
                </td>
            </tr>
            <tr>
                <td>Method Signature: GetVideos(string):JSON Array</td>
            </tr>
            <tr>
                <td>String Value: <asp:TextBox runat="server" ID="input" CssClass="textbox" /></td>
                <td><asp:Button id="Button1" Text="Submit" OnClientClick="return GetVideos()" runat="server"/></td>
            </tr>
        </table>
        <br />
        <br />
        <div id="DivVideos"></div>
    </form>
</body>
</html>
