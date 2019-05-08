using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WebPages_Constitution : System.Web.UI.Page
{
    DALC _db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }


        DataTable dt = _db.GetPagesOrderBy(Utils.PageType.Konstitusiya);


        rptPosts.DataSource = dt;
        rptPosts.DataBind();

        string lang = Config.getLang(Page);

        ltrPageTitle.Text = DALC.GetStaticValue("constitution_page_title");
        Page.Title = ltrPageTitle.Text + " " + Config.GetAppSetting("ProjectName");
        ltrLeftBarTitle.Text = DALC.GetStaticValue("constitution_left_bar_title");

        int pageId = Page.RouteData.Values["id"].ToParseInt();
        if (pageId == 0)
        {
            DataTable dtPage = _db.GetPage(Utils.PageType.KonstitusiyaDefaultPage, Config.getLang(Page));

            loadPage(dtPage);

            ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / {2} ", lang,
                                    DALC.GetStaticValue("home_breadcrumb_title"),
                                    Config.HtmlRemoval.StripTagsRegex(ltrPageTitle.Text));


            shareBox(Config.HtmlRemoval.StripTagsRegex(ltrPageTitle.Text),
                     "Constitution",
                     dtPage.Rows[0]["content"].ToParseStr(),
                     "");
        }
        else
        {
            DataTable dtPage = _db.GetPage(pageId, Config.getLang(Page));

            loadPage(dtPage);

            ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / <a href=\"/{0}/constitution\"> {2}</a> / {3} ", lang,
                                  DALC.GetStaticValue("home_breadcrumb_title"),
                                  Config.HtmlRemoval.StripTagsRegex(ltrPageTitle.Text),
                                  Config.HtmlRemoval.StripTagsRegex(ltrTitle.Text));

            shareBox(Config.HtmlRemoval.StripTagsRegex(ltrTitle.Text),
                    "Constitution",
                    dtPage.Rows[0]["content"].ToParseStr(),
                    "");
        }

       
      
    }

    private void loadPage(DataTable dtPage)
    {
        if (dtPage.Rows.Count < 1)
        {
            Config.Rd("/error");
        }

        ltrTitle.Text = dtPage.Rows[0]["title"].ToParseStr();
        ltrContent.Text = dtPage.Rows[0]["content"].ToParseStr();
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