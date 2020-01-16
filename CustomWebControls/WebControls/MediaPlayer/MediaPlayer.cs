using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Design;
using System.Web.UI.Design;
using System.ComponentModel.Design;
using System.Drawing;

namespace APTemplate
{
    /// <summary>
    /// 自定義的Windows Media Player媒體播放控制項。
    /// </summary>
    [ToolboxData("<{0}:MediaPlayer runat=server></{0}:MediaPlayer>")]
    [ToolboxBitmap(typeof(MediaPlayer), "Resources.Control_MediaPlayer.bmp")]
    public class MediaPlayer : ActiveX
    {
        public enum EUIMode
        {
            NotSet = 0,
            Invisible = 1,
            None = 2,
            Mini = 3,
            Full = 4,
        }

        String FUrl = null;
        Boolean FAutoStart = true;
        EUIMode FUIMode = EUIMode.NotSet;

        #region Public Properties & Methods

        public MediaPlayer()
        {
            base.ClassId = "6BF52A52-394A-11D3-B153-00C04F79FAA6";
        }

        /// <summary>
        ///播放檔案來源。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("播放檔案來源。")]
        [Bindable(true), Editor(typeof(UrlEditor), typeof(UITypeEditor)), UrlProperty()]
        public string Url
        {
            get { return FUrl; }
            set { FUrl = value; }
        }

        /// <summary>
        ///是否自動播放。
        /// </summary>
        [DefaultValue(true),
         Category("自訂"),
         Description("是否自動播放。")]
        public Boolean AutoStart
        {
            get { return FAutoStart; }
            set { FAutoStart = value; }
        }

        /// <summary>
        ///顯示界面模式。
        /// </summary>
        [DefaultValue(typeof(EUIMode), "NotSet"),
         Category("自訂"),
         Description("顯示界面模式。")]
        public EUIMode UIMode
        {
            get { return FUIMode; }
            set { FUIMode = value; }
        }

        /// <summary>
        ///是否自動播放。
        /// </summary>
        [PersistenceMode(PersistenceMode.InnerProperty)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public override List<ActiveXParam> Params
        {
            get { return base.Params; }
        }

        #endregion

        #region Protected Properties & Methods

        protected override void CreateChildControls()
        {
            if (this.DesignMode)
            {
                AddParam();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            AddParam();
        }

        #endregion

        #region Private Properties & Methods

        private void AddParam()
        {
            if (this.Url != null)
                AddParam("URL", this.ResolveClientUrl(this.Url));
                AddParam("autoStart", this.AutoStart.ToString());
            if (this.UIMode != EUIMode.NotSet)
                AddParam("uiMode", this.UIMode.ToString());
            base.CreateChildControls();
        }

        private void AddParam(string Name, string Value)
        {
            try
            {
                ActiveXParam oParam;
                oParam = new ActiveXParam(Name, Value);
                //ActiveXParam DeletedParamObj = base.Params.Find(e => { return e.Name == oParam.Name; });
                ActiveXParam DeletedParamObj = base.Params.Find(new SearchFunctor(oParam).SearchPredicate);
                base.Params.Remove(DeletedParamObj);
                base.Params.Add(oParam);
            }
            catch (Exception ex)
            {

            }

        }

        #endregion
    }
}