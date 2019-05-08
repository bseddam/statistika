using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_Pages_Default : System.Web.UI.Page
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
       

        switch (Config.getPageType)
        {
            case Utils.PageType.News:
                ltrPageTitle.Text = "Xəbərlər";
                break;
            case Utils.PageType.Videos:
                ltrPageTitle.Text = "Videolar";
                break;
            case Utils.PageType.Content:
                ltrPageTitle.Text = "Kontent";
                break;
            case Utils.PageType.Laws:
                ltrPageTitle.Text = "Qanunlar";
                break;
            case Utils.PageType.SummaryGoals:
                ltrPageTitle.Text = "Qısa xülasə";
                break;
            case Utils.PageType.About:
                ltrPageTitle.Text = "Haqqımızda";
                break;
            case Utils.PageType.International:
                ltrPageTitle.Text = "Beynəlxalq çağırışlar";
                break;
            case Utils.PageType.Konstitusiya:
                ltrPageTitle.Text = "Konstitusiyadan çıxarışlar";
                break;
            case Utils.PageType.Order:
                ltrPageTitle.Text = "Fərmanlar";
                break;
            case Utils.PageType.MechanismOtherPages:
                ltrPageTitle.Text = "Digər səhifələr";
                break;

            default:
                break;
        }
        //_loadDropDowns();

        //_dataPermission();

        //_loadGridFromDb();


        lnkSearch.Visible = false;
        PnlSearch.Visible = false;
    }


    #region Methods

   
    void _loadGridFromDb()
    {

        DataTable dt = _db.GetPages(Config.getPageType);

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
        if (Config.getPageType == Utils.PageType.Laws)
        {
            pageNAme = "lawadd";
        }
        switch (btn.CommandArgument)
        {

            case "add":
                Config.Rd("/admin/pages/" + pageNAme + ".aspx?typeid=" + Request.QueryString["typeid"].ToParseStr());
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
        Utils.MethodType val = _db.StaticValueUpdate(
                             id: e.Keys["id"].ToParseInt(),
                             name_az: e.NewValues["name_az"].ToParseStr(),
                             name_en: e.NewValues["name_en"].ToParseStr()
                         );
        Grid.StartEdit(-1);
        _loadGridFromDb();
        e.Cancel = true;
    }
    protected void Edit_Click(object sender, EventArgs e)
    {
        string pageNAme = "add";
        if (Config.getPageType == Utils.PageType.Laws)
        {
            pageNAme = "lawadd";
        }

        string typeId = Request.QueryString["typeid"].ToParseStr();
        Response.Redirect(pageNAme + ".aspx?id=" + (sender as LinkButton).CommandArgument + "&typeid=" + typeId, true);
    }
    protected void delete_Click(object sender, EventArgs e)
    {
        _db.PageDelete((sender as LinkButton).CommandArgument.ToParseInt());

        //_db.PageDelete(e.Keys["id"].ToParseInt());
        _loadGridFromDb();
        //e.Cancel = true;
    }
    protected void Grid_RowDeleting(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {

    }
}