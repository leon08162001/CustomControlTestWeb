<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default15.aspx.cs" Inherits="Default15" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
	<%--<wcc:Menu ID="Menu1" runat="server" ConnectionKey="default" 
  DataType="DataBase" ItemHoverBgColor="239, 239, 239" 
  MainItemBgColor="DarkOrange" MainItemForeColor="DarkKhaki" 
  MenuItemWidth="145" MenuLevel="20" MenuPosition="Horizontal" 
  MenuTableBaseName="Menu" MenuType="Thick_Borders" MenuWidth="450" 
  SubItemBgColor="Silver" SubItemForeColor="85, 85, 85" Target="self" />--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
	<wcc:Menu ID="Menu2" runat="server" ConnectionKey="Default" 
  DataType="DataBase" ItemHoverBgColor="239, 239, 239" 
  MainItemBgColor="255, 255, 255" MainItemForeColor="51, 51, 51" 
  MenuItemWidth="145" MenuLevel="20" MenuPosition="Horizontal" 
  MenuTableBaseName="Menu" MenuType="Blue_Tones" MenuWidth="450" 
  SubItemBgColor="239, 239, 239" SubItemForeColor="85, 85, 85" Target="self" />
	<wcc:PopupCalendar ID="PopupCalendar1" runat="server" />
	<br />
</asp:Content>

