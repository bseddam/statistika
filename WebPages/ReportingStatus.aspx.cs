using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
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
        Page.Title  = DALC.GetStaticValue("compare_page_title");
       
        ltrBreadCrumb.Text = string.Format("<a href='/{0}/home'> {1}</a> / {2}  ",
             lang,
             DALC.GetStaticValue("home_breadcrumb_title"),
             DALC.GetStaticValue("compare_page_title"));

        rptIndicators.DataSource = _db.GetIndicatorsReportingStatus();
        rptIndicators.DataBind();

    }
   

}