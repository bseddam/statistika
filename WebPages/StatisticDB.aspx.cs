using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTreeList;
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
        loadFromViewState();

        if (IsPostBack)
        {
            return;
        }
        string lang = Config.getLang(Page);

        _loadDropdowns(lang);

        Page.Title = ltrTitle.Text = DALC.GetStaticValue("statistical_database_page_title");
        ltrBreadCrumb.Text = string.Format("<a href='/{0}/home'> {1}</a> / {2}  ",
                     lang,
                     DALC.GetStaticValue("home_breadcrumb_title"),
                     ltrTitle.Text);


        _loadGoals(lang);

        lblGoalTitle.Text = DALC.GetStaticValue("statistical_database_goal_title");
        lblDesc.Text = DALC.GetStaticValue("statistical_database_desc");
        txtSearch.Attributes["placeholder"] = DALC.GetStaticValue("statistical_database_search_text");
        lblIndicatorTitle.Text = DALC.GetStaticValue("statistical_database_indicator_title");
        lblYearTitle.Text = DALC.GetStaticValue("statistical_database_year_title");

        BtnSearch.Text = DALC.GetStaticValue("statistical_database_search_btn");
        btnHesabat.Text = DALC.GetStaticValue("statistical_database_btn_hesabat");
        lblNote.Text = DALC.GetStaticValue("statistical_database_note");

        btnSelectAll.Text = "<i class='fa fa-check-circle-o'></i> " + DALC.GetStaticValue("statistical_database_select_all");
        btnUnselectAll.Text = "<i class='fa fa-times-circle-o'></i> " +  DALC.GetStaticValue("statistical_database_unselect_all");


        pnlResult.Visible = false;
        pnlIndicator.Style["display"] = "none";
    }
    protected void lnkchkSelectAll_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkYears.Items.Count; i++)
        {
            chkYears.Items[i].Selected = true;
        }
       
    }
    protected void lnkchkUnselectAll_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkYears.Items.Count; i++)
        {
            chkYears.Items[i].Selected = false;
        }
    
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
        ddlGoals.Items.Insert(0, new ListItem(DALC.GetStaticValue("statistical_database_goal_selector"), "-1"));
        ddlGoals.SelectedIndex = 0;
    }

    private void _loadDropdowns(string lang)
    {
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


    private void _loadFootnotes(Footnote_Id footnote_id)
    {
        footnote.Text = "";
        DataTable dtFootnote = _db.GetFootnotesOrderById();
        string lang = Config.getLang(Page);

        SortedDictionary<int, string> values = new SortedDictionary<int, string>();
        for (int i = 0; i < dtFootnote.Rows.Count; i++)
        {
            int _id = dtFootnote.Rows[i]["id"].ToParseInt();
            if (footnote_id.CheckKey(_id))
            {
                values.Add(footnote_id.GetValue(_id), dtFootnote.Rows[i]["desc_" + lang].ToParseStr());
            }
        }

        foreach (KeyValuePair<int, string> item in values)
        {
            footnote.Text += string.Format(@"<li><div id='footnote-{0}'></div>{0}. {1} </li>",
                                   item.Key,
                                  item.Value);
        }
    }
    private void _loadIndicators(int goalId)
    {
        string lang = Config.getLang(Page);
        //rptIndicators.DataTextField = "name_az";

        DataTable dt = _db.GetIndicators_withDesc(goalId, lang);

        treeList1.DataSource = dt;
        treeList1.DataBind();
        ViewState["treeList1"] = treeList1.DataSource;

        DataTable dtGoal = _db.GetGoalById(goalId);
        lblGoalName.Text = DALC.GetStaticValue("goal_value") + " " + dtGoal.Rows[0]["priority"] + " . " + dtGoal.Rows[0]["name_" + lang];

        imgGoal.ImageUrl = "/images/goals-" + lang + "/goal-" + goalId.ToString().PadLeft(2, '0') + ".png";



        //loadScriptForTooltip();

    }
    public void loadScriptForTooltip()
    {

        ScriptManager.RegisterClientScriptBlock(Page,
             Page.GetType(),
             "script",
             "$('[data-toggle=\"tooltip\"]').tooltip();",
             true);
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
    private void loadFromViewState()
    {

        if (ViewState["treeList1"] != null)
        {
            treeList1.DataSource = ViewState["treeList1"] as DataTable;
            treeList1.DataBind();
        }
        if (ViewState["Grid"] != null)
        {
            Grid.DataSource = ViewState["Grid"] as DataTable;
            Grid.DataBind();
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "script", " setTimeout(function(){$('.grid-cell').css('border-bottom-width', '');},100);", true);
        }
        //loadScriptForTooltip();
    }



    protected void btn_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        string lang = Config.getLang(Page);

        int rowCount = 0;
        for (int i_year = 0; i_year < chkYears.Items.Count; i_year++)
        {
            if (chkYears.Items[i_year].Selected == true)
            {
                rowCount++;
            }
        }


        int indicator_count = 0;
        string _indicators = "";
        for (int i = 0; i < treeList1.GetSelectedNodes().Count; i++)
        {
            if (treeList1.GetSelectedNodes()[i].Selected)
            {
                _indicators += treeList1.GetSelectedNodes()[i].GetValue("Id").ToParseStr() + ",";
                indicator_count++;
            }
        }
        if (rowCount == 0)
        {
            lblError.Text += DALC.GetStaticValue("statistical_database_year_not_selected") + "<br>";
        }
        if (indicator_count == 0)
        {
            lblError.Text += DALC.GetStaticValue("statistical_database_indicator_not_selected") + "<br>";
        }
        if (lblError.Text.Length > 0)
        {
            return;
        }
        pnlResult.Visible = true;
        Grid.Visible = true;


        DataTable dtHesabat = new DataTable();
        dtHesabat.Columns.Add("id", typeof(int));
        dtHesabat.Columns.Add("parent_id", typeof(int));
        dtHesabat.Columns.Add("IndicatorCode", typeof(string));
        dtHesabat.Columns.Add("IndicatorCode_html", typeof(string));
        dtHesabat.Columns.Add("IndicatorSize", typeof(string));


        Grid.Columns.Clear();

        GridViewDataColumn column = new GridViewDataColumn();

        column = new GridViewDataTextColumn();
        column.Caption = DALC.GetStaticValue("statistical_database_grid_indicator_code");
        column.FieldName = "IndicatorCode";
        column.FixedStyle = GridViewColumnFixedStyle.Left;
        column.PropertiesEdit.EncodeHtml = false;
        column.CellStyle.HorizontalAlign = HorizontalAlign.Left;
        column.Width = 700;
        column.VisibleIndex = 0;
        Grid.Columns.Add(column);

        column = new GridViewDataTextColumn();
        column.Caption = DALC.GetStaticValue("statistical_database_grid_indicator_code");
        column.FieldName = "IndicatorCode_html";
        column.FixedStyle = GridViewColumnFixedStyle.Left;
        column.PropertiesEdit.EncodeHtml = false;
        column.CellStyle.HorizontalAlign = HorizontalAlign.Left;
        column.Width = 700;
        column.VisibleIndex = 1;
        Grid.Columns.Add(column);

        column = new GridViewDataColumn();
        column.Caption = DALC.GetStaticValue("statistical_database_grid_indicator_size");
        column.FieldName = "IndicatorSize";
        column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
        column.Width = 150;
        column.VisibleIndex = 2;
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

            column = new GridViewDataTextColumn();
            column.Caption = years[i_year].ToParseStr();
            column.FieldName = years[i_year].ToParseStr();
            column.PropertiesEdit.EncodeHtml = false;
            column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
            Grid.Columns.Add(column);

            dtHesabat.Columns.Add("footnote_" + years[i_year].ToParseStr(), typeof(string));

            GridViewDataTextColumn columnF = new GridViewDataTextColumn();
            columnF.Caption = " ";
            columnF.FieldName = "footnote_" + years[i_year].ToParseStr();
            columnF.PropertiesEdit.EncodeHtml = false;
            columnF.Visible = false;
            Grid.Columns.Add(columnF);
        }

        DataTable dtH = _db.GetHesabat2(_indicators.Trim(','), _years.Trim(','), lang);
        string footnote_title = DALC.GetStaticValue("statistical_database_footnote_title");

        Footnote_Id _footnote_id = new Footnote_Id();

        for (int i = 0; i < dtH.Rows.Count; i++)
        {
            DataRow dr = dtHesabat.NewRow();
            dr["id"] = dtH.Rows[i]["id"].ToParseInt();
            int _parent_id = _db.GetIndicatorById(dtH.Rows[i]["indicator_id"].ToParseInt()).Rows[0]["parent_id"].ToParseInt();
            dr["parent_id"] = _parent_id;

            string __code = Config.ClearIndicatorCode(dtH.Rows[i]["IndicatorCode"].ToParseStr());

            if (__code.Split('.').Length == 3)
            {
                dr["IndicatorCode_html"] = string.Format("{0} {1}",
                __code,
                dtH.Rows[i]["IndicatorName"].ToParseStr());
            }
            else
            {
                int _count = parent_count(dtH.Rows[i]["indicator_id"].ToParseInt(), 0);
                dr["IndicatorCode_html"] = string.Format(" {0}",
                     generated_space_html(_count) + dtH.Rows[i]["IndicatorName"].ToParseStr());
            }

            if (__code.Split('.').Length == 3)
            {
                dr["IndicatorCode"] = string.Format("{0} {1}",
                __code,
                dtH.Rows[i]["IndicatorName"].ToParseStr());
            }
            else
            {
                int _count = parent_count(dtH.Rows[i]["indicator_id"].ToParseInt(), 0);
                dr["IndicatorCode"] = string.Format(" {0}",
                     generated_space(_count) + dtH.Rows[i]["IndicatorName"].ToParseStr());
            }
            dr["IndicatorSize"] = dtH.Rows[i]["IndicatorSize"].ToParseStr();
            int footnote_no = 1;
            for (int i_year = 0; i_year < years.Count; i_year++)
            {
                DataTable dtHs = _db.GetHesabat2_value(dtH.Rows[i]["indicator_id"].ToParseInt(), years[i_year]);


                //DataView dvVal = dtHs.DefaultView;
                //dvVal.RowFilter = string.Format("region_id={0} and year={1} and indicator_id={2}",
                //    dtH.Rows[i]["id"].ToParseInt(),
                //    years[i_year],
                //    dtH.Rows[i]["indicator_id"].ToParseInt()
                //    );



                string footnote = "";
                if (dtHs.Rows[0]["footnote_id"].ToParseStr() != "")
                {

                    _footnote_id.Add(dtHs.Rows[0]["footnote_id"].ToParseInt());

                    footnote = string.Format("<sup><a href='#footnote-{0}' title='{1}' data-id='{2}'>[{0}]</a></sup>",
                                _footnote_id.GetValue(dtHs.Rows[0]["footnote_id"].ToParseInt()),
                                footnote_title,
                                dtHs.Rows[0]["footnote_id"].ToParseStr());

                }



                dr["footnote_" + years[i_year].ToParseStr()] = footnote;

                dr[years[i_year].ToParseStr()] = checkValue(dtHs.Rows[0]["value"].ToParseStr()) + " " + footnote;
                footnote_no++;
            }
            dtHesabat.Rows.Add(dr);
        }



        Grid.DataSource = dtHesabat;
        Grid.DataBind();
        Grid.Columns["IndicatorCode_html"].Visible = true;
        Grid.Columns["IndicatorCode"].Visible = false;

        ViewState["Grid"] = Grid.DataSource;
        //Session["Grid"] = dtHesabat;

        _loadFootnotes(_footnote_id);

        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "script", " setTimeout(function(){$('.grid-cell').css('border-bottom-width', '');},100);", true);

    }
    int parent_count(int id, int count)
    {
        DataTable dt = _db.GetIndicatorById(id);
        string _parentId = dt.Rows[0]["parent_id"].ToParseStr();

        if (_parentId != "0")
        {
            count++;
            return parent_count(dt.Rows[0]["parent_id"].ToParseInt(), count);
        }
        return count;
    }
    string generated_space_html(int count)
    {

        string _result = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
        for (int i = 0; i < count; i++)
        {
            _result += "&nbsp;&nbsp;&nbsp;&nbsp;";
        }

        return _result;
    }

    string generated_space(int count)
    {

        string _result = "              ";
        for (int i = 0; i < count; i++)
        {
            _result += "     ";
        }

        return _result;
    }
    string checkValue(string value)
    {
        if (value == "0")
        {
            return "0,0";
        }
        if (value.Contains("..."))
        {
            return "...";
        }
        //if (value.Length == 0)
        //{
        //    return "-";
        //}

        return value.Replace('.', ',');

    }


    protected void txtSearch_TextChanged(object sender, EventArgs e)
    {
        //DataTable dt = Session["Grid"] as DataTable;
        //DataView dv = dt.DefaultView;

        //dv.RowFilter = string.Format("IndicatorCode like '%{0}%' or IndicatorSize like '%{0}%' ",
        //   txtSearch.Text.Trim());
        //Grid.DataSource = dv.ToTable();
        //Grid.DataBind();
        //ViewState["Grid"] = Grid.DataSource;

        Grid.FilterExpression =
            string.Format("IndicatorCode like '%{0}%' or IndicatorSize like '%{0}%' ",
            txtSearch.Text.Trim());
    }
    protected void LnkExport_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        Grid.Columns["IndicatorCode"].Visible = true;
        Grid.Columns["IndicatorCode_html"].Visible = false;

        gridExporter.FileName = DateTime.Now.ToString("ddMMyyyhhmm");
        switch (btn.CommandArgument)
        {
            case "exc": gridExporter.WriteXlsxToResponse(); break;
            case "csv": gridExporter.WriteCsvToResponse(); break;
            case "pdf": gridExporter.WritePdfToResponse(); break;
        }
        Grid.Columns["IndicatorCode"].Visible = false;
        Grid.Columns["IndicatorCode_html"].Visible = true;
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
    protected void btnSelectAll_Click(object sender, EventArgs e)
    {
        treeList1.SelectAll();
    }
    protected void btnUnselectAll_Click(object sender, EventArgs e)
    {
        treeList1.UnselectAll();
    }
   
}

class Footnote_Id
{
    Dictionary<int, int> footnote_ids = new Dictionary<int, int>();

    public void Add(int key)
    {
        if (!footnote_ids.ContainsKey(key))
        {
            int value = GetLastValue() + 1;
            footnote_ids.Add(key, value);
        }
    }
    public bool CheckKey(int key)
    {
        return footnote_ids.ContainsKey(key);
    }
    public int GetValue(int key)
    {
        return footnote_ids[key];
    }
    public Dictionary<int, int> GetList
    {
        get
        {
            return footnote_ids;
        }
    }

    private int GetLastValue()
    {
        return footnote_ids.LastOrDefault().Value;
    }
}