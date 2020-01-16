<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default14.aspx.cs" Inherits="Default14" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
      <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
      <asp:Image ID="Image1" runat="server" />
      <wcc:PopupCalendar ID="PopupCalendar1" runat="server" CalendarStyle=Modern />
      <wcc:CalendarRange ID="CalendarRange1" runat="server" />
    </div>
    <asp:Button ID="Button1" runat="server" Text="Button" />
    </form>
</body>
</html>
