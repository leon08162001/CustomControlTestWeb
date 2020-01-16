using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
namespace APTemplate
{
    /// <summary>
    /// 圖形驗證碼圖形物件類別
    /// </summary>
    [Serializable()]
    public class CaptchaImage
    {
        private Int32 _height;
        private Int32 _width;
        private Random _rand;
        private DateTime _generatedAt;
        private string _randomText;
        private Int32 _randomTextLength;
        private string _randomTextChars;
        private Font _fontFamilyName;
        private FontWarpFactor _fontWarp;
        private BackgroundNoiseLevel _backgroundNoise;
        private LineNoiseLevel _lineNoise;
        private string _guid;

        #region Public Enums
        ///<summary>
        ///Amount of random font warping to apply to rendered text
        ///</summary>
        public enum FontWarpFactor
        {
            None,
            Low,
            Medium,
            High,
            Extreme
        }

        ///<summary>
        ///Amount of random font warping to apply to rendered text
        ///</summary>
        public enum BackgroundNoiseLevel
        {
            None,
            Low,
            Medium,
            High,
            Extreme
        }

        ///<summary>
        ///Amount of curved line noise to add to rendered image
        ///</summary>
        public enum LineNoiseLevel
        {
            None,
            Low,
            Medium,
            High,
            Extreme
        }
        #endregion

        #region	Public Properties & Methods
        ///<summary>
        ///Returns a GUID that uniquely identifies this Captcha
        ///</summary>
        public string UniqueId
        {
            get { return _guid; }
        }

        ///<summary>
        ///Returns the date and time this image was last rendered
        ///</summary>
        public DateTime RenderedAt
        {
            get { return _generatedAt; }
        }

        ///<summary>
        ///圖形驗證碼的字型設定.
        ///</summary>
        public Font Font
        {
            get { return _fontFamilyName; }
            set
            {
                try
                {
                    _fontFamilyName = value;
                }
                catch (Exception ex)
                {
                    _fontFamilyName = new System.Drawing.Font(FontFamily.GenericSerif, 28);
                }
            }
        }

        ///<summary>
        ///圖形驗證碼文字扭曲的程度.
        ///</summary>
        public FontWarpFactor FontWarp
        {
            get { return _fontWarp; }
            set { _fontWarp = value; }
        }

        ///<summary>
        ///圖形驗證碼背景雜訊的程度.
        ///</summary>
        public BackgroundNoiseLevel BackgroundNoise
        {
            get { return _backgroundNoise; }
            set { _backgroundNoise = value; }
        }

        ///<summary>
        ///Amount of random warping to apply to the Captcha text.
        ///</summary>
        public LineNoiseLevel LineNoise
        {
            get { return _lineNoise; }
            set { _lineNoise = value; }
        }

        ///<summary>
        ///出現在圖形驗證碼控制項的文字總集合. 
        ///</summary>
        public string TextChars
        {
            get { return _randomTextChars; }
            set
            {
                _randomTextChars = value;
                _randomText = GenerateRandomText();
            }
        }

        ///<summary>
        ///產生圖形驗證碼的長度.
        ///</summary>
        public Int32 TextLength
        {
            get { return _randomTextLength; }
            set
            {
                _randomTextLength = value;
                _randomText = GenerateRandomText();
            }
        }

        ///<summary>
        ///圖形驗證碼控制項所呈現的驗證碼文字.
        ///</summary>
        public string Text
        {
            get { return _randomText; }
        }

        ///<summary>
        ///圖形驗證碼控制項圖形的寬度.
        ///</summary>
        public Int32 Width
        {
            get { return _width; }
            set
            {
                if (value < 60)
                    throw new ArgumentOutOfRangeException("width", value, "寬度必須大於等於60.");
                _width = value;
            }
        }

        ///<summary>
        ///圖形驗證碼控制項圖形的高度.
        ///</summary>
        public Int32 Height
        {
            get { return _height; }
            set
            {
                if (value < 20)
                    throw new ArgumentOutOfRangeException("height", value, "高度必須大於等於20.");
                _height = value;
            }
        }

        public CaptchaImage()
        {
            _rand = new Random();
            _fontWarp = FontWarpFactor.None;
            _backgroundNoise = BackgroundNoiseLevel.Low;
            _lineNoise = LineNoiseLevel.None;
            _width = 125;
            _height = 30;
            _randomTextLength = 5;
            _randomTextChars = "ACDEFGHJKLNPQRTUVXYZ2346789";
            _fontFamilyName = new Font(FontFamily.GenericSerif, 28);
            _randomText = GenerateRandomText();
            _generatedAt = DateTime.Now;
            _guid = Guid.NewGuid().ToString();
        }

