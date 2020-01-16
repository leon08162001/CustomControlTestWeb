using System;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Oracle.DataAccess.Client;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Caching;
using System.Drawing;
using AjaxPro;
using System.Drawing.Design;

namespace APTemplate
{
    /// <summary>
    /// 列舉-下拉式選單的層數。
    /// </summary>
    public enum DropDownLevel
    {
        /// <summary>
        /// 頁面顯示一層下拉式選單
        /// </summary>
        One = 0,
        /// <summary>
        /// 頁面顯示二層下拉式選單
        /// </summary>
        Two = 1,
        /// <summary>
        /// 頁面顯示三層下拉式選單
        /// </summary>
        Three = 2,
        /// <summary>
        /// 頁面顯示四層下拉式選單
        /// </summary>
        Four = 3,
        /// <summary>
        /// 頁面顯示五層下拉式選單
        /// </summary>
        Five = 4
    }

    /// <summary>
    /// 列舉-階層式下拉式選單的呈現方向。
    /// </summary>
    public enum EDisplayOrientation
    {
        /// <summary>
        /// 階層式下拉式選單以水平方向呈現
        /// </summary>
        Horizontal = 0,
        /// <summary>
        /// 階層式下拉式選單以垂直方向呈現
        /// </summary>
        Vertical = 1
    }

    /// <summary>
    /// 自定義三層式聯動下拉式選單控制項。
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item><description>下拉式選單必須跟資料庫結合，無法單獨使用。</description></item>
    /// </list>
    /// </remarks>
    [ToolboxData("<{0}:DropDownList_Multiple runat=server></{0}:DropDownList_Multiple>")]
    [ToolboxBitmap(typeof(DropDownList))]
    public class DropDownList_Multiple : ValidationBase
    {
        /// <summary>
        /// 儲存第一個下拉式選單的資料來源SQL指令
        /// </summary>
        protected string _FirstSelectSQL = "";
        /// <summary>
        /// 儲存第二個下拉式選單的資料來源SQL指令
        /// </summary>
        protected string _SecondSelectSQL = "";
        /// <summary>
        /// 儲存第三個下拉式選單的資料來源SQL指令
        /// </summary>
        protected string _ThirdSelectSQL = "";
        /// <summary>
        /// 儲存第四個下拉式選單的資料來源SQL指令
        /// </summary>
        protected string _ForthSelectSQL = "";
        /// <summary>
        /// 儲存第五個下拉式選單的資料來源SQL指令
        /// </summary>
        protected string _FifthSelectSQL = "";
        /// <summary>
        /// 儲存第二個下拉式選單SQL指令中的篩選欄位
        /// </summary>
        protected string _SecondFilterField = "";
        /// <summary>
        /// 儲存第三個下拉式選單SQL指令中的篩選欄位
        /// </summary>
        protected string _ThirdFilterField = "";
        /// <summary>
        /// 儲存第四個下拉式選單SQL指令中的篩選欄位
        /// </summary>
        protected string _ForthFilterField = "";
        /// <summary>
        /// 儲存第五個下拉式選單SQL指令中的篩選欄位
        /// </summary>
        protected string _FifthFilterField = "";
        /// <summary>
        /// 儲存執行SQL指令的資料庫連線Key
        /// </summary>
        protected string _ConnectionKey = "Default";
        /// <summary>
        /// 儲存下拉式選單中是否有初始項目
        /// </summary>
        private bool _HasInitialItem = false;
        /// <summary>
        /// 儲存第一個下拉式選單的控制項ID
        /// </summary>
        protected string _FirstDropDownListID = null;
        /// <summary>
        /// 儲存第二個下拉式選單的控制項ID
        /// </summary>
        protected string _SecondDropDownListID = null;
        /// <summary>
        /// 儲存第三個下拉式選單的控制項ID
        /// </summary>
        protected string _ThirdDropDownListID = null;
        /// <summary>
        /// 儲存第四個下拉式選單的控制項ID
        /// </summary>
        protected string _ForthDropDownListID = null;
        /// <summary>
        /// 儲存第五個下拉式選單的控制項ID
        /// </summary>
        protected string _FifthDropDownListID = null;
        protected string _TitleAlign = "left";
        /// <summary>
        /// 儲存第一個下拉式選單中的初始項目值
        /// </summary>
        private string _FirstInitialValue;
        /// <summary>
        /// 儲存第二個下拉式選單中的初始項目值
        /// </summary>
        private string _SecondInitialValue;
        /// <summary>
        /// 儲存第三個下拉式選單中的初始項目值
        /// </summary>
        private string _ThirdInitialValue;
        /// <summary>
        /// 儲存第四個下拉式選單中的初始項目值
        /// </summary>
        private string _ForthInitialValue;
        /// <summary>
        /// 儲存第五個下拉式選單中的初始項目值
        /// </summary>
        private string _FifthInitialValue;
        /// <summary>
        /// 儲存第一個下拉式選單中的初始項目文字
        /// </summary>
        private string _FirstInitialText = "--請選擇一個值--";
        /// <summary>
        /// 儲存第二個下拉式選單中的初始項目文字
        /// </summary>
        private string _SecondInitialText = "--請選擇一個值--";
        /// <summary>
        /// 儲存第三個下拉式選單中的初始項目文字
        /// </summary>
        private string _ThirdInitialText = "--請選擇一個值--";
        /// <summary>
        /// 儲存第四個下拉式選單中的初始項目文字
        /// </summary>
        private string _ForthInitialText = "--請選擇一個值--";
        /// <summary>
        /// 儲存第五個下拉式選單中的初始項目文字
        /// </summary>
        private string _FifthInitialText = "--請選擇一個值--";
        /// <summary>
        /// 儲存下拉式選單的層數
        /// </summary>
        protected DropDownLevel _DropDownListLevel = DropDownLevel.Two;
        private EDisplayOrientation _EDisplayOrientation = EDisplayOrientation.Horizontal;
        private string SelectedVal1;
        private string SelectedVal2;
        private string SelectedVal3;
        private string SelectedVal4;
        private string SelectedVal5;
        /// <summary>
        /// 建立DropDownList_Multiple控制項所需的table
        /// </summary>
        protected Table Table1 = new Table();
        /// <summary>
        /// 建立DropDownList_Multiple控制項所需的row
        /// </summary>
        protected TableRow TableRow = new TableRow();
        /// <summary>
        /// 建立DropDownList_Multiple控制項所需的cell
        /// </summary>
        protected TableCell TableCell1 = new TableCell();
        /// <summary>
        /// 建立DropDownList_Multiple控制項所需的cell
        /// </summary>
        protected TableCell TableCell2 = new TableCell();
        /// <summary>
        /// 建立DropDownList_Multiple控制項的第一個標籤
        /// </summary>
        protected Label Label1 = new Label();
        /// <summary>
        /// 建立DropDownList_Multiple控制項的第一個下拉式選單
        /// </summary>
        protected DropDownList DropDownList1 = new DropDownList();
        /// <summary>
        /// 建立DropDownList_Multiple控制項的第二個標籤
        /// </summary>
        protected Label Label2 = new Label();
        /// <summary>
        /// 建立DropDownList_Multiple控制項的第二個下拉式選單
        /// </summary>
        protected DropDownList DropDownList2 = new DropDownList();
        /// <summary>
        /// 建立DropDownList_Multiple控制項的第三個標籤
        /// </summary>
        protected Label Label3 = new Label();
        /// <summary>
        /// 建立DropDownList_Multiple控制項的第三個下拉式選單
        /// </summary>
        protected DropDownList DropDownList3 = new DropDownList();
        /// <summary>
        /// 建立DropDownList_Multiple控制項的第四個標籤
        /// </summary>
        protected Label Label4 = new Label();
        /// <summary>
        /// 建立DropDownList_Multiple控制項的第四個下拉式選單
        /// </summary>
        protected DropDownList DropDownList4 = new DropDownList();
        /// <summary>
        /// 建立DropDownList_Multiple控制項的第五個標籤
        /// </summary>
        protected Label Label5 = new Label();
        /// <summary>
        /// 建立DropDownList_Multiple控制項的第五個下拉式選單
        /// </summary>
        protected DropDownList DropDownList5 = new DropDownList();
        protected PlaceHolder PlaceHolder1 = new PlaceHolder();
        protected PlaceHolder PlaceHolder2 = new PlaceHolder();
        protected PlaceHolder PlaceHolder3 = new PlaceHolder();
        protected PlaceHolder PlaceHolder4 = new PlaceHolder();
        protected PlaceHolder PlaceHolder5 = new PlaceHolder();
        /// <summary>
        /// 內部儲存第一個下拉式選單的UniqueID
        /// </summary>
        protected string _FirstSelectedUniqueID = "";
        /// <summary>
        /// 內部儲存第一個下拉式選單的ClientID
        /// </summary>
        protected string _FirstSelectedClientID = "";
        /// <summary>
        /// 內部儲存第二個下拉式選單的UniqueID
        /// </summary>
        protected string _SecondSelectedUniqueID = "";
        /// <summary>
        /// 內部儲存第二個下拉式選單的ClientID
        /// </summary>
        protected string _SecondSelectedClientID = "";
        /// <summary>
        /// 內部儲存第三個下拉式選單的UniqueID
        /// </summary>
        protected string _ThirdSelectedUniqueID = "";
        /// <summary>
        /// 內部儲存第三個下拉式選單的ClientID
        /// </summary>
        protected string _ThirdSelectedClientID = "";
        /// <summary>
        /// 內部儲存第四個下拉式選單的UniqueID
        /// </summary>
        protected string _ForthSelectedUniqueID = "";
        /// <summary>
        /// 內部儲存第四個下拉式選單的ClientID
        /// </summary>
        protected string _ForthSelectedClientID = "";
        /// <summary>
        /// 內部儲存第五個下拉式選單的UniqueID
        /// </summary>
        protected string _FifthSelectedUniqueID = "";
        /// <summary>
        /// 內部儲存第五個下拉式選單的ClientID
        /// </summary>
        protected string _FifthSelectedClientID = "";

        protected dbkind _dbkind = dbkind.SQL_Server;

        protected string _ConnectionString = "";

        protected Boolean _FirstItemSelected = true;

        #region Public Properties & Methods

