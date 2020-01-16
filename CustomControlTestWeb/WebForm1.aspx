<%@ Page language="c#" Inherits="WebForm1" CodeFile="WebForm1.aspx.cs" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
<%--		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">--%>
		<LINK href="css/StyleSheet1.css" type="text/css" rel="stylesheet">
		<%--<script language="JavaScript" src="core.js" type="text/javascript"></script>
		<script language="JavaScript" src="events.js" type="text/javascript"></script>
		<script language="JavaScript" src="css.js" type="text/javascript"></script>
		<script language="JavaScript" src="coordinates.js" type="text/javascript"></script>
		<script language="JavaScript" src="drag.js" type="text/javascript"></script>--%>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
    <wcc:CollapsiblePanel ID="CollapsiblePanel4" runat="server" TitleCSS="test" ExpandImage="~/images/ToggleDown.gif"
					CollapseImage="~/images/ToggleUp.gif" Width="100%" PanelCSS="panel" 
      Draggable="true">
      <wcc:PopupCalendar ID="PopupCalendar1" runat="server" ValidationType="NoCheck">
      </wcc:PopupCalendar>
      <br />
      <wcc:Email ID="Email1" runat="server" ValidationType=Label />
      <br />
      <asp:Panel ID="Panel1" runat="server">
		 <wcc:ToolBar ID="ToolBar1" runat="server" OnClick="ToolBar_Click"
			ButtonHeight="20px" ButtonWidth="20px">
			<Items>
					<wcc:ToolBarButton Enabled="True" Visible="True" 
					ImageUrl="~/images/icon_down.gif" ToolTip="mov03.wmv" 
					ClickArgument="mov03.wmv" ActiveImageUrl="images/mp3_play.jpg" />
				<wcc:ToolBarButton Enabled="True" Visible="True" 
					ImageUrl="~/images/icon_down.gif" ToolTip="mov04.wmv" 
					ClickArgument="mov04.wmv" />
				<wcc:ToolBarButton Enabled="True" Visible="True" 
					ImageUrl="~/images/icon_eis.gif" ToolTip="mov05.wmv" 
					ClickArgument="mov05.wmv" />
				<wcc:ToolBarButton Enabled="True" Visible="True" 
					ImageUrl="~/images/icon_exit.gif" ToolTip="mov06.wmv" 
					ClickArgument="mov06.wmv" />
				<wcc:ToolBarButton Enabled="True" Visible="True" 
					ImageUrl="~/images/topicon_01.gif" IsSeperator="true" ClickArgument="E" />
				<wcc:ToolBarButton Enabled="True" Visible="True" 
					ImageUrl="~/images/icon_help.gif" ToolTip="mov07.wmv" 
					ClickArgument="mov07.wmv" />
				<wcc:ToolBarButton Enabled="True" Visible="True" 
					ImageUrl="~/images/icon_home.gif" ToolTip="mov08.wmv" 
					ClickArgument="mov08.wmv" />
				<wcc:ToolBarButton Enabled="True" Visible="True" 
					ImageUrl="~/images/icon_other.gif" ToolTip="mov09.wmv" 
					ClickArgument="mov09.wmv" />
				<wcc:ToolBarButton Enabled="True" Visible="True" 
					ImageUrl="~/images/icon_save.gif" ToolTip="mov10.wmv" 
					ClickArgument="mov10.wmv" />
				<wcc:ToolBarButton Enabled="True" Visible="True" 
					ImageUrl="~/images/icon_up.gif" ToolTip="mov11.wmv" ClickArgument="mov11.wmv" />
				<wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/PDF.jpg" 
					ToolTip="mov12.wmv" ClickArgument="mov12.wmv" />
				<wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/PPT.jpg" 
					ToolTip="mov13.wmv" ClickArgument="mov13.wmv" />
				<wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/VSD.jpg" 
					ToolTip="11 All rise.mp3" ClickArgument="11 All rise.mp3" />
				<wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/XLS.jpg" 
					ToolTip="2012清晰完美中字版(www.an80.com).rmvb" ClickArgument="2012清晰完美中字版(www.an80.com).rmvb" />
				<wcc:ToolBarLabel Enabled="True" Text="ss" Visible="True" />
			</Items>
		</wcc:ToolBar>
		<br />
		 <wcc:MediaPlayer ID="MediaPlayer1" runat="server" Width=600 Height=480 
          AutoStart=true UIMode=Full Url="http://localhost:90/Media/mov03.wmv" 
          Params-Capacity="16">
    </wcc:MediaPlayer>
     </asp:Panel>
    </wcc:CollapsiblePanel>
			<wcc:CollapsiblePanel id="CollapsiblePanel2" runat="server" State="Expanded" 
        PanelCSS="panel"
				Width="250px" CollapseImage="~/images/ToggleUp.gif" ExpandImage="~/images/ToggleDown.gif" 
        TitleCSS="test" TitleForeColor="White"
				TitleBackColor="Gray" Text="Member Login" Draggable="true">
				<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0" 
          style="width: 250px">
					<TR>
						<TD>Login name</TD>
						<TD>
							<asp:TextBox id="TextBox1" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD>Password</TD>
						<TD>
							<asp:TextBox id="TextBox2" runat="server"></asp:TextBox></TD>
					</TR>
					<TR>
						<TD></TD>
						<TD align="right">
							<asp:Button id="Button2" runat="server" Text="Button"></asp:Button></TD>
					</TR>
				</TABLE>
				<wcc:CollapsiblePanel id="CollapsiblePanel3" runat="server" Text="Daily Events" TitleCSS="test" ExpandImage="~/images/ToggleDown.gif"
					CollapseImage="~/images/ToggleUp.gif" Width="100%" PanelCSS="panel" Draggable="false">
					<TABLE align="left">
						<TR>
							<TD valign="middle">
								<asp:Calendar id="Calendar1" runat="server" Width="200px" CellPadding="4" DayNameFormat="FirstLetter"
									BorderColor="#999999" Font-Names="Verdana" Font-Size="8pt" Height="180px" ForeColor="Black"
									BackColor="White">
									<TodayDayStyle ForeColor="Black" BackColor="#CCCCCC"></TodayDayStyle>
									<SelectorStyle BackColor="#CCCCCC"></SelectorStyle>
									<NextPrevStyle VerticalAlign="Bottom"></NextPrevStyle>
									<DayHeaderStyle Font-Size="7pt" Font-Bold="True" BackColor="#CCCCCC"></DayHeaderStyle>
									<SelectedDayStyle Font-Bold="True" ForeColor="White" BackColor="#666666"></SelectedDayStyle>
									<TitleStyle Font-Bold="True" BorderColor="Black" BackColor="#999999"></TitleStyle>
									<WeekendDayStyle BackColor="#FFFFCC"></WeekendDayStyle>
									<OtherMonthDayStyle ForeColor="#808080"></OtherMonthDayStyle>
								</asp:Calendar></TD>
						</TR>
					</TABLE>
				</wcc:CollapsiblePanel>
			</wcc:CollapsiblePanel>
    <wcc:TabsView ID="TabsView1" runat="server" 
      SelectedTabBackColor="239, 239, 239" TabBackColor="239, 239, 239" 
      TabButtonBorderColor="'#d4d0c8'" TabPageLeftBackImageHeight="0px" 
      TabPageLeftBackImageWidth="0px" TabPageRightBackImageHeight="0px" 
      TabPageRightBackImageWidth="0px" TabTextColor="Black" TabTextSize="Medium" 
      CurrentTabIndex="1">
      <Tabs>
        <wcc:TabPage ID="TabPage1" runat="server" PaddingBottom="4px" 
          PaddingLeft="4px" PaddingRight="4px" PaddingTop="4px" 
          style="padding-top:3px; padding-bottom:3px; padding-left:3px; padding-right:3px;" 
          Text="Tab Item" BorderColor="Red" BorderStyle="Solid" BorderWidth="1px">
        </wcc:TabPage>
        <wcc:TabPage ID="TabPage8" runat="server" PaddingBottom="2px" 
          PaddingLeft="2px" PaddingRight="5px" PaddingTop="5px" 
          style="padding-top:5px; padding-bottom:2px; padding-left:2px; padding-right:5px;" 
          Text="Tab Item" BorderColor="Red" BorderStyle="Solid" BorderWidth="1px">
        </wcc:TabPage>
        <wcc:TabPage ID="TabPage3" runat="server" PaddingBottom="10px" 
          PaddingLeft="10px" PaddingRight="10px" PaddingTop="10px" 
          style="padding-top:10px;padding-bottom:10px;padding-left:10px;padding-right:10px;" 
          Text="Tab Item">
        </wcc:TabPage>
        <wcc:TabPage ID="TabPage4" runat="server" PaddingBottom="10px" 
          PaddingLeft="10px" PaddingRight="10px" PaddingTop="10px" 
          style="padding-top:10px;padding-bottom:10px;padding-left:10px;padding-right:10px;" 
          Text="Tab Item">
        </wcc:TabPage>
        <wcc:TabPage ID="TabPage5" runat="server" PaddingBottom="10px" 
          PaddingLeft="10px" PaddingRight="10px" PaddingTop="10px" 
          style="padding-top:10px;padding-bottom:10px;padding-left:10px;padding-right:10px;" 
          Text="Tab Item">
        </wcc:TabPage>
        <wcc:TabPage ID="TabPage6" runat="server" PaddingBottom="10px" 
          PaddingLeft="10px" PaddingRight="10px" PaddingTop="10px" 
          style="padding-top:10px;padding-bottom:10px;padding-left:10px;padding-right:10px;" 
          Text="Tab Item">
        </wcc:TabPage>
        <wcc:TabPage ID="TabPage7" runat="server" PaddingBottom="10px" 
          PaddingLeft="10px" PaddingRight="10px" PaddingTop="10px" 
          style="padding-top:10px;padding-bottom:10px;padding-left:10px;padding-right:10px;" 
          Text="Tab Item">
        </wcc:TabPage>
      </Tabs>
    </wcc:TabsView>
			</form>
	</body>
</HTML>
