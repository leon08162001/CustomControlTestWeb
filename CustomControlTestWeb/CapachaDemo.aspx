<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CapachaDemo.aspx.cs" Inherits="test11" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
  <style type="text/css">
  #login-panel{  
    position: absolute;  
    top: 26px;  
    right: 0px;  
    width: 190px;  
    padding: 10px 15px 5px 15px;  
    background: #2a2a2a;  
    font-size: 8pt;  
    font-weight: bold;  
    color: #FFF;  
    display: none;  
}
  </style>
</head>
<body>
  <form id="form1" runat="server">
  <div>
    <wcc:Captcha ID="Captcha1" runat="server" />
    <wcc:ToolBar ID="ToolBar1" runat="server">
      <Items>
        <wcc:ToolBarButton CauseValidation="False" Enabled="True" 
          IsSeperator="False" Visible="True" />
      </Items>
      <CustomControls>
        <wcc:CalendarRange ID="CalendarRange1" runat="server" />
      </CustomControls>
    </wcc:ToolBar>
    <wcc:BarCode ID="BarCode1" runat="server" BarCodeHeight="30px" 
      BarCodeType="Ean13" IsShowPrice="False" />
    <wcc:CollapsiblePanel ID="CollapsiblePanel1" runat="server" PanelCSS="login-panel">
    </wcc:CollapsiblePanel>
  </div>
  </form>
</body>
</html>
