using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WebPages_Law : System.Web.UI.Page
{
    
    DALC _db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }

        string lang = Config.getLang(Page);
        
        string title = DALC.GetStaticValue("reportingstatus_page_title");
        //Page.Title = title + " " + Config.GetAppSetting("ProjectName");
        Page.Title = title ;
        ltrBreadCrumb.Text = string.Format("<a href='/{0}/home'> {1}</a> / {2}  ",
             lang,
             DALC.GetStaticValue("home_breadcrumb_title"),
             DALC.GetStaticValue("reporting_targets_h"));



        lblgostericinote2.Text = DALC.GetStaticValue("target_report");
        lblcemi2.Text = DALC.GetStaticValue("reportingstatuscemi_value");



        rptIndicators2.DataSource = _db.GetTargetsReportingStatusPriotet();
        rptIndicators2.DataBind();



        lblmeqseduzrenote2.Text = DALC.GetStaticValue("reportingstatuspurpose_value");



        DataTable dt = _db.GetTargetsReportingStatusSum();
        if (dt != null)
        {
            lblgostericicemi2.Text = dt.Rows[0]["say"].ToParseStr();
        }
        DataTable dty1 = _db.GetTargetsReportingStatusSumPrioritetdir();
        if (dty1 != null)
        {
            lblpiroritetdir.Text = dty1.Rows[0]["say"].ToParseStr();
            lblpiroritetdirfaiz.Text = dty1.Rows[0]["faiz"].ToParseStr();
            lblpiroritetdirnote.Text = dty1.Rows[0]["name_" + lang].ToParseStr();
        }

        DataTable dty3 = _db.GetTargetsReportingStatusSumPrioritetDeyil();
        if (dty3 != null)
        {
            lblpiroritetdeyil.Text = dty3.Rows[0]["say"].ToParseStr();
            lblpiroritetdeyilfaiz.Text = dty3.Rows[0]["faiz"].ToParseStr();
            lblpiroritetdeyilnote.Text = dty3.Rows[0]["name_" + lang].ToParseStr(); ;
        }



        DataTable dtx = _db.GetPages(Utils.PageType.International);
        string content = dtx.Rows[0]["content_" + lang].ToParseStr();
        string pageTitle = Config.HtmlRemoval.StripTagsRegex(dtx.Rows[0]["title_" + lang].ToParseStr());
      

        shareBox(pageTitle,
    "reporting status",
    content.Length > 1000 ? content.Substring(0, 1000) : content,
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