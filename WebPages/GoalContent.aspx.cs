using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_GoalContent : System.Web.UI.Page
{
    DALC _db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }
        string lang = Config.getLang(Page);
        int goalId = Page.RouteData.Values["goalid"].ToParseInt();
        DataTable dt = _db.GetGoalById(goalId);
        if (dt.Rows.Count > 0)
        {
            lblGoalName.Text = dt.Rows[0]["name_" + lang].ToParseStr();
            

            Page.Title = lblGoalName.Text + " - " + Config.GetAppSetting("ProjectName");

            imgGoal.ImageUrl = "/images/goals-" + lang + "/goal-" + dt.Rows[0]["id"].ToParseStr().PadLeft(2, '0') + ".png";
            ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / {2} ", lang,
            DALC.GetStaticValue("home_breadcrumb_title"), DALC.GetStaticValue("goal_value") + " " + dt.Rows[0]["priority"].ToParseStr());
        }
        rptIndicators.DataSource = _db.GetIndicatorsByParentId(goalId, 0, 1);
        rptIndicators.DataBind();

    }
}