using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_Unsubscribe : System.Web.UI.Page
{
    DALC _db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = Config.GetAppSetting("ProjectName");
        if (IsPostBack)
        {
            return;
        }
        int id=Cryptography.Decrypt(Request.QueryString["token"]).ToParseInt();
        if (id == -1)
        {
            Config.Rd("/error?wrong id");
            return;
        }
      


        string lang = Config.getLang(Page);

       
        ltrContent.Text = DALC.GetStaticValue("unsubscribe_content");
        ltrTitle.Text = DALC.GetStaticValue("unsubscribe_breadcrumb");


        ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / {2} ",
            lang,
            DALC.GetStaticValue("home_breadcrumb_title"),
            Config.HtmlRemoval.StripTagsRegex(ltrTitle.Text));

        int count = _db.CheckSubscribe(id);
        if (count == 0)
        {
            BtnConfirm.Visible = false;
            lblSuccess.Text = DALC.GetStaticValue("unsubscribe_success_text2");
        }

        BtnConfirm.Text = DALC.GetStaticValue("unsubscribe_btn_confirm");

    }
    protected void BtnConfirm_Click(object sender, EventArgs e)
    {
        int id = Cryptography.Decrypt(Request.QueryString["token"]).ToParseInt();
        if (id == -1)
        {
            Config.Rd("/error?wrong id");
            return;
        }

    
        Utils.MethodType val = _db.SubscribeUpdate(id);
        if (val == Utils.MethodType.Error)
        {
            lblError.Text = DALC.GetStaticValue("unsubscribe_error_text");
        }
        else
        {
            lblSuccess.Text = DALC.GetStaticValue("unsubscribe_success_text");
            BtnConfirm.Visible = false;
        }
    }
}