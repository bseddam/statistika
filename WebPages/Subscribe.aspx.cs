using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_Subscribe : System.Web.UI.Page
{
    DALC _db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }
        string lang = Config.getLang(Page);

        string pageName = DALC.GetStaticValue("subscribe_page_title");

        Page.Title = pageName + " - " + Config.GetAppSetting("ProjectName");

        ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / {2} ",
            lang,
            DALC.GetStaticValue("home_breadcrumb_title"),
            pageName);



        txtEmail.Attributes["placeholder"] = DALC.GetStaticValue("subscribe_email");
        txtAtaadi.Attributes["placeholder"] = DALC.GetStaticValue("subscribe_atadi");
        txtname.Attributes["placeholder"] = DALC.GetStaticValue("subscribe_name");
        txtsurname.Attributes["placeholder"] = DALC.GetStaticValue("subscribe_surname");

        btnSend.Text = DALC.GetStaticValue("subscribe_send");

        ltrSendLetter.Text = DALC.GetStaticValue("subscribe_title");
        CheckEmail.ErrorMessage = DALC.GetStaticValue("subscribe_incorrect_email");
        lblMessage.Text = "";

        LocationText.Text = DALC.GetStaticValue("subscribe_side_text");
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        lblMessage.ForeColor = System.Drawing.Color.Red;

        if (txtname.Text.Trim().Length < 3 || txtsurname.Text.Trim().Length < 3 || txtEmail.Text.Trim().Length < 3)
        {
            lblMessage.Text = DALC.GetStaticValue("subscribe_fill_inputs_message");
            return;
        }




        Utils.MethodType v = _db.SubscribeInsert(txtname.Text, txtsurname.Text, txtAtaadi.Text, txtEmail.Text);
        if (v == Utils.MethodType.Succes)
        {
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = DALC.GetStaticValue("subscribe_send_success_message");
        }
        else
        {
            lblMessage.Text = DALC.GetStaticValue("subscribe_send_error_message");
        }
    }
}