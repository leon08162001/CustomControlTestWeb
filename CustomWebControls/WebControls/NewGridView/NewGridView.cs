using System;
using System.Globalization;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Drawing.Design;
using Microsoft.VisualBasic;
using System.Drawing;
using System.Collections;

namespace APTemplate
{
    /// <summary>
    /// 列舉-新增資料列的位置。
    /// </summary>
    public enum NewRowPosition
    {
        /// <summary>
        /// 置頂
        /// </summary>
        Top = 0,
        /// <summary>
        /// 置底
        /// </summary>
        Bottom = 1
    }

    /// <summary>
    /// Advanced pager function GridView
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item><description>開發人員可透過 VS 2005 / 2008，自訂「單數列、雙數列、滑鼠置於其上的資料列」的背景顏色。</description></item>
    /// <item><description>加入無資料列時是否顯示標題欄位功能。</description></item>
    /// <item><description>目前水平對齊方式一律為「靠右」，垂直對齊方式一律為「靠上」，字體大小固定為小型字，未開發給開發人員修改。</description></item>
    /// </list>
    /// </remarks>
    [ToolboxData("<{0}:NewGridView runat=server></{0}:NewGridView>"),
     ToolboxBitmap(typeof(GridView)),
     DefaultProperty("MouseOverBackColor")]
    public class NewGridView : System.Web.UI.WebControls.GridView
    {
        private GridViewRowCollection _newRows;
        private List<int> _changedRows;
        private System.Drawing.Color FOddRowBackColor = System.Drawing.Color.Empty;         // 單數列的背景顏色
        private System.Drawing.Color FEvenRowBackColor = System.Drawing.Color.Empty;        // 雙數列的背景顏色
        private System.Drawing.Color FMouseOverBackColor = System.Drawing.Color.Empty;      // 滑鼠移至資料列上時的背景顏色
        private System.Drawing.Color FMouseOverCellBackColor = System.Drawing.Color.Empty;      // 滑鼠移至資料列上每一欄時的背景顏色
        private NewRowPosition _NewRowPosition = NewRowPosition.Bottom;
        // 字體大小
        private FontUnit _fontSize = FontUnit.Empty;

        //無資料時gridview是否顯示標題列
        Boolean FEmptyShowHeader = false;

        GridViewRow _FooterRow;

        #region Public Properties & Methods

