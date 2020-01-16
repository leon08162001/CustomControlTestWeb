<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default11.aspx.cs" Inherits="Default11" %>

<%--<%@ Register Assembly="JihSun" Namespace="JihSun" TagPrefix="cc1" %>
--%>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>未命名頁面</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
			<wcc:Email ID="Email1" runat="server" />
			<wcc:Captcha ID="Captcha1" runat="server" />
			<wcc:ListBoxToListBox ID="ListBoxToListBox1" runat="server" 
				ListForeColor="Orange">
				<SecondListItems>
					<asp:ListItem>D</asp:ListItem>
				</SecondListItems>
				<FirstListItems>
					<asp:ListItem>A</asp:ListItem>
					<asp:ListItem>B</asp:ListItem>
					<asp:ListItem>C</asp:ListItem>
				</FirstListItems>
			</wcc:ListBoxToListBox>
			</div>
    </form>
</body>