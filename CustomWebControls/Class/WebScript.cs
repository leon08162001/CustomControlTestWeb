using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Specialized;

namespace APTemplate
{
    /// <summary>
    /// 列舉-按鈕型態
    /// </summary>
    public enum ButtonType
    {
        Select,
        Edit,
        Delete,
        Hyplink
    }

    /// <summary>
    /// 列舉-事件類型
    /// </summary>
    public enum EventType
    {
        OnClick,
        OnDblClick
    }

    public enum WindowType
    {
        /// <summary>
        /// 以window.open開啟新視窗
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 以window.showModalDiag開啟新視窗
        /// </summary>
        ModalDialog = 2,
        /// <summary>
        /// 以window.showModelessDiag開啟新視窗
        /// </summary>
        ModelessDialog = 3
    }

    public enum TargetType
    {
        /// <summary>
        /// 另開新視窗
        /// </summary>
        blank = 1,
        /// <summary>
        /// 開新視窗於父層視窗
        /// </summary>
        parent = 2,
        /// <summary>
        /// 開新視窗於目前視窗
        /// </summary>
        self = 3,
        /// <summary>
        /// 開新視窗於頂層視窗
        /// </summary>
        top = 4,
        /// <summary>
        /// 於指定的frame中開啟
        /// </summary>
        Frame = 5
    }

    public enum MessageType
    {
        /// <summary>
        /// 開啟訊息視窗
        /// </summary>
        Alert,
        /// <summary>
        /// 開啟確認視窗
        /// </summary>
        Confirm,
        /// <summary>
        /// 開啟輸入視窗
        /// </summary>
        Prompt
    }

    public enum ReturnValueType
    {
        /// <summary>
        /// 傳回值給呼叫window.open指令的視窗
        /// </summary>
        Opener,
        /// <summary>
        /// 傳回值給呼叫window.showModal或window..showModeless指令的視窗
        /// </summary>
        dialogArguments
    }

    /// <summary>
    /// WebScript 的摘要描述。
    /// </summary>
    public class WebScript
    {
        public WebScript()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        /// <summary>
        /// 取得PowerGrid中某個欄位的欄位序號。
        /// Grid:PowerGrid控制項ID	
        /// ColumnName:欄位名
        /// </summary>
        public static int GetGridColumnIndex(GridView Grid, string ColumnName)
        {
            int ReturnValue = -1;
            int CurrentIndex = -1;
            int i = Grid.AutoGenerateDeleteButton || Grid.AutoGenerateEditButton || Grid.AutoGenerateSelectButton ? 1 : 0;


            foreach (DataControlField Column in Grid.Columns)
            {
                CurrentIndex += 1;
                if (Column.HeaderText == ColumnName)
                    return CurrentIndex + i;
                else
                    continue;
            }
            return ReturnValue;
        }

