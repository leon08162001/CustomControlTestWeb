using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic;
using System.Drawing;
using System.Web.UI.Design;
using System.Drawing.Design;

namespace APTemplate
{
    /// <summary>
    /// 列舉-ProgressBar型態
    /// </summary>
    public enum ProgressType
    {
        Default = 0,
        Image = 1
    }

    /// <summary>
    /// 列舉-ProgressText顯示方向
    /// </summary>
    public enum ProgressDirectionType
    {
        horizontal,
        vertical
    }

    /// <summary>
    /// 自定義的進度列控制項。
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ProgressBar runat=server></{0}:ProgressBar>")]
    [ToolboxBitmap(typeof(ProgressType), "Resources.progressbar.gif")]
    public class ProgressBar : CompositeControl, INamingContainer
    {
        protected Label ProgressLabel = new Label();
        protected System.Web.UI.WebControls.Image ImgProgressBar = new System.Web.UI.WebControls.Image();
        protected Boolean _IsShowSystemExceptionText = false;
        protected Boolean _IsShowTrueText = false;
        protected Boolean _IsShowFalseText = false;
        protected Boolean _IsNeedForPercentage = false;
        protected Boolean _IsNeedForNewWindow = false;
        protected String _TrueText = "";
        protected String _FalseText = "";
        protected String _AjaxServerClass = "";
        protected String _AjaxServerMethod = "DoWork";
        protected int _ProgressTimeOut = 20;
        protected int _ProgressTimeSpan = 1;
        protected String _TimeOutText = "";
        protected ProgressType _ProgressType = ProgressType.Default;
        protected string _ImageUrl = "";
        protected ProgressDirectionType _ProgressDirectionType = ProgressDirectionType.horizontal;

        /// <summary>
        ///ProgressBar類型。
        /// </summary>
        [Category("自訂")]
        [DefaultValue(ProgressType.Default)]
        [Description("ProgressBar類型。")]
        public ProgressType ProgressType
        {
            get { return _ProgressType; }
            set { _ProgressType = value; }
        }

        /// <summary>
        /// 進度列進行中顯示的文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("進度列進行中顯示的文字。")]
        public String ProgressText
        {
            get
            {
                return ProgressLabel.Text;
            }

            set
            {
                ProgressLabel.Text = value;
            }
        }

        /// <summary>
        /// 進度列進行中顯示文字的方向。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("進度列進行中顯示文字的方向。")]
        public ProgressDirectionType ProgressTextDirection
        {
            get { return _ProgressDirectionType; }
            set { _ProgressDirectionType = value; }
        }

        /// <summary>
        /// 實作Ajax Sserver端程式所在的Class名稱(若有命名空間需包含完整命名空間)。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("實作Ajax Sserver端程式所在的Class名稱(若有命名空間需包含完整命名空間)。")]
        public String AjaxServerClass
        {
            get
            {
                return _AjaxServerClass;
            }

            set
            {
                _AjaxServerClass = value;
            }
        }

        /// <summary>
        /// 實作Ajax Sserver端程式所在的Method名稱。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("實作Ajax Sserver端程式所在的Method名稱。")]
        public String AjaxServerMethod
        {
            get
            {
                return _AjaxServerMethod;
            }

            set
            {
                _AjaxServerMethod = value;
            }
        }

        /// <summary>
        ///ProgressBar運行中的圖片位置。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("ProgressBar運行中的圖片位置。")]
        [UrlProperty()]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        public string ImageUrl
        {
            get { return _ImageUrl; }
            set { _ImageUrl = value; }
        }

        /// <summary>
        /// 執行工作TimeOut的時間(秒)。
        /// </summary>
        [DefaultValue(20),
         Category("自訂"),
         Description("執行工作TimeOut的時間(秒)")]
        public int ProgressTimeOut
        {
            get
            {
                return _ProgressTimeOut;
            }

            set
            {
                _ProgressTimeOut = value;
            }
        }

        /// <summary>
        /// 更新進度的間隔時間(秒)。
        /// </summary>
        [DefaultValue(1),
         Category("自訂"),
         Description("更新進度的間隔時間(秒)")]
        public int ProgressTimeSpan
        {
            get
            {
                return _ProgressTimeSpan;
            }

            set
            {
                _ProgressTimeSpan = value;
            }
        }

        /// <summary>
        /// 發生TimeOut時顯示的訊息。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("發生TimeOut時顯示的訊息。")]
        public String TimeOutText
        {
            get
            {
                return _TimeOutText;
            }

            set
            {
                _TimeOutText = value;
            }
        }