        /// <summary>
        /// 單數列的背景顏色。
        /// </summary>
        [Category("自訂"),
         Description("單數列的背景顏色。"),
         Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public System.Drawing.Color OddRowBackColor
        {
            get
            {
                return FOddRowBackColor;
            }
            set
            {
                FOddRowBackColor = value;
            }
        }

        /// <summary>
        /// 雙數列的背景顏色。
        /// </summary>
        [Category("自訂"),
         Description("雙數列的背景顏色。"),
         Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public System.Drawing.Color EvenRowBackColor
        {
            get
            {
                return FEvenRowBackColor;
            }
            set
            {
                FEvenRowBackColor = value;
            }
        }

        /// <summary>
        /// 使用者滑鼠置於其上時的資料列的背景顏色。
        /// </summary>
        [Category("自訂"),
         Description("使用者滑鼠置於其上時的資料列的背景顏色。"),
         Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public System.Drawing.Color MouseOverBackColor
        {
            get
            {
                return FMouseOverBackColor;
            }
            set
            {
                FMouseOverBackColor = value;
            }
        }

        /// <summary>
        /// 使用者滑鼠置於其上時的資料欄位的背景顏色。
        /// </summary>
        [Category("自訂"),
         Description("使用者滑鼠置於其上時的資料欄位的背景顏色。"),
         Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public System.Drawing.Color MouseOverCellBackColor
        {
            get
            {
                return FMouseOverCellBackColor;
            }
            set
            {
                FMouseOverCellBackColor = value;
            }
        }

        /// <summary>
        /// 整個 WizardGridView 控制項，包括「頁碼列」裡的字體大小 (預設為 Small)。
        /// </summary>
        [System.ComponentModel.Category("自訂"),
        System.ComponentModel.Description("整個 WizardGridView 控制項，包括「頁碼列」裡的字體大小 (預設為 Small)。")]
        public FontUnit CustomFontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                _fontSize = value;
            }
        }

        ///<summary>   
        ///無資料時是否顯示欄位標題。   
        ///</summary>
        [Category("自訂"),
         Description("無資料時是否顯示欄位標題。")]
        public Boolean EmptyShowHeader
        {
            get
            {
                return FEmptyShowHeader;
            }
            set
            {
                FEmptyShowHeader = value;
            }
        }

        /// <summary>
        /// 新增資料列在gridview中的位置。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
        Description("新增資料列在gridview中的位置。")]
        public NewRowPosition NewRowPosition
        {
            get { return _NewRowPosition; }
            set { _NewRowPosition = value; }
        }

        public override GridViewRow FooterRow
        {
            get
            {
                return _FooterRow == null ? base.FooterRow : _FooterRow;
            }
        }

        [Browsable(false)]
        public GridViewRowCollection NewRows
        {
            get
            {
                return (this._newRows == null) ? new
                        GridViewRowCollection(new ArrayList()) :
                        this._newRows;
            }
        }

        ///<summary>   
        ///新增資料列的筆數。   
        ///</summary>
        [Category("自訂"),
        Description("新增資料列的筆數。")]
        public int NewRowCount
        {
            get
            {
                object viewState = this.ViewState["NewRowCount"];
                return (viewState == null) ? 0 : (int)viewState;
            }
            set 
            {
                this.ViewState["NewRowCount"] = value;
            }
        }

        [Browsable(false)]
        public GridViewRowCollection NewRowsChanged
        {
            get
            {
                if (this._changedRows != null)
                {
                    ArrayList changedRows = new ArrayList();

                    foreach (int rowIndex in this._changedRows)
                    {
                        changedRows.Add(this._newRows[rowIndex]);
                    }
                    return new GridViewRowCollection(changedRows);
                }
                return new GridViewRowCollection(new ArrayList());
            }
        }

        #endregion

        #region Protected Properties & Methods

        protected override void OnLoad(EventArgs e)
        {
            this.CreateRows();
            base.OnLoad(e);
        }

        ///<summary>
        ///建立子控制項。
        ///</summary>
        ///<param name="dataSource">控制項的資料來源。</param> 
        ///<param name="dataBinding">true 指示子控制項繫結至資料，否則為 false。</param>
        ///<returns>建立的資料列數目。</returns>
        protected override int CreateChildControls(System.Collections.IEnumerable dataSource, bool dataBinding)
        {
            int iRowCount = 0;
            iRowCount = base.CreateChildControls(dataSource, dataBinding);
            if ((this.EmptyShowHeader && iRowCount == 0))
            {
                    CreateEmptyTable();
            }
            return iRowCount;
        }

        protected override void OnRowDataBound(System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            base.OnRowDataBound(e);
            if (this.SkinID != "" || this.CssClass != "")
                return;
            // 單數列、雙數列、滑鼠移至資料列上時，的預設背景顏色。
            // 若開發人員有 GridView 的「屬性」視窗去設定自訂顏色，則以開發人員的設定為準，否則以下列的預設值為準
            if (OddRowBackColor == System.Drawing.Color.Empty)
                OddRowBackColor = System.Drawing.Color.FromName("#FFFFFF");        // 白色
            if (EvenRowBackColor == System.Drawing.Color.Empty)
                EvenRowBackColor = System.Drawing.Color.FromName("#FFFFFF");       // 白色            
            if (MouseOverBackColor == System.Drawing.Color.Empty)
                MouseOverBackColor = System.Drawing.Color.FromName("#FFFFFF");     // 白色


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex % 2 == 0)    // 單數列
                {
                    if (FOddRowBackColor != System.Drawing.Color.Empty)
                    {
                        e.Row.Style[HtmlTextWriterStyle.BackgroundColor] = ColorTranslator.ToHtml(FOddRowBackColor);
                    }

                    if (FMouseOverBackColor != System.Drawing.Color.Empty)
                    {
                        // 滑鼠移至資料列上的顏色
                        e.Row.Attributes["onmouseover"] += "GDMouseChgBkColor(this,'" + System.Drawing.ColorTranslator.ToHtml(FMouseOverBackColor) + "');";
                        // 滑鼠離開資料列上的顏色
                        e.Row.Attributes["onmouseout"] += "GDMouseChgBkColor(this,'" + System.Drawing.ColorTranslator.ToHtml(FOddRowBackColor) + "');";
                    }

                    if (FMouseOverCellBackColor != System.Drawing.Color.Empty)
                    {
                        for (int i = 0; i < e.Row.Cells.Count; i++)
                        {
                            // 滑鼠移至資料欄上的顏色
                            e.Row.Cells[i].Attributes["onmouseover"] += "GDMouseChgBkColor(this,'" + System.Drawing.ColorTranslator.ToHtml(FMouseOverCellBackColor) + "');";
                            // 滑鼠離開資料欄上的顏色
                            e.Row.Cells[i].Attributes["onmouseout"] += "GDMouseChgBkColor(this,'');";
                        }
                    }
                }
                else    // 雙數列
                {
                    if (FEvenRowBackColor != System.Drawing.Color.Empty)
                    {
                        e.Row.Style[HtmlTextWriterStyle.BackgroundColor] = ColorTranslator.ToHtml(FEvenRowBackColor);
                    }

                    if (FMouseOverBackColor != System.Drawing.Color.Empty)
                    {
                        // 滑鼠移至資料列上的顏色
                        e.Row.Attributes["onmouseover"] += "GDMouseChgBkColor(this,'" + System.Drawing.ColorTranslator.ToHtml(FMouseOverBackColor) + "');";
                        // 滑鼠離開資料列上的顏色
                        e.Row.Attributes["onmouseout"] += "GDMouseChgBkColor(this,'" + System.Drawing.ColorTranslator.ToHtml(FEvenRowBackColor) + "');";
                    }

                    if (FMouseOverCellBackColor != System.Drawing.Color.Empty)
                    {
                        for (int i = 0; i < e.Row.Cells.Count; i++)
                        {
                            // 滑鼠移至資料欄上的顏色
                            e.Row.Cells[i].Attributes["onmouseover"] += "GDMouseChgBkColor(this,'" + System.Drawing.ColorTranslator.ToHtml(FMouseOverCellBackColor) + "');";
                            // 滑鼠離開資料欄上的顏色
                            e.Row.Cells[i].Attributes["onmouseout"] += "GDMouseChgBkColor(this,'');";
                        }
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.Pager)
            {
                e.Row.BackColor = System.Drawing.Color.White;
                e.Row.HorizontalAlign = HorizontalAlign.Right;

            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            //this.CreateRows();
            // 2008/09/17 新增，設定整個 WizardGridView 控件的字體大小            
            if (this.CustomFontSize == FontUnit.Empty)
            {
                this.CustomFontSize = FontUnit.Small;
                this.Font.Size = CustomFontSize;
            }
            else
            {
                this.Font.Size = CustomFontSize;
            }

            if (FooterRow != null)
                FooterRow.Visible = this.ShowFooter;

            RegisterJavaScript.RegisterContolIncludeScript(Page);

            base.OnPreRender(e);
        }

        #endregion

        #region Private Properties & Methods

        ///<summary>   
        ///建立無資料只顯示標題的表格。   
        ///</summary>
        private void CreateEmptyTable()
        {
            Table oTable;
            int iCount = 0;
            GridViewRowEventArgs e;
            DataControlField[] oFields;

            oTable = new Table();
            if (this.Columns.Count > 0)
            {
                iCount = this.Columns.Count;
                oFields = new DataControlField[iCount];
                this.Columns.CopyTo(oFields, 0);	//取得目前定義 Columns 複本
            }
            else
            {
                if (this.DataSource.GetType() == typeof(System.Data.DataTable))
                    iCount = (this.DataSource as System.Data.DataTable).Columns.Count;

                oFields = new DataControlField[iCount];
                for (int i = 0; i < iCount; i++)
                {
                    oFields[i] = new BoundField();
                    oFields[i].HeaderText = (this.DataSource as System.Data.DataTable).Columns[i].ColumnName;
                }
            }

            //建立Header列  
            this.Controls.Clear();
            GridViewRow HeaderRow = base.CreateRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal);
            this.InitializeRow(HeaderRow, oFields);	//資料列初始化
            oTable.Rows.Add(HeaderRow);
            this.Controls.AddAt(0, oTable);
            e = new GridViewRowEventArgs(HeaderRow);
            this.OnRowCreated(e);	//引發 RowCreated 事件
            oTable = (Table)this.Controls[0];

            //建立空白的資料列
            GridViewRow EmptyRow = new GridViewRow(-1, -1, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);
            this.InitializeRow(EmptyRow, oFields);
            oTable.Rows.Add(EmptyRow);

            //建立Footer資料列
            _FooterRow = base.CreateRow(-1, -1, DataControlRowType.Footer, DataControlRowState.Insert);
            this.InitializeRow(_FooterRow, oFields);
            _FooterRow.Visible = this.ShowFooter;
            oTable.Rows.Add(_FooterRow);

            this.Controls.Clear();
            this.Controls.Add(oTable);
        }

        private DataControlField[] GetDataControlFields()
        {
            DataControlField[] fields =
                new DataControlField[this.Columns.Count];
            base.Columns.CopyTo(fields, 0);
            return fields;
        }

        private GridViewRow CreateNewRow(int rowIndex, DataControlField[] fields)
        {
            GridViewRow newRow = base.CreateRow(rowIndex, -1,
                                 DataControlRowType.DataRow,
                                 DataControlRowState.Insert);
            base.InitializeRow(newRow, fields);
            this.AddRowChanged(newRow);
            return newRow;
        }

        private void AddRowChanged(Control control)
        {
            foreach (Control ctr in control.Controls)
            {
                if (ctr is TextBox)
                {
                    ((TextBox)ctr).TextChanged +=
                       new EventHandler(this.RowChanged);
                }
                else if (ctr is ListControl)
                {
                    ((ListControl)ctr).SelectedIndexChanged +=
                            new EventHandler(this.RowChanged);
                }
                else if (ctr is CheckBox)
                {
                    ((CheckBox)ctr).CheckedChanged +=
                      new EventHandler(this.RowChanged);
                }

                if (ctr.HasControls())
                {
                    this.AddRowChanged(ctr);
                }
            }
        }

        private void RowChanged(object sender, EventArgs e)
        {
            GridViewRow row =
               (GridViewRow)((Control)sender).NamingContainer;

            if (this._changedRows == null)
            {
                this._changedRows = new List<int>();
            }

            if (!this._changedRows.Contains(row.RowIndex))
            {
                this._changedRows.Add(row.RowIndex);
            }
        }

        private void CreateRows()
        {
            if (this.NewRowCount > 0)
            {
                ArrayList list = new ArrayList();
                DataControlField[] fields = this.GetDataControlFields();
                for (int i = 0; i < this.NewRowCount; i++)
                {
                    GridViewRow newRow = this.CreateNewRow(i, fields);
                    list.Add(newRow);
                }
                this._newRows = new GridViewRowCollection(list);

                Table grid;
                if (this.Rows.Count == 0)
                {
                    grid = this.CreateChildTable();
                    this.Controls.Add(grid);
                    if (this.ShowHeader)
                    {
                        GridViewRow headerRow =
                           this.CreateHeaderRow(fields);
                        grid.Rows.Add(headerRow);
                    }
                }
                else
                {
                    grid = (Table)this.Rows[0].Parent;
                }

                foreach (GridViewRow newRow in this._newRows)
                {
                    int NewRowIndex = this.NewRowPosition == NewRowPosition.Top ? 1 : grid.Rows.Count;
                    grid.Rows.AddAt(NewRowIndex, newRow);       
                }
            }
        }

        private GridViewRow CreateHeaderRow(DataControlField[] fields)
        {
            GridViewRow headerRow = base.CreateRow(-1, -1,
                DataControlRowType.Header, DataControlRowState.Normal);
            base.InitializeRow(headerRow, fields);

            return headerRow;
        }
        #endregion

    }
}