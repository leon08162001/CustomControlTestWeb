<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Flash_Demo.aspx.cs" Inherits="Demo_Flash_Demo" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click"  />
        <br />
         <wcc:Flash ID="Flash1" runat="server" Height="390px" Width="640px" Quality="Best">
      <Params>
        <wcc:ActiveXParam Name="allowFullScreen" Value="true" />
        <wcc:ActiveXParam Name="allowScriptAccess" Value="always" />
        <wcc:ActiveXParam Name="quality" Value="best" />
      </Params>
    </wcc:Flash>
    </form>
</body>
</html>
