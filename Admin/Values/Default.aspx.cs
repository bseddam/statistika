using DevExpress.Web.ASPxHtmlEditor;
using DevExpress.Web.ASPxTabControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Values_Default : System.Web.UI.Page
{
    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    DevExpress.Web.ASPxClasses.ASPxWebControl.GlobalTheme = "MetropolisBlue";
    //}

    DALC _db = new DALC();
    Utils.Tables _pageTable = Utils.Tables.static_values;
    const string _defaultDropdownRowText = "Bütün məlumatlar";


    protected void Page_Load(object sender, EventArgs e)
    {
        Config.isLogin(Page);
        ////  Config.checkPermission(_pageTable, Utils.LogType.select);
        _loadGridFromViewState();
        if (IsPostBack) return;

        lblFilter.Text = "Axtarış";
        ltrPageTitle.Text = "Mətnlər";

        _loadDropDowns();
        _loadGridFromDb();

        //_dataPermission();

        //_loadGridFromDb();


        lnkSearch.Visible = false;
        PnlSearch.Visible = true;
    }




    #region Methods
    private void _loadDropDowns()
    {
        Pages.DataSource = _db.GetStaticPages();
        Pages.DataBind();
        Pages.Items.Insert(0, new DevExpress.Web.ASPxEditors.ListEditItem(_defaultDropdownRowText, 0));
        Pages.SelectedIndex = 0;
        _loadGridFromDb();
    }
    void _loadGridFromDb()
    {
        DataTable dt;

        if (Pages.Value.ToParseInt() == 0)
        {
            dt = _db.GetStaticValues();
        }
        else
        {
            dt = _db.GetStaticValues(Pages.Value.ToParseInt());
        }

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



        PnlSearch.Style["display"] = "block";
        _loadGridFromDb();
    }

    protected void Grid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
    {
        ASPxPageControl tabs = Grid.FindEditFormTemplateControl("tabs") as ASPxPageControl;

        ASPxHtmlEditor txtDesc_az = tabs.TabPages[0].FindControl("name_az") as ASPxHtmlEditor;
        ASPxHtmlEditor txtDesc_en = tabs.TabPages[1].FindControl("name_en") as ASPxHtmlEditor;

        Utils.MethodType val = _db.StaticValueUpdate(
                             id: e.Keys["id"].ToParseInt(),
                             name_az: txtDesc_az.Html,
                             name_en: txtDesc_en.Html
                         );
        Grid.StartEdit(-1);
        _loadGridFromDb();
        e.Cancel = true;
    }
}