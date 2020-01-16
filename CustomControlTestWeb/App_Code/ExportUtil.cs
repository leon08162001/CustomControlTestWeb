using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Configuration;
using System.Web.UI.WebControls;
//using EAIUtility;

   public static class ExportUtil
    {
       private static DataTable dt;
       private static FileInfo FileInfo;
       private static StreamWriter SW;

        /// <summary>
        /// 將資料輸出至檔案
        /// </summary>
        public static void ExportDataToFile(IEnumerable<string> Data, string FileName)
        {
            string sLogMsg;
            try
            {
                FileInfo = new FileInfo(FileName);
                if (FileInfo.Exists) FileInfo.Delete();

                SW = FileInfo.CreateText();  
                foreach (string row in Data)
                {
                    SW.WriteLine(row);
                }
            }
            catch (Exception ex)
            {
                sLogMsg = String.Format("Method[{0}]執行錯誤-[{1}]", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
                //LogUtility.LogMessage(LogUtility.LogType.Error, null, sLogMsg);
            }
            finally
            {
                SW.Close();
            }
        }

        /// <summary>
        /// 輸出資料表資料成特定格式資料
        /// </summary>
        public static IEnumerable<string> ExportTableToData(DataTable Table, string ColumnSplit)
        {
            List<string> DataList = new List<string>();
            IEnumerable<string> IEnumData = DataList.AsEnumerable();
            string sLogMsg;
            try
            {
                sLogMsg = String.Format("Method[{0}]開始執行]", System.Reflection.MethodBase.GetCurrentMethod().Name);
                //LogUtility.LogMessage(LogUtility.LogType.Info, null, sLogMsg);
                Console.WriteLine(sLogMsg);
                dt = Table;
                    sLogMsg = String.Format("DataTable筆數-[{0}]", dt.Rows.Count);
                    //LogUtility.LogMessage(LogUtility.LogType.Info, null, sLogMsg);
                    if (dt.Rows.Count == 0)   //無資料時
                    {

                    }
                    else
                    {
                        IEnumerable<DataRow> IEnumDataRow = dt.AsEnumerable();
                        //輸出欄位名稱
                        string sColumnName="";
                        foreach (DataColumn Column in dt.Columns)
                        {
                            sColumnName += Column.ColumnName + ColumnSplit;
                        }
                        if (sColumnName != "")
                        {
                            sColumnName = sColumnName.Substring(0, sColumnName.Length - 1);
                        }
                        if (sColumnName != "")
                        {
                            DataList.Add(sColumnName);
                        }
                        //輸出資料
                        foreach (DataRow dr in IEnumDataRow)
                        {
                            string sData = "";
                            foreach (DataColumn dc in dt.Columns)
                            {
                                string sColumnData = Convert.ToString(dr[dc]).ToLower() == "true" ? "1" : Convert.ToString(dr[dc]).ToLower() == "false" ? "0" : Convert.ToString(dr[dc]);
                                sColumnData = sColumnData.Replace("\n", "");    //換行字元必須取代,否則輸出會換行
                                sColumnData = sColumnData.Replace("\r\n", "");
                                sData += sColumnData + ColumnSplit;
                            }
                            if (sData != "")
                            {
                                sData = sData.Substring(0, sData.Length - 1);
                            }
                            if (sData != "")
                            {
                                DataList.Add(sData);
                            }
                        }
                        IEnumData = DataList.AsEnumerable();
                    }
               
            }
            catch (Exception ex)
            {
                sLogMsg = String.Format("Method[{0}]執行錯誤-[{1}]", System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message + ex.StackTrace);
                //LogUtility.LogMessage(LogUtility.LogType.Error, null, sLogMsg);
            }
            return IEnumData;
        }

        /// <summary>
        /// 輸出GridView資料成DataTable(僅適用TemplateField內含label)
        /// </summary>
        public static DataTable ExportGridViewToTable(GridView ctlGridView)
        {
            DataTable DT = new DataTable();
            //取得gridview標題欄位加入到DataTable欄位名稱
            GridViewRow HeaderRow = ctlGridView.HeaderRow; 
            for (int i = 1; i < HeaderRow.Cells.Count; i++)
            {
                TableCell col = HeaderRow.Cells[i];
                string sColName = col.Text;
                
                if (sColName.IndexOf("<br/>")!=-1)
                {
                    sColName = sColName.Replace("<br/>", ",");
                    List<string> newColNames = sColName.Split(new char[] { ',' }).ToList();
                    foreach (string snewCol in newColNames)
                    {
                        DataColumn DC = new DataColumn(snewCol);
                        DT.Columns.Add(DC);
                    }
                }
                else
                {
                    DataColumn DC = new DataColumn(sColName);
                    DT.Columns.Add(DC);
                }
            }
            //取得gridview資料列加入至DataTable
            foreach (GridViewRow row in ctlGridView.Rows)
            {
                DataRow DR = DT.NewRow();
                List<string> Data = new List<string>();
                for (int i = 1; i < row.Cells.Count; i++)
                {
                    TableCell col = row.Cells[i];
                    for (int j = 1; j < col.Controls.Count; j++)
                    {
                        if (typeof(Label) == col.Controls[j].GetType())
                        {
                            Label ctrl= (Label)col.Controls[j];
                            string sColValue = ctrl.Text.Trim();
                            Data.Add(sColValue);
                        }
                    }
                }
                DR.ItemArray = Data.ToArray();
                DT.Rows.Add(DR);
            }
                return DT;
        }

        /// <summary>
        /// 輸出資料表資料成特定格式資料
        /// </summary>
        public static DataTable GetTableSchema()
        {
            return dt.Clone();
        }

        /// <summary>
        /// oPage 該網頁
        /// strRealFile 實際檔案 (路徑 + 檔名)
        /// downloadfilename 下載後變更的檔名
        /// </summary>
        /// <param name="strDate"></param>
        /// <param name="intType"></param>
        /// <returns></returns>
        public static void DownloadFile(System.Web.UI.Page oPage, string strRealFile, string downloadfilename)
        {
            oPage.Response.ClearHeaders();
            oPage.Response.Clear();
            oPage.Response.Expires = 0;
            oPage.Response.Buffer = true;
            oPage.Response.AddHeader("Accept-Language", "zh-tw");
            //檔案名稱
            oPage.Response.HeaderEncoding = System.Text.Encoding.GetEncoding(950);
            oPage.Response.AddHeader("content-disposition", "attachment; filename=\"" + System.Web.HttpUtility.UrlEncode(downloadfilename) + "\"");
            oPage.Response.ContentType = "Application/octet-stream";
            //檔案內容
            oPage.Response.BinaryWrite(System.IO.File.ReadAllBytes(strRealFile));
            FileInfo FileInfo = new FileInfo(strRealFile);
            FileInfo.Delete();
            oPage.Response.End(); 
        }
    }