        /// <summary>
        /// 執行第一個下拉選單所連動的第二、三、四、五下拉選單內容。
        /// </summary>
        /// <param name="ObjValue">第一個下拉選單的選項值。</param>
        /// <returns>以DataSet回傳第二、三、四、五下拉選單內容。</returns>
        [AjaxPro.AjaxMethod()]
        public DataSet GetFirstCascadingListData(string ObjValue)
        {
            DataSet DS = new DataSet();
            DataTable GenericDT;
            string FilterValue = "";
            //DropDownList_Multiple obj = (DropDownList_Multiple)HttpContext.Current.Session["DropDownList_Multiple"];
            DropDownList_Multiple obj = (DropDownList_Multiple)HttpContext.Current.Cache["DropDownList_Multiple"];
            //第二個下拉選單
            GenericDT = new DataTable();
            GenericDT.Columns.Add("Value");
            GenericDT.Columns.Add("Text");
            if (obj.HasInitialItem)
            {
                DataRow InitialRow = GenericDT.NewRow();
                InitialRow["Value"] = obj.SecondInitialValue;
                InitialRow["Text"] = obj.SecondInitialText;
                GenericDT.Rows.Add(InitialRow);
            }
            if (obj.DropDownListLevel >= DropDownLevel.Two)
            {
                FilterValue = ObjValue;
                DataTable DT = GetTable(GetFilterSQL(obj.SecondSelectSQL, obj.SecondFilterField, FilterValue), obj.ConnectionString,this.dbkind);
                foreach (DataRow dr in DT.Rows)
                {
                    DataRow NewDr = GenericDT.NewRow();
                    NewDr["Value"] = dr[obj.SecondDataValueField];
                    NewDr["Text"] = dr[obj.SecondDataTextField];
                    GenericDT.Rows.Add(NewDr);
                }
                DS.Merge(GenericDT);

                //第三個下拉選單
                if (obj.DropDownListLevel >= DropDownLevel.Three)
                {
                    GenericDT = new DataTable();
                    GenericDT.Columns.Add("Value");
                    GenericDT.Columns.Add("Text");
                    if (obj.HasInitialItem)
                    {
                        DataRow InitialRow = GenericDT.NewRow();
                        InitialRow["Value"] = obj.SecondInitialValue;
                        InitialRow["Text"] = obj.SecondInitialText;
                        GenericDT.Rows.Add(InitialRow);
                    }
                    else
                    {
                        if (DT.Rows.Count > 0)
                        {
                            FilterValue = Convert.ToString(DT.Rows[0][obj.SecondDataValueField]);
                            string a = Convert.ToString(DT.Rows[0][obj.SecondDataValueField]);
                            DT = GetTable(GetFilterSQL(obj.ThirdSelectSQL, obj.ThirdFilterField, FilterValue), obj.ConnectionString, this.dbkind);
                            foreach (DataRow dr in DT.Rows)
                            {
                                DataRow NewDr = GenericDT.NewRow();
                                NewDr["Value"] = dr[obj.ThirdDataValueField];
                                NewDr["Text"] = dr[obj.ThirdDataTextField];
                                GenericDT.Rows.Add(NewDr);
                            }
                        }
                    }
                    DS.Merge(GenericDT);
                    //第四個下拉選單
                    if (obj.DropDownListLevel >= DropDownLevel.Four)
                    {
                        GenericDT = new DataTable();
                        GenericDT.Columns.Add("Value");
                        GenericDT.Columns.Add("Text");
                        if (obj.HasInitialItem)
                        {
                            DataRow InitialRow = GenericDT.NewRow();
                            InitialRow["Value"] = obj.ThirdInitialValue;
                            InitialRow["Text"] = obj.ThirdInitialText;
                            GenericDT.Rows.Add(InitialRow);
                        }
                        else
                        {
                            if (DT.Rows.Count > 0)
                            {
                                FilterValue = Convert.ToString(DT.Rows[0][obj.ThirdDataValueField]);
                                string a = Convert.ToString(DT.Rows[0][obj.ThirdDataValueField]);
                                DT = GetTable(GetFilterSQL(obj.ForthSelectSQL, obj.ForthFilterField, FilterValue), obj.ConnectionString, this.dbkind);
                                foreach (DataRow dr in DT.Rows)
                                {
                                    DataRow NewDr = GenericDT.NewRow();
                                    NewDr["Value"] = dr[obj.ForthDataValueField];
                                    NewDr["Text"] = dr[obj.ForthDataTextField];
                                    GenericDT.Rows.Add(NewDr);
                                }
                            }
                        }
                        DS.Merge(GenericDT);
                        //第五個下拉選單
                        if (obj.DropDownListLevel >= DropDownLevel.Five)
                        {
                            GenericDT = new DataTable();
                            GenericDT.Columns.Add("Value");
                            GenericDT.Columns.Add("Text");
                            if (obj.HasInitialItem)
                            {
                                DataRow InitialRow = GenericDT.NewRow();
                                InitialRow["Value"] = obj.ForthInitialValue;
                                InitialRow["Text"] = obj.ForthInitialText;
                                GenericDT.Rows.Add(InitialRow);
                            }
                            else
                            {
                                if (DT.Rows.Count > 0)
                                {
                                    FilterValue = Convert.ToString(DT.Rows[0][obj.ForthDataValueField]);
                                    string a = Convert.ToString(DT.Rows[0][obj.ForthDataValueField]);
                                    DT = GetTable(GetFilterSQL(obj.FifthSelectSQL, obj.FifthFilterField, FilterValue), obj.ConnectionString, this.dbkind);
                                    foreach (DataRow dr in DT.Rows)
                                    {
                                        DataRow NewDr = GenericDT.NewRow();
                                        NewDr["Value"] = dr[obj.FifthDataValueField];
                                        NewDr["Text"] = dr[obj.FifthDataTextField];
                                        GenericDT.Rows.Add(NewDr);
                                    }
                                }
                            }
                            DS.Merge(GenericDT);
                        }
                    }
                }
            }
            return DS;
        }

        /// <summary>
        /// 執行第二個下拉選單所連動的第三、四、五個下拉選單內容。
        /// </summary>
        /// <param name="ObjValue">第二個下拉選單的選項值。</param>
        /// <returns>以DataSet回傳第三、四、五個下拉選單內容。</returns>
        [AjaxPro.AjaxMethod()]
        public DataSet GetSecondCascadingListData(string ObjValue)
        {
            DataSet DS = new DataSet();
            DataTable GenericDT;
            string FilterValue = "";
            //DropDownList_Multiple obj = (DropDownList_Multiple)HttpContext.Current.Session["DropDownList_Multiple"];
            DropDownList_Multiple obj = (DropDownList_Multiple)HttpContext.Current.Cache["DropDownList_Multiple"];
            if (obj.DropDownListLevel >= DropDownLevel.Three)
            {
                FilterValue = ObjValue;
                DataTable DT = GetTable(GetFilterSQL(obj.ThirdSelectSQL, obj.ThirdFilterField, FilterValue), obj.ConnectionString, this.dbkind);
                GenericDT = new DataTable();
                GenericDT.Columns.Add("Value");
                GenericDT.Columns.Add("Text");
                if (obj.HasInitialItem)
                {
                    DataRow InitialRow = GenericDT.NewRow();
                    InitialRow["Value"] = obj.ThirdInitialValue;
                    InitialRow["Text"] = obj.ThirdInitialText;
                    GenericDT.Rows.Add(InitialRow);
                }
                foreach (DataRow dr in DT.Rows)
                {
                    DataRow NewDr = GenericDT.NewRow();
                    NewDr["Value"] = dr[obj.ThirdDataValueField];
                    NewDr["Text"] = dr[obj.ThirdDataTextField];
                    GenericDT.Rows.Add(NewDr);
                }
                DS.Merge(GenericDT);
                //第四個下拉選單
                if (obj.DropDownListLevel >= DropDownLevel.Four)
                {
                    GenericDT = new DataTable();
                    GenericDT.Columns.Add("Value");
                    GenericDT.Columns.Add("Text");
                    if (obj.HasInitialItem)
                    {
                        DataRow InitialRow = GenericDT.NewRow();
                        InitialRow["Value"] = obj.ThirdInitialValue;
                        InitialRow["Text"] = obj.ThirdInitialText;
                        GenericDT.Rows.Add(InitialRow);
                    }
                    else
                    {
                        if (DT.Rows.Count > 0)
                        {
                            FilterValue = Convert.ToString(DT.Rows[0][obj.ThirdDataValueField]);
                            string a = Convert.ToString(DT.Rows[0][obj.ThirdDataValueField]);
                            DT = GetTable(GetFilterSQL(obj.ForthSelectSQL, obj.ForthFilterField, FilterValue), obj.ConnectionString, this.dbkind);
                            foreach (DataRow dr in DT.Rows)
                            {
                                DataRow NewDr = GenericDT.NewRow();
                                NewDr["Value"] = dr[obj.ForthDataValueField];
                                NewDr["Text"] = dr[obj.ForthDataTextField];
                                GenericDT.Rows.Add(NewDr);
                            }
                        }
                    }
                    DS.Merge(GenericDT);
                    //第五個下拉選單
                    if (obj.DropDownListLevel >= DropDownLevel.Five)
                    {
                        GenericDT = new DataTable();
                        GenericDT.Columns.Add("Value");
                        GenericDT.Columns.Add("Text");
                        if (obj.HasInitialItem)
                        {
                            DataRow InitialRow = GenericDT.NewRow();
                            InitialRow["Value"] = obj.ForthInitialValue;
                            InitialRow["Text"] = obj.ForthInitialText;
                            GenericDT.Rows.Add(InitialRow);
                        }
                        else
                        {
                            if (DT.Rows.Count > 0)
                            {
                                FilterValue = Convert.ToString(DT.Rows[0][obj.ForthDataValueField]);
                                string a = Convert.ToString(DT.Rows[0][obj.ForthDataValueField]);
                                DT = GetTable(GetFilterSQL(obj.FifthSelectSQL, obj.FifthFilterField, FilterValue), obj.ConnectionString, this.dbkind);
                                foreach (DataRow dr in DT.Rows)
                                {
                                    DataRow NewDr = GenericDT.NewRow();
                                    NewDr["Value"] = dr[obj.FifthDataValueField];
                                    NewDr["Text"] = dr[obj.FifthDataTextField];
                                    GenericDT.Rows.Add(NewDr);
                                }
                            }
                        }
                        DS.Merge(GenericDT);
                    }
                }
            }
            return DS;
        }

