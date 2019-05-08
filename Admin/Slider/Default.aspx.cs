using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Slider_Default : System.Web.UI.Page
{

    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    DevExpress.Web.ASPxClasses.ASPxWebControl.GlobalTheme = "MetropolisBlue";
    //}

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
        ltrPageTitle.Text = "Slider";

        //_loadDropDowns();

        //_dataPermission();

        //_loadGridFromDb();


        lnkSearch.Visible = false;
        PnlSearch.Visible = false;
    }


    #region Methods


    void _loadGridFromDb()
    {

        DataTable dtGoals = _db.GetGoals();

        GridViewDataComboBoxColumn column = Grid.Columns["goal_id"] as GridViewDataComboBoxColumn;
        column.PropertiesComboBox.Items.Clear();
        foreach (DataRow item in dtGoals.Rows)
        {
            column.PropertiesComboBox.Items.Add(new DevExpress.Web.ASPxEditors.ListEditItem("Məqsəd " + item["priority"].ToParseStr()+". " + item["name_short_az"].ToParseStr(), item["Id"].ToParseInt()));
        }
        column.PropertiesComboBox.Items.Insert(0, new DevExpress.Web.ASPxEditors.ListEditItem(_defaultDropdownRowText, ""));
        DataTable dt = _db.GetSlider();

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
                Config.Rd("/admin/slider/add.aspx");
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


    protected void Edit_Click(object sender, EventArgs e)
    {
        Response.Redirect("add.aspx?id=" + (sender as LinkButton).CommandArgument, true);
    }
    protected void delete_Click(object sender, EventArgs e)
    {
        _db.SliderDelete((sender as LinkButton).CommandArgument.ToParseInt());

        //_db.PageDelete(e.Keys["id"].ToParseInt());
        _loadGridFromDb();
        //e.Cancel = true;
    }
    protected void Grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

    }
}