        /// <summary>
        /// 按下控制項(作用控制項)會產生另開視窗效果。
        /// Container：作用控制項ID的容器	
        /// ControlID：作用控制項ID
        /// windowtype：開啟視窗類型
        /// Url：連結的網址
        /// MessageCollection：開啟視窗所含QueryString資訊的儲存器
        /// Features：視窗特色
        /// WindowWidth：視窗寬度
        /// WindowHeight：視窗高度
        /// </summary>
        public static void OpenWindow(Object Container, string ControlID, EventType eventtype, WindowType windowtype, string Url, NameValueCollection MessageCollection, string Features, int WindowWidth, int WindowHeight)
        {
            Control ControlType = null;
            string JavaScript = "";
            string QueryString = "";
            string EventName = "";
            if (Container.ToString().EndsWith("_aspx"))
                ControlType = ((Page)Container).FindControl(ControlID);
            else if (Container.ToString().EndsWith("_ascx"))
                ControlType = (Control)Container.GetType().GetMethod("FindControl").Invoke(Container, new object[] { ControlID });
            else if (Container is DataControlFieldCell)
                if (ControlID == "")
                {
                    ControlType = ((DataControlFieldCell)Container).Controls[0];
                }
                else
                    ControlType = ((TableCell)Container).FindControl(ControlID);
            else if (Container is GridView)
                ControlType = ((GridView)Container).FindControl(ControlID);
            else if (Container is WebControl)
                ControlType = ((WebControl)Container).FindControl(ControlID);

            if (ControlType != null)
            {
                string[] Name;
                string[] Value;
                if (MessageCollection != null)
                {
                    Name = new string[MessageCollection.AllKeys.GetLength(0)];
                    Value = new string[MessageCollection.AllKeys.GetLength(0)];
                    int i = 0;
                    QueryString = "?";
                    foreach (string Key in MessageCollection.AllKeys)
                    {
                        Name[i] = Key;
                        Value[i] = MessageCollection.Get(Key);
                        QueryString += Name[i] + "=" + Value[i] + "&";
                        i++;
                    }
                    QueryString = QueryString.Substring(0, QueryString.Length - 1);
                }

                switch (windowtype)
                {
                    case WindowType.Normal:
                        JavaScript += "var winl = (screen.width-" + WindowWidth + ")/2;var wint = (screen.height-" + WindowHeight + ")/2;if (winl < 0) winl = 0;if (wint < 0) wint = 0;var top=wint;var left = winl;oNewWindow=window.open('" + Url + QueryString + "','newWindow','" + Features + ",width=" + WindowWidth + ",height=" + WindowHeight + ",top='+top+',left='+left);oNewWindow.focus();return false;";
                        break;
                    case WindowType.ModalDialog:
                        JavaScript += "oParentForm=document;var winl = (screen.width-" + WindowWidth + ")/2;var wint = (screen.height-" + WindowHeight + ")/2;if (winl < 0) winl = 0;if (wint < 0) wint = 0;var top=wint;var left = winl;window.showModalDialog('" + Url + QueryString + "',oParentForm,'" + Features + ";dialogWidth=" + WindowWidth + "px;dialogHeight=" + WindowHeight + "px;dialogTop='+top+';dialogLeft='+left);return false;";
                        break;
                    case WindowType.ModelessDialog:
                        JavaScript += "oParentForm=document;var winl = (screen.width-" + WindowWidth + ")/2;var wint = (screen.height-" + WindowHeight + ")/2;if (winl < 0) winl = 0;if (wint < 0) wint = 0;var top=wint;var left = winl;if (oNewWindow==undefined)oNewWindow=window.showModelessDialog('" + Url + QueryString + "',oParentForm,'" + Features + ";dialogWidth=" + WindowWidth + "px;dialogHeight=" + WindowHeight + "px;dialogTop='+top+';dialogLeft='+left);else oNewWindow.focus();return false;";
                        break;
                }
                switch (eventtype)
                {
                    case EventType.OnClick:
                        EventName = "onclick";
                        break;
                    case EventType.OnDblClick:
                        EventName = "ondblclick";
                        break;
                }

                if (ControlType is WebControl)
                    ((WebControl)ControlType).Attributes.Add(EventName, JavaScript);
                else if (ControlType is HtmlControl)
                    ((HtmlControl)ControlType).Attributes.Add(EventName, JavaScript);
            }
        }

