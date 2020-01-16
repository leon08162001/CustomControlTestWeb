using System;
using System.Configuration;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Design;

namespace APTemplate
{
    /// <summary>
    /// 自定義的Flash媒體播放控制項。
    /// </summary>
    [ToolboxData("<{0}:Flash runat=server></{0}:Flash>")]
    [ToolboxBitmap(typeof(Flash), "Resources.Control_Flash.bmp")]
    public class Flash : ActiveX
    {
        public enum EQuality
        {
            NotSet = 0,
            Low = 1,
            High = 2,
            Autolow = 3,
            Autohigh = 4,
            Best = 5
        }

        public enum EAllowScriptAccess
        {
            Always = 0,
            Never = 1
        }

        private string FMovieUrl = null;
        private EQuality FQuality = EQuality.NotSet;
        private bool _AllowFullScreen = true;
        private EAllowScriptAccess _AllowScriptAccess = EAllowScriptAccess.Always;

        #region Public Properties & Methods

        public Flash()
        {
            base.ClassId = "D27CDB6E-AE6D-11CF-96B8-444553540000";
            base.CodeBase = "http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0";
        }

        /// <summary>
        ///Flash 控制項參數集合。
        /// </summary>
        [DefaultValue(""),
        Category("自訂"),
        Description("Flash 控制項參數集合。")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public override List<ActiveXParam> Params
        {
            get { return base.Params; }
        }

        /// <summary>
        ///影音品質。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("影音品質。")]
        public EQuality Quality
        {
            get { return FQuality; }
            set { FQuality = value; }
        }

        /// <summary>
        ///是否全螢幕。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("是否全螢幕。")]
        public bool AllowFullScreen
        {
            get { return _AllowFullScreen; }
            set { _AllowFullScreen = value; }
        }

        /// <summary>
        ///是否允許存取跨網域的javascript資源。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("是否允許存取跨網域的javascript資源。")]
        public EAllowScriptAccess AllowScriptAccess
        {
            get { return _AllowScriptAccess; }
            set { _AllowScriptAccess = value; }
        }

        /// <summary>
        ///Flash 檔案來源。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("Flash 檔案來源。"), UrlProperty()]
        public string MovieUrl
        {
            get { return FMovieUrl; }
            set { FMovieUrl = value; }
        }

        #endregion

        #region Protected Properties & Methods

        protected override void CreateChildControls()
        {
            HtmlGenericControl oEmbed;
            AddParam("movie", this.MovieUrl != null && this.MovieUrl != "" ? ResolveClientUrl(this.MovieUrl) : "");
            if (Quality != EQuality.NotSet)
            {
                AddParam("quality", Quality.ToString().ToLower());
            }
            AddParam("allowFullScreen", AllowFullScreen.ToString().ToLower());
            AddParam("allowscriptaccess", AllowScriptAccess.ToString().ToLower());
            base.CreateChildControls();
            oEmbed = CreateEmbed();
            this.Controls.Add(oEmbed);
        }
        #endregion

        #region Private Properties & Methods

        private void AddParam(string Name, string Value)
        {
            ActiveXParam oParam;
            oParam = new ActiveXParam(Name, Value);
            //ActiveXParam DeletedParamObj = base.Params.Find(e => { return e.Name == oParam.Name; });
            ActiveXParam DeletedParamObj = base.Params.Find(new SearchFunctor(oParam).SearchPredicate);
            FParams.Remove(DeletedParamObj);
            FParams.Add(oParam);
        }

        private HtmlGenericControl CreateEmbed()
        {
            HtmlGenericControl oEmbed;
            oEmbed = new HtmlGenericControl();
            oEmbed.TagName = "embed";
            oEmbed.Attributes["src"] = this.MovieUrl != null && this.MovieUrl != "" ? this.ResolveClientUrl(MovieUrl) : "";
            oEmbed.Attributes["pluginspage"] = "http://www.macromedia.com/go/getflashplayer";
            oEmbed.Attributes["type"] = "application/x-shockwave-flash";
            oEmbed.Attributes["width"] = Width.ToString();
            oEmbed.Attributes["height"] = Height.ToString();
            foreach (ActiveXParam oParam in Params)
            {
                if (oParam.Name != "movie")
                {
                    oEmbed.Attributes[oParam.Name] = oParam.Value;
                }
            }
            return oEmbed;
        }

        #endregion

    }
}