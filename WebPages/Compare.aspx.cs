using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_Compare : System.Web.UI.Page
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

        _loadDropdowns(lang);

        Page.Title = ltrTitle.Text = DALC.GetStaticValue("compare_page_title");
        ltrBreadCrumb.Text = string.Format("<a href='/{0}/home'> {1}</a> / {2}  ",
                     lang,
                     DALC.GetStaticValue("home_breadcrumb_title"),
                     ltrTitle.Text);


        _loadGoals(lang);

        lblGoalTitle.Text = DALC.GetStaticValue("compare_goal_title");
        lblDesc.Text = DALC.GetStaticValue("compare_desc");
        //lblRegionTitle.Text = DALC.GetStaticValue("statistical_database_region_title");
        lblIndicatorTitle.Text = DALC.GetStaticValue("compare_indicator_title");
        lblYearTitle.Text = DALC.GetStaticValue("compare_year_title");

        btnHesabat.Text = DALC.GetStaticValue("compare_btn_hesabat");
        BtnSearch.Text = DALC.GetStaticValue("compare_search_btn");


        pnlResult.Visible = false;
        pnlIndicator.Style["display"] = "none";

    }

    private void _loadGoals(string lang)
    {
        string goal = DALC.GetStaticValue("goal_value");
        DataTable dt = _db.GetGoals();

        foreach (DataRow item in dt.Rows)
        {
            ddlGoals.Items.Add(new ListItem()
            {
                Text = string.Format("{0} {1} . {2}", goal, item["priority"], item["name_short_" + lang]),
                Value = item["id"].ToParseStr()
            });
        }
        ddlGoals.Items.Insert(0, new ListItem(DALC.GetStaticValue("compare_goal_selector"), "-1"));

        ddlGoals.SelectedIndex = 0;
        //ddlGoals_SelectedIndexChanged(null, null);
    }

    private void _loadDropdowns(string lang)
    {
        DataTable dtYears = _db.GetHesabat_Years();
        chkYears.DataSource = dtYears;
        chkYears.DataBind();
    }

    protected void lnkGoal_Click(object sender, EventArgs e)
    {
        //LinkButton btn = (sender as LinkButton);
        //string id = btn.CommandArgument;

        //for (int i = 0; i < rptGoals.Items.Count; i++)
        //{
        //    LinkButton myBtn = rptGoals.Items[i].FindControl("lnkGoal") as LinkButton;
        //    myBtn.CssClass = "";
        //}

        //btn.CssClass = "active";


        //_loadIndicators(id.ToParseInt());


    }
    public string GetDataTemplate(int parentId, string code, string name, int id, string desc)
    {
        if (parentId == 0)
        {
            return string.Format("<a data-toggle='tooltip' href='/{0}/indicators/{1}/{2}' title='{3}'>{4} {5}</a>",
                Config.getLang(Page),
                id,
                Config.Slug(name),
                desc,
                Config.ClearIndicatorCode(code),
                name);
        }
        else
        {
            return string.Format("{0} ",
                name);
        }
    }
    private void _loadIndicators(int goalId)
    {
        string lang = Config.getLang(Page);
        //rptIndicators.DataTextField = "name_az";

        DataTable dt = _db.GetIndicators_withDesc2(goalId, lang);



        treeList1.DataSource = dt;
        treeList1.DataBind();
        ViewState["treeList1"] = treeList1.DataSource;


        //for (int i = 0; i < rptIndicators.Items.Count; i++)
        //{
        //    rptIndicators.Items[i].Text = string.Format("{0} {1}",
        //        Config.ClearIndicatorCode(dt.Rows[i]["code"].ToParseStr()),
        //        dt.Rows[i]["name_" + Config.getLang(Page)]);
        //}
    }

    private void loadFromViewState()
    {
        if (ViewState["treeList1"] == null)
        {
            return;
        }
        treeList1.DataSource = ViewState["treeList1"] as DataTable;
        treeList1.DataBind();
    }


    protected void btn_Click(object sender, EventArgs e)
    {
        lblMuqayise1.Text = lblMuqayise2.Text = lblError.Text = "";
        string lang = Config.getLang(Page);

        int rowCount = 0;
        for (int i_year = 0; i_year < chkYears.Items.Count; i_year++)
        {
            if (chkYears.Items[i_year].Selected == true)
            {
                rowCount++;
            }
        }

        if (rowCount == 0)
        {
            lblError.Text += DALC.GetStaticValue("compare_year_not_selected") + "<br>";
        }


        List<int> indicators = new List<int>();
        int indicator_count = 0;
        string secilenGosderici = "";
        for (int i = 0; i < treeList1.GetSelectedNodes().Count; i++)
        {
            if (treeList1.GetSelectedNodes()[i].Selected == true)
            {
                indicators.Add(treeList1.GetSelectedNodes()[i].Key.ToParseInt());
                secilenGosderici += string.Format("{0} {1};",
                    Config.ClearIndicatorCode(treeList1.GetSelectedNodes()[i].GetValue("code").ToParseStr()),
                    treeList1.GetSelectedNodes()[i].GetValue("name_" + lang).ToParseStr());
                indicator_count++;
            }
        }

        if (indicator_count != 2)
        {
            lblError.Text += DALC.GetStaticValue("compare_indicator_not_selected") + "<br>";
        }

        if (lblError.Text.Length > 0)
        {
            lblMuqayise1.Text = lblMuqayise2.Text = "";
            pnlResult.Visible = false;
            return;
        }
        lblSelectedGoal.Text = ddlGoals.SelectedItem.Text;
        lblMuqayise1.Text = secilenGosderici.Split(';')[0];
        lblMuqayise2.Text = secilenGosderici.Split(';')[1];

        pnlResult.Visible = true;
        //lblDownload.Text = DALC.GetStaticValue("download");
        lblMuqayiseEdilenlerLabel.Text = DALC.GetStaticValue("compare_text");
        compare_download_label.Text = DALC.GetStaticValue("compare_download_label");
        compare_download_jpg.Text = DALC.GetStaticValue("compare_download_jpg");
        compare_download_png.Text = DALC.GetStaticValue("compare_download_png");
        compare_download_print.Text = DALC.GetStaticValue("compare_download_print");

        loadChartMutipleIndicator(lang, indicators);
    }

    void loadChartMutipleIndicator(string lang, List<int> indicators)
    {

        string data = "";

        string columns = "data.addColumn('string', 'Year');";
        for (int i = 0; i < indicators.Count; i++)
        {
            DataTable dtIndicator = _db.GetIndicatorById(indicators[i]);
            columns += string.Format("data.addColumn('number', '{0}');", dtIndicator.Rows[0]["name_" + lang]);
        }

        data += columns.Trim(',');
        //data += "[" + columns.Trim(',') + "],";
        int rowCount = 0;
        data += "data.addRows([";
        for (int i_year = 0; i_year < chkYears.Items.Count; i_year++)
        {
            if (chkYears.Items[i_year].Selected == true)
            {
                string values = "";
                foreach (int indicatorid in indicators)
                {
                    string value = _db.GetHesabatforChart_Value(indicatorid, chkYears.Items[i_year].Text.ToParseInt());
                    if (!value.Contains("...") && !value.Contains("-"))
                    {
                        values += string.Format("{0},", value);
                    }
                }
                if (values != "")
                {
                    data += string.Format(" ['{0}',{1}],",
                             chkYears.Items[i_year].Text.ToParseInt(),
                            values.Trim(','));
                }
                rowCount++;
            }
        }
        data = data.Trim(',');
        data += " ]);";



        _loadChart(data, rowCount);
    }

    void _loadChart(string data, int row_count)
    {
        string script = @"google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart(type) {
            var data = new google.visualization.DataTable();
               " + data + @"
            var options = {
            title: '',hAxis: {
            format: ' ',
            gridlines: {
            count: " + row_count + @"
            },
            },
            curveType: 'function',legend: { position: 'bottom',maxLines: 3  }, pointSize: 5
            };

            var chart;
            if (type=='1') {
                 chart = new google.visualization.BarChart(document.getElementById('chart_div'));
            }
            else if (type=='2') {
                chart = new google.visualization.LineChart(document.getElementById('chart_div'));
            }
            else if (type=='3') {
                chart = new google.visualization.ComboChart(document.getElementById('chart_div'));
            }else {
               chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
            }
            chart.draw(data, options);
        $('.chart-down-compare').attr('href',chart.getImageURI());
            }";
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        //Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "", script, true);
        //chart_script.Text = "<script>"+script+"</script>";
    }



    void loadChartMutipleIndicator1(string lang, List<int> indicators)
    {

        string data = "";

        string columns = "'Year',";
        for (int i = 0; i < indicators.Count; i++)
        {
            DataTable dtIndicator = _db.GetIndicatorById(indicators[i]);
            columns += string.Format("'{0}',", dtIndicator.Rows[0]["name_" + lang]);
        }


        data += "[" + columns.Trim(',') + "],";
        int rowCount = 0;
        for (int i_year = 0; i_year < chkYears.Items.Count; i_year++)
        {
            if (chkYears.Items[i_year].Selected == true)
            {
                string values = "";
                foreach (int indicatorid in indicators)
                {
                    values += string.Format("{0},", _db.GetHesabatforChart_Value(indicatorid, chkYears.Items[i_year].Text.ToParseInt()));
                }
                data += string.Format(" [{0},{1}],",
                         chkYears.Items[i_year].Text.ToParseInt(),
                        values.Trim(','));
                rowCount++;
            }
        }
        data = data.Trim(',');


        _loadChart(data, rowCount);
    }

    void _loadChart1(string data, int row_count)
    {
        string script = @"google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart(type) {
            var data = google.visualization.arrayToDataTable([" + data + @"]);

            var options = {
            title: '',hAxis: {
            format: ' ',
            gridlines: {
            count: " + row_count + @"
            },
            },
            curveType: 'function',legend: { position: 'bottom',maxLines: 3  }, pointSize: 5
            };

            var chart;
            if (type=='1') {
                 chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
            }
            else if (type=='2') {
                chart = new google.visualization.LineChart(document.getElementById('chart_div'));
            }
            else if (type=='3') {
                chart = new google.visualization.ComboChart(document.getElementById('chart_div'));
            }else {
               chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
            }
            chart.draw(data, options);
        $('.chart-down').attr('href',chart.getImageURI());
            }";
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        //Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "", script, true);
        //chart_script.Text = "<script>"+script+"</script>";
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        _loadIndicators(ddlGoals.SelectedValue.ToParseInt());

        if (pnlIndicator.Style["display"] == "none")
        {
            pnlIndicator.Style["display"] = "block";
            ddlGoals.Items.RemoveAt(0);
        }
    }
}