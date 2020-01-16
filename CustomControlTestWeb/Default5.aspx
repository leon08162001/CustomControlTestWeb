<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default5.aspx.cs" Inherits="Default5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>未命名頁面</title>
</head>
<body>
  <form id="form1" runat="server">
    <asp:Table ID="Table1" runat="server" BorderWidth="0" CellPadding="0" CellSpacing="0">
      <asp:TableRow ID="TableRow1" runat="server">
        <asp:TableCell ID="TableCell1" runat="server" Width="10" Height="20">
        </asp:TableCell>
        <asp:TableCell ID="TableCell2" runat="server">
          <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="White" Font-Underline="false">A4444</asp:LinkButton>
        </asp:TableCell>
        <asp:TableCell ID="TableCell3" runat="server" Width="11" Height="20">
        </asp:TableCell>
        <asp:TableCell ID="TableCell4" runat="server" Width="10" Height="20">
        </asp:TableCell>
        <asp:TableCell ID="TableCell5" runat="server">
          <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="White" Font-Underline="false">B</asp:LinkButton>
        </asp:TableCell>
        <asp:TableCell ID="TableCell6" runat="server" Width="11" Height="20">
        </asp:TableCell>
        <asp:TableCell ID="TableCell7" runat="server" Width="10" Height="20">
        </asp:TableCell>
        <asp:TableCell ID="TableCell8" runat="server">
          <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="White" Font-Underline="false">C</asp:LinkButton>
        </asp:TableCell>
        <asp:TableCell ID="TableCell9" runat="server" Width="11" Height="20">
        </asp:TableCell>
      </asp:TableRow>
    </asp:Table>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
      <asp:View ID="A" runat="server">
        <asp:Panel ID="Panel1" runat="server" Width="1182px" Height="768px" BorderWidth="2" BorderStyle="Outset" ScrollBars="Auto">
        </asp:Panel>
      </asp:View>
      <asp:View ID="B" runat="server">
        <asp:Panel ID="Panel2" runat="server" Width="1182px" Height="768px" BorderWidth="2" BorderStyle="Outset" ScrollBars="Auto">
        </asp:Panel>
      </asp:View>
      <asp:View ID="C" runat="server">
        <asp:Panel ID="Panel3" runat="server" Width="1182px" Height="768px" BorderWidth="2" BorderStyle="Outset" ScrollBars="Auto">
        </asp:Panel>
      </asp:View>
    </asp:MultiView>
  </form>
</body>
</html>
