<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="requests.aspx.cs" Inherits="compound_web.requests" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            font-size: x-large;
            text-align: center;
            margin-left: 400px;
        }
        .auto-style2 {
            text-align: left;
        }
        .auto-style3 {
            text-align: center;
            font-size: x-large;
        }
        .auto-style4 {
            font-size: medium;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="auto-style1">
            <div class="auto-style2">
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Logout" />
                <strong style="text-align: left">
                <br />
                <br />
                <br />
                Existing Holiday requests&nbsp;&nbsp;&nbsp;
                <br />
                <br />
            </div>
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
            <br />
            </strong>
        </div>
        <div class="auto-style3">

            <strong>Submit a holiday request<br />
            <br />
            <asp:Label ID="Label1" runat="server" ForeColor="Red" style="font-size: medium"></asp:Label>
            <br />
            <br />
            </strong><span class="auto-style4">From Date:</span><strong>
            <asp:TextBox ID="TextBox1" runat="server" TextMode="Date"></asp:TextBox>
&nbsp;<br />
            <br />
            </strong><span class="auto-style4">To Date:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><strong>
            <asp:TextBox ID="TextBox2" runat="server" TextMode="Date"></asp:TextBox>
&nbsp;<br />
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Submit" />
            </strong>

        </div>
    </form>
</body>
</html>
