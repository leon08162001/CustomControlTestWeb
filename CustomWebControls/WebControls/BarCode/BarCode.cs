using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Design;
using System.Web.Caching;
using System.Web.UI.Design.WebControls;

namespace APTemplate
{
    /// <summary>
    /// 自定義的BarCode控制項。
    /// </summary>
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:BarCode runat=server></{0}:BarCode>")]
    [ToolboxBitmap(typeof(BarCode), "Resources.barcode.bmp")]
    public class BarCode : CompositeControl, INamingContainer
    {
        public enum BarImageFormat
        {
            JPEG, PNG, GIF, BMP
        }
        public enum AlignType
        {
            Left, Center, Right
        }

        public enum BarCodeWeightType
        {
            Small = 1, Medium, Large
        }

        public enum BarCodeTypeOption
        {
            Codabar =1,
            Code11 = 2,
            Code128 = 3,
            Code128_A = 4,
            Code128_B = 5,
            Code128_C = 6,      
            Code39 = 7,
            Code93 = 8,
            Ean13 = 9, 
            Ean8 = 10, 
            FIM =11,
            Interleaved2of5 = 12,
            ISBN = 13,
            ITF14 = 14,
            JAN13 = 15,
            MSI = 16,
            Pharmacode = 17,
            Postnet = 18,
            Standard2of5 = 19,
            Telepen = 20,
            UPC_A = 21,
            UPC_E = 22,
            UPCSupplement2 = 23,
            UPCSupplement5 = 24
        }

        public enum ScaleOption
        {
            _08 = 8,
            _09 = 9,
            _10 = 10,
            _11 = 11,
            _12 = 12,
            _13 = 13,
            _14 = 14,
            _15 = 15,
            _16 = 16,
            _17 = 17,
            _18 = 18,
            _19 = 19,
            _20 = 20
        }

        private BarImageFormat _ImageFormat = BarImageFormat.JPEG;
        private AlignType _TitleAlign = AlignType.Center;
        private AlignType _TextAlign = AlignType.Center;
        private String _BarCodeText = "";
        private Unit _BarCodeHeight = 50;
        private bool _IsShowTitle = true;
        private bool _IsShowPrice = true;
        private bool _IsShowBarText = true;
        private BarCodeTypeOption _BarCodeType = BarCodeTypeOption.Code39;
        private String _Title = "請輸入產品名稱:";
        private BarCodeWeightType _BarCodeWeight = BarCodeWeightType.Small;
        private Font _TitleFont = new Font("Courier", 8);
        private Font _PriceFont = new Font("Courier", 8);
        private Font _TextFont = new Font("Courier", 8);
        private ScaleOption _Scale = ScaleOption._08;
        private int _Price = 0;

        protected Table Table1 = new Table();
        protected TableRow TableRow1 = new TableRow();
        protected TableRow TableRow2 = new TableRow();
        protected TableRow TableRow3 = new TableRow();
        protected TableRow TableRow4 = new TableRow();
        protected TableCell TableCell1 = new TableCell();
        protected TableCell TableCell2 = new TableCell();
        protected TableCell TableCell3 = new TableCell();
        protected TableCell TableCell4 = new TableCell();
        protected Label TitleLbl = new Label();
        protected Label PriceLbl = new Label();
        protected System.Web.UI.WebControls.Image BarCodeImg = new System.Web.UI.WebControls.Image();
        protected Label BarCodeLbl = new Label();


        #region Public Properties
        /// <summary>
        /// 產生BarCode圖檔的格式。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("產生BarCode圖檔的格式。")]
        public BarImageFormat ImageFormat
        {
            get { return _ImageFormat; }
            set { _ImageFormat = value; }
        }
        /// <summary>
        /// BarCode標題文字對齊方式。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("BarCode標題文字對齊方式。")]
        public AlignType TitleAlign
        {
            get { return _TitleAlign; }
            set { _TitleAlign = value; }
        }

        /// <summary>
        /// BarCode碼對齊方式。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("BarCode碼對齊方式。")]
        public AlignType TextAlign
        {
            get { return _TextAlign; }
            set { _TextAlign = value; }
        }

