using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Security.Cryptography;
using Microsoft.Security.Application;

namespace APTemplate
{
    /// <summary>
    /// Responsible for security issues
    /// </summary>
    public static class Security
    {
        /// <summary>
        /// if this properties is true, in this case will set two steps security over exchanged data
        /// The default value of this property is true
        /// </summary>
        internal static bool _SetHighLevelSecurityForHtmlTags = true;
        internal static string EncodeHtml(string html)
        {
            return AntiXss.HtmlEncode(Sanitizer.GetSafeHtmlFragment(html));
        }
    }
}
