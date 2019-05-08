using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class WebPages_StatisticDB : System.Web.UI.Page
{
    DALC _db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {

        //http://data.unescap.org/sdg/#compareData
        if (IsPostBack)
        {
            return;
        }
        Grid.Visible = false;
        string lang = Config.getLang(Page);

        _loadDropdowns(lang);

        Page.Title = ltrTitle.Text = DALC.GetStaticValue("statistical_database_page_title");
        ltrBreadCrumb.Text = string.Format("<a href='/{0}/home'> {1}</a> / {2}  ",
                     lang,
                     DALC.GetStaticValue("home_breadcrumb_title"),
                     ltrTitle.Text);


        _loadGoals();

        lblRegionTitle.Text = DALC.GetStaticValue("statistical_database_region_title");
        lblGoalTitle.Text = DALC.GetStaticValue("statistical_database_goal_title");
        lblIndicatorTitle.Text = DALC.GetStaticValue("statistical_database_indicator_title");
        lblYearTitle.Text = DALC.GetStaticValue("statistical_database_year_title");

        btnHesabat.Text = DALC.GetStaticValue("statistical_database_btn_hesabat");

        //RegionList_SelectedIndexChanged(null, null);
        //DataTable dtIndicator = _db.GetIndicatorById(indicatorid);

        //if (dtIndicator == null || dtIndicator.Rows.Count < 1)
        //{
        //    Config.Rd("/error?null");
        //    return;
        //}


        //DataTable dtGoal = _db.GetGoalById(dtIndicator.Rows[0]["goal_id"].ToParseInt());
        //if (dtGoal.Rows.Count > 0)
        //{
        //    lblGoalName.Text = DALC.GetStaticValue("goal_value") + " " + dtGoal.Rows[0]["priority"] + " . " + dtGoal.Rows[0]["name_short_" + lang];
        //    imgGoal.ImageUrl = "/images/goals-" + lang + "/goal-" + dtGoal.Rows[0]["id"].ToParseStr().PadLeft(2, '0') + ".png";
        //}

        //LtrIndicatorInfo.Text = dtIndicator.Rows[0]["info_" + lang].ToParseStr();



        //Page.Title = dtIndicator.Rows[0]["name_" + lang].ToParseStr();


        //DataTable dtMetadata = _db.GetMetaDataClient(indicatorid, 0, lang);
        //rptMetaData.DataSource = dtMetadata;
        //rptMetaData.DataBind();


        //for (int i = 0; i < dtMetadata.Rows.Count; i++)
        //{
        //    DataTable dtMsub = _db.GetMetaDataClient(indicatorid, dtMetadata.Rows[i]["list_id"].ToParseInt(), lang);
        //    Literal ltrInfo = rptMetaData.Items[i].FindControl("ltrInfo") as Literal;
        //    ltrInfo.Text = dtMetadata.Rows[i]["name_" + lang].ToParseStr();

        //    if (dtMsub.Rows.Count > 0)
        //    {
        //        Repeater rptMetadataSub = rptMetaData.Items[i].FindControl("rptMetadataSub") as Repeater;
        //        rptMetadataSub.DataSource = dtMsub;
        //        rptMetadataSub.DataBind();
        //    }

        //}


    }

    private void _loadGoals()
    {
        DataTable dt = _db.GetGoals();
        rptGoals.DataSource = dt;
        rptGoals.DataBind();

        _loadIndicators(dt.Rows[0]["id"].ToParseInt());
        for (int i = 0; i < rptGoals.Items.Count; i++)
        {
            LinkButton myBtn = rptGoals.Items[i].FindControl("lnkGoal") as LinkButton;

            myBtn.CssClass = i == 0 ? "active" : "";
        }
        //for (int i = 0; i < dt.Rows.Count; i++)
        //{
        //    DataTable dtMsub = _db.GetIndicators(dt.Rows[i]["id"].ToParseInt());
        //    Repeater rptIndicators = rptGoals.Items[i].FindControl("rptIndicators") as Repeater;
        //    rptIndicators.DataSource = dtMsub;
        //    rptIndicators.DataBind();
        //}
    }

    private void _loadDropdowns(string lang)
    {
        RegionList.DataTextField = "name_" + lang;
        RegionList.DataSource = _db.GetRegion();
        RegionList.DataBind();
        RegionList.SelectedIndex = 0;

        DataTable dtYears = _db.GetHesabat_Years();
        chkYears.DataSource = dtYears;
        chkYears.DataBind();
    }
    protected void RegionList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //        int indicatorid = Page.RouteData.Values["indicatorid"].ToParseInt();

        //        string script = @"google.charts.load('current', { 'packages': ['corechart'] });
        //        google.charts.setOnLoadCallback(drawChart);
        //
        //        function drawChart() {
        //            var data = google.visualization.arrayToDataTable([
        //            ";
        //        Dictionary<string, string>
        //    selectedRegion = new Dictionary<string, string>
        //        ();
        //        string columns = "'Year',";
        //        for (int i = 0; i < RegionList.Items.Count; i++)
        //        {
        //            if (RegionList.Items[i].Selected)
        //            {
        //                columns += string.Format("'{0}',", RegionList.Items[i].Text);
        //                selectedRegion.Add(RegionList.Items[i].Value, RegionList.Items[i].Text);
        //            }
        //        }

        //        script += "[" + columns.Trim(',') + "],";

        //        DataTable dtYears = _db.GetHesabatforChart_Years(indicatorid);

        //        for (int i_year = 0; i_year < dtYears.Rows.Count; i_year++)
        //        {
        //            string values = "";
        //            foreach (KeyValuePair<string, string>
        //                item in selectedRegion)
        //            {
        //                string value = _db.GetHesabatforChart_Value(indicatorid, item.Key.ToParseInt(), dtYears.Rows[i_year]["year"].ToParseInt());
        //                values += value + ",";
        //            }

        //            script += string.Format(" [{0},{1}],",
        //            dtYears.Rows[i_year]["year"].ToParseInt(),
        //            values.Trim(','));



        //        }
        //        script = script.Trim(',');
        //        script += "]);";

        //        script += @"
        //            var options = {
        //            title: '',hAxis: {
        //
        //            gridlines: {
        //            count: " + dtYears.Rows.Count + @"
        //            },
        //            },
        //            curveType: 'function',legend: { position: 'bottom',maxLines: 3  }, pointSize: 5
        //            };
        //
        //            var chart = new google.visualization.LineChart(document.getElementById('chart_div'));
        //
        //            chart.draw(data, options);
        //            }";
        //        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);


    }

    protected void lnkGoal_Click(object sender, EventArgs e)
    {
        LinkButton btn = (sender as LinkButton);
        string id = btn.CommandArgument;

        for (int i = 0; i < rptGoals.Items.Count; i++)
        {
            LinkButton myBtn = rptGoals.Items[i].FindControl("lnkGoal") as LinkButton;
            myBtn.CssClass = "";
        }

        btn.CssClass = "active";


        _loadIndicators(id.ToParseInt());


    }

    private void _loadIndicators(int goalId)
    {

        rptIndicators.DataTextField = "name_az";

        DataTable dt = _db.GetIndicators(goalId);

        rptIndicators.DataSource = dt;
        rptIndicators.DataBind();


        for (int i = 0; i < rptIndicators.Items.Count; i++)
        {
            rptIndicators.Items[i].Text = string.Format("{0} {1}",
                Config.ClearIndicatorCode(dt.Rows[i]["code"].ToParseStr()),
                dt.Rows[i]["name_" + Config.getLang(Page)]);
        }
    }
    protected void btn_Click(object sender, EventArgs e)
    {
        Grid.Visible = true;
        string lang = Config.getLang(Page);
        string _indicators = "";
        for (int i = 0; i < rptIndicators.Items.Count; i++)
        {
            if (rptIndicators.Items[i].Selected)
            {
                _indicators += rptIndicators.Items[i].Value + ",";
            }
        }
        string _regionIds = "";
        for (int i = 0; i < RegionList.Items.Count; i++)
        {
            if (RegionList.Items[i].Selected)
            {
                _regionIds += RegionList.Items[i].Value + ",";
            }
        }
        DataTable dtHesabat = new DataTable();
        dtHesabat.Columns.Add("id", typeof(int));
        dtHesabat.Columns.Add("GoalName", typeof(string));
        dtHesabat.Columns.Add("IndicatorCode", typeof(string));
        dtHesabat.Columns.Add("IndicatorName", typeof(string));
        dtHesabat.Columns.Add("IndicatorSize", typeof(string));
        dtHesabat.Columns.Add("RegionName", typeof(string));


        Grid.Columns.Clear();

        GridViewDataColumn column = new GridViewDataColumn();
        column.Caption = DALC.GetStaticValue("statistical_database_grid_goal_name");
        column.FieldName = "GoalName";
        Grid.Columns.Add(column);

        column = new GridViewDataColumn();
        column.Caption = DALC.GetStaticValue("statistical_database_grid_indicator_code");
        column.FieldName = "IndicatorCode";
        Grid.Columns.Add(column);

        column = new GridViewDataColumn();
        column.Caption = DALC.GetStaticValue("statistical_database_grid_indicator_name");
        column.FieldName = "IndicatorName";
        Grid.Columns.Add(column);

        column = new GridViewDataColumn();
        column.Caption = DALC.GetStaticValue("statistical_database_grid_indicator_size");
        column.FieldName = "IndicatorSize";
        Grid.Columns.Add(column);

        column = new GridViewDataColumn();
        column.Caption = DALC.GetStaticValue("statistical_database_grid_region_name");
        column.FieldName = "RegionName";
        Grid.Columns.Add(column);


        List<int> years = new List<int>();

        string _years = "";
        for (int i = 0; i < chkYears.Items.Count; i++)
        {
            if (chkYears.Items[i].Selected)
            {
                years.Add(chkYears.Items[i].Value.ToParseInt());
                _years += chkYears.Items[i].Value + ",";
            }
        }
        years.Sort();

        for (int i_year = 0; i_year < years.Count; i_year++)
        {
            dtHesabat.Columns.Add(years[i_year].ToParseStr(), typeof(string));

            column = new GridViewDataColumn();
            column.Caption = years[i_year].ToParseStr();
            column.FieldName = years[i_year].ToParseStr();
            Grid.Columns.Add(column);
        }

        DataTable dtH = _db.GetHesabat2(_indicators.Trim(','), _years.Trim(','), _regionIds.Trim(','), lang);

        for (int i = 0; i < dtH.Rows.Count; i++)
        {
            DataRow dr = dtHesabat.NewRow();
            dr["id"] = dtH.Rows[i]["id"].ToParseInt();
            dr["GoalName"] = dtH.Rows[i]["GoalName"].ToParseStr();
            dr["IndicatorCode"] = Config.ClearIndicatorCode(dtH.Rows[i]["IndicatorCode"].ToParseStr());
            dr["IndicatorName"] = dtH.Rows[i]["IndicatorName"].ToParseStr();
            dr["IndicatorSize"] = dtH.Rows[i]["IndicatorSize"].ToParseStr();
            dr["RegionName"] = dtH.Rows[i]["RegionName"].ToParseStr();

            for (int i_year = 0; i_year < years.Count; i_year++)
            {
                string dtHs = _db.GetHesabat2_value(dtH.Rows[i]["indicator_id"].ToParseInt(),
                                                  years[i_year],
                                                  dtH.Rows[i]["region_id"].ToParseInt());

                //DataView dvVal = dtHs.DefaultView;
                //dvVal.RowFilter = string.Format("region_id={0} and year={1} and indicator_id={2}",
                //    dtH.Rows[i]["id"].ToParseInt(),
                //    years[i_year],
                //    dtH.Rows[i]["indicator_id"].ToParseInt()
                //    );

                dr[years[i_year].ToParseStr()] = dtHs;

            }
            dtHesabat.Rows.Add(dr);
        }

        //for (int i_year = 0; i_year <= years.Count; i_year++)
        //{
        //    for (int i = 0; i < dtHesabat.Rows.Count; i++)
        //    {
        //        DataView dvVal = dtH.DefaultView;
        //        dvVal.RowFilter = string.Format(" id={0} ", dtH.Rows[i]["id"].ToParseInt());

        //        dtHesabat.Rows[i][years[i_year].ToParseStr()] = dvVal.Count > 0 ? dvVal[0]["value"] : 0;
        //    }
        //}

        Grid.DataSource = dtHesabat;
        Grid.DataBind();
        ViewState["Grid"] = Grid.DataSource;
    }
}