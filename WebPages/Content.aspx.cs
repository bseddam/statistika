using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WebPages_Content : System.Web.UI.Page
{
    DALC _db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }
        string lang = Config.getLang(Page);
        int goalId = Page.RouteData.Values["goalId"].ToParseInt();

        DataTable dt = _db.GetSlidersByGoal(goalId, Page);
        if (dt.Rows.Count > 0)
        {
            lblGoalName.Text = DALC.GetStaticValue("goal_value") + " " + dt.Rows[0]["priority"] + " . " + dt.Rows[0]["GoalName"];
            Page.Title = lblGoalName.Text + " - " + Config.GetAppSetting("ProjectName");
            imgGoal.ImageUrl = "/images/goals-" + lang + "-white/" + dt.Rows[0]["goal_id"] + ".png";
        }

        rptSlider.DataSource = dt;
        rptSlider.DataBind();

        DataTable dtGoad = _db.GetSlidersGoalList(goalId, lang);
        rptGoals.DataSource = dtGoad;
        rptGoals.DataBind();

        lblPageTitle.Text = DALC.GetStaticValue("slider_content_title");

        ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / {2} ", lang,
            DALC.GetStaticValue("home_breadcrumb_title"), lblPageTitle.Text);

        string url = System.Configuration.ConfigurationManager.AppSettings["WebSiteURL"];

        shareBox(lblGoalName.Text,
            "news",
            dt.Rows[0]["content_" + lang].ToParseStr(),
            url + "/uploads/slider/" + dtGoad.Rows[0]["image_url"].ToParseStr());
    }

    private void shareBox(string pageTitle, string PageType, string Description, string image_url)
    {
        Description = HttpUtility.HtmlDecode(Config.HtmlRemoval.StripTagsRegex(Description));

        string pageUrl = Request.Url.ToString();
        share_text.Text = DALC.GetStaticValue("share_text");
        shareFb.NavigateUrl = "http://www.facebook.com/sharer.php?u=" + pageUrl;
        shareTwt.NavigateUrl = "https://twitter.com/share?url=" + pageUrl;
        shareMail.NavigateUrl = string.Format("mailto:?subject={0}&body={1}", pageTitle, pageUrl);
        shareLinkedin.NavigateUrl = string.Format("http://www.linkedin.com/shareArticle?mini=true&url={0}&title={1}&summary={2}&source={0}", pageUrl, pageTitle, Description);


        #region Google

        //Header.Controls.Add(new HtmlMeta
        //{
        //    Name = "keywords",
        //    Content = ""
        //});

        Header.Controls.Add(new HtmlMeta
        {
            Name = "description",
            Content = Description
        });

        #endregion

        #region Facebook

        Header.Controls.Add(new HtmlMeta
        {
            Name = "og:title",
            Content = pageTitle
        });

        Header.Controls.Add(new HtmlMeta
        {
            Name = "og:type",
            Content = PageType
        });

        Header.Controls.Add(new HtmlMeta
        {
            Name = "og:image",
            Content = image_url
        });
        Header.Controls.Add(new HtmlMeta
        {
            Name = "og:description",
            Content = Description
        });



        #endregion

        #region Twitter

        Header.Controls.Add(new HtmlMeta
        {
            Name = "twitter:card",
            Content = "summary"
        });

        Header.Controls.Add(new HtmlMeta
        {
            Name = "twitter:title",
            Content = pageTitle
        });

        Header.Controls.Add(new HtmlMeta
        {
            Name = "twitter:description",
            Content = Description
        });
        Header.Controls.Add(new HtmlMeta
        {
            Name = "twitter:image",
            Content = image_url
        });



        #endregion

    }
}