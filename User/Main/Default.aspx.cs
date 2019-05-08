using DevExpress.Web.ASPxGridView;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Main_Default : System.Web.UI.Page
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
        Config.isUserLogin(Page);
        ////  Config.checkPermission(_pageTable, Utils.LogType.select);

        _loadGridFromDb();
        if (IsPostBack) return;

        lblFilter.Text = "Axtarış";
        ltrPageTitle.Text = "Aidiyyatı göstəricilər üzrə hesabatlılıq";
        //_loadDropDowns();

        //_dataPermission();

        //_loadGridFromDb();


        lnkSearch.Visible = false;
        PnlSearch.Visible = false;
    }


    #region Methods


    void _loadGridFromDb()
    {




        DataTable dt = _db.GetIndicatorsByOrg_Year(DALC.UserInfo.OrgId, DateTime.Now.Year - 1);

        Grid.DataSource = dt;
        Grid.DataBind();
        Grid.SettingsPager.PageSize = 20;
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


    public string GetStr(int bas_gun, int bas_ay, int son_gun, int son_ay, int elaveGun, int id)
    {
        DateTime dtBegin = DateTime.Parse(string.Format("{0}.{1}.{2}", bas_ay, bas_gun, DateTime.Now.Year));
        DateTime dtFinish = DateTime.Parse(string.Format("{0}.{1}.{2}", son_ay, son_gun, DateTime.Now.Year));


        dtFinish = dtFinish.AddDays(elaveGun);

        string result = "Məlumat daxil olunması mümkün deyil ";
        if (DateTime.Now >= dtBegin)
        {
            if (DateTime.Now <= dtFinish)
            {
                result = string.Format("<a href='add.aspx?id={0}'>Məlumat daxil et</a>", id);
            }
            else
            {
                result = "Məlumat daxil olunması vaxtı bitib";
            }
        }
        else
        {
            result = "Məlumat daxil olunması aktiv deyil";
        }


        return result;
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
                Config.Rd("/user/main/add.aspx");
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
        string pageNAme = "add";
        string typeId = Request.QueryString["typeid"].ToParseStr();
        Response.Redirect(pageNAme + ".aspx?id=" + (sender as LinkButton).CommandArgument, true);
    }
}