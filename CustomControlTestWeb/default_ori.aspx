<%@ Page language="c#" Inherits="EeekSoft.PopupTest.Default" validaterequest="false" CodeFile="default_ori.aspx.cs" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
  <head>
    <title>EeekSoft Popup Window Test - Basic</title>
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content=C# name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema>
  </head>
<body style="margin:0px">
<form id=Form1 method=post runat="server"><asp:button id=btnPopup 
    style="Z-INDEX: 100; LEFT: 123px; POSITION: absolute; TOP: 280px" 
    runat="server" text="Popup" onclick="btnPopup_Click"></asp:button><asp:label id=Label1 style="Z-INDEX: 101; LEFT: 16px; POSITION: absolute; TOP: 16px" runat="server">Text to show:</asp:label><asp:textbox id=textMsg style="Z-INDEX: 102; LEFT: 120px; POSITION: absolute; TOP: 16px" runat="server" columns="50" width="336px">This text will be displayed in popup window.</asp:textbox><asp:label id=Label2 style="Z-INDEX: 103; LEFT: 16px; POSITION: absolute; TOP: 168px" runat="server">Text to show in info window:</asp:label><asp:textbox id=textFull style="Z-INDEX: 104; LEFT: 120px; POSITION: absolute; TOP: 192px" runat="server" columns="39" width="336px" rows="4" textmode="MultiLine" height="80px">This very long text will be displayed in new window. To open this window click on &quot;Show Popup&quot; button and then click on popup window.</asp:textbox>
<wcc:popupwin id=popupWin 
    style="Z-INDEX: 105; LEFT: 296px; POSITION: absolute; TOP: 528px" 
    runat="server" width="230px" height="100px" windowsize="300, 200" 
    windowscroll="False" dockmode="BottomLeft" colorstyle="Custom" 
    gradientdark="210, 200, 220" textcolor="0, 0, 3" shadow="125, 90, 160" 
    lightshadow="185, 170, 200" darkshadow="128, 0, 102" visible="False" 
    showlink="True" offsetx="150">
</wcc:popupwin><asp:textbox id=textTitle style="Z-INDEX: 106; LEFT: 120px; POSITION: absolute; TOP: 48px" runat="server" columns="50" Width="336px">Title here</asp:textbox><asp:label id=Label3 style="Z-INDEX: 107; LEFT: 16px; POSITION: absolute; TOP: 48px" runat="server" Width="32px">Title:</asp:label><asp:dropdownlist id=clrStyle style="Z-INDEX: 108; LEFT: 120px; POSITION: absolute; TOP: 80px" runat="server" Width="336px">
<asp:ListItem Value="Red">Red</asp:ListItem>
<asp:ListItem Value="Green">Green</asp:ListItem>
<asp:ListItem Value="Blue" Selected="True">Blue</asp:ListItem>
<asp:ListItem Value="Violet">Violet</asp:ListItem>
</asp:dropdownlist><asp:label id=Label4 style="Z-INDEX: 109; LEFT: 16px; POSITION: absolute; TOP: 80px" runat="server">Color style:</asp:label><asp:button id=btn4Ever style="Z-INDEX: 110; LEFT: 184px; POSITION: absolute; TOP: 280px" runat="server" Text="No hide" onclick="btn4Ever_Click"></asp:button><asp:dropdownlist id=popDocking style="Z-INDEX: 111; LEFT: 120px; POSITION: absolute; TOP: 112px" runat="server" Width="336px">
<asp:ListItem Value="Bottom - Left">Bottom - Left</asp:ListItem>
<asp:ListItem Value="Bottom - Right" Selected="True">Bottom - Right</asp:ListItem>
</asp:dropdownlist><asp:label id=Label5 style="Z-INDEX: 112; LEFT: 16px; POSITION: absolute; TOP: 112px" runat="server">Docking:</asp:label>

<wcc:popupwin id=popupWin2 title="Second window" style="Z-INDEX: 113; LEFT: 584px; POSITION: absolute; TOP: 528px" runat="server" text="Text to display in new window. <br><b>This text is from second window...</b>" width="230px" dockmode="BottomRight" gradientdark="225, 225, 208" textcolor="0, 50, 0" shadow="160, 180, 140" lightshadow="208, 221, 195" darkshadow="50, 100, 50" visible="False" actiontype="RaiseEvents" gradientlight="255, 255, 255" message="You can have more than one popup window ! <br/><br/><b>This has custom color scheme</b>" hideafter="8000" offsety="130" onpopupclosed="popupWin2_PopupClose" onlinkclicked="popupWin2_LinkClick"></wcc:popupwin>
<asp:button id=btnTwo style="Z-INDEX: 114; LEFT: 256px; POSITION: absolute; TOP: 280px" runat="server" Text="Two popups" onclick="btnTwo_Click"></asp:button><asp:label id=lblMsg style="Z-INDEX: 115; LEFT: 120px; POSITION: absolute; TOP: 320px" runat="server" Width="336px" Visible="False" ForeColor="Red">Label</asp:label><wcc:popupwin id=popupBob title="Visit CodeProject.Com" style="Z-INDEX: 116; LEFT: 16px; POSITION: absolute; TOP: 528px" runat="server" Width="216px" Visible="False" linktarget="_blank" Link="http://www.codeproject.com" ActionType="OpenLink" DockMode="BottomLeft" Message="<img src=&quot;bob.gif&quot; border=&quot;0&quot; align=&quot;right&quot;><p><b>THE CODE <span style=&quot;color:#00a000;&quot;>PROJECT</span></b><br><br>The Visual Studio Developer's Homepage.</p>" LightShadow="255, 192, 128" Shadow="128, 64, 0" TextColor="0, 0, 0" DarkShadow="0, 0, 0" GradientLight="251, 238, 187" GradientDark="255, 153, 0"></wcc:popupwin><asp:button id=btnBob style="Z-INDEX: 117; LEFT: 368px; POSITION: absolute; TOP: 280px" runat="server" Text="CP Banner" onclick="btnBob_Click"></asp:button><asp:HyperLink id="HyperLink1" style="Z-INDEX: 119; LEFT: 16px; POSITION: absolute; TOP: 376px" runat="server" NavigateUrl="anchor.aspx">Test page for anchor control (advanced)</asp:HyperLink><asp:HyperLink id="HyperLink2" style="Z-INDEX: 120; LEFT: 16px; POSITION: absolute; TOP: 352px" runat="server" NavigateUrl="simple.aspx">Simple anchor control tests</asp:HyperLink>
<asp:label id="Label6" runat="server" style="Z-INDEX: 121; LEFT: 16px; POSITION: absolute; TOP: 144px">Drag&Drop:</asp:label><asp:DropDownList id="dropDrag" style="Z-INDEX: 122; LEFT: 120px; POSITION: absolute; TOP: 144px" runat="server" Width="336px">
<asp:ListItem Value="Enable Drag&amp;drop" Selected="True">Enable Drag&amp;drop</asp:ListItem>
<asp:ListItem Value="Disable Drag&amp;drop">Disable Drag&amp;drop</asp:ListItem>
</asp:DropDownList>
</form>
<div style="HEIGHT:2000px"></div>
</body>
</html>
