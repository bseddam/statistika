using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WebPages_NationalMechamism : System.Web.UI.Page
{
    public const int MechanismGundelikKod = 10;
    DALC _db = new DALC();

    public int getPageId
    {
        get
        {
            return Page.RouteData.Values["id"].ToParseInt();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }
        int pageid = Page.RouteData.Values["id"].ToParseInt();


        string lang = Config.getLang(Page);

        #region LeftMenu

        DataTable dt = _db.GetPagesOrderBy(Utils.PageType.MechanismOtherPages);
        rptPosts.DataSource = dt;
        rptPosts.DataBind();

        #endregion


        Page.Title = Config.GetAppSetting("ProjectName");
        ltrLeftBarTitle.Text = DALC.GetStaticValue("national_mechanism_left_bar_title");
        ltrGundelik.Text = Literal1.Text = DALC.GetStaticValue("national_mechanism_progress_text");

        //rptPosts1.DataSource = dt;
        //rptPosts1.DataBind();




        PnlGundelik.Visible = false;
        PnlShareBox.Visible = false;
        if (pageid == 0)
        {
            DataTable dtM = _db.GetPage(Utils.PageType.MechanismDefaultPage, lang);

            loadPage(dtM);

            ltrBreadCrumb.Text = string.Format("<a href='/{0}/home'> {1}</a> / {2}  ",
               lang,
               DALC.GetStaticValue("home_breadcrumb_title"),
               Config.HtmlRemoval.StripTagsRegex(dtM.Rows[0]["title"].ToParseStr()));
        }
        else if (pageid == MechanismGundelikKod)
        {

            rptContent.DataSource = _db.GetPagesOrderBy(Utils.PageType.MechanismGundelik);
            rptContent.DataBind();
            PnlGundelik.Visible = true;
            loadBreadCrumb(lang, ltrGundelik.Text);
        }
        else
        {
            DataTable dtM = _db.GetPage(pageid, lang);
            loadPage(dtM);
            loadBreadCrumb(lang, dtM.Rows[0]["title"].ToParseStr());
        }



      

    }

    private void loadBreadCrumb(string lang,string category)
    {
        DataTable dtTitle = _db.GetPage(Utils.PageType.MechanismDefaultPage, lang);

        ltrBreadCrumb.Text = string.Format(@"
                    <a href='/{0}/home'> {1}</a> /
                    <a href='/{0}/national-implementation-mechanism'> {2}</a> /
                     {3}  ",
                 lang,
                 DALC.GetStaticValue("home_breadcrumb_title"),
                 Config.HtmlRemoval.StripTagsRegex(dtTitle.Rows[0]["title"].ToParseStr()),
                  Config.HtmlRemoval.StripTagsRegex(category));
    }

    
    private void loadPage(DataTable dt)
    {
        if (dt.Rows.Count < 1)
        {
            Config.Rd("/error?4");
        }
        rptPosts1.DataSource = dt;
        rptPosts1.DataBind();
        PnlShareBox.Visible = true;
        shareBox(Config.HtmlRemoval.StripTagsRegex(ltrLeftBarTitle.Text),
          "national-implementation-mechanism",
          dt.Rows[0]["content"].ToParseStr(),
          "");
        //lblPageTitle.Text = dtM.Rows[0]["title"].ToParseStr();
        //lblPageContent.Text = dtM.Rows[0]["content"].ToParseStr();


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