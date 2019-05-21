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

        DataTable dt = _db.GetIndicatorsReportingStatusSum();
        if (dt != null)
        {
            lblgostericicemi.Text = dt.Rows[0]["say"].ToParseStr();
        }
        DataTable dt1 = _db.GetIndicatorsReportingStatusSumMovcud();
        if (dt1 != null)
        {
            lblmovcuddur.Text = dt1.Rows[0]["say"].ToParseStr();
            lblmovcuddurfaiz.Text = dt1.Rows[0]["faiz"].ToParseStr();
            lblmovcuddurnote.Text = dt1.Rows[0]["name_" + lang].ToParseStr();
        }
        DataTable dt2 = _db.GetIndicatorsReportingStatusSumPlan();
        if (dt2 != null)
        {
            lblplan.Text = dt2.Rows[0]["say"].ToParseStr();
            lblplanfaiz.Text = dt2.Rows[0]["faiz"].ToParseStr();
            lblplannote.Text = dt2.Rows[0]["name_" + lang].ToParseStr();
        }
        DataTable dt3 = _db.GetIndicatorsReportingStatusSumArasdirilir();
        if (dt3 != null)
        {
            lblmelumatyoxdur.Text = dt3.Rows[0]["say"].ToParseStr();
            lblmelumatyoxdurfaiz.Text = dt3.Rows[0]["faiz"].ToParseStr();
            lblmelumatyoxdurnote.Text = dt3.Rows[0]["name_"+ lang].ToParseStr(); ;
        }
    }
   

}