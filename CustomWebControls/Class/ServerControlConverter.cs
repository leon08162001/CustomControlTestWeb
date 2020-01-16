// Directly taken from: http://weblogs.asp.net/asmith/archive/2003/09/04/26360.aspx
// By Andy Smith (http://www.metabuilders.com/)

using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace APTemplate
{
  public class ServerControlConverter : StringConverter 
  {
    #region Make It A ComboBox
    public override bool GetStandardValuesSupported(ITypeDescriptorContext context) 
    {
      return true;
    }
    public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) 
    {
      return false;
    }
    #endregion

    #region Display Control IDs In List
    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) 
    {
      if ((context == null) || (context.Container == null)) 
      {
        return null; 
      }
      Object[] serverControls = this.GetControls(context.Container);
      if (serverControls != null) 
      {
        return new StandardValuesCollection(serverControls); 
      }
      return null; 
    }
    private object[] GetControls(IContainer container) 
    {
      ArrayList availableControls = new ArrayList();
      foreach( IComponent component in container.Components ) 
      {
        Control serverControl = component as Control;
        if ( serverControl != null && 
          !(serverControl is Page) && 
          serverControl.ID != null && 
          serverControl.ID.Length != 0  && 
          IncludeControl(serverControl) 
          ) 
        {
          availableControls.Add(serverControl.ID);
        }
      }
      availableControls.Sort(Comparer.Default);
      return availableControls.ToArray(); 
    }
    #endregion

    //Override this method to customize which controls show up in the list
    protected virtual Boolean IncludeControl( Control serverControl ) 
    {
      return true;
    }
  }
}
