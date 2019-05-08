using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxTreeList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Goals_Default : System.Web.UI.Page
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
        ltrPageTitle.Text = "Məqsədlər";

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
        DataTable dt = _db.GetGoals();


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




    protected void treeList_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {

        ASPxPageControl tabs = treeList.FindEditFormTemplateControl("tabs") as ASPxPageControl;

        ASPxHtmlEditor txtDesc_az = tabs.TabPages[1].FindControl("Description_az") as ASPxHtmlEditor;
        ASPxHtmlEditor txtDesc_en = tabs.TabPages[2].FindControl("Description_en") as ASPxHtmlEditor;

        ASPxHtmlEditor txtFacts_az = tabs.TabPages[3].FindControl("Facts_az") as ASPxHtmlEditor;
        ASPxHtmlEditor txtFacts_en = tabs.TabPages[4].FindControl("Facts_en") as ASPxHtmlEditor;

        ASPxHtmlEditor txtPriority_desc_az = tabs.TabPages[3].FindControl("priority_desc_az") as ASPxHtmlEditor;
        ASPxHtmlEditor txtPriority_desc_en = tabs.TabPages[4].FindControl("priority_desc_en") as ASPxHtmlEditor;

        Utils.MethodType val = _db.GoalUpdate(
                                  name_short_az: (e.NewValues["name_short_az"]).ToParseStr(),
                                  name_short_en: (e.NewValues["name_short_en"]).ToParseStr(),
                                  name_az: (e.NewValues["name_az"]).ToParseStr(),
                                  name_en: (e.NewValues["name_en"]).ToParseStr(),
                                  priority: (e.NewValues["priority"]).ToParseInt(),

                                  desc_az: txtDesc_az.Html.ToParseStr(),
                                  facts_az: txtFacts_az.Html.ToParseStr(),
                                  priority_desc_az: txtPriority_desc_az.Html.ToParseStr(),

                                  desc_en: txtDesc_en.Html.ToParseStr(),
                                  facts_en: txtFacts_en.Html.ToParseStr(),
                                   priority_desc_en: txtPriority_desc_en.Html.ToParseStr(),
                                  id: e.Keys["id"].ToParseInt()
                              );


        _loadGridFromDb();
        e.Cancel = true;
    }
    protected void treeList_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {
        ASPxPageControl tabs = treeList.FindEditFormTemplateControl("tabs") as ASPxPageControl;
        ASPxHtmlEditor txtDesc_az = tabs.TabPages[1].FindControl("Description_az") as ASPxHtmlEditor;
        ASPxHtmlEditor txtDesc_en = tabs.TabPages[2].FindControl("Description_en") as ASPxHtmlEditor;

        ASPxHtmlEditor txtFacts_az = tabs.TabPages[3].FindControl("Facts_az") as ASPxHtmlEditor;

        ASPxHtmlEditor txtFacts_en = tabs.TabPages[4].FindControl("Facts_en") as ASPxHtmlEditor;

        Utils.MethodType val = _db.GoalInsert(
                                  name_short_az: (e.NewValues["name_short_az"]).ToParseStr(),
                                  name_short_en: (e.NewValues["name_short_en"]).ToParseStr(),
                                  name_az: (e.NewValues["name_az"]).ToParseStr(),
                                  name_en: (e.NewValues["name_en"]).ToParseStr(),
                                  desc_az: txtDesc_az.Html.ToParseStr(),
                                  facts_az: txtFacts_az.Html.ToParseStr(),
                                  desc_en: txtDesc_en.Html.ToParseStr(),
                                  facts_en: txtFacts_en.Html.ToParseStr(),
                                  priority: (e.NewValues["priority"]).ToParseInt()
                              );
        _loadGridFromDb();
        e.Cancel = true;
    }
}