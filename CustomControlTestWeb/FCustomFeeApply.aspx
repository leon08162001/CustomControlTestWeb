<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FCustomFeeApply.aspx.cs"
	Inherits="FCustomFeeApply" %>
<%@ Register Assembly="WebCustomControls" Namespace="APTemplate" TagPrefix="wcc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<style type="text/css">
		.style1
		{
			text-align: center;
			background-color: #95B3DE;
		}
		.rowcenter
		{
			text-align: center;
			border-bottom-color: Gray;
			border-bottom-width: 1;
			border-bottom-style: solid;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<div>
		<table width="90%">
			<tr>
				<td colspan="4" style="text-align: center">
					<asp:Label ID="Label1" runat="server" Text="證  券  客　戶　手　續　費　折　讓　申　請　單"></asp:Label>
				</td>
			</tr>
			<tr>
				<td style="width: 7%">
					<asp:Label ID="Label2" runat="server" Text="客戶屬性:"></asp:Label>
				</td>
				<td>
					<asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="2">
						<asp:ListItem Value="1">1.個人戶</asp:ListItem>
						<asp:ListItem Value="2">2.法人戶</asp:ListItem>
					</asp:RadioButtonList>
				</td>
				<td>
					<asp:Label ID="Label24" runat="server" Text="填表日期:"></asp:Label>
					<asp:Label ID="Label25" runat="server" Text="2009/9/2"></asp:Label>
				</td>
				<td>
					<asp:Label ID="Label3" runat="server" Text="單號:"></asp:Label>
					<asp:Label ID="Label4" runat="server" Text="B-2009070001"></asp:Label>
				</td>
			</tr>
		</table>
	</div>
	<table cellspacing="1" cellpadding="1" rules="all" border="1" style="border-color: #339966;
		border-width: 1px; border-style: Solid; width: 90%; margin-bottom: 0px">
		<tr>
			<td style="width: 9%;">
				分公司:
			</td>
			<td style="width: 18%;">
				<asp:Label ID="Label6" runat="server" Text="永和分公司"></asp:Label>
			</td>
			<td style="width: 7%;">
				單位:
			</td>
			<td style="width: 18%;">
				證券一部
			</td>
			<td style="width: 7%;">
				員編:
			</td>
			<td style="width: 18%;">
				<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
			</td>
			<td style="width: 7%;">
				營業員:
			</td>
			<td style="width: 18%;">
				王大明
			</td>
		</tr>
		<tr>
			<td>
				主要帳號:
			</td>
			<td colspan="3">
				<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
				<asp:Label ID="Label7" runat="server" Text="游釧茹"></asp:Label>
			</td>
			<td>
				關聯帳號:
			</td>
			<td colspan="3">
				<wcc:TextBox_PopupWindow ID="TextBox_PopupWindow1" runat="server" HasBorder="False"
					Title="" IsShowTitle="false" TitleBackColor="White" TitleForeColor="Black" TextBoxWidth="250px" />
			</td>
		</tr>
		<tr>
			<td>
				異動原因:
			</td>
			<td colspan="7">
				<wcc:NewDropDownList ID="NewDropDownList1" runat="server" IsShowTitle="false"
					TitleBackColor="White" TitleForeColor="Black" HasBorder="false">
					<Items>
						<asp:ListItem Selected="True" Value="1">新申請</asp:ListItem>
						<asp:ListItem Value="2">變更關聯戶帳號</asp:ListItem>
						<asp:ListItem Value="3">變更條件</asp:ListItem>
						<asp:ListItem Value="4">註銷折讓</asp:ListItem>
					</Items>
				</wcc:NewDropDownList>
				<asp:Label ID="Label5" runat="server" Text="申請時最近交易營業員："></asp:Label>
				<asp:Label ID="Label23" runat="server" Text="98011 劉德華"></asp:Label>
			</td>
		</tr>
		<tr>
			<td colspan="1">
				客戶概況:
			</td>
			<td colspan="5">
				<table>
					<tr>
						<td>
							<asp:Label ID="Label8" runat="server" Text="客戶簡介:"></asp:Label>
							<asp:TextBox ID="TextBox3" runat="server" Width="380px"></asp:TextBox>
						</td>
					</tr>
					<tr>
						<td colspan="2">
							(變更折讓請檢附客戶最近三個月之業績)
						</td>
					</tr>
					<tr>
						<td colspan="2">
							<wcc:NewDropDownList ID="NewDropDownList2" runat="server" Title="預估業績:" TitleBackColor="White"
								TitleForeColor="Black" HasBorder="False">
								<Items>
									<asp:ListItem Selected="True">0-500萬</asp:ListItem>
									<asp:ListItem>500萬(含)-1000萬</asp:ListItem>
									<asp:ListItem>1000萬(含)-5000萬</asp:ListItem>
									<asp:ListItem>5000萬(含)-1億</asp:ListItem>
									<asp:ListItem>1億(含)-3億</asp:ListItem>
									<asp:ListItem>3億(含)-5億</asp:ListItem>
									<asp:ListItem>5億(含)以上</asp:ListItem>
								</Items>
							</wcc:NewDropDownList>
						</td>
					</tr>
					<tr>
						<td>
							<wcc:NewDropDownList ID="NewDropDownList3" runat="server" Title="財力狀況:" TitleBackColor="White"
								TitleForeColor="Black" HasBorder="False">
								<Items>
									<asp:ListItem Selected="True">500萬以下</asp:ListItem>
									<asp:ListItem>500萬(含)-1000萬</asp:ListItem>
									<asp:ListItem>1000萬(含)-3000萬</asp:ListItem>
									<asp:ListItem>3000萬(含)-5000萬</asp:ListItem>
									<asp:ListItem>5000萬(含)-1億</asp:ListItem>
									<asp:ListItem>1億(含)以上</asp:ListItem>
								</Items>
							</wcc:NewDropDownList>
						</td>
					</tr>
				</table>
			</td>
			<td colspan="1">
				上傳附件:
			</td>
			<td colspan="1">
				<wcc:Button_PopupWindow ID="Button_PopupWindow1" runat="server" />
			</td>
		</tr>
		<tr>
			<td rowspan="2">
				申請資格:
			</td>
			<td colspan="7">
				<table>
					<tr>
						<td style="border-color: Black; border-width: 1px">
							下單方式:
						</td>
						<td>
							<table cellspacing="1" cellpadding="1" border="0" style="border-color: Black; border-width: 1px;
								border-style: Solid;">
								<tr>
									<td colspan="3">
										<asp:CheckBox ID="CheckBox1" runat="server" Text="個人戶" Font-Bold="true" Font-Size="Small" />
									</td>
								</tr>
								<tr>
									<td>
										<asp:Label ID="Label9" runat="server" Text="1.傳統單最低業績：" Font-Bold="true" Font-Size="Small"></asp:Label>
										<asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
										<asp:Label ID="Label13" runat="server" Font-Size="Small">(元)</asp:Label>
									</td>
									<td style="width: 30px">
									</td>
									<td>
										<asp:Label ID="Label10" runat="server" Text="2.電子單最低業績：" Font-Bold="true" Font-Size="Small"></asp:Label>
										<asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
										<asp:Label ID="Label12" runat="server" Font-Size="Small">(元)</asp:Label>
									</td>
								</tr>
								<tr>
									<td colspan="3">
										<asp:Label ID="Label11" runat="server" Text="3.傳統單＋電子單合併最低業績：" Font-Bold="true" Font-Size="Small"></asp:Label>
										<asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
										<asp:Label ID="Label14" runat="server" Font-Size="Small">(元)</asp:Label>
									</td>
								</tr>
							</table>
							<br />
							<table cellspacing="1" cellpadding="1" border="0" style="border-color: Black; border-width: 1px;
								border-style: Solid;">
								<tr>
									<td colspan="3">
										<asp:CheckBox ID="CheckBox2" runat="server" Text="關聯戶" Font-Bold="true" Font-Size="Small" />
									</td>
								</tr>
								<tr>
									<td>
										<asp:Label ID="Label15" runat="server" Text="1.傳統單合併最低業績：" Font-Bold="true" Font-Size="Small"></asp:Label>
										<asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
										<asp:Label ID="Label16" runat="server" Font-Size="Small">(元)</asp:Label>
									</td>
									<td style="width: 30px">
									</td>
									<td>
										<asp:CheckBox ID="CheckBox3" runat="server" Font-Size="Small" Text="合併業績找落點" />
										<asp:CheckBox ID="CheckBox4" runat="server" Font-Size="Small" Text="不合併業績找落點" />
									</td>
								</tr>
								<tr>
									<td>
										<asp:Label ID="Label19" runat="server" Text="2.電子單合併最低業績：" Font-Bold="true" Font-Size="Small"></asp:Label>
										<asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
										<asp:Label ID="Label20" runat="server" Font-Size="Small">(元)</asp:Label>
									</td>
									<td style="width: 30px">
									</td>
									<td>
										<asp:CheckBox ID="CheckBox5" runat="server" Font-Size="Small" Text="合併業績找落點" />
										<asp:CheckBox ID="CheckBox6" runat="server" Font-Size="Small" Text="不合併業績找落點" />
									</td>
								</tr>
								<tr>
									<td>
										<asp:Label ID="Label17" runat="server" Text="3.傳統單＋電子單合併最低業績：" Font-Bold="true" Font-Size="Small"></asp:Label>
										<asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
										<asp:Label ID="Label18" runat="server" Font-Size="Small">(元)</asp:Label>
									</td>
									<td style="width: 30px">
									</td>
									<td>
										<asp:CheckBox ID="CheckBox7" runat="server" Font-Size="Small" Text="合併業績(傳+電)分別找落點" />
										<asp:CheckBox ID="CheckBox8" runat="server" Font-Size="Small" Text="合併業績(傳OR電) 分別找落點" />
										<asp:CheckBox ID="CheckBox9" runat="server" Font-Size="Small" Text="不合併業績找落點" />
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td colspan="7">
				<table width="100%">
					<tr>
						<td style="border-color: Black; border-width: 1px; border-style: solid; width: 13%;">
							扣除優惠手續費:
						</td>
						<td style="border-color: Black; border-width: 1px; border-style: Solid; width: 87%;">
							<table>
								<tr>
									<td colspan="3">
										<asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal"
											Style="display: inline">
											<asp:ListItem Selected="True">扣除(傳月+電月-傳日-電日，負數歸零)</asp:ListItem>
											<asp:ListItem>不扣除(營業員自行負擔)</asp:ListItem>
											<asp:ListItem>其他:</asp:ListItem>
										</asp:RadioButtonList>
										<asp:TextBox ID="TextBox10" runat="server" Width="250px"></asp:TextBox>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td rowspan="2">
				折讓條件:
			</td>
			<td colspan="7">
				<table width="100%">
					<tr>
						<td style="border-color: Black; border-width: 1px; border-style: solid; width: 13%;">
							折讓失效年月:
						</td>
						<td style="border-color: Black; border-width: 1px; border-style: Solid; width: 87%;">
							<table>
								<tr>
									<td colspan="3">
										<wcc:PopupCalendar ID="PopupCalendar2" runat="server" Font-Size="Medium"
											TitleBackColor="White" TitleForeColor="Black" NeedValue="false" IsShowTitle="false"
											HasBorder="false" DateFormat="年月" />
										(當月一日起失效)
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td colspan="7">
				<table width="100%">
					<tr>
						<td style="border-color: Black; border-width: 1px; border-style: solid; width: 13%;">
							傳統單折讓設定(月折):
						</td>
						<td style="border-color: Black; border-width: 1px; border-style: Solid; width: 87%;">
							<table>
								<tr>
									<td colspan="3">
										<asp:Button ID="Button1" runat="server" Text="新增級距" />
										<br />
										<table cellpadding="1" cellspacing="0" border="1">
											<tr>
												<td class="style1">
													級數
												</td>
												<td class="style1">
													起始總業績
												</td>
												<td class="style1">
													截止總業績
												</td>
												<td class="style1">
													客戶折讓<br>
													(元/百萬)
												</td>
												<td class="style1">
													接單獎金<br>
													(元/百萬)
												</td>
												<td class="style1">
													底薪制<br>
													折數(%)
												</td>
												<td class="style1">
													績效制<br>
													折數(%)
												</td>
												<td class="style1">
													展業獎金
												</td>
												<td class="style1">
													A<br>
													(元/百萬)
												</td>
												<td class="style1">
													B<br>
													(元/百萬)
												</td>
											</tr>
											<tr class="rowcenter">
												<td>
													1
												</td>
												<td>
													1
												</td>
												<td>
													49,999,999
												</td>
												<td>
													300.00
												</td>
												<td>
													&nbsp;
												</td>
												<td>
													60.00
												</td>
												<td>
													100.00
												</td>
												<td>
													&nbsp;
												</td>
												<td>
													700.00
												</td>
												<td>
													&nbsp;
												</td>
											</tr>
											<tr class="rowcenter">
												<td>
													2
												</td>
												<td>
													50,000,000
												</td>
												<td>
													99,999,999
												</td>
												<td>
													400.00
												</td>
												<td>
													&nbsp;
												</td>
												<td>
													50.00
												</td>
												<td>
													100.00
												</td>
												<td>
													&nbsp;
												</td>
												<td>
													&nbsp;
												</td>
												<td>
													&nbsp;
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td style="border-color: Black; border-width: 1px; border-style: solid; width: 13%;">
							電子單折讓設定(月折):
						</td>
						<td style="border-color: Black; border-width: 1px; border-style: Solid; width: 87%;">
							<table>
								<tr>
									<td colspan="3">
										<asp:Button ID="Button2" runat="server" Text="新增級距" />
										<br />
										<table cellpadding="1" cellspacing="0" border="1">
											<tr>
												<td class="style1">
													級數
												</td>
												<td class="style1">
													起始總業績
												</td>
												<td class="style1">
													截止總業績
												</td>
												<td class="style1">
													客戶折讓<br>
													(元/百萬)
												</td>
												<td class="style1">
													接單獎金<br>
													(元/百萬)
												</td>
												<td class="style1">
													底薪制<br>
													折數(%)
												</td>
												<td class="style1">
													績效制<br>
													折數(%)
												</td>
												<td class="style1">
													展業獎金
												</td>
												<td class="style1">
													A<br>
													(元/百萬)
												</td>
												<td class="style1">
													B<br>
													(元/百萬)
												</td>
											</tr>
											<tr class="rowcenter">
												<td>
													1
												</td>
												<td>
													1
												</td>
												<td>
													49,999,999
												</td>
												<td>
													300.00
												</td>
												<td>
													&nbsp;
												</td>
												<td>
													60.00
												</td>
												<td>
													100.00
												</td>
												<td>
													&nbsp;
												</td>
												<td>
													700.00
												</td>
												<td>
													&nbsp;
												</td>
											</tr>
											<tr class="rowcenter">
												<td>
													2
												</td>
												<td>
													50,000,000
												</td>
												<td>
													99,999,999
												</td>
												<td>
													400.00
												</td>
												<td>
													&nbsp;
												</td>
												<td>
													50.00
												</td>
												<td>
													100.00
												</td>
												<td>
													&nbsp;
												</td>
												<td>
													&nbsp;
												</td>
												<td>
													&nbsp;
												</td>
											</tr>
										</table>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td style="border-color: Black; border-width: 1px; border-style: solid; width: 13%;">
							優惠手續費:
						</td>
						<td style="border-color: Black; border-width: 1px; border-style: Solid; width: 87%;">
							<table>
								<tr>
									<td colspan="3">
										<asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal"
											Style="display: inline">
											<asp:ListItem>五級制</asp:ListItem>
											<asp:ListItem>三級制(僅台北)</asp:ListItem>
											<asp:ListItem>固定制</asp:ListItem>
										</asp:CheckBoxList>
										<asp:PlaceHolder ID="PlaceHolder1" runat="server">&nbsp;優惠:</asp:Label><asp:TextBox
											ID="TextBox11" runat="server"></asp:TextBox>
											元/億&nbsp;(公司收取:<asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>元/億) </asp:PlaceHolder>
									</td>
								</tr>
							</table>
							<table>
								<tr>
									<td colspan="3">
										<asp:Label ID="Label21" runat="server" Text="通路總部建檔人員:"></asp:Label>
										<asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
										<wcc:PopupCalendar ID="PopupCalendar3" runat="server" Title="建檔日期:" TextBoxWidth="70px"
											Font-Size="Medium" TitleBackColor="White" TitleForeColor="Black" HasBorder="false"
											NeedValue="false" />
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td rowspan="2">
				其他條件:
			</td>
			<td colspan="7">
				<table width="100%">
					<tr>
						<td style="border-color: Black; border-width: 1px; border-style: solid; width: 13%;">
							外接資訊:
						</td>
						<td style="border-color: Black; border-width: 1px; border-style: Solid; width: 87%;">
							<asp:CheckBoxList ID="CheckBoxList2" runat="server" RepeatDirection="Horizontal"
								Style="display: inline">
								<asp:ListItem>網贏專案</asp:ListItem>
								<asp:ListItem>外接單機</asp:ListItem>
							</asp:CheckBoxList>
							&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:Label ID="Label22" runat="server" Text="外接資訊費 $   5,000 / 季"></asp:Label>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td colspan="7">
				<table width="100%">
					<tr>
						<td style="border-color: Black; border-width: 1px; border-style: solid; width: 13%;">
							促銷方案:
						</td>
						<td style="border-color: Black; border-width: 1px; border-style: Solid; width: 87%;">
							<table cellpadding="1" cellspacing="0" border="1">
								<tr>
									<td class="style1">
										促銷方案代號<br>
										(名稱)
									</td>
									<td class="style1">
										級數
									</td>
									<td class="style1">
										起始金額<br>
										(元)
									</td>
									<td class="style1">
										截止金額<br>
										(元)
									</td>
									<td class="style1">
										客戶折讓<br>
										(元/萬)
									</td>
									<td class="style1">
										原折讓扣減<br>
										(元/萬)
									</td>
									<td class="style1">
										傳統獎金<br>
										(元/萬)
									</td>
									<td class="style1">
										電子獎金<br>
										(元/萬)
									</td>
									<td class="style1">
										業績折數<br>
										-傳統(%)
									</td>
									<td class="style1">
										業績折數<br>
										-電子(%)
									</td>
								</tr>
								<tr class="rowcenter">
									<td>
										098S01(南崁七週年-<br>
										倚天C699(70萬折700)*12期)
									</td>
									<td>
										1
									</td>
									<td>
										1
									</td>
									<td>
										10.00
									</td>
									<td>
										0.00
									</td>
									<td>
										0.20
									</td>
									<td>
										0.20
									</td>
									<td>
										0.20
									</td>
									<td>
										100.00
									</td>
									<td>
										100.00
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td>
				備註說明:
			</td>
			<td colspan="7">
				<asp:TextBox ID="TextBox14" runat="server" Width="100%" Rows="5" TextMode="MultiLine"
					MaxLength="500"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				簽核意見:
			</td>
			<td colspan="7">
				<asp:TextBox ID="TextBox15" runat="server" Width="100%" Rows="5" TextMode="MultiLine"
					MaxLength="500"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				原申請條件:
			</td>
			<td colspan="7">
				<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="#">原有申請表單號超連結</asp:HyperLink>
			</td>
		</tr>
	</table>
	</form>
</body>
</html>
