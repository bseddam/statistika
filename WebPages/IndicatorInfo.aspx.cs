using DevExpress.Web.ASPxHtmlEditor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class WebPages_IndicatorInfo : System.Web.UI.Page
{
    DALC _db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {
        _loadFromViewState();
        if (IsPostBack)
        {
            return;
        }
        string lang = Config.getLang(Page);
        int indicatorid = Page.RouteData.Values["indicatorid"].ToParseInt();

        _loadGoalInfo(indicatorid, lang);
        _loadMetadata(indicatorid, lang);
        _loadLabels();


        _hide_empty_labels();

    }
    void _helper_hide_empty_label(Label value, Label label)
    {
        if (value.Text.Length == 0)
        {
            label.Visible = false;
        }
    }
    void _hide_empty_labels()
    {
        _helper_hide_empty_label(lblSize, lblSizeLabel);
        _helper_hide_empty_label(lblSource, lblSourceLabel);
        _helper_hide_empty_label(lblNote, lblNoteLabel);
    }

    private void _loadFromViewState()
    {
        if (ViewState["treelist"] != null)
        {
            treeList1.DataSource = ViewState["treelist"] as DataTable;
            treeList1.DataBind();
        }
    }

    void loadData(string lang, List<int> indicators)
    {
        loadChartMutipleIndicator(lang, indicators);
        loadTableMutipleIndicator(lang, indicators);
    }

    private void _loadLabels()
    {
        lblMetaInfo.Text = DALC.GetStaticValue("indicator_metadata_info");
        lblIndicatorName.Text = DALC.GetStaticValue("indicator_name");
        lblNationalMetadata.Text = DALC.GetStaticValue("indicator_national_metadata");
        lblGlobalMetadata.Text = DALC.GetStaticValue("indicator_global_metadata");
        indicator_national_metadata_info.Text = DALC.GetStaticValue("indicator_national_metadata_info");
        indicator_global_metadata_info.Text = DALC.GetStaticValue("indicator_global_metadata_info");
        lblSelectIndicator.Text = DALC.GetStaticValue("indicator_select_text");
        lblTabChart.Text = DALC.GetStaticValue("indicator_tab_chart");
        lblTabTable.Text = DALC.GetStaticValue("indicator_tab_table");

        //lblDownload.Text = DALC.GetStaticValue("download");

        lblSourceLabel.Text = lblSourceLabel1.Text = DALC.GetStaticValue("indicator_source");
        lblNoteLabel.Text = lblNoteLabel1.Text = DALC.GetStaticValue("indicator_note");
        lblSizeLabel.Text = DALC.GetStaticValue("indicator_olcu_vahidi");


        indicator_download_label.Text = DALC.GetStaticValue("indicator_download_label");
        indicator_download_jpg.Text = DALC.GetStaticValue("indicator_download_jpg");
        indicator_download_png.Text = DALC.GetStaticValue("indicator_download_png");
        indicator_download_print.Text = DALC.GetStaticValue("indicator_download_print");
    }

    public string getIndicatorName(string code, string name, int parentId)
    {
        if (parentId == 0)
        {
            return string.Format("{0} {1}", Config.ClearIndicatorCode(code), name);
        }
        else
        {
            return string.Format("{0} ", name);
        }
    }

    private void _loadMetadata(int indicatorid, string lang)
    {
        DataTable dtMetadata = _db.GetMetaDataClient(indicatorid, 0, lang);
        rptMetaData.DataSource = dtMetadata;
        rptMetaData.DataBind();

        for (int i = 0; i < dtMetadata.Rows.Count; i++)
        {
            DataTable dtMsub = _db.GetMetaDataClient(indicatorid, dtMetadata.Rows[i]["list_id"].ToParseInt(), lang);


            if (dtMsub.Rows.Count > 0)
            {
                _noM_Sub = 1;
                Repeater rptMetadataSub = rptMetaData.Items[i].FindControl("rptMetadataSub") as Repeater;
                rptMetadataSub.DataSource = dtMsub;
                rptMetadataSub.DataBind();
            }
        }
    }

    void _loadGlobalMetadata(int indicatorid, string lang)
    {
        lblGlobalMetadata.Visible = true;

        DataTable dtMetadata = _db.GetMetaDataClient_global(indicatorid, lang);
        rptGlobalMetada.DataSource = dtMetadata;
        rptGlobalMetada.DataBind();
    }

    private void _loadGoalInfo(int indicatorid, string lang)
    {
        string goal_value = DALC.GetStaticValue("goal_value");


        indicatorid = _db.indiqatoryoxla(indicatorid);
        //Response.Write(indicatorid);




        DataTable dtIndicator = _db.GetIndicatorById(indicatorid);
        

        
        if (dtIndicator == null || dtIndicator.Rows.Count < 1)
        {
            Config.Rd("/error?null");
            return;
        }
        DataTable dtGoal = _db.GetGoalById(dtIndicator.Rows[0]["goal_id"].ToParseInt());
        if (dtGoal.Rows.Count > 0)
        {
            lblGoalName.Text = dtGoal.Rows[0]["name_" + lang].ToParseStr();
            imgGoal.ImageUrl = "/images/goals-" + lang + "/goal-" + dtGoal.Rows[0]["id"].ToParseStr().PadLeft(2, '0') + ".png";
        }

        LtrIndicatorInfo.Text = dtIndicator.Rows[0]["info_" + lang].ToParseStr();
        lblIndicatorName_1.Text = dtIndicator.Rows[0]["name_" + lang].ToParseStr();
        //lblPageTitle.Text = DALC.GetStaticValue("slider_content_title");

        ltrBreadCrumb.Text = string.Format("<a href='/{0}/home'> {1}</a> / <a href='/{0}/goals/{2}/{3}/indicators'>{4}</a> / {5} {6}  ",
                     lang,
                     DALC.GetStaticValue("home_breadcrumb_title"),
                     dtGoal.Rows[0]["id"],
                     Config.Slug(dtGoal.Rows[0]["name_short_" + lang].ToParseStr()),
                     goal_value + " " + dtGoal.Rows[0]["priority"],
                     DALC.GetStaticValue("indicator_value"),
                     Config.ClearIndicatorCode(dtIndicator.Rows[0]["code"].ToParseStr()));

        Page.Title = Config.ClearIndicatorCode(dtIndicator.Rows[0]["code"].ToParseStr()) + " " + dtIndicator.Rows[0]["name_" + lang].ToParseStr();

        lblIndicatorTitle.Text = string.Format("{0} {1} {2}",
             DALC.GetStaticValue("indicator_value"),
             Config.ClearIndicatorCode(dtIndicator.Rows[0]["code"].ToParseStr()),
             dtIndicator.Rows[0]["name_" + lang].ToParseStr());
        if (lblIndicatorTitle.Text.Length > 500)
        {
            lblIndicatorTitle.Font.Size = 10;
        }
        if (400 < lblIndicatorTitle.Text.Length && lblIndicatorTitle.Text.Length < 500)
        {
            lblIndicatorTitle.Font.Size = 11;
        }
        if (300 < lblIndicatorTitle.Text.Length && lblIndicatorTitle.Text.Length < 400)
        {
            lblIndicatorTitle.Font.Size = 12;
        }
        if (200 < lblIndicatorTitle.Text.Length && lblIndicatorTitle.Text.Length < 300)
        {
            lblIndicatorTitle.Font.Size = 13;
        }
        if(200 > lblIndicatorTitle.Text.Length)
        {
            lblIndicatorTitle.Font.Size = 14;
        }


        _loadSubIndicators(indicatorid, lang, dtIndicator.Rows[0]["code"].ToParseStr(),
            dtIndicator.Rows[0]["goal_id"].ToParseInt(),
            dtIndicator.Rows[0]["name_" + lang].ToParseStr());



        lblNote.Text = lblNote1.Text = dtIndicator.Rows[0]["note_" + lang].ToParseStr();
        lblSource.Text = lblSource1.Text = dtIndicator.Rows[0]["source_" + lang].ToParseStr();
        lblSize.Text = _db.GetIndicatorSizeById(dtIndicator.Rows[0]["size_id"].ToParseInt()).Rows[0]["name_" + lang].ToParseStr();



        //milli olarsa global metadata olmasin
        if (dtIndicator.Rows[0]["type_code"].ToParseStr() != "1")
        {
            _loadGlobalMetadata(indicatorid, lang);
        }
        //keyfte olarsa diqarm gorunmesin
        if (dtIndicator.Rows[0]["size_code"].ToParseStr() == "35")
        {
            pnlDiaqramTable.Visible = false;
            pnlInfo.Visible = true;

            shareBox(lblIndicatorTitle.Text,
                    "indicator",
                    LtrIndicatorInfo.Text,
                    "");

        }
        else
        {
            pnlInfo.Visible = false;
            pnlDiaqramTable.Visible = true;
            loadData(lang, new List<int> { indicatorid });
        }
		if(lblGoalName.Text.Length > 235)
        {
            lblGoalName.Font.Size = 17;
        }
        if (210 < lblGoalName.Text.Length && lblGoalName.Text.Length < 235)
        {
            lblGoalName.Font.Size = 16;
        }



    }
    public int _noM = 0;
    public int _noM_Sub = 1;


    private void _loadSubIndicators(int indicatorid, string lang, string code, int goal_id, string indicatorName)
    {
        string _code = Config.ClearIndicatorCode(code);
        DataTable dt = _db.GetIndicatorsByParentId_2(indicatorid);
        DataTable dtFinal = new DataTable();
        dtFinal.Merge(dt);
        foreach (DataRow item in dt.Rows)
        {
            int id = item["id"].ToParseInt();
            if (id == indicatorid)
            {
                continue;
            }
            DataTable dtN = _db.GetIndicatorsByParentId_3(id);
            if (dtN == null)
            {
                continue;
            }
            dtFinal.Merge(dtN);
        }


        treeList1.DataSource = dtFinal;
        treeList1.DataBind();
        ViewState["treelist"] = dtFinal;
        if (dt.Rows.Count == 0)
        {
            pnlSubIndicator.Visible = false;
            pnlContent.CssClass = "col-md-12";
        }


    }


    string getCode(string code)
    {
        string[] arr = code.Split('.');


        return arr[arr.Length - 1];

    }


    void loadChartMutipleIndicator(string lang, List<int> indicators)
    {

        string data = "";
        DataTable dtYears;
        if (indicators.Count == 1)
        {
            dtYears = _db.GetHesabatforChart_Years_1(indicators[0]);
        }
        else
        {
            dtYears = _db.GetHesabat_Years();
        }

        string _years = "";
        foreach (DataRow item in dtYears.Rows)
        {
            _years += item["year"] + ",";
        }
        _years = _years.Trim(',');

        string _indicators = "";
        foreach (int item in indicators)
        {
            _indicators += item + ",";
        }
        _indicators = _indicators.Trim(',');



        string columns = "data.addColumn('string', 'Year');";


        DataTable dtH = _db.GetHesabat2(_indicators.Trim(','), _years.Trim(','), lang);
        if (dtH == null)
        {
            // pnlContent.Visible = false;
            return;
        }
        for (int i = 0; i < dtH.Rows.Count; i++)
        {
            columns += string.Format("data.addColumn('number', '{0}');", dtH.Rows[i]["IndicatorName"]);
            columns += "data.addColumn({type: 'string', role: 'tooltip','p': {'html': true}});";
        }

        data += columns.Trim(',');
        //data += "[" + columns.Trim(',') + "],";


        data += "data.addRows([";
        for (int i_year = 0; i_year < dtYears.Rows.Count; i_year++)
        {
            string values = "";
            string _tooltip = "";
            string value = "";
            foreach (int indicatorid in indicators)
            {
                DataTable dtIndicator = _db.GetIndicatorById(indicatorid);

                value = _db.GetHesabatforChart_Value(indicatorid, dtYears.Rows[i_year]["year"].ToParseInt());


                if (Config.IsNumeric_double(value))
                {
                    //value = "null";
                    //continue;

                    _tooltip = string.Format("<div>{5}: <b>{0}</b><br/><span>{1}:<b>{2}</b></span><br/>{4}<b>{3}</b></div>",
                            dtYears.Rows[i_year]["year"].ToParseInt(),
                            removeNewLine(dtIndicator.Rows[0]["name_" + lang].ToString()).Trim(),
                            removeNewLine(value.Replace('.', ',')),
                            removeNewLine(dtIndicator.Rows[0]["size_name_" + lang].ToString()).Trim(),
                            Config.HtmlRemoval.StripTagsRegex(DALC.GetStaticValue("indicator_olcu_vahidi")).Trim(),
                            Config.HtmlRemoval.StripTagsRegex(DALC.GetStaticValue("indicator_table_column_year")).Trim()
                            );

                    values += string.Format("{0},'{1}',", value, _tooltip);
                }
                else
                {
                    values += string.Format("null,' ',", value, _tooltip);
                    //values += "";
                }
            }

            if (values != "")
            {
                data += string.Format(" ['{0}',{1}],",
                   dtYears.Rows[i_year]["year"].ToParseInt(),
                   values.Trim(','));
            }



        }
        data = data.Trim(',');
        data += " ]);";


        _loadChart(data, dtYears.Rows.Count);
    }
    string removeNewLine(string s)
    {
        //return s.Replace(Environment.NewLine, "");
        return System.Text.RegularExpressions.Regex.Replace(s, @"\t|\n|\r", "");
    }
    void loadTableMutipleIndicator(string lang, List<int> indicators)
    {

        string data = "";

        string columns = "<td>" + DALC.GetStaticValue("indicator_table_column_year") + "</td>";
        for (int i = 0; i < indicators.Count; i++)
        {
            DataTable dtIndicator = _db.GetIndicatorById(indicators[i]);
            columns += string.Format("<td style='min-wdith:200px'>{0}</td>", dtIndicator.Rows[0]["name_" + lang]);
        }



        data += "<tr>" + columns + "</tr>";

        DataTable dtYears = _db.GetHesabat_Years();
        for (int i_year = 0; i_year < dtYears.Rows.Count; i_year++)
        {
            string values = "";
            foreach (int indicatorid in indicators)
            {
                string _value = _db.GetHesabatforChart_Value(indicatorid, dtYears.Rows[i_year]["year"].ToParseInt());
                values += string.Format("<td>{0}</td>", checkValue(_value));
            }
            data += string.Format("<tr><td>{0}</td>{1}</tr>",
                    dtYears.Rows[i_year]["year"].ToParseInt(),
                    values.Trim(','));
        }


        ltrTable.Text = " <table class='table table-bordered'>" + data + "</table>";

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

    void _loadChart(string data, int row_count)
    {
        string script = @"google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart(type) {
            var data = new google.visualization.DataTable();
               " + data + @"
            var options = 
            {
                tooltip: {isHtml: true},
                title: '',
                hAxis: {
                    format: ' ',
                    gridlines: 
                    {
                        count: " + row_count + @"
                    },
                },
                curveType: 'function',
                legend: 
                { 
                    position: 'bottom',
                    alignment: 'center' ,
                    maxLines: 5
                }, 
                pointSize: 5
            };

            var chart;
            if (type=='1') {
                 chart = new google.visualization.BarChart(document.getElementById('chart_div'));
            }
            else if (type=='2') {
                chart = new google.visualization.LineChart(document.getElementById('chart_div'));
            }
            else if (type=='3') {
                chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
            }
            else if (type=='4') {
                chart = new google.visualization.PieChart(document.getElementById('chart_div'));
            }else {
               chart = new google.visualization.LineChart(document.getElementById('chart_div'));
            }
            chart.draw(data, options);
        $('.chart-down-indicator').attr('href',chart.getImageURI());
            };";

        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "", script, true);
        //Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "", script, true);
        //chart_script.Text = "<script>"+script+"</script>";
    }
    protected void lnkSelectAll_Click(object sender, EventArgs e)
    {
        treeList1.SelectAll();

        btnIndicator_Click(null, null);
    }
    protected void lnkUnselectAll_Click(object sender, EventArgs e)
    {
        treeList1.UnselectAll();

        btnIndicator_Click(null, null);
    }
    protected void btnIndicator_Click(object sender, EventArgs e)
    {

        string lang = Config.getLang(Page);

        List<int> indicators = new List<int>();

        foreach (var item in treeList1.GetSelectedNodes())
        {
            if (item.Selected == true)
            {
                indicators.Add(item.Key.ToParseInt());
            }
        }

        loadData(lang, indicators);
    }

    private void shareBox(string pageTitle, string PageType, string Description, string image_url)
    {
        Description = HttpUtility.HtmlDecode(Config.HtmlRemoval.StripTagsRegex(Description));

        string pageUrl = Request.Url.ToString();
        share_text.Text = DALC.GetStaticValue("share_text");
        shareFb.NavigateUrl = "http://www.facebook.com/sharer.php?u=" + pageUrl;
        shareTwt.NavigateUrl = "https://twitter.com/share?url=" + pageUrl;
        shareMail.NavigateUrl = string.Format("mailto:?subject={0}&body={1}", pageTitle, pageUrl);
        shareLinkedin.NavigateUrl = string.Format("http://www.linkedin.com/shareArticle?mini=true&url={0}&title={1}&summary={2}&source={0}", pageUrl, pageTitle, Description);


        #region Google

        //Header.Controls.Add(new HtmlMeta
        //{
        //    Name = "keywords",
        //    Content = ""
        //});

        Header.Controls.Add(new HtmlMeta
        {
            Name = "description",
            Content = Description
        });

        #endregion

        #region Facebook

        Header.Controls.Add(new HtmlMeta
        {
            Name = "og:title",
            Content = pageTitle
        });

        Header.Controls.Add(new HtmlMeta
        {
            Name = "og:type",
            Content = PageType
        });

        Header.Controls.Add(new HtmlMeta
        {
            Name = "og:image",
            Content = image_url
        });
        Header.Controls.Add(new HtmlMeta
        {
            Name = "og:description",
            Content = Description
        });



        #endregion

        #region Twitter

        Header.Controls.Add(new HtmlMeta
        {
            Name = "twitter:card",
            Content = "summary"
        });

        Header.Controls.Add(new HtmlMeta
        {
            Name = "twitter:title",
            Content = pageTitle
        });

        Header.Controls.Add(new HtmlMeta
        {
            Name = "twitter:description",
            Content = Description
        });
        Header.Controls.Add(new HtmlMeta
        {
            Name = "twitter:image",
            Content = image_url
        });



        #endregion

    }
    protected void _exportExcel(string filename)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=" + filename + ".xlsx");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";

        Response.Write(getExportSource());
        Response.End();

    }
    protected void LnkExport_Click(object sender, EventArgs e)
    {
        string val = (sender as LinkButton).CommandArgument;
        string filename = DateTime.Now.Ticks.ToString();

        ASPxHtmlEditor editor = new ASPxHtmlEditor();
        editor.Html = getExportSource();
        switch (val)
        {
            case "xsl": _exportExcel(filename); break;
            case "doc":
                editor.Export(HtmlEditorExportFormat.Docx, string.Format("{0}.docx", filename));
                break;


            default:

                editor.Export(HtmlEditorExportFormat.Pdf, string.Format("{0}.pdf", filename));
                break;
        }
    }
    string getExportSource()
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        PnlExport.RenderControl(htmlWrite);

        return stringWrite.ToString();
    }
}

