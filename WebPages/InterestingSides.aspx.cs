using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WebPages_InterestingSides : System.Web.UI.Page
{
    DALC _db = new DALC();
    int rowCount = Utils.rowCount;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblPageTitle.Text = DALC.GetStaticValue("interesting_sides_page_title");
        Page.Title = lblPageTitle.Text + " " + Config.GetAppSetting("ProjectName");

        if (IsPostBack)
        {
            return;
        }
        pnlShare.Visible = false;
        DataTable dt = _db.GetPages(Utils.PageType.TerefdaslarCategory);
        rptCategory.DataSource = dt;
        rptCategory.DataBind();

        //Page.Title = Config.GetAppSetting("ProjectName");
        ltrLeftBarTitle.Text = DALC.GetStaticValue("interesting_sides_left_bar_title");

        ltrLeftInfo.Text = DALC.GetStaticValue("interesting_sides_left_bar_info");


        string lang = Config.getLang(Page);
        ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / {2} ", lang,
            DALC.GetStaticValue("home_breadcrumb_title"),
            lblPageTitle.Text);

        if (getCategoryID != 0)
        {
            ltrBreadCrumb.Text = string.Format(@"
                                <a href='/{0}/home'> {1}</a> / 
                                <a href='/{0}/interesting-sides'> {2} </a> / 
                                {3}",
                                lang,
                                DALC.GetStaticValue("home_breadcrumb_title"),
                                lblPageTitle.Text,
                                _db.GetPagesByID(getCategoryID).Rows[0]["title_" + lang]);
        }



        if (getContentId != 0)
        {
            DataTable dtContent = _db.GetTerefdaslarById(getContentId);
            if (dtContent != null)
            {
                lblContent.Text = dtContent.Rows[0]["content_" + lang].ToParseStr();
                lblContentTitle.Text = dtContent.Rows[0]["title_" + lang].ToParseStr();
                imgContent.ImageUrl = string.Format("/uploads/pages/{0}", dtContent.Rows[0]["Filename"]);

                string cat_name = _db.GetPagesByID(dtContent.Rows[0]["page_id"].ToParseInt()).Rows[0]["title_" + lang].ToParseStr();

                ltrBreadCrumb.Text = string.Format(@"
                    <a href='/{0}/home'> {1}</a> /
                    <a href='/{0}/interesting-sides'>{2}</a> / 
                    <a href='/{0}/interesting-sides/{3}/{4}'>{5}</a> / 
                    {6}  ",
                  lang,
                  DALC.GetStaticValue("home_breadcrumb_title"),
                  Config.HtmlRemoval.StripTagsRegex(lblPageTitle.Text),
                  dtContent.Rows[0]["page_id"],
                  Config.Slug(cat_name),
                  cat_name,
                  Config.HtmlRemoval.StripTagsRegex(lblContentTitle.Text));

                string WebSiteURL = System.Configuration.ConfigurationManager.AppSettings["WebSiteURL"];

                shareBox(Config.HtmlRemoval.StripTagsRegex(lblContentTitle.Text),
                    "interesting sides",
                     lblContent.Text,
                     WebSiteURL + imgContent.ImageUrl);

                pnlShare.Visible = true;
            }
        }

        else if (getCategoryID == 0)
        {
            loadDataAll(0);
            loadPagerAll();
        }
        else
        {
            loadData(getCategoryID, 0);
            loadPager(getCategoryID);
        }



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
    public int getCategoryID
    {
        get
        {
            int id = Page.RouteData.Values["catId"].ToParseInt();
            return id;
        }
    }
    public int getContentId
    {
        get
        {
            int id = Page.RouteData.Values["contentId"].ToParseInt();
            return id;
        }
    }
    void loadData(int categoryID, int page)
    {
        rptPosts.DataSource = _db.GetTerefdaslarByCat(categoryID, page, rowCount);
        rptPosts.DataBind();

    }
    void loadPager(int categoryID)
    {
        rptPager.Visible = false;
        DataTable dtPager = new DataTable();
        dtPager.Columns.Add("name", typeof(string));
        dtPager.Columns.Add("num", typeof(int));
        dtPager.Columns.Add("cssClass", typeof(string));

        int totalCount = _db.GetTerefdaslarByCatCount(categoryID);

        if (rowCount <= totalCount)
        {
            double totalPageCount = Math.Ceiling(((double)totalCount / (double)rowCount));

            for (int i = 0; i < totalPageCount; i++)
            {
                DataRow dr = dtPager.NewRow();

                if (i == 0)
                {
                    dr["cssClass"] = "active";
                }

                dr["name"] = i + 1;
                dr["num"] = i;
                dtPager.Rows.Add(dr);

            }

            rptPager.DataSource = dtPager;
            rptPager.DataBind();
            rptPager.Visible = true;
        }
    }

    void loadDataAll(int page)
    {


        rptPosts.DataSource = _db.GetTerefdaslarAll(Utils.PageType.TerefdaslarCategory, page, rowCount);
        rptPosts.DataBind();



    }
    void loadPagerAll()
    {
        rptPager.Visible = false;
        DataTable dtPager = new DataTable();
        dtPager.Columns.Add("name", typeof(string));
        dtPager.Columns.Add("num", typeof(int));
        dtPager.Columns.Add("cssClass", typeof(string));

        int totalCount = _db.GetTerefdaslarAllCount(Utils.PageType.TerefdaslarCategory);

        if (rowCount <= totalCount)
        {
            double totalPageCount = Math.Ceiling(((double)totalCount / (double)rowCount));

            for (int i = 0; i < totalPageCount; i++)
            {
                DataRow dr = dtPager.NewRow();
                if (i == 0)
                {
                    dr["cssClass"] = "active";
                }
                dr["name"] = i + 1;
                dr["num"] = i;
                dtPager.Rows.Add(dr);

            }

            rptPager.DataSource = dtPager;
            rptPager.DataBind();
            rptPager.Visible = true;
        }
    }
    public string getContent(string text)
    {
        string newText = Config.HtmlRemoval.StripTagsRegex(text);
        return newText.Length > 300 ? (newText.Substring(0, 300) + "...") : newText;
    }
    protected void lnkPager_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (sender as LinkButton);
        int page = lnk.CommandArgument.ToParseInt();
        if (getCategoryID == 0)
        {
            loadDataAll(page);
        }
        else
        {
            loadData(getCategoryID, page);
        }
        for (int i = 0; i < rptPager.Items.Count; i++)
        {
            (rptPager.Items[i].FindControl("lnkPager") as LinkButton).CssClass = "";
        }

        lnk.CssClass = "active";
    }
}