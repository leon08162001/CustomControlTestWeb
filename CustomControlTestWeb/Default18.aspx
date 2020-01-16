<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default18.aspx.cs" Inherits="Default18" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/jscript" src="js/WebScript.js"></script>
</head>
<body onunload="unload();">
    <form id="form1" runat="server">
    <div>
      <asp:Panel ID="Panel1" runat="server">
      <wcc:PopupCalendar ID="PopupCalendar1" runat="server" TextBoxID="CalText" CalendarStyle="Modern" />
      <asp:Button ID="Button1" runat="server" Text="Button" />
      </asp:Panel>
    </div>
    </form>
</body>
</html>
