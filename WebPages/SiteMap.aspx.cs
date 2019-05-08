using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_SiteMap : System.Web.UI.Page
{
    DALC db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }

        ltrPageTitle.Text = Page.Title = DALC.GetStaticValue("sitemap_page_title");

        DataTable dtHeader = db.GetSiteMap(0,Config.getLang(Page));
        rptHeaderMenu.DataSource = dtHeader;
        rptHeaderMenu.DataBind();

        for (int i = 0; i < dtHeader.Rows.Count; i++)
        {
            if (dtHeader.Rows[i]["SubCOunt"].ToParseInt() > 0)
            {
                Repeater rptHeaderSub = rptHeaderMenu.Items[i].FindControl("rptHeaderSub") as Repeater;
                rptHeaderSub.DataSource = db.GetSiteMap(dtHeader.Rows[i]["Id"].ToParseInt(), Config.getLang(Page));
                rptHeaderSub.DataBind();
            }
        }

    }
}