using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_Page : System.Web.UI.Page
{
    DALC _db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }
        //pageid
        int pageid = Page.RouteData.Values["videoid"].ToParseInt();
        string lang = Config.getLang(Page);
        DataTable dt = _db.GetPage(pageid, lang);
        if (dt.Rows.Count < 1)
        {
            Config.Rd("/error?1");
        }

        ltrTitle.Text = dt.Rows[0]["title"].ToParseStr();
        ltrContent.Text = dt.Rows[0]["content"].ToParseStr();
        ltrDate.Text = dt.Rows[0]["pageDt"].ToParseStr();

        string url = getIframeURL(dt.Rows[0]["video_url"].ToParseStr());
        ltrVideo.Text = " <div class='embed-responsive embed-responsive-16by9'><iframe class='embed-responsive-item' src='" + url + "' allowfullscreen></iframe> </div>";

        Page.Title = ltrTitle.Text + " - " + Config.GetAppSetting("ProjectName");

        lblRealtedTitle.Text = DALC.GetStaticValue("related_video_title");


        rptVideos.DataSource = _db.GetPagesOther(Utils.PageType.Videos, pageid);
        rptVideos.DataBind();



        ltrBreadCrumb.Text = string.Format("<a href='/{0}/home'> {1}</a> / <a href='/{0}/videos/list'>{2}</a> / {3}  ",
                 lang,
                 DALC.GetStaticValue("home_breadcrumb_title"),
                 DALC.GetStaticValue("videos_page_title"),
                 Config.HtmlRemoval.StripTagsRegex(ltrTitle.Text));

    }

    public string getIframeURL(string link)
    {
        return Config.getYoutubeIframeURL(link);
    }
}