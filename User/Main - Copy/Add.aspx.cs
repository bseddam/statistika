using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Main_Add : System.Web.UI.Page
{
    DALC _db = new DALC();
    Utils.Tables _pageTable = Utils.Tables.pages;
    protected void Page_Load(object sender, EventArgs e)
    {
        Config.isUserLogin(Page);

        if (IsPostBack) return;

        _loadDropDowns();

        LtrPageTitle.Text = "Yeni ";
        lblyear.Text = DateTime.Now.Year.ToParseStr(); ;

        int _dataID = _getIDFromQuery;
        if (_dataID > 0)
        {
            _loadDataByID(_dataID);

            LtrPageTitle.Text = "Məlumatları yenilə";
        }



    }

    private void _loadDropDowns()
    {
        Region.DataSource = _db.GetRegion();
        Region.DataBind();

        Indicator.DataSource = _db.GetIndicatorsByQurum(DALC.UserInfo.OrgId);
        Indicator.DataBind();
    }

    int _getIDFromQuery
    {
        get
        {
            if (Request.QueryString.ToString().Length < 1)
            {
                //Config.Rd("/error");
                return -1;
            }


            return Request.QueryString["id"].ToParseInt();
        }
    }



    private void _loadDataByID(int DataID)
    {
        // Config.checkPermission(_pageTable, Utils.LogType.update);

        DataTable dt = _db.GetHesabatById(DataID);

        if (dt == null && dt.Rows.Count < 1) return;

        Indicator.Value = dt.Rows[0]["indicator_id"].ToParseStr();
        Region.Value = dt.Rows[0]["region_id"].ToParseStr();

        txtValue.Value = dt.Rows[0]["value"].ToParseStr();

        btnSave.CommandArgument = "update";

        // ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "aa", "setTimeout(_calculate_diplomCount(), 500);", true);


    }
    private void _showPanel(string text, Utils.MethodType _type)
    {
        string _css = "";

        switch (_type)
        {
            case Utils.MethodType.Succes:
                _css = "alert alert-success";
                break;
            case Utils.MethodType.Error:
                _css = "alert alert-danger";
                break;
        }

        PnlInfo.CssClass = _css;
        LblInfo.Text = text;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _infoText = "";
        Utils.MethodType _type = Utils.MethodType.Error;

        #region validation

        if (Region.Value == null)
        {
            _infoText += " - Region seçilməyib <br/>";
        }
        if (Indicator.Value == null)
        {
            _infoText += " - Göstərici seçilməyib <br/>";
        }

        if (txtValue.Value == null)
        {
            _infoText += " - Dəyər  qeyd olunmuyub <br/>";
        }



        #endregion

        if (_infoText.Length > 2)
        {
            _showPanel(_infoText, _type);
            return;
        }

        Utils.MethodType returnVal = Utils.MethodType.Error;

        if (btnSave.CommandArgument == "update")
        {
            returnVal = _db.HesabatUpdate(_getIDFromQuery,
                DALC.UserInfo.ID,
                                        Indicator.Value.ToParseInt(),
                                        Region.Value.ToParseInt(),
                                        DateTime.Now.Year,
                                        txtValue.Value);
        }
        else
        {
            returnVal = _db.HesabatInsert(DALC.UserInfo.ID,
                                        Indicator.Value.ToParseInt(),
                                        Region.Value.ToParseInt(),
                                        DateTime.Now.Year,
                                        txtValue.Value);

        }





        if (returnVal == Utils.MethodType.Succes)
        {
            _infoText = "Məlumat yadda saxlanıldı";
        }
        else
        {
            _infoText = "XƏTA ! Məlumatı yadda saxlamaq mümkün olmadı";
        }


        _showPanel(_infoText, returnVal);

    }
}