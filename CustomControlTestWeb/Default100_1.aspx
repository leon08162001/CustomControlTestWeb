<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default100_1.aspx.cs" Inherits="Default100" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title></title>
</head>
<body>
  <form id="form1" runat="server">
 <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
  </asp:ScriptManager>--%>
 <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
      <div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
        <wcc:TabsView ID="TabsView1" runat="server" SelectedTabBackColor="DarkRed" TabBackColor="Yellow"
          TabButtonBorderColor="DarkOrange" TabPageLeftBackImageHeight="22px" TabPageLeftBackImageWidth="3px"
          TabPageRightBackImageHeight="22px" TabPageRightBackImageWidth="3px" TabTextColor="Black"
          TabTextSize="Medium" TabPageCenterBackImageUrl="~/images/tai_button_02.gif" TabPageRightBackImageUrl="~/images/tai_button_03.gif"
          TabTextBold="True" BorderColor="#FF3300" BorderWidth="2px" Height="768px" TabPageLeftBackImageUrl="~/images/tai_button_01.gif"
          Width="1024px" SelectedTabPageCenterBackImageUrl="~/images/PageTab_center_1.gif"
          SelectedTabPageLeftBackImageUrl="~/images/beige_rounded_bl.gif" SelectedTabPageRightBackImageUrl="~/images/beige_rounded_br.gif"
          IsUseTabBackImage="True" 
          OnTabSelectionChanging="TabsView1_TabSelectionChanging">
          <Tabs>
            <%--<wcc:TabPage runat="server" Text="Tab Item1" PaddingTop="10px" PaddingBottom="10px"
              PaddingLeft="10px" PaddingRight="10px" ID="TabPage1" Style="padding-top: 10px;
              padding-bottom: 10px; padding-left: 10px; padding-right: 10px;" Height="591px"
              Width="979px" ToolTip="C" BorderColor="Red" BorderStyle="Solid" BorderWidth="1px">
              <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                  <wcc:Captcha ID="Captcha1" runat="server" CaptchaLength="1" CaptchaBackgroundNoise="Extreme"
                    CaptchaFont="Trebuchet MS, 27.75pt" CaptchaFontWarping="High" Font-Names="標楷體"
                    CaptchaLineNoise="Extreme" CaptchaWidth="120" CaptchaMaxTimeout="10" CaptchaMinTimeout="2"
                    CaptchaChars="0123456789" />
                </ContentTemplate>
              </asp:UpdatePanel>
              <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" EnableViewState="true">
                <ContentTemplate>
                  <wcc:Captcha ID="Captcha2" runat="server" CaptchaLength="1" CaptchaBackgroundNoise="Extreme"
                    CaptchaFont="Trebuchet MS, 27.75pt" CaptchaFontWarping="High" Font-Names="標楷體"
                    CaptchaLineNoise="Extreme" CaptchaWidth="120" CaptchaMaxTimeout="10" CaptchaMinTimeout="2"
                    CaptchaChars="0123456789" />
                  <wcc:CalendarRange ID="CalendarRange1" runat="server" />
                  <wcc:CalendarRange ID="CalendarRange3" runat="server" />
                  <wcc:Identity ID="Identity1" runat="server" />
                  <br />
                  <asp:Button ID="Button3" runat="server" Text="Send" OnClick="Button3_Click" />
                </ContentTemplate>
              </asp:UpdatePanel>
            </wcc:TabPage>--%>
            <wcc:TabPage ID="TabPage2" runat="server" Height="439px" PaddingBottom="10px"
              PaddingLeft="10px" PaddingRight="10px" PaddingTop="10px" Style="padding-top: 10px;
              padding-bottom: 10px; padding-left: 10px; padding-right: 10px;" Text="Tab Item2"
              Width="727px" ToolTip="B" BorderColor="Red" BorderStyle="Solid" BorderWidth="1px">
             <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" EnableViewState="true">
                <ContentTemplate>--%>
                  <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                  <asp:Button ID="Button2" runat="server" CausesValidation="False" Text="Button" />
                <%--</ContentTemplate>
              </asp:UpdatePanel>--%>
            </wcc:TabPage>
           <%-- <wcc:TabPage ID="TabPage3" runat="server" Height="591px" PaddingBottom="10px"
              PaddingLeft="10px" PaddingRight="10px" PaddingTop="10px" Style="padding-top: 10px;
              padding-bottom: 10px; padding-left: 10px; padding-right: 10px;" Text="Tab Item3"
              Width="979px" ToolTip="C" BorderColor="Red" BorderStyle="Solid" BorderWidth="1px">
              <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" EnableViewState="true">
                <ContentTemplate>
                  <wcc:ToolBar ID="ToolBar2" runat="server" OnClick="ToolBar_Click" ButtonHeight="20px"
                    ButtonWidth="20px" 
                    onbutton_confirmyesnoclick="ToolBar2_Button_ConfirmYesNoClick">
                    <Items>
                      <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/icon_Dhome.gif"
                        ToolTip="11 All rise.mp3" ClickArgument="11 All rise.mp3" OnClientClick="window.alert('How Are You?');"
                        PostBackUrl="Default3.aspx" />
                      <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
                        ToolTip="閻亦格-我不是你想像那麼勇敢" ClickArgument="我不是你想像那麼勇敢.mp3" />
                      <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
                        ToolTip="閻亦格-李香蘭.mp3" ClickArgument="李香蘭.mp3" />
                      <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
                        ToolTip="閻亦格-Because you love me" ClickArgument="Because you love me.avi" />
                      <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="images/separator.gif"
                        IsSeperator="true" ClickArgument="E" />
                      <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
                        ToolTip="閻亦格-fighter" ClickArgument="fighter.mp3" />
                      <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
                        ToolTip="閻亦格-也許明天" ClickArgument="也許明天.avi" />
                      <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
                        ToolTip="閻亦格-天黑黑" ClickArgument="天黑黑.avi" />
                      <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
                        ToolTip="閻亦格-我們都寂寞" ClickArgument="我們都寂寞.avi" />
                      <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
                        ToolTip="閻亦格-李香蘭" ClickArgument="李香蘭.mp3" />
                      <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
                        ToolTip="閻亦格-愛如潮水" ClickArgument="愛如潮水.avi" />
                      <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
                        ToolTip="閻亦格-愛很簡單" ClickArgument="愛很簡單.avi" />
                      <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
                        ToolTip="閻亦格-讓一切隨風" ClickArgument="讓一切隨風.avi" />
                      <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/XLS.jpg"
                        ToolTip="2012清晰完美中字版(www.an80.com).rmvb" ClickArgument="2012清晰完美中字版(www.an80.com).rmvb" />
                      <wcc:ToolBarLabel Enabled="True" Text="ss" Visible="True" />
                    </Items>
                    <CustomControls>
                      <wcc:CalendarRange ID="CalendarRange2" runat="server" FirstDate="2008-11-24"
                        IsShowTitle="True" NeedValidation="True" NeedValue="True" SecondDate="2008-11-24"
                        TextBoxID="" TextBoxID1="" TextBoxWidth="" TextLength="5000" Title="請輸入日期區間 : "
                        TitleBackColor="Aqua" TitleForeColor="Sienna" TitleWidth="" ValidationType="NoCheck" />
                      <wcc:Button_ConfirmYesNo ID="BB" runat="server" ButtonID="BB"
                        CancelConfirm="False" ClientScript="" CommandArgument="" CommandName="" PostBackUrl=""
                        CausesValidation="false" Text="BBB" Width="" />
                        <wcc:Button_ConfirmYesNo ID="CC" runat="server" ButtonID="CC"
                        CancelConfirm="False" ClientScript="" CommandArgument="" CommandName="" PostBackUrl=""
                        CausesValidation="false" Text="CCC" Width="" />
                    </CustomControls>
                  </wcc:ToolBar>
                  <br />
                    <wcc:MediaPlayer ID="MediaPlayer1" runat="server" Width="600" Height="480" AutoStart="False"
                    UIMode="Full" Url="http://localhost/Media/mov03.wmv">
                  <params>
                      <wcc:ActiveXParam Name="URL" Value="http://localhost/Media/mov03.wmv"></wcc:ActiveXParam>
                      <wcc:ActiveXParam Name="autoStart" Value="False"></wcc:ActiveXParam>
                      <wcc:ActiveXParam Name="uiMode" Value="Full"></wcc:ActiveXParam>
                    </params>
                  </wcc:MediaPlayer>
                </ContentTemplate>
              </asp:UpdatePanel>
            </wcc:TabPage>--%>
          </Tabs>
        </wcc:TabsView>
      </div>
      <wcc:ToolBar ID="ToolBar1" runat="server" OnClick="ToolBar_Click" ButtonHeight="20px"
        ButtonWidth="20px" onbutton_confirmyesnoclick="ToolBar2_Button_ConfirmYesNoClick">
        <Items>
          <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/icon_Dhome.gif"
            ToolTip="11 All rise.mp3" ClickArgument="11 All rise.mp3" OnClientClick="window.alert('How Are You?');"
            PostBackUrl="Default3.aspx" />
          <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
            ToolTip="閻亦格-我不是你想像那麼勇敢" ClickArgument="我不是你想像那麼勇敢.mp3" />
          <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
            ToolTip="閻亦格-失戀無罪" ClickArgument="失戀無罪.mp3" />
          <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
            ToolTip="閻亦格-Because you love me" ClickArgument="Because you love me.avi" />
          <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="images/separator.gif"
            IsSeperator="true" ClickArgument="E" />
          <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
            ToolTip="閻亦格-superwoman" ClickArgument="superwoman.avi" />
          <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
            ToolTip="閻亦格-也許明天" ClickArgument="也許明天.avi" />
          <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
            ToolTip="閻亦格-天黑黑" ClickArgument="天黑黑.avi" />
          <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
            ToolTip="閻亦格-我們都寂寞" ClickArgument="我們都寂寞.avi" />
          <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
            ToolTip="閻亦格-李香蘭" ClickArgument="李香蘭.mp3" />
          <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
            ToolTip="閻亦格-愛如潮水" ClickArgument="愛如潮水.avi" />
          <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
            ToolTip="閻亦格-愛很簡單" ClickArgument="愛很簡單.avi" />
          <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico"
            ToolTip="閻亦格-讓一切隨風" ClickArgument="讓一切隨風.avi" />
          <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/XLS.jpg"
            ToolTip="2012清晰完美中字版(www.an80.com).rmvb" ClickArgument="2012清晰完美中字版(www.an80.com).rmvb" />
          <wcc:ToolBarLabel Enabled="True" Text="ss" Visible="True" />
        </Items>
        <%--<CustomControls>
			  <wcc:Button_ConfirmYesNo ID="Button_ConfirmYesNo1" runat="server" CancelConfirm="False" CommandArgument="新增"
          PostBackUrl="default3.aspx" Message="確定新增嗎?" Text="新增" />
			  <wcc:Button_Normal ID="Button_Normal2" runat="server" ButtonID="" 
          ClientScript="" CommandArgument="更新" CommandName="" PostBackUrl="" Text="更新" 
          Width="" />
			<wcc:DropDownList_Date ID="DropDownList_Date11" runat=server FirstTitle="出生年/月/日:" />
			  <wcc:Email ID="Email2" runat="server" IsShowTitle="True" NeedValidation="True" 
          NeedValue="True" TextBoxID="" TextBoxWidth="" TextLength="5000" Title="Email: " 
          TitleBackColor="255, 255, 128" TitleForeColor="192, 192, 0" TitleWidth=""
          ValidationType="Label" />
				<wcc:TextBox_PopupWindow ID="TextBox_PopupWindow2" runat="server" Carry="保留" 
					DecimalLength="無" Features="status=yes,scrollbars=yes" IsShowTitle="True" 
					NeedValidation="True" NeedValue="True" TextBoxID="" TextBoxWidth="" 
					TextLength="5000" Title="請輸入標題..." TitleBackColor="Aqua" 
					TitleForeColor="Sienna" TitleWidth="" ValidationFormat="無" 
					ValidationType="Label" WindowHeight="700" WindowType="Normal" 
					WindowWidth="1024" />
				<wcc:Identity ID="Identity2" runat="server" IsShowTitle="True" 
					NeedValidation="True" NeedValue="True" TextBoxID="" TextBoxWidth="" 
					TextLength="5000" Title="身分證ID : " TitleBackColor="Aqua" 
					TitleForeColor="Sienna" TitleWidth="" ValidationType="Label" />
				<wcc:Number ID="Number2" runat="server" IsShowTitle="True" 
					NeedValidation="True" NeedValue="True" Text="0" TextBoxID="" TextBoxWidth="" 
					TextLength="5000" Title="請輸入數字 : " TitleBackColor="Aqua" 
					TitleForeColor="Sienna" TitleWidth="" ValidationType="Label" />
				<wcc:Number_Decimal ID="Number_Decimal2" runat="server" Carry="保留" 
					DecimalLength="無" IsShowTitle="True" NeedValidation="True" NeedValue="True" 
					Text="0" TextBoxID="" TextBoxWidth="" TextLength="5000" Title="請輸入數字 : " 
					TitleBackColor="Aqua" TitleForeColor="Sienna" TitleWidth="" 
					ValidationType="Label" />
				<wcc:NumberRange ID="NumberRange2" runat="server" FirstText="0" 
					IsShowTitle="True" NeedValidation="True" NeedValue="True" Operator="大於等於" 
					SecondText="0" TextBoxID="" TextBoxID1="" TextBoxWidth="" TextLength="5000" 
					Title="請輸入數字區間 : " TitleBackColor="Aqua" TitleForeColor="Sienna" TitleWidth="" 
					ValidationType="Label" />
				<wcc:PopupCalendar ID="PopupCalendar2" runat="server" IsShowTitle="True" 
					NeedValidation="True" NeedValue="True" Text="2008-11-18" TextBoxID="" 
					TextBoxWidth="" TextLength="5000" Title="請輸入日期 : " TitleBackColor="Aqua" 
					TitleForeColor="Sienna" TitleWidth="" ValidationType="Label" />
				<wcc:Button_PopupWindow ID="Button_PopupWindow1" runat="server" ButtonID="" 
					ClientScript="" CommandArgument="" CommandName="" 
					Features="status=yes,scrollbars=yes" PostBackUrl="" Text="..." Width="" />
				<wcc:DecimalRange ID="DecimalRange2" runat="server" Carry="保留" 
					DecimalLength="無" FirstText="0" IsShowTitle="True" NeedValidation="True" 
					NeedValue="True" Operator="大於等於" SecondText="0" TextBoxID="" TextBoxID1="" 
					TextBoxWidth="" TextLength="5000" Title="請輸入數字區間 : " TitleBackColor="Aqua" 
					TitleForeColor="Sienna" TitleWidth="" ValidationType="Label" />
		</CustomControls>--%><CustomControls>
      <wcc:CalendarRange ID="CalendarRange2" runat="server" FirstText="2008-11-24"
        IsShowTitle="True" NeedValidation="True" NeedValue="True" SecondText="2008-11-24"
        TextBoxID="" TextBoxID1="" TextBoxWidth="" TextLength="5000" Title="請輸入日期區間 : "
        TitleBackColor="Aqua" TitleForeColor="Sienna" TitleWidth="" ValidationType="NoCheck" />
      <%--<wcc:TextBox_PopupWindow ID="TextBox_PopupWindow2" runat="server" Carry="保留" 
						DecimalLength="無" Features="status=yes,scrollbars=yes" IsShowTitle="True" 
						NeedValidation="True" NeedValue="True" TextBoxID="" TextBoxWidth="" 
						TextLength="5000" Title="請輸入標題..." TitleAlign="left" TitleBackColor="Aqua" 
						TitleForeColor="Sienna" TitleWidth="" ValidationFormat="無" 
						ValidationType="Label" WindowHeight="700" WindowType="Normal" 
						WindowWidth="1024" />--%><wcc:Button_ConfirmYesNo ID="Button_ConfirmYesNo2" runat="server"
              ButtonID="AA" CancelConfirm="False" ClientScript="" CommandArgument="" CommandName=""
              PostBackUrl="" Text="AAA" Width="" />
    </CustomControls>
      </wcc:ToolBar>
      <br />
      <wcc:MediaPlayer ID="MediaPlayer2" runat="server" Width="600" Height="480" AutoStart="True"
        UIMode="Full" Url="http://localhost/Media/mov03.wmv">
        <Params>
          <wcc:ActiveXParam Name="URL" Value="http://localhost/Media/mov03.wmv"></wcc:ActiveXParam>
          <wcc:ActiveXParam Name="autoStart" Value="True"></wcc:ActiveXParam>
          <wcc:ActiveXParam Name="uiMode" Value="Full"></wcc:ActiveXParam>
        </Params>
      </wcc:MediaPlayer>
<%--    </ContentTemplate>
  </asp:UpdatePanel>--%>
  </form>
</body>
</html>
