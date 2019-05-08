using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class WebPages_UsefulLinks : System.Web.UI.Page
{
    DALC _db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {
        string lang = Config.getLang(Page);
        string pageTitle = DALC.GetStaticValue("usefullinks_page_title");
        ltrTitle.Text = pageTitle;
        Page.Title = pageTitle + " - " + Config.GetAppSetting("ProjectName");

        if (IsPostBack)
        {
            return;
        }
        DataTable dtGoals = _db.GetGoals();
        rptGoals.DataSource = dtGoals;
        rptGoals.DataBind();

        ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / {2} ",
            lang,
            DALC.GetStaticValue("home_breadcrumb_title"),
            pageTitle);


        if (getNationalValueFromSession == 1)
        {
            btnMilli.CssClass = "active";
        }
        else
        {
            btnBeynalxalq.CssClass = "active";
        }


        btnBeynalxalq.Text = DALC.GetStaticValue("usefullinks_page_international");
        btnMilli.Text = DALC.GetStaticValue("usefullinks_page_national");

        btnTarget.Text = DALC.GetStaticValue("usefullinks_page_targets_global");
        btnIndicator.Text = DALC.GetStaticValue("usefullinks_page_indicators_global");



        DataView dv = dtGoals.DefaultView;
        dv.RowFilter = "id=" + getGoalId;

        if (dv.Count > 0)
        {
            lblGoalName.Text = DALC.GetStaticValue("goal_value") + " " + dv[0]["priority"] + ". " + dv[0]["name_" + lang];
            imgGoal.ImageUrl = "/images/goals-" + Config.getLang(Page) + "/goal-" + dv[0]["id"].ToParseStr().PadLeft(2, '0') + ".png";
        }



        lblTargetColumn1.Text = DALC.GetStaticValue("usefullinks_page_targets_column1");
        lblTargetColumn2.Text = DALC.GetStaticValue("usefullinks_page_targets_column2");

        ltrIndicatorColumn1.Text = DALC.GetStaticValue("usefullinks_page_indicator_column1");
        ltrIndicatorColumn2.Text = DALC.GetStaticValue("usefullinks_page_indicator_column2");
        ltrIndicatorColumn3.Text = DALC.GetStaticValue("usefullinks_page_indicator_column3");

        //_loadTargets();

        if (Session["ul-target"] == "indicator")
        {
            helperTargetClick(btnIndicator);
        }
        else
        {
            helperTargetClick(btnTarget);
        }

    }
    int getGoalId
    {

        get
        {
            int goalId = Page.RouteData.Values["goalId"].ToParseInt();
            goalId = (goalId == 0 ? 1 : goalId);

            return goalId;
        }
    }

    int getNationalValueFromSession
    {

        get
        {
            if (Session["ul-global"] == "global")
            {
                return 0;
            }
            return 1;
        }
    }

    int getNationalValue
    {
        get
        {
            if (btnBeynalxalq.CssClass == "active")
            {
                return btnBeynalxalq.CommandArgument.ToParseInt();
            }
            else
            {
                return btnMilli.CommandArgument.ToParseInt();
            }
        }
    }
    private void _loadTargets()
    {
        string lang = Config.getLang(Page);
        DataTable dtTargets = _db.GetTargets(getGoalId);
        lblTargetColumn1_result.Text = "";
        for (int i = 0; i < dtTargets.Rows.Count; i++)
        {
            lblTargetColumn1_result.Text += string.Format("{0}. {1}", dtTargets.Rows[i]["code"], dtTargets.Rows[i]["name_" + lang]) + "<br><br>";
        }
        DataTable dtTargetsQurum = _db.Get_useful_links_targets(getNationalValue, getGoalId);
        rptTargetsColumn2.DataSource = dtTargetsQurum;
        rptTargetsColumn2.DataBind();
    }

    void _loadIndicators()
    {
        string lang = Config.getLang(Page);
        DataTable dt = _db.Get_useful_links_indicators(getNationalValue, getGoalId, lang);
        rptIndicators.DataSource = dt;
        rptIndicators.DataBind();
    }

    protected void btnBeynalxalq_Click(object sender, EventArgs e)
    {
        btnBeynalxalq.CssClass = btnMilli.CssClass = "";
        LinkButton btn = (sender as LinkButton);
        btn.CssClass = "active";

        _loadTargets();
        _loadIndicators();

        string text = "global";
        Session["ul-global"] = "global";
        if (btnBeynalxalq.CssClass != "active")
        {
            text = "national";
            Session["ul-global"] = "national";
        }

        btnTarget.Text = DALC.GetStaticValue("usefullinks_page_targets_" + text);
        btnIndicator.Text = DALC.GetStaticValue("usefullinks_page_indicators_" + text);


    }
    protected void btnTarget_Click(object sender, EventArgs e)
    {
        btnTarget.CssClass = btnIndicator.CssClass = "";
        LinkButton btn = (sender as LinkButton);
       
        helperTargetClick(btn);
    }

    void helperTargetClick(LinkButton btn)
    {
        btn.CssClass = "active";

        Session["ul-target"] = btn.CommandArgument;
        pnlTargets.Visible = pnlIndicator.Visible = false;
        if (btn.CommandArgument == "target")
        {
            pnlTargets.Visible = true;
            _loadTargets();
        }
        else if (btn.CommandArgument == "indicator")
        {
            pnlIndicator.Visible = true;
            _loadIndicators();
        }
    }
}