        /// <summary>
        /// 按下控制項(作用控制項)會產生另開視窗效果。
        /// Container：作用控制項ID的容器	
        /// ControlID：作用控制項ID
        /// windowtype：開啟視窗類型
        /// Url：連結的網址
        /// MessageCollection：開啟視窗所含QueryString資訊的儲存器
        /// Features：視窗特色
        /// WindowWidth：視窗寬度
        /// WindowHeight：視窗高度
        /// </summary>
        public static void OpenWindowWithValidation(Object Container, string ControlID, string ValidationGroup, EventType eventtype, WindowType windowtype, string Url, NameValueCollection MessageCollection, string Features, int WindowWidth, int WindowHeight)
        {
          Control ControlType = null;
          string JavaScript = "";
          string QueryString = "";
          string EventName = "";
          if (Container.ToString().EndsWith("_aspx"))
            ControlType = ((Page)Container).FindControl(ControlID);
          else if (Container.ToString().EndsWith("_ascx"))
            ControlType = (Control)Container.GetType().GetMethod("FindControl").Invoke(Container, new object[] { ControlID });
          else if (Container is DataControlFieldCell)
            if (ControlID == "")
            {
              ControlType = ((DataControlFieldCell)Container).Controls[0];
            }
            else
              ControlType = ((TableCell)Container).FindControl(ControlID);
          else if (Container is GridView)
            ControlType = ((GridView)Container).FindControl(ControlID);
          else if (Container is WebControl)
            ControlType = ((WebControl)Container).FindControl(ControlID);

          if (ControlType != null)
          {
            string[] Name;
            string[] Value;
            if (MessageCollection != null)
            {
              Name = new string[MessageCollection.AllKeys.GetLength(0)];
              Value = new string[MessageCollection.AllKeys.GetLength(0)];
              int i = 0;
              QueryString = "?";
              foreach (string Key in MessageCollection.AllKeys)
              {
                Name[i] = Key;
                Value[i] = MessageCollection.Get(Key);
                QueryString += Name[i] + "=" + Value[i] + "&";
                i++;
              }
              QueryString = QueryString.Substring(0, QueryString.Length - 1);
            }

            switch (windowtype)
            {
              case WindowType.Normal:
                if (ValidationGroup != "")
                {
                  JavaScript += "if(typeof(Page_ClientValidate) == 'function' && !Page_ClientValidate('" + ValidationGroup + "')){return false;}var winl = (screen.width-" + WindowWidth + ")/2;var wint = (screen.height-" + WindowHeight + ")/2;if (winl < 0) winl = 0;if (wint < 0) wint = 0;var top=wint;var left = winl;oNewWindow=window.open('" + Url + QueryString + "','newWindow','" + Features + ",width=" + WindowWidth + ",height=" + WindowHeight + ",top='+top+',left='+left);oNewWindow.focus();return false;";
                }
                else
                {
                  JavaScript += "if(typeof(Page_ClientValidate) == 'function' && !Page_ClientValidate()){return false;}var winl = (screen.width-" + WindowWidth + ")/2;var wint = (screen.height-" + WindowHeight + ")/2;if (winl < 0) winl = 0;if (wint < 0) wint = 0;var top=wint;var left = winl;oNewWindow=window.open('" + Url + QueryString + "','newWindow','" + Features + ",width=" + WindowWidth + ",height=" + WindowHeight + ",top='+top+',left='+left);oNewWindow.focus();return false;";
                }
                  break;
              case WindowType.ModalDialog:
                  if (ValidationGroup != "")
                  {
                    JavaScript += "if(typeof(Page_ClientValidate) == 'function' && !Page_ClientValidate('" + ValidationGroup + "')){return false;}oParentForm=document;var winl = (screen.width-" + WindowWidth + ")/2;var wint = (screen.height-" + WindowHeight + ")/2;if (winl < 0) winl = 0;if (wint < 0) wint = 0;var top=wint;var left = winl;window.showModalDialog('" + Url + QueryString + "',oParentForm,'" + Features + ";dialogWidth=" + WindowWidth + "px;dialogHeight=" + WindowHeight + "px;dialogTop='+top+';dialogLeft='+left);return false;";
                  }
                  else
                  {
                    JavaScript += "if(typeof(Page_ClientValidate) == 'function' && !Page_ClientValidate()){return false;}oParentForm=document;var winl = (screen.width-" + WindowWidth + ")/2;var wint = (screen.height-" + WindowHeight + ")/2;if (winl < 0) winl = 0;if (wint < 0) wint = 0;var top=wint;var left = winl;window.showModalDialog('" + Url + QueryString + "',oParentForm,'" + Features + ";dialogWidth=" + WindowWidth + "px;dialogHeight=" + WindowHeight + "px;dialogTop='+top+';dialogLeft='+left);return false;";

                  }
                  break;
              case WindowType.ModelessDialog:
                  if (ValidationGroup != "")
                  {
                    JavaScript += "if(typeof(Page_ClientValidate) == 'function' && !Page_ClientValidate('" + ValidationGroup + "')){return false;}oParentForm=document;var winl = (screen.width-" + WindowWidth + ")/2;var wint = (screen.height-" + WindowHeight + ")/2;if (winl < 0) winl = 0;if (wint < 0) wint = 0;var top=wint;var left = winl;if (oNewWindow==undefined)oNewWindow=window.showModelessDialog('" + Url + QueryString + "',oParentForm,'" + Features + ";dialogWidth=" + WindowWidth + "px;dialogHeight=" + WindowHeight + "px;dialogTop='+top+';dialogLeft='+left);else oNewWindow.focus();return false;";
                  }
                  else
                  {
                    JavaScript += "if(typeof(Page_ClientValidate) == 'function' && !Page_ClientValidate()){return false;}oParentForm=document;var winl = (screen.width-" + WindowWidth + ")/2;var wint = (screen.height-" + WindowHeight + ")/2;if (winl < 0) winl = 0;if (wint < 0) wint = 0;var top=wint;var left = winl;if (oNewWindow==undefined)oNewWindow=window.showModelessDialog('" + Url + QueryString + "',oParentForm,'" + Features + ";dialogWidth=" + WindowWidth + "px;dialogHeight=" + WindowHeight + "px;dialogTop='+top+';dialogLeft='+left);else oNewWindow.focus();return false;";

                  }
                  break;
            }
            switch (eventtype)
            {
              case EventType.OnClick:
                EventName = "onclick";
                break;
              case EventType.OnDblClick:
                EventName = "ondblclick";
                break;
            }

            if (ControlType is WebControl)
              ((WebControl)ControlType).Attributes.Add(EventName, JavaScript);
            else if (ControlType is HtmlControl)
              ((HtmlControl)ControlType).Attributes.Add(EventName, JavaScript);
          }
        }

