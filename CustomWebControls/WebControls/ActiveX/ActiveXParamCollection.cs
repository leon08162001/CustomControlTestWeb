using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace APTemplate
{
  [Serializable()]
  public class ActiveXParamCollection : CollectionBase
  {

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public ActiveXParam this[int index]
    {
      get
      {
        return (ActiveXParam)List[index];
      }
      set
      {
        List[index] = value;
      }
    }

    /// <summary>
    /// 加入成員。
    /// </summary>
    public int Add(ActiveXParam item)
    {
      return List.Add(item);
    }

    /// <summary>
    /// 取得成員索引。
    /// </summary>
    public int IndexOf(ActiveXParam item)
    {
      return List.IndexOf(item);
    }

    /// <summary>
    /// 插入成員。
    /// </summary>
    public void Insert(int index,ActiveXParam value)
    {
      List.Insert(index, value);
    }

    /// <summary>
    /// 移除成員。
    /// </summary>
    public void Remove(ActiveXParam value)
    {
      List.Remove(value);
    }

    /// <summary>
    /// 判斷集合是否包含指定成員。
    /// </summary>
    public Boolean Contains(ActiveXParam value)
    {
      return List.Contains(value);
    }
  }
}