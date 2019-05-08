using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
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
        //pageid
        int pageid = Page.RouteData.Values["pageid"].ToParseInt();

        string lang = Config.getLang(Page);

        DataTable dt = _db.GetPage(pageid, lang);
        if (dt.Rows.Count < 1)
        {
            Config.Rd("/error?3");
        }

        ltrTitle.Text = dt.Rows[0]["title"].ToParseStr();
        ltrContent.Text = dt.Rows[0]["content"].ToParseStr();
        ltrDate.Text = dt.Rows[0]["pageDt"].ToParseStr();
        if (dt.Rows[0]["type_id"].ToParseInt() == (int)Utils.PageType.Videos)
        {
            string videoId = dt.Rows[0]["video_url"].ToParseStr().Split(new string[] { "v=" }, StringSplitOptions.None)[1];
            string url = "https://www.youtube.com/embed/" + videoId;
            ltrVideo.Text = " <div class='embed-responsive embed-responsive-16by9'><iframe class='embed-responsive-item' src='" + url + "' allowfullscreen></iframe> </div>";
        }


        Page.Title = Config.HtmlRemoval.StripTagsRegex(ltrTitle.Text) + " - " + Config.GetAppSetting("ProjectName");

        PagesImagesTitle.Text = DALC.GetStaticValue("pages_images_title");


        DataTable dtImages = _db.GetPagesContent(pageid);
        rptImages.DataSource = dtImages;
        rptImages.DataBind();

        if (rptImages.Items.Count == 0)
        {
            pnlImages.Visible = false;
        }
        if (getPageType == Utils.PageType.Order)
        {
            pnlImages.Visible = false;
        }
        rptNews.DataSource = _db.GetPages(getPageType, 5);
        rptNews.DataBind();


        ltrBreadCrumb.Text = string.Format("<a href='/{0}/home'> {1}</a> / <a href='/{0}/news/list'>{2}</a> / {3}  ",
                 lang,
                 DALC.GetStaticValue("home_breadcrumb_title"),
                 DALC.GetStaticValue(getPageType.ToParseStr().ToLower() + "_page_title"),
                 Config.HtmlRemoval.StripTagsRegex(ltrTitle.Text));

        string WebSiteURL = System.Configuration.ConfigurationManager.AppSettings["WebSiteURL"];

        shareBox(ltrTitle.Text,
                "news",
                ltrContent.Text,
                dtImages.Rows.Count > 0 ? (WebSiteURL + "/uploads/pages/" + dtImages.Rows[0]["name"].ToParseStr()) : "");

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
    Utils.PageType getPageType
    {

        get
        {
            Utils.PageType _type = Utils.PageType.News;

            switch (Page.RouteData.Values["pageType"].ToParseStr())
            {
                case "news": _type = Utils.PageType.News; break;
                case "videos": _type = Utils.PageType.Videos; break;
                case "law": _type = Utils.PageType.Laws; break;
                case "order": _type = Utils.PageType.Order; break;

            }
            return _type;
        }
    }
}