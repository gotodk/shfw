<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testguid.aspx.cs" Inherits="testguid" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>重要测试文件，不要删除</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="生成一个新的有序guid" OnClick="Button1_Click" />
        <hr />
    


        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" Text="从这个guid获取精确时间" OnClick="Button2_Click" />
        <hr />



    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" Text="加密测试" OnClick="Button3_Click"  />
        <hr />



          <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button4" runat="server" Text="分解权值测试" OnClick="Button4_Click"  />
        <hr />
    </div>
    </form>
</body>
</html>
