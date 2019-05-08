using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Web.ASPxTabControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_List_Metadata_Global : System.Web.UI.Page
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
        ltrPageTitle.Text = "Qlobal metaməlumatlar";

        GoalList.SelectedIndex = 0;
        GoalList_SelectedIndexChanged(null, null);
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

    #region Methods


    void _loadGridFromDb()
    {
        if (Indicator.Value.ToParseInt() == 0)
        {
            return;
        }

        _db.MetadataUpdateAll_global(Indicator.Value.ToParseInt());

        #region
        DataTable dt = _db.GetMetaDataList_global();

        GridViewDataComboBoxColumn column = Grid.Columns["list_id"] as GridViewDataComboBoxColumn;
        column.PropertiesComboBox.Items.Clear();
        foreach (DataRow item in dt.Rows)
        {
            column.PropertiesComboBox.Items.Add(new DevExpress.Web.ASPxEditors.ListEditItem(item["name_az"].ToParseStr(), item["id"].ToParseInt()));
        }
        column.PropertiesComboBox.Items.Insert(0, new DevExpress.Web.ASPxEditors.ListEditItem(_defaultDropdownRowText1, ""));

        #endregion

        dt = _db.GetMetaData_global(Indicator.Value.ToParseInt());

        Grid.DataSource = dt;
        Grid.DataBind();

        ViewState["Grid"] = Grid.DataSource;

        // LtrFilterCount.Text = "Məlumat sayı: " + treeList.VisibleRowCount;
    }
    public int _no = 1;
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
                Grid.AddNewRow();
                //Config.Rd("/admin/pages/" + pageNAme + ".aspx?typeid=law");
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
        ASPxPageControl tabs = Grid.FindEditFormTemplateControl("tabs") as ASPxPageControl;

        ASPxHtmlEditor txtDesc_az = tabs.TabPages[0].FindControl("name_az") as ASPxHtmlEditor;
        ASPxHtmlEditor txtDesc_en = tabs.TabPages[1].FindControl("name_en") as ASPxHtmlEditor;

        Utils.MethodType val = _db.MetaDataUpdate_global(
                                        id: e.Keys["id"].ToParseInt(),
                                        indicator_id: Indicator.Value.ToParseInt(),
                                        list_id: e.NewValues["list_id"].ToParseInt(),
                                        name_az: txtDesc_az.Html,
                                        name_en: txtDesc_en.Html
                                 );
        Grid.StartEdit(-1);
        _loadGridFromDb();
        e.Cancel = true;
    }
    protected void Grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        Utils.MethodType val = _db.MetaDataDelete_global(id: e.Keys["id"].ToParseInt());
        Grid.StartEdit(-1);
        _loadGridFromDb();
        e.Cancel = true;
    }
    protected void Grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl tabs = Grid.FindEditFormTemplateControl("tabs") as ASPxPageControl;

        ASPxHtmlEditor txtDesc_az = tabs.TabPages[0].FindControl("name_az") as ASPxHtmlEditor;
        ASPxHtmlEditor txtDesc_en = tabs.TabPages[1].FindControl("name_en") as ASPxHtmlEditor;
        Utils.MethodType val = _db.MetaDataInsert_global(
                                indicator_id: Indicator.Value.ToParseInt(),
                                list_id: e.NewValues["list_id"].ToParseInt(),
                                name_az: txtDesc_az.Html,
                                name_en: txtDesc_en.Html
                               );
        Grid.StartEdit(-1);
        _loadGridFromDb();
        e.Cancel = true;
    }
    protected void GoalList_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dtGoals = _db.GetIndicatorsByParentId(GoalList.Value.ToParseInt(), 0, 1);
        Indicator.Items.Clear();
        foreach (DataRow row in dtGoals.Rows)
        {
            Indicator.Items.Add(string.Format("{0} {1}", Config.ClearIndicatorCode(row["code"].ToParseStr()), row["name_az"]), row["id"].ToParseStr());
        }
        Indicator.SelectedIndex = 0;
    }
}