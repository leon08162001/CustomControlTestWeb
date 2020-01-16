<%@ Page Language="c#" Inherits="EeekSoft.PopupTest.tst" CodeFile="simple.aspx.cs" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Popup Window Test - Simple</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <wcc:PopupWin ID="popupLoad" Title="Anchor Test" Style="z-index: 100; left: 16px;
        position: absolute; top: 544px" runat="server" Message="Click on link to reopen this window.."
        Width="200px" Height="85px" ColorStyle="Green" DockMode="BottomLeft" HideAfter="4000">
    </wcc:PopupWin>
    <wcc:PopupWinAnchor ID="PopupWinAnchor4" Style="z-index: 114; left: 568px; position: absolute;
        top: 152px" runat="server" LinkedControl="id2" PopupToShow="popupBtn" ChangeTexts="True"
        NewMessage="You clicked on second button !" NewText="Nothing here :)" NewTitle="Second button">
    </wcc:PopupWinAnchor>
    <wcc:PopupWin ID="popupBob" Title="Visit CodeProject.Com" Style="z-index: 112; left: 0px;
        position: absolute; top: 0px" runat="server" Message="<img src=&quot;bob.gif&quot; border=&quot;0&quot; align=&quot;right&quot;><p><b>THE CODE <span style=&quot;color:#00a000;&quot;>PROJECT</span></b><br><br>The Visual Studio Developer's Homepage.</p>"
        Width="216px" LinkTarget="_blank" Link="http://www.codeproject.com" ActionType="OpenLink"
        DockMode="BottomRight" LightShadow="255, 192, 128" Shadow="128, 64, 0" TextColor="0, 0, 0"
        DarkShadow="0, 0, 0" GradientLight="251, 238, 187" GradientDark="255, 153, 0"
        AutoShow="False" HideAfter="4000"></wcc:PopupWin>
    <input id="id2" style="z-index: 108; left: 136px; width: 80px; position: absolute;
        top: 104px; height: 24px" type="button" value="Second" name="Button1">
    <asp:HyperLink ID="HyperLink1" Style="z-index: 105; left: 16px; position: absolute;
        top: 208px" runat="server" NavigateUrl="default.aspx">Test page for basic popup features</asp:HyperLink>
    <asp:HyperLink ID="HyperLink3" Style="z-index: 104; left: 16px; position: absolute;
        top: 232px" runat="server" NavigateUrl="anchor.aspx">Test page for anchor control (advanced)</asp:HyperLink>
    <wcc:PopupWinAnchor ID="PopupWinAnchor1" Style="z-index: 101; left: 568px; position: absolute;
        top: 16px" runat="server" LinkedControl="reopenGreen" PopupToShow="popupLoad"
        HandledEvent="onmouseover"></wcc:PopupWinAnchor>
    <asp:HyperLink ID="reopenGreen" Style="z-index: 102; left: 16px; position: absolute;
        top: 16px" runat="server" Width="232px" Height="24px" NavigateUrl="#">Reopen green window (mouseover)</asp:HyperLink>
    <asp:HyperLink ID="reopenCp" Style="z-index: 103; left: 16px; position: absolute;
        top: 40px" runat="server" NavigateUrl="#">Open CP Banner (mouseover)</asp:HyperLink>
    <input id="id1" style="z-index: 106; left: 40px; width: 80px; position: absolute;
        top: 104px; height: 24px" type="button" value="First">
    <asp:Label ID="Label1" Style="z-index: 107; left: 16px; position: absolute; top: 72px"
        runat="server">PopupWinAnchor controls can be assigned to Html input</asp:Label>
    <wcc:PopupWin ID="popupBtn" Style="z-index: 109; left: 16px; position: absolute;
        top: 432px" runat="server" ColorStyle="Red" AutoShow="False" DockMode="BottomLeft"
        OffsetY="115" HideAfter="3000" ActionType="OpenLink" 
        DarkShadow="250, 235, 215" GradientDark="10, 36, 106" 
        GradientLight="0, 255, 255" LightShadow="245, 245, 220" Link="default.aspx" 
        LinkTarget="blank" OffsetX="150" Shadow="255, 228, 196" Text="" 
        TextColor="255, 255, 0"></wcc:PopupWin>
    <wcc:PopupWinAnchor ID="PopupWinAnchor2" Style="z-index: 111; left: 568px; position: absolute;
        top: 120px" runat="server" LinkedControl="id1" PopupToShow="popupBtn"
        NewMessage="You clicked on first button !" NewText="aaaaaaaaaaaaaaa" 
        NewTitle="bbbbbbbbbbbbbb">
    </wcc:PopupWinAnchor>
    <wcc:PopupWinAnchor ID="PopupWinAnchor3" Style="z-index: 113; left: 568px; position: absolute;
        top: 48px" runat="server" LinkedControl="reopenCp" PopupToShow="popupBob" HandledEvent="onmouseover">
    </wcc:PopupWinAnchor>
    </form>
</body>
</html>
