using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WebPages_NationalPriority : System.Web.UI.Page
{
    DALC _db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }
        //pageid
        int goalId = Page.RouteData.Values["goalId"].ToParseInt();



        string lang = Config.getLang(Page);
        ltrTitle.Text = DALC.GetStaticValue("national_priority_page_title");
        Page.Title = Config.HtmlRemoval.StripTagsRegex(ltrTitle.Text) + " - " + Config.GetAppSetting("ProjectName");

        ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / {2} ", lang,
                    DALC.GetStaticValue("home_breadcrumb_title"), ltrTitle.Text);

        DataTable dtGoals = _db.GetGoals();

        rptGoals.DataSource = dtGoals;
        rptGoals.DataBind();

        DataView dv = dtGoals.DefaultView;
        dv.RowFilter = "id=" + goalId;

        if (dv.Count > 0)
        {
            lblGoalName.Text = DALC.GetStaticValue("goal_value") + " " + dv[0]["priority"] + " . " + dv[0]["name_" + lang];
            imgGoal.ImageUrl = "/images/goals-" + Config.getLang(Page) + "-white/" + dv[0]["id"] + ".png";
            lblGoalContent.Text = dv[0]["priority_desc_" + lang].ToParseStr();
        }


        ltrtab1.Text = DALC.GetStaticValue("national_priority_tab1_title");
        ltrtab2.Text = DALC.GetStaticValue("national_priority_tab2_title");


        rptTargets.DataSource = _db.GetTargets_1(goalId, true);
        rptTargets.DataBind();

        rptIndicators.DataSource = _db.GetIndicatorsForNationaPriority(goalId);
        rptIndicators.DataBind();

        shareBox(Config.HtmlRemoval.StripTagsRegex(ltrTitle.Text),
                   "national priority",
                   ltrContent.Text,
                   "");
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