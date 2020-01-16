<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BarcodeDemo.aspx.cs" Inherits="Demo_BarcodeDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
    <wcc:BarCode ID="BarCode1" runat="server" BarCodeText="471687250518" BarCodeType="Ean13"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Ean13" />
</body>
</html>
