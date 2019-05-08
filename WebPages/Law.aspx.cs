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
        //pageid
        int goalId = Page.RouteData.Values["pageid"].ToParseInt();
        string lang = Config.getLang(Page);
        DataTable dtGoal = _db.GetGoalById(goalId);

        lblGoalName.Text = DALC.GetStaticValue("goal_value") + " " + dtGoal.Rows[0]["priority"] + " . " + dtGoal.Rows[0]["name_" + lang];
        Page.Title = lblGoalName.Text + " - " + Config.GetAppSetting("ProjectName");
        imgGoal.ImageUrl = "/images/goals-" + lang + "-white/" + goalId + ".png";


        ltrPageName.Text = DALC.GetStaticValue("laws_page_title");

        rptContent.DataSource = _db.GetPagesByGoalIdOrderby(Utils.PageType.Laws, goalId);
        rptContent.DataBind();


        DataTable dt = _db.GetGoals();

        rptGoals.DataSource = dt;
        rptGoals.DataBind();

        ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / {2} ", lang,
         DALC.GetStaticValue("home_breadcrumb_title"),
        Config.HtmlRemoval.StripTagsRegex(ltrPageName.Text));

    }
}