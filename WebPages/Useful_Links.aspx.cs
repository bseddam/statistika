using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class WebPages_UsefulLinks : System.Web.UI.Page
{
    DALC _db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {
        string lang = Config.getLang(Page);
        string pageTitle = DALC.GetStaticValue("usefullinks_page_title");
        Page.Title = pageTitle + " - " + Config.GetAppSetting("ProjectName");

        if (IsPostBack)
        {
            return;
        }

        ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / {2} ",
            lang,
            DALC.GetStaticValue("home_breadcrumb_title"),
            pageTitle);

    }

}