        /// <summary>
        /// BarCode控制項的BarCode碼。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("BarCode控制項的BarCode碼。")]
        public String BarCodeText
        {
            get { return _BarCodeText; }
            set { _BarCodeText = value.ToUpper(); }
        }

        /// <summary>
        /// BarCode條碼圖形的高度。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("BarCode條碼圖形的高度。")]
        public Unit BarCodeHeight
        {
            get { return _BarCodeHeight; }
            set { _BarCodeHeight = value; }
        }

        /// <summary>
        /// 是否顯示標題文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("是否顯示標題文字。")]
        public bool IsShowTitle
        {
            get { return _IsShowTitle; }
            set { _IsShowTitle = value; }
        }

        /// <summary>
        /// 是否顯示價格文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("是否顯示價格文字。")]
        public bool IsShowPrice
        {
            get { return _IsShowPrice; }
            set { _IsShowPrice = value; }
        }

        /// <summary>
        /// 是否顯示BarCode碼文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("是否顯示BarCode碼文字。")]
        public bool IsShowBarText
        {
            get { return _IsShowBarText; }
            set { _IsShowBarText = value; }
        }

        /// <summary>
        /// BarCode標題文字。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("BarCode標題文字。")]
        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        /// <summary>
        /// BarCode產品的價格。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("BarCode產品的價格。")]
        public int Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        /// <summary>
        /// 小BarCode柱狀圖形的粗細程度。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("BarCode柱狀圖形的粗細程度。")]
        public BarCodeWeightType BarCodeWeight
        {
            get { return _BarCodeWeight; }
            set { _BarCodeWeight = value; }
        }

        /// <summary>
        /// BarCode標題文字的字型。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("BarCode標題文字的字型。")]
        public Font TitleFont
        {
            get { return _TitleFont; }
            set { _TitleFont = value; }
        }

        /// <summary>
        /// BarCode價格文字的字型。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("BarCode價格文字的字型。")]
        public Font PriceFont
        {
            get { return _PriceFont; }
            set { _PriceFont = value; }
        }

        /// <summary>
        /// BarCode碼文字的字型。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("BarCode碼文字的字型。")]
        public Font TextFont
        {
            get { return _TextFont; }
            set { _TextFont = value; }
        }

        /// <summary>
        /// BarCode條碼的類型。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description("BarCode條碼的類型。")]
        public BarCodeTypeOption BarCodeType
        {
            get { return _BarCodeType; }
            set { _BarCodeType = value; }
        }

        /// <summary>
        /// BarCode條碼放大縮小比例。
        /// </summary>
        [DefaultValue(""),
         Category("自訂"),
         Description(" BarCode條碼放大縮小比例。")]
        public ScaleOption Scale
        {
            get { return _Scale; }
            set { _Scale = value; }
        }
        #endregion

        #region Public Methods

        public static bool CheckNumericOnly(string Data)
        {
            //This function takes a string of data and breaks it into parts and trys to do Int64.TryParse
            //This will verify that only numeric data is contained in the string passed in.  The complexity below
            //was done to ensure that the minimum number of interations and checks could be performed.

            //9223372036854775808 is the largest number a 64bit number(signed) can hold so ... make sure its less than that by one place
            int STRING_LENGTHS = 18;

            string temp = Data;
            string[] strings = new string[(Data.Length / STRING_LENGTHS) + ((Data.Length % STRING_LENGTHS == 0) ? 0 : 1)];

            int i = 0;
            while (i < strings.Length)
                if (temp.Length >= STRING_LENGTHS)
                {
                    strings[i++] = temp.Substring(0, STRING_LENGTHS);
                    temp = temp.Substring(STRING_LENGTHS);
                }//if
                else
                    strings[i++] = temp.Substring(0);

            foreach (string s in strings)
            {
                long value = 0;
                if (!Int64.TryParse(s, out value))
                    return false;
            }//foreach

            return true;
        }
        #endregion

        #region Protected Methods

