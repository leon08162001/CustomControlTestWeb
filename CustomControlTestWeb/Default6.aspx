<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default6.aspx.cs" Inherits="Default6" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>未命名頁面</title>
    
</head>
<body>
	<form id="form1" runat="server">
	<div style="vertical-align:text-top">
		<wcc:ListBoxToListBox ID="ListBoxToListBox1" runat="server" 
      ListBackColor="AliceBlue" ListHeight="150px" ListWidth="80px" 
      ListForeColor="YellowGreen" />
		<wcc:ListBoxToListBox ID="ListBoxToListBox2" runat="server" />

		<asp:Button ID="Button1" runat="server" Text="Button" />
	</div>
	</form>
</body>
</html>
