using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_SearchDetailed : System.Web.UI.Page
{
    DALC _db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {
        loadFromViewState();
        if (IsPostBack)
        {
            return;
        }
        string lang = Config.getLang(Page);


        loadLabel();
        loadGridColumns();
        loadDropdown(lang);


        Page.Title = Config.HtmlRemoval.StripTagsRegex(ltrTitle.Text) + " - " + Config.GetAppSetting("ProjectName");


        ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / {2} ",
            lang,
            DALC.GetStaticValue("home_breadcrumb_title"),
           Config.HtmlRemoval.StripTagsRegex(ltrTitle.Text));


        pnlGoalResult.Visible =
        PnlIndicatorResult.Visible =
        PnlTargetResult.Visible = false;


    }

    private void loadFromViewState()
    {
        _helperViewState(GridGoal);
        _helperViewState(GridTarget);
        _helperViewState(GridIndicator);
    }
    void _helperViewState(ASPxGridView grid)
    {
        string stateName = grid.ID;
        if (ViewState[stateName] != null)
        {
            grid.DataSource = ViewState[stateName] as DataTable;
            grid.DataBind();
        }
    }

    private void loadGridColumns()
    {
        GridGoal.Columns["priority"].Caption = DALC.GetStaticValue("search_goal_grid_no_column");
        GridGoal.Columns["id"].Caption = DALC.GetStaticValue("search_goal_grid_code_column");
        GridGoal.Columns["name_az"].Caption = DALC.GetStaticValue("search_goal_grid_name_column");

        GridTarget.Columns["priority"].Caption = DALC.GetStaticValue("search_target_grid_no_column");
        GridTarget.Columns["code"].Caption = DALC.GetStaticValue("search_target_grid_code_column");
        GridTarget.Columns["name_az"].Caption = DALC.GetStaticValue("search_target_grid_name_column");

        GridIndicator.Columns["priority"].Caption = DALC.GetStaticValue("search_indicator_grid_no_column");
        GridIndicator.Columns["code"].Caption = DALC.GetStaticValue("search_indicator_grid_code_column");
        GridIndicator.Columns["name_az"].Caption = DALC.GetStaticValue("search_indicator_grid_name_column");

    }

    private void loadDropdown(string lang)
    {
        ddlIndicatorQurum.DataValueField = "id";
        ddlIndicatorQurum.DataTextField = "name_" + lang;
        ddlIndicatorQurum.DataSource = _db.GetQurum();
        ddlIndicatorQurum.DataBind();
        ddlIndicatorQurum.Items.Insert(0,
            new ListItem()
            {
                Text = DALC.GetStaticValue("search_indicator_qurum_placeholder"),
                Value = ""
            }
        );


        ddlIndicatorStatus.DataValueField = "id";
        ddlIndicatorStatus.DataTextField = "name_" + lang;
        ddlIndicatorStatus.DataSource = _db.GetIndicators_status();
        ddlIndicatorStatus.DataBind();
        ddlIndicatorStatus.Items.Insert(0,
          new ListItem()
          {
              Text = DALC.GetStaticValue("search_indicator_status_placeholder"),
              Value = ""
          }
      );
    }

    private void loadLabel()
    {
        ltrTitle.Text = DALC.GetStaticValue("search_detailed_title");

        tabs.TabPages[0].Text = DALC.GetStaticValue("search_tab1");
        tabs.TabPages[1].Text = DALC.GetStaticValue("search_tab2");
        tabs.TabPages[2].Text = DALC.GetStaticValue("search_tab3");

        lblGoalName.Text = DALC.GetStaticValue("search_goal_name_label");
        lblGoalCode.Text = DALC.GetStaticValue("search_goal_code_label");
        txtGoalCode.Attributes["placeholder"] = DALC.GetStaticValue("search_goal_code_placeholder");
        txtGoalName.Attributes["placeholder"] = DALC.GetStaticValue("search_goal_name_placeholder");


        lblTargetCode.Text = DALC.GetStaticValue("search_target_code_label");
        lblTargetName.Text = DALC.GetStaticValue("search_target_name_label");
        txtTargetCode.Attributes["placeholder"] = DALC.GetStaticValue("search_target_code_placeholder");
        txtTargetName.Attributes["placeholder"] = DALC.GetStaticValue("search_target_name_placeholder");




        rbPriotet.Items.Add(new ListItem(DALC.GetStaticValue("search_target_priotet3"), ""));
        rbPriotet.Items.Add(new ListItem(DALC.GetStaticValue("search_target_priotet1"), "1"));
        rbPriotet.Items.Add(new ListItem(DALC.GetStaticValue("search_target_priotet2"), "0"));
        rbPriotet.SelectedIndex = 0;

        lblIndicatorCode.Text = DALC.GetStaticValue("search_indicator_code_label");
        lblIndicatorName.Text = DALC.GetStaticValue("search_indicator_name_label");
        lblIndicatorStatus.Text = DALC.GetStaticValue("search_indicator_status_label");

        txtIndicatorCode.Attributes["placeholder"] = DALC.GetStaticValue("search_indicator_code_placeholder");
        txtIndicatorName.Attributes["placeholder"] = DALC.GetStaticValue("search_indicator_name_placeholder");



        lblIndicatorPriotet.Text = DALC.GetStaticValue("search_indicator_priotet_label");

        rbIndicatorPriotet.Items.Add(new ListItem(DALC.GetStaticValue("search_indicator_priotet3"), ""));
        rbIndicatorPriotet.Items.Add(new ListItem(DALC.GetStaticValue("search_indicator_priotet1"), "1"));
        rbIndicatorPriotet.Items.Add(new ListItem(DALC.GetStaticValue("search_indicator_priotet2"), "2"));
        rbIndicatorPriotet.SelectedIndex = 0;

        lblIndicatorQurum.Text = DALC.GetStaticValue("search_indicator_qurum_label");
        btnTargetSearch.Text =
        btnIndicatorSearch.Text =
        BtnSearchGoal.Text = DALC.GetStaticValue("search_btn");


     



    }
    public int goal_row = 1;
    public int target_row = 1;
    public int indicator_row = 1;
    protected void BtnSearchGoal_Click(object sender, EventArgs e)
    {
        goal_row = 1;
        lblGoalError.Text = "";
        string code = txtGoalCode.Text.Trim();
        string name = txtGoalName.Text.Trim();

        if (code.Length == 0 && name.Length == 0)
        {
            lblGoalError.Text = DALC.GetStaticValue("search_goal_error_text");
            return;
        }
        DataTable dt = _db.GetSearch_Goal(code, name);
        GridGoal.DataSource = dt;
        GridGoal.DataBind();
        ViewState["GridGoal"] = dt;

        pnlGoalResult.Visible = true;
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "script", "  $('.grid-cell').css('border-bottom-width', '');", true);

    }
    public string GetGoalURL(int id, string name_short_az)
    {
        string lang = Config.getLang(Page);

        return string.Format("/{0}/goal-info/{1}/{2}", lang, id, Config.Slug(name_short_az));
    }

    protected void btnTargetSearch_Click(object sender, EventArgs e)
    {
        target_row = 1;
        lblTargetError.Text = "";
        string code = txtTargetCode.Text.Trim();
        string name = txtTargetName.Text.Trim();

        if (code.Length == 0 && name.Length == 0)
        {
            lblTargetError.Text = DALC.GetStaticValue("search_target_error_text");
            return;
        }
        DataTable dt = _db.GetSearch_Target(code, name, rbPriotet.SelectedValue);
        GridTarget.DataSource = dt;
        GridTarget.DataBind();
        ViewState["GridTarget"] = dt;

        PnlTargetResult.Visible = true;
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "script",
            @"$('.grid-cell').css('border-bottom-width', '');", true);

    }
    public string GetTargetURL(int id)
    {
        string lang = Config.getLang(Page);

        return string.Format("/{0}/goal-info/{1}#tab1", lang, id);
    }

    protected void btnIndicatorSearch_Click(object sender, EventArgs e)
    {
        indicator_row = 1;
        lblIndicatorError.Text = "";

        string code = txtIndicatorCode.Text.Trim();
        string name = txtIndicatorName.Text.Trim();
        string qurum = ddlIndicatorQurum.SelectedValue.ToParseStr();
        string status = ddlIndicatorStatus.SelectedValue.ToParseStr();

        if (code.Length == 0 && name.Length == 0 && qurum.Length == 0 && status.Length == 0)
        {
            lblIndicatorError.Text = DALC.GetStaticValue("search_indicator_error_text");
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "script",
            @"$('.select2').select2();", true);
            return;
        }

        DataTable dt = _db.GetSearch_Indicator(code, name, rbIndicatorPriotet.SelectedValue, qurum, status);
        GridIndicator.DataSource = dt;
        GridIndicator.DataBind();
        ViewState["GridIndicator"] = dt;

        PnlIndicatorResult.Visible = true;
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "script",
            @"$('.grid-cell').css('border-bottom-width', '');$('.select2').select2();", true);
    }



    public string GetIndicatorURL(int id, string name)
    {
        string lang = Config.getLang(Page);

        return string.Format("/{0}/indicators/{1}/{2}", lang, id, Config.Slug(name));
    }

}