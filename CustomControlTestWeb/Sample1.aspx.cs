using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Default32 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		DiscountFeeScriptEvent();
	//  RadioButtonList1.Items[0].Attributes["onclick"] = "document.getElementById('" + TextBox1.ClientID + "').disabled=true;" +
	//                                                    "document.getElementById('" + TextBox1.ClientID + "').value='';" +
	//                                                    "document.getElementById('" + CustomValidator1.ClientID + "').style['display']='none';";
	//  RadioButtonList1.Items[1].Attributes["onclick"] = "document.getElementById('" + TextBox1.ClientID + "').disabled=true;" +
	//                                                    "document.getElementById('" + TextBox1.ClientID + "').value='';" +
	//                                                    "document.getElementById('" + CustomValidator1.ClientID + "').style['display']='none';";
	//  RadioButtonList1.Items[2].Attributes["onclick"] = "document.getElementById('" + TextBox1.ClientID + "').disabled=false;";
	}
	protected void Button1_Click(object sender, EventArgs e)
	{
		
	}

	//扣除優惠手續費-javascript設定
	private void DiscountFeeScriptEvent()
	{
		string JsRadioButton= "<script language=javascript> \n" +
			                        "\t function DiscountFeeScriptForRbn12(){ \n" +
															"\t document.getElementById('" + TextBox1.ClientID + "').disabled=true; \n" +
															"\t document.getElementById('" + TextBox1.ClientID + "').value=''; \n" +
															"\t document.getElementById('" + CustomValidator1.ClientID + "').style['display']='none'; \n" +
															"\t} \n" +
															"\t \n" +
															"\t function DiscountFeeScriptForRbn3(){ \n" +
															"\t document.getElementById('" + TextBox1.ClientID + "').disabled=false; \n" +
															"\t} \n" +
				                      "</script>";

		Page.ClientScript.RegisterStartupScript(this.GetType(), "Date_ClientValidate", JsRadioButton);
		RadioButtonList1.Items[0].Attributes["onclick"] = "DiscountFeeScriptForRbn12();";
		RadioButtonList1.Items[1].Attributes["onclick"] = "DiscountFeeScriptForRbn12();";
		RadioButtonList1.Items[2].Attributes["onclick"] = "DiscountFeeScriptForRbn3();";
	}
}
