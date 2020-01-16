using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sample2 : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    string Num1ClientId = Number1.FirstTextBoxClientID;
    ((TextBox)Number1.FindControl("A")).Attributes["onblur"] = "if(document.getElementById('" + Num1ClientId + "').value!=0){setChkEnable('" + CheckBoxList1.ClientID + "',true);}else{setChkCheck('" + CheckBoxList1.ClientID + "',false);setChkEnable('" + CheckBoxList1.ClientID + "',false);}";
    CheckBoxList1.Items[0].Attributes["onclick"] = "CheckBoxList_Click(this);";
    CheckBoxList1.Items[1].Attributes["onclick"] = "CheckBoxList_Click(this);";
  }
  protected void Button1_Click(object sender, EventArgs e)
  {
    string s = CheckBoxList1.Text;
  }
}
