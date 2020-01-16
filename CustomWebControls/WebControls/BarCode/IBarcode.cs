using System;
using System.Collections.Generic;
using System.Text;

namespace APTemplate
{
    interface IBarcode
    {
        string Encoded_Value
        {
            get;
        }

        string RawData
        {
            get;
        }

        string FormattedData
        {
            get;
        }
    }
}
