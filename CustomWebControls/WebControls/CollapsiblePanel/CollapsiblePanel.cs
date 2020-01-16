using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace APTemplate
{
    //enum to define the initial state of the panel
    public enum PanelState
    {
        Expanded,
        Collapsed
    }

    //enum to define title/icon placement
    public enum TitleDirection
    {
        Left,
        Right
    }

    /// <summary>
    /// Summary description for WebCustomControl1.
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:CollapsiblePanel runat=server></{0}:CollapsiblePanel>")]
    [Designer("System.Web.UI.Design.ReadWriteControlDesigner, System.Design")]
    [ToolboxBitmap(typeof(CollapsiblePanel), "Resources.CollapsiblePanel.bmp")]
    [PersistChildren(true)]
    [ParseChildren(false)]
    public class CollapsiblePanel : WebControl, IPostBackDataHandler, INamingContainer
    {
        #region Event Declaration
        public event EventHandler PanelStateChanged;
        #endregion Event Declaration

        #region Member Variables
        private Panel divContainer;
        private Color backColor;
        private Color foreColor;
        private string collapseImage;
        private string expandImage;

        #endregion Member Variables

        #region Constants
        private const string C_EMPTY_LINK = "#";
        private const string C_COLLAPSIBLE_SCRIPT = "<script language=\"JavaScript\">\n" +
            "<!--\n" +
            "function ExpandCollapse(container, collapse, expand)\n" +
            "{\n\t" +
            "var obj = document.getElementById(container + '_collapsible');\n\t" +
            "var fld = document.getElementById(container + '_state');\n\t" +
            "var img = document.getElementById(container + '_button');\n\t" +
            "if (obj!=null)\n\t" +
            "{\n\t\t" +
            "var p = obj.style\n\t\t" +
            "obj.style.display = (obj.style.display==\"block\") ? \"''\" : \"block\";\n\t\t" +
            "fld.value = obj.style.display;\n\t\t" +
            "img.src = (fld.value==\"block\")  ? collapse : expand;\n\t" +
            "}\n}\n// -->\n</script>";

        private const string C_DRAGGABLE_REGISTRATION = "<script language=\"JavaScript\">\n" +
          "function draggableRegistrationFor{0}()\n" +
          "{{\n" +
            "  var group;\n" +
            "  var coordinates = ToolMan.coordinates();\n" +
            "  var drag = ToolMan.drag()\n" +
            "  var boxDrag = document.getElementById(\"{0}\");\n" +
            "  drag.createSimpleGroup(boxDrag);\n" +
          "}}\n" +
          "addLoadEvent(draggableRegistrationFor{0});\n" +
          "</script>";

        private const string addLoadEvent = "<script language=\"JavaScript\">\n" +
        "<!--\n" +
        "function addLoadEvent(func)\n" +
        "{\n" +
        "  var oldonload = window.onload;\n" +
        "  if (typeof window.onload != 'function')\n" +
        "  {\n" +
        "    window.onload = func;\n" +
        "  }\n" +
        "  else\n" +
        "  {\n" +
        "    window.onload = function()\n" +
        "    {\n" +
        "      oldonload();\n" +
        "      func();\n" +
        "    }\n" +
        "  }\n" +
        "}\n" +
        "// -->\n</script>";
        #endregion Constants

        #region Public Properties & Properties

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        public string Text
        {
            get
            {
                object o = ViewState["text"];
                if (o != null)
                    return o.ToString();
                else
                    return string.Empty;
            }

            set
            {
                ViewState["text"] = value;
            }
        }


        [Bindable(true)]
        [Category("Title Appearance")]
        [DefaultValue("")]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        public string CollapseImage
        {
            get
            {
                return collapseImage;
            }

            set
            {
                collapseImage = value;
            }
        }


        [Bindable(true)]
        [Category("Title Appearance")]
        [DefaultValue("")]
        [Editor(typeof(ImageUrlEditor), typeof(UITypeEditor))]
        public string ExpandImage
        {
            get
            {
                return expandImage;
            }
            set
            {
                expandImage = value;
            }
        }

        [Bindable(true)]
        [Category("Title Appearance")]
        [DefaultValue("")]
        public Color TitleBackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        [Bindable(true)]
        [Category("Title Appearance")]
        [DefaultValue("")]
        public Color TitleForeColor
        {
            get { return foreColor; }
            set { foreColor = value; }
        }


        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(PanelState.Expanded)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public PanelState State
        {
            get
            {
                object o = ViewState["initialState"];
                return (o != null) ? (PanelState)o : PanelState.Expanded;
            }
            set
            {
                ViewState["initialState"] = value;

            }
        }


        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Allows collapsing/expanding the panel by clicking on the title")]
        public bool TitleClickable
        {
            get
            {
                //				return titleClickable;
                object o = ViewState["titleClickable"];
                return (o != null) ? Convert.ToBoolean(o) : false;
            }
            set
            {
                ViewState["titleClickable"] = value;
            }
        }


        [Bindable(true)]
        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Make the panel draggable using scripts provided at http://tool-man.org/. Work only in IE6, Firefox 1.0, and Safari 1.3. Check http://tool-man.org/ for updated scripts")]
        public bool Draggable
        {
            get
            {
                object o = ViewState["draggable"];
                return (o != null) ? Convert.ToBoolean(o) : false;
            }
            set
            {
                ViewState["draggable"] = value;
            }
        }

        [Bindable(true)]
        [Category("Title Appearance")]
        [DefaultValue("")]
        [Description("Stylesheet class to apply for the title")]
        public string TitleCSS
        {
            get
            {
                object o = ViewState["titleCSS"];
                return (o != null) ? o.ToString() : string.Empty;
            }
            set
            {
                ViewState["titleCSS"] = value;
            }
        }


        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Description("Stylesheet class to apply for the container area")]
        public string PanelCSS
        {
            get
            {
                object o = ViewState["panelCSS"];
                return (o != null) ? o.ToString() : string.Empty;
            }
            set
            {
                ViewState["panelCSS"] = value;
            }
        }


        [Bindable(true)]
        [Category("Title Appearance")]
        [Description("Alignment of the title text")]
        [DefaultValue(HorizontalAlign.Left)]
        [PersistenceMode(PersistenceMode.Attribute)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public HorizontalAlign TitleAlign
        {
            get
            {
                object o = ViewState["titleAlign"];
                if (o != null)
                {
                    return (HorizontalAlign)o;
                }

                return HorizontalAlign.Left;
            }
            set
            {
                ViewState["titleAlign"] = value;
            }
        }


        [Bindable(true)]
        [Category("Title Appearance")]
        [Description("Controls whether title and image is on the right or left hand side of the panel")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [DefaultValue(TitleDirection.Left)]
        [PersistenceMode(PersistenceMode.Attribute)]
        public TitleDirection Direction
        {
            get
            {
                object o = ViewState["direction"];
                if (o != null)
                {
                    return (TitleDirection)o;
                }

                return TitleDirection.Left;
            }
            set
            {
                ViewState["direction"] = value;
            }
        }

        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            PanelState current = this.State;
            string temp = postCollection[postDataKey + ":state"];

            if (temp != null)
                this.State = (temp == "none") ? PanelState.Collapsed : PanelState.Expanded;

            if (current != this.State)
                return true; //panel's state has changed
            else
                return false;//panel's state has not changed
        }

        #region IPostBackDataHandler Members

        public void RaisePostDataChangedEvent()
        {
            //Get's called IF LoadPostData(...) returns true (panel's state changed)
            if (PanelStateChanged != null)
            {
                //raise our event notifying the state change
                PanelStateChanged(this, EventArgs.Empty);
            }
        }

        #endregion

        #endregion

        #region Protected Properties & Methods

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.Style.Clear();

            divContainer = new Panel();
            divContainer.ID = "container";

            Page.RegisterRequiresPostBack(this);
        }

        protected override void RenderChildren(HtmlTextWriter output)
        {
            if (HasControls())
            {
                StringWriter sw = new StringWriter();
                HtmlTextWriter htx = new HtmlTextWriter(sw);

                //Render our outermost div seperately
                //container.RenderControl(htx);
                divContainer.RenderControl(htx);

                string containerData = sw.ToString();

                sw = new StringWriter();
                htx = new HtmlTextWriter(sw);

                foreach (Control ctrl in Controls)
                {
                    //Render all children except the outermost table that we
                    //already rendered
                    if (ctrl.ClientID != (this.ClientID + "_container"))
                        ctrl.RenderControl(htx);
                }

                //insert the rendered user data (controls and things) user
                //have dragged and dropped at design time to the correct
                //location in the output text. This checks for the placeholder {0}
                //we created at CreateContainerControls()

                output.Write(containerData, sw.ToString());

                //I must admit that what I did here is not the best approach. 
                //There should be a much more intuitive way to do this
                //Let me know if there is so I can learn.
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            //Create the outermost table and add it to controls collection
            Controls.Add(CreateContainerControls());

            //Prepare a hidden field (used by our javascript)
            HtmlInputHidden hiddenField = new HtmlInputHidden();
            hiddenField.ID = "state";
            hiddenField.Value = (this.State == PanelState.Collapsed) ? "none" : "block";

            Controls.Add(hiddenField);

            //Call to render all the children controls
            RenderChildren(output);
        }

        protected override void OnPreRender(EventArgs e)
        {
            //Output the javascript if it is needed
            if (!Page.IsClientScriptBlockRegistered("ExpandCollapse") && (this.Enabled))
            {
                Page.RegisterClientScriptBlock("ExpandCollapse", C_COLLAPSIBLE_SCRIPT);
            }

            if (!Page.IsStartupScriptRegistered("addLoadEvent") && (this.Draggable))
                Page.RegisterStartupScript("addLoadEvent", addLoadEvent);

            if (!Page.IsStartupScriptRegistered("dragInit" + "_" + this.ClientID) && (this.Draggable))
                Page.RegisterStartupScript("dragInit" + "_" + this.ClientID, String.Format(C_DRAGGABLE_REGISTRATION, this.ClientID + "_container"));

            if (this.Enabled)
                RegisterJavaScript.RegisterCollapsiblePanelScript(Page);
        }

        #endregion

        #region Private Properties & Methods

        private Control CreateTitle()
        {
            Label header = new Label();

            header.Text = this.Text;
            header.Width = Unit.Percentage(100);

            //If there is a CSS defined, it will be used
            //instead of the inline styles
            if (this.TitleCSS != string.Empty)
                header.CssClass = this.TitleCSS;
            else
            {
                header.ForeColor = this.TitleForeColor;
                header.BackColor = this.TitleBackColor;
            }

            //if title is clickable, wrap it around a href with
            //an onClick even to call the javascript
            if (this.TitleClickable)
            {
                HtmlAnchor ha = new HtmlAnchor();
                ha.HRef = C_EMPTY_LINK;
                ha.Controls.Add(header);

                //Only allow expanding/collapsing if control is enabled
                ha.Attributes.Add("onClick", GetOnClickScript());

                return ha;
            }
            else
                return header; //if title is not clickable just pass title in a label element


        }

        private TableRow CreateHeaderControls()
        {
            HtmlImage controlButton = new HtmlImage();
            HtmlAnchor anchor = new HtmlAnchor();
            Table headerTable = new Table();
            TableRow headerRow = new TableRow();
            TableCell titleCell = new TableCell();
            TableCell controlCell = new TableCell();

            headerTable.Width = this.Width;
            headerTable.CellPadding = 0;
            headerTable.CellSpacing = 0;

            headerRow.BackColor = this.TitleBackColor;

            controlButton.ID = "button";
            controlButton.Src = GetImage();
            controlButton.Border = 0;

            //Only allow expanding/collapsing if control is enabled
            controlButton.Attributes.Add("onClick", GetOnClickScript());

            anchor.HRef = C_EMPTY_LINK;
            anchor.Controls.Add(controlButton);

            titleCell.Width = Unit.Percentage(100);
            titleCell.HorizontalAlign = this.TitleAlign;
            titleCell.Controls.Add(CreateTitle());

            controlCell.Controls.Add(anchor);

            //if direction is left (Default) have the text on the right side
            //else image on the right side
            if (this.Direction == TitleDirection.Left)
                headerRow.Cells.AddRange(new TableCell[] { titleCell, controlCell });
            else
                headerRow.Cells.AddRange(new TableCell[] { controlCell, titleCell });

            return headerRow;
        }

        //private Table CreateContainerControls()
        private Panel CreateContainerControls()
        {
            Table layoutTable = new Table();
            layoutTable.Width = this.Width;

            //use default styles if no style class is applied
            if (this.PanelCSS == string.Empty)
            {
                layoutTable.BorderWidth = Unit.Pixel(1);
                layoutTable.CellPadding = 0;
                layoutTable.CellSpacing = 0;
            }
            else
                layoutTable.CssClass = this.PanelCSS;

            TableRow controlHolderRow = new TableRow();
            TableRow contentHolderRow = new TableRow();

            //name of the row which we manipulate at client side to
            //mimic the expand/collapse behaviour
            contentHolderRow.ID = "collapsible";

            //set the row's display style to block or none depending on
            //the selected initial state of the panel
            contentHolderRow.Style.Add("display", (this.State == PanelState.Collapsed) ? "none" : "block");

            TableCell controlHolderCell = new TableCell();
            TableCell contentHolderCell = new TableCell();

            contentHolderCell.ColumnSpan = 2;

            //users contents added at design time will be rendered into 
            //this location
            LiteralControl userArea = new LiteralControl();
            userArea.Text = "{0}";

            contentHolderCell.Controls.Add(userArea);

            controlHolderRow.Cells.Add(controlHolderCell);
            contentHolderRow.Cells.Add(contentHolderCell);

            layoutTable.Rows.AddRange(new TableRow[] { CreateHeaderControls(), contentHolderRow });

            divContainer.Controls.Add(layoutTable);

            return divContainer;
        }

        private string GetOnClickScript()
        {
            if (this.Enabled)
            {
                this.collapseImage = this.collapseImage != null && this.collapseImage.StartsWith("~/") ? this.collapseImage.Substring(2) : "";
                this.expandImage = this.expandImage != null && this.expandImage.StartsWith("~/") ? this.expandImage.Substring(2) : "";
                return String.Format("ExpandCollapse('{0}','{1}','{2}')", this.ClientID, this.collapseImage, this.expandImage);
            }
            else
                return "return false;";
        }

        private string GetImage()
        {
            return (this.State == PanelState.Collapsed) ? this.expandImage : this.collapseImage;
        }

        #endregion

    }
}