        /// <summary>
        /// 對任何控制項產生對話方塊。
        /// Container：作用控制項ID上一層容器
        /// ControlID：作用控制項ID
        /// EventName：Client端事件名
        /// messagetype：對話方塊類型(警示,確認,要求輸入)
        /// /// Message：對話方塊訊息文字
        /// </summary>
        public static void AddClientMessageToControl(Object Container, string ControlID, EventType eventtype, MessageType messagetype, string Message)
        {
            Control ControlType = null;
            string JavaScript = "";
            string EventName = "";
            if (Container.ToString().EndsWith("_aspx"))
                ControlType = ((Page)Container).FindControl(ControlID);
            else if (Container.ToString().EndsWith("_ascx"))
                ControlType = (Control)Container.GetType().GetMethod("FindControl").Invoke(Container, new object[] { ControlID });
            else if (Container is DataControlFieldCell)
                if (ControlID == "")
                    ControlType = ((DataControlFieldCell)Container).Controls[0];
                else
                    ControlType = ((TableCell)Container).FindControl(ControlID);
            else if (Container is GridView)
                ControlType = ((GridView)Container).FindControl(ControlID);
            else if (Container is WebControl)
                ControlType = ((WebControl)Container).FindControl(ControlID);

            if (ControlType != null)
            {
                if (messagetype == MessageType.Alert)
                {
                    JavaScript += "javascript:window.alert('" + Message + "');";
                }
                if (messagetype == MessageType.Confirm)
                {
                  JavaScript += "javascript:var event = arguments[0] || window.event;if(!window.confirm('" + Message + "')) {event.cancelBubble = true;return false;}";
                }
                if (messagetype == MessageType.Prompt)
                {
                    JavaScript += "javascript:var value;value=window.prompt('" + Message + "','');if(value==null)return false;";
                }

                switch (eventtype)
                {
                    case EventType.OnClick:
                        EventName = "onclick";
                        break;
                    case EventType.OnDblClick:
                        EventName = "ondblclick";
                        break;
                }

                if (ControlType is WebControl)
                    ((WebControl)ControlType).Attributes.Add(EventName, JavaScript);
                else if (ControlType is HtmlControl)
                    ((HtmlControl)ControlType).Attributes.Add(EventName, JavaScript);
            }
        }

