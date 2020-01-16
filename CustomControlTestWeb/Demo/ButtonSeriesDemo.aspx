<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ButtonSeriesDemo.aspx.cs" Inherits="Demo_ButtonSeriesDemo" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wcc:Button_ConfirmYesNo ID="Button_ConfirmYesNo1" runat="server" />
        <wcc:Button_Normal ID="Button_Normal1" runat="server" />
        <wcc:Button_PopupWindow ID="Button_PopupWindow1" runat="server" />
    </div>
    </form>
</body>
</html>
