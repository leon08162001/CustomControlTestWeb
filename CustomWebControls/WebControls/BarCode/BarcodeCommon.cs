using System;
using System.Collections.Generic;
using System.Text;

namespace APTemplate
{
    abstract class BarcodeCommon
    {
        protected string Raw_Data = "";
        protected string Formatted_Data = "";
        protected List<string> _Errors = new List<string>();

        #region Public Properties & Methods

        public string RawData
        {
            get { return this.Raw_Data; }
        }

        public string FormattedData
        {
            get { return this.Formatted_Data; }
            set { this.Formatted_Data = value; }
        }

        public List<string> Errors
        {
            get { return this._Errors; }
        }

        public void Error(string ErrorMessage)
        {
            this._Errors.Add(ErrorMessage);
            throw new Exception(ErrorMessage);
        }

        public bool IsNumeric(string input)
        {
            try
            {
                Int32 i32temp = new Int32();
                if (!Int32.TryParse(input, out i32temp))
                {
                    //parse didnt work so check each char because it may just be too long.
                    foreach (char c in input)
                    {
                        if (!char.IsDigit(c))
                            return false;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
