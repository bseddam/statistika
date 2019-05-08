using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_Partners_Add : System.Web.UI.Page
{
    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    DevExpress.Web.ASPxClasses.ASPxWebControl.GlobalTheme = "MetropolisBlue";
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


        //Config.checkPermission(_pageTable, Utils.LogType.insert);
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

        DataTable dt = _db.GetPartner(DataID);

        if (dt == null && dt.Rows.Count < 1) return;

        title_az.Text = dt.Rows[0]["title_az"].ToParseStr();
        title_en.Text = dt.Rows[0]["title_en"].ToParseStr();

        btnSave.CommandName = dt.Rows[0]["image_url"].ToParseStr();
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

        string filename_az = "image-" + DateTime.Now.Ticks;
        string rootFolder = Server.MapPath("~/uploads/partners/");

        #region validation

        if (title_az.Text.Trim().Length < 3)
        {
            _infoText += " - Başlıq qeyd olunmuyub <br/>";
        }
       
        if (fu_image_az.HasFile)
        {
            filename_az += Path.GetExtension(fu_image_az.FileName);
            fu_image_az.SaveAs(rootFolder + filename_az);
        }
        else
        {
            filename_az = btnSave.CommandName;
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
            returnVal = _db.PartnerUpdate(_getIDFromQuery,
                title_az.Text.Trim(),
                title_en.Text.Trim(),
                filename_az,
                Url.Text.Trim());
        }
        else
        {
            returnVal = _db.PartnerInsert(
                title_az.Text.Trim(),
                title_en.Text.Trim(),
                filename_az,
                Url.Text.Trim());
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