        /// <summary>
        /// 設定子視窗回傳值給父視窗。
        /// Container：作用控制項ID的容器	
        /// ControlID：啟動傳回值的控制項ID
        /// ReceivedValueObjId：接受傳回值的父視窗控制項ID陣列
        /// ClientIdOfOriginValue：來源值子視窗控制項ClientID陣列
        /// IsCauseValidation：是否欄位驗證
        /// ReturnType：指定父視窗的類型
        /// </summary>
        public static void AddReturnValue(Object Container, string ControlID, string[] ReceivedValueObjId, string[] ClientIdOfOriginValue, bool IsCauseValidation, ReturnValueType ReturnType)
        {
            Control ControlType = null;
            string JavaScript = "";
            if (Container.ToString().EndsWith("_aspx"))
                ControlType = ((Page)Container).FindControl(ControlID);
            else if (Container.ToString().EndsWith("_ascx"))
                ControlType = (Control)Container.GetType().GetMethod("FindControl").Invoke(Container, new object[] { ControlID });
            else if (Container is DataControlFieldCell)
                if (ControlID == "")
                    ControlType = ((DataControlFieldCell)Container).Controls[0];
                else
                    ControlType = ((TableCell)Container).FindControl(ControlID);
            else if (Container is GridView)
                ControlType = ((GridView)Container).FindControl(ControlID);
            else if (Container is WebControl)
                ControlType = ((WebControl)Container).FindControl(ControlID);

            if (ControlType != null)
            {
                if (ReceivedValueObjId.GetLength(0) != ClientIdOfOriginValue.GetLength(0))
                    return;
                else
                {
                    for (int i = 0; i < ReceivedValueObjId.GetLength(0); i++)
                    {
                        if (ReturnType == ReturnValueType.dialogArguments)
                            JavaScript += "GetValueFromShowModal('" + ReceivedValueObjId[i] + "',document.getElementById('" + ClientIdOfOriginValue[i] + "').value);";
                            //JavaScript += "GetValueFromShowModal('" + ReceivedValueObjId[i] + "',document.getElementById('" + ClientIdOfOriginValue[i] + "'));";
                        else
                            JavaScript += "GetValueFromNormal('" + ReceivedValueObjId[i] + "',document.getElementById('" + ClientIdOfOriginValue[i] + "').value);";
                            //JavaScript += "GetValueFromNormal('" + ReceivedValueObjId[i] + "',document.getElementById('" + ClientIdOfOriginValue[i] + "'));";
                    }
                }
            }

            JavaScript += "window.close();return false;";

            if (IsCauseValidation == true)
                JavaScript = "if (typeof(Page_ClientValidate) == 'function'){if(Page_ClientValidate()){" + JavaScript + "}}";

            if (ControlType is WebControl)
                ((WebControl)ControlType).Attributes.Add("onclick", JavaScript);
            else if (ControlType is HtmlControl)
                ((HtmlControl)ControlType).Attributes.Add("onclick", JavaScript);
        }

