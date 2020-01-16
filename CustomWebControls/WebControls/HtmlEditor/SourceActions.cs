using System.Web.UI;

namespace APTemplate
{
    /// <summary>
    /// Sigma secure text editor
    /// </summary>
    public class SourceActions : System.Web.UI.Page
    {
        #region fields
        internal string _SourceCode = string.Empty;
        #endregion

        #region Cunstructor
        public SourceActions()
        {
            //
            // TODO: Add constructor if it is requiered
            //
        }

        #endregion Cunstructor

        #region Source provider
        /// <summary>
        /// provide editor html source code
        /// </summary>
        /// <summary>
        /// Sets the Current page.
        /// </summary>
        /// <param name="CurrentPage">The current page.</param>
        /// <returns>void</returns>
        internal void SourceProvider(Page CurrentPage)
        {
            CurrentPage.ClientScript.GetPostBackEventReference(CurrentPage, string.Empty);
            if (CurrentPage.IsPostBack)
            {
                string eventTarget = (CurrentPage.Request["__EVENTTARGET"] == null ? string.Empty : CurrentPage.Request["__EVENTTARGET"]);
                string eventArgument = (CurrentPage.Request["__EVENTARGUMENT"] == null ? string.Empty : CurrentPage.Request["__EVENTARGUMENT"]);
                if (eventTarget == "getHtmlData")
                {
                    if (Security._SetHighLevelSecurityForHtmlTags)
                    {
                        _SourceCode = Security.EncodeHtml(eventArgument);
                    }
                    else
                    {
                        _SourceCode = eventArgument;
                    }
                }
            }
        }
        #endregion
    }
}