        /// <summary>
        /// 進度列執行完畢成功時顯示的文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("進度列執行完畢成功時顯示的文字。")]
        public String TrueText
        {
            get
            {
                return _TrueText;
            }

            set
            {
                _TrueText = value;
            }
        }

        /// <summary>
        /// 進度列執行完畢失敗時顯示的文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("進度列執行完畢失敗時顯示的文字。")]
        public String FalseText
        {
            get
            {
                return _FalseText;
            }

            set
            {
                _FalseText = value;
            }
        }

        /// <summary>
        /// 進度列執行工作發生Exception時是否顯示Exception訊息。
        /// </summary>
        [DefaultValue(false),
         Category("自訂"),
         Description("進度列執行工作發生Exception時是否顯示Exception訊息。")]
        public Boolean IsShowSystemExceptionText
        {
            get
            {
                return _IsShowSystemExceptionText;
            }

            set
            {
                _IsShowSystemExceptionText = value;
            }
        }

        /// <summary>
        /// 進度列執行工作完畢回傳true時是否顯示TrueText屬性值。
        /// </summary>
        [DefaultValue(false),
         Category("自訂"),
         Description("進度列執行工作完畢回傳true時是否顯示TrueText屬性值。")]
        public Boolean IsShowTrueText
        {
            get
            {
                return _IsShowTrueText;
            }

            set
            {
                _IsShowTrueText = value;
            }
        }

        /// <summary>
        /// 進度列執行工作完畢回傳false時是否顯示FalseText屬性值。
        /// </summary>
        [DefaultValue(false),
         Category("自訂"),
         Description("進度列執行工作完畢回傳false時是否顯示FalseText屬性值。")]
        public Boolean IsShowFalseText
        {
            get
            {
                return _IsShowFalseText;
            }

            set
            {
                _IsShowFalseText = value;
            }
        }

        /// <summary>
        /// 是否產生進度列百分比的資訊(須於server端撰寫計算進度百分比的程式)。
        /// </summary>
        [DefaultValue(false),
         Category("自訂"),
         Description("是否產生進度列百分比的資訊(須於server端撰寫計算進度百分比的程式)。")]
        public Boolean IsNeedForPercentage
        {
            get
            {
                return _IsNeedForPercentage;
            }

            set
            {
                _IsNeedForPercentage = value;
            }
        }

