<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TabViewDemo.aspx.cs" Inherits="TabViewDemo" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <br />
                <wcc:TabsView ID="TabsView1" runat="server" SelectedTabBackColor="DarkRed" TabBackColor="Yellow"
                    TabButtonBorderColor="DarkOrange" TabPageLeftBackImageHeight="22px" TabPageLeftBackImageWidth="3px"
                    TabPageRightBackImageHeight="22px" TabPageRightBackImageWidth="3px" TabTextColor="Black"
                    TabTextSize="Medium" TabPageCenterBackImageUrl="~/images/tai_button_02.gif" TabPageRightBackImageUrl="~/images/tai_button_03.gif"
                    TabTextBold="True" BorderColor="#FF3300" BorderWidth="2px" Width="1300px" Height="500px" TabPageLeftBackImageUrl="~/images/tai_button_01.gif"
                    SelectedTabPageCenterBackImageUrl="~/images/PageTab_center_1.gif"
                    SelectedTabPageLeftBackImageUrl="~/images/beige_rounded_bl.gif" SelectedTabPageRightBackImageUrl="~/images/beige_rounded_br.gif"
                    IsUseTabBackImage="True" OnTabSelectionChanging="TabsView1_TabSelectionChanging">
                    <Tabs>
                        <wcc:TabPage runat="server" Text="Tab Item1" PaddingTop="10px" PaddingBottom="10px"
                            PaddingLeft="10px" PaddingRight="10px" ID="TabPage1" Style="padding-top: 10px;
                            padding-bottom: 10px; padding-left: 10px; padding-right: 10px;" 
                            ToolTip="A" BorderColor="Red" BorderStyle="Solid" BorderWidth="1px">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <wcc:Captcha ID="Captcha1" runat="server" CaptchaLength="6" CaptchaBackgroundNoise="Extreme"
                                        CaptchaFont="Trebuchet MS, 27.75pt" CaptchaFontWarping="High" Font-Names="標楷體"
                                        CaptchaLineNoise="Extreme" CaptchaWidth="120" CaptchaMaxTimeout="60" CaptchaMinTimeout="2"
                                        CaptchaChars="0123456789" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" EnableViewState="true">
                                <ContentTemplate>
                                    <wcc:Captcha ID="Captcha2" runat="server" CaptchaLength="6" CaptchaBackgroundNoise="Extreme"
                                        CaptchaFont="Trebuchet MS, 27.75pt" CaptchaFontWarping="High" Font-Names="標楷體"
                                        CaptchaLineNoise="Extreme" CaptchaWidth="120" CaptchaMaxTimeout="60" CaptchaMinTimeout="2"
                                        CaptchaChars="0123456789" />
                                    <wcc:CalendarRange ID="CalendarRange1" runat="server" />
                                    <br />
                                    <wcc:CalendarRange ID="CalendarRange3" runat="server" />
                                    <wcc:Identity ID="Identity1" runat="server" />
                                    <br />
                                    <asp:Button ID="Button3" runat="server" Text="Send" OnClick="Button3_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </wcc:TabPage>
                        <wcc:TabPage ID="TabPage2" runat="server" PaddingBottom="10px" PaddingLeft="10px"
                            PaddingRight="10px" PaddingTop="10px" Style="padding-top: 10px; padding-bottom: 10px;
                            padding-left: 10px; padding-right: 10px;" Text="Tab Item2" ToolTip="B"
                             BorderColor="Red" BorderStyle="Solid" BorderWidth="1px">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" EnableViewState="true">
                                <ContentTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                    <asp:Button ID="Button2" runat="server" CausesValidation="False" Text="Button" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </wcc:TabPage>
                        <wcc:TabPage ID="TabPage3" runat="server" PaddingBottom="10px" PaddingLeft="10px"
                            PaddingRight="10px" PaddingTop="10px" Style="padding-top: 10px; padding-bottom: 10px;
                            padding-left: 10px; padding-right: 10px;" Text="Tab Item3" ToolTip="C"
                            BorderColor="white" BorderStyle="None">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" EnableViewState="true">
                                <ContentTemplate>
                                    <wcc:ToolBar ID="ToolBar2" runat="server" OnClick="ToolBar2_Click" ButtonHeight="20px"
                                        ButtonWidth="20px" OnButton_ConfirmYesNoClick="ToolBar2_Button_ConfirmYesNoClick">
                                        <Items>
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/icon_Dhome.gif"
                                                ToolTip="11 All rise.mp3" ClickArgument="11 All rise.mp3" OnClientClick="window.alert('How Are You?');"
                                                PostBackUrl="Default3.aspx" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico" ToolTip="閻亦格-我不是你想像那麼勇敢"
                                                ClickArgument="我不是你想像那麼勇敢.mp3" OverImageUrl="images/XLS.jpg" ActiveImageUrl="images/VSD.jpg"
                                                OnClientClick="window.alert('AA');" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico" ToolTip="閻亦格-李香蘭.mp3"
                                                ClickArgument="李香蘭.mp3" ActiveImageUrl="~/images/drop2.gif" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico" ToolTip="閻亦格-Because you love me"
                                                ClickArgument="Because you love me.avi" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/separator.gif"
                                                IsSeperator="true" ClickArgument="E" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico" ToolTip="閻亦格-fighter"
                                                ClickArgument="fighter.mp3" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico" ToolTip="閻亦格-也許明天"
                                                ClickArgument="也許明天.avi" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico" ToolTip="閻亦格-天黑黑"
                                                ClickArgument="天黑黑.avi" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico" ToolTip="閻亦格-我們都寂寞"
                                                ClickArgument="我們都寂寞.avi" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico" ToolTip="閻亦格-李香蘭"
                                                ClickArgument="李香蘭.mp3" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico" ToolTip="閻亦格-愛如潮水"
                                                ClickArgument="愛如潮水.avi" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico" ToolTip="閻亦格-愛很簡單"
                                                ClickArgument="愛很簡單.avi" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico" ToolTip="閻亦格-讓一切隨風"
                                                ClickArgument="讓一切隨風.avi" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/XLS.jpg" ToolTip="2012清晰完美中字版(www.an80.com).rmvb"
                                                ClickArgument="2012清晰完美中字版(www.an80.com).rmvb" />
                                            <wcc:ToolBarLabel Enabled="True" Text="ss" Visible="True" />
                                        </Items>
                                        <CustomControls>
                                            <wcc:CalendarRange ID="CalendarRange2" runat="server" FirstDate="2008-11-24" IsShowTitle="True"
                                                NeedValidation="True" NeedValue="True" SecondDate="2008-11-24" TextBoxID="" TextBoxID1=""
                                                TextBoxWidth="" TextLength="5000" Title="請輸入日期區間 : " TitleBackColor="Aqua" TitleForeColor="Sienna"
                                                TitleWidth="" ValidationType="NoCheck" />
                                            <wcc:Button_ConfirmYesNo ID="BB" runat="server" ButtonID="BB" CancelConfirm="False"
                                                ClientScript="" CommandArgument="" CommandName="" PostBackUrl="" CausesValidation="false"
                                                Text="BBB" Width="" />
                                            <wcc:Button_ConfirmYesNo ID="CC" runat="server" ButtonID="CC" CancelConfirm="False"
                                                ClientScript="" CommandArgument="" CommandName="" PostBackUrl="" CausesValidation="false"
                                                Text="CCC" Width="" />
                                        </CustomControls>
                                    </wcc:ToolBar>
                                    <br />
                                    <wcc:MediaPlayer ID="MediaPlayer1" runat="server" Width="600" Height="480" AutoStart="True"
                                        UIMode="Full" Url="http://localhost/Media/mov03.wmv">
                                        <Params>
                                            <wcc:ActiveXParam Name="URL" Value="http://localhost/Media/mov03.wmv"></wcc:ActiveXParam>
                                            <wcc:ActiveXParam Name="autoStart" Value="False"></wcc:ActiveXParam>
                                            <wcc:ActiveXParam Name="uiMode" Value="Full"></wcc:ActiveXParam>
                                        </Params>
                                    </wcc:MediaPlayer>
                                    <wcc:ToolBar ID="ToolBar3" runat="server" OnClick="ToolBar3_Click" ButtonHeight="20px"
                                        ButtonWidth="20px" OnButton_ConfirmYesNoClick="ToolBar2_Button_ConfirmYesNoClick">
                                        <Items>
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/icon_Dhome.gif"
                                                ToolTip="11 All rise.mp3" ClickArgument="11 All rise.mp3" OnClientClick="window.alert('How Are You?');"
                                                PostBackUrl="Default3.aspx" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/mp3.jpg" ToolTip="閻亦格-我不是你想像那麼勇敢"
                                                ClickArgument="我不是你想像那麼勇敢.mp3" ActiveImageUrl="~/images/mp3_play.jpg" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/mp3.jpg" ToolTip="閻亦格-李香蘭.mp3"
                                                ClickArgument="李香蘭.mp3" ActiveImageUrl="~/images/mp3_play.jpg" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/mp3.jpg" ToolTip="閻亦格-fighter.mp3"
                                                ClickArgument="fighter.mp3" ActiveImageUrl="~/images/mp3_play.jpg" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/separator.gif"
                                                IsSeperator="true" ClickArgument="E" ActiveImageUrl="~/images/gif_search_01.gif" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/mp3.jpg" ToolTip="11 All rise.mp3"
                                                ClickArgument="11 All rise.mp3" ActiveImageUrl="~/images/mp3_play.jpg" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/mp3.jpg" ToolTip="12 街角的祝福.mp3"
                                                ClickArgument="12 街角的祝福.mp3" ActiveImageUrl="~/images/mp3_play.jpg" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/mp3.jpg" ToolTip="13 EVERYTHING.mp3"
                                                ClickArgument="13 EVERYTHING.mp3" ActiveImageUrl="~/images/mp3_play.jpg" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/mp3.jpg" ToolTip="14 Hey Jude.mp3"
                                                ClickArgument="14 Hey Jude.mp3" ActiveImageUrl="~/images/mp3_play.jpg" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/mp3.jpg" ToolTip="17 If tomorrow never comes.mp3"
                                                ClickArgument="17 If tomorrow never comes.mp3" ActiveImageUrl="~/images/mp3_play.jpg" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/mp3.jpg" ToolTip="18 我是女人.mp3"
                                                ClickArgument="18 我是女人.mp3" ActiveImageUrl="~/images/mp3_play.jpg" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/mp3.jpg" ToolTip="16 hitfm-016.mp3"
                                                ClickArgument="16 hitfm-016.mp3" ActiveImageUrl="~/images/mp3_play.jpg" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/mp3.jpg" ToolTip="閻亦格-讓一切隨風"
                                                ClickArgument="讓一切隨風.avi" ActiveImageUrl="~/images/mp3_play.jpg" />
                                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/mp3.jpg" ToolTip="2012清晰完美中字版(www.an80.com).rmvb"
                                                ClickArgument="2012清晰完美中字版(www.an80.com).rmvb" ActiveImageUrl="~/images/mp3_play.jpg" />
                                            <wcc:ToolBarLabel Enabled="True" Text="ss" Visible="True" />
                                        </Items>
                                        <CustomControls>
                                            <wcc:CalendarRange ID="CalendarRange12" runat="server" FirstDate="2008-11-24" IsShowTitle="True"
                                                NeedValidation="True" NeedValue="True" SecondDate="2008-11-24" TextBoxID="" TextBoxID1=""
                                                TextBoxWidth="" TextLength="5000" Title="請輸入日期區間 : " TitleBackColor="Aqua" TitleForeColor="Sienna"
                                                TitleWidth="" ValidationType="NoCheck" />
                                            <wcc:Button_ConfirmYesNo ID="DD" runat="server" ButtonID="DD" CancelConfirm="False"
                                                ClientScript="" CommandArgument="" CommandName="" PostBackUrl="" CausesValidation="false"
                                                Text="FFF" Width="" />
                                            <wcc:Button_ConfirmYesNo ID="EE" runat="server" ButtonID="EE" CancelConfirm="False"
                                                ClientScript="" CommandArgument="" CommandName="" PostBackUrl="" CausesValidation="false"
                                                Text="EEE" Width="" />
                                        </CustomControls>
                                    </wcc:ToolBar>
                                    <br />
                                    <wcc:MediaPlayer ID="MediaPlayer2" runat="server" Width="600" Height="480" AutoStart="true"
                                        UIMode="Full" Url="http://localhost/Media/mov03.wmv">
                                        <Params>
                                            <wcc:ActiveXParam Name="URL" Value="http://localhost/Media/mov03.wmv"></wcc:ActiveXParam>
                                            <wcc:ActiveXParam Name="autoStart" Value="True"></wcc:ActiveXParam>
                                            <wcc:ActiveXParam Name="uiMode" Value="Full"></wcc:ActiveXParam>
                                        </Params>
                                    </wcc:MediaPlayer>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </wcc:TabPage>
                    </Tabs>
                </wcc:TabsView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="TabsView1" EventName="TabSelectionChanging" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>