        /// 透過RadioButton設定隱藏值。
        /// Container：啟動賦予值給隱藏欄位的控制項的容器	
        /// ControlID：啟動賦予值給隱藏欄位的控制項ID
        /// SourcePage：id=HiddenID的隱藏欄位所在頁面
        /// HiddenID：要賦予值的輸入控制項ID
        /// SourceValue：來源值
        public static void AddValueToHiddenOnRadioClick(Object Container, string ControlID, Page SourcePage, string[] HiddenID, string[] SourceValue)
        {
            Control ControlType = null;
            HtmlInputHidden HiddenControl = null;
            string JavaScript = "";
            if (Container.ToString().EndsWith("_aspx"))
                ControlType = ((Page)Container).FindControl(ControlID);
            else if (Container.ToString().EndsWith("_ascx"))
                ControlType = (Control)Container.GetType().GetMethod("FindControl").Invoke(Container, new object[] { ControlID });
            else if (Container is DataControlFieldCell)
                if (ControlID == "")
                    ControlType = ((DataControlFieldCell)Container).Controls[0];
                else
                    ControlType = ((TableCell)Container).FindControl(ControlID);
            else if (Container is GridView)
                ControlType = ((GridView)Container).FindControl(ControlID);
            else if (Container is WebControl)
                ControlType = ((WebControl)Container).FindControl(ControlID);

            if (ControlType != null)
            {
                if (HiddenID.GetLength(0) != SourceValue.GetLength(0))
                    return;
                else
                {
                    for (int i = 0; i < HiddenID.GetLength(0); i++)
                    {
                        HiddenControl = (HtmlInputHidden)SourcePage.FindControl(HiddenID[i]);
                        if (HiddenControl != null)
                        {
                            if (ControlID != "") { JavaScript += "RadioButtonUnchecked('" + ControlID + "');this.checked=true;"; }
                            JavaScript += "document.getElementById('" + HiddenControl.ClientID + "').value='" + SourceValue[i].Trim() + "';";
                        }
                    }
                }
            }

            if (ControlType is WebControl)
                ((WebControl)ControlType).Attributes.Add("onclick", JavaScript);
            else if (ControlType is HtmlControl)
                ((HtmlControl)ControlType).Attributes.Add("onclick", JavaScript);
        }

        /// 透過CheckBox設定隱藏值。
        /// Container：啟動賦予值給隱藏欄位的控制項的容器	
        /// ControlID：啟動賦予值給隱藏欄位的控制項ID
        /// SourcePage：id=HiddenID的隱藏欄位所在頁面
        /// HiddenID：要賦予值的輸入控制項ID
        /// SourceValue：來源值
        public static void AddValueToHiddenOnCheckBoxClick(Object Container, string ControlID, Page SourcePage, string[] HiddenID, string[] SourceValue)
        {
            Control ControlType = null;
            HtmlInputHidden HiddenControl = null;
            string JavaScript = "";
            if (Container.ToString().EndsWith("_aspx"))
                ControlType = ((Page)Container).FindControl(ControlID);
            else if (Container.ToString().EndsWith("_ascx"))
                ControlType = (Control)Container.GetType().GetMethod("FindControl").Invoke(Container, new object[] { ControlID });
            else if (Container is DataControlFieldCell)
                if (ControlID == "")
                    ControlType = ((DataControlFieldCell)Container).Controls[0];
                else
                    ControlType = ((TableCell)Container).FindControl(ControlID);
            else if (Container is GridView)
                ControlType = ((GridView)Container).FindControl(ControlID);
            else if (Container is WebControl)
                ControlType = ((WebControl)Container).FindControl(ControlID);

            if (ControlType != null)
            {
                if (HiddenID.GetLength(0) != SourceValue.GetLength(0))
                    return;
                else
                {
                    for (int i = 0; i < HiddenID.GetLength(0); i++)
                    {
                        HiddenControl = (HtmlInputHidden)SourcePage.FindControl(HiddenID[i]);
                        if (HiddenControl != null)
                        {
                            JavaScript += "if(this.checked==true){document.getElementById('" + HiddenControl.ClientID + "').value+='" + SourceValue[i].Trim() + ";';}else{document.getElementById('" + HiddenControl.ClientID + "').value=document.getElementById('" + HiddenControl.ClientID + "').value.replace('" + SourceValue[i].Trim() + ";','');}";
                        }
                    }
                }
            }

            if (ControlType is WebControl)
                ((WebControl)ControlType).Attributes.Add("onclick", JavaScript);
            else if (ControlType is HtmlControl)
                ((HtmlControl)ControlType).Attributes.Add("onclick", JavaScript);
        }

