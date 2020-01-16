<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListBoxToListBoxDemo.aspx.cs"
    Inherits="Demo_ListBoxToListBoxDemo" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <wcc:ListBoxToListBox ID="ListBoxToListBox1" runat="server" 
            ListForeColor="Orange" ListHeight="200px" ListWidth="120px">
            <FirstListItems>
                <asp:ListItem>A</asp:ListItem>
                <asp:ListItem>B</asp:ListItem>
                <asp:ListItem>C</asp:ListItem>
            </FirstListItems>
            <SecondListItems>
                <asp:ListItem>D</asp:ListItem>
            </SecondListItems>
        </wcc:ListBoxToListBox>
        <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
    </div>
    </form>
</body>
</html>
