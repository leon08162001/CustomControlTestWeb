using System;
using System.ComponentModel;

namespace APTemplate
{
    [Serializable()]
    public class ActiveXParam
    {
        String FName = null;
        String FValue = null;

        public ActiveXParam()
        {

        }

        public ActiveXParam(string Name, string Value)
        {
            FName = Name;
            FValue = Value;
        }

        /// <summary>
        ///參數名稱。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("參數名稱。")]
        public string Name
        {
            get { return FName; }
            set { FName = value; }
        }

        /// <summary>
        ///參數值。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("參數值。")]
        public string Value
        {
            get { return FValue; }
            set { FValue = value; }
        }
    }
}