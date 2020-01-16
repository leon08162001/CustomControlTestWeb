<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test6.aspx.cs" Inherits="test6" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wcc:NewDropDownList ID="NewDropDownList1" runat="server" TitleBackColor="Yellow" EnableViewState="true">
        <Items>
            <asp:ListItem Text="A" Value="A"></asp:ListItem>
            <asp:ListItem Text="B" Value="B"></asp:ListItem>
        </Items>
        </wcc:NewDropDownList>
        <wcc:Identity ID="Identity1" runat="server" EnableViewState="false" 
            ValidationType="Alert" />
        <asp:Button ID="Button1" runat="server" Text="Button" />
    </div>
    </form>
</body>
</html>
