<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Email_Identity_TextBoxSeriesDemo.aspx.cs" Inherits="Demo_EmailDemo" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wcc:Email ID="Email1" runat="server"  TextBoxWidth="200px" 
             HasBorder="True" />
        <wcc:Identity ID="Identity1" runat="server" />
        <wcc:TextBox_Normal ID="TextBox_Normal1" runat="server" />
        <wcc:TextBox_PopupWindow ID="TextBox_PopupWindow1" runat="server" />
    </div>
    <asp:Button ID="Button1" runat="server" Text="Button" />
    </form>
</body>
</html>
