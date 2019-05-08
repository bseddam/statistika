using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_News : System.Web.UI.Page
{
    DALC _db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }


        DataTable dt = _db.GetPages(Utils.PageType.Videos);
        rptNews.DataSource = dt;
        rptNews.DataBind();

        lblTitle.Text = DALC.GetStaticValue(Utils.PageType.Videos.ToParseStr().ToLower() + "_page_title");

        Page.Title = lblTitle.Text + " - " + Config.GetAppSetting("ProjectName");

        ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / {2} ", Config.getLang(Page),
     DALC.GetStaticValue("home_breadcrumb_title"), lblTitle.Text);
    }


    public string getIframeURL(string link)
    {
        return Config.getYoutubeIframeURL(link);
    }
}