        /// <summary>
        /// 執行第三個下拉選單所連動的第四、五個下拉選單內容。
        /// </summary>
        /// <param name="ObjValue">第三個下拉選單的選項值。</param>
        /// <returns>以DataSet回傳第四、五個下拉選單內容。</returns>
        [AjaxPro.AjaxMethod()]
        public DataSet GetThirdCascadingListData(string ObjValue)
        {
            DataSet DS = new DataSet();
            DataTable GenericDT;
            string FilterValue = "";
            //DropDownList_Multiple obj = (DropDownList_Multiple)HttpContext.Current.Session["DropDownList_Multiple"];
            DropDownList_Multiple obj = (DropDownList_Multiple)HttpContext.Current.Cache["DropDownList_Multiple"];
            if (obj.DropDownListLevel >= DropDownLevel.Four)
            {
                FilterValue = ObjValue;
                DataTable DT = GetTable(GetFilterSQL(obj.ForthSelectSQL, obj.ForthFilterField, FilterValue), obj.ConnectionString, this.dbkind);
                GenericDT = new DataTable();
                GenericDT.Columns.Add("Value");
                GenericDT.Columns.Add("Text");
                if (obj.HasInitialItem)
                {
                    DataRow InitialRow = GenericDT.NewRow();
                    InitialRow["Value"] = obj.ForthInitialValue;
                    InitialRow["Text"] = obj.ForthInitialText;
                    GenericDT.Rows.Add(InitialRow);
                }
                foreach (DataRow dr in DT.Rows)
                {
                    DataRow NewDr = GenericDT.NewRow();
                    NewDr["Value"] = dr[obj.ForthDataValueField];
                    NewDr["Text"] = dr[obj.ForthDataTextField];
                    GenericDT.Rows.Add(NewDr);
                }
                DS.Merge(GenericDT);
                //第五個下拉選單
                if (obj.DropDownListLevel >= DropDownLevel.Five)
                {
                    GenericDT = new DataTable();
                    GenericDT.Columns.Add("Value");
                    GenericDT.Columns.Add("Text");
                    if (obj.HasInitialItem)
                    {
                        DataRow InitialRow = GenericDT.NewRow();
                        InitialRow["Value"] = obj.ForthInitialValue;
                        InitialRow["Text"] = obj.ForthInitialText;
                        GenericDT.Rows.Add(InitialRow);
                    }
                    else
                    {
                        if (DT.Rows.Count > 0)
                        {
                            FilterValue = Convert.ToString(DT.Rows[0][obj.ForthDataValueField]);
                            string a = Convert.ToString(DT.Rows[0][obj.ForthDataValueField]);
                            DT = GetTable(GetFilterSQL(obj.FifthSelectSQL, obj.FifthFilterField, FilterValue), obj.ConnectionString, this.dbkind);
                            foreach (DataRow dr in DT.Rows)
                            {
                                DataRow NewDr = GenericDT.NewRow();
                                NewDr["Value"] = dr[obj.FifthDataValueField];
                                NewDr["Text"] = dr[obj.FifthDataTextField];
                                GenericDT.Rows.Add(NewDr);
                            }
                        }
                    }
                    DS.Merge(GenericDT);
                }
            }
            return DS;
        }

        /// <summary>
        /// 執行第四個下拉選單所連動的第五個下拉選單內容。
        /// </summary>
        /// <param name="ObjValue">第四個下拉選單的選項值。</param>
        /// <returns>以DataSet回傳第五個下拉選單內容。</returns>
        [AjaxPro.AjaxMethod()]
        public DataSet GetForthCascadingListData(string ObjValue)
        {
            DataSet DS = new DataSet();
            DataTable GenericDT;
            string FilterValue = "";
            //DropDownList_Multiple obj = (DropDownList_Multiple)HttpContext.Current.Session["DropDownList_Multiple"];
            DropDownList_Multiple obj = (DropDownList_Multiple)HttpContext.Current.Cache["DropDownList_Multiple"];
            if (obj.DropDownListLevel >= DropDownLevel.Five)
            {
                FilterValue = ObjValue;
                DataTable DT = GetTable(GetFilterSQL(obj.FifthSelectSQL, obj.FifthFilterField, FilterValue), obj.ConnectionString, this.dbkind);
                GenericDT = new DataTable();
                GenericDT.Columns.Add("Value");
                GenericDT.Columns.Add("Text");
                if (obj.HasInitialItem)
                {
                    DataRow InitialRow = GenericDT.NewRow();
                    InitialRow["Value"] = obj.FifthInitialValue;
                    InitialRow["Text"] = obj.FifthInitialText;
                    GenericDT.Rows.Add(InitialRow);
                }
                foreach (DataRow dr in DT.Rows)
                {
                    DataRow NewDr = GenericDT.NewRow();
                    NewDr["Value"] = dr[obj.FifthDataValueField];
                    NewDr["Text"] = dr[obj.FifthDataTextField];
                    GenericDT.Rows.Add(NewDr);
                }
                DS.Merge(GenericDT);
            }
            return DS;
        }


        /// <summary>
        /// 階層式下拉式選單的呈現方向。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
     Description("階層式下拉式選單的呈現方向。")]
        public EDisplayOrientation DisplayOrientation
        {
            get { return _EDisplayOrientation; }
            set { _EDisplayOrientation = value; }
        }

        /// <summary>
        /// 第一個下拉式選單的控制項ID。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("小數位數的長度。")]
        public string FirstDropDownListID
        {
            get { return _FirstDropDownListID; }
            set { _FirstDropDownListID = value; }
        }

        /// <summary>
        /// 第二個下拉式選單的控制項ID。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第二個下拉式選單的控制項ID。")]
        public string SecondDropDownListID
        {
            get { return _SecondDropDownListID; }
            set { _SecondDropDownListID = value; }
        }

        /// <summary>
        /// 第三個下拉式選單的控制項ID。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第三個下拉式選單的控制項ID。")]
        public string ThirdDropDownListID
        {
            get { return _ThirdDropDownListID; }
            set { _ThirdDropDownListID = value; }
        }

        /// <summary>
        /// 第四個下拉式選單的控制項ID。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第四個下拉式選單的控制項ID。")]
        public string ForthDropDownListID
        {
            get { return _ForthDropDownListID; }
            set { _ForthDropDownListID = value; }
        }

        /// <summary>
        /// 第五個下拉式選單的控制項ID。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第五個下拉式選單的控制項ID。")]
        public string FifthDropDownListID
        {
            get { return _FifthDropDownListID; }
            set { _FifthDropDownListID = value; }
        }

        /// <summary>
        /// 此自訂控制項在網頁上是否可見。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("此自訂控制項在網頁上是否可見。")]
        public new bool Visible
        {
            get { return Table1.Visible; }
            set { Table1.Visible = value; }
        }

        /// <summary>
        /// 此自訂控制項在網頁上是否啟用。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("此自訂控制項在網頁上是否啟用。")]
        public new bool Enabled
        {
            get { return Table1.Enabled; }
            set { Table1.Enabled = value; }
        }

        /// <summary>
        /// 連線的資料庫類型。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("連線的資料庫類型。")]
        public dbkind dbkind
        {
            get { return _dbkind; }
            set { _dbkind = value; }
        }

        /// <summary>
        /// 下拉式選單的層數。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("下拉式選單的層數。")]
        public DropDownLevel DropDownListLevel
        {
            get
            {
                if (_DropDownListLevel <= DropDownLevel.One)
                {
                    DropDownList2.Visible = false;
                    Label2.Visible = false;
                    DropDownList3.Visible = false;
                    Label3.Visible = false;
                    DropDownList4.Visible = false;
                    Label4.Visible = false;
                    DropDownList5.Visible = false;
                    Label5.Visible = false;
                }
                else if (_DropDownListLevel == DropDownLevel.Two)
                {
                    DropDownList2.Visible = true;
                    Label2.Visible = true;
                    DropDownList3.Visible = false;
                    Label3.Visible = false;
                    DropDownList4.Visible = false;
                    Label4.Visible = false;
                    DropDownList5.Visible = false;
                    Label5.Visible = false;
                }
                else if (_DropDownListLevel == DropDownLevel.Three)
                {
                    DropDownList2.Visible = true;
                    Label2.Visible = true;
                    DropDownList3.Visible = true;
                    Label3.Visible = true;
                    DropDownList4.Visible = false;
                    Label4.Visible = false;
                    DropDownList5.Visible = false;
                    Label5.Visible = false;
                }
                else if (_DropDownListLevel == DropDownLevel.Four)
                {
                    DropDownList2.Visible = true;
                    Label2.Visible = true;
                    DropDownList3.Visible = true;
                    Label3.Visible = true;
                    DropDownList4.Visible = true;
                    Label4.Visible = true;
                    DropDownList5.Visible = false;
                    Label5.Visible = false;
                }
                else if (_DropDownListLevel == DropDownLevel.Five)
                {
                    DropDownList2.Visible = true;
                    Label2.Visible = true;
                    DropDownList3.Visible = true;
                    Label3.Visible = true;
                    DropDownList4.Visible = true;
                    Label4.Visible = true;
                    DropDownList5.Visible = true;
                    Label5.Visible = true;
                }
                return _DropDownListLevel;
            }
            set
            {
                if (value <= DropDownLevel.One)
                {
                    DropDownList2.Visible = false;
                    Label2.Visible = false;
                    DropDownList3.Visible = false;
                    Label3.Visible = false;
                    DropDownList4.Visible = false;
                    Label4.Visible = false;
                    DropDownList5.Visible = false;
                    Label5.Visible = false;
                }
                else if (value == DropDownLevel.Two)
                {
                    DropDownList2.Visible = true;
                    Label2.Visible = true;
                    DropDownList3.Visible = false;
                    Label3.Visible = false;
                    DropDownList4.Visible = false;
                    Label4.Visible = false;
                    DropDownList5.Visible = false;
                    Label5.Visible = false;
                }
                else if (value == DropDownLevel.Three)
                {
                    DropDownList2.Visible = true;
                    Label2.Visible = true;
                    DropDownList3.Visible = true;
                    Label3.Visible = true;
                    DropDownList4.Visible = false;
                    Label4.Visible = false;
                    DropDownList5.Visible = false;
                    Label5.Visible = false;
                }
                else if (value == DropDownLevel.Four)
                {
                    DropDownList2.Visible = true;
                    Label2.Visible = true;
                    DropDownList3.Visible = true;
                    Label3.Visible = true;
                    DropDownList4.Visible = true;
                    Label4.Visible = true;
                    DropDownList5.Visible = false;
                    Label5.Visible = false;
                }
                else if (value == DropDownLevel.Five)
                {
                    DropDownList2.Visible = true;
                    Label2.Visible = true;
                    DropDownList3.Visible = true;
                    Label3.Visible = true;
                    DropDownList4.Visible = true;
                    Label4.Visible = true;
                    DropDownList5.Visible = true;
                    Label5.Visible = true;
                }
                _DropDownListLevel = value;
            }
        }