        ///<summary>
        ///Forces a new Captcha image to be generated using current property value settings.
        ///</summary>
        public Bitmap RenderImage()
        {
            return GenerateImagePrivate();
        }

        #endregion

        #region Private Properties & Methods

        ///<summary>
        /// generate random text for the CAPTCHA
        ///</summary>
        private string GenerateRandomText()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(_randomTextLength);
            Int32 maxLength = _randomTextChars.Length;
            for (int i = 0; i < _randomTextLength; i++)
            {
                sb.Append(_randomTextChars.Substring(_rand.Next(maxLength), 1));
            }
            System.Threading.Thread.Sleep(15);
            return sb.ToString();
        }

        ///<summary>
        ///Returns a random point within the specified x and y ranges
        ///</summary>
        private PointF RandomPoint(int xmin, int xmax, ref int ymin, ref int ymax)
        {
            return new PointF(_rand.Next(xmin, xmax), _rand.Next(ymin, ymax));
        }

        ///<summary>
        ///Returns a random point within the specified rectangle
        ///</summary>
        private PointF RandomPoint(Rectangle rect)
        {
            int ymin = rect.Top;
            int ymax = rect.Bottom;
            return RandomPoint(rect.Left, rect.Width, ref ymin, ref ymax);
        }

        ///<summary>
        ///Returns a GraphicsPath containing the specified string and font
        ///</summary>
        private GraphicsPath TextPath(string s, Font f, Rectangle r)
        {
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Near;
            sf.LineAlignment = StringAlignment.Near;
            GraphicsPath gp = new GraphicsPath();
            gp.AddString(s, f.FontFamily, Convert.ToInt32(f.Style), f.Size, r, sf);
            return gp;
        }

        ///<summary>
        ///Returns the CAPTCHA font in an appropriate size 
        ///</summary>
        private Font GetFont()
        {
            return this.Font;
        }

        ///<summary>
        ///Renders the CAPTCHA image
        ///</summary>
        private Bitmap GenerateImagePrivate()
        {
            Font fnt = null;
            Rectangle rect;
            Brush br;
            Bitmap bmp = new Bitmap(_width, _height, PixelFormat.Format32bppArgb);
            Graphics gr = Graphics.FromImage(bmp);
            gr.SmoothingMode = SmoothingMode.AntiAlias;

            //fill an empty white rectangle
            rect = new Rectangle(0, 0, _width, _height);
            br = new SolidBrush(Color.White);
            gr.FillRectangle(br, rect);

            int charOffset = 0;
            Double charWidth = _width / _randomTextLength;
            Rectangle rectChar;
            foreach (Char c in _randomText)
            {
                //establish font and draw area
                fnt = GetFont();
                rectChar = new Rectangle(Convert.ToInt32(charOffset * charWidth), 0, Convert.ToInt32(charWidth), _height);
                //warp the character
                GraphicsPath gp = TextPath(Convert.ToString(c), fnt, rectChar);
                WarpText(gp, rectChar);

                //draw the character
                br = new SolidBrush(Color.Black);
                gr.FillPath(br, gp);

                charOffset += 1;
            }
            AddNoise(gr, rect);
            AddLine(gr, rect);

            //clean up unmanaged resources
            fnt.Dispose();
            br.Dispose();
            gr.Dispose();
            return bmp;
        }

