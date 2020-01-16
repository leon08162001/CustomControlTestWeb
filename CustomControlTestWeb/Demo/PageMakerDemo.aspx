<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PageMakerDemo.aspx.cs"
    Inherits="FCSWeb.PageMakerDemo" Async="false" %>

<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="x-ua-compatible" content="IE=9" >
    <link type="text/css" rel="Stylesheet" href="../css/Style.css" />
    <link type="text/css" rel="Stylesheet" href="../css/GridView.css" />
    <script language="javascript" type="text/javascript">
        function doProgressWork() {
            var qryField = document.getElementById("<%= ddlQryField.ClientID %>").value;
            var qryValue = document.getElementById("<%= txtQryValue.ClientID %>").value;
            var sortField = document.getElementById("<%= ddlSortField.ClientID %>").value;
            var params = new Array();
            params[0]=qryField;
            params[1]=qryValue;
            params[2] = sortField;
            var obj = document.getElementById("<%= btnProgress.ClientID %>");
            ProgressBar1_doWork(obj, params);
            //ProgressBar1_doWork(obj);
        }
function btnProgress_onclick() {

}

    </script>
    <style type="text/css">
        .style1
        {
            height: 29px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" ScriptMode="Debug" runat="server" />
        <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table class="pageFontStyle">
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label1" runat="server" Text="查詢欄位: "></asp:Label>
                                <asp:DropDownList ID="ddlQryField" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlQryField_SelectedIndexChanged">
                                    <asp:ListItem Value="0" Selected="True">FCS NO</asp:ListItem>
                                    <asp:ListItem Value="1">Int.No</asp:ListItem>
                                    <asp:ListItem Value="2">FTP目錄</asp:ListItem>
                                    <asp:ListItem Value="3">FTP上行檔案</asp:ListItem>
                                    <asp:ListItem Value="4">FTP下行檔案</asp:ListItem>
                                    <asp:ListItem Value="5">出報表</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtQryValue" runat="server" Width="170px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblQryDesc" runat="server" Text="" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="left" class="style1">
                                <asp:Label ID="Label2" runat="server" Text="排序依據: "></asp:Label>
                                <asp:DropDownList ID="ddlSortField" runat="server">
                                    <asp:ListItem Value="0" Selected="True">FCS NO</asp:ListItem>
                                    <asp:ListItem Value="1">FTP上行檔案</asp:ListItem>
                                    <asp:ListItem Value="2">FTP下行檔案</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="left">
                                <asp:Label ID="Label3" runat="server" Text="請注意,查詢結果不包含T6500,FCS-6500" ForeColor="Red"></asp:Label>
                            </td>
                            <tr>
                                <td colspan="3" align="center">
                                    <asp:Button ID="btnQry" runat="server" Text="查詢" OnClick="btnQry_Click" style="display:none;" />
                                    <input type="button" id="btnProgress" runat="server" value="查詢" onclick="document.getElementById('btnQry').click();doProgressWork();" />
                                    <br />
                                    <wcc:ProgressBar ID="ProgressBar1" runat="server" Width="200px" ProgressText="查詢中,請稍候..."
                                        IsShowSuccessText="false" BorderColor="Red" BorderStyle="Groove" BorderWidth="2px"
                                        Enabled="False" BackColor="Yellow" Font-Size="9pt" TimeOutText="執行時間超過" ProgressTimeOut="7200"
                                        SuccessText="查詢完成!" FalseText="查詢發生系統問題!" IsShowFalseText="True" IsShowSystemExceptionText="True"
                                        IsShowTrueText="False" TrueText="查詢完成!" Style="top: 0px; left: 0px" ImageUrl="~/images/progressbar13.gif"
                                        ProgressType="Image" IsNeedForPercentage="false" AjaxServerClass="FCSWeb.PageMakerDemo"
                                        IsNeedForNewWindow="false" Font-Names="微軟正黑體" ProgressTimeSpan="1" 
                                        ProgressTextDirection="horizontal" AjaxServerMethod="DoWork" />
                                </td>
                            </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlQryField" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table class="pageFontStyle">
                        <asp:Panel ID="palShowResult" runat="server" Visible="false">
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnExportData" runat="server" Text="匯出查詢結果" OnClick="btnExportData_Click" />
                                    <input id="btnPrint" type="button" value="列印" onclick="print();" />
                                </td>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <td style="text-align:center">
                                <wcc:PageMaker ID="PageMaker1" runat="server" PageSize="5" PagedControlID="GridQryResult" Font-Size="Medium" ToolTip="分頁控制項"
                                    SyncPageMakerID="PageMaker2" Align="center" OnCustomPageSize="PageMaker_CustomPageSize" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridQryResult" runat="server" CellPadding="4" ForeColor="#333333"
                                    GridLines="None" CssClass="GridViewStyle" AutoGenerateColumns="False" RowStyle-HorizontalAlign="Left"
                                    EnableViewState="false" AllowSorting="true"
                                    onsorting="GridQryResult_Sorting" onrowcreated="GridQryResult_RowCreated">
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="序號" Visible="true" SortExpression="SerialNo" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="GridSerialNo" runat="server" CssClass="englishFont" Text='<%# Eval("SerialNo").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FCS NO" SortExpression="FileID" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="GridFileID" runat="server" CssClass="englishFont" Text='<%# Eval("FileID").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="檔案說明" SortExpression="FileDescription" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="GridFileDescription" runat="server" Text='<%# ConvertValueToX(Eval("FileDescription").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FTP上行檔案<br/>FTP下行檔案">
                                            <ItemTemplate>
                                                <asp:Label ID="GridUpFile" runat="server" CssClass="englishFont" Text='<%# ConvertValueToX(CheckFolder_File(Eval("FTPDirSource").ToString() , Eval("FTPFileSource").ToString())) %>'></asp:Label>
                                                <br />
                                                <asp:Label ID="GridDownFile" runat="server" CssClass="englishFont" Text='<%# ConvertValueToX(CheckFolder_File(Eval("FTPDirTarget").ToString() , Eval("FTPFileTarget").ToString())) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="上行日期<br/>下行日期">
                                            <ItemTemplate>
                                                <asp:Label ID="GridYYYYMMDDSource" runat="server" CssClass="englishFont" Text='<%# ConvertValueToSpace(Eval("YYYYMMDDSource").ToString()) %>'></asp:Label>
                                                <br />
                                                <asp:Label ID="GridYYYYMMDDTarget" runat="server" CssClass="englishFont" Text='<%# ConvertValueToSpace(Eval("YYYYMMDDTarget").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FCS轉格式">
                                            <ItemTemplate>
                                                <asp:Label ID="GridConvertFlag" runat="server" CssClass="englishFont" Text='<%# Eval("ConvertFlag").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="出報表">
                                            <ItemTemplate>
                                                <asp:Label ID="GridRPTFlag" runat="server" CssClass="englishFont" Text='<%# ConvertValueToSpace(Eval("RPTFlag").ToString()) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Int.No" SortExpression="INTNo" HeaderStyle-ForeColor="White">
                                            <ItemTemplate>
                                                <asp:Label ID="GridINTNo" runat="server" CssClass="englishFont" Text='<%# Eval("INTNo").ToString() %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="GridViewFooterStyle" />
                                    <RowStyle CssClass="GridViewRowStyle" />
                                    <SelectedRowStyle CssClass="GridViewSelectedRowStyle" />
                                    <PagerStyle CssClass="GridViewPagerStyle" />
                                    <HeaderStyle CssClass="GridViewHeaderStyle" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:center">
                                <wcc:PageMaker ID="PageMaker2" runat="server" PageSize="5" PagedControlID="GridQryResult" Font-Size="Medium"
                                    SyncPageMakerID="PageMaker1" Align="center" OnCustomPageSize="PageMaker_CustomPageSize" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnQry" EventName="Click" />
                    <asp:PostBackTrigger ControlID="btnExportData" />
                    <asp:AsyncPostBackTrigger ControlID="GridQryResult" EventName="Sorting" />
                </Triggers>
            </asp:UpdatePanel>
        </center>
    </div>
    </form>
</body>
</html>