        /// <summary>
        /// 下拉式選單中是否有初始項目。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("下拉式選單中是否有初始項目。")]
        public bool HasInitialItem
        {
            get { return _HasInitialItem; }
            set { _HasInitialItem = value; }
        }

        /// <summary>
        /// 下拉式選單中第一個為選擇項目。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("下拉式選單中第一個為選擇項目。")]
        public bool FirstItemSelected
        {
            get { return _FirstItemSelected; }
            set { _FirstItemSelected = value; }
        }

        /// <summary>
        /// 第一個下拉式選單中的初始項目值。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第一個下拉式選單中的初始項目值。")]
        public string FirstInitialValue
        {
            get
            {
                _FirstInitialValue = HasInitialItem == true ? "-1" : null;
                return _FirstInitialValue;
            }
        }

        /// <summary>
        /// 第二個下拉式選單中的初始項目值。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第二個下拉式選單中的初始項目值。")]
        public string SecondInitialValue
        {
            get
            {
                _SecondInitialValue = HasInitialItem == true ? "-1" : null;
                return _SecondInitialValue;
            }
        }

        /// <summary>
        /// 第三個下拉式選單中的初始項目值。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第三個下拉式選單中的初始項目值。")]
        public string ThirdInitialValue
        {
            get
            {
                _ThirdInitialValue = HasInitialItem == true ? "-1" : null;
                return _ThirdInitialValue;
            }
        }

        /// <summary>
        /// 第四個下拉式選單中的初始項目值。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第四個下拉式選單中的初始項目值。")]
        public string ForthInitialValue
        {
            get
            {
                _ForthInitialValue = HasInitialItem == true ? "-1" : null;
                return _ForthInitialValue;
            }
        }

        /// <summary>
        /// 第五個下拉式選單中的初始項目值。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第五個下拉式選單中的初始項目值。")]
        public string FifthInitialValue
        {
            get
            {
                _FifthInitialValue = HasInitialItem == true ? "-1" : null;
                return _FifthInitialValue;
            }
        }

        /// <summary>
        /// 第一個下拉式選單中的初始項目文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第一個下拉式選單中的初始項目文字。")]
        public string FirstInitialText
        {
            get { return _FirstInitialText; }
            set { _FirstInitialText = value; }
        }

        /// <summary>
        /// 第二個下拉式選單中的初始項目文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第二個下拉式選單中的初始項目文字。")]
        public string SecondInitialText
        {
            get { return _SecondInitialText; }
            set { _SecondInitialText = value; }
        }

        /// <summary>
        /// 第三個下拉式選單中的初始項目文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第三個下拉式選單中的初始項目文字。")]
        public string ThirdInitialText
        {
            get { return _ThirdInitialText; }
            set { _ThirdInitialText = value; }
        }

        /// <summary>
        /// 第四個下拉式選單中的初始項目文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第四個下拉式選單中的初始項目文字。")]
        public string ForthInitialText
        {
            get { return _ForthInitialText; }
            set { _ForthInitialText = value; }
        }

        /// <summary>
        /// 第五個下拉式選單中的初始項目文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第五個下拉式選單中的初始項目文字。")]
        public string FifthInitialText
        {
            get { return _FifthInitialText; }
            set { _FifthInitialText = value; }
        }

        /// <summary>
        /// 設定第一個標籤的背景顏色。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("設定第一個標籤的背景顏色。")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public Color TitleBackColor
        {
            get
            {
                if (ViewState["TitleBackColor"] == null)
                    return Color.Aqua;
                else
                    return (Color)ViewState["TitleBackColor"];
            }
            set { ViewState["TitleBackColor"] = value; }
        }

        /// <summary>
        /// 設定標籤文字顏色。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("設定標籤文字顏色。")]
        [Editor(typeof(ColorEditor), typeof(UITypeEditor))]
        public Color TitleForeColor
        {
            get
            {
                if (ViewState["TitleForeColor"] == null)
                    return Color.Sienna;
                else
                    return (Color)ViewState["TitleForeColor"];
            }
            set { ViewState["TitleForeColor"] = value; }
        }

        /// <summary>
        /// 設定第一個標籤寬度。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("設定第一個標籤寬度。")]
        public Unit TitleWidth
        {
            get
            {
                if (ViewState["TitleWidth"] == null)
                    return Label1.Width;
                else
                    return (Unit)ViewState["TitleWidth"];
            }
            set { ViewState["TitleWidth"] = value; }
        }

        /// <summary>
        /// 設定下拉式選單寬度。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("設定下拉式選單寬度。")]
        public Unit DropDownWidth
        {
            get
            {
                if (ViewState["DropDownWidth"] == null)
                    return DropDownList1.Width;
                else
                    return (Unit)ViewState["DropDownWidth"];
            }
            set { ViewState["DropDownWidth"] = value; }
        }


        /// <summary>
        /// 設定第一個標籤對齊方式。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("設定第一個標籤對齊方式。")]
        public virtual Align TitleAlign
        {
            get
            {
                if (ViewState["TitleAlign"] == null)
                    return Align.left;
                else
                    return (Align)ViewState["TitleAlign"];
            }
            set
            {
                ViewState["TitleAlign"] = value;
                _TitleAlign = (int)value == 1 ? "left" : (int)value == 2 ? "right" : (int)value == 3 ? "center" : "justify";
            }
        }

        /// <summary>
        /// 設定是否顯示標籤。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("設定是否顯示標籤。")]
        public bool IsShowTitle
        {
            get
            {
                if (ViewState["IsShowTitle"] == null)
                    return true;
                else
                    return (bool)ViewState["IsShowTitle"];
            }
            set { ViewState["IsShowTitle"] = value; }
        }

        /// <summary>
        ///控制項是否加上邊框。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("控制項是否加上邊框。")]
        public virtual bool HasBorder
        {
            get
            {
                if (ViewState["HasBorder"] == null)
                    return true;
                else
                    return (bool)ViewState["HasBorder"];
            }
            set { ViewState["HasBorder"] = value; }
        }

        /// <summary>
        /// 第一個下拉式選單標籤文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第一個下拉式選單標籤文字。")]
        public string FirstTitle
        {
            get { return Label1.Text; }
            set { Label1.Text = value; }
        }

        /// <summary>
        /// 第二個下拉式選單標籤文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第二個下拉式選單標籤文字。")]
        public string SecondTitle
        {
            get { return Label2.Text; }
            set { Label2.Text = value; }
        }

        /// <summary>
        /// 第三個下拉式選單標籤文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第三個下拉式選單標籤文字。")]
        public string ThirdTitle
        {
            get { return Label3.Text; }
            set { Label3.Text = value; }
        }

        /// <summary>
        /// 第四個下拉式選單標籤文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第四個下拉式選單標籤文字。")]
        public string ForthTitle
        {
            get { return Label4.Text; }
            set { Label4.Text = value; }
        }

        /// <summary>
        /// 第五個下拉式選單標籤文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第五個下拉式選單標籤文字。")]
        public string FifthTitle
        {
            get { return Label5.Text; }
            set { Label5.Text = value; }
        }

        /// <summary>
        /// 第一個下拉式選單的選擇項目值。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第一個下拉式選單的選擇項目值。")]
        public string FirstSelectedValue
        {
            get { return DropDownList1.SelectedValue; }
            set { DropDownList1.SelectedValue = value; }
        }

        /// <summary>
        /// 第二個下拉式選單的選擇項目值。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第二個下拉式選單的選擇項目值。")]
        public string SecondSelectedValue
        {
            get { return DropDownList2.SelectedValue; }
            set { DropDownList2.SelectedValue = value; }
        }

        /// <summary>
        /// 第三個下拉式選單的選擇項目值。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第三個下拉式選單的選擇項目值。")]
        public string ThirdSelectedValue
        {
            get { return DropDownList3.SelectedValue; }
            set { DropDownList3.SelectedValue = value; }
        }

        /// <summary>
        /// 第四個下拉式選單的選擇項目值。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第四個下拉式選單的選擇項目值。")]
        public string ForthSelectedValue
        {
            get { return DropDownList4.SelectedValue; }
            set { DropDownList4.SelectedValue = value; }
        }

        /// <summary>
        /// 第五個下拉式選單的選擇項目值。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第五個下拉式選單的選擇項目值。")]
        public string FifthSelectedValue
        {
            get { return DropDownList5.SelectedValue; }
            set { DropDownList5.SelectedValue = value; }
        }

        /// <summary>
        /// 第一個下拉式選單的選擇項目索引。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第一個下拉式選單的選擇項目索引。")]
        public int FirstSelectedIndex
        {
            get { return DropDownList1.SelectedIndex; }
            set { DropDownList1.SelectedIndex = value; }
        }

        /// <summary>
        /// 第二個下拉式選單的選擇項目索引。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第二個下拉式選單的選擇項目索引。")]
        public int SecondSelectedIndex
        {
            get { return DropDownList2.SelectedIndex; }
            set { DropDownList2.SelectedIndex = value; }
        }

        /// <summary>
        /// 第三個下拉式選單的選擇項目索引。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第三個下拉式選單的選擇項目索引。")]
        public int ThirdSelectedIndex
        {
            get { return DropDownList3.SelectedIndex; }
            set { DropDownList3.SelectedIndex = value; }
        }

        /// <summary>
        /// 第四個下拉式選單的選擇項目索引。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第四個下拉式選單的選擇項目索引。")]
        public int ForthSelectedIndex
        {
            get { return DropDownList4.SelectedIndex; }
            set { DropDownList4.SelectedIndex = value; }
        }

        /// <summary>
        /// 第五個下拉式選單的選擇項目索引。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第五個下拉式選單的選擇項目索引。")]
        public int FifthSelectedIndex
        {
            get { return DropDownList5.SelectedIndex; }
            set { DropDownList5.SelectedIndex = value; }
        }

        /// <summary>
        /// 第一個下拉式選單的選擇項目文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第一個下拉式選單的選擇項目文字。")]
        public string FirstSelectedText
        {
            get
            {
                if (DropDownList1.SelectedItem == null)
                    return null;
                else
                    return DropDownList1.SelectedItem.Text;
            }
            set { DropDownList1.SelectedItem.Text = value; }
        }

