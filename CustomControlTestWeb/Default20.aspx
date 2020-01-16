<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default20.aspx.cs" Inherits="Default20" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:ListView ID="ListView1" runat="server">
      <LayoutTemplate>
      <table runat="server" id="tbl1" runat="server" width="95%">
      <tr>
      <td style="width:45%; font-weight: bold">個人基本資料</td>
      <td style="width:45%; font-weight: bold">個人進階資料</td>
      </tr>
      <tr runat="server" id="itemPlaceholder" ></tr>
      </table>
      </asp:Table>
      </LayoutTemplate>
      <ItemTemplate>
        <asp:TableRow runat="server" ID="TableRow1">
         <asp:TableCell runat="server" ID="TableCell1">
           <asp:Label ID="Label1" runat="server" Text='<%# Eval("CIFNAME") %>'></asp:Label>
         </asp:TableCell>
         <asp:TableCell runat="server" ID="TableCell3">
           <asp:Label ID="Label3" runat="server" Text='<%# Eval("CIFADR") %>'></asp:Label>
         </asp:TableCell>
      </asp:TableRow>
      <asp:TableRow runat="server" ID="TableRow2">
         <asp:TableCell runat="server" ID="TableCell2">
           <asp:Label ID="Label2" runat="server" Text='<%# Eval("cdb_brnonm_cname") %>'></asp:Label>
         </asp:TableCell>
         <asp:TableCell runat="server" ID="TableCell4">
           <asp:Label ID="Label4" runat="server" Text='<%# Convert.ToInt32(Eval("db_mutmr_isday")) %>'></asp:Label>
         </asp:TableCell>
      </asp:TableRow>
      </ItemTemplate>
      <ItemSeparatorTemplate>
      <tr><td colspan="2"><hr /></td></tr>
      </ItemSeparatorTemplate>
      </asp:ListView>
    </div>
    </form>
</body>
</html>
