<%@ Page Language="C#" AutoEventWireup="true" CodeFile="dbinfo.aspx.cs" Inherits="dbinfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>数据库信息查看</title>
    		<script src="js/jquery-2.1.1.min.js"></script>
 
</head>
<body>
    <form id="form1" runat="server">
    <div>
 
    
        <asp:GridView ID="GridView1" runat="server" Width="2000px" AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" Font-Size="Small" ForeColor="Black" GridLines="Vertical" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="表名" HeaderText="表名" />
                <asp:BoundField DataField="列名" HeaderText="列名" />
                <asp:BoundField DataField="是否主键" HeaderText="是否主键" />
                <asp:BoundField DataField="是否自增长主键" HeaderText="是否自增长主键" />
                <asp:BoundField DataField="值类型" HeaderText="值类型" />
                <asp:BoundField DataField="值长度限制" HeaderText="值长度限制" />
                <asp:BoundField DataField="数字列总长度上限" HeaderText="数字列总长度上限" />
                <asp:BoundField DataField="数字列小数位数上限" HeaderText="数字列小数位数上限" />
                <asp:BoundField DataField="默认值" HeaderText="默认值" />
                <asp:BoundField DataField="列描述" HeaderText="列描述" />
                <asp:BoundField DataField="创建时间" HeaderText="创建时间" />
                <asp:BoundField DataField="最后修改时间" HeaderText="最后修改时间" />
                <asp:BoundField DataField="索引排序" HeaderText="索引排序" />
                <asp:BoundField DataField="索引名" HeaderText="索引名" />
                <asp:BoundField DataField="列顺序" HeaderText="列顺序" />
                <asp:BoundField DataField="是否计算列" HeaderText="是否计算列" />
                <asp:BoundField DataField="表描述" HeaderText="表描述" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
        <asp:Button ID="Button3" runat="server" Text="导出上表" OnClick="Button3_Click" />
        <br />
        <br />
        <br />
        <asp:GridView ID="GridView2" runat="server" CellPadding="4" GridLines="Vertical" Width="700" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" AutoGenerateColumns="False" Font-Size="Small" ForeColor="Black" >
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="名称" HeaderText="名称" />
                <asp:BoundField DataField="类型" HeaderText="类型" />
                <asp:BoundField DataField="类型描述" HeaderText="类型描述" />
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
    
        <br />
        <asp:Button ID="Button4" runat="server" Text="导出上表" OnClick="Button4_Click" />
    
    </div>
    </form>
</body>
</html>
 