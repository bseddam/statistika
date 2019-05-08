using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_OrderList : System.Web.UI.Page
{
    DALC _db = new DALC();
    int rowCount = Utils.rowCount;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }

        loadData(0);
        loadPager();

        lblTitle.Text = DALC.GetStaticValue(getPageType.ToParseStr().ToLower() + "_page_title");

        Page.Title = lblTitle.Text + " - " + Config.GetAppSetting("ProjectName");

        ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / {2} ", Config.getLang(Page),
        DALC.GetStaticValue("home_breadcrumb_title"),
        lblTitle.Text);

    }
    public Utils.PageType getPageType
    {

        get
        {
            Utils.PageType _type = Utils.PageType.Order;

            
            return _type;
        }
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

    void loadData(int page)
    {
        rptNews.DataSource = _db.GetPages_All(getPageType, page, rowCount);
        rptNews.DataBind();
    }
    void loadPager()
    {
        rptPager.Visible = false;
        DataTable dtPager = new DataTable();
        dtPager.Columns.Add("name", typeof(string));
        dtPager.Columns.Add("num", typeof(int));
        dtPager.Columns.Add("cssClass", typeof(string));

        int totalCount = _db.GetPages_AllCount(getPageType);

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
}