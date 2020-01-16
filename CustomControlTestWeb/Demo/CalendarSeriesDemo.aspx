<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CalendarSeriesDemo.aspx.cs" Inherits="Demo_CalendarSeriesDemo" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wcc:PopupCalendar ID="PopupCalendar1" runat="server" TitleAlign="right" FirstDate="9999-12-01" />
        <wcc:CalendarRange ID="CalendarRange1" runat="server" TextAlign="right" />
    </div>
    </form>
</body>
</html>
