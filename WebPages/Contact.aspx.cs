using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_Contact : System.Web.UI.Page
{
    DALC _db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }
        string lang = Config.getLang(Page);

        string pageName = DALC.GetStaticValue("contact_page_title");

        Page.Title = pageName + " - " + Config.GetAppSetting("ProjectName");




        ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / {2} ",
            lang,
            DALC.GetStaticValue("home_breadcrumb_title"),
            pageName);



        txtEmail.Attributes["placeholder"] = DALC.GetStaticValue("contact_email_placeholder");
        txtContent.Attributes["placeholder"] = DALC.GetStaticValue("contact_content_placeholder");
        txtSubject.Attributes["placeholder"] = DALC.GetStaticValue("contact_subject_placeholder");

        btnSend.Text = DALC.GetStaticValue("contact_send_button");

        ltrSendLetter.Text = DALC.GetStaticValue("contact_section1_title");
        ltrMapMarker.Text = DALC.GetStaticValue("contact_section2_title");
        CheckEmail.ErrorMessage = DALC.GetStaticValue("contact_incorrect_email");
        lblMessage.Text = "";

        LocationText.Text = DALC.GetStaticValue("footer_contact_content");
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        lblMessage.Text = "";
        lblMessage.ForeColor = System.Drawing.Color.Red;

        if (txtContent.Text.Trim().Length<3 || txtEmail.Text.Trim().Length<3 || txtSubject.Text.Trim().Length<3)
        {
            lblMessage.Text = DALC.GetStaticValue("contact_fill_inputs_message");
            return;
        }



        string toEmail = System.Configuration.ConfigurationManager.AppSettings["FeedBackMail"];

        int v = Config.SendMail(toEmail, txtSubject.Text.Trim(), txtContent.Text.Trim());
        if (v == 1)
        {
            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = DALC.GetStaticValue("contact_send_success_message");
        }
        else
        {
            lblMessage.Text = DALC.GetStaticValue("contact_send_error_message");
        }
    }
}