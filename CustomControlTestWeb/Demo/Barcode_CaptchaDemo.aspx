<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Barcode_CaptchaDemo.aspx.cs"
    Inherits="test" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">

        function pageLoad() {
        }
    
    </script>

</head>
<body onunload="unload();">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <wcc:Captcha ID="Captcha1" runat="server" 
                CaptchaFont="標楷體, 24pt, style=Bold, Underline" 
                CaptchaBackgroundNoise="Extreme" CaptchaChars="ACDEFGHJKLNPQRTUVXYZ" 
                CaptchaFontWarping="Extreme" CaptchaLineNoise="Extreme" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <wcc:Captcha ID="Captcha2" runat="server" 
                CaptchaFont="標楷體, 24pt, style=Bold, Italic, Underline" 
                CaptchaBackgroundNoise="None" CaptchaChars="12346789" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <wcc:BarCode ID="BarCode14" runat="server" BarCodeText="A40156B" BarCodeType="Codabar"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Codabar" />
        <br />
    <wcc:BarCode ID="BarCode8" runat="server" BarCodeText="123456789" BarCodeType="Code11"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Code11" />
    <br />
     <wcc:BarCode ID="BarCode4" runat="server" BarCodeText="978986181821" BarCodeType="Code128"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Code128" />
    <br />
    <wcc:BarCode ID="BarCode5" runat="server" BarCodeText="978986133567" BarCodeType="Code128_A"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Code128_A" />
    <br />
    <wcc:BarCode ID="BarCode6" runat="server" BarCodeText="978985671236" BarCodeType="Code128_B"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Code128_B" />
    <br />
    <wcc:BarCode ID="BarCode7" runat="server" BarCodeText="687236181857" BarCodeType="Code128_C"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Code128_C" />
    <br />
    <wcc:BarCode ID="BarCode13" runat="server" BarCodeText="1234567" BarCodeType="Code39"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Code39" />
    <br />
    <wcc:BarCode ID="BarCode9" runat="server" BarCodeText="ABC123456789" BarCodeType="Code93"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Code93" />
    <br />
    <wcc:BarCode ID="BarCode2" runat="server" BarCodeText="4716413842347" BarCodeType="Ean13"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Ean13" />
    <br />
    <wcc:BarCode ID="BarCode15" runat="server" BarCodeText="01234565" BarCodeType="Ean8"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Ean8" />
    <br />
    <%--<wcc:BarCode ID="BarCode16" runat="server" BarCodeText="01234565" BarCodeType="FIM"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="FIM" />
    <br />--%>
    <wcc:BarCode ID="BarCode16" runat="server" BarCodeText="01234565" BarCodeType="Interleaved2of5"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Interleaved2of5" />
    <br />
    <wcc:BarCode ID="BarCode3" runat="server" BarCodeText="978123456789" BarCodeType="ISBN"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="ISBN" />
    <br />
    <wcc:BarCode ID="BarCode17" runat="server" BarCodeText="10012345678902" BarCodeType="ITF14"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="ITF14" />
    <br />
    <wcc:BarCode ID="BarCode18" runat="server" BarCodeText="4901234567894" BarCodeType="JAN13"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="JAN13" />
    <br />
    <wcc:BarCode ID="BarCode10" runat="server" BarCodeText="123456789" BarCodeType="MSI"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="MSI" />
    <br />
    <%--<wcc:BarCode ID="BarCode11" runat="server" BarCodeText="131070" BarCodeType="Pharmacode"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Pharmacode" />
    <br />--%>
    <wcc:BarCode ID="BarCode12" runat="server" BarCodeText="10010" BarCodeType="Postnet"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Postnet" />
    <br />
    <wcc:BarCode ID="BarCode19" runat="server" BarCodeText="12345670" BarCodeType="Standard2of5"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Standard2of5" />
    <br />
    <wcc:BarCode ID="BarCode20" runat="server" BarCodeText="ABC12345" BarCodeType="Telepen"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="Telepen" />
    <br />
    <wcc:BarCode ID="BarCode1" runat="server" BarCodeText="449837487716" BarCodeType="UPC_A"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="UPC_A" />
    <br />
    <wcc:BarCode ID="BarCode21" runat="server" BarCodeText="120211" BarCodeType="UPC_E"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="UPC_E" />
    <br />
    <wcc:BarCode ID="BarCode22" runat="server" BarCodeText="36" BarCodeType="UPCSupplement2"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="UPCSupplement2" />
    <br />
    <wcc:BarCode ID="BarCode23" runat="server" BarCodeText="67378" BarCodeType="UPCSupplement5"
        BarCodeWeight="Small" BarCodeHeight="50px" Scale="_15" Title="UPCSupplement5" />
    </form>
</body>
</html>
