<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default7.aspx.cs" Inherits="Default7" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>未命名頁面</title>
</head>
<body>
  <form id="form1" runat="server">
  <div>
  <wcc:ToolBar ID="ToolBar1" runat="server" OnClick="ToolBar_Click">
			<Items>
				<wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="images/icon_Dhome.gif" ToolTip="11 All rise.mp3" ClickArgument="11 All rise.mp3" PostBackUrl=Default10.aspx />
				<wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="images/icon_down.gif" ToolTip="12 街角的祝福.mp3" ClickArgument="12 街角的祝福.mp3" />
				<wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="images/icon_eis.gif" ToolTip="13 EVERYTHING.mp3" ClickArgument="13 EVERYTHING.mp3" />
				<wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="images/icon_exit.gif" ToolTip="14 Hey Jude.mp3" ClickArgument="14 Hey Jude.mp3" />
				<wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="images/topicon_01.gif" IsSeperator="true" ClickArgument="E" />
				<wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="images/icon_help.gif" ToolTip="15 戀上(另)一個人.mp3" ClickArgument="15 戀上(另)一個人.mp3" />
				<wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="images/icon_home.gif" ToolTip="16 hitfm-016.mp3" ClickArgument="16 hitfm-016.mp3" />
				<wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="images/icon_other.gif" ToolTip="17 If tomorrow never comes.mp3" ClickArgument="17 If tomorrow never comes.mp3" />
				<wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="images/icon_save.gif" ToolTip="18 我是女人.mp3" ClickArgument="18 我是女人.mp3" />
			</Items>
		</wcc:ToolBar>
		<br />
		<wcc:ActiveX ID="TBActiveX1" runat="server" 
			ClassId="6BF52A52-394A-11D3-B153-00C04F79FAA6" Height="337px" Width="608px">
        </wcc:ActiveX>
  </div>
  </form>
</body>
</html>
