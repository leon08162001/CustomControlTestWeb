using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text;

namespace APTemplate
{
    public class PageSizeCustomEventArgs : EventArgs
    {
        private DropDownList _PageSizeSelect;
        public PageSizeCustomEventArgs(DropDownList PageSizeSelect)
        {
            _PageSizeSelect = PageSizeSelect;
        }
        public DropDownList PageSizeSelect
        {
            get { return _PageSizeSelect; }
        }
    }
}
