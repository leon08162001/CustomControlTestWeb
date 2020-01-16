<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NumberSeriesDemo.aspx.cs" Inherits="Demo_NumberDemo" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <%--<wcc:Number ID="Number1" runat="server" ValidationType="Alert" 
           NeedValue="False" TitleAlign="left" Text="0" TextAlign="right" 
            IsNeedComma="True" TextLength="8" />
        <wcc:Number_Decimal ID="Number_Decimal1" runat="server" DecimalLength="二位" TextAlign="right" 
            IsNeedComma="True" TextLength="10" />--%>
        <br />
       <%-- <wcc:NumberRange ID="NumberRange1" runat="server" IsNeedComma="True" 
            TextAlign="right" ValidationType="Label" />--%>
        <%--<wcc:Number_Decimal ID="Number_Decimal1" IsNeedComma="true" runat="server" 
            TextAlign="justify" DecimalLength="四位" />--%>
        <wcc:DecimalRange ID="DecimalRange1" runat="server" DecimalLength="四位" TextLength="11" IsNeedComma="true" TextAlign="right" />
    
    </div>
    <asp:Button ID="Button1" runat="server" Text="Button" />
    </form>
</body>
</html>
