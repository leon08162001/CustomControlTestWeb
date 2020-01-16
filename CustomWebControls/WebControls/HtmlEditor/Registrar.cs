using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace APTemplate
{
    /// <summary>
    /// Registration responsible
    /// </summary>
    internal class Registrar
    {
        /// <summary>
        /// Registers list of scripts on page 
        /// </summary>
        /// <param name="ScriptsList"></param>
        /// <param name="CurrentPage"></param>
        /// <param name="ThisType"></param>
        internal static void RegisterScripts(List<string> ScriptsList, Page CurrentPage, Type ThisType)
        {
            foreach (string script in ScriptsList)
            {
                CurrentPage.ClientScript.RegisterClientScriptInclude(ThisType, script + "_Key", CurrentPage.ClientScript.GetWebResourceUrl(ThisType, script));

            }
        }
    }
}
