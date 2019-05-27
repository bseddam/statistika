using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WebPages_NationalReports : System.Web.UI.Page
{
    DALC _db = new DALC();
    int rowCount = Utils.rowCount;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }

        string lang = Config.getLang(Page);
        pnlContent.Visible = pnlShortMaterial.Visible = pnlTamMaterial.Visible = false;
        lblPageTitle.Text = DALC.GetStaticValue("national_reports_page_title");
        Page.Title = lblPageTitle.Text + " " + Config.GetAppSetting("ProjectName");

        ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / {2} ", lang,
            DALC.GetStaticValue("home_breadcrumb_title"), lblPageTitle.Text);

        if (getContentId != 0)
        {
            DataTable dtContent = _db.GetNationalReportsById(getContentId);
            if (dtContent != null)
            {


                pnlContent.Visible = true;

                lblDate.Text = dtContent.Rows[0]["pageDt"].ToParseStr();
                lblContent.Text = dtContent.Rows[0]["content_" + lang].ToParseStr();
                lblContentTitle.Text = dtContent.Rows[0]["title_" + lang].ToParseStr();
                imgContent.ImageUrl = string.Format("/uploads/pages/{0}", dtContent.Rows[0]["image_filename_" + lang]);

                linkDownloadReport.Text = DALC.GetStaticValue("download_report");

                linkTamMaterial.NavigateUrl = string.Format("/uploads/pages/{0}", dtContent.Rows[0]["full_material_"+lang]);
                linkTamMaterial.Text = DALC.GetStaticValue("full_material");
                linkShortMaterial.NavigateUrl = string.Format("/uploads/pages/{0}", dtContent.Rows[0]["short_material_" + lang]);
                linkShortMaterial.Text = DALC.GetStaticValue("short_material");

                ltrBreadCrumb.Text = string.Format("<a href='/{0}/home'> {1}</a> / <a href='/{0}/national-reports'>{2}</a> / {3}  ",
                  lang,
                  DALC.GetStaticValue("home_breadcrumb_title"),
                  Config.HtmlRemoval.StripTagsRegex(lblPageTitle.Text),
                   Config.HtmlRemoval.StripTagsRegex(lblContentTitle.Text));


                string WebSiteURL = System.Configuration.ConfigurationManager.AppSettings["WebSiteURL"];
                shareBox(lblContentTitle.Text,
                        "publication",
                        lblContent.Text,
                        WebSiteURL + imgContent.ImageUrl);

                if (dtContent.Rows[0]["full_material_"+lang].ToParseStr().Length != 0)
                {
                    pnlTamMaterial.Visible = true;
                }

                if (dtContent.Rows[0]["short_material_"+lang].ToParseStr().Length != 0)
                {
                    pnlShortMaterial.Visible = true;
                }
            }
        }

        else if (getContentId == 0)
        {
            loadData(0);
            loadPager();
        }
        //else
        //{
        //    loadData(getCategoryID, 0);
        //}


    }

    public int getContentId
    {
        get
        {
            int id = Page.RouteData.Values["id"].ToParseInt();
            return id;
        }
    }
    void loadData(int page)
    {


        rptPosts.DataSource = _db.GetNationalReportsAll(page, rowCount);
        rptPosts.DataBind();




    }
    void loadPager()
    {
        rptPager.Visible = false;
        DataTable dtPager = new DataTable();
        dtPager.Columns.Add("name", typeof(string));
        dtPager.Columns.Add("num", typeof(int));
        dtPager.Columns.Add("cssClass", typeof(string));

        int totalCount = _db.GetNationalReportsAllCount();

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
        loadData(page);

        for (int i = 0; i < rptPager.Items.Count; i++)
        {
            (rptPager.Items[i].FindControl("lnkPager") as LinkButton).CssClass = "";
        }

        lnk.CssClass = "active";
    }

    private void shareBox(string pageTitle, string PageType, string Description, string image_url)
    {
        Description = HttpUtility.HtmlDecode(Config.HtmlRemoval.StripTagsRegex(Description));

        string pageUrl = Request.Url.ToString();
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