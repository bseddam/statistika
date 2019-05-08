using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class Admin_IndicatorReports_Add : System.Web.UI.Page
{


    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    DevExpress.Web.ASPxClasses.ASPxWebControl.GlobalTheme = "MetropolisBlue";
    //}

    DALC _db = new DALC();
    Utils.Tables _pageTable = Utils.Tables.static_values;
    const string _defaultDropdownRowText = "--Bütün məlumatlar--";
    const int startYear = 2015;

    protected void Page_Load(object sender, EventArgs e)
    {
        Config.isLogin(Page);
        ////  Config.checkPermission(_pageTable, Utils.LogType.select);


        if (IsPostBack) return;
        CheckValues();
        _loadGridFromDb();

        getIndicatorData();

        //_loadDropDowns();

        //_dataPermission();

        //_loadGridFromDb();
        // lnkSearch.Visible = false;
        PnlSearch.Visible = false;
    }

    private void getIndicatorData()
    {
        DataTable dt = _db.GetIndicatorById(_getIDFromQuery);

        if (dt == null)
        {
            Config.Rd("/user/error?dt-null");
            return;
        }

        if (dt.Rows.Count < 1)
        {
            Config.Rd("/user/error?dt-row-count");
            return;
        }

        //bool hasAccess = IndicatorValidDt(
        //                  dt.Rows[0]["teqdim_olunma_bas_gun"].ToParseInt(),
        //                  dt.Rows[0]["teqdim_olunma_bas_ay"].ToParseInt(),
        //                  dt.Rows[0]["teqdim_olunma_son_gun"].ToParseInt(),
        //                  dt.Rows[0]["teqdim_olunma_son_ay"].ToParseInt()
        //                 );


        //if (hasAccess == false)
        //{
        //    Config.Rd("/user/error?date-invalid");
        //    return;
        //}
        DataTable dtSize = _db.GetIndicatorSizeById(dt.Rows[0]["size_id"].ToParseInt());
        ltrSize.Text = "Ölçü vahidi: <b>" + dtSize.Rows[0]["name_az"].ToParseStr() + " </b>";
        ltrPageTitle.Text = string.Format("{0} {1}",
            Config.ClearIndicatorCode(dt.Rows[0]["code"].ToParseStr()),
            dt.Rows[0]["name_az"].ToParseStr());
        sizeCode.Value = dtSize.Rows[0]["code"].ToParseStr();
    }


    #region Methods

    int _getIDFromQuery
    {
        get
        {
            if (Request.QueryString.ToString().Length < 1)
            {
                Config.Rd("/user/error?id-is-null");
                return -1;
            }


            return Request.QueryString["id"].ToParseInt();
        }
    }

    void _loadGridFromDb()
    {
        DataTable dtHesabat = new DataTable();
        dtHesabat.Columns.Add("region_id", typeof(int));
        dtHesabat.Columns.Add("region_name", typeof(string));
        dtHesabat.Columns.Add("region_code", typeof(string));

        Grid.Columns.Clear();

        GridViewDataTextColumn column = new GridViewDataTextColumn();
        column.Caption = "id";
        column.FieldName = "region_id";
        column.EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False;
        column.Visible = false;
        Grid.Columns.Add(column);

        column = new GridViewDataTextColumn();
        column.Caption = "Region kodu";
        column.FieldName = "region_code";
        column.EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False;
        column.Width = 100;
        column.FixedStyle = GridViewColumnFixedStyle.Left;
        column.Visible = false;
        Grid.Columns.Add(column);

        column = new GridViewDataTextColumn();
        column.Caption = "İstinad zonası";
        column.FieldName = "region_name";
        column.EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False;
        column.Width = 250;
        column.FixedStyle = GridViewColumnFixedStyle.Left;
        Grid.Columns.Add(column);

        for (int i_year = startYear; i_year <= DateTime.Now.Year - 1; i_year++)
        {
            dtHesabat.Columns.Add(i_year.ToParseStr(), typeof(string));
            dtHesabat.Columns.Add("footnote_" + i_year.ToParseStr(), typeof(int));

            column = new GridViewDataTextColumn();

            column.Caption = i_year.ToParseStr();
            column.FieldName = i_year.ToParseStr();

            GridViewDataComboBoxColumn columnFootnote = new GridViewDataComboBoxColumn();
            columnFootnote.Caption = "Qeydlər " + i_year;
            columnFootnote.FieldName = "footnote_" + i_year;
            columnFootnote.Width = 100;
            column.EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
            columnFootnote.EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;

            //if (DALC.UserInfo.FullAccess)
            //{

            //}
            //else
            //{
            //    if (i_year == DateTime.Now.Year - 1)
            //    {
            //        column.EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;
            //        columnFootnote.EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.True;

            //    }
            //    else
            //    {
            //        column.CellStyle.BackColor = System.Drawing.Color.FromName("#e4e6e8");
            //        column.EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False;

            //        columnFootnote.CellStyle.BackColor = System.Drawing.Color.FromName("#e4e6e8");
            //        columnFootnote.EditFormSettings.Visible = DevExpress.Utils.DefaultBoolean.False;

            //    }
            //}

            Grid.Columns.Add(column);
            Grid.Columns.Add(columnFootnote);
        }
        DataTable dtRegion = _db.GetRegion();
        DataTable dtFootnotes = _db.GetFootnotes();
        for (int i = 0; i < dtRegion.Rows.Count; i++)
        {
            DataRow dr = dtHesabat.NewRow();

            dr["region_id"] = dtRegion.Rows[i]["id"].ToParseInt();
            dr["region_code"] = dtRegion.Rows[i]["code"].ToParseStr();
            dr["region_name"] = (dtRegion.Rows[i]["sub_id"].ToParseInt() == 0 ? "" : "--") + dtRegion.Rows[i]["name_az"].ToParseStr();
            for (int i_year = startYear; i_year <= DateTime.Now.Year - 1; i_year++)
            {
                DataTable dtHs = _db.GetHesabat1(_getIDFromQuery, i_year);

                DataView dvVal = dtHs.DefaultView;
                dvVal.RowFilter = string.Format("region_id={0} and year={1}", dtRegion.Rows[i]["id"].ToParseInt(), i_year);

                dr[i_year.ToParseStr()] = dvVal.Count > 0 ? dvVal[0]["value"] : "";
                dr["footnote_" + i_year] = dvVal.Count > 0 ? dvVal[0]["footnote_id"] : "";
                GridViewDataComboBoxColumn columnFootnote = Grid.Columns["footnote_" + i_year] as GridViewDataComboBoxColumn;
                columnFootnote.PropertiesComboBox.Items.Clear();
                foreach (DataRow item in dtFootnotes.Rows)
                {
                    columnFootnote.PropertiesComboBox.Items.Add(new DevExpress.Web.ASPxEditors.ListEditItem(item["name"].ToParseStr(), item["id"].ToParseInt()));
                }
                columnFootnote.PropertiesComboBox.Items.Insert(0, new DevExpress.Web.ASPxEditors.ListEditItem("", ""));


            }
            dtHesabat.Rows.Add(dr);
        }









        Grid.DataSource = dtHesabat;
        Grid.DataBind();
        ViewState["Grid"] = Grid.DataSource;
        // LtrFilterCount.Text = "Məlumat sayı: " + treeList.VisibleRowCount;
    }

    void _loadGridFromViewState()
    {
        if (ViewState["Grid"] != null)
        {
            Grid.DataSource = ViewState["Grid"] as System.Data.DataTable;
            Grid.DataBind();
        }
    }


    public bool IndicatorValidDt(int bas_gun, int bas_ay, int son_gun, int son_ay)
    {
        DateTime dtBegin = DateTime.Parse(string.Format("{0}.{1}.{2}", bas_ay, bas_gun, DateTime.Now.Year));
        DateTime dtFinish = DateTime.Parse(string.Format("{0}.{1}.{2}", son_ay, son_gun, DateTime.Now.Year));

        bool result = false;
        if (DateTime.Now >= dtBegin)
        {
            if (DateTime.Now <= dtFinish)
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }
        else
        {
            result = false;
        }


        return result;
    }

    void _export(string value)
    {
        gridExporter.FileName = DateTime.Now.ToString("ddMMyyyhhmm");
        switch (value)
        {
            case "ex_xls": gridExporter.WriteXlsToResponse(); break;
            case "ex_xlsx": gridExporter.WriteXlsxToResponse(); break;
            //case "ex_csv": gridExporter.WriteCsvToResponse(); break;
            case "ex_pdf": gridExporter.WritePdfToResponse(); break;
            case "ex_rtf": gridExporter.WriteRtfToResponse(); break;
        }
    }

    void _dataPermission()
    {
        //Utils.PermissionTables _data = _db.GetPermissionTable(_pageTable);
        //btnAdd.Visible = _data.isInsert;

        //if (_data.isUpdate)
        //{
        //    Grid.ClientSideEvents.RowDblClick = "function(s, e) {" + Page.ClientScript.GetPostBackClientHyperlink(btnEdit, "") + " ; }";
        //}
        //else
        //{
        //    Grid.Columns[10].Visible = false;
        //}
        //if (!_data.isDelete)
        //{
        //    Grid.Columns[11].Visible = false;
        //}
    }

    void CheckValues()
    {
        int indicatorId = _getIDFromQuery;
        DataTable dtRepeated = _db.GetIndicatorRepeatedById_2(indicatorId);
        string[] _arr = dtRepeated.Rows[0]["tekrarlanan_gosderici_kod"].ToParseStr().Split(',');
        string codes = "";
        foreach (string item in _arr)
        {
            codes += "'" + item.Trim() + "',";
        }
        codes = codes.Trim(',');

        if (codes.Length == 0)
        {
            return;
        }
        //tekrarlanan gosderici
        DataTable dt = _db.GetIndicatorByCodes(codes);

        for (int i_year = startYear; i_year <= DateTime.Now.Year - 1; i_year++)
        {
            int count = _db.CheckHesabatCount(indicatorId, i_year);
            if (count == 0)
            {
                _db.HesabatNullInsert(DALC.UserInfo.ID, indicatorId, i_year);
            }
        }

        for (int i_year = startYear; i_year <= DateTime.Now.Year - 1; i_year++)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int count = _db.CheckHesabatCount(dt.Rows[i]["id"].ToParseInt(), i_year);
                if (count == 0)
                {
                    _db.HesabatNullInsert(DALC.UserInfo.ID, dt.Rows[i]["id"].ToParseInt(), i_year);
                }
            }
        }
    }

    #endregion

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        lblError.Text = "";

        PnlSearch.Style["display"] = "block";
        _loadGridFromDb();
    }


    protected void Edit_Click(object sender, EventArgs e)
    {
        string pageNAme = "add";
        string typeId = Request.QueryString["typeid"].ToParseStr();
        Response.Redirect(pageNAme + ".aspx?id=" + (sender as LinkButton).CommandArgument, true);
    }
    protected void Grid_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
    {
        int curYear = DateTime.Now.Year - 1;
        int indicatorID = _getIDFromQuery;
        foreach (var item in e.UpdateValues)
        {
            var region_id = Convert.ToInt32(item.Keys["region_id"]);
            for (int i_year = startYear; i_year <= curYear; i_year++)
            {
                object yearValue = item.NewValues[i_year.ToString()];
                object footnote_id = item.NewValues["footnote_" + i_year.ToString()];
                HesabatUpdate(indicatorID, region_id, i_year, yearValue, footnote_id);
            }
        }

        _loadGridFromDb();

        e.Handled = true;
    }

    private void HesabatUpdate(int indicatorID, int region_id, int i_year, object yearValue, object footnote_id)
    {
        //esas gosderici
        _db.HesabatUpdate1(indicatorID, region_id, i_year, yearValue, footnote_id);

        DataTable dtRepeated = _db.GetIndicatorRepeatedById_2(indicatorID);
        string[] _arr = dtRepeated.Rows[0]["tekrarlanan_gosderici_kod"].ToParseStr().Split(',');
        string codes = "";
        foreach (string item in _arr)
        {
            codes += "'" + item.Trim() + "',";
        }
        codes = codes.Trim(',');
        //tekrarlanan gosderici
        DataTable dt = _db.GetIndicatorByCodes(codes);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            _db.HesabatUpdate1(dt.Rows[i]["id"].ToParseInt(), region_id, i_year, yearValue, footnote_id);
        }

    }

    protected void Grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        // string x = e.NewValues["2017"].ToParseStr();
    }
    protected void Grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        int curYear = DateTime.Now.Year - 1;
        if (e.NewValues[curYear.ToParseStr()].ToParseStr().Contains(","))
        {
            e.RowError = "Daxil olunan rəqəmlər vergüllə(,) ilə daxil oluna bilməz. Nöqtədən isdifadə edin";
        }


        if (e.HasErrors)
        {
            e.RowError = "Daxil olunan rəqəmlər vergüllə(,) ilə daxil oluna bilməz. Nöqtədən isdifadə edin";
        }
    }
    protected void Grid_ParseValue(object sender, DevExpress.Web.Data.ASPxParseValueEventArgs e)
    {
        // e.Value = e.Value.ToParseStr().Replace(".", ",");

    }
}