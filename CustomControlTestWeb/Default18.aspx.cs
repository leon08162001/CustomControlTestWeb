using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default18 : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
      PopupCalendar1.FirstDate = Convert.ToString(Request["Val"]) != "" ? Convert.ToDateTime(Request["Val"]) : PopupCalendar1.FirstDate;
		WebScript.AddReturnValue(this.Page, Button1.ID, new string[] { Request["SourceCID"].ToString() }, new string[] { PopupCalendar1.FirstTextBoxClientID }, false, ReturnValueType.dialogArguments);
	}
}
