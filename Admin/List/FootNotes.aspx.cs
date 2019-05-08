using DevExpress.Web.ASPxTreeList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_List_FootNotes : System.Web.UI.Page
{
    DALC _db = new DALC();
    Utils.Tables _pageTable = Utils.Tables.menu_type;
    const string _defaultDropdownRowText = "--Bütün məlumatlar--";

    protected void Page_Load(object sender, EventArgs e)
    {
        Config.isLogin(Page);
        ////  Config.checkPermission(_pageTable, Utils.LogType.select);

        _loadGridFromDb();
        if (IsPostBack) return;

        lblFilter.Text = "Axtarış";
        ltrPageTitle.Text = "Qeydlər";


        lnkSearch.Visible = false;
        PnlSearch.Visible = false;
    }


    #region Methods

    void _loadGridFromDb()
    {
        DataTable dt = _db.GetFootnotes();
        Grid.DataSource = dt;
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
        _loadGridFromDb();
    }

    protected void Grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        Utils.MethodType val = _db.FootnoteUpdate(
                                  name: (e.NewValues["name"]).ToParseStr(),
                                  desc_az: (e.NewValues["desc_az"]).ToParseStr(),
                                  desc_en: (e.NewValues["desc_en"]).ToParseStr(),
                                  id: e.Keys["id"].ToParseInt()
                              );

        Grid.StartEdit(-1);
        _loadGridFromDb();
        e.Cancel = true;
    }
    protected void Grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        Utils.MethodType val = _db.FootnoteDelete(id: e.Keys["id"].ToParseInt());

        Grid.StartEdit(-1);
        _loadGridFromDb();
        e.Cancel = true;

    }
    protected void Grid_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

        Utils.MethodType val = _db.FootnoteInsert(
                                  name: (e.NewValues["name"]).ToParseStr(),
                                  desc_az: (e.NewValues["desc_az"]).ToParseStr(),
                                  desc_en: (e.NewValues["desc_en"]).ToParseStr(),
                                  user_id: DALC.UserInfo.ID,
                                  org_id: DALC.UserInfo.OrgId
                              );
        Grid.StartEdit(-1);
        _loadGridFromDb();
        e.Cancel = true;
    }
}