        /// <summary>
        /// ProgressBar是否在新視窗中執行。
        /// </summary>
        [DefaultValue(false),
         Category("自訂"),
         Description("ProgressBar是否在新視窗中執行。")]
        public Boolean IsNeedForNewWindow
        {
            get
            {
                return _IsNeedForNewWindow;
            }

            set
            {
                _IsNeedForNewWindow = value;
            }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (this.AjaxServerClass == "")
            {
                this.AjaxServerClass = this.Page.GetType().ToString().Replace("ASP.", "").Replace("_aspx", "");
            }
            ImgProgressBar.ID = this.ClientID + "_ImgProgressBar";
            ProgressLabel.ID = this.ClientID + "_progressText";
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            if (this.ProgressType == ProgressType.Default)
            {
                for (int i = 1; i <= 9; i++)
                {
                    output.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_progress" + i);
                    output.RenderBeginTag(HtmlTextWriterTag.Span);
                    output.Write("&nbsp;&nbsp;");
                    output.RenderEndTag();
                    output.Write(" ");
                }
            }
            else
            {
                ImgProgressBar.ImageUrl = base.ResolveClientUrl(this.ImageUrl);
                ImgProgressBar.RenderControl(output);
            }
            if (ProgressTextDirection == ProgressDirectionType.vertical) { output.Write("<br />"); }
            ProgressLabel.ForeColor = this.ForeColor;
            ProgressLabel.Font.Bold = this.Font.Bold;
            ProgressLabel.Font.Size = this.Font.Size;
            ProgressLabel.Font.Italic = this.Font.Italic;
            ProgressLabel.Font.Name = this.Font.Name;
            ProgressLabel.Font.Overline = this.Font.Overline;
            ProgressLabel.Font.Underline = this.Font.Underline;
            ProgressLabel.Font.Strikeout = this.Font.Strikeout;
            ProgressLabel.RenderControl(output);
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_divProgressBar");
            if (this.Enabled)
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "inline-block");
            }
            else
            {
                writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
            }
            writer.AddStyleAttribute(HtmlTextWriterStyle.TextAlign, "center");
            writer.AddStyleAttribute(HtmlTextWriterStyle.Position, "relative");
            //邊框顏色
            String BorderColor = "#" + Conversion.Hex(this.BackColor.R).PadLeft(2, '0') + Conversion.Hex(this.BackColor.G).PadLeft(2, '0') + Conversion.Hex(this.BackColor.B).PadLeft(2, '0');
            writer.AddStyleAttribute(HtmlTextWriterStyle.BorderColor, BorderColor);
            //邊框寬度
            writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, this.BorderWidth.Value + "px");
            //邊框樣式
            writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, this.BorderStyle.ToString());
            writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingLeft, "2px");
            writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingRight, "2px");
            writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingTop, "2px");
            writer.AddStyleAttribute(HtmlTextWriterStyle.PaddingBottom, "2px");
            //背景顏色
            String BackColor = this.BackColor.Name == "0" ? "#ffffff" : "#" + Conversion.Hex(this.BackColor.R).PadLeft(2, '0') + Conversion.Hex(this.BackColor.G).PadLeft(2, '0') + Conversion.Hex(this.BackColor.B).PadLeft(2, '0');
            writer.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, BackColor);
            //前景顏色
            String ForeColor = "#" + Conversion.Hex(this.ForeColor.R).PadLeft(2, '0') + Conversion.Hex(this.ForeColor.G).PadLeft(2, '0') + Conversion.Hex(this.ForeColor.B).PadLeft(2, '0');
            writer.AddStyleAttribute(HtmlTextWriterStyle.Color, ForeColor);
            //控制項寬度
            if(this.ProgressType == ProgressType.Default)
                writer.AddStyleAttribute(HtmlTextWriterStyle.Width, this.Width.Value + "px");
        }

        protected override void OnPreRender(EventArgs e)
        {
            RegisterJavaScript.RegisterContolIncludeScript(this.Page);
            if (!Page.ClientScript.IsStartupScriptRegistered(this.ClientID + "_ProgressBarScript"))
            {
                string script = GenerateProgressScript();
                Page.ClientScript.RegisterStartupScript(this.GetType(), this.ClientID + "_ProgressBarScript", script);
            }
            base.OnPreRender(e);
        }

        private string GenerateProgressScript()
        {
            StringBuilder SB = new StringBuilder("");
            SB.Append("<script language=javascript> \n");
            SB.Append("\t /* \n");
            SB.Append("\t 請在.cs檔加入下列程式碼 \n");
            SB.Append("\t using AjaxPro; \n");
            SB.Append("\t \n");
            SB.Append("\t protected void Page_Load(object sender, EventArgs e) \n");
            SB.Append("\t { \n");
            SB.Append("\t\t AjaxPro.Utility.RegisterTypeForAjax(typeof(" + this.AjaxServerClass + ")); \n");
            SB.Append("\t } \n");
            SB.Append("\n");
            SB.Append("\t //執行工作的程序 \n");
            SB.Append("\t [AjaxMethod()] \n");
            SB.Append("\t public bool " + this.AjaxServerMethod + "() 或者 public bool " + this.AjaxServerMethod + "(string[] XXX) \n");
            SB.Append("\t { \n");
            SB.Append("\t\t \n");
            SB.Append("\t } \n");
            SB.Append("\n");
            if (this.IsNeedForPercentage)
            {
                SB.Append("\t //執行計算進度列百分比的程序 \n");
                SB.Append("\t [AjaxMethod()] \n");
                SB.Append("\t public bool " + this.ClientID + "_StatisticProgress() \n");
                SB.Append("\t { \n");
                SB.Append("\t\t \n");
                SB.Append("\t } \n");
                SB.Append("\n");
            }
            SB.Append("\t */ \n");

            SB.Append("\t var ajaxpro_" + this.ClientID + "; \n");
            SB.Append("\t var activeObj_" + this.ClientID + ";    //促發progressbar的物件 \n");
            SB.Append("\t var " + this.ClientID + "= new ProgressBar(); \n");
            SB.Append("\t var isShowSystemExceptionText_" + this.ClientID + " = " + IsShowSystemExceptionText.ToString().ToLower() + "; //是否顯示Server端或Client端exception訊息\n");
            SB.Append("\t var isShowTrueText_" + this.ClientID + " = " + IsShowTrueText.ToString().ToLower() + "; //是否顯示成功訊息\n");
            SB.Append("\t var isShowFalseText_" + this.ClientID + " = " + IsShowFalseText.ToString().ToLower() + "; //是否顯示失敗訊息\n");
            SB.Append("\t var trueText_" + this.ClientID + " = \"" + TrueText + "\"; //成功訊息\n");
            SB.Append("\t var falseText_" + this.ClientID + " = \"" + FalseText + "\"; //失敗訊息\n");
            SB.Append("\t var timeoutText_" + this.ClientID + " = \"" + TimeOutText + "\"; //工作Timeout訊息\n");
            SB.Append("\t var " + this.ClientID + "_statisticTimer=\"\"; \n");

            SB.Append("\t function " + this.ClientID + "_doWork(disabledObj,params) \n");
            SB.Append("\t { \n");
            SB.Append("\t\t if(disabledObj != null) \n");
            SB.Append("\t\t { \n");
            SB.Append("\t\t\t activeObj_" + this.ClientID + " = disabledObj; \n");
            SB.Append("\t\t\t activeObj_" + this.ClientID + ".disabled = true; \n");
            SB.Append("\t\t } \n");

            SB.Append("\t\t " + this.ClientID + "_startProgressBar('" + this.ClientID + "_divProgressBar'); \n");
            SB.Append("\t\t ajaxpro_" + this.ClientID + " = AjaxPro; \n");
            SB.Append("\t\t ajaxpro_" + this.ClientID + ".timeoutPeriod = " + this.ProgressTimeOut * 1000 + "; \n");
            SB.Append(this.IsShowSystemExceptionText ? "\t\t ajaxpro_" + this.ClientID + ".onError = " + this.ClientID + "_onError; \n" : "");
            SB.Append("\t\t ajaxpro_" + this.ClientID + ".onTimeout = " + this.ClientID + "_onTimeout; \n");
            SB.Append("\t\t if(params != null) \n");
            SB.Append("\t\t { \n");
            SB.Append("\t\t\t " + this.AjaxServerClass + "." + this.AjaxServerMethod + "(params," + this.ClientID + "_doWorkCallBack); \n");
            SB.Append("\t\t } \n");
            SB.Append("\t\t else \n");
            SB.Append("\t\t { \n");
            SB.Append("\t\t\t " + this.AjaxServerClass + "." + this.AjaxServerMethod + "(" + this.ClientID + "_doWorkCallBack); \n");
            SB.Append("\t\t } \n");



            SB.Append(this.IsNeedForPercentage ? "\t\t " + this.ClientID + "_statisticTimer = setTimeout(\"" + this.ClientID + "_statisticProgress()\",100); \n" : "");
            SB.Append("\t } \n");
            SB.Append("\n");

            SB.Append("\t function " + this.ClientID + "_doWorkCallBack(res) \n");
            SB.Append("\t { \n");
            SB.Append("\t\t " + this.ClientID + "_stopProgressBar('" + this.ClientID + "_divProgressBar'); \n");
            SB.Append("\t\t try \n");
            SB.Append("\t\t { \n");
            SB.Append("\t\t\t if(activeObj_" + this.ClientID + " != null) \n");
            SB.Append("\t\t\t { \n");
            SB.Append("\t\t\t\t activeObj_" + this.ClientID + ".disabled = false; \n");
            SB.Append("\t\t\t } \n");
            SB.Append("\t\t } \n");
            SB.Append("\t\t catch(ex) \n");
            SB.Append("\t\t {} \n");
            SB.Append("\t\t if(isShowTrueText_" + this.ClientID + "  && res.value) \n");
            SB.Append("\t\t { \n");
            SB.Append("\t\t\t window.alert(trueText_" + this.ClientID + "); \n");
            SB.Append("\t\t } \n");
            SB.Append("\t\t if(isShowFalseText_" + this.ClientID + "  && res.value == false) \n");
            SB.Append("\t\t { \n");
            SB.Append("\t\t\t window.alert(falseText_" + this.ClientID + "); \n");
            SB.Append("\t\t } \n");
            SB.Append(this.IsNeedForNewWindow ? "\t\t window.close(); \n" : "");
            SB.Append("\t } \n");
            SB.Append("\n");

            SB.Append("\t function " + this.ClientID + "_onError(error) \n");
            SB.Append("\t { \n");
            SB.Append("\t\t " + this.ClientID + "_stopProgressBar('" + this.ClientID + "_divProgressBar'); \n");
            SB.Append("\t\t window.alert(error.Message + \"\\r\\n[ExceptionType:\" + error.Type + \"]\"); \n");
            SB.Append(this.IsNeedForNewWindow ? "\t\t window.close(); \n" : "");
            SB.Append("\t } \n");
            SB.Append("\n");

            SB.Append("\t function " + this.ClientID + "_onTimeout() \n");
            SB.Append("\t { \n");
            SB.Append("\t\t " + this.ClientID + "_stopProgressBar('" + this.ClientID + "_divProgressBar'); \n");
            SB.Append("\t\t if(activeObj_" + this.ClientID + " != null) \n");
            SB.Append("\t\t { \n");
            SB.Append("\t\t\t activeObj_" + this.ClientID + ".disabled = false; \n");
            SB.Append("\t\t } \n");
            SB.Append("\t\t if(timeoutText_" + this.ClientID + " != \"\") \n");
            SB.Append("\t\t { \n");
            SB.Append("\t\t\t window.alert(timeoutText_" + this.ClientID + "); \n");
            SB.Append("\t\t } \n");
            SB.Append(this.IsNeedForNewWindow ? "\t\t window.close(); \n" : "");
            SB.Append("\t } \n");
            SB.Append("\n");

            SB.Append("\t function " + this.ClientID + "_startProgressBar(id) \n");
            SB.Append("\t { \n");
            if (Context.Request.Browser.Type.IndexOf("IE") != -1 && float.Parse(Context.Request.Browser.Version) < 8)
            {
                SB.Append("\t\t document.getElementById(id).style.display = \"inline\"; \n");
            }
            else
            {
                SB.Append("\t\t document.getElementById(id).style.display = \"inline-block\"; \n");
            }
            SB.Append(this.ProgressType == ProgressType.Default ? "\t\t " + this.ClientID + ".update('" + this.ClientID + "'); \n" : "");
            SB.Append("\t } \n");
            SB.Append("\n");

            SB.Append("\t /* \n");
            SB.Append("\t Hides the progress bar and corresponding text \n");
            SB.Append("\t */ \n");
            SB.Append("\t function " + this.ClientID + "_stopProgressBar(id) \n");
            SB.Append("\t { \n");
            SB.Append(this.IsNeedForPercentage ? "\t\t clearTimeout(" + this.ClientID + "_statisticTimer); \n" : "");
            //SB.Append("\t\t var progressTextElement = document.getElementById(\"" + this.ClientID + "_progressText\"); \n");
            SB.Append("\t\t var progressTextElement = document.getElementById(\"" + ProgressLabel.ClientID + "\"); \n");
            SB.Append("\t\t progressTextElement.innerHTML =\"" + ProgressLabel.Text + "\"; \n");
            SB.Append("\t\t document.getElementById(id).style.display = \"none\"; \n");
            SB.Append(this.ProgressType == ProgressType.Default ? "\t\t " + this.ClientID + ".stop('" + this.ClientID + "'); \n" : "");
            SB.Append("\t\t " + this.ClientID + "_statisticTimer=null; \n");
            //SB.Append("\t ajaxpro.queue.count = 0; \n");
            //SB.Append("\t ajaxpro.queue.abort(); \n");
            //SB.Append("\t ajaxpro=null; \n");
            SB.Append("\t } \n");
            SB.Append("\n");

            if (this.IsNeedForPercentage)
            {
                SB.Append("\t function " + this.ClientID + "_statisticProgress() \n");
                SB.Append("\t { \n");
                //SB.Append("\t\t var progressTextElement = document.getElementById(\"" + this.ClientID + "_progressText\"); \n");
                SB.Append("\t\t var progressTextElement = document.getElementById(\"" + ProgressLabel.ClientID + "\"); \n");
                SB.Append("\t\t var i = " + this.AjaxServerClass + "." + this.ClientID + "_StatisticProgress().value; //synchronisch call to retrieve current progress; \n");
                SB.Append("\t\t if (i < 100) \n");
                SB.Append("\t\t { \n");
                SB.Append("\t\t\t progressTextElement.innerHTML =\"" + ProgressLabel.Text + "(\" + i + \"%)\"; \n");
                SB.Append("\t\t\t setTimeout(\"" + this.ClientID + "_statisticProgress()\", " + this.ProgressTimeSpan * 1000 + "); //Retrieve current Progress every second\n");
                SB.Append("\t\t } \n");
                SB.Append("\t\t else \n");
                SB.Append("\t\t { \n");
                SB.Append("\t\t\t progressTextElement.innerHTML =\"" + ProgressLabel.Text + "(100%)\"; \n");
                SB.Append("\t\t } \n");
                SB.Append("\n");
            }
            SB.Append("</script> \n");
            return SB.ToString();
        }
    }
}
