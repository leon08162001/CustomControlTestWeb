<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MenuDemo.aspx.cs" Inherits="Demo_MenuDemo" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
       <%-- <wcc:Menu ID="Menu1" runat="server" ConnectionKey="default" DataType="DataBase"
            ItemHoverBgColor="239, 239, 239" MainItemBgColor="DarkOrange" MainItemForeColor="DarkKhaki"
            MenuItemWidth="145" MenuLevel="20" MenuPosition="Horizontal" MenuTableBaseName="Menu"
            MenuType="Thick_Borders" MenuWidth="450" SubItemBgColor="Silver" SubItemForeColor="85, 85, 85"
            Target="self" />--%>
            
              <wcc:Menu ID="Menu2" runat="server" ConnectionKey="default" DataType="DataBase" MenuWidth="435"
            ItemHoverBgColor="239, 239, 239" MainItemBgColor="MediumTurquoise" MainItemForeColor="Blue"
             MenuLevel="20" MenuPosition="Horizontal" MenuTableBaseName="Menu"
            MenuType="Basic_Bounce_Arrows" SubItemBgColor="Silver" SubItemForeColor="85, 85, 85"
            Target="self" />
    </div>
    </form>
</body>
</html>
