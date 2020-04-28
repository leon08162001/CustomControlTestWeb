using System;
using System.Configuration;
using System.ComponentModel;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace APTemplate
{
    /// <summary>
    /// 列舉-資料的驗證方式
    /// </summary>
    public enum OptionValidationType
    {
        /// <summary>
        /// 不驗證
        /// </summary>
        NoCheck,
        /// <summary>
        /// 驗證錯誤時以標籤顯示錯誤訊息
        /// </summary>
        Label,
        /// <summary>
        /// 驗證錯誤時以視窗顯示錯誤訊息
        /// </summary>
        Alert
    }

    /// <summary>
    /// 自訂控制項驗證功能的基底類別
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item><description>自定義的驗證功能，為抽象類別，必須繼承使用。</description></item>
    /// </list>
    /// </remarks>
    [ToolboxData("<{0}:ValidationBase runat=server></{0}:ValidationBase>")]
    abstract public class ValidationBase : CompositeControl, INamingContainer
    {
        protected override void OnPreRender(EventArgs e)
        {
            //處理驗證機制程式碼
            Context.Session["HasValidationSummary"] = null;
            if (ValidationType == OptionValidationType.Alert || ValidationType == OptionValidationType.Label)
            {
                List<Control> AllControls = PublicFunc.GetChildControls(Page);
                if (this.NeedValidation)
                {
                    foreach (Control Ctl in AllControls)
                    {
                        if (Ctl.GetType() == typeof(Button) || Ctl.GetType() == typeof(ImageButton) || Ctl.GetType() == typeof(LinkButton) || Ctl.GetType() == typeof(APTemplate.Button_Normal) || Ctl.GetType() == typeof(APTemplate.Button_ConfirmYesNo))
                        {
                            if (Convert.ToBoolean(Ctl.GetType().GetProperty("CausesValidation").GetValue(Ctl, null)) == true && ((IButtonControl)Ctl).ValidationGroup == this.ValidationGroup)
                            {
                                ((WebControl)Ctl).Attributes["onclick"] += ((WebControl)Ctl).Attributes["onclick"] == null || ((WebControl)Ctl).Attributes["onclick"].IndexOf("if (Page_ClientValidate('" + this.ValidationGroup + "')==false){return false;};") == -1 ? "if (Page_ClientValidate('" + this.ValidationGroup + "')==false){return false;};" : "";
                            }
                        }
                        if (Ctl.GetType() == typeof(HtmlInputSubmit) && ((HtmlInputSubmit)Ctl).ValidationGroup == this.ValidationGroup)
                        {
                            ((HtmlControl)Ctl).Attributes["onclick"] += ((HtmlControl)Ctl).Attributes["onclick"] == null || ((HtmlControl)Ctl).Attributes["onclick"].IndexOf("if (Page_ClientValidate('" + this.ValidationGroup + "')==false){return false;};") == -1 ? "if (Page_ClientValidate('" + this.ValidationGroup + "')==false){return false;};" : "";
                        }
                    }
                }
            }
        }

        /// <summary>
        ///是否需要驗證資料。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("是否需要驗證資料。")]
        public virtual bool NeedValidation
        {
            get
            {
                if (ViewState["NeedValidation"] == null)
                    return true;
                else
                    return (bool)ViewState["NeedValidation"];
            }
            set { ViewState["NeedValidation"] = value; }
        }

        /// <summary>
        ///資料的驗證方式。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("資料的驗證方式。")]
        public virtual OptionValidationType ValidationType
        {
            get
            {
                if (ViewState["ValidationType"] == null)
                    return OptionValidationType.Label;
                else
                    return (OptionValidationType)ViewState["ValidationType"];
            }
            set
            {
                ViewState["ValidationType"] = value;
                if (value == OptionValidationType.Alert)
                {
                    ViewState["ValidationSummary"] = ViewState["ValidationSummary"] == null ? new ValidationSummary() : (ValidationSummary)ViewState["ValidationSummary"];
                }
            }
        }

        /// <summary>
        ///驗證群組名稱。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("驗證群組名稱。")]
        public virtual String ValidationGroup
        {
            get
            {
                if (ViewState["ValidationGroup"] == null)
                    return "";
                else
                    return (String)ViewState["ValidationGroup"];
            }
            set { ViewState["ValidationGroup"] = value; }
        }

        protected void SetValidationType(OptionValidationType ValidationType, BaseValidator[] Validator, PlaceHolder Container)
        {
            if (ValidationType == OptionValidationType.Label)
            {

            }

            else if (ValidationType == OptionValidationType.Alert)
            {
                ValidationSummary VS = (ValidationSummary)ViewState["ValidationSummary"];
                VS.ID = "ValidationSummary";
                foreach (BaseValidator Val in Validator)
                {
                    if (Container.Controls.Contains(Val))
                    {
                        Val.Text = "*";
                    }
                }
                VS.ShowSummary = false;
                VS.ShowMessageBox = true;
                if (Context.Session["HasValidationSummary"] == null)
                {
                    Container.Controls.Add(VS);
                    Context.Session["HasValidationSummary"] = true;
                }
            }
            else if (ValidationType == OptionValidationType.NoCheck)
            {
                foreach (BaseValidator Val in Validator)
                {
                    Container.Controls.Remove(Val);
                }
            }
        }
    }
}