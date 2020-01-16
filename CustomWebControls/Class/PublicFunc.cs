using System;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.VisualBasic;

namespace APTemplate
{
  public enum IDType
  {
    ID,
    ClientID
  }

  public class PublicFunc
  {
    public static string GetIniInfo(string iniFile, string iniSection)
    {
      if (!File.Exists(iniFile))
      {
        return "error";
      }
      StreamReader iniRead = new StreamReader(iniFile, System.Text.Encoding.GetEncoding(950));
      try
      {
        string iniStr = iniRead.ReadToEnd();
        int i, cLine, BegLineNum, EndLineNum;
        BegLineNum = 0;
        EndLineNum = 0;
        string getValue = "";
        string[] cLst = iniStr.Split(Strings.Chr(13));
        cLine = cLst.Length;
        for (i = 0; i < cLine; i++)
        {
          if (cLst[i] == "[" + iniSection + "]" || cLst[i] == "\n[" + iniSection + "]")
          {
            BegLineNum = i + 1;
            continue;
          }
          if (BegLineNum != 0)
          {
            if ((cLst[i].Substring(0, 1) == "[" || cLst[i].Substring(0, 2) == "\n[") && cLst[i].Substring(cLst[i].Length - 1) == "]")
            {
              EndLineNum = i - 1;
              break;
            }
          }
        }
        if (BegLineNum != 0 && EndLineNum == 0)
          EndLineNum = cLst.Length - 1;

        if (BegLineNum != 0)
        {
          for (i = BegLineNum; i < EndLineNum + 1; i++)
          {
            getValue += cLst[i] + Strings.Chr(13);
          }
        }
        return getValue.Substring(1, getValue.Length - 2);
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
      finally
      {
        iniRead.Close();
      }
    }

    public static string GetResourcesInfo(string ResourceContent, string iniSection)
    {
      try
      {
        string iniStr = ResourceContent;
        int i, cLine, BegLineNum, EndLineNum;
        BegLineNum = 0;
        EndLineNum = 0;
        string getValue = "";
        string[] cLst = iniStr.Split(Strings.Chr(13));
        cLine = cLst.Length;
        for (i = 0; i < cLine; i++)
        {
          if (cLst[i] == "[" + iniSection + "]" || cLst[i] == "\n[" + iniSection + "]")
          {
            BegLineNum = i + 1;
            continue;
          }
          if (BegLineNum != 0)
          {
            if ((cLst[i].Substring(0, 1) == "[" || cLst[i].Substring(0, 2) == "\n[") && cLst[i].Substring(cLst[i].Length - 1) == "]")
            {
              EndLineNum = i - 1;
              break;
            }
          }
        }
        if (BegLineNum != 0 && EndLineNum == 0)
          EndLineNum = cLst.Length - 1;

        if (BegLineNum != 0)
        {
          for (i = BegLineNum; i < EndLineNum + 1; i++)
          {
            getValue += cLst[i] + Strings.Chr(13);
          }
        }
        return getValue.Substring(1, getValue.Length - 2);
      }
      catch (Exception ex)
      {
        return ex.Message;
      }
    }

    public static string GetPath(Page Page)
    {
      int num = Page.AppRelativeTemplateSourceDirectory.Split('/').Length - 2;
      switch (num)
      {
        case 1:
          return "../";
        case 2:
          return "../../";
        case 3:
          return "../../../";
        default:
          return "";
      }
    }

    public static Control GetControl(Control SearchControl, string ControlID)
    {
        Control Result = null;
        Result = FindControlRecursive(SearchControl, ControlID);
        return Result;
    }

    public static List<Control> GetChildControls(Control SearchControl)
    {
      List<Control> ResultList = new List<Control>();
      if (SearchControl != null)
        GetControls(SearchControl, ref ResultList);
      return ResultList;
    }

    public static List<Control> GetChildControls(Control SearchControl, Type ChildControlType)
    {
      List<Control> ResultList = new List<Control>();
      if (SearchControl != null)
      GetControls(SearchControl, ChildControlType, ref ResultList);
      return ResultList;
    }

    public static List<Control> GetChildControls(Control SearchControl, IDType IDType, String ChildControlName)
    {
      List<Control> ResultList = new List<Control>();
      if (SearchControl != null)
      GetControls(SearchControl, IDType, ChildControlName, ref ResultList);
      return ResultList;
    }

    private static void GetControls(Control SearchControl, ref List<Control> ResultList)
    {
      if (SearchControl.Controls.Count == 0)
        return;
      else
      {
        foreach (Control ctrl in SearchControl.Controls)
        {
          if (ctrl.Controls.Count == 0)
          {
            ResultList.Add(ctrl);
          }
          else
          {
            GetControls(ctrl, ref ResultList);
          }
        }
      }
    }

    private static void GetControls(Control SearchControl, Type ChildControlType, ref List<Control> ResultList)
    {
      if (SearchControl.Controls.Count == 0)
        return;
      else
      {
        foreach (Control ctrl in SearchControl.Controls)
        {
          if (ctrl.Controls.Count == 0)
          {
            if (ctrl.GetType() == ChildControlType)
            {
              ResultList.Add(ctrl);
            }
          }
          else
          {
            GetControls(ctrl, ChildControlType, ref ResultList);
          }
        }
      }
    }

    private static void GetControls(Control SearchControl, IDType IDType, String ChildControlName, ref List<Control> ResultList)
    {
      if (SearchControl.Controls.Count == 0)
        return;
      else
      {
        foreach (Control ctrl in SearchControl.Controls)
        {
          if (ctrl.Controls.Count == 0)
          {
            switch (IDType)
            {
              case IDType.ID:
                if (ctrl.ID != null && ctrl.ID.ToUpper() == ChildControlName.ToUpper())
                {
                  ResultList.Add(ctrl);
                }
                break;
              case IDType.ClientID:
                if (ctrl.ClientID != null && ctrl.ClientID.ToUpper() == ChildControlName.ToUpper())
                {
                  ResultList.Add(ctrl);
                }
                break;
            }
          }
          else
          {
            GetControls(ctrl, IDType, ChildControlName, ref ResultList);
          }
        }
      }
    }

    private static Control FindControlRecursive(Control rootControl, string controlId)
    {
        if (rootControl.ID == controlId)
            return rootControl;
        foreach (Control control in rootControl.Controls)
        {
            Control foundControl = FindControlRecursive(control, controlId);
            if (foundControl != null)
            { return foundControl; }
        }
        return null;
    } 
  }
}