
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Pages_MechanismProgress : System.Web.UI.Page
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
        _loadGridFromViewState();
        if (IsPostBack) return;

        lblFilter.Text = "Axtarış";
        ltrPageTitle.Text = "Milli inkişaf gündəliyi";


        //_dataPermission();

        _loadDropDowns();
        _loadGridFromDb();


        lnkSearch.Visible = false;
        PnlSearch.Visible = false;
    }

    private void _loadDropDowns()
    {
        GoalList.DataSource = _db.GetGoals();
        GoalList.DataBind();
        
    }


    #region Methods

    Utils.PageType getPageType
    {

        get
        {
            Utils.PageType _type = Utils.PageType.MechanismGundelik;

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

        DataTable dt = _db.GetPages(getPageType);

        Grid.DataSource = dt;
        Grid.DataBind();



        ViewState["Grid"] = Grid.DataSource;

        // LtrFilterCount.Text = "Məlumat sayı: " + treeList.VisibleRowCount;
    }
    public string getPageURL(int typeId, int id)
    {
        string pageName = "";
        switch (typeId)
        {
            case (int)Utils.PageType.Videos:
                pageName = "video";
                break;
            case (int)Utils.PageType.Laws:
                pageName = "law";
                break;
            default:
                pageName = "page";
                break;
        }
        return string.Format("/{0}/{1}", pageName, id);
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
                Config.Rd("/admin/pages/" + pageNAme + ".aspx?typeid=mechanism_progress");
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


        pnlContent.Style["display"] = "block";
        PnlSearch.Style["display"] = "block";
        _loadGridFromDb();
    }

  
    protected void Edit_Click(object sender, EventArgs e)
    {
        string pageNAme = "add";


        Response.Redirect(pageNAme + ".aspx?id=" + (sender as LinkButton).CommandArgument + "&typeid=mechanism_progress", true);
    }
    protected void delete_Click(object sender, EventArgs e)
    {
        _db.PageDelete((sender as LinkButton).CommandArgument.ToParseInt());

        //_db.PageDelete(e.Keys["id"].ToParseInt());
        _loadGridFromDb();
        //e.Cancel = true;
    }
   
}