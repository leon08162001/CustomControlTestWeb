using System;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace APTemplate
{
    public class BarCodeImageHandler : IHttpHandler
    {
        //Rectangle rect;
        Graphics gr;
        Bitmap BarCodeImg;
        string Id;
        string BarCode = "";
        int ImageFormat = 0;
        string BarCodeType = "";
        int width = 0;
        Single scale;
        int height = 0;
        int weight = 1;
        IBarcode ibarcode;
        Bitmap bmp;
        #region IHttpHandler 成員

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpApplication app = context.ApplicationInstance;
            Id = app.Request.QueryString["id"];
            if (Id == null)
            {
                BarCode = app.Request.QueryString["BarCode"];
                ImageFormat = Convert.ToInt16(app.Request.QueryString["ImageFormat"]);
                BarCodeType = app.Request.QueryString["type"];
                scale = Convert.ToSingle(app.Request.QueryString["scale"]) * (Single)0.1;
                height = Convert.ToInt16(app.Request.QueryString["height"]);
                weight = Convert.ToInt16(app.Request.QueryString["weight"]);
            }
            else
            {
                BarCode = app.Context.Cache[Id + "_BarCode"].ToString();
                ImageFormat = Convert.ToInt16(app.Context.Cache[Id + "_ImageFormat"]);
                BarCodeType = app.Context.Cache[Id + "_type"].ToString();
                scale = Convert.ToSingle(app.Context.Cache[Id + "_scale"]) * (Single)0.1;
                height = Convert.ToInt16(app.Context.Cache[Id + "_height"]);
                weight = Convert.ToInt16(app.Context.Cache[Id + "_weight"]);
                app.Context.Cache.Remove(Id + "_BarCode");
                app.Context.Cache.Remove(Id + "_ImageFormat");
                app.Context.Cache.Remove(Id + "_type");
                app.Context.Cache.Remove(Id + "_scale");
                app.Context.Cache.Remove(Id + "_height");
                app.Context.Cache.Remove(Id + "_weight");
            }
            GenerateBarCodeImage(BarCodeType);
            switch (ImageFormat)
            {
                case 0:
                    BarCodeImg.Save(app.Context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    app.Response.ContentType = "image/jpeg";
                    break;
                case 1:
                    BarCodeImg.Save(app.Context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
                    app.Response.ContentType = "image/png";
                    break;
                case 2:
                    BarCodeImg.Save(app.Context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
                    app.Response.ContentType = "image/gif";
                    break;
                case 3:
                    BarCodeImg.Save(app.Context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Bmp);
                    app.Response.ContentType = "image/bmp";
                    break;
                default:
                    BarCodeImg.Save(app.Context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    app.Response.ContentType = "image/jpeg";
                    break;
            }
            BarCodeImg.Dispose();
            app.Response.StatusCode = 200;
            context.ApplicationInstance.CompleteRequest();
        }

        private Bitmap GenerateCodabar(string code)
        {
            ibarcode = new Codabar(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateCode11(string code)
        {
            ibarcode = new Code11(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateCode39(string code)
        {
            ibarcode = new Code39(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateCode93(string code)
        {
            ibarcode = new Code93(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateEAN8(string code)
        {
            ibarcode = new EAN8(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateEAN13(string code)
        {
            ibarcode = new EAN13(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateFIM(string code)
        {
            ibarcode = new FIM(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateInterleaved2of5(string code)
        {
            ibarcode = new Interleaved2of5(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateCode128(string code)
        {
            ibarcode = new Code128(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateCode128_A(string code)
        {
            ibarcode = new Code128(code, Code128.TYPES.A);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateCode128_B(string code)
        {
            ibarcode = new Code128(code, Code128.TYPES.B);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateCode128_C(string code)
        {
            ibarcode = new Code128(code, Code128.TYPES.C);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateISBN(string code)
        {
            ibarcode = new ISBN(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateITF14(string code)
        {
            ibarcode = new ITF14(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateJAN13(string code)
        {
            ibarcode = new JAN13(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateMSI(string code)
        {
            ibarcode = new MSI(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GeneratePharmacode(string code)
        {
            ibarcode = new Pharmacode(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GeneratePostnet(string code)
        {
            ibarcode = new Postnet(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateStandard2of5(string code)
        {
            ibarcode = new Standard2of5(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateTelepen(string code)
        {
            ibarcode = new Telepen(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateUPC_A(string code)
        {
            ibarcode = new UPCA(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateUPC_E(string code)
        {
            ibarcode = new UPCE(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateUPCSupplement2(string code)
        {
            ibarcode = new UPCSupplement2(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateUPCSupplement5(string code)
        {
            ibarcode = new UPCSupplement5(code);
            return GenerateImage(ibarcode);
        }

        private Bitmap GenerateImage(IBarcode ibarcode)
        {
            string encodedString = "";
            encodedString = ibarcode.Encoded_Value;
            int encodedStringLength = encodedString.Length;
            double wideToNarrowRatio = 1;
            width = 0;
            for (int i = 0; i < encodedStringLength; i++)
            {
                if (encodedString[i] == '1')
                    width += (int)(wideToNarrowRatio * (int)weight);
                else
                    width += (int)weight;
            }
            width = Convert.ToInt32(width * scale);
            height = Convert.ToInt32(height * scale);
            bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            gr = Graphics.FromImage(bmp);
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //Calculates the scale
            Single wid = Convert.ToSingle(width) / Convert.ToSingle(encodedStringLength);
            float x = 0;
            float yTop = 0;

            for (int i = 0; i < encodedStringLength; i++)
            {
                string CurrentCode = encodedString.Substring(i, 1);
                gr.FillRectangle(CurrentCode == "1" ? Brushes.Black : Brushes.White, x, yTop, wid, height);
                x += wid;
            }
            return bmp;
        }

        private void GenerateBarCodeImage(string BarCodeType)
        {
            switch (BarCodeType.ToLower())
            {
                case "codabar":
                    BarCodeImg = GenerateCodabar(BarCode);
                    break;
                case "code11":
                    BarCodeImg = GenerateCode11(BarCode);
                    break;
                case "code128":
                    BarCodeImg = GenerateCode128(BarCode);
                    break;
                case "code128_a":
                    BarCodeImg = GenerateCode128_A(BarCode);
                    break;
                case "code128_b":
                    BarCodeImg = GenerateCode128_B(BarCode);
                    break;
                case "code128_c":
                    BarCodeImg = GenerateCode128_C(BarCode);
                    break;
                case "code39":
                    BarCodeImg = GenerateCode39(BarCode);
                    break;
                case "code93":
                    BarCodeImg = GenerateCode93(BarCode);
                    break;
                case "ean8":
                    BarCodeImg = GenerateEAN8(BarCode);
                    break;
                case "ean13":
                    BarCodeImg = GenerateEAN13(BarCode);
                    break;
                case "fim":
                    BarCodeImg = GenerateFIM(BarCode);
                    break;
                case "interleaved2of5":
                    BarCodeImg = GenerateInterleaved2of5(BarCode);
                    break;
                case "isbn":
                    BarCodeImg = GenerateISBN(BarCode);
                    break;
                case "itf14":
                    BarCodeImg = GenerateITF14(BarCode);
                    break;
                case "jan13":
                    BarCodeImg = GenerateJAN13(BarCode);
                    break;
                case "msi":
                    BarCodeImg = GenerateMSI(BarCode);
                    break;
                case "pharmacode":
                    BarCodeImg = GeneratePharmacode(BarCode);
                    break;
                case "postnet":
                    BarCodeImg = GeneratePostnet(BarCode);
                    break;
                case "standard2of5":
                    BarCodeImg = GenerateStandard2of5(BarCode);
                    break;
                case "telepen":
                    BarCodeImg = GenerateTelepen(BarCode);
                    break;
                case "upc_a":
                    BarCodeImg = GenerateUPC_A(BarCode);
                    break;
                case "upc_e":
                    BarCodeImg = GenerateUPC_E(BarCode);
                    break;
                case "upcsupplement2":
                    BarCodeImg = GenerateUPCSupplement2(BarCode);
                    break;
                case "upcsupplement5":
                    BarCodeImg = GenerateUPCSupplement5(BarCode);
                    break;
                default:

                    break;
            }
        }

        #endregion
    }
}