<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QRCodeDemo.aspx.cs" Inherits="Demo_QRCodeDemo" %>

<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="30">
        <%--<wcc:QRCode ID="QRCode1" runat="server" 
            QRCodeText="XC388710961021016000100000000000000140000000016095713EDMzW9ev0g/ts4AyPvXxpA==" 
            Version="Ver5_108" ErrorCorrection="M"></wcc:QRCode>--%>
            <%--<wcc:QRCode ID="QRCode1" runat="server" 
            QRCodeText="These are films that give us pieces of time that we can never forget. They have the power to entertain, enchant, inform, and move us emotionally - and change our perceptions of things. The films below range from the earliest defining silent films of Hollywood, to all the genre types (screwball comedies, westerns, etc.), and to the blockbusters and epics of today. These 'Greatest Films' refuse to fade from memory even after the long passage of time - they share the unifying fact of being seen and talked about decades after they were made. Many of these Greatest Films were made many years ago, and overlooked when they were first released, yet they have endured the test of time. These films were chosen with very specific Selection Criteria - further explained in another section of this site. These crucial film selections have undoubtedly left an indelible mark upon our lives and reflect many defining moments of the last 100 years." 
            Version="Ver28" ErrorCorrection="L" ImageSize="SS" ImageHeight="220px" ImageWidth="220px"></wcc:QRCode>--%>
             <wcc:QRCode ID="QRCode1" runat="server" QRCodeText="" 
             Version="Ver35" ErrorCorrection="Q" ImageSize="XXL" ImageHeight="300px" ImageWidth="300px"></wcc:QRCode>
    </div>   
    </form>
</body>
</html>
