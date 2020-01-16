<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestDemo.aspx.cs" Inherits="Demo_TestDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>未命名頁面</title>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wcc:Identity ID="Identity1" runat="server" TextLength="11" 
            ValidationType="Label" />
        <br />
        <wcc:PopupCalendar ID="PopupCalendar1" runat="server" />
    </div>
    </form>
</body>
</html>
