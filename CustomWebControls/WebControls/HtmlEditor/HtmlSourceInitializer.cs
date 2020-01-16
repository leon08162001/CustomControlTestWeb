using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace APTemplate
{
    internal static class HtmlSourceInitializer
    {
        #region fields
        private static StringBuilder _HtmlSource = null;

        #endregion

        #region getHtmlSource
        public static StringBuilder RichTextHtmlSource
        {
            get
            {
                if (_HtmlSource != null)
                {
                    return _HtmlSource;
                }
                return null;
            }
        }

        #endregion

        #region Initialize html source
        /// <summary>
        /// Initializes the Html source of editor
        /// </summary>
        /// <param name="CurrentPage"></param>
        public static void InitializeHtmlSource(Page CurrentPage)
        {
            //Page page = new Page();
            StringBuilder HtmlSource = new StringBuilder();
            //HtmlSource.Append("<div id=\"loadingZone\" style=\"margin-left:300px;margin-top:100px\"><div style=\"font-family:tahoma;color:#B9B9B9\" id=\"loadingSms\">Loading...</div><br class=\"clear\" /><div id=\"loadingBar\"><div id=\"loaderParent\" style=\"width:300px;border-color:#DFE0DE;border:1px;border-style:groove\"><div style=\"width:10px;height:3px;background-color:#ADADAD\" id=\"progressBar\"></div></div></div><div id=\"infoLoading\"></div></div>");
            HtmlSource.Append("<center id=\"centerElement\" style=\"display:none\">");
            HtmlSource.Append("<table>");
            HtmlSource.Append("<table cellpadding=\"0px\" cellspacing=\"0px\">");
            HtmlSource.Append("<tr>");
            HtmlSource.Append("<td align=\"left\" valign=\"bottom\" style=\"width:100px\"></td>");
            HtmlSource.Append("<td align=\"left\" valign=\"bottom\">");
            HtmlSource.Append("<div id=\"textToolsContainer\" style='width: 600px; text-align: left; margin-bottom: 10px'>");
            HtmlSource.Append("<table cellpadding=\"0px\" cellspacing=\"1px\">");
            HtmlSource.Append("<tr>");
            HtmlSource.Append("<td>");
            HtmlSource.Append("<table>");
            HtmlSource.Append("<tr>");

            #region font size,font family
            HtmlSource.Append("<td class=\"Panels\">");
            HtmlSource.Append("<table cellpadding=\"0px\" cellspacing=\"1px\">");
            HtmlSource.Append("<tr>");
            HtmlSource.Append("<td valign=\"bottom\">");
            HtmlSource.Append("<div>");
            HtmlSource.Append("<select class=\"fontFamilyComboBox\" id=\"fonts\" onchange=\"textEdit('fontname',this[this.selectedIndex].value)\">");
            HtmlSource.Append("<option value=\"Arial\">Arial</option>");
            HtmlSource.Append("<option value=\"Comic Sans MS\">Comic Sans MS</option>");
            HtmlSource.Append("<option value=\"Courier New\">Courier New</option>");
            HtmlSource.Append("<option value=\"Monotype Corsiva\">Monotype</option>");
            HtmlSource.Append("<option value=\"Tahoma\">Tahoma</option>");
            HtmlSource.Append("<option value=\"Times\">Times</option>");
            HtmlSource.Append("</select>");
            HtmlSource.Append("</div>");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            HtmlSource.Append("<select class=\"fontSizeComboBox\" id=\"size\" onchange=\"textEdit('fontsize',this[this.selectedIndex].value)\">");
            HtmlSource.Append("<option value=\"1\">xx-Small</option>");
            HtmlSource.Append("<option value=\"2\">x-Small</option>");
            HtmlSource.Append("<option value=\"3\">Small</option>");
            HtmlSource.Append("<option value=\"4\">medium</option>");
            HtmlSource.Append("<option value=\"5\">Large</option>");
            HtmlSource.Append("<option value=\"6\">x-Large</option>");
            HtmlSource.Append("<option value=\"7\">xx-Large</option>");
            HtmlSource.Append("</select>");
            HtmlSource.Append("</td>");
            HtmlSource.Append("</tr>");
            HtmlSource.Append("</table>");
            HtmlSource.Append("</td>");

            #endregion
            //font edit
            #region Font Edit
            HtmlSource.Append("<td class=\"Panels\">");
            HtmlSource.Append("<table cellpadding=\"0px\" cellspacing=\"1px\">");
            HtmlSource.Append("<tr>");

            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input  title=\"Bold\" type=\"button\" class=\"BoldButtons\" id=\"boldButton\"  onclick=\"textEdit('bold')\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('italic')\" id=\"italic\" title=\"Italic\" class=\"ItalicButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.text_italic.png") + "' />");
            HtmlSource.Append("<input  title=\"Italic\" type=\"button\" id=\"italic\" class=\"ItalicButtons\" onclick=\"textEdit('italic')\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append(" <td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('underline')\" id=\"underline\" title=\"UnderLine\" class=\"UnderLineButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.underline.png") + "' />");
            HtmlSource.Append("<input title=\"UnderLine\" type=\"button\" id=\"underline\"  class=\"UnderLineButtons\" onclick=\"textEdit('underline')\" />");
            HtmlSource.Append("</td>");

            HtmlSource.Append(" <td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('underline')\" id=\"underline\" title=\"UnderLine\" class=\"UnderLineButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.underline.png") + "' />");
            HtmlSource.Append("<input title=\"UnderLine\" type=\"button\" id=\"underline\"  class=\"strikeThroughButtons\" onclick=\"textEdit('StrikeThrough')\" />");
            HtmlSource.Append("</td>");

            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('justifyleft')\" title=\"align left\" class=\"LeftJustify\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.format_justify_left.png") + "' />");
            HtmlSource.Append("<input type=\"button\" class=\"LeftJustify\" onclick=\"textEdit('justifyleft')\" title=\"align left\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('justifycenter')\" title=\"center\" class=\"CenterButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.center.png") + "' />");
            HtmlSource.Append("<input type=\"button\" class=\"CenterButtons\" onclick=\"textEdit('justifycenter')\" title=\"center\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('justifyright')\"  title=\"align right\" class=\"RightJustify\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.format_justify_right.png") + "' />");
            HtmlSource.Append("<input type=\"button\"  class=\"RightJustify\" onclick=\"textEdit('justifyright')\" title=\"align right\" />");
            HtmlSource.Append("</td>");
            //--
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input class=\"colorPickerButton\" type=\"button\" onclick=\"showAdvanceColorPicker(event)\" title=\"color picker\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input id=\"numberedList\" type=\"button\" class=\"NumberedList\" onclick=\"textEdit('insertorderedlist')\" title=\"Numbered List\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input id=\"bulletList\" type=\"button\" class=\"BulletList\" onclick=\"textEdit('insertunorderedlist')\" title=\"Bullets List\" />");
            HtmlSource.Append("</td>");

            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('justifyright')\"  title=\"align right\" class=\"RightJustify\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.format_justify_right.png") + "' />");
            HtmlSource.Append("<input type=\"button\"  class=\"subScriptButtons\" onclick=\"textEdit('subscript')\" title=\"Subscript\" />");
            HtmlSource.Append("</td>");

            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('justifyright')\"  title=\"align right\" class=\"RightJustify\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.format_justify_right.png") + "' />");
            HtmlSource.Append("<input type=\"button\"  class=\"superScriptButtons\" onclick=\"textEdit('superscript')\" title=\"Superscript\" />");
            HtmlSource.Append("</td>");

            //--
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('dirLTR')\"  title=\"ltr\" class=\"ltrButton\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.ltr_direction.png") + "' />");
            HtmlSource.Append("<input id=\"ltrButton\" type=\"button\" class=\"ltrButton_on\" onclick=\"textEdit('dirLTR')\" title=\"ltr\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('dirRTL')\"  title=\"rtl\" class=\"rtlButton\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.rtl_direction.png") + "' />");
            HtmlSource.Append("<input id=\"rtlButton\" type=\"button\" class=\"rtlButton\" onclick=\"textEdit('dirRTL')\" title=\"rtl\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('outdent')\"  title=\"Outdent\" class=\"outdent\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.outdent.png") + "' />");
            HtmlSource.Append("<input id=\"outdentButton\" type=\"button\" class=\"outdent\" onclick=\"textEdit('outdent')\" title=\"Outdent\"/>");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('indent')\" title=\"Indent\" class=\"indent\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.indent.png") + "' />");
            HtmlSource.Append("<input id=\"indentButton\" type=\"button\" class=\"indent\"  onclick=\"textEdit('indent')\" title=\"Indent\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("</tr>");
            HtmlSource.Append("</table>");
            HtmlSource.Append("</td>");

            #endregion

            HtmlSource.Append("</tr>");
            HtmlSource.Append("</table>");
            HtmlSource.Append("</td>");
            HtmlSource.Append("</tr>");
            HtmlSource.Append("<tr>");
            HtmlSource.Append("<td>");
            HtmlSource.Append("<table>");
            HtmlSource.Append("<tr>");

            //form maker
            #region Form maker futures
            HtmlSource.Append("<td class=\"Panels\">");
            HtmlSource.Append("<table cellpadding=\"0px\" cellspacing=\"1px\">");
            HtmlSource.Append("<tr>");
            HtmlSource.Append("<td valign=\"bottom\">");
            HtmlSource.Append("<input  title=\"Insert Button\" type=\"button\" class=\"insertButton-Button\" id=\"insertButton\"  onclick=\"CreateObject('insertButton')\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input  title=\"Insert CheckBox\" type=\"button\" id=\"Button2\" class=\"insertCheckBoxButton\" onclick=\"pasteHtmlElements('checkBox','','')\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input title=\"Insert ComboBox\" type=\"button\" id=\"Button3\"  class=\"insertComboBoxButton\" onclick=\"pasteHtmlElements('comboBox','','')\" />");
            HtmlSource.Append("</td>");

            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input title=\"Insert RadioButton\" type=\"button\" id=\"RaioButton\"  class=\"insertRadioButtons\" onclick=\"pasteHtmlElements('radioButton','','')\" />");
            HtmlSource.Append("</td>");

            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input type=\"button\" class=\"insertFileUploadButton\" onclick=\"pasteHtmlElements('file','','')\" title=\"Insert File Upload\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input type=\"button\" class=\"insertPasswordButton\" onclick=\"pasteHtmlElements('password','','')\" title=\"Insert Password\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input type=\"button\" class=\"insertTextBoxButton\" onclick=\"pasteHtmlElements('textBox','','')\" title=\"Insert TextBox\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input type=\"button\" class=\"inserTextAreaButton\" onclick=\"pasteHtmlElements('textArea','','')\" title=\"Insert TextArea\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input type=\"button\" class=\"createTableIcon\"  onclick=\"CreateObject('InsertTable')\" title=\"Insert Table\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("</tr>");
            HtmlSource.Append("</table>");
            HtmlSource.Append("</td>");

            #endregion

            //link,...
            #region Link,image,print,... future
            HtmlSource.Append("<td class=\"Panels\">");
            HtmlSource.Append("<table cellpadding=\"0px\" cellspacing=\"1px\">");
            HtmlSource.Append("<tr>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.link2.png") + "' />");
            HtmlSource.Append("<input type=\"button\" class=\"CreateLink\"  onclick=\"CreateObject('CreateLink')\" title=\"Link\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.link_break.png") + "' />");
            HtmlSource.Append("<input type=\"button\" class=\"BreakLink\"  onclick=\"textEdit('unlink')\" title=\"Break Link\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.insert_image.png") + "' />");
            HtmlSource.Append("<input type=\"button\" class=\"InsertImage\"  onclick=\"CreateObject('InsertImage')\" title=\"Insert Image\" />");
            HtmlSource.Append("</td>");


            //HtmlSource.Append("<td valign=\"bottom\">");
            ////HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.insert_image.png") + "' />");
            //HtmlSource.Append("<input type=\"button\" class=\"insertSWF\"  onclick=\"createSWFMaker()\" title=\"Insert SWF\" />");
            //HtmlSource.Append("</td>");

            //Insert Special charcter
            HtmlSource.Append("<td valign=\"bottom\">");
            HtmlSource.Append("<input class=\"insertSpecialCharacterButton\" type='button' onclick='createSpecialCharacters(event)' />");
            HtmlSource.Append("</td>");

            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.HorizontalLine.png") + "' />");
            HtmlSource.Append("<input type=\"button\" class=\"HorizontalLine\"  onclick=\"pasteHtmlElements('horizontalLine','','')\" title=\"Insert Horizontal Line\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.page_break.png") + "' />");
            HtmlSource.Append("<input type=\"button\" class=\"pageBreak\"  onclick=\"pasteHtmlElements('pageBreaker','','')\" title=\"Page Break\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.printer.png") + "' />");
            HtmlSource.Append("<input type=\"button\" class=\"PrintButton\"  onclick=\"textEdit('Print')\" title=\"Print\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.eraser.png") + "' />");
            HtmlSource.Append("<input type=\"button\" class=\"eraserButton\"  onclick=\"textEdit('removeFormat')\" title=\"Remove Format\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            HtmlSource.Append("<input id=\"backColor\" type=\"button\" class=\"HighlighterButton\"  onclick=\"colorPickerAction(event,this.id,'ColorPickerDiv')\" title=\"Background Color\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            HtmlSource.Append("<input type=\"button\" class=\"SelectAll\" onclick=\"textEdit('SelectAll')\" title=\"Select All(Ctrl+A)\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("</tr>");
            HtmlSource.Append("</table>");
            HtmlSource.Append("</td>");
            #endregion



            //font size&family


            //cut,copy,...preview
            //-----------------------------------
            #region cut,copy,...preview
            HtmlSource.Append("</tr>");
            HtmlSource.Append("<tr>");
            HtmlSource.Append("<td>");
            HtmlSource.Append("</td>");
            HtmlSource.Append("</tr>");
            HtmlSource.Append("<tr>");
            HtmlSource.Append("<td class=\"Panels\">");
            HtmlSource.Append("<table cellpadding=\"0px\" cellspacing=\"1px\">");
            HtmlSource.Append("<tr>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input type=\"button\" class=\"Cut\" onclick=\"textEdit('cut')\" title=\"Cut(Ctrl+x)\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input type=\"button\" class=\"Paste\" onclick=\"textEdit('paste')\" title=\"Paste(Ctrl+v)\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input type=\"button\" class=\"Copy\" onclick=\"textEdit('copy')\" title=\"Copy(Ctrl+c)\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input title=\"Undo(Ctrl+z)\" class=\"undoButtons\" type=\"button\" onclick=\"textEdit('undo')\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input title=\"Redo(Ctrl+y)\" class=\"redoButtons\" type=\"button\" onclick=\"textEdit('redo')\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input  title=\"Save as\" type=\"button\" class=\"SaveasButton\"  onclick=\"saveAs()\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input title=\"View Source\" class=\"ViewSourceButton\" type=\"button\" onclick=\"switchToSourceMode();\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input title=\"Design\" class=\"ViewDesignModeButton\" type=\"button\" onclick=\"switchToDesignMode()\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("<td valign=\"bottom\">");
            //HtmlSource.Append("<img onclick=\"textEdit('bold')\" id=\"bold\" title=\"Bold\" class=\"BoldButtons\" alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.font_bold.png") + "' />");
            HtmlSource.Append("<input title=\"Preview\" class=\"previewButton\" type=\"button\" onclick=\"preview()\" />");
            HtmlSource.Append("</td>");
            HtmlSource.Append("</tr>");
            HtmlSource.Append("</table>");
            HtmlSource.Append("</td>");

            #endregion
            //resizer
            #region Resizer
            HtmlSource.Append("<td class=\"Panels\">");
            HtmlSource.Append("<table cellpadding=\"0px\" cellspacing=\"1px\">");
            HtmlSource.Append("<tr>");
            HtmlSource.Append("<td  valign=\"top\" class=\"slider\">");
            HtmlSource.Append("<div id=\"dd\" class=\"ltt\"></div>");
            HtmlSource.Append("&nbsp;");
            HtmlSource.Append("<img alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.resize_both.png") + "' /><input checked=\"checked\" type=\"radio\" value=\"both\" name=\"resizerRBT\"/>");
            HtmlSource.Append("<img alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.resize_horizontal.png") + "' /><input type=\"radio\" value=\"vertical\" name=\"resizerRBT\"/>");
            HtmlSource.Append("<img alt=\"\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.resize_vertical.png") + "' /><input type=\"radio\" value=\"horizontal\" name=\"resizerRBT\"/>");
            HtmlSource.Append("</td>");
            HtmlSource.Append("</tr>");
            HtmlSource.Append("</table>");

            HtmlSource.Append("</td>");
            HtmlSource.Append("</tr>");
            HtmlSource.Append("</table>");
            HtmlSource.Append("</td>");
            HtmlSource.Append("</tr>");
            #endregion

            HtmlSource.Append("</table>");
            HtmlSource.Append("</div>");
            HtmlSource.Append("</td>");
            HtmlSource.Append("</tr>");

            HtmlSource.Append("</table>");
            HtmlSource.Append("<table>");
            HtmlSource.Append("<tr>");
            HtmlSource.Append("<td align=\"left\">");
            HtmlSource.Append("<div id=\"DesignMode\" style=\"vertical-align:top\">");
            HtmlSource.Append("<table cellpadding=\"0px\" cellspacing=\"0px\">");
            HtmlSource.Append("<tr>");
            HtmlSource.Append("<td class=\"iFrameParentTd\" align=\"left\">");
            HtmlSource.Append("<div class=\"iFrameParentTd\" id=\"iframeDiv\">");
            HtmlSource.Append("<iframe frameborder=\"0\" id=\"textEditor\" class=\"iFrame\"></iframe>");
            HtmlSource.Append("</div>");
            HtmlSource.Append("<div>");
            HtmlSource.Append("<textarea style=\" display:none\" class=\"textArea\" id=\"sourceTxt\"></textarea>");
            HtmlSource.Append("</div>");
            HtmlSource.Append("</td>");
            HtmlSource.Append("</tr>");
            HtmlSource.Append("</table>");
            HtmlSource.Append("<div>");
            HtmlSource.Append("<div style=\"display:none\" id=\"ColorPickerDiv\" class=\"colorPickerDiv\">");
            HtmlSource.Append("<img  alt=\"\"  src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.palette.jpg") + "' usemap=\"#color_pallete\" />");
            HtmlSource.Append("<map  name=\"color_pallete\" id=\"color_pallete\">");
            HtmlSource.Append("<area alt=\"\" shape=\"rect\" coords=\"7,7,31,30\" href=\"#F20000\" onclick=\"getColor(this);\" />");
            HtmlSource.Append("<area alt=\"\" shape=\"rect\" coords=\"35,7,59,30\" href=\"#FCFB04\" onclick=\"getColor(this);\" />");
            HtmlSource.Append("<area alt=\"\" shape=\"rect\" coords=\"63,7,87,31\" href=\"#F9BBEF\"  onclick=\"getColor(this);\" />");
            HtmlSource.Append("<area alt=\"\" shape=\"rect\" coords=\"92,7,115,31\" href=\"#7B7B7B\" onclick=\"getColor(this);\" />");
            HtmlSource.Append("<area alt=\"\" shape=\"rect\" coords=\"7,33,30,58\" href=\"#008000\"  onclick=\"getColor(this);\"/>");
            HtmlSource.Append("<area alt=\"\" shape=\"rect\" coords=\"36,34,59,57\" href=\"#008080\" onclick=\"getColor(this);\" />");
            HtmlSource.Append("<area alt=\"\" shape=\"rect\" coords=\"63,34,86,58\" href=\"#0000ff\" onclick=\"getColor(this);\" />");
            HtmlSource.Append("<area alt=\"\" shape=\"rect\" coords=\"92,35,115,57\" href=\"#666699\" onclick=\"getColor(this);\" />");
            HtmlSource.Append("<area alt=\"\" shape=\"rect\" coords=\"8,61,31,85\" href=\"#339966\"  onclick=\"getColor(this);\" />");
            HtmlSource.Append("<area alt=\"\" shape=\"rect\" coords=\"35,62,59,85\" href=\"#33cccc\" onclick=\"getColor(this);\" />");
            HtmlSource.Append("<area alt=\"\" shape=\"rect\" coords=\"64,61,86,85\" href=\"#3366ff\" onclick=\"getColor(this);\" />");
            HtmlSource.Append("<area alt=\"\" shape=\"rect\" coords=\"92,61,115,85\" href=\"#800080\"  onclick=\"getColor(this);\" />");
            HtmlSource.Append("<area alt=\"\" shape=\"rect\" coords=\"91,88,115,111\" href=\"#FFFFFF\" onclick=\"getColor(this);\" />");
            HtmlSource.Append("<area alt=\"\" shape=\"rect\" coords=\"64,88,87,111\" href=\"#00ccff\" onclick=\"getColor(this);\" />");
            HtmlSource.Append("<area alt=\"\" shape=\"rect\" coords=\"36,88,59,112\" href=\"#00ffff\" onclick=\"getColor(this);\" />");
            HtmlSource.Append("<area alt=\"\" shape=\"rect\" coords=\"8,88,30,111\" href=\"#00ff00\" onclick=\"getColor(this);\" />");
            HtmlSource.Append("</map>");
            HtmlSource.Append("</div>");

            HtmlSource.Append("<span id=\"colorPalDemo\" name=\"colorPalDemo\" class=\"\" style=\"height:50px;width:50px;display:block;color:#ffffff;\"></span></div></div></td></tr></table></center>");


            HtmlSource.Append("<div style=\"display:none\" id=\"advanceColorPickerDiv\" class=\"advanceColorPickerDiv\">");
            HtmlSource.Append("<img border='0' style='margin-right: 2px;' src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.RichTextBoxIcons.colormap.gif") + "' usemap='#colormap' alt='colormap' /><map id='colormap' name='colormap' onmouseout='mouseOutMap()'>");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='63,0,72,4,72,15,63,19,54,15,54,4' onclick='setAdvanceColorPickerValue(\"#003366\")' alt='#003366' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='81,0,90,4,90,15,81,19,72,15,72,4' onclick='setAdvanceColorPickerValue(\"#336699\")' alt='#336699' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='99,0,108,4,108,15,99,19,90,15,90,4' onclick='setAdvanceColorPickerValue(\"#3366CC\")' alt='#3366CC' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='117,0,126,4,126,15,117,19,108,15,108,4' onclick='setAdvanceColorPickerValue(\"#003399\")' alt='#003399' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='135,0,144,4,144,15,135,19,126,15,126,4' onclick='setAdvanceColorPickerValue(\"#000099\")' alt='#000099' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='153,0,162,4,162,15,153,19,144,15,144,4' onclick='setAdvanceColorPickerValue(\"#0000CC\")' alt='#0000CC' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='171,0,180,4,180,15,171,19,162,15,162,4' onclick='setAdvanceColorPickerValue(\"#000066\")' alt='#000066' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='54,15,63,19,63,30,54,34,45,30,45,19' onclick='setAdvanceColorPickerValue(\"#006666\")' alt='#006666' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='72,15,81,19,81,30,72,34,63,30,63,19' onclick='setAdvanceColorPickerValue(\"#006699\")' alt='#006699' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='90,15,99,19,99,30,90,34,81,30,81,19' onclick='setAdvanceColorPickerValue(\"#0099CC\")' alt='#0099CC' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='108,15,117,19,117,30,108,34,99,30,99,19' onclick='setAdvanceColorPickerValue(\"#0066CC\")' alt='#0066CC' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='126,15,135,19,135,30,126,34,117,30,117,19' onclick='setAdvanceColorPickerValue(\"#0033CC\")' alt='#0033CC' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='144,15,153,19,153,30,144,34,135,30,135,19' onclick='setAdvanceColorPickerValue(\"#0000FF\")' alt='#0000FF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='162,15,171,19,171,30,162,34,153,30,153,19' onclick='setAdvanceColorPickerValue(\"#3333FF\")'  alt='#3333FF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='180,15,189,19,189,30,180,34,171,30,171,19' onclick='setAdvanceColorPickerValue(\"#333399\")'  alt='#333399' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='45,30,54,34,54,45,45,49,36,45,36,34' onclick='setAdvanceColorPickerValue(\"#669999\")' alt='#669999' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='63,30,72,34,72,45,63,49,54,45,54,34' onclick='setAdvanceColorPickerValue(\"#009999\")' alt='#009999' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='81,30,90,34,90,45,81,49,72,45,72,34' onclick='setAdvanceColorPickerValue(\"#33CCCC\")' alt='#33CCCC' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='99,30,108,34,108,45,99,49,90,45,90,34' onclick='setAdvanceColorPickerValue(\"#00CCFF\")' alt='#00CCFF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='117,30,126,34,126,45,117,49,108,45,108,34' onclick='setAdvanceColorPickerValue(\"#0099FF\")' alt='#0099FF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='135,30,144,34,144,45,135,49,126,45,126,34' onclick='setAdvanceColorPickerValue(\"#0066FF\")' alt='#0066FF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='153,30,162,34,162,45,153,49,144,45,144,34' onclick='setAdvanceColorPickerValue(\"#3366FF\")' alt='#3366FF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='171,30,180,34,180,45,171,49,162,45,162,34' onclick='setAdvanceColorPickerValue(\"#3333CC\")' alt='#3333CC' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='189,30,198,34,198,45,189,49,180,45,180,34' onclick='setAdvanceColorPickerValue(\"#666699\")' alt='#666699' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='36,45,45,49,45,60,36,64,27,60,27,49' onclick='setAdvanceColorPickerValue(\"#339966\")' alt='#339966' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='54,45,63,49,63,60,54,64,45,60,45,49' onclick='setAdvanceColorPickerValue(\"#00CC99\")' alt='#00CC99' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='72,45,81,49,81,60,72,64,63,60,63,49' onclick='setAdvanceColorPickerValue(\"#00FFCC\")' alt='#00FFCC' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='90,45,99,49,99,60,90,64,81,60,81,49' onclick='setAdvanceColorPickerValue(\"#00FFFF\")' alt='#00FFFF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='108,45,117,49,117,60,108,64,99,60,99,49' onclick='setAdvanceColorPickerValue(\"#33CCFF\")' alt='#33CCFF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='126,45,135,49,135,60,126,64,117,60,117,49' onclick='setAdvanceColorPickerValue(\"#3399FF\")' alt='#3399FF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='144,45,153,49,153,60,144,64,135,60,135,49' onclick='setAdvanceColorPickerValue(\"#6699FF\")' alt='#6699FF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='162,45,171,49,171,60,162,64,153,60,153,49' onclick='setAdvanceColorPickerValue(\"#6666FF\")' alt='#6666FF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='180,45,189,49,189,60,180,64,171,60,171,49' onclick='setAdvanceColorPickerValue(\"#6600FF\")' alt='#6600FF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='198,45,207,49,207,60,198,64,189,60,189,49' onclick='setAdvanceColorPickerValue(\"#6600CC\")' alt='#6600CC' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='27,60,36,64,36,75,27,79,18,75,18,64' onclick='setAdvanceColorPickerValue(\"#339933\")' alt='#339933' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='45,60,54,64,54,75,45,79,36,75,36,64' onclick='setAdvanceColorPickerValue(\"#00CC66\")' alt='#00CC66' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='63,60,72,64,72,75,63,79,54,75,54,64' onclick='setAdvanceColorPickerValue(\"#00FF99\")' alt='#00FF99' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='81,60,90,64,90,75,81,79,72,75,72,64' onclick='setAdvanceColorPickerValue(\"#66FFCC\")' alt='#66FFCC' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='99,60,108,64,108,75,99,79,90,75,90,64' onclick='setAdvanceColorPickerValue(\"#66FFFF\")' alt='#66FFFF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='117,60,126,64,126,75,117,79,108,75,108,64' onclick='setAdvanceColorPickerValue(\"#66CCFF\")' alt='#66CCFF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='135,60,144,64,144,75,135,79,126,75,126,64' onclick='setAdvanceColorPickerValue(\"#99CCFF\")' alt='#99CCFF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='153,60,162,64,162,75,153,79,144,75,144,64' onclick='setAdvanceColorPickerValue(\"#9999FF\")' alt='#9999FF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='171,60,180,64,180,75,171,79,162,75,162,64' onclick='setAdvanceColorPickerValue(\"#9966FF\")' alt='#9966FF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='189,60,198,64,198,75,189,79,180,75,180,64' onclick='setAdvanceColorPickerValue(\"#9933FF\")' alt='#9933FF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='207,60,216,64,216,75,207,79,198,75,198,64' onclick='setAdvanceColorPickerValue(\"#9900FF\")' alt='#9900FF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='18,75,27,79,27,90,18,94,9,90,9,79' onclick='setAdvanceColorPickerValue(\"#006600\")' alt='#006600' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='36,75,45,79,45,90,36,94,27,90,27,79' onclick='setAdvanceColorPickerValue(\"#00CC00\")' alt='#00CC00' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='54,75,63,79,63,90,54,94,45,90,45,79' onclick='setAdvanceColorPickerValue(\"#00FF00\")' alt='#00FF00' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='72,75,81,79,81,90,72,94,63,90,63,79' onclick='setAdvanceColorPickerValue(\"#66FF99\")' alt='#66FF99' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='90,75,99,79,99,90,90,94,81,90,81,79' onclick='setAdvanceColorPickerValue(\"#99FFCC\")' alt='#99FFCC' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='108,75,117,79,117,90,108,94,99,90,99,79' onclick='setAdvanceColorPickerValue(\"#CCFFFF\")' alt='#CCFFFF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='126,75,135,79,135,90,126,94,117,90,117,79' onclick='setAdvanceColorPickerValue(\"#CCCCFF\")' alt='#CCCCFF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='144,75,153,79,153,90,144,94,135,90,135,79' onclick='setAdvanceColorPickerValue(\"#CC99FF\")' alt='#CC99FF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='162,75,171,79,171,90,162,94,153,90,153,79' onclick='setAdvanceColorPickerValue(\"#CC66FF\")' alt='#CC66FF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='180,75,189,79,189,90,180,94,171,90,171,79' onclick='setAdvanceColorPickerValue(\"#CC33FF\")' alt='#CC33FF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='198,75,207,79,207,90,198,94,189,90,189,79' onclick='setAdvanceColorPickerValue(\"#CC00FF\")' alt='#CC00FF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='216,75,225,79,225,90,216,94,207,90,207,79' onclick='setAdvanceColorPickerValue(\"#9900CC\")' alt='#9900CC' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='9,90,18,94,18,105,9,109,0,105,0,94' onclick='setAdvanceColorPickerValue(\"#003300\")' alt='#003300' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='27,90,36,94,36,105,27,109,18,105,18,94' onclick='setAdvanceColorPickerValue(\"#009933\")' alt='#009933' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='45,90,54,94,54,105,45,109,36,105,36,94' onclick='setAdvanceColorPickerValue(\"#33CC33\")' alt='#33CC33' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='63,90,72,94,72,105,63,109,54,105,54,94' onclick='setAdvanceColorPickerValue(\"#66FF66\")' alt='#66FF66' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='81,90,90,94,90,105,81,109,72,105,72,94' onclick='setAdvanceColorPickerValue(\"#99FF99\")' alt='#99FF99' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='99,90,108,94,108,105,99,109,90,105,90,94' onclick='setAdvanceColorPickerValue(\"#CCFFCC\")' alt='#CCFFCC' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='117,90,126,94,126,105,117,109,108,105,108,94' onclick='setAdvanceColorPickerValue(\"#FFFFFF\")' alt='#FFFFFF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='135,90,144,94,144,105,135,109,126,105,126,94' onclick='setAdvanceColorPickerValue(\"#FFCCFF\")' alt='#FFCCFF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='153,90,162,94,162,105,153,109,144,105,144,94' onclick='setAdvanceColorPickerValue(\"#FF99FF\")' alt='#FF99FF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='171,90,180,94,180,105,171,109,162,105,162,94' onclick='setAdvanceColorPickerValue(\"#FF66FF\")' alt='#FF66FF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='189,90,198,94,198,105,189,109,180,105,180,94' onclick='setAdvanceColorPickerValue(\"#FF00FF\")' alt='#FF00FF' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='207,90,216,94,216,105,207,109,198,105,198,94' onclick='setAdvanceColorPickerValue(\"#CC00CC\")' alt='#CC00CC' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='225,90,234,94,234,105,225,109,216,105,216,94' onclick='setAdvanceColorPickerValue(\"#660066\")' alt='#660066' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='18,105,27,109,27,120,18,124,9,120,9,109' onclick='setAdvanceColorPickerValue(\"#336600\")' alt='#336600' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='36,105,45,109,45,120,36,124,27,120,27,109' onclick='setAdvanceColorPickerValue(\"#009900\")' alt='#009900' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='54,105,63,109,63,120,54,124,45,120,45,109' onclick='setAdvanceColorPickerValue(\"#66FF33\")' alt='#66FF33' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='72,105,81,109,81,120,72,124,63,120,63,109' onclick='setAdvanceColorPickerValue(\"#99FF66\")' alt='#99FF66' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='90,105,99,109,99,120,90,124,81,120,81,109' onclick='setAdvanceColorPickerValue(\"#CCFF99\")' alt='#CCFF99' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='108,105,117,109,117,120,108,124,99,120,99,109' onclick='setAdvanceColorPickerValue(\"#FFFFCC\")' alt='#FFFFCC' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='126,105,135,109,135,120,126,124,117,120,117,109' onclick='setAdvanceColorPickerValue(\"#FFCCCC\")' alt='#FFCCCC' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='144,105,153,109,153,120,144,124,135,120,135,109' onclick='setAdvanceColorPickerValue(\"#FF99CC\")' alt='#FF99CC' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='162,105,171,109,171,120,162,124,153,120,153,109' onclick='setAdvanceColorPickerValue(\"#FF66CC\")' alt='#FF66CC' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='180,105,189,109,189,120,180,124,171,120,171,109' onclick='setAdvanceColorPickerValue(\"#FF33CC\")' alt='#FF33CC' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='198,105,207,109,207,120,198,124,189,120,189,109' onclick='setAdvanceColorPickerValue(\"#CC0099\")' alt='#CC0099' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='216,105,225,109,225,120,216,124,207,120,207,109' onclick='setAdvanceColorPickerValue(\"#993399\")' alt='#993399' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='27,120,36,124,36,135,27,139,18,135,18,124' onclick='setAdvanceColorPickerValue(\"#333300\")' alt='#333300' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='45,120,54,124,54,135,45,139,36,135,36,124' onclick='setAdvanceColorPickerValue(\"#669900\")' alt='#669900' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='63,120,72,124,72,135,63,139,54,135,54,124' onclick='setAdvanceColorPickerValue(\"#99FF33\")' alt='#99FF33' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='81,120,90,124,90,135,81,139,72,135,72,124' onclick='setAdvanceColorPickerValue(\"#CCFF66\")' alt='#CCFF66' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='99,120,108,124,108,135,99,139,90,135,90,124' onclick='setAdvanceColorPickerValue(\"#FFFF99\")' alt='#FFFF99' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='117,120,126,124,126,135,117,139,108,135,108,124' onclick='setAdvanceColorPickerValue(\"#FFCC99\")' alt='#FFCC99' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='135,120,144,124,144,135,135,139,126,135,126,124' onclick='setAdvanceColorPickerValue(\"#FF9999\")' alt='#FF9999' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='153,120,162,124,162,135,153,139,144,135,144,124' onclick='setAdvanceColorPickerValue(\"#FF6699\")' alt='#FF6699' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='171,120,180,124,180,135,171,139,162,135,162,124' onclick='setAdvanceColorPickerValue(\"#FF3399\")' alt='#FF3399' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='189,120,198,124,198,135,189,139,180,135,180,124' onclick='setAdvanceColorPickerValue(\"#CC3399\")' alt='#CC3399' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='207,120,216,124,216,135,207,139,198,135,198,124' onclick='setAdvanceColorPickerValue(\"#990099\")' alt='#990099' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='36,135,45,139,45,150,36,154,27,150,27,139' onclick='setAdvanceColorPickerValue(\"#666633\")' alt='#666633' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='54,135,63,139,63,150,54,154,45,150,45,139' onclick='setAdvanceColorPickerValue(\"#99CC00\")' alt='#99CC00' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='72,135,81,139,81,150,72,154,63,150,63,139' onclick='setAdvanceColorPickerValue(\"#CCFF33\")' alt='#CCFF33' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='90,135,99,139,99,150,90,154,81,150,81,139' onclick='setAdvanceColorPickerValue(\"#FFFF66\")' alt='#FFFF66' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='108,135,117,139,117,150,108,154,99,150,99,139' onclick='setAdvanceColorPickerValue(\"#FFCC66\")' alt='#FFCC66' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='126,135,135,139,135,150,126,154,117,150,117,139' onclick='setAdvanceColorPickerValue(\"#FF9966\")' alt='#FF9966' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='144,135,153,139,153,150,144,154,135,150,135,139' onclick='setAdvanceColorPickerValue(\"#FF6666\")' alt='#FF6666' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='162,135,171,139,171,150,162,154,153,150,153,139' onclick='setAdvanceColorPickerValue(\"#FF0066\")' alt='#FF0066' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='180,135,189,139,189,150,180,154,171,150,171,139' onclick='setAdvanceColorPickerValue(\"#CC6699\")' alt='#CC6699' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='198,135,207,139,207,150,198,154,189,150,189,139' onclick='setAdvanceColorPickerValue(\"#993366\")' alt='#993366' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='45,150,54,154,54,165,45,169,36,165,36,154' onclick='setAdvanceColorPickerValue(\"#999966\")' alt='#999966' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='63,150,72,154,72,165,63,169,54,165,54,154' onclick='setAdvanceColorPickerValue(\"#CCCC00\")' alt='#CCCC00' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='81,150,90,154,90,165,81,169,72,165,72,154' onclick='setAdvanceColorPickerValue(\"#FFFF00\")' alt='#FFFF00' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='99,150,108,154,108,165,99,169,90,165,90,154' onclick='setAdvanceColorPickerValue(\"#FFCC00\")' alt='#FFCC00' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='117,150,126,154,126,165,117,169,108,165,108,154' onclick='setAdvanceColorPickerValue(\"#FF9933\")' alt='#FF9933' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='135,150,144,154,144,165,135,169,126,165,126,154' onclick='setAdvanceColorPickerValue(\"#FF6600\")' alt='#FF6600' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='153,150,162,154,162,165,153,169,144,165,144,154' onclick='setAdvanceColorPickerValue(\"#FF5050\")' alt='#FF5050' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='171,150,180,154,180,165,171,169,162,165,162,154' onclick='setAdvanceColorPickerValue(\"#CC0066\")' alt='#CC0066' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='189,150,198,154,198,165,189,169,180,165,180,154' onclick='setAdvanceColorPickerValue(\"#660033\")' alt='#660033' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='54,165,63,169,63,180,54,184,45,180,45,169' onclick='setAdvanceColorPickerValue(\"#996633\")' alt='#996633' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='72,165,81,169,81,180,72,184,63,180,63,169' onclick='setAdvanceColorPickerValue(\"#CC9900\")' alt='#CC9900' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='90,165,99,169,99,180,90,184,81,180,81,169' onclick='setAdvanceColorPickerValue(\"#FF9900\")' alt='#FF9900' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='108,165,117,169,117,180,108,184,99,180,99,169' onclick='setAdvanceColorPickerValue(\"#CC6600\")' alt='#CC6600' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='126,165,135,169,135,180,126,184,117,180,117,169' onclick='setAdvanceColorPickerValue(\"#FF3300\")' alt='#FF3300' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='144,165,153,169,153,180,144,184,135,180,135,169' onclick='setAdvanceColorPickerValue(\"#FF0000\")'alt='#FF0000' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='162,165,171,169,171,180,162,184,153,180,153,169' onclick='setAdvanceColorPickerValue(\"#CC0000\")' alt='#CC0000' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='180,165,189,169,189,180,180,184,171,180,171,169' onclick='setAdvanceColorPickerValue(\"#990033\")' alt='#990033' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='63,180,72,184,72,195,63,199,54,195,54,184' onclick='setAdvanceColorPickerValue(\"#663300\")' alt='#663300' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='81,180,90,184,90,195,81,199,72,195,72,184' onclick='setAdvanceColorPickerValue(\"#996600\")' alt='#996600' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='99,180,108,184,108,195,99,199,90,195,90,184' onclick='setAdvanceColorPickerValue(\"#CC3300\")' alt='#CC3300' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='117,180,126,184,126,195,117,199,108,195,108,184' onclick='setAdvanceColorPickerValue(\"#993300\")' alt='#993300' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='135,180,144,184,144,195,135,199,126,195,126,184' onclick='setAdvanceColorPickerValue(\"#990000\")' alt='#990000' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='153,180,162,184,162,195,153,199,144,195,144,184' onclick='setAdvanceColorPickerValue(\"#800000\")' alt='#800000' />");
            HtmlSource.Append("<area style='cursor: pointer' shape='poly' coords='171,180,180,184,180,195,171,199,162,195,162,184' onclick='setAdvanceColorPickerValue(\"#993333\")' alt='#993333' />");
            HtmlSource.Append("</map>");
            HtmlSource.Append("</div>");
            HtmlSource.Append("</div>");
            HtmlSource.Append("<script language=\"javascript\" src='" + CurrentPage.ClientScript.GetWebResourceUrl(typeof(APTemplate.HtmlEditor), "APTemplate.Resources.HtmlEditor.js.RichText.js") + "'></script>");
            _HtmlSource = HtmlSource;
        }
        #endregion
    }
}
