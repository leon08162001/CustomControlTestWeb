using System;
using System.Web.UI;

namespace APTemplate
{
    /// <summary>
    /// The ControlAConverter class inherits from <see cref="ServerControlConverter"/>.
    /// It overrides the method IncludeControl which only returns true if the control passed
    /// to it is of type PageMaker
    /// </summary>
    public class PageMakerConverter : ServerControlConverter
    {
        /// <summary>
        /// Returns true when <param>serverControl</param> is of type <see cref="PageMaker"/>, false otherwise.
        /// </summary>
        /// <param name="serverControl">The control to examine for its type</param>
        /// <returns>A <see cref="bool"/> indicating whether the passed in control is of type <see cref="PageMaker"/> or not.</returns>
        protected override Boolean IncludeControl(Control serverControl)
        {
            if (serverControl is PageMaker)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
