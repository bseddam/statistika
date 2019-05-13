
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_List_Targets : System.Web.UI.Page
{

    DALC _db = new DALC();
    Utils.Tables _pageTable = Utils.Tables.static_values;
    const string _defaultDropdownRowText = "--Bütün məlumatlar--";


    protected void Page_Load(object sender, EventArgs e)
    {
        Config.isLogin(Page);
        ////  Config.checkPermission(_pageTable, Utils.LogType.select);

        _loadGridFromDb();
        if (IsPostBack) return;

        lblFilter.Text = "Axtarış";
        ltrPageTitle.Text = "Hədəflər";

        _loadDropDowns();

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
        DataTable dt = _db.GetTargetsFull1(GoalList.Value.ToParseInt());

        Grid.DataSource = dt;
        Grid.DataBind();
        ViewState["Grid"] = Grid.DataSource;
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
        switch (btn.CommandArgument)
        {
            case "add":
                Config.Rd("/diplomblanks/blankitem.aspx");
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



        _loadGridFromDb();

        PnlContent.Style["display"] = "block";
        PnlSearch.Style["display"] = "block";
        PnlContent.Visible = true;
    }

    protected void Grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        Utils.MethodType val = _db.TargetsUpdate1(
                             id: e.Keys["id"].ToParseInt(),
                             name_az: e.NewValues["name_az"].ToParseStr(),
                             name_en: e.NewValues["name_en"].ToParseStr(),
                             code: e.NewValues["code"].ToParseStr(),
                             goal_id: GoalList.Value.ToParseInt(),
                             milli_priotet: e.NewValues["milli_priotet"]
                         );
        Grid.StartEdit(-1);
        _loadGridFromDb();
        e.Cancel = true;
    }
    protected void Grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        Utils.MethodType val = _db.TargetsDelete1(id: e.Keys["id"].ToParseInt());
        Grid.StartEdit(-1);
        _loadGridFromDb();
        e.Cancel = true;
    }
    protected void Grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        Utils.MethodType val = _db.TargetsInsert1(
                             name_az: e.NewValues["name_az"].ToParseStr(),
                             name_en: e.NewValues["name_en"].ToParseStr(),
                             code: e.NewValues["code"].ToParseStr(),
                             goal_id: GoalList.Value.ToParseInt(),
                             milli_priotet: e.NewValues["milli_priotet"]
                         );
        Grid.StartEdit(-1);
        _loadGridFromDb();
        e.Cancel = true;
    }
}