        /// <summary>
        /// 第二個下拉式選單的選擇項目文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第二個下拉式選單的選擇項目文字。")]
        public string SecondSelectedText
        {
            get
            {
                if (DropDownList2.SelectedItem == null)
                    return null;
                else
                    return DropDownList2.SelectedItem.Text;
            }
            set { DropDownList2.SelectedItem.Text = value; }
        }

        /// <summary>
        /// 第三個下拉式選單的選擇項目文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第三個下拉式選單的選擇項目文字。")]
        public string ThirdSelectedText
        {
            get
            {
                if (DropDownList3.SelectedItem == null)
                    return null;
                else
                    return DropDownList3.SelectedItem.Text;
            }
            set { DropDownList3.SelectedItem.Text = value; }
        }

        /// <summary>
        /// 第四個下拉式選單的選擇項目文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第四個下拉式選單的選擇項目文字。")]
        public string ForthSelectedText
        {
            get
            {
                if (DropDownList4.SelectedItem == null)
                    return null;
                else
                    return DropDownList4.SelectedItem.Text;
            }
            set { DropDownList4.SelectedItem.Text = value; }
        }

        /// <summary>
        /// 第五個下拉式選單的選擇項目文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第五個下拉式選單的選擇項目文字。")]
        public string FifthSelectedText
        {
            get
            {
                if (DropDownList5.SelectedItem == null)
                    return null;
                else
                    return DropDownList5.SelectedItem.Text;
            }
            set { DropDownList5.SelectedItem.Text = value; }
        }

        /// <summary>
        /// 第一個下拉式選單的所有項目。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第一個下拉式選單的所有項目。")]
        public ListItemCollection FirstItems
        {
            get { return DropDownList1.Items; }
        }

        /// <summary>
        /// 第二個下拉式選單的所有項目。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第二個下拉式選單的所有項目。")]
        public ListItemCollection SecondItems
        {
            get { return DropDownList2.Items; }
        }

        /// <summary>
        /// 第三個下拉式選單的所有項目。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第三個下拉式選單的所有項目。")]
        public ListItemCollection ThirdItems
        {
            get { return DropDownList3.Items; }
        }

        /// <summary>
        /// 第四個下拉式選單的所有項目。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第四個下拉式選單的所有項目。")]
        public ListItemCollection ForthItems
        {
            get { return DropDownList4.Items; }
        }

        /// <summary>
        /// 第五個下拉式選單的所有項目。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第五個下拉式選單的所有項目。")]
        public ListItemCollection FifthItems
        {
            get { return DropDownList5.Items; }
        }

        /// <summary>
        /// 第二個下拉式選單SQL指令中的篩選欄位。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description(" 第二個下拉式選單SQL指令中的篩選欄位。")]
        public string SecondFilterField
        {
            get { return _SecondFilterField; }
            set { _SecondFilterField = value; }
        }

        /// <summary>
        /// 第三個下拉式選單SQL指令中的篩選欄位。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description(" 第三個下拉式選單SQL指令中的篩選欄位。")]
        public string ThirdFilterField
        {
            get { return _ThirdFilterField; }
            set { _ThirdFilterField = value; }
        }

        /// <summary>
        /// 第四個下拉式選單SQL指令中的篩選欄位。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description(" 第四個下拉式選單SQL指令中的篩選欄位。")]
        public string ForthFilterField
        {
            get { return _ForthFilterField; }
            set { _ForthFilterField = value; }
        }

        /// <summary>
        /// 第五個下拉式選單SQL指令中的篩選欄位。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description(" 第五個下拉式選單SQL指令中的篩選欄位。")]
        public string FifthFilterField
        {
            get { return _FifthFilterField; }
            set { _FifthFilterField = value; }
        }

        /// <summary>
        /// 第一個下拉式選單的資料來源SQL指令。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第一個下拉式選單的資料來源SQL指令。")]
        public string FirstSelectSQL
        {
            get { return _FirstSelectSQL; }
            set { _FirstSelectSQL = value; }
        }

        /// <summary>
        /// 第二個下拉式選單的資料來源SQL指令。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第二個下拉式選單的資料來源SQL指令。")]
        public string SecondSelectSQL
        {
            get
            { return _SecondSelectSQL; }
            set
            { _SecondSelectSQL = value; }
        }

        /// <summary>
        /// 第三個下拉式選單的資料來源SQL指令。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第三個下拉式選單的資料來源SQL指令。")]
        public string ThirdSelectSQL
        {
            get { return _ThirdSelectSQL; }
            set { _ThirdSelectSQL = value; }
        }

        /// <summary>
        /// 第四個下拉式選單的資料來源SQL指令。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第四個下拉式選單的資料來源SQL指令。")]
        public string ForthSelectSQL
        {
            get { return _ForthSelectSQL; }
            set { _ForthSelectSQL = value; }
        }

        /// <summary>
        /// 第五個下拉式選單的資料來源SQL指令。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第五個下拉式選單的資料來源SQL指令。")]
        public string FifthSelectSQL
        {
            get { return _FifthSelectSQL; }
            set { _FifthSelectSQL = value; }
        }

        /// <summary>
        /// 第一個下拉式選單中項目值的來源欄位。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第一個下拉式選單中項目值的來源欄位。")]
        public string FirstDataValueField
        {
            get { return DropDownList1.DataValueField; }
            set { DropDownList1.DataValueField = value; }
        }

        /// <summary>
        /// 第二個下拉式選單中項目值的來源欄位。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第二個下拉式選單中項目值的來源欄位。")]
        public string SecondDataValueField
        {
            get { return DropDownList2.DataValueField; }
            set { DropDownList2.DataValueField = value; }
        }

        /// <summary>
        /// 第三個下拉式選單中項目值的來源欄位。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第三個下拉式選單中項目值的來源欄位。")]
        public string ThirdDataValueField
        {
            get { return DropDownList3.DataValueField; }
            set { DropDownList3.DataValueField = value; }
        }

        /// <summary>
        /// 第四個下拉式選單中項目值的來源欄位。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第四個下拉式選單中項目值的來源欄位。")]
        public string ForthDataValueField
        {
            get { return DropDownList4.DataValueField; }
            set { DropDownList4.DataValueField = value; }
        }

        /// <summary>
        /// 第五個下拉式選單中項目值的來源欄位。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第五個下拉式選單中項目值的來源欄位。")]
        public string FifthDataValueField
        {
            get { return DropDownList5.DataValueField; }
            set { DropDownList5.DataValueField = value; }
        }

        /// <summary>
        /// 第一個下拉式選單中項目文字的來源欄位。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第一個下拉式選單中項目文字的來源欄位。")]
        public string FirstDataTextField
        {
            get { return DropDownList1.DataTextField; }
            set { DropDownList1.DataTextField = value; }
        }

        /// <summary>
        /// 第二個下拉式選單中項目文字的來源欄位。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第二個下拉式選單中項目文字的來源欄位。")]
        public string SecondDataTextField
        {
            get { return DropDownList2.DataTextField; }
            set { DropDownList2.DataTextField = value; }
        }

        /// <summary>
        /// 第三個下拉式選單中項目文字的來源欄位。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第三個下拉式選單中項目文字的來源欄位。")]
        public string ThirdDataTextField
        {
            get { return DropDownList3.DataTextField; }
            set { DropDownList3.DataTextField = value; }
        }

        /// <summary>
        /// 第四個下拉式選單中項目文字的來源欄位。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第四個下拉式選單中項目文字的來源欄位。")]
        public string ForthDataTextField
        {
            get { return DropDownList4.DataTextField; }
            set { DropDownList4.DataTextField = value; }
        }

        /// <summary>
        /// 第五個下拉式選單中項目文字的來源欄位。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("第五個下拉式選單中項目文字的來源欄位。")]
        public string FifthDataTextField
        {
            get { return DropDownList5.DataTextField; }
            set { DropDownList5.DataTextField = value; }
        }

        /// <summary>
        /// 執行SQL指令的資料庫連線Key。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("執行SQL指令的資料庫連線Key。")]
        public string ConnectionKey
        {
            get { return _ConnectionKey; }
            set { _ConnectionKey = value; }
        }

