<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TEST1.aspx.cs" Inherits="Demo_TEST1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" Text="Button" />
            <wcc:DropDownList_YearMonth ID="DropDownList_YearMonth1" runat="server" />
            <wcc:DropDownList_YearMonth ID="DropDownList_YearMonth2" runat="server" />
        </div>
    </form>
</body>
</html>
