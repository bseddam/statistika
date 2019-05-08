using DevExpress.Web.ASPxTreeList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Main_Default : System.Web.UI.Page
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
        ltrPageTitle.Text = "Menu";

        //_loadDropDowns();

        //_dataPermission();

        //_loadGridFromDb();


        lnkSearch.Visible = false;
        PnlSearch.Visible = false;
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
        DataTable dt = _db.GetMenuClient();

        TreeListComboBoxColumn column = treeList.Columns["ParentId"] as TreeListComboBoxColumn;
        column.PropertiesComboBox.Items.Clear();
        foreach (DataRow item in dt.Rows)
        {
            column.PropertiesComboBox.Items.Add(new DevExpress.Web.ASPxEditors.ListEditItem(item["Name_az"].ToParseStr(), item["Id"].ToParseInt()));
        }
        column.PropertiesComboBox.Items.Insert(0, new DevExpress.Web.ASPxEditors.ListEditItem("---", "0"));

        column = treeList.Columns["TypeId"] as TreeListComboBoxColumn;
        column.PropertiesComboBox.DataSource = _db.GetList(Utils.Tables.menu_type);
        column.PropertiesComboBox.ValueField = "ID";
        column.PropertiesComboBox.ValueType = typeof(int);
        column.PropertiesComboBox.TextField = "Name";

        treeList.DataSource = dt;
        treeList.DataBind();

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


        if (e.NewValues["TypeId"] == null && e.NewValues["Name_az"] == null)
        {
            Utils.MethodType val = _db.MenuClientUpdate(
                                    priority: (e.NewValues["Priority"]).ToParseInt(),
                                    parentId: (e.NewValues["ParentId"] ?? e.OldValues["ParentId"]).ToParseInt(),
                                    id: e.Keys["Id"].ToParseInt()
                                );
        }
        else
        {
            Utils.MethodType val = _db.MenuClientUpdate(
                                  priority: (e.NewValues["Priority"]).ToParseInt(),
                                  url: (e.NewValues["URL"]).ToParseStr(),
                                  typeid: (e.NewValues["TypeId"]).ToParseInt(),
                                  parentId: (e.NewValues["ParentId"]).ToParseInt(),
                                  name_az: (e.NewValues["Name_az"]).ToParseStr(),
                                  name_en: (e.NewValues["Name_en"]).ToParseStr(),
                                  id: e.Keys["Id"].ToParseInt()
                              );
        }


        treeList.StartEdit("");
        _loadGridFromDb();
        e.Cancel = true;
    }
    protected void treeList_NodeDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        Utils.MethodType val = _db.MenuClientDelete(id: e.Keys["Id"].ToParseInt());

        treeList.StartEdit("");
        _loadGridFromDb();
        e.Cancel = true;

    }
    protected void treeList_NodeInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        Utils.MethodType val = _db.MenuClientInsert(
                                  URL: e.NewValues["URL"].ToParseStr(),
                                  typeid: e.NewValues["TypeId"].ToParseInt(),
                                  parentid: e.NewValues["ParentId"].ToParseInt(),
                                  Name_az: e.NewValues["Name_az"].ToParseStr(),
                                  Name_en: e.NewValues["Name_en"].ToParseStr(),
                                  Priority: (e.NewValues["Priority"]).ToParseInt()
                              );
        treeList.StartEdit("");
        _loadGridFromDb();
        e.Cancel = true;

    }

}