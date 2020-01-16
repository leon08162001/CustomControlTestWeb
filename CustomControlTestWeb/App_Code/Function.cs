using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// PublicFunc 的摘要描述

/// </summary>
public static class Func
{
	public static DataTable CopyToDataTable(IEnumerable AnonymusType)
	{
		DataTable Tbl = new DataTable();
		IEnumerator IEnum = AnonymusType.GetEnumerator();
		if (IEnum.MoveNext())
		{
			object Q = IEnum.Current;
      if (Q.GetType().ToString() == "System.String")
      {
        string FieldName = "string";
        Tbl.Columns.Add(FieldName, Q.GetType());
      }
      else
      {
        System.Reflection.PropertyInfo[] Properties = Q.GetType().GetProperties();
        foreach (var Property in Properties)
        {
          string FieldName = Property.Name;
          Tbl.Columns.Add(FieldName, Property.PropertyType);
        }
      }
		}

		foreach (var Obj in AnonymusType)
		{
			DataRow NewRow = Tbl.NewRow();
      if (Obj.GetType().ToString() == "System.String")
      {
        NewRow[0] = Obj;
      }
      else
      {
        System.Reflection.PropertyInfo[] Properties = Obj.GetType().GetProperties();
        foreach (var Property in Properties)
        {
          string FieldName = Property.Name;
          object FieldValue = Property.GetValue(Obj, null);
          NewRow[FieldName] = FieldValue;
        }
      }
			Tbl.Rows.Add(NewRow);
		}
		return (Tbl);
	}

  public static string FormatDate(string yyyyMMddhhmmss)
  {
    string val=null;
    if (yyyyMMddhhmmss != null)
    {
      val = yyyyMMddhhmmss.Substring(0, 4) + "/" + yyyyMMddhhmmss.Substring(4, 2) + "/" + yyyyMMddhhmmss.Substring(6, 2) +
                 " " + yyyyMMddhhmmss.Substring(8, 2) + ":" + yyyyMMddhhmmss.Substring(10, 2) + ":" + yyyyMMddhhmmss.Substring(12, 2);
    }
      return val; 
  }
}
