<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test8.aspx.cs" Inherits="test8" Async="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">  
    <title></title>
</head>
<script language="javascript" type="text/javascript">

</script>
<body style=" margin:5px">
    <form id="form1" runat="server">
    <div>
    <%--<asp:Panel ID="Panel2" runat="server" Height="20" Width="400px" BorderWidth="1" HorizontalAlign="Center">
        <asp:Panel ID="Panel1" runat="server" Height="20px" Width="0px" BackColor="Blue">
            <asp:Label ID="Label1" runat="server"></asp:Label></asp:Panel>
    </asp:Panel>--%>
    <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
    <%--<asp:Panel ID="Panel2" runat="server" style="display:none">--%>
        <asp:Image ID="Image1" runat="server" Width="214px" Height="15px" Visible="false" BorderWidth="0" ImageUrl="~/images/pleasewait.gif" />
    <%--</asp:Panel>--%>
        <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
        <input id="hidden_IsProgress" type="hidden" value="false" runat="server" /> 
    </asp:Panel>

    </div>
    </form>
</body>
</html>