        /// <summary>
        /// 開啟日期選擇器。
        /// Container：作用控制項ID的容器	
        /// ControlID：開啟日期的控制項ID
        /// ClientIDofReturnValue：接受回傳值的控制項ID
        /// windowtype：開啟視窗類型
        /// Url：連結的網址
        /// MessageCollection：開啟視窗所含QueryString資訊的儲存器
        /// Features：視窗特色
        /// MessageCollection：視窗寬度
        /// WindowHeight：視窗高度
        /// IsCenter：視窗是否置中
        /// </summary>
        public static void OpenDatePicker(Object Container, string ControlID, string ClientIDofReturnValue, WindowType windowtype, string Url, NameValueCollection MessageCollection, string Features, int WindowWidth, int WindowHeight, bool IsCenter)
        {
            Control ControlType = null;
            string JavaScript = "";
            string QueryString = "";
            if (Container.ToString().EndsWith("_aspx"))
                ControlType = ((Page)Container).FindControl(ControlID);
            else if (Container.ToString().EndsWith("_ascx"))
                ControlType = (Control)Container.GetType().GetMethod("FindControl").Invoke(Container, new object[] { ControlID });
            else if (Container.ToString().EndsWith("_ascx"))
                ControlType = ((UserControl)Container).FindControl(ControlID);
            else if (Container is DataControlFieldCell)
                if (ControlID == "")
                    ControlType = ((DataControlFieldCell)Container).Controls[0];
                else
                    ControlType = ((TableCell)Container).FindControl(ControlID);
            else if (Container is GridView)
                ControlType = ((GridView)Container).FindControl(ControlID);
            else if (Container is WebControl)
                ControlType = ((WebControl)Container).FindControl(ControlID);

            if (ControlType != null)
            {
                string[] Name;
                string[] Value;
                if (MessageCollection != null)
                {
                    Name = new string[MessageCollection.AllKeys.GetLength(0)];
                    Value = new string[MessageCollection.AllKeys.GetLength(0)];
                    int i = 0;
                    QueryString = "?";
                    foreach (string Key in MessageCollection.AllKeys)
                    {
                        Name[i] = Key;
                        Value[i] = MessageCollection.Get(Key);
                        QueryString += Name[i] + "=" + Value[i] + "&";
                        i++;
                    }
                    QueryString = QueryString.Substring(0, QueryString.Length - 1);
                }

                if (IsCenter)
                    JavaScript += "var oReturnValue;var top=(screen.availHeight-" + WindowHeight + ")/2;var left=(screen.availWidth-" + WindowWidth + ")/2;";
                else
                    JavaScript += "var oReturnValue;var top=window.event.screenY-window.event.offsetY;var left=window.event.screenX-window.event.offsetX;";

                switch (windowtype)
                {
                    case WindowType.Normal:
                        break;
                    case WindowType.ModalDialog:
                        JavaScript += "oReturnValue=window.showModalDialog('" + Url + QueryString + "',document.getElementById('" + ClientIDofReturnValue + "').value,'" + Features + ";dialogWidth:" + WindowWidth + "px;dialogHeight:" + WindowHeight + "px;dialogTop:'+top+'px;dialogLeft:'+left+'px');document.getElementById('" + ClientIDofReturnValue + "').value=formatDate(oReturnValue);";
                        break;
                    case WindowType.ModelessDialog:
                        break;
                }

                if (ControlType is WebControl)
                    ((WebControl)ControlType).Attributes.Add("onclick", JavaScript);
                else if (ControlType is HtmlControl)
                    ((HtmlControl)ControlType).Attributes.Add("onclick", JavaScript);
            }
        }

        public static string TrimSpecialChar(string chars)
        {
            chars.Replace("&nbsp;", "");
            return chars;
        }
    }
}