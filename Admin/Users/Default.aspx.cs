using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTreeList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Users_Default : System.Web.UI.Page
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
        ltrPageTitle.Text = "Qeydiyyatdan keçənlər";

        //_loadDropDowns();

        //_dataPermission();

        //_loadGridFromDb();


        lnkSearch.Visible = false;
        PnlSearch.Visible = true;
    }


    #region Methods
    void _loadDropDowns()
    {

        //TypeID.DataSource = _db.GetList(Utils.Tables.menu_type);
        //TypeID.DataBind();

        //TypeID.Items.Insert(0, new ListEditItem(_defaultDropdownRowText, "0"));
        //TypeID.SelectedIndex = 0;
        //if (DALC._selectedUniID.Length > 0)
        //{
        //    UniversityID.Value = DALC._selectedUniID;
        //}
        //else
        //{
        //    UniversityID.SelectedIndex = 0;
        //}
    }

    void _loadGridFromDb()
    {
        DataTable dt = _db.GetUsers(Status.Value.ToParseInt());

        GridViewDataComboBoxColumn column = treeList.Columns["org_id"] as GridViewDataComboBoxColumn;
        column.PropertiesComboBox.DataSource = _db.GetQurum();
        column.PropertiesComboBox.ValueField = "id";
        column.PropertiesComboBox.ValueType = typeof(int);
        column.PropertiesComboBox.TextField = "name_az";

        
      
        
        if (Status.Value.ToParseStr() == "0")
        {
            treeList.Visible = true;
            treeList2.Visible = false;
            treeList.DataSource = dt;
            treeList.DataBind();
            lnkColumn.OnClientClick = "treeList.ShowCustomizationWindow(); return false;";
        }
        else
        {
            treeList.Visible = false;
            treeList2.Visible = true;
            treeList2.DataSource = dt;
            treeList2.DataBind();
            lnkColumn.OnClientClick = "treeList2.ShowCustomizationWindow(); return false;";
        }
        ViewState["Grid"] = treeList.DataSource;
        // LtrFilterCount.Text = "Məlumat sayı: " + treeList.VisibleRowCount;
    }

    void _loadGridFromViewState()
    {
        if (ViewState["Grid"] != null)
        {
            treeList.DataSource = ViewState["Grid"] as System.Data.DataTable;
            treeList.DataBind();
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



    protected void LnkDelete_Click(object sender, EventArgs e)
    {
        //Config.checkPermission(_pageTable, Utils.LogType.delete);

        //int id = GridDiplomBlanks.GetRowValues(GridDiplomBlanks.FocusedRowIndex, GridDiplomBlanks.KeyFieldName).ToParseInt();
        //string _uniName = GridDiplomBlanks.GetRowValues(GridDiplomBlanks.FocusedRowIndex, "UniName").ToParseStr();

        //Utils.MethodType _val = _db.DiplomBlankDelete(_uniName, id);

        //if (_val == Utils.MethodType.Error)
        //{
        //    Config.ScriptAdd("Xəta ! Məlumatı yadda saxlamaq mümkün olmadı", Page);
        //    return;
        //}

        //_loadGridFromDb();
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        lblError.Text = "";



        PnlSearch.Style["display"] = "block";
        _loadGridFromDb();
    }
    protected void treeList_NodeUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {


        //if (e.NewValues["TypeId"] == null && e.NewValues["Name_az"] == null)
        //{
        //    Utils.MethodType val = _db.MenuClientUpdate(
        //                            priority: 0,

        //                            parentId: (e.NewValues["ParentId"] ?? e.OldValues["ParentId"]).ToParseInt(),

        //                            id: e.Keys["Id"].ToParseInt()
        //                        );
        //}
        //else
        //{
        //    Utils.MethodType val = _db.MenuClientUpdate(
        //                          url: (e.NewValues["URL"]).ToParseStr(),
        //                          typeid: (e.NewValues["TypeId"]).ToParseInt(),
        //                          parentId: (e.NewValues["ParentId"]).ToParseInt(),
        //                          name_az: (e.NewValues["Name_az"]).ToParseStr(),
        //                          name_en: (e.NewValues["Name_en"]).ToParseStr(),
        //                          id: e.Keys["Id"].ToParseInt()
        //                      );
        //}


        //treeList.StartEdit("");
        //_loadGridFromDb();
        //e.Cancel = true;
    }
    protected void treeList_NodeDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        //Utils.MethodType val = _db.MenuClientDelete(id: e.Keys["Id"].ToParseInt());

        //treeList.StartEdit("");
        //_loadGridFromDb();
        //e.Cancel = true;

    }
    protected void treeList_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        //Utils.MethodType val = _db.MenuClientInsert(
        //                          URL: e.NewValues["URL"].ToParseStr(),
        //                          typeid: e.NewValues["TypeId"].ToParseInt(),
        //                          parentid: e.NewValues["ParentId"].ToParseInt(),
        //                          Name_az: e.NewValues["Name_az"].ToParseStr(),
        //                          Name_en: e.NewValues["Name_en"].ToParseStr(),
        //                          Priority: 0
        //                      );
        //treeList.StartEdit("");
        //_loadGridFromDb();
        //e.Cancel = true;

    }

    protected void Status_SelectedIndexChanged(object sender, EventArgs e)
    {
        _loadGridFromDb();



    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        _db.UserUpdate((sender as LinkButton).CommandArgument.ToParseInt(), 1);

        _loadGridFromDb();
    }
    protected void btnFullAccess_Click(object sender, EventArgs e)
    {
        _db.UserUpdate_full_access((sender as LinkButton).CommandArgument.ToParseInt(), 1);
        _loadGridFromDb();
    }
    protected void BtnCancelFullAcess_Click(object sender, EventArgs e)
    {
        _db.UserUpdate_full_access((sender as LinkButton).CommandArgument.ToParseInt(), 0);
        _loadGridFromDb();
    }
    protected void treeList_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        _db.UserUpdate(id: e.Keys["id"].ToParseInt(),
                        org_id: e.NewValues["org_id"].ToParseInt(),
                        name: e.NewValues["name"].ToParseStr(),
                        surname: e.NewValues["surname"].ToParseStr(),
                        patronymic: e.NewValues["patronymic"].ToParseStr(),
                        position: e.NewValues["position"].ToParseStr(),
                        phone_mob: e.NewValues["phone_mob"].ToParseStr(),
                        phone_work: e.NewValues["phone_work"].ToParseStr(),
                        email: e.NewValues["email"].ToParseStr(),
                        pass: e.NewValues["password"].ToParseStr());

        treeList.StartEdit(-1);
        _loadGridFromDb();
        e.Cancel = true;
    }
    protected void treeList_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        Utils.MethodType val = _db.UserDelete(
                         id: e.Keys["id"].ToParseInt()
                     );
        treeList.StartEdit(-1);
        _loadGridFromDb();
        e.Cancel = true;
    }
}