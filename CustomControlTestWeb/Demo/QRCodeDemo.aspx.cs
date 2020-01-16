using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Demo_QRCodeDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        QRCode1.QRCodeText = @"行政院長江宜樺18日7度赴立法院，民進黨團昨已定調會讓他上台，" + Environment.NewLine +
                            "上午國是論壇結束後，原定應繼續進行議程，但立法院長王金平於10點宣布國民黨團要求協商，" + Environment.NewLine +
                            "院會休息，江宜樺在宣布休息後暫時離開座位，至後方休息室等待。院會在短暫休息協商後，" + Environment.NewLine +
                            "10點30分續開會宣讀報告事項，報告事項於11點50分宣讀完畢，" + Environment.NewLine +
                            "主席王金平隨即宣布邀請行政院長上台報告，江宜樺終於在上午院會休息前站上發言台，順利進行施政報告。" + Environment.NewLine +
                            "主席王金平邀請的話語一出，現場國民黨立委立刻拍手歡迎，在場的台聯立委雖未杯葛上台，" + Environment.NewLine +
                            "但仍高舉「台電漲價無理」、「監聽國會無法無天」、「服貿協議＝黑箱協議」" + Environment.NewLine +
                            "行政院長江宜樺18日7度赴立法院，民進黨團昨已定調會讓他上台，" + Environment.NewLine +
                            "上午國是論壇結束後，原定應繼續進行議程，但立法院長王金平於10點宣布國民黨團要求協商，" + Environment.NewLine +
                            "院會休息，江宜樺在宣布休息後暫時離開座位，至後方休息室等待。院會在短暫休息協商後，" + Environment.NewLine +
                            "等抗議牌子試圖干擾報告進行，國民黨立委立即上前勸阻，江宜樺則一派鎮定，不受影響繼續朗讀報告。";

        //QRCode1.QRCodeText = "姓名: 李乃興" + Environment.NewLine +
        //                    "Mobile: 0924-117-575" + Environment.NewLine +
        //                    "WebSite: www.eprint.com.tw" + Environment.NewLine +
        //                    "Home: (02)2924-2117" + Environment.NewLine +
        //                    "Email: leonlee@wistronits.com.tw";

    }
}