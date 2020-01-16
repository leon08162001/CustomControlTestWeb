<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ToolBar_MediaPlayerDemo.aspx.cs"
    Inherits="Demo_ToolBar_MediaPlayerDemo" %>
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
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <wcc:ToolBar ID="ToolBar1" runat="server" OnClick="ToolBar_Click" ButtonHeight="20px"
                    ButtonWidth="20px" OnButton_ConfirmYesNoClick="ToolBar1_Button_ConfirmYesNoClick">
                    <Items>
                        <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/icon_Dhome.gif"
                            ToolTip="11 All rise.mp3" ClickArgument="11 All rise.mp3" OnClientClick="window.alert('How Are You?');"
                            PostBackUrl="Default3.aspx" />
                        <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico" ToolTip="閻亦格-我不是你想像那麼勇敢"
                            ClickArgument="我不是你想像那麼勇敢.mp3" />
                        <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico" ToolTip="閻亦格-失戀無罪"
                            ClickArgument="失戀無罪.mp3" />
                        <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico" ToolTip="閻亦格-Because you love me"
                            ClickArgument="Because you love me.avi" />
                        <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/separator.gif"
                            IsSeperator="true" ClickArgument="E" />
                        <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/wmv.ico" ToolTip="閻亦格-superwoman"
                            ClickArgument="superwoman.avi" />
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
                        <%--<wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/XLS.jpg" ToolTip="2012清晰完美中字版(www.an80.com).rmvb"
                            ClickArgument="2012清晰完美中字版(www.an80.com).rmvb" />--%>
                            <wcc:ToolBarButton Enabled="True" Visible="True" ImageUrl="~/images/XLS.jpg" ToolTip="2012清晰完美中字版(www.an80.com).rmvb"
                            ClickArgument="v=-uzttSaDgqg" />
                        <wcc:ToolBarLabel Enabled="True" Text="ss" Visible="True" />
                    </Items>
                    <CustomControls>
                        <wcc:CalendarRange ID="CalendarRange2" runat="server" FirstText="2008-11-24" IsShowTitle="True"
                            NeedValidation="True" NeedValue="True" SecondText="2008-11-24" TextBoxID="" TextBoxID1=""
                            TextBoxWidth="" TextLength="5000" Title="請輸入日期區間 : " TitleBackColor="Aqua" TitleForeColor="Sienna"
                            TitleWidth="" ValidationType="NoCheck" />
                        <wcc:Button_ConfirmYesNo ID="Button_ConfirmYesNo2" runat="server" ButtonID="AA"
                            CancelConfirm="False" ClientScript="" CommandArgument="" CommandName="" PostBackUrl=""
                            Text="AAA" Width="" />
                    </CustomControls>
                </wcc:ToolBar>
                <br />
                <wcc:MediaPlayer ID="MediaPlayer1" runat="server" Width="600" Height="480" AutoStart="true"
                    UIMode="Full" Url="http://localhost/Media/mov03.wmv">
                </wcc:MediaPlayer>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
