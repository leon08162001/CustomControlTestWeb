<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
  CodeFile="Default21.aspx.cs" Inherits="Default21" Title="未命名頁面" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <table border="1" cellpadding="1" cellspacing="1">
    <tr>
      <td>
        <asp:Label ID="Label1" runat="server" Text="列印BarCode標籤次數"></asp:Label>
      </td>
      <td>
        <asp:DropDownList ID="DDLLabelTimes" runat="server">
          <asp:ListItem Text="1" Selected="True"></asp:ListItem>
          <asp:ListItem Text="5"></asp:ListItem>
          <asp:ListItem Text="10"></asp:ListItem>
          <asp:ListItem Text="50"></asp:ListItem>
          <asp:ListItem Text="100"></asp:ListItem>
          <asp:ListItem Text="500"></asp:ListItem>
          <asp:ListItem Text="1000"></asp:ListItem>
        </asp:DropDownList>
      </td>
    </tr>
    <tr>
      <td>
        <asp:Label ID="Label2" runat="server" Text="列印欄數"></asp:Label>
      </td>
      <td>
        <asp:DropDownList ID="DDLColTimes" runat="server">
          <asp:ListItem Text="1" Selected="True"></asp:ListItem>
          <asp:ListItem Text="2"></asp:ListItem>
          <asp:ListItem Text="3"></asp:ListItem>
          <asp:ListItem Text="4"></asp:ListItem>
          <asp:ListItem Text="5"></asp:ListItem>
          <asp:ListItem Text="6"></asp:ListItem>
          <asp:ListItem Text="7"></asp:ListItem>
          <asp:ListItem Text="8"></asp:ListItem>
          <asp:ListItem Text="9"></asp:ListItem>
          <asp:ListItem Text="10"></asp:ListItem>
        </asp:DropDownList>
      </td>
    </tr>
    <tr>
      <td>
        <asp:Label ID="Label3" runat="server" Text="篩選BarCode"></asp:Label>
      </td>
      <td>
        <%--<asp:TextBox ID="TxtBarCode" runat="server" MaxLength="13"></asp:TextBox>--%>
        <wcc:ListBoxToListBox ID="ListBoxToListBox1" runat="server">
        </wcc:ListBoxToListBox>
      </td>
    </tr>
    <tr>
      <td colspan="2" style="text-align: center">
        <asp:Button ID="Btn_Confirm" runat="server" Text="確定" OnClick="Btn_Confirm_Click" />
      </td>
    </tr>
    <tr>
      <td colspan="2">
        <wcc:PageMaker ID="PageMaker1" runat="server" PagedControlID="DataList1" PageSize="10" Align="right" />
        <asp:DataList ID="DataList1" runat="server" RepeatColumns="1">
          <ItemTemplate>
            <wcc:BarCode ID="BarCode1" runat="server" BarCodeHeight="30" BarCodeText='<%# Eval("BarCode") %>'
              BarCodeType="Ean13" BarCodeWeight="Small" BorderWidth="2px" Scale="_10" TextAlign="Center"
              TextFont="Times New Roman, 9pt" Title='<%# Eval("Name") %>' TitleAlign="Center"
              TitleFont="新細明體, 8pt" Price='<%# Eval("Price") %>' />
          </ItemTemplate>
        </asp:DataList>
      </td>
    </tr>
  </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
