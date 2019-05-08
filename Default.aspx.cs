using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    DALC _db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack)
        {
            return;
        }
        string lang = Page.RouteData.Values["lang"].ToParseStr();
        if (lang.Length == 0)
        {
            Config.Rd("/az/home");
        }
        lang = Config.getLang(Page);

        Page.Title = DALC.GetStaticValue("home_page_title") + " - " + Config.GetAppSetting("ProjectName");

        rptNews.DataSource = _db.GetPages(Utils.PageType.News, 5);
        rptNews.DataBind();

        DataTable dt = _db.GetPages(Utils.PageType.Videos, 1);

        if (dt.Rows.Count > 0)
        {
            VideoUrl.Text = dt.Rows[0]["title_" + Config.getLang(Page)].ToParseStr();
            VideoUrl.NavigateUrl = "/" + Config.getLang(Page) + "/video/" + dt.Rows[0]["id"].ToParseStr();

            string url = Config.getYoutubeIframeURL(dt.Rows[0]["video_url"].ToParseStr());
            ltrEmbed.Text = "<iframe class='embed-responsive-item' src='" + url + "' allowfullscreen></iframe>";
        }

        rptSlider.DataSource = _db.GetSliders(1, Page);
        rptSlider.DataBind();


        rptGoals.DataSource = _db.GetGoals();
        rptGoals.DataBind();

        home_goal_title.Text = DALC.GetStaticValue("home_goal_title");
        home_goal_subtitle.Text = DALC.GetStaticValue("home_goal_subtitle");
        home_slider_title.Text = DALC.GetStaticValue("home_slider_title");
    }
}