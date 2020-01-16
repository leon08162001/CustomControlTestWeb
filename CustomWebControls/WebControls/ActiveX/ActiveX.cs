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
    /// 自定義的ActiveX控制項。
    /// </summary>
    [ToolboxData("<{0}:ActiveX runat=server></{0}:ActiveX>")]
    [ToolboxBitmap(typeof(ActiveX), "Resources.Control_ActiveX.bmp")]
    public class ActiveX : CompositeControl, INamingContainer
    {
        string FClassId = "";
        string FCodeBase = "";
        protected List<ActiveXParam> FParams = new List<ActiveXParam>();

        /// <summary>
        ///ActiveX 控制項的 ClassId。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("ActiveX 控制項的 ClassId。")]
        public string ClassId
        {
            get { return FClassId; }
            set { FClassId = value; }
        }

        /// <summary>
        ///程式下載位址。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("程式下載位址")]
        public string CodeBase
        {
            get { return FCodeBase; }
            set { FCodeBase = value; }
        }

        /// <summary>
        ///ActiveX 控制項參數集合。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("ActiveX 控制項參數集合。")]
        [PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
        public virtual List<ActiveXParam> Params
        {
            get { return FParams; }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                //return HtmlTextWriterTag.Object;
              return HtmlTextWriterTag.Div;
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
          
          //writer.AddAttribute("style", "display: inline;");
          writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "inline");
          writer.AddStyleAttribute(HtmlTextWriterStyle.Width, base.Width.Value.ToString() + "px");
          writer.AddStyleAttribute(HtmlTextWriterStyle.Height, base.Height.Value.ToString() + "px");

          //加入 ActiveX 元件的 classid
          writer.AddAttribute("classid", String.Format("clsid:{0}", ClassId));
          //加入 ActiveX 元件的 CodeBase
          if (CodeBase != null)
          {
            writer.AddAttribute("codebase", CodeBase);
          }
          writer.RenderBeginTag(HtmlTextWriterTag.Object);
          base.RenderContents(writer);
        }

        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            writer.AddAttribute("style", "display: inline; z-index: -99999;");
            //加入 ActiveX 元件的 classid
            //writer.AddAttribute("classid", String.Format("clsid:{0}", ClassId));
            //加入 ActiveX 元件的 CodeBase
            //if (CodeBase != null)
            //{
            //    writer.AddAttribute("codebase", CodeBase);
            //}
            base.AddAttributesToRender(writer);
        }

        private void AddParam(string Name, string Value)
        {
            HtmlGenericControl oParam;
            oParam = new HtmlGenericControl("param");
            oParam.Attributes.Add("name", Name);
            oParam.Attributes.Add("value", Value);
            this.Controls.Add(oParam);
        }

        protected override void CreateChildControls()
        {
            foreach (ActiveXParam oParam in Params)
            {
                AddParam(oParam.Name, oParam.Value);
            }
            base.CreateChildControls();
        }
    }

    public class SearchFunctor
    {
      protected ActiveXParam target = null;

      public SearchFunctor(ActiveXParam target)
      {
        this.target = target;
      }

      public bool SearchPredicate(ActiveXParam Acx)
      {
        return Acx.Name == target.Name;
      }
    }
}