using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_PublicationResearch : System.Web.UI.Page
{
    DALC _db = new DALC();
    int rowCount = 3;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }

        string lang = Config.getLang(Page);

        linkPublications.Text = DALC.GetStaticValue("publications_menu_title");
        linkPublications.NavigateUrl = string.Format("/{0}/publication", lang);

        linkResearch.Text = DALC.GetStaticValue("researches_menu_title");
        linkResearch.NavigateUrl = string.Format("/{0}/research", lang);

        ltrLeftInfo.Text = DALC.GetStaticValue("researches_publication_left_menu_info");

        Page.Title = Config.GetAppSetting("ProjectName");

        lblPageTitle.Text = DALC.GetStaticValue("researches_page_title2");
        Page.Title = lblPageTitle.Text + " " + Config.GetAppSetting("ProjectName");

        ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / {2} ", lang,
         DALC.GetStaticValue("home_breadcrumb_title"),
         DALC.GetStaticValue("home_box_4"));

        ltrPageTitle2.Text = DALC.GetStaticValue("publications_page_title2");

        loadData(0);
        loadPublication(0);
        _loadCategory();
    }
    private void _loadCategory()
    {
        rptPubCategory.DataSource = _db.GetPublicationCategory();
        rptPubCategory.DataBind();
    }
    void loadData(int page)
    {
        rptPosts.DataSource = _db.GetResearchAll(page, rowCount);
        rptPosts.DataBind();
    }
    void loadPublication(int page)
    {
        rptPublication.DataSource = _db.GetPublicationsAll(page, rowCount);
        rptPublication.DataBind();
    }
    public string getContent(string text)
    {
        string newText = Config.HtmlRemoval.StripTagsRegex(text);
        return newText.Length > 300 ? (newText.Substring(0, 300) + "...") : newText;
    }
}