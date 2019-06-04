using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxGridView;
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
        if (!IsPostBack)
        {
            pnlchart.Visible = true;
            datatable.Visible = false;
            lblclasschart.Text = "class='active'";
            lblclassdatatable.Text = "";
            hdncharttype.Value = "2";
            _hide_empty_labels();

        }
        _loadFromViewState();
        if (IsPostBack)
        {
            return;
        }
        string lang = Config.getLang(Page);
        _loadDropdowns(lang);

        int indicatorid = Page.RouteData.Values["indicatorid"].ToParseInt();

        _loadGoalInfo(indicatorid, lang);
        _loadMetadata(indicatorid, lang);
        _loadLabels();
        //btnIndicator_Click(null, null);

        shareBox(lblIndicatorTitle.Text,
                "indicator",
                LtrIndicatorInfo.Text,
                "");
    }
    void _helper_hide_empty_label(Label value, Label label)
    {
        if (value.Text.Length == 0 || value == null)
        {
            label.Visible = false;
        }
    }
    void _hide_empty_labels()
    {
        //_helper_hide_empty_label(lblSize, lblSizeLabel);
        _helper_hide_empty_label(lblSource, lblSourceLabel);
        _helper_hide_empty_label(lblNote, lblNoteLabel);
        _helper_hide_empty_label(lblFootNote, lblFootNoteLabel);
    }

    private void _loadFromViewState()
    {
        if (ViewState["treelist"] != null)
        {
            treeList1.DataSource = ViewState["treelist"] as DataTable;
            treeList1.DataBind();
        }
        if (ViewState["Grid"] != null)
        {
            Grid.DataSource = ViewState["Grid"] as DataTable;
            Grid.DataBind();
            //ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "script", " setTimeout(function(){$('.grid-cell').css('border-bottom-width', '');},100);", true);
        }
    }


    private void _loadLabels()
    {
        lblYearTitle.Text = DALC.GetStaticValue("statistical_database_year_title");
        lblMetaInfo.Text = DALC.GetStaticValue("indicator_metadata_info");
        lblIndicatorName.Text = DALC.GetStaticValue("indicator_name");
        lblNationalMetadata.Text = DALC.GetStaticValue("indicator_national_metadata");
        lblGlobalMetadata.Text = DALC.GetStaticValue("indicator_global_metadata");
        indicator_national_metadata_info.Text = DALC.GetStaticValue("indicator_national_metadata_info");
        indicator_global_metadata_info.Text = DALC.GetStaticValue("indicator_global_metadata_info");
        lblSelectIndicator.Text = DALC.GetStaticValue("indicator_select_text");
        lnkbTabChart.Text = DALC.GetStaticValue("indicator_tab_chart");
        lnkbTabTable.Text = DALC.GetStaticValue("indicator_tab_table");

        //lblDownload.Text = DALC.GetStaticValue("download");

        lblSourceLabel.Text = lblSourceLabel1.Text = DALC.GetStaticValue("indicator_source");
        lblNoteLabel.Text = lblNoteLabel1.Text = DALC.GetStaticValue("indicator_note");
        //lblSizeLabel.Text = DALC.GetStaticValue("indicator_olcu_vahidi");
        lblFootNoteLabel.Text = DALC.GetStaticValue("statistical_database_note");

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
        int a =0;
        int indicatoridnational = _db.indiqatoryoxla(indicatorid);
        DataTable dtMetadata = _db.GetMetaDataClient(indicatoridnational, 0, lang);

        rptMetaData.DataSource = dtMetadata;
        rptMetaData.DataBind();

        for (int i = 0; i < dtMetadata.Rows.Count; i++)
        {
            DataTable dtMsub = _db.GetMetaDataClient(indicatoridnational, dtMetadata.Rows[i]["list_id"].ToParseInt(), lang);
            for (int j = 0; j < dtMsub.Rows.Count; j++)
            {
                string aa = dtMsub.Rows[j]["name_" + lang].ToParseStr();
                if (dtMsub.Rows[j]["name_" + lang].ToParseStr() != "" && dtMsub.Rows[j]["name_" + lang]!=null)
                {
                    a++;
                }
            }
           
            if (dtMsub.Rows.Count > 0)
            {
                _noM_Sub = 1;
                Repeater rptMetadataSub = rptMetaData.Items[i].FindControl("rptMetadataSub") as Repeater;
                rptMetadataSub.DataSource = dtMsub;
                rptMetadataSub.DataBind();
            }
        }
        if(a>0)
        {
            pnlnationalmetadata.Visible = true;
            lblstyyeglobalmetadata.Text = "style='display:none'";
            lblglobalmetadataactive.Text = "class='active'";
            lblglobalmetadataactive1.Text = "";
        }
        else
        {
            pnlnationalmetadata.Visible = false;
            lblstyyeglobalmetadata.Text = "style=''";
            lblglobalmetadataactive.Text = "";
            lblglobalmetadataactive1.Text = "class='active'";
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

        int indicatoridnational = _db.indiqatoryoxla(indicatorid);
        //Response.Write(indicatorid);

        DataTable dtIndicator = _db.GetIndicatorById(indicatoridnational);

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
        else if (400 < lblIndicatorTitle.Text.Length && lblIndicatorTitle.Text.Length < 500)
        {
            lblIndicatorTitle.Font.Size = 11;
        }
        else if(300 < lblIndicatorTitle.Text.Length && lblIndicatorTitle.Text.Length < 400)
        {
            lblIndicatorTitle.Font.Size = 12;
        }
        else if(200 < lblIndicatorTitle.Text.Length && lblIndicatorTitle.Text.Length < 300)
        {
            lblIndicatorTitle.Font.Size = 13;
        }
        else if(200 > lblIndicatorTitle.Text.Length)
        {
            lblIndicatorTitle.Font.Size = 14;
        }



        _loadSubIndicators(indicatoridnational, lang, dtIndicator.Rows[0]["code"].ToParseStr(),
            dtIndicator.Rows[0]["goal_id"].ToParseInt(),
            dtIndicator.Rows[0]["name_" + lang].ToParseStr());



        lblNote.Text = lblNote1.Text = dtIndicator.Rows[0]["note_" + lang].ToParseStr();
        lblSource.Text = lblSource1.Text = dtIndicator.Rows[0]["source_" + lang].ToParseStr();
        //lblSize.Text = _db.GetIndicatorSizeById(dtIndicator.Rows[0]["size_id"].ToParseInt()).Rows[0]["name_" + lang].ToParseStr();



        //milli olarsa global metadata olmasin
        //if (dtIndicator.Rows[0]["type_code"].ToParseStr() != "1")
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


            DataTable dt = null;
            dt = _db.GetIndicatorsByParentId_2(indicatoridnational);
            DataTable dtN = null;
            foreach (DataRow item in dt.Rows)
            {
                int id = item["id"].ToParseInt();
                if (id == indicatoridnational)
                {
                    continue;
                }
                dtN = _db.GetIndicatorsByParentId_3(id);
                if (dtN == null)
                {
                    continue;
                }
            }

            if (dt.Rows.Count == 0 && dtN == null)
            {
                pnlDiaqramTable.Visible = false;
            }
            else
            {
                pnlDiaqramTable.Visible = true;
            }


            //Response.Write("aaa" + years[j] + years[j+1] + years[j + 2]);
            loadChartMutipleIndicator(lang, new List<int> { indicatoridnational },hdncharttype.Value);
        }
        if (lblGoalName.Text.Length > 235)
        {
            lblGoalName.Font.Size = 17;
        }
        if (210 < lblGoalName.Text.Length && lblGoalName.Text.Length < 235)
        {
            lblGoalName.Font.Size = 16;
        }
        if (lblGoalName.Text.Length > 250)
        {
            lblGoalName.Font.Size = 12;
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

        DataTable diagramsiz = _db.withoutdiagram(indicatorid);

        treeList1.DataSource = dtFinal;
        treeList1.DataBind();
        if (treeList1.Nodes.Count > 0 && diagramsiz.Rows.Count<1)
        {
            treeList1.Nodes[0].Selected = true;
        }
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


    void loadChartMutipleIndicator(string lang, List<int> indicators,string charttype)
    {
        int j = 0;
        int[] years = new int[10];
        for (int i = 0; i < chkYears.Items.Count; i++)
        {
            if (chkYears.Items[i].Selected == true)
            {
                years[j] = chkYears.Items[i].Text.ToParseInt();
                j++;
            }
        }

        string data = "";
        DataTable dtYears;
        if (indicators.Count == 1)
        {
            dtYears = _db.GetHesabatforChart_Years_1(indicators[0], years);
        }
        else
        {
            dtYears = _db.GetHesabat_Years(indicators, years);
        }
        if(dtYears==null)
        {
            return;
        }
        string _years = "";
        foreach (DataRow item in dtYears.Rows)
        {
            _years += item["year"] + ",";
        }
        _years = _years.Trim(',');




        DataTable dtindicators=new DataTable();
        if (indicators.Count == 1)
        {
            dtindicators = _db.GetHesabatforChart_Indicators_1(indicators[0], years);
        }
        else
        {
            dtindicators = _db.GetHesabat_Indicators(indicators, years);
        }
        if (dtindicators == null)
        {
            return;
        }







        string _indicators = "";
        foreach (DataRow item in dtindicators.Rows)
        {
            _indicators += item["indicator_id"] + ",";
        }
        _indicators = _indicators.Trim(',');


      

         


        string columns = "data.addColumn('string', 'Year');";
        DataTable dtH = null;
        if (_years.Trim(',') != "" && _years!=null && _indicators.Trim(',') != "" && _indicators != null)
        {
             dtH = _db.GetHesabat2(_indicators.Trim(','), _years, lang);
        }
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
            foreach (DataRow indicatorid in dtindicators.Rows)
            {
                DataTable dtIndicator = _db.GetIndicatorById(indicatorid[0].ToParseInt());

                value = _db.GetHesabatforChart_Value(indicatorid[0].ToParseInt(), dtYears.Rows[i_year]["year"].ToParseInt());

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

        _loadChart(data, dtYears.Rows.Count,charttype);
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


        //ltrTable.Text = " <table class='table table-bordered'>" + data + "</table>";



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

    void _loadChart(string data, int row_count, string type)
    {
        string script = @"google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);

        function drawChart() {
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
            if ("+ type + @"==1) {
                 chart = new google.visualization.BarChart(document.getElementById('chart_div'));
            }
            else if (" + type + @"==2) {

                chart = new google.visualization.LineChart(document.getElementById('chart_div'));
            }
            else if (" + type + @"==3) {
                chart = new google.visualization.ColumnChart(document.getElementById('chart_div'));
            }
            else if (" + type + @"==4) {
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
    protected void lnkchkSelectAll_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkYears.Items.Count; i++)
        {
            chkYears.Items[i].Selected=true;
        }
        btnIndicator_Click(null, null);
    }
    protected void lnkchkUnselectAll_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < chkYears.Items.Count; i++)
        {
            chkYears.Items[i].Selected = false;
        }
        btnIndicator_Click(null, null);
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

        if (pnlchart.Visible == true)
        {
            datatable.Visible = false;
            lblclasschart.Text = "class='active'";
            lblclassdatatable.Text = "";
            lblFootNoteLabel.Visible = false;
            lblFootNote.Visible = false;
            lblNote.Visible = true;
            lblNoteLabel.Visible = true;
            loadChartMutipleIndicator(lang, indicators, hdncharttype.Value);
            _hide_empty_labels();
        }
        else if(datatable.Visible == true)
        {
            pnlchart.Visible = false;
            lblclasschart.Text = "";
            lblclassdatatable.Text = "class='active'";
            lblFootNoteLabel.Visible = true;
            lblFootNote.Visible = true;
            lblNote.Visible = false;
            lblNoteLabel.Visible = false;
            cedvelgoster();
            _hide_empty_labels();
        }

       
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
        shareFb1.NavigateUrl = "http://www.facebook.com/sharer.php?u=" + pageUrl;
        shareTwt1.NavigateUrl = "https://twitter.com/share?url=" + pageUrl;
        shareMail1.NavigateUrl = string.Format("mailto:?subject={0}&body={1}", pageTitle, pageUrl);
        shareLinkedin1.NavigateUrl = string.Format("http://www.linkedin.com/shareArticle?mini=true&url={0}&title={1}&summary={2}&source={0}", pageUrl, pageTitle, Description);


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
    protected void LnkExport_Click1(object sender, EventArgs e)
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
    private void _loadDropdowns(string lang)
    {
        DataTable dtYears = _db.GetHesabat_Years();
        chkYears.DataSource = dtYears;
        chkYears.DataBind();
        for (int i = 0; i < chkYears.Items.Count; i++)
        {
            //if(chkYears.Items[i].Value!="2018")
            chkYears.Items[i].Selected = true;
        }
    }
    void cedvelgoster()
    {

        //lblError.Text = "";
        string lang = Config.getLang(Page);

        //int rowCount = 0;
        //for (int i_year = 0; i_year < chkYears.Items.Count; i_year++)
        //{
        //    if (chkYears.Items[i_year].Selected == true)
        //    {
        //        rowCount++;
        //    }
        //}
       


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
        //if (rowCount == 0)
        {
            //lblError.Text += DALC.GetStaticValue("statistical_database_year_not_selected") + "<br>";
        }
        if (indicator_count == 0)
        {
            //lblError.Text += DALC.GetStaticValue("statistical_database_indicator_not_selected") + "<br>";
        }
        //if (lblError.Text.Length > 0)
        //{
        //    return;
        //}
        //pnlResult.Visible = true;
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
        column.Width = 1000;
     
        Grid.Columns.Add(column);

        column = new GridViewDataTextColumn();
        column.Caption = DALC.GetStaticValue("statistical_database_grid_indicator_code");
        column.FieldName = "IndicatorCode_html";
        column.FixedStyle = GridViewColumnFixedStyle.Left;
        column.PropertiesEdit.EncodeHtml = false;
        column.CellStyle.HorizontalAlign = HorizontalAlign.Left;
        column.Width = 500;
       
        Grid.Columns.Add(column);

        column = new GridViewDataColumn();
        column.Caption = DALC.GetStaticValue("statistical_database_grid_indicator_size");
        column.FieldName = "IndicatorSize";
        column.CellStyle.HorizontalAlign = HorizontalAlign.Center;
        column.Width = 150;
     
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
        string footnote_title = DALC.GetStaticValue("statistical_database_footnote_title");

        Footnote_Id1 _footnote_id = new Footnote_Id1();
        if (_years!="" && treeList1.GetSelectedNodes().Count != 0)
        {
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




            DataTable dtH = null;
            if (_years.Trim(',') != "" && _years != null && _indicators.Trim(',') != "" && _indicators != null)
            {
                dtH = _db.GetHesabat21(_indicators.Trim(','), _years.Trim(','), lang);
                //Label1.Text = dtH.Rows.Count.ToParseStr();
            }
            int b = 0;
            if (dtH == null)
            {
                //pnlContent.Visible = false;
                return;
            }
            else
            {


            }


            int c = 0;
            for (int i = 0; i < dtH.Rows.Count; i++)
            {
                b++;
                DataRow dr = dtHesabat.NewRow();
                dr["id"] = dtH.Rows[i]["id"].ToParseInt();
                c = dtH.Rows[i]["id"].ToParseInt();
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
                    DataTable dtHs = null;
                    int kk = 0;
                    if (_years.Trim(',') != "" && _years != null)
                    {
                        kk = dtH.Rows[i]["indicator_id"].ToParseInt();
                        dtHs = _db.GetHesabat2_value(dtH.Rows[i]["indicator_id"].ToParseInt(), years[i_year]);
                    }

                    if (dtHs.Rows.Count == 0)
                    {
                        // pnlContent.Visible = false;
                        return;
                    }



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

                    //illeri burdan elave edir
                    dr[years[i_year].ToParseStr()] = checkValue(dtHs.Rows[0]["value"].ToParseStr()) + " " + footnote;
                    footnote_no++;

                }
                dtHesabat.Rows.Add(dr);
            }
            //Response.Write(dtHesabat.Rows[0][5].ToString());


            
        }
        else
        {
            dtHesabat = null;
        }
       

        Grid.DataSource = dtHesabat;
        Grid.DataBind();
        Grid.Columns["IndicatorCode_html"].Visible = true;
        Grid.Columns["IndicatorCode"].Visible = false;

        ViewState["Grid"] = Grid.DataSource;
        //Session["Grid"] = dtHesabat;

        _loadFootnotes(_footnote_id);
        if (Grid.VisibleRowCount < 5)
            Grid.Settings.VerticalScrollBarMode = ScrollBarMode.Hidden;
        else
            Grid.Settings.VerticalScrollBarMode = ScrollBarMode.Visible;

        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "script", " setTimeout(function(){$('.grid-cell').css('border-bottom-width', '');},100);", true);

    
    }
    private void _loadFootnotes(Footnote_Id1 footnote_id)
    {
        lblFootNote.Text = "";
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
            lblFootNote.Text += string.Format(@"<li><div id='footnote-{0}'></div>{0}. {1} </li>",
                                   item.Key,
                                  item.Value);
        }
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
    string getExportSource()
    {
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        PnlExport.RenderControl(htmlWrite);

        return stringWrite.ToString();
    }



    protected void chkYears_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnIndicator_Click(null, null);
    }

    protected void lnkbTabChart_Click(object sender, EventArgs e)
    {
        pnlchart.Visible = true;
        datatable.Visible = false;
        btnIndicator_Click(null, null);
       
    }

    protected void lnkbTabTable_Click(object sender, EventArgs e)
    {
        datatable.Visible = true;
        pnlchart.Visible = false;
        btnIndicator_Click(null, null);
       
    }

    protected void imgbchart1_Click(object sender, ImageClickEventArgs e)
    {
        hdncharttype.Value = "1";
        btnIndicator_Click(null, null);
    }
    protected void imgbchart2_Click(object sender, ImageClickEventArgs e)
    {
        hdncharttype.Value = "3";
        btnIndicator_Click(null, null);
    }
    protected void imgbchart3_Click(object sender, ImageClickEventArgs e)
    {

        hdncharttype.Value = "2";
        btnIndicator_Click(null, null);
    }




}

class Footnote_Id1
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

