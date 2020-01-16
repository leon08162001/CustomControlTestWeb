using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using EAIUtility;
using System.Configuration;
using AjaxPro;
using System.IO;

namespace FCSWeb
{
    public partial class PageMakerDemo : System.Web.UI.Page
    {
        protected static string Guid = "";
        public static bool bFinishQry = false;
        private const string m_DBName = "FCSDB";
        private string m_QrySql = "SELECT [FileID],[FTPDirSource],[FileDescription],[FTPDirTarget],[FTPFileTarget],[FTPFileSource]," +
                          "[RPTFlag],[ConvertFlag],[YYYYMMDDSource],[YYYYMMDDTarget],[INTNo] FROM [FCS].[dbo].[View_FileListBatch]";
        private char[] m_FieldValueSplit = new char[] { ',' };
        private string gvSortExpression;
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(PageMakerDemo));
            SetQryDescription(ddlQryField.SelectedItem.Value);
            //if (Page.IsPostBack && ViewState["DT"] != null)
            //{
            //    GridQryResult.DataSource = (DataTable)ViewState["DT"];
            //    PageMaker1.DataBind();
            //    PageMaker2.DataBind();
            //}
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Page.IsPostBack && ViewState["DT"] != null)
            {
                GridQryResult.DataSource = (DataTable)ViewState["DT"];
                PageMaker1.DataBind();
                PageMaker2.DataBind();
            }
        }

        /// <summary>
        /// Long running background operation
        /// </summary>
        [AjaxMethod()]
        public bool DoWork(string[] Params)
        {
            bool bResult = false;
            try
            {
                while (Guid == "")
                {
                    System.Threading.Thread.Sleep(1);
                }

                string TempGuid = Guid;
                FileInfo TempGuidFile = new FileInfo(Server.MapPath("~") + @"\Progress\" + TempGuid);
                while (TempGuidFile.Exists)
                {
                    TempGuidFile = new FileInfo(Server.MapPath("~") + @"\Progress\" + TempGuid);
                }
                //while (Guid != "" && Guid != "XX")
                //{

                //}
                //if (Guid == "") bResult = true;
                //else if (Guid == "XX") bResult = false;
                bResult = true;
            }
            catch (Exception ex)
            {
                bResult = false;
            }
            return bResult;
        }

        protected void ddlQryField_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddlQryField = (DropDownList)sender;
            if (ddlQryField.SelectedValue == "0" || ddlQryField.SelectedValue == "1" || ddlQryField.SelectedValue == "2" || ddlQryField.SelectedValue == "3" || ddlQryField.SelectedValue == "4")
            {
                txtQryValue.Visible = true;
            }
            else if (ddlQryField.SelectedValue == "5")
            {
                txtQryValue.Visible = false;
            }
            SetQryDescription(ddlQryField.SelectedItem.Value);
        }

        protected void btnQry_Click(object sender, EventArgs e)
        {
            ViewState["SortField"] = null;
            Guid = System.Guid.NewGuid().ToString();
            string TempGuid = Guid;
            FileInfo TempGuidFile = new FileInfo(Server.MapPath("~") + @"\Progress\" + TempGuid);
            try
            {
                using (TempGuidFile.Create()) { }
                string sSql = GetQuerySQL();
                if (!sSql.Equals(""))
                {
                    DoQuery(sSql);
                    //System.Threading.Thread.Sleep(5000);
                    DoBinding();
                }
                palShowResult.Visible = PageMaker1.Visible;
                if (!palShowResult.Visible)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "NoData", "<script>window.alert('查無資料')</script>", false);
                }
                TempGuidFile.Delete();
                Guid = "";
            }
            catch (IOException IOE)
            {
                //Guid = "XX";
            }
            catch (Exception ex)
            {
                //Guid = "XX";
            }
        }

        protected void btnExportData_Click(object sender, EventArgs e)
        {
            if (ViewState["DT"] != null)
            {
                DataTable DT = (DataTable)ViewState["DT"];
                if (DT.Rows.Count > 0 && ViewState["ExportDT"] == null)
                {
                    GridQryResult.DataSource = (DataTable)ViewState["DT"];
                    GridQryResult.DataBind();
                    ViewState["ExportDT"] = ExportUtil.ExportGridViewToTable(GridQryResult);
                }
                if (ViewState["ExportDT"] != null)
                {
                    string m_FileName = ConfigurationSettings.AppSettings["ExportFileName"];
                    string sTime = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    string FileName = Server.MapPath(@"~\Export\") + m_FileName + sTime + ".txt";
                    DataTable ExportDT = (DataTable)ViewState["ExportDT"];
                    IEnumerable<string> IData = ExportUtil.ExportTableToData(ExportDT, ",");
                    ExportUtil.ExportDataToFile(IData, FileName);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "ExportData", "<script>window.alert('查詢資料匯出完成')</script>", false);
                    string DownLoadFileName = m_FileName + ".txt";
                    ExportUtil.DownloadFile(this.Page, FileName, DownLoadFileName);
                }
            }
        }

        /// <summary>
        /// 依據SQL查詢資料並將資料保存至ViewState
        /// </summary>
        private void DoQuery(string sSQL)
        {
            try
            {
                DataTable DT = BspDB.ExecuteDataSet(System.Data.CommandType.Text, sSQL, m_DBName).Tables[0];
                DT = SetTableSerialNo(DT);
                ViewState["DT"] = DT;
            }
            catch (Exception ex)
            {
                LogUtility.LogMessage(LogUtility.LogType.Error, null, ex.Message);
                throw ex;
            }
        }

        private DataTable SetTableSerialNo(DataTable DT)
        {
            System.Type typeInt32 = System.Type.GetType("System.Int32");
            DataColumn DC = new DataColumn("SerialNo", typeInt32);
            DT.Columns.Add(DC);
            int i = 1;
            foreach (DataRow row in DT.Rows)
            {
                row["SerialNo"] = i;
                i++;
            }
            return DT;
        }

        /// <summary>
        /// 將GridView資料繫結
        /// </summary>
        private void DoBinding()
        {
            if (ViewState["DT"] != null)
            {
                GridQryResult.DataSource = (DataTable)ViewState["DT"];
                //GridQryResult.DataBind();
                PageMaker1.DataBind();
                PageMaker2.DataBind();
            }
        }


        /// <summary>
        /// 依據UI操作產生查詢SQL
        /// </summary>
        private string GetQuerySQL(string QryField, string QryValue, string SortField)
        {
            string sSql = "";
            //查詢FileID,INTNo
            if (QryField == "0" || QryField == "1")
            {
                string FiledValues = "";
                List<string> TempValues = QryValue.Trim().Split(m_FieldValueSplit).ToList<string>();
                foreach (string Val in TempValues)
                {
                    if (!Val.Equals(""))
                    {
                        if (QryField == "0")
                        {
                            FiledValues += string.Format("'FCS-{0}'", Val) + ",";
                        }
                        else if (QryField == "1")
                            FiledValues += string.Format("'{0}'", Val) + ",";
                    }
                }
                if (!FiledValues.Equals(""))
                {
                    FiledValues = FiledValues.Substring(0, FiledValues.Length - 1);
                    sSql = string.Format("{0} where {1} in ({2}) order by {3}", m_QrySql,
                           GetQryFieldName(QryField), FiledValues, GetSortFieldName(SortField));
                }
                else if (FiledValues.Equals(""))
                {
                    sSql = string.Format("{0} order by {1}", m_QrySql, GetSortFieldName(SortField));
                }
            }
            //查詢FTP目錄
            else if (QryField == "2")
            {
                string FiledValues = QryValue.Trim();
                if (!FiledValues.Equals(""))
                {
                    sSql = string.Format("{0} where FTPDirSource like '%{1}%' OR FTPDirTarget like '%{2}%' order by {3}", m_QrySql,
                           FiledValues, FiledValues, GetSortFieldName(SortField));
                }
                else if (FiledValues.Equals(""))
                {
                    sSql = string.Format("{0} order by {1}", m_QrySql, GetSortFieldName(SortField));
                }
            }
            //查詢FTP上行檔案,FTP下行檔案
            else if (QryField == "3" || QryField == "4")
            {
                string FiledValues = QryValue.Trim();
                if (!FiledValues.Equals(""))
                {
                    sSql = string.Format("{0} where {1} like '%{2}%' order by {3}", m_QrySql,
                           GetQryFieldName(QryField), FiledValues, GetSortFieldName(SortField));
                }
                else if (FiledValues.Equals(""))
                {
                    sSql = string.Format("{0} order by {1}", m_QrySql, GetSortFieldName(SortField));
                }
            }
            //查詢RPTFlag
            else if (QryField == "5")
            {
                string FiledValues = "Y";
                sSql = string.Format("{0} where {1} = '{2}' order by {3}", m_QrySql,
                       GetQryFieldName(QryField), FiledValues, GetSortFieldName(SortField));
            }
            return sSql;
        }

        /// <summary>
        /// 依據UI操作產生查詢SQL
        /// </summary>
        private string GetQuerySQL()
        {
            string sSql = "";
            //查詢FileID,INTNo
            if (ddlQryField.SelectedItem.Value == "0" || ddlQryField.SelectedItem.Value == "1")
            {
                string FiledValues = "";
                List<string> TempValues = txtQryValue.Text.Trim().Split(m_FieldValueSplit).ToList<string>();
                foreach (string Val in TempValues)
                {
                    if (!Val.Equals(""))
                    {
                        if (ddlQryField.SelectedItem.Value == "0")
                        {
                            FiledValues += string.Format("'FCS-{0}'", Val) + ",";
                        }
                        else if (ddlQryField.SelectedItem.Value == "1")
                            FiledValues += string.Format("'{0}'", Val) + ",";
                    }
                }
                if (!FiledValues.Equals(""))
                {
                    FiledValues = FiledValues.Substring(0, FiledValues.Length - 1);
                    sSql = string.Format("{0} where {1} in ({2}) order by {3}", m_QrySql,
                           GetQryFieldName(ddlQryField.SelectedItem.Value), FiledValues, GetSortFieldName(ddlSortField.SelectedItem.Value));
                }
                else if (FiledValues.Equals(""))
                {
                    sSql = string.Format("{0} order by {1}", m_QrySql, GetSortFieldName(ddlSortField.SelectedItem.Value));
                }
            }
            //查詢FTP目錄
            else if (ddlQryField.SelectedItem.Value == "2")
            {
                string FiledValues = txtQryValue.Text.Trim();
                if (!FiledValues.Equals(""))
                {
                    sSql = string.Format("{0} where FTPDirSource like '%{1}%' OR FTPDirTarget like '%{2}%' order by {3}", m_QrySql,
                           FiledValues, FiledValues, GetSortFieldName(ddlSortField.SelectedItem.Value));
                }
                else if (FiledValues.Equals(""))
                {
                    sSql = string.Format("{0} order by {1}", m_QrySql, GetSortFieldName(ddlSortField.SelectedItem.Value));
                }
            }
            //查詢FTP上行檔案,FTP下行檔案
            else if (ddlQryField.SelectedItem.Value == "3" || ddlQryField.SelectedItem.Value == "4")
            {
                string FiledValues = txtQryValue.Text.Trim();
                if (!FiledValues.Equals(""))
                {
                    sSql = string.Format("{0} where {1} like '%{2}%' order by {3}", m_QrySql,
                           GetQryFieldName(ddlQryField.SelectedItem.Value), FiledValues, GetSortFieldName(ddlSortField.SelectedItem.Value));
                }
                else if (FiledValues.Equals(""))
                {
                    sSql = string.Format("{0} order by {1}", m_QrySql, GetSortFieldName(ddlSortField.SelectedItem.Value));
                }
            }
            //查詢RPTFlag
            else if (ddlQryField.SelectedItem.Value == "5")
            {
                string FiledValues = "Y";
                sSql = string.Format("{0} where {1} = '{2}' order by {3}", m_QrySql,
                       GetQryFieldName(ddlQryField.SelectedItem.Value), FiledValues, GetSortFieldName(ddlSortField.SelectedItem.Value));
            }
            return sSql;
        }

        /// <summary>
        ///若gridview欄位為空值轉換成X
        /// </summary>
        protected string ConvertValueToX(string sValue)
        {
            string sConvertValueToX;
            if (sValue.Trim().Equals(""))
            {
                sConvertValueToX = "X";
            }
            else
            {
                sConvertValueToX = sValue;
            }
            return sConvertValueToX;
        }

        /// <summary>
        ///若gridview欄位為空值轉換成Space
        /// </summary>
        protected string ConvertValueToSpace(string sValue)
        {
            string sConvertValueToSpace;
            if (sValue.Trim().Equals(""))
            {
                sConvertValueToSpace = " ";
            }
            else
            {
                sConvertValueToSpace = sValue;
            }
            return sConvertValueToSpace;
        }

        /// <summary>
        ///針對FTP上行檔案及FTP下行檔案的值重新付值
        /// </summary>
        protected string CheckFolder_File(string sFolder, string sFile)
        {
            string sResult;
            if ((!sFolder.Equals("")) && sFile.Equals(""))
            {
                sResult = "X";
            }
            else
            {
                sResult = String.Format("{0}{1}", sFolder, sFile);
            }
            return sResult;
        }

        /// <summary>
        ///將UI上查詢欄位轉換成對應資料表的欄位名稱
        /// </summary>
        private string GetQryFieldName(string ddlQryFieldValue)
        {
            string sQryFieldName = "";
            if (ddlQryFieldValue == "0")
            {
                sQryFieldName = "FileID";
            }
            else if (ddlQryFieldValue == "1")
            {
                sQryFieldName = "INTNo";
            }
            else if (ddlQryFieldValue == "2")
            {
                sQryFieldName = "FTPDirSource";
            }
            else if (ddlQryFieldValue == "3")
            {
                sQryFieldName = "FTPFileSource";
            }
            else if (ddlQryFieldValue == "4")
            {
                sQryFieldName = "FTPFileTarget";
            }
            else if (ddlQryFieldValue == "5")
            {
                sQryFieldName = "RPTFlag";
            }
            return sQryFieldName;
        }

        /// <summary>
        ///將UI上排序欄位轉換成對應資料表的欄位名稱
        /// </summary>
        private string GetSortFieldName(string ddlSortFieldValue)
        {
            string sSortFieldName = "";
            if (ddlSortFieldValue == "0")
            {
                sSortFieldName = "FileID";
            }
            else if (ddlSortFieldValue == "1")
            {
                sSortFieldName = "FTPFileSource";
            }
            else if (ddlSortFieldValue == "2")
            {
                sSortFieldName = "FTPFileTarget";
            }
            return sSortFieldName;
        }

        /// <summary>
        ///建立查詢說明
        /// </summary>
        private void SetQryDescription(string ddlQryFieldValue)
        {
            if (ddlQryFieldValue == "0")
            {
                lblQryDesc.Text = "(請輸入右4碼,可多個值以逗號隔開 ex:0001,0002...)";
            }
            else if (ddlQryFieldValue == "1")
            {
                lblQryDesc.Text = "(可多個值以逗號隔開 ex:00,01...)";
            }
            else if (ddlQryFieldValue == "2")
            {
                lblQryDesc.Text = "(模糊查詢,僅接受一個值)";
            }
            else if (ddlQryFieldValue == "3")
            {
                lblQryDesc.Text = "(模糊查詢,僅接受一個值)";
            }
            else if (ddlQryFieldValue == "4")
            {
                lblQryDesc.Text = "(模糊查詢,僅接受一個值)";
            }
            else if (ddlQryFieldValue == "5")
            {
                lblQryDesc.Text = "";
            }
        }
        protected void GridQryResult_Sorting(object sender, GridViewSortEventArgs e)
        {
            ViewState["SortField"] = e.SortExpression;
            string sSql = GetQuerySQL();
            if (!sSql.Equals(""))
            {
                DoQuery(sSql);
                if (ViewState["DT"] != null)
                {
                    DataTable DT = (DataTable)ViewState["DT"];

                    if (ViewState["mySorting"] == null)
                    {
                        e.SortDirection = SortDirection.Descending;
                        ViewState["mySorting"] = "Descending";
                    }
                    else
                    {
                        //如果目前的排序方法，已經是「正排序」，那再度按下排序欄位之後，就變成「反排序」。
                        if (ViewState["mySorting"].ToString() == "Ascending")
                        {
                            e.SortDirection = SortDirection.Descending;
                            ViewState["mySorting"] = "Descending";
                        }
                        else
                        {
                            e.SortDirection = SortDirection.Ascending;
                            ViewState["mySorting"] = "Ascending";
                        }
                    }

                    if (e.SortDirection == SortDirection.Ascending)
                    {
                        DT.DefaultView.Sort = e.SortExpression + " asc";
                    }
                    else
                    {
                        DT.DefaultView.Sort = e.SortExpression + " desc";
                    }
                    ViewState["DT"] = DT.DefaultView.ToTable();
                    GridQryResult.DataSource = (DataTable)ViewState["DT"];
                    PageMaker1.DataBind();
                    PageMaker2.DataBind();

                    //if (e.SortExpression == "SerialNo")
                    //{
                    //    string s = GridQryResult.HeaderRow.Cells[1].Text;
                    //}
                    //else if (e.SortExpression == "FileID")
                    //{
                    //    string s = GridQryResult.HeaderRow.Cells[1].Text;
                    //}
                    //else if (e.SortExpression == "FileDescription")
                    //{
                    //    string s = GridQryResult.HeaderRow.Cells[2].Text;
                    //}
                }
            }
        }

        //protected void ShowSortedIcon(GridView gvData, GridViewRow gvRow)
        //{
        //    for (int index = 0; index <= gvData.Columns.Count - 1; index++)
        //    {
        //        if ((gvData.Columns[index].SortExpression == gvSortExpression) && (gvData.Columns[index].SortExpression != ""))
        //        {
        //            Image img = new Image();
        //            if ((string)ViewState["mySorting"] == "Ascending")
        //            {
        //                img.ImageUrl = "~/Images/sortascending.gif";
        //                img.ToolTip = "遞增排列";
        //            }
        //            else
        //            {
        //                img.ImageUrl = "~/Images/sortdescending.gif";
        //                img.ToolTip = "遞减排列";
        //            }
        //            gvRow.Cells[index].Controls.Add(img);
        //        }
        //    }
        //}
        protected void ShowSortedIcon(GridView gvData, GridViewRow gvRow)
        {
            if (ViewState["SortField"] != null)
            {
                for (int index = 0; index <= gvData.Columns.Count - 1; index++)
                {
                    if ((gvData.Columns[index].SortExpression == ViewState["SortField"].ToString()) && (gvData.Columns[index].SortExpression != ""))
                    {
                        Image img = new Image();
                        if ((string)ViewState["mySorting"] == "Ascending")
                        {
                            img.ImageUrl = "~/Images/sortascending.gif";
                            img.ToolTip = "遞增排列";
                        }
                        else
                        {
                            img.ImageUrl = "~/Images/sortdescending.gif";
                            img.ToolTip = "遞减排列";
                        }
                        gvRow.Cells[index].Controls.Add(img);
                    }
                }
            }
        }
        protected void GridQryResult_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                ShowSortedIcon(GridQryResult, e.Row);
            }
        }
        protected void PageMaker_CustomPageSize(object sender, APTemplate.PageSizeCustomEventArgs e)
        {
            DropDownList ddlPagSize = e.PageSizeSelect;
            //ddlPagSize.AppendDataBoundItems = true;
            DataTable dt = new DataTable();
            dt.Columns.Add("PageSize");
            DataRow dr = dt.NewRow();
            dr["PageSize"] = "10";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["PageSize"] = "20";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr["PageSize"] = "30";
            dt.Rows.Add(dr);
            foreach (DataRow Row in dt.Rows)
            {
                ddlPagSize.Items.Add(Row["PageSize"].ToString());
            }
            //ddlPagSize.DataSource = dt;
            //ddlPagSize.DataTextField = "PageSize";
            //ddlPagSize.DataValueField = "PageSize";
            //ddlPagSize.DataBind();

            //ddlPagSize.Items.Add("10");
            //ddlPagSize.Items.Add("20");
            //ddlPagSize.Items.Add("30");
            //ddlPagSize.Items.Add("40");
            //ddlPagSize.Items.Add("50");
        }
}
}