         /// <summary>
        /// 執行SQL指令的資料庫連線字串。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("執行SQL指令的資料庫連線字串。")]
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }

        /// <summary>
        /// 重新查詢並更新下拉式選單項目。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("重新查詢並更新下拉式選單項目。")]
        public void Refresh()
        {
            DropDownList1.Items.Clear();
            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            DropDownList4.Items.Clear();
            DropDownList5.Items.Clear();
            if (HasInitialItem == true)
            {
                DropDownList1.AppendDataBoundItems = true;
                DropDownList2.AppendDataBoundItems = true;
                DropDownList3.AppendDataBoundItems = true;
                DropDownList4.AppendDataBoundItems = true;
                DropDownList5.AppendDataBoundItems = true;
                DropDownList1.Items.Add(new ListItem(FirstInitialText, FirstInitialValue));
                DropDownList2.Items.Add(new ListItem(SecondInitialText, SecondInitialValue));
                DropDownList3.Items.Add(new ListItem(ThirdInitialText, ThirdInitialValue));
                DropDownList4.Items.Add(new ListItem(ForthInitialText, ForthInitialValue));
                DropDownList5.Items.Add(new ListItem(FifthInitialText, FifthInitialValue));
                DropDownList1.SelectedValue = FirstInitialValue;
                DropDownList2.SelectedValue = SecondInitialValue;
                DropDownList3.SelectedValue = ThirdInitialValue;
                DropDownList4.SelectedValue = ForthInitialValue;
                DropDownList5.SelectedValue = FifthInitialValue;
            }
            DropDownList1.DataSource = GetTable(FirstSelectSQL, this.ConnectionString, this.dbkind);
            DropDownList1.DataTextField = FirstDataTextField;
            DropDownList1.DataValueField = FirstDataValueField;
            DropDownList1.DataBind();
            ListItem item1 = DropDownList1.Items.FindByValue(SelectedVal1);
            if (item1 != null)
            {
                DropDownList1.SelectedValue = SelectedVal1;
            }
            else
            {
                DropDownList1.SelectedIndex = 0;
            }
            if (DropDownList1.Items.Count > 0 && DropDownListLevel >= DropDownLevel.Two)
            {
                DropDownList2.DataSource = GetTable(GetFilterSQL(SecondSelectSQL, SecondFilterField, FirstSelectedValue), this.ConnectionString, this.dbkind);
                DropDownList2.DataTextField = SecondDataTextField;
                DropDownList2.DataValueField = SecondDataValueField;
                DropDownList2.DataBind();
                ListItem item2 = DropDownList2.Items.FindByValue(SelectedVal2);
                if (item2 != null)
                {
                    DropDownList2.SelectedValue = SelectedVal2;
                }
                else
                {
                    DropDownList2.SelectedIndex = 0;
                }
                if (DropDownList2.Items.Count > 0 && DropDownListLevel >= DropDownLevel.Three)
                {
                    DropDownList3.DataSource = GetTable(GetFilterSQL(ThirdSelectSQL, ThirdFilterField, SecondSelectedValue), this.ConnectionString, this.dbkind);
                    DropDownList3.DataTextField = ThirdDataTextField;
                    DropDownList3.DataValueField = ThirdDataValueField;
                    DropDownList3.DataBind();
                    ListItem item3 = DropDownList3.Items.FindByValue(SelectedVal3);
                    if (item3 != null)
                    {
                        DropDownList3.SelectedValue = SelectedVal3;
                    }
                    else
                    {
                        DropDownList3.SelectedIndex = 0;
                    }
                    if (DropDownList3.Items.Count > 0 && DropDownListLevel >= DropDownLevel.Four)
                    {
                        DropDownList4.DataSource = GetTable(GetFilterSQL(ForthSelectSQL, ForthFilterField, ThirdSelectedValue), this.ConnectionString, this.dbkind);
                        DropDownList4.DataTextField = ForthDataTextField;
                        DropDownList4.DataValueField = ForthDataValueField;
                        DropDownList4.DataBind();
                        ListItem item4 = DropDownList4.Items.FindByValue(SelectedVal4);
                        if (item4 != null)
                        {
                            DropDownList4.SelectedValue = SelectedVal4;
                        }
                        else
                        {
                            DropDownList4.SelectedIndex = 0;
                        }
                        if (DropDownList4.Items.Count > 0 && DropDownListLevel >= DropDownLevel.Five)
                        {
                            DropDownList5.DataSource = GetTable(GetFilterSQL(FifthSelectSQL, FifthFilterField, ForthSelectedValue), this.ConnectionString, this.dbkind);
                            DropDownList5.DataTextField = FifthDataTextField;
                            DropDownList5.DataValueField = FifthDataValueField;
                            DropDownList5.DataBind();
                            ListItem item5 = DropDownList5.Items.FindByValue(SelectedVal5);
                            if (item5 != null)
                            {
                                DropDownList5.SelectedValue = SelectedVal5;
                            }
                            else
                            {
                                DropDownList5.SelectedIndex = 0;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Protected Properties & Methods

        protected override void OnInit(EventArgs e)
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Cache["DropDownList_Multiple"] == null)
                {
                    //HttpContext.Current.Session.Timeout = 600;
                    //HttpContext.Current.Session["DropDownList_Multiple"] = this;
                    HttpContext.Current.Cache.Insert("DropDownList_Multiple", this, null, DateTime.Now.AddMinutes(600), Cache.NoSlidingExpiration);
                }
                DropDownList1.ID = FirstDropDownListID == null || FirstDropDownListID == "" ? "DropDownList1" : FirstDropDownListID;
                DropDownList2.ID = SecondDropDownListID == null || SecondDropDownListID == "" ? "DropDownList2" : SecondDropDownListID;
                DropDownList3.ID = ThirdDropDownListID == null || ThirdDropDownListID == "" ? "DropDownList3" : ThirdDropDownListID;
                DropDownList4.ID = ForthDropDownListID == null || ForthDropDownListID == "" ? "DropDownList4" : ForthDropDownListID;
                DropDownList5.ID = FifthDropDownListID == null || FifthDropDownListID == "" ? "DropDownList5" : FifthDropDownListID;
            }
            Page.RegisterRequiresControlState(this);
        }

        protected override void OnLoad(EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(APTemplate.DropDownList_Multiple));
            CompareValidator CV1 = null;
            CompareValidator CV2 = null;
            CompareValidator CV3 = null;
            CompareValidator CV4 = null;
            CompareValidator CV5 = null;
            if (this.ConnectionKey != "")
            {
                this.ConnectionString = ConfigurationManager.ConnectionStrings[this.ConnectionKey].ConnectionString;
            }
            if (NeedValidation)
            {
                CV1 = new CompareValidator();
                CV1.ControlToValidate = DropDownList1.ID;
                CV1.Font.Size = FontUnit.Small;
                CV1.Display = ValidatorDisplay.Dynamic;
                CV1.ValueToCompare = FirstInitialValue;
                CV1.ErrorMessage = "請選擇第一個下拉選單值!";
                CV1.Type = ValidationDataType.String;
                CV1.Operator = ValidationCompareOperator.NotEqual;
                CV1.ValidationGroup = this.ValidationGroup;

                CV2 = new CompareValidator();
                CV2.ControlToValidate = DropDownList2.ID;
                CV2.Font.Size = FontUnit.Small;
                CV2.Display = ValidatorDisplay.Dynamic;
                CV2.ValueToCompare = SecondInitialValue;
                CV2.ErrorMessage = "請選擇第二個下拉選單值!";
                CV2.Type = ValidationDataType.String;
                CV2.Operator = ValidationCompareOperator.NotEqual;
                CV2.ValidationGroup = this.ValidationGroup;

                CV3 = new CompareValidator();
                CV3.ControlToValidate = DropDownList3.ID;
                CV3.Font.Size = FontUnit.Small;
                CV3.Display = ValidatorDisplay.Dynamic;
                CV3.ValueToCompare = ThirdInitialValue;
                CV3.ErrorMessage = "請選擇第三個下拉選單值!";
                CV3.Type = ValidationDataType.String;
                CV3.Operator = ValidationCompareOperator.NotEqual;
                CV3.ValidationGroup = this.ValidationGroup;

                CV4 = new CompareValidator();
                CV4.ControlToValidate = DropDownList4.ID;
                CV4.Font.Size = FontUnit.Small;
                CV4.Display = ValidatorDisplay.Dynamic;
                CV4.ValueToCompare = ForthInitialValue;
                CV4.ErrorMessage = "請選擇第四個下拉選單值!";
                CV4.Type = ValidationDataType.String;
                CV4.Operator = ValidationCompareOperator.NotEqual;
                CV4.ValidationGroup = this.ValidationGroup;

                CV5 = new CompareValidator();
                CV5.ControlToValidate = DropDownList5.ID;
                CV5.Font.Size = FontUnit.Small;
                CV5.Display = ValidatorDisplay.Dynamic;
                CV5.ValueToCompare = FifthInitialValue;
                CV5.ErrorMessage = "請選擇第五個下拉選單值!";
                CV5.Type = ValidationDataType.String;
                CV5.Operator = ValidationCompareOperator.NotEqual;
                CV5.ValidationGroup = this.ValidationGroup;

                PlaceHolder1.Controls.Add(CV1);
                PlaceHolder2.Controls.Add(CV2);
                PlaceHolder3.Controls.Add(CV3);
                PlaceHolder4.Controls.Add(CV4);
                PlaceHolder5.Controls.Add(CV5);
            }

            SetValidationType(base.ValidationType, new BaseValidator[] { CV1 }, PlaceHolder1);
            SetValidationType(base.ValidationType, new BaseValidator[] { CV2 }, PlaceHolder2);
            SetValidationType(base.ValidationType, new BaseValidator[] { CV3 }, PlaceHolder3);
            SetValidationType(base.ValidationType, new BaseValidator[] { CV4 }, PlaceHolder4);
            SetValidationType(base.ValidationType, new BaseValidator[] { CV5 }, PlaceHolder5);
            DropDownList1.Items.Clear();
            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            DropDownList4.Items.Clear();
            DropDownList5.Items.Clear();
            if (HasInitialItem == true)
            {
                DropDownList1.AppendDataBoundItems = true;
                DropDownList2.AppendDataBoundItems = true;
                DropDownList3.AppendDataBoundItems = true;
                DropDownList4.AppendDataBoundItems = true;
                DropDownList5.AppendDataBoundItems = true;
                DropDownList1.Items.Add(new ListItem(FirstInitialText, FirstInitialValue));
                DropDownList2.Items.Add(new ListItem(SecondInitialText, SecondInitialValue));
                DropDownList3.Items.Add(new ListItem(ThirdInitialText, ThirdInitialValue));
                DropDownList4.Items.Add(new ListItem(ForthInitialText, ForthInitialValue));
                DropDownList5.Items.Add(new ListItem(FifthInitialText, FifthInitialValue));
            }
            DropDownList1.DataSource = GetTable(FirstSelectSQL, this.ConnectionString, this.dbkind);
            DropDownList1.DataTextField = FirstDataTextField;
            DropDownList1.DataValueField = FirstDataValueField;
            DropDownList1.DataBind();
            DropDownList1.SelectedValue = SelectedVal1;
            if (DropDownList1.Items.Count > 0 && DropDownListLevel >= DropDownLevel.Two)
            {
                DropDownList2.DataSource = GetTable(GetFilterSQL(SecondSelectSQL, SecondFilterField, FirstSelectedValue), this.ConnectionString, this.dbkind);
                DropDownList2.DataTextField = SecondDataTextField;
                DropDownList2.DataValueField = SecondDataValueField;
                DropDownList2.DataBind();
                DropDownList2.SelectedValue = SelectedVal2;
                if (DropDownList2.Items.Count > 0 && DropDownListLevel >= DropDownLevel.Three)
                {
                    DropDownList3.DataSource = GetTable(GetFilterSQL(ThirdSelectSQL, ThirdFilterField, SecondSelectedValue), this.ConnectionString, this.dbkind);
                    DropDownList3.DataTextField = ThirdDataTextField;
                    DropDownList3.DataValueField = ThirdDataValueField;
                    DropDownList3.DataBind();
                    DropDownList3.SelectedValue = SelectedVal3;
                    if (DropDownList3.Items.Count > 0 && DropDownListLevel >= DropDownLevel.Four)
                    {
                        DropDownList4.DataSource = GetTable(GetFilterSQL(ForthSelectSQL, ForthFilterField, ThirdSelectedValue), this.ConnectionString, this.dbkind);
                        DropDownList4.DataTextField = ForthDataTextField;
                        DropDownList4.DataValueField = ForthDataValueField;
                        DropDownList4.DataBind();
                        DropDownList4.SelectedValue = SelectedVal4;
                        if (DropDownList4.Items.Count > 0 && DropDownListLevel >= DropDownLevel.Five)
                        {
                            DropDownList5.DataSource = GetTable(GetFilterSQL(FifthSelectSQL, FifthFilterField, ForthSelectedValue), this.ConnectionString, this.dbkind);
                            DropDownList5.DataTextField = FifthDataTextField;
                            DropDownList5.DataValueField = FifthDataValueField;
                            DropDownList5.DataBind();
                            DropDownList5.SelectedValue = SelectedVal5;
                        }
                    }
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            int SelIdx = FirstItemSelected ? 0 : -1;
            if (NeedValidation)
            {
                if (DropDownListLevel == DropDownLevel.Two)
                {
                    DropDownList1.Attributes.Add("onchange", "showNext(this.options[this.selectedIndex].value,'" + DropDownList2.ClientID + "','" + DropDownList3.ClientID + "','" + DropDownList4.ClientID + "','" + DropDownList5.ClientID + "',1," + SelIdx + ");AlertValidation();");
                }
                if (DropDownListLevel == DropDownLevel.Three)
                {
                    DropDownList1.Attributes.Add("onchange", "showNext(this.options[this.selectedIndex].value,'" + DropDownList2.ClientID + "','" + DropDownList3.ClientID + "','" + DropDownList4.ClientID + "','" + DropDownList5.ClientID + "',1," + SelIdx + ");AlertValidation();");
                    DropDownList2.Attributes.Add("onchange", "showNext(this.options[this.selectedIndex].value,'" + DropDownList3.ClientID + "','" + DropDownList4.ClientID + "','" + DropDownList5.ClientID + "',null,2," + SelIdx + ");AlertValidation();");
                }
                if (DropDownListLevel == DropDownLevel.Four)
                {
                    DropDownList1.Attributes.Add("onchange", "showNext(this.options[this.selectedIndex].value,'" + DropDownList2.ClientID + "','" + DropDownList3.ClientID + "','" + DropDownList4.ClientID + "','" + DropDownList5.ClientID + "',1," + SelIdx + ");AlertValidation();");
                    DropDownList2.Attributes.Add("onchange", "showNext(this.options[this.selectedIndex].value,'" + DropDownList3.ClientID + "','" + DropDownList4.ClientID + "','" + DropDownList5.ClientID + "',null,2," + SelIdx + ");AlertValidation();");
                    DropDownList3.Attributes.Add("onchange", "showNext(this.options[this.selectedIndex].value,'" + DropDownList4.ClientID + "','" + DropDownList5.ClientID + "',null,null,3," + SelIdx + ");AlertValidation();");
                }
                if (DropDownListLevel == DropDownLevel.Five)
                {
                    DropDownList1.Attributes.Add("onchange", "showNext(this.options[this.selectedIndex].value,'" + DropDownList2.ClientID + "','" + DropDownList3.ClientID + "','" + DropDownList4.ClientID + "','" + DropDownList5.ClientID + "',1," + SelIdx + ");AlertValidation();");
                    DropDownList2.Attributes.Add("onchange", "showNext(this.options[this.selectedIndex].value,'" + DropDownList3.ClientID + "','" + DropDownList4.ClientID + "','" + DropDownList5.ClientID + "',null,2," + SelIdx + ");AlertValidation();");
                    DropDownList3.Attributes.Add("onchange", "showNext(this.options[this.selectedIndex].value,'" + DropDownList4.ClientID + "','" + DropDownList5.ClientID + "',null,null,3," + SelIdx + ");AlertValidation();");
                    DropDownList4.Attributes.Add("onchange", "showNext(this.options[this.selectedIndex].value,'" + DropDownList5.ClientID + "',null,null,null,4," + SelIdx + ");AlertValidation();");
                }
            }
            else
            {
                if (DropDownListLevel == DropDownLevel.Two)
                {
                    DropDownList1.Attributes.Add("onchange", "showNext(this.options[this.selectedIndex].value,'" + DropDownList2.ClientID + "','" + DropDownList3.ClientID + "','" + DropDownList4.ClientID + "','" + DropDownList5.ClientID + "',1," + SelIdx + ");");
                }
                if (DropDownListLevel == DropDownLevel.Three)
                {
                    DropDownList1.Attributes.Add("onchange", "showNext(this.options[this.selectedIndex].value,'" + DropDownList2.ClientID + "','" + DropDownList3.ClientID + "','" + DropDownList4.ClientID + "','" + DropDownList5.ClientID + "',1," + SelIdx + ");");
                    DropDownList2.Attributes.Add("onchange", "showNext(this.options[this.selectedIndex].value,'" + DropDownList3.ClientID + "','" + DropDownList4.ClientID + "','" + DropDownList5.ClientID + "',null,2," + SelIdx + ");");
                }
                if (DropDownListLevel == DropDownLevel.Four)
                {
                    DropDownList1.Attributes.Add("onchange", "showNext(this.options[this.selectedIndex].value,'" + DropDownList2.ClientID + "','" + DropDownList3.ClientID + "','" + DropDownList4.ClientID + "','" + DropDownList5.ClientID + "',1," + SelIdx + ");");
                    DropDownList2.Attributes.Add("onchange", "showNext(this.options[this.selectedIndex].value,'" + DropDownList3.ClientID + "','" + DropDownList4.ClientID + "','" + DropDownList5.ClientID + "',null,2," + SelIdx + ");");
                    DropDownList3.Attributes.Add("onchange", "showNext(this.options[this.selectedIndex].value,'" + DropDownList4.ClientID + "','" + DropDownList5.ClientID + "',null,null,3," + SelIdx + ");");
                }
                if (DropDownListLevel == DropDownLevel.Five)
                {
                    DropDownList1.Attributes.Add("onchange", "showNext(this.options[this.selectedIndex].value,'" + DropDownList2.ClientID + "','" + DropDownList3.ClientID + "','" + DropDownList4.ClientID + "','" + DropDownList5.ClientID + "',1," + SelIdx + ");");
                    DropDownList2.Attributes.Add("onchange", "showNext(this.options[this.selectedIndex].value,'" + DropDownList3.ClientID + "','" + DropDownList4.ClientID + "','" + DropDownList5.ClientID + "',null,2," + SelIdx + ");");
                    DropDownList3.Attributes.Add("onchange", "showNext(this.options[this.selectedIndex].value,'" + DropDownList4.ClientID + "','" + DropDownList5.ClientID + "',null,null,3," + SelIdx + ");");
                    DropDownList4.Attributes.Add("onchange", "showNext(this.options[this.selectedIndex].value,'" + DropDownList5.ClientID + "',null,null,null,4," + SelIdx + ");");
                }
            }
            RegisterJavaScript.RegisterContolIncludeScript(Page);
            if (NeedValidation)
            { base.OnPreRender(e); }
        }

        protected override void CreateChildControls()
        {
            EnsureChildControls();

            if (!this.DesignMode)
            {
                if (Context.Request.Browser.Type.IndexOf("IE") != -1 && float.Parse(Context.Request.Browser.Version) < 8)
                {
                    Table1.Attributes["style"] += "display:inline;vertical-align:top;";
                }
                else
                {
                    Table1.Attributes["style"] += "display:inline-block;vertical-align:top;";
                }
            }
            else
            {
                Table1.Attributes["style"] += "display:inline;vertical-align:top;";
            }
            TableCell1.BorderWidth = this.HasBorder ? 1 : 0;
            TableCell1.BorderStyle = BorderStyle.Solid;
            TableCell1.BorderColor = Color.FromArgb(99, 99, 99);
            TableCell1.BackColor = this.TitleBackColor;
            TableCell1.ForeColor = this.TitleForeColor;
            TableCell1.CssClass = this.CssClass;
            TableCell2.BorderWidth = this.HasBorder ? 1 : 0;
            TableCell2.BorderStyle = BorderStyle.Solid;
            TableCell2.BorderColor = Color.FromArgb(99, 99, 99);
            TableCell2.ForeColor = this.TitleForeColor;
            Label1.Width = this.TitleWidth;
            Label1.Visible = this.IsShowTitle;
            Label2.Width = this.TitleWidth;
            //Label2.Visible = this.IsShowTitle;
            Label2.Attributes["style"] += "vertical-align:middle;";
            Label3.Width = this.TitleWidth;
            //Label3.Visible = this.IsShowTitle;
            Label3.Attributes["style"] += "vertical-align:middle;";
            Label4.Width = this.TitleWidth;
            //Label4.Visible = this.IsShowTitle;
            Label4.Attributes["style"] += "vertical-align:middle;";
            Label5.Width = this.TitleWidth;
            //Label5.Visible = this.IsShowTitle;
            Label5.Attributes["style"] += "vertical-align:middle;";
            TableCell1.Visible = this.IsShowTitle;
            Label1.Font.Size = this.Font.Size.IsEmpty ? FontUnit.Small : this.Font.Size;
            Label2.Font.Size = this.Font.Size.IsEmpty ? FontUnit.Small : this.Font.Size;
            Label3.Font.Size = this.Font.Size.IsEmpty ? FontUnit.Small : this.Font.Size;
            Label4.Font.Size = this.Font.Size.IsEmpty ? FontUnit.Small : this.Font.Size;
            Label5.Font.Size = this.Font.Size.IsEmpty ? FontUnit.Small : this.Font.Size;
            Label1.Attributes["style"] += "text-align:" + _TitleAlign + ";";
            Label2.Attributes["style"] += "text-align:" + _TitleAlign + ";";
            Label3.Attributes["style"] += "text-align:" + _TitleAlign + ";";
            Label4.Attributes["style"] += "text-align:" + _TitleAlign + ";";
            Label5.Attributes["style"] += "text-align:" + _TitleAlign + ";";
            DropDownList1.Width = DropDownWidth;
            DropDownList2.Width = DropDownWidth;
            DropDownList3.Width = DropDownWidth;
            DropDownList4.Width = DropDownWidth;
            DropDownList5.Width = DropDownWidth;

            string LineText = DisplayOrientation == EDisplayOrientation.Vertical ? "<br>" : "";
            TableCell1.Controls.Clear();
            TableCell2.Controls.Clear();

            if (DisplayOrientation == EDisplayOrientation.Horizontal)
            {
                TableCell1.Controls.Add(Label1);
                TableCell2.Controls.Add(DropDownList1);
                TableCell2.Controls.Add(PlaceHolder1);
                TableCell2.Controls.Add(new LiteralControl(LineText));
                TableCell2.Controls.Add(Label2);
                TableCell2.Controls.Add(DropDownList2);
                TableCell2.Controls.Add(PlaceHolder2);
                TableCell2.Controls.Add(new LiteralControl(LineText));
                TableCell2.Controls.Add(Label3);
                TableCell2.Controls.Add(DropDownList3);
                TableCell2.Controls.Add(PlaceHolder3);
                TableCell2.Controls.Add(new LiteralControl(LineText));
                TableCell2.Controls.Add(Label4);
                TableCell2.Controls.Add(DropDownList4);
                TableCell2.Controls.Add(PlaceHolder4);
                TableCell2.Controls.Add(new LiteralControl(LineText));
                TableCell2.Controls.Add(Label5);
                TableCell2.Controls.Add(DropDownList5);
                TableCell2.Controls.Add(PlaceHolder5);

            }
            else if (DisplayOrientation == EDisplayOrientation.Vertical)
            {
                if (_DropDownListLevel == DropDownLevel.Five)
                {
                    TableCell1.Controls.Add(Label1);
                    TableCell1.Controls.Add(new LiteralControl(LineText));
                    TableCell1.Controls.Add(Label2);
                    TableCell1.Controls.Add(new LiteralControl(LineText));
                    TableCell1.Controls.Add(Label3);
                    TableCell1.Controls.Add(new LiteralControl(LineText));
                    TableCell1.Controls.Add(Label4);
                    TableCell1.Controls.Add(new LiteralControl(LineText));
                    TableCell1.Controls.Add(Label5);
                    TableCell2.Controls.Add(DropDownList1);
                    TableCell2.Controls.Add(PlaceHolder1);
                    TableCell2.Controls.Add(new LiteralControl(LineText));
                    TableCell2.Controls.Add(DropDownList2);
                    TableCell2.Controls.Add(PlaceHolder2);
                    TableCell2.Controls.Add(new LiteralControl(LineText));
                    TableCell2.Controls.Add(DropDownList3);
                    TableCell2.Controls.Add(PlaceHolder3);
                    TableCell2.Controls.Add(new LiteralControl(LineText));
                    TableCell2.Controls.Add(DropDownList4);
                    TableCell2.Controls.Add(PlaceHolder4);
                    TableCell2.Controls.Add(new LiteralControl(LineText));
                    TableCell2.Controls.Add(DropDownList5);
                    TableCell2.Controls.Add(PlaceHolder5);
                }
                else if (_DropDownListLevel == DropDownLevel.Four)
                {
                    TableCell1.Controls.Add(Label1);
                    TableCell1.Controls.Add(new LiteralControl(LineText));
                    TableCell1.Controls.Add(Label2);
                    TableCell1.Controls.Add(new LiteralControl(LineText));
                    TableCell1.Controls.Add(Label3);
                    TableCell1.Controls.Add(new LiteralControl(LineText));
                    TableCell1.Controls.Add(Label4);
                    TableCell2.Controls.Add(DropDownList1);
                    TableCell2.Controls.Add(PlaceHolder1);
                    TableCell2.Controls.Add(new LiteralControl(LineText));
                    TableCell2.Controls.Add(DropDownList2);
                    TableCell2.Controls.Add(PlaceHolder2);
                    TableCell2.Controls.Add(new LiteralControl(LineText));
                    TableCell2.Controls.Add(DropDownList3);
                    TableCell2.Controls.Add(PlaceHolder3);
                    TableCell2.Controls.Add(new LiteralControl(LineText));
                    TableCell2.Controls.Add(DropDownList4);
                    TableCell2.Controls.Add(PlaceHolder4);
                }
                else if (_DropDownListLevel == DropDownLevel.Three)
                {
                    TableCell1.Controls.Add(Label1);
                    TableCell1.Controls.Add(new LiteralControl(LineText));
                    TableCell1.Controls.Add(Label2);
                    TableCell1.Controls.Add(new LiteralControl(LineText));
                    TableCell1.Controls.Add(Label3);
                    TableCell2.Controls.Add(DropDownList1);
                    TableCell2.Controls.Add(PlaceHolder1);
                    TableCell2.Controls.Add(new LiteralControl(LineText));
                    TableCell2.Controls.Add(DropDownList2);
                    TableCell2.Controls.Add(PlaceHolder2);
                    TableCell2.Controls.Add(new LiteralControl(LineText));
                    TableCell2.Controls.Add(DropDownList3);
                    TableCell2.Controls.Add(PlaceHolder3);
                }
                else if (_DropDownListLevel == DropDownLevel.Two)
                {
                    TableCell1.Controls.Add(Label1);
                    TableCell1.Controls.Add(new LiteralControl(LineText));
                    TableCell1.Controls.Add(Label2);
                    TableCell2.Controls.Add(DropDownList1);
                    TableCell2.Controls.Add(PlaceHolder1);
                    TableCell2.Controls.Add(new LiteralControl(LineText));
                    TableCell2.Controls.Add(DropDownList2);
                    TableCell2.Controls.Add(PlaceHolder2);
                }
                else if (_DropDownListLevel == DropDownLevel.One)
                {
                    TableCell1.Controls.Add(Label1);
                    TableCell2.Controls.Add(DropDownList1);
                    TableCell2.Controls.Add(PlaceHolder1);
                }
            }
            TableRow.Cells.Add(TableCell1);
            TableRow.Cells.Add(TableCell2);
            Table1.Rows.Add(TableRow);
            this.Controls.Add(Table1);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            Table1.RenderControl(writer);
        }

        protected override void LoadControlState(object savedState)
        {
            object[] allStates = (object[])savedState;
            _FirstSelectedUniqueID = (string)allStates[0];
            _FirstSelectedClientID = (string)allStates[1];
            _SecondSelectedUniqueID = (string)allStates[2];
            _SecondSelectedClientID = (string)allStates[3];
            _ThirdSelectedUniqueID = (string)allStates[4];
            _ThirdSelectedClientID = (string)allStates[5];
            _ForthSelectedUniqueID = (string)allStates[6];
            _ForthSelectedClientID = (string)allStates[7];
            _FifthSelectedUniqueID = (string)allStates[8];
            _FifthSelectedClientID = (string)allStates[9];
            SelectedVal1 = Context.Request.Form[_FirstSelectedUniqueID] != null ? Context.Request.Form[_FirstSelectedUniqueID].ToString() : null;
            SelectedVal2 = Context.Request.Form[_SecondSelectedUniqueID] != null ? Context.Request.Form[_SecondSelectedUniqueID].ToString() : null;
            SelectedVal3 = Context.Request.Form[_ThirdSelectedUniqueID] != null ? Context.Request.Form[_ThirdSelectedUniqueID].ToString() : null;
            SelectedVal4 = Context.Request.Form[_ForthSelectedUniqueID] != null ? Context.Request.Form[_ForthSelectedUniqueID].ToString() : null;
            SelectedVal5 = Context.Request.Form[_FifthSelectedUniqueID] != null ? Context.Request.Form[_FifthSelectedUniqueID].ToString() : null;
        }

        protected override object SaveControlState()
        {
            object[] allStates = new object[10];
            allStates[0] = DropDownList1.UniqueID;
            allStates[1] = DropDownList1.ClientID;
            allStates[2] = DropDownList2.UniqueID;
            allStates[3] = DropDownList2.ClientID;
            allStates[4] = DropDownList3.UniqueID;
            allStates[5] = DropDownList3.ClientID;
            allStates[6] = DropDownList4.UniqueID;
            allStates[7] = DropDownList4.ClientID;
            allStates[8] = DropDownList5.UniqueID;
            allStates[9] = DropDownList5.ClientID;
            return allStates;
        }

        #endregion

        #region Privated Properties & Methods

        private void ChildControlsSetting()
        {
            //設定Table1
            Table1.ID = "Table1";

            //設定Label1
            Label1.ID = "Label1";

            //DropDownList1
            DropDownList1.ID = "DropDownList1";
            DropDownList1.EnableViewState = true;

            //設定Label2
            Label2.ID = "Label2";

            //DropDownList2
            DropDownList2.ID = "DropDownList2";
            DropDownList2.EnableViewState = true;

            //設定Label3
            Label3.ID = "Label3";

            //DropDownList3
            DropDownList3.ID = "DropDownList3";
            DropDownList3.EnableViewState = true;

            //設定Label4
            Label4.ID = "Label4";

            //DropDownList4
            DropDownList4.ID = "DropDownList4";
            DropDownList4.EnableViewState = true;

            //設定Label5
            Label5.ID = "Label5";

            //DropDownList5
            DropDownList5.ID = "DropDownList5";
            DropDownList5.EnableViewState = true;
        }

        private DataTable GetTable(string SQL, string ConnectionString, dbkind dbkind)
        {
            dbkind Datatype = dbkind;
            IDbConnection ICon;
            IDataAdapter IDa;
            DataSet DS = new DataSet();   
            if (Datatype == dbkind.SQL_Server)
            {
                ICon = new SqlConnection(ConnectionString);
                IDa = new SqlDataAdapter(SQL, (SqlConnection)ICon);
                ICon.Open();
                IDa.Fill(DS);
                ICon.Close();
            }
            else if (Datatype == dbkind.Oracle)
            {
                ICon = new OracleConnection(ConnectionString);
                IDa = new OracleDataAdapter(SQL, (OracleConnection)ICon);
                ICon.Open();
                IDa.Fill(DS);
                ICon.Close();
            }                 
            return DS.Tables[0];
        }

        private string GetFilterSQL(string SelectSQL, string QueryField, string QueryValue)
        {
            string SQL = "";
            int SortIdx = SelectSQL.IndexOf("order by");
            switch (SortIdx)
            {
                case -1:
                    if (SelectSQL.IndexOf("where") != -1)
                        SQL = string.Format(SelectSQL + @" and {0}='{1}'", QueryField, QueryValue);
                    else
                        SQL = string.Format(SelectSQL + @" where {0}='{1}'", QueryField, QueryValue);
                    break;

                default:
                    string Sort = SelectSQL.Substring(SortIdx);
                    string NewSelectSQL = SelectSQL.Replace(Sort, "");
                    if (NewSelectSQL.IndexOf("where") != -1)
                        SQL = string.Format(NewSelectSQL + @" and {0}='{1}' " + Sort, QueryField, QueryValue);
                    else
                        SQL = string.Format(NewSelectSQL + @" where {0}='{1}' " + Sort, QueryField, QueryValue);
                    break;
            }
            return SQL;
        }

        #endregion

    }
}