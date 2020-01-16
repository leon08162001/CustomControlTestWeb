using System;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;

namespace APTemplate
{
	public class RegisterJavaScript
	{
    /// <summary>
    /// 註冊自訂控制項所使用的javascript檔。
    /// </summary>
    /// <param name="Page">註冊javascript於所在的網頁。</param>
		public static void RegisterContolIncludeScript(Page Page)
		{
			Page.ClientScript.RegisterClientScriptResource(typeof(RegisterJavaScript), "APTemplate.Resources.ControlJS.WebScript.js");
			Page.ClientScript.RegisterClientScriptResource(typeof(RegisterJavaScript), "APTemplate.Resources.ControlJS.calendar.js");
			Page.ClientScript.RegisterClientScriptResource(typeof(RegisterJavaScript), "APTemplate.Resources.ControlJS.TabsView.js");
			//Page.ClientScript.RegisterClientScriptResource(typeof(RegisterJavaScript), "APTemplate.Resources.ControlJS.JSValidation.js");
            Page.ClientScript.RegisterClientScriptResource(typeof(RegisterJavaScript), "APTemplate.Resources.ControlJS.chtpopcalendar.js");
            Page.ClientScript.RegisterClientScriptResource(typeof(RegisterJavaScript), "APTemplate.Resources.ControlJS.popcalendar.js");
            Page.ClientScript.RegisterClientScriptResource(typeof(RegisterJavaScript), "APTemplate.Resources.ControlJS.progressbar.js");
            Page.ClientScript.RegisterClientScriptResource(typeof(RegisterJavaScript), "APTemplate.Resources.ControlJS.AjaxUpload.js");
    }

    public static void RegisterCollapsiblePanelScript(Page Page)
    {
      Page.ClientScript.RegisterClientScriptResource(typeof(RegisterJavaScript), "APTemplate.Resources.ControlJS.core.js");
      Page.ClientScript.RegisterClientScriptResource(typeof(RegisterJavaScript), "APTemplate.Resources.ControlJS.events.js");
      Page.ClientScript.RegisterClientScriptResource(typeof(RegisterJavaScript), "APTemplate.Resources.ControlJS.css.js");
      Page.ClientScript.RegisterClientScriptResource(typeof(RegisterJavaScript), "APTemplate.Resources.ControlJS.coordinates.js");
      Page.ClientScript.RegisterClientScriptResource(typeof(RegisterJavaScript), "APTemplate.Resources.ControlJS.drag.js");
    }

     public static bool HasScriptManager(Page Page)
    {
      object Obj = null;
      List<Control> AllControls = PublicFunc.GetChildControls(Page);
      foreach (Control Ctl in AllControls)
      {
        
        if (Ctl.GetType() == typeof(System.Web.UI.ScriptManager))
        {
          Obj = Ctl;
          break;
        }
      }

      AllControls.Clear();
      AllControls = PublicFunc.GetChildControls(Page.Master);
      foreach (Control Ctl in AllControls)
      {

        if (Ctl.GetType() == typeof(System.Web.UI.ScriptManager))
        {
          Obj = Ctl;
          break;
        }
      }

      if (Obj != null)
        return true;
      else
        return false;
     }
	}
}
