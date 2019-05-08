using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Indicators_Default : System.Web.UI.Page
{
    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    DevExpress.Web.ASPxClasses.ASPxWebControl.GlobalTheme = "MetropolisBlue";
    //}

    DALC _db = new DALC();
    Utils.Tables _pageTable = Utils.Tables.static_values;
    const string _defaultDropdownRowText = "--Bütün məlumatlar--";

    const string _defaultDropdownRowText1 = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Config.isLogin(Page);
        ////  Config.checkPermission(_pageTable, Utils.LogType.select);
        _loadDropDowns();
        _loadGridFromDb();
        if (IsPostBack) return;

        lblFilter.Text = "Axtarış";
        ltrPageTitle.Text = "Göstəricilər";


        GoalList.SelectedIndex = 0;

        //_dataPermission();

        //_loadGridFromDb();


        lnkSearch.Visible = false;
        PnlSearch.Visible = true;
        PnlContent.Visible = false;
    }

    private void _loadDropDowns()
    {
        GoalList.DataSource = _db.GetGoals();
        GoalList.DataBind();
    }

    void fillDropdown(ASPxComboBox combo, DataTable dt)
    {
        combo.ValueField = "id";
        if (combo == uygunluqList || combo == seviyeList)
        {
            combo.TextField = "name";
        }
        else
        {
            combo.TextField = "name_az";
        }


        combo.DataSource = dt;
        combo.DataBind();
        combo.Items.Insert(0, new DevExpress.Web.ASPxEditors.ListEditItem(_defaultDropdownRowText1, "0"));
    }

    #region Methods

    Utils.PageType getPageType
    {

        get
        {
            Utils.PageType _type = Utils.PageType.Laws;

            //switch (Request.QueryString["typeid"])
            //{
            //    case "news": _type = Utils.PageType.News; break;
            //    case "content": _type = Utils.PageType.Content; break;
            //    case "videos": _type = Utils.PageType.Videos; break;
            //    case "law": _type = Utils.PageType.Laws; break;

            //}
            return _type;
        }
    }
    void _loadGridFromDb()
    {

        #region
        DataTable dt = _db.GetIndicatorsType();


        GridViewDataComboBoxColumn column = Grid.Columns["type_id"] as GridViewDataComboBoxColumn;
        column.PropertiesComboBox.Items.Clear();
        foreach (DataRow item in dt.Rows)
        {
            column.PropertiesComboBox.Items.Add(new DevExpress.Web.ASPxEditors.ListEditItem(item["name_az"].ToParseStr(), item["id"].ToParseInt()));
        }
        column.PropertiesComboBox.Items.Insert(0, new DevExpress.Web.ASPxEditors.ListEditItem(_defaultDropdownRowText1, ""));

        fillDropdown(TypeList, dt);





        dt = _db.GetIndicators_status();
        column = Grid.Columns["status_id"] as GridViewDataComboBoxColumn;
        column.PropertiesComboBox.Items.Clear();
        foreach (DataRow item in dt.Rows)
        {
            column.PropertiesComboBox.Items.Add(new DevExpress.Web.ASPxEditors.ListEditItem(item["name_az"].ToParseStr(), item["id"].ToParseInt()));
        }
        column.PropertiesComboBox.Items.Insert(0, new DevExpress.Web.ASPxEditors.ListEditItem(_defaultDropdownRowText1, ""));
        fillDropdown(StatusList, dt);


        dt = _db.GetList("indicator_seviye");
        column = Grid.Columns["seviye"] as GridViewDataComboBoxColumn;
        column.PropertiesComboBox.Items.Clear();
        foreach (DataRow item in dt.Rows)
        {
            column.PropertiesComboBox.Items.Add(new DevExpress.Web.ASPxEditors.ListEditItem(item["name"].ToParseStr(), item["id"].ToParseInt()));
        }
        column.PropertiesComboBox.Items.Insert(0, new DevExpress.Web.ASPxEditors.ListEditItem(_defaultDropdownRowText1, ""));
        fillDropdown(seviyeList, dt);


        dt = _db.GetQurum();
        column = Grid.Columns["qurum_id"] as GridViewDataComboBoxColumn;
        column.PropertiesComboBox.Items.Clear();
        foreach (DataRow item in dt.Rows)
        {
            column.PropertiesComboBox.Items.Add(new DevExpress.Web.ASPxEditors.ListEditItem(item["name_az"].ToParseStr(), item["id"].ToParseInt()));
        }
        column.PropertiesComboBox.Items.Insert(0, new DevExpress.Web.ASPxEditors.ListEditItem(_defaultDropdownRowText1, ""));
        fillDropdown(qurumList, dt);


        dt = _db.GetIndicatorSize();
        column = Grid.Columns["size_id"] as GridViewDataComboBoxColumn;
        column.PropertiesComboBox.Items.Clear();
        foreach (DataRow item in dt.Rows)
        {
            column.PropertiesComboBox.Items.Add(new DevExpress.Web.ASPxEditors.ListEditItem(item["name_az"].ToParseStr(), item["id"].ToParseInt()));
        }
        column.PropertiesComboBox.Items.Insert(0, new DevExpress.Web.ASPxEditors.ListEditItem(_defaultDropdownRowText1, ""));
        fillDropdown(sizeList, dt);


        dt = _db.GetList("indicator_uygunluq");
        column = Grid.Columns["uygunluq_id"] as GridViewDataComboBoxColumn;
        column.PropertiesComboBox.Items.Clear();
        foreach (DataRow item in dt.Rows)
        {
            column.PropertiesComboBox.Items.Add(new DevExpress.Web.ASPxEditors.ListEditItem(item["name"].ToParseStr(), item["id"].ToParseInt()));
        }
        column.PropertiesComboBox.Items.Insert(0, new DevExpress.Web.ASPxEditors.ListEditItem(_defaultDropdownRowText1, ""));
        fillDropdown(uygunluqList, dt);
        #endregion


        dt = _db.GetIndicators(GoalList.Value.ToParseInt());

        Grid.DataSource = dt;
        Grid.DataBind();


        Parent_id.Items.Clear();
        foreach (DataRow item in dt.Rows)
        {
            Parent_id.Items.Add(new DevExpress.Web.ASPxEditors.ListEditItem(
                Config.ClearIndicatorCode(item["code"].ToParseStr()) + " . " + item["name_az"].ToParseStr(), item["id"].ToParseInt()));
        }
        Parent_id.Items.Insert(0, new DevExpress.Web.ASPxEditors.ListEditItem(_defaultDropdownRowText1, ""));


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

    #endregion

    protected void LnkPnlMenu_Click(object sender, EventArgs e)
    {
        LinkButton btn = sender as LinkButton;
        string pageNAme = "add";

        switch (btn.CommandArgument)
        {

            case "add":
                clearForm();
                btnSave.CommandName = "insert";
                popupEdit.ShowOnPageLoad = true;
                break;

            //case "search":
            //    PnlSearch.Visible = true;
            //    break;
            case "export":
                _loadGridFromViewState();
                _export(btn.CommandName);
                break;

        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        lblError.Text = "";



        PnlSearch.Style["display"] = "block";
        PnlContent.Visible = true; ;
        _loadGridFromDb();
    }

    protected void Grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        //Utils.MethodType val = _db.IndicatorsUpdate(
        //                                id: e.Keys["id"].ToParseInt(),
        //                                qurum_id: e.NewValues["qurum_id"].ToParseInt(),
        //                                size_id: e.NewValues["size_id"].ToParseInt(),
        //                                goal_id: GoalList.Value.ToParseInt(),
        //                                type_id: e.NewValues["type_id"].ToParseInt(),
        //                                uygunluq_id: e.NewValues["uygunluq_id"].ToParseInt(),
        //                                code: e.NewValues["code"].ToParseStr(),
        //                                tekrarlanan_gosderici_kod: e.NewValues["tekrarlanan_gosderici_kod"].ToParseStr(),
        //                                name_az: e.NewValues["name_az"].ToParseStr(),
        //                                name_en: e.NewValues["name_en"].ToParseStr(),
        //                                info_az: e.NewValues["info_az"].ToParseStr(),
        //                                info_en: e.NewValues["info_en"].ToParseStr(),
        //                                seviye: e.NewValues["seviye"].ToParseInt(),
        //                                teqdim_olunma_bas_gun: e.NewValues["teqdim_olunma_bas_gun"].ToParseInt(),
        //                                teqdim_olunma_bas_ay: e.NewValues["teqdim_olunma_bas_ay"].ToParseInt(),
        //                                teqdim_olunma_son_gun: e.NewValues["teqdim_olunma_son_gun"].ToParseInt(),
        //                                teqdim_olunma_son_ay: e.NewValues["teqdim_olunma_son_ay"].ToParseInt(),
        //                                status_id: e.NewValues["status_id"].ToParseInt(),
        //                                orderBy: e.NewValues["orderBy"].ToParseInt(),
        //                                note: e.NewValues["note"].ToParseStr(),
        //                                source: e.NewValues["source"].ToParseStr()
        //                         );
        Grid.StartEdit(-1);
        _loadGridFromDb();
        e.Cancel = true;
    }


    protected void Grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        Utils.MethodType val = _db.IndicatorsDelete(id: e.Keys["id"].ToParseInt());
        Grid.StartEdit(-1);
        _loadGridFromDb();
        e.Cancel = true;
    }
    protected void Grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //Utils.MethodType val = _db.IndicatorInsert(
        //                        qurum_id: e.NewValues["qurum_id"].ToParseInt(),
        //                        size_id: e.NewValues["size_id"].ToParseInt(),
        //                        goal_id: GoalList.Value.ToParseInt(),
        //                        type_id: e.NewValues["type_id"].ToParseInt(),
        //                        uygunluq_id: e.NewValues["uygunluq_id"].ToParseInt(),
        //                        code: e.NewValues["code"].ToParseStr(),
        //                        tekrarlanan_gosderici_kod: e.NewValues["tekrarlanan_gosderici_kod"].ToParseStr(),
        //                        name_az: e.NewValues["name_az"].ToParseStr(),
        //                        name_en: e.NewValues["name_en"].ToParseStr(),
        //                        info_az: e.NewValues["info_az"].ToParseStr(),
        //                        info_en: e.NewValues["info_en"].ToParseStr(),
        //                        seviye: e.NewValues["seviye"].ToParseInt(),
        //                        teqdim_olunma_bas_gun: e.NewValues["teqdim_olunma_bas_gun"].ToParseInt(),
        //                        teqdim_olunma_bas_ay: e.NewValues["teqdim_olunma_bas_ay"].ToParseInt(),
        //                        teqdim_olunma_son_gun: e.NewValues["teqdim_olunma_son_gun"].ToParseInt(),
        //                        teqdim_olunma_son_ay: e.NewValues["teqdim_olunma_son_ay"].ToParseInt(),
        //                        status_id: e.NewValues["status_id"].ToParseInt(),
        //                        orderBy: e.NewValues["orderBy"].ToParseInt(),
        //                        note: e.NewValues["note"].ToParseStr(),
        //                        source: e.NewValues["source"].ToParseStr()
        //                       );
        Grid.StartEdit(-1);
        _loadGridFromDb();
        e.Cancel = true;
    }
    protected void Grid_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
    {
        if (e.NewValues["type_id"].ToParseInt() == 0)
        {
            e.RowError = "Tipi seçilməyib";
            return;
        }
        if (e.NewValues["teqdim_olunma_bas_ay"].ToParseInt() == 0 || e.NewValues["teqdim_olunma_bas_gun"].ToParseInt() == 0)
        {
            e.RowError = "Ilkin tarix seçilməyib";
            return;
        }
        if (e.NewValues["teqdim_olunma_son_ay"].ToParseInt() == 0 || e.NewValues["teqdim_olunma_son_gun"].ToParseInt() == 0)
        {
            e.RowError = "Ilkin tarix seçilməyib";
            return;
        }
    }
    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        clearForm();

        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        DataTable dt = _db.GetIndicatorById(id);

        TypeList.Value = dt.Rows[0]["type_id"].ToParseStr();
        StatusList.Value = dt.Rows[0]["status_id"].ToParseStr();
        qurumList.Value = dt.Rows[0]["qurum_id"].ToParseStr();
        sizeList.Value = dt.Rows[0]["size_id"].ToParseStr();
        uygunluqList.Value = dt.Rows[0]["uygunluq_id"].ToParseStr();
        Parent_id.Value = dt.Rows[0]["parent_id"].ToParseStr();
        seviyeList.Value = dt.Rows[0]["seviye"].ToParseStr();

        Kod.Value = dt.Rows[0]["code"].ToParseStr();
        Name_az.Value = dt.Rows[0]["name_az"].ToParseStr();
        Name_en.Value = dt.Rows[0]["name_en"].ToParseStr();
        tekrarKod.Value = dt.Rows[0]["tekrarlanan_gosderici_kod"].ToParseStr();

        source_az.Html = dt.Rows[0]["source_az"].ToParseStr();
        source_en.Html = dt.Rows[0]["source_en"].ToParseStr();

        baslama_tarix_gun.Value = dt.Rows[0]["teqdim_olunma_bas_gun"].ToParseStr();
        baslama_tarix_ay.Value = dt.Rows[0]["teqdim_olunma_bas_ay"].ToParseStr();
        son_tarix_gun.Value = dt.Rows[0]["teqdim_olunma_son_gun"].ToParseStr();
        son_tarix_ay.Value = dt.Rows[0]["teqdim_olunma_son_ay"].ToParseStr();
        gunSayi.Value = dt.Rows[0]["elave_gun"].ToParseStr();

        info_az.Html = dt.Rows[0]["info_az"].ToParseStr();
        info_en.Html = dt.Rows[0]["info_en"].ToParseStr();

        note_az.Html = dt.Rows[0]["note_az"].ToParseStr();
        note_en.Html = dt.Rows[0]["note_en"].ToParseStr();

        btnSave.CommandName = "update";
        btnSave.CommandArgument = id.ToString();
        popupEdit.ShowOnPageLoad = true;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        lblPopError.Text = "";
        Utils.MethodType val = Utils.MethodType.Error;

        string _goal = Kod.Value.ToParseStr().Split('.')[0];


        if (_goal.ToParseInt() != GoalList.Value.ToParseInt())
        {
            lblPopError.Text = "Daxil olunan kod və məqsəd uyğun deyil";
            return;
        }


        if (btnSave.CommandName == "insert")
        {
            val = _db.IndicatorInsert(
                              qurum_id: qurumList.Value.ToParseInt(),
                              size_id: sizeList.Value.ToParseInt(),
                              goal_id: GoalList.Value.ToParseInt(),
                              type_id: TypeList.Value.ToParseInt(),
                              uygunluq_id: uygunluqList.Value.ToParseInt(),
                              code: Kod.Value.ToParseStr(),
                              tekrarlanan_gosderici_kod: tekrarKod.Value.ToParseStr(),
                              name_az: Name_az.Value.ToParseStr(),
                              name_en: Name_en.Value.ToParseStr(),
                              info_az: info_az.Html,
                              info_en: info_en.Html,
                              seviye: seviyeList.Value.ToParseInt(),
                              teqdim_olunma_bas_gun: baslama_tarix_gun.Value.ToParseInt(),
                              teqdim_olunma_bas_ay: baslama_tarix_ay.Value.ToParseInt(),
                              teqdim_olunma_son_gun: son_tarix_gun.Value.ToParseInt(),
                              teqdim_olunma_son_ay: son_tarix_ay.Value.ToParseInt(),
                              status_id: StatusList.Value.ToParseInt(),
                              note_az: note_az.Html,
                              source_az: source_az.Html,
                              note_en: note_en.Html,
                              source_en: source_en.Html,
                              elave_gun: gunSayi.Value.ToParseInt(),
                              parent_id: Parent_id.Value.ToParseInt()
                             );
        }
        else
        {
            val = _db.IndicatorsUpdate(
                                      id: btnSave.CommandArgument.ToParseInt(),
                                      qurum_id: qurumList.Value.ToParseInt(),
                                      size_id: sizeList.Value.ToParseInt(),
                                      goal_id: GoalList.Value.ToParseInt(),
                                      type_id: TypeList.Value.ToParseInt(),
                                      uygunluq_id: uygunluqList.Value.ToParseInt(),
                                      code: Kod.Value.ToParseStr(),
                                      tekrarlanan_gosderici_kod: tekrarKod.Value.ToParseStr(),
                                      name_az: Name_az.Value.ToParseStr(),
                                      name_en: Name_en.Value.ToParseStr(),
                                      info_az: info_az.Html,
                                      info_en: info_en.Html,
                                      seviye: seviyeList.Value.ToParseInt(),
                                      teqdim_olunma_bas_gun: baslama_tarix_gun.Value.ToParseInt(),
                                      teqdim_olunma_bas_ay: baslama_tarix_ay.Value.ToParseInt(),
                                      teqdim_olunma_son_gun: son_tarix_gun.Value.ToParseInt(),
                                      teqdim_olunma_son_ay: son_tarix_ay.Value.ToParseInt(),
                                      status_id: StatusList.Value.ToParseInt(),
                                      note_az: note_az.Html,
                                      source_az: source_az.Html,
                                      note_en: note_en.Html,
                                      source_en: source_en.Html,
                                      elave_gun: gunSayi.Value.ToParseInt(),
                                      parent_id: Parent_id.Value.ToParseInt()
                                );
        }

        if (val == Utils.MethodType.Error)
        {
            lblPopError.Text = "XƏTA! Yadda saxlamaq mümkün olmadı.";
            return;
        }

        _loadGridFromDb();
        popupEdit.ShowOnPageLoad = false;

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        popupEdit.ShowOnPageLoad = false;
    }


    void clearForm()
    {

        TypeList.Value =
        StatusList.Value =
        qurumList.Value =
        sizeList.Value =
        uygunluqList.Value =
        Parent_id.Value =
        seviyeList.Value = "";

        Kod.Value =
        Name_az.Value =
        Name_en.Value =
        tekrarKod.Value = "";

        source_az.Html = source_en.Html = "";

        baslama_tarix_gun.Value =
        baslama_tarix_ay.Value =
        son_tarix_gun.Value =
        gunSayi.Value =
        son_tarix_ay.Value = "";


    }
    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        int id = (sender as LinkButton).CommandArgument.ToParseInt();
        Utils.MethodType val = _db.IndicatorsDelete(id: id);
        _loadGridFromDb();
    }
}