        protected override void CreateChildControls()
        {
            EnsureChildControls();
            //BarCodeImg
            string ImgUrl = "";
            if (this.DesignMode)
            {
                ImgUrl += "BarCodeImage.aspx?BarCode=" + this.BarCodeText + "&ImageFormat=" + Convert.ToInt16(this.ImageFormat) + "&type=" + this.BarCodeType.ToString() + "&scale=" + Convert.ToSingle(this.Scale) + "&height=" + Convert.ToInt16(this.BarCodeHeight.Value) + "&weight=" + Convert.ToInt16(this.BarCodeWeight);
            }
            else
            {
                string sGuid = Guid.NewGuid().ToString();
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_BarCode", this.BarCodeText, null, DateTime.Now.AddMinutes(20), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_ImageFormat", Convert.ToInt16(this.ImageFormat), null, DateTime.Now.AddMinutes(20), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_type", this.BarCodeType, null, DateTime.Now.AddMinutes(20), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_scale", Convert.ToSingle(this.Scale), null, DateTime.Now.AddMinutes(20), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_height", Convert.ToInt16(this.BarCodeHeight.Value), null, DateTime.Now.AddMinutes(20), Cache.NoSlidingExpiration);
                HttpContext.Current.Cache.Insert(this.ClientID + "_" + sGuid + "_weight", Convert.ToInt16(this.BarCodeWeight), null, DateTime.Now.AddMinutes(20), Cache.NoSlidingExpiration);
                ImgUrl += "BarCodeImage.aspx?id=" + this.ClientID + "_" + sGuid;

            }
            BarCodeImg.ImageUrl = ImgUrl;
            BarCodeImg.AlternateText = "產出BarCode影像時發生錯誤";
            BarCodeImg.BorderWidth = Unit.Pixel(0);

            //TitleLbl
            TitleLbl.Text = this.Title;
            TitleLbl.Font.Bold = this.TitleFont.Bold;
            TitleLbl.Font.Italic = this.TitleFont.Italic;
            TitleLbl.Font.Name = this.TitleFont.Name;
            TitleLbl.Font.Strikeout = this.TitleFont.Strikeout;
            TitleLbl.Font.Underline = this.TitleFont.Underline;
            TitleLbl.Font.Size = FontUnit.Parse(TitleFont.Size.ToString());
            TitleLbl.Visible = this.IsShowTitle;

            //PriceLbl
            PriceLbl.Text = "NT: " + this.Price.ToString();
            PriceLbl.Font.Bold = this.PriceFont.Bold;
            PriceLbl.Font.Italic = this.PriceFont.Italic;
            PriceLbl.Font.Name = this.PriceFont.Name;
            PriceLbl.Font.Strikeout = this.PriceFont.Strikeout;
            PriceLbl.Font.Underline = this.PriceFont.Underline;
            PriceLbl.Font.Size = FontUnit.Parse(PriceFont.Size.ToString());
            PriceLbl.Visible = this.IsShowPrice;

            //BarCodeLbl
            BarCodeLbl.Text = this.BarCodeText;
            BarCodeLbl.Font.Bold = this.TextFont.Bold;
            BarCodeLbl.Font.Italic = this.TextFont.Italic;
            BarCodeLbl.Font.Name = this.TextFont.Name;
            BarCodeLbl.Font.Strikeout = this.TextFont.Strikeout;
            BarCodeLbl.Font.Underline = this.TextFont.Underline;
            BarCodeLbl.Font.Size = FontUnit.Parse(TextFont.Size.ToString());
            BarCodeLbl.Visible = this.IsShowBarText;

            //TableCell1
            TableCell1.HorizontalAlign = this.TitleAlign == AlignType.Left ? HorizontalAlign.Left : this.TitleAlign == AlignType.Right ? HorizontalAlign.Right : HorizontalAlign.Center;
            TableCell1.Controls.Add(TitleLbl);

            //TableCell2
            TableCell2.HorizontalAlign = this.TitleAlign == AlignType.Left ? HorizontalAlign.Left : this.TitleAlign == AlignType.Right ? HorizontalAlign.Right : HorizontalAlign.Center;
            TableCell2.Controls.Add(PriceLbl);

            //TableCell3
            TableCell3.Controls.Add(BarCodeImg);

            //TableCell4
            TableCell4.HorizontalAlign = this.TextAlign == AlignType.Left ? HorizontalAlign.Left : this.TextAlign == AlignType.Right ? HorizontalAlign.Right : HorizontalAlign.Center;
            TableCell4.Controls.Add(BarCodeLbl);

            //TableRow1
            TableRow1.Controls.Add(TableCell1);

            //TableRow2
            TableRow2.Controls.Add(TableCell2);

            //TableRow3
            TableRow3.Controls.Add(TableCell3);

            //TableRow4
            TableRow4.Controls.Add(TableCell4);

            //Table1
            if (!this.DesignMode)
            {
                if (Context.Request.Browser.Type.IndexOf("IE") != -1 && float.Parse(Context.Request.Browser.Version) < 8)
                {
                    Table1.Attributes["style"] += "display:inline;vertical-align:top;";
                }
                else
                {
                    Table1.Attributes["style"] += "display:inline-block;vertical-align:top;";
                }
            }
            else
            {
                Table1.Attributes["style"] += "display:inline;vertical-align:top;";
            }
            Table1.Attributes["style"] += CssStyle();
            Table1.CssClass = CssClass;
            Table1.Controls.Add(TableRow1);
            Table1.Controls.Add(TableRow2);
            Table1.Controls.Add(TableRow3);
            Table1.Controls.Add(TableRow4);

            this.Controls.Add(Table1);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            Table1.RenderControl(writer);
        }