        ///<summary>
        ///Warp the provided text GraphicsPath by a variable amount
        ///</summary>
        private void WarpText(GraphicsPath textPath, Rectangle rect)
        {
            Single WarpDivisor = 0;
            Single RangeModifier = 0;
            switch (_fontWarp)
            {
                case FontWarpFactor.None:
                    break;
                case FontWarpFactor.Low:
                    WarpDivisor = 6F;
                    RangeModifier = 1F;
                    break;
                case FontWarpFactor.Medium:
                    WarpDivisor = 5F;
                    RangeModifier = 1.3F;
                    break;
                case FontWarpFactor.High:
                    WarpDivisor = 4.5F;
                    RangeModifier = 1.4F;
                    break;
                case FontWarpFactor.Extreme:
                    WarpDivisor = 4F;
                    RangeModifier = 1.5F;
                    break;
            }
            RectangleF rectF;
            rectF = new RectangleF(Convert.ToSingle(rect.Left), 0, Convert.ToSingle(rect.Width), rect.Height);
            int hrange = WarpDivisor == 0F ? Convert.ToInt32(WarpDivisor) : Convert.ToInt32(rect.Height / WarpDivisor);
            int wrange = WarpDivisor == 0F ? Convert.ToInt32(WarpDivisor) : Convert.ToInt32(rect.Width / WarpDivisor);
            int left = rect.Left - Convert.ToInt32(wrange * RangeModifier);
            int top = rect.Top - Convert.ToInt32(hrange * RangeModifier);
            int width = rect.Left + rect.Width + Convert.ToInt32(wrange * RangeModifier);
            int height = rect.Top + rect.Height + Convert.ToInt32(hrange * RangeModifier);
            if (left < 0) { left = 0; }
            if (top < 0) { top = 0; }
            if (width > this.Width) { width = this.Width; }
            if (height > this.Height) { height = this.Height; }
            int ymin = height - hrange;
            int ymax = top + hrange;
            PointF leftTop = RandomPoint(left, left + wrange, ref top, ref ymax);
            PointF rightTop = RandomPoint(width - wrange, width, ref top, ref ymax);
            PointF leftBottom = RandomPoint(left, left + wrange, ref ymin, ref height);
            PointF rightBottom = RandomPoint(width - wrange, width, ref ymin, ref height);

            PointF[] points = new PointF[] { leftTop, rightTop, leftBottom, rightBottom };
            Matrix m = new Matrix();
            m.Translate(0, 0);
            textPath.Warp(points, rectF, m, WarpMode.Perspective, 0);
        }

        ///<summary>
        ///Add a variable level of graphic noise to the image
        ///</summary>
        private void AddNoise(Graphics graphics1, Rectangle rect)
        {
            int density = 0;
            int size = 0;
            //const int fix = 100;
            switch (_backgroundNoise)
            {
                case BackgroundNoiseLevel.None:
                    return;
                case BackgroundNoiseLevel.Low:
                    density = 30;
                    //density = fix;
                    size = 40;
                    break;
                case BackgroundNoiseLevel.Medium:
                    density = 18;
                    //density = Convert.ToInt32(fix * 1.5);
                    size = 40;
                    break;
                case BackgroundNoiseLevel.High:
                    density = 16;
                    //density = Convert.ToInt32(fix * 2);
                    size = 39;
                    break;
                case BackgroundNoiseLevel.Extreme:
                    density = 12;
                    //density = Convert.ToInt32(fix * 3);
                    size = 38;
                    break;
            }
            SolidBrush br = new SolidBrush(Color.Black);
            int max = Convert.ToInt32(Math.Max(rect.Width, rect.Height) / size);
            for (int i = 0; i < Convert.ToInt32((rect.Width * rect.Height) / density) + 1; i++)
            //for (int i = 0; i < density; i++)
            {
                //graphics1.FillEllipse(br, _rand.Next(rect.Width), _rand.Next(rect.Height),_rand.Next(max), _rand.Next(max));
                graphics1.FillEllipse(br, _rand.Next(rect.Width), _rand.Next(rect.Height), 2, 2);
            }
            br.Dispose();
        }

        ///<summary>
        ///Add variable level of curved lines to the image
        ///</summary>
        private void AddLine(Graphics graphics1, Rectangle rect)
        {
            int length = 0;
            Single width = 0F;
            int linecount = 0;
            switch (_lineNoise)
            {
                case LineNoiseLevel.None:
                    return;
                case LineNoiseLevel.Low:
                    length = 4;
                    width = Convert.ToSingle(_height / 31.25); //1.6
                    break;
                case LineNoiseLevel.Medium:
                    length = 5;
                    width = Convert.ToSingle(_height / 27.7777); //1.8
                    break;
                case LineNoiseLevel.High:
                    length = 3;
                    width = Convert.ToSingle(_height / 25); //2.0
                    break;
                case LineNoiseLevel.Extreme:
                    length = 3;
                    width = Convert.ToSingle(_height / 22.7272); //2.2
                    break;
            }
            PointF[] pf = new PointF[length];
            Pen p = new Pen(Color.Black, width);
            for (int l = 1; l < linecount + 1; l++)
            {
                for (int i = 0; i < length + 1; i++)
                {
                    pf[i] = RandomPoint(rect);
                }
                graphics1.DrawCurve(p, pf, 1.75F);
            }
            p.Dispose();
        }

        #endregion
    }
}