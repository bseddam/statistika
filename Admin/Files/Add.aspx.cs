using DevExpress.Web.ASPxHtmlEditor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_Files_Add : System.Web.UI.Page
{
    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    DevExpress.Web.ASPxClasses.ASPxWebControl.GlobalTheme = "Moderno";
    //}

    DALC _db = new DALC();
    Utils.Tables _pageTable = Utils.Tables.pages;

    protected void Page_Load(object sender, EventArgs e)
    {

        Config.isLogin(Page);

        if (IsPostBack) return;


        LtrPageTitle.Text = "Yeni";

        int _dataID = _getIDFromQuery;

        if (_dataID > 0)
        {
            _loadDataByID(_dataID);

            LtrPageTitle.Text = "Məlumatları yenilə";
        }

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

        DataTable dt = _db.GetFile(DataID);

        if (dt == null || dt.Rows.Count < 1) return;

        txtname.Text = dt.Rows[0]["name"].ToParseStr();
        btnSave.CommandName = dt.Rows[0]["filename"].ToParseStr();


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

        if (txtname.Text.Trim().Length < 3)
        {
            _infoText += " - Ad qeyd olunmuyub <br/>";
        }
        if (btnSave.CommandArgument != "update")
        {
            if (fileUpload.HasFile == false)
            {
                _infoText += " - Fayl seçilməyib <br/>";
            }
        }


        #endregion

        if (_infoText.Length > 2)
        {
            _showPanel(_infoText, _type);
            return;
        }

        Utils.MethodType returnVal = Utils.MethodType.Error;

        string filename = "";

        if (fileUpload.HasFile)
        {
            filename = "file-" + DateTime.Now.Ticks + System.IO.Path.GetExtension(fileUpload.FileName);
            fileUpload.SaveAs(Server.MapPath("~/uploads/files/" + filename));
        }
        if (btnSave.CommandArgument == "update")
        {
            filename = btnSave.CommandName;

            returnVal = _db.FileUpdate(_getIDFromQuery, txtname.Text.Trim(), filename);

        }
        else
        {
            returnVal = _db.FileInsert(txtname.Text.Trim(), filename);
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