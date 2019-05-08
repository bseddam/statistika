using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WebPages_Publication_list : System.Web.UI.Page
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

        linkPublications.Text = DALC.GetStaticValue("publications_menu_title");
        linkPublications.NavigateUrl = string.Format("/{0}/publication", lang);

        linkResearch.Text = DALC.GetStaticValue("researches_menu_title");
        linkResearch.NavigateUrl = string.Format("/{0}/research", lang);

        ltrLeftInfo.Text = DALC.GetStaticValue("publication_left_menu_info");

        Page.Title = Config.GetAppSetting("ProjectName");


        DataTable dtCategory = _db.GetPublicationCategroyById(getTypeid);

        lblPageTitle.Text = dtCategory.Rows[0]["name_" + lang].ToParseStr();
        Page.Title = lblPageTitle.Text + " " + Config.GetAppSetting("ProjectName");

        ltrBreadCrumb.Text = string.Format("<a href='/{0}/home'> {1}</a> / <a href='/{0}/publication-research'> {4} </a> / <a href='/{0}/publication'> {2} </a> / {3} ", lang,
         DALC.GetStaticValue("home_breadcrumb_title"),
         DALC.GetStaticValue("publications_page_title"),
         dtCategory.Rows[0]["name_" + lang],
         DALC.GetStaticValue("home_box_4")
         );


        loadData(0);
        loadPager();
        _loadCategory();


    


    }

    private void _loadCategory()
    {
        rptPubCategory.DataSource = _db.GetPublicationCategory();
        rptPubCategory.DataBind();

    }

    public int getTypeid
    {
        get
        {
            int id = Page.RouteData.Values["typeid"].ToParseInt();
            return id;
        }
    }

    void loadData(int page)
    {
        rptPosts.DataSource = _db.GetPublicationsAll(page, rowCount, getTypeid);
        rptPosts.DataBind();
    }
    void loadPager()
    {
        rptPager.Visible = false;
        DataTable dtPager = new DataTable();
        dtPager.Columns.Add("name", typeof(string));
        dtPager.Columns.Add("num", typeof(int));
        dtPager.Columns.Add("cssClass", typeof(string));

        int totalCount = _db.GetPublicationsAllCount(getTypeid);

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
}