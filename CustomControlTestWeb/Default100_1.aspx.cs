﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using APTemplate;

public partial class Default100 : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    SetCaptcha();
  }
  protected void TabsView1_TabSelectionChanging(object sender, APTemplate.TabSelectionChangingEventArgs e)
  {
    int newindex = e.NewIndex;
    int previousindex = e.PreviousIndex;
    if (newindex == 0)
    {

    }
    else if (newindex == 1)
    {
      //Captcha1.Visible = false;
      //Captcha2.Visible = false;
    }
    else if (newindex == 2)
    {
      //Captcha1.Visible = false;
      //Captcha2.Visible = false;
    }
  }

  protected void ToolBar_Click(Object sender, EventArgs e)
  {
    //ToolButton ToolButton = (ToolButton)sender;
    //string ClickArgument = ToolButton.ClickArgument;
    //string ImageUrl = ToolButton.ImageUrl;
    //string ToolTip = ToolButton.ToolTip;
    //string id = Identity1.Text;
    //long Num1 = Number1.Text;
    //DateTime Calendar1=PopupCalendar1.Text;
    //decimal Decimal1 = DecimalRange1.FirstText;
    //decimal Decimal2 = DecimalRange1.SecondText;
    //string mail1 = Email1.Text;
    //string a = "a";
    //string b = "b";
  
    ToolBarButton ToolButton = (ToolBarButton)sender;
    string MediaPath = "http://localhost:90/Media/";
    string MediaFile = ToolButton.ClickArgument;
    //MediaPlayer1.Url = MediaPath + MediaFile;
    MediaPlayer2.Url = MediaPath + MediaFile;
  }
  protected void Button3_Click(object sender, EventArgs e)
  {
    ViewState["CaptchaVisible"] = false;
    SetCaptcha();
    //Button3.Attributes["onclick"] = "";
  }

  private void SetCaptcha()
  {
    if (ViewState["CaptchaVisible"] != null)
    {
      //Captcha1.Visible = (bool)ViewState["CaptchaVisible"];
      //Captcha2.Visible = (bool)ViewState["CaptchaVisible"];
    }
    else
    {
      //Captcha1.Visible = true;
      //Captcha2.Visible = true;
    }
  }
  protected void ToolBar2_Button_ConfirmYesNoClick(object sender, EventArgs e)
  {

  }
}