        #endregion

        #region Private Properties & Methods

        ///<summary>
        ///are we in design mode?
        ///</summary>
        private bool IsDesignMode
        {
            get { return (HttpContext.Current == null); }
        }

        ///<summary>
        ///returns HTML-ized color strings
        ///</summary>
        private string HtmlColor(System.Drawing.Color color)
        {
            if (color.IsEmpty)
                return "";
            if (color.IsNamedColor)
                return color.ToKnownColor().ToString();
            if (color.IsSystemColor)
                return color.ToString();
            return "#" + color.ToArgb().ToString("x").Substring(2);
        }

        ///<summary>
        ///returns css "style=" tag for this control
        ///based on standard control visual properties
        ///</summary>
        private string CssStyle()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string strColor;
            if (BorderWidth.ToString().Length > 0)
            {
                sb.Append("border-width:");
                sb.Append(BorderWidth.ToString());
                sb.Append(";");
            }
            if (BorderStyle != System.Web.UI.WebControls.BorderStyle.NotSet)
            {
                sb.Append("border-style:");
                sb.Append(BorderStyle.ToString());
                sb.Append(";");
            }
            strColor = HtmlColor(BorderColor);
            if (strColor.Length > 0)
            {
                sb.Append("border-color:");
                sb.Append(strColor);
                sb.Append(";");
            }
            strColor = HtmlColor(BackColor);
            if (strColor.Length > 0)
            {
                sb.Append("background-color:" + strColor + ";");
            }
            strColor = HtmlColor(ForeColor);
            if (strColor.Length > 0)
            {
                sb.Append("color:" + strColor + ";");
            }
            if (Font.Bold)
            {
                sb.Append("font-weight:bold;");
            }
            if (Font.Italic)
            {
                sb.Append("font-style:italic;");
            }
            if (Font.Underline)
            {
                sb.Append("text-decoration:underline;");
            }
            if (Font.Strikeout)
            {
                sb.Append("text-decoration:line-through;");
            }
            if (Font.Overline)
            {
                sb.Append("text-decoration:overline;");
            }

            if (Font.Size.ToString().Length > 0)
            {
                sb.Append("font-size:" + Font.Size.ToString() + ";");
            }

            if (Font.Names.Length > 0)
            {
                sb.Append("font-family:");
                foreach (string strFontFamily in Font.Names)
                {
                    sb.Append(strFontFamily);
                    sb.Append(",");
                }
                sb.Length = sb.Length - 1;
                sb.Append(";");
            }

            if (Height.ToString() != "")
            {
                sb.Append("height:" + Height.ToString() + ";");
            }

            if (Width.ToString() != "")
            {
                sb.Append("width" + Width.ToString() + ";");
            }

            if (sb.ToString() == " style=''")
                return "";
            else
                return sb.ToString();
        }

        #endregion
    }
}