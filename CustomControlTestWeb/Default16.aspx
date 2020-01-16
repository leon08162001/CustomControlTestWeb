<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default16.aspx.cs" Inherits="Default16" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
</head>
<body>
  <form id="form1" runat="server">
  <div>
    <wcc:Menu ID="Menu1" runat="server" ConnectionKey="default" DataType="DataBase"
      ItemHoverBgColor="239, 239, 239" MainItemBgColor="255, 255, 255" MainItemForeColor="51, 51, 51"
      MenuItemWidth="145" MenuLevel="50" MenuPosition=Vertical MenuTableBaseName="Menu"
      MenuType=Tree_Menu MenuWidth="400" SubItemBgColor="239, 239, 239" SubItemForeColor="85, 85, 85"
      Target="self" CheckBoxBoundField="isCheck" HasCheckBox="true"  
			Font-Size="Small" Font-Bold="True" />
    <br />
    <wcc:PopupCalendar ID="PopupCalendar1" runat="server"
      CalendarStyle=Modern IsShowTitle=True NeedValue="True"
			TitleAlign="center" TitleBackColor="Blue" TitleForeColor="Snow"
			TitleWidth="120px" Text="2008/11/12" />
    <asp:Button ID="Button1" runat="server" Text="Button" />
  </div>
  </form>
</body>
</html>
