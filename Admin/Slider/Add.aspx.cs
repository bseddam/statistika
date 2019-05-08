using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_Slider_Add : System.Web.UI.Page
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



        _loadDropDowns();

        LtrPageTitle.Text = "Yeni səhifə";

        int _dataID = _getIDFromQuery;

        if (_dataID > 0)
        {
            _loadDataByID(_dataID);

            LtrPageTitle.Text = "Səhifə məlumatlarını yenilə";
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

        DataTable dt = _db.GetSlider(DataID);

        if (dt == null && dt.Rows.Count < 1) return;

        ddlGoals.SelectedValue = dt.Rows[0]["goal_id"].ToParseStr();
        title_az.Text = dt.Rows[0]["title_az"].ToParseStr();
        title_en.Text = dt.Rows[0]["title_en"].ToParseStr();

        Content_az.Html = dt.Rows[0]["content_az"].ToParseStr();
        Content_en.Html = dt.Rows[0]["content_en"].ToParseStr();

        chkShowHomeSlider.Checked = dt.Rows[0]["show_home_slider"].ToParseStr() == "1";
        btnSave.CommandName = dt.Rows[0]["image_url_az"].ToParseStr();
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

    private void _loadDropDowns()
    {
        ddlGoals.DataSource = _db.GetGoals();
        ddlGoals.DataBind();


    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _infoText = "";
        Utils.MethodType _type = Utils.MethodType.Error;

        string filename_az = "image-" + DateTime.Now.Ticks;
        string rootFolder = Server.MapPath("~/uploads/slider/");

        #region validation

        if (title_az.Text.Trim().Length < 3)
        {
            _infoText += " - Başlıq qeyd olunmuyub <br/>";
        }
        if (Content_az.Html.Trim().Length < 10)
        {
            _infoText += " - Mətn qeyd olunmuyub <br/>";
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
            returnVal = _db.SliderUpdate(_getIDFromQuery,
                ddlGoals.SelectedValue.ToParseInt(),
                title_az.Text.Trim(),
                title_en.Text.Trim(),
                Content_az.Html,
                Content_en.Html,
                filename_az,
                filename_az,
                chkShowHomeSlider.Checked);
        }
        else
        {
            returnVal = _db.SliderInsert(ddlGoals.SelectedValue.ToParseInt(),
                title_az.Text.Trim(),
                title_en.Text.Trim(),
                Content_az.Html,
                Content_en.Html,
                filename_az,
                filename_az,
                chkShowHomeSlider.Checked);

            int pageId = _db.getSliderId.ToParseInt();

            SubscribeContent content = new SubscribeContent();
            content.Id = pageId;
            content.Name = title_az.Text.Trim();
            content.Content = Content_az.Html;
            content.Type = Utils.PageType.Slider;

            SubscribeEmailSender emailSender = new SubscribeEmailSender(content);
            emailSender.Send();


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