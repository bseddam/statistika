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

        DataTable dt = _db.GetUsefullLinks(DataID);

        if (dt == null && dt.Rows.Count < 1) return;

        ddlheaderlink.SelectedValue = dt.Rows[0]["id1"].ToParseStr();


        useful_links_url_az.Text = dt.Rows[0]["useful_links_url_az"].ToParseStr();
        useful_links_url_en.Text = dt.Rows[0]["useful_links_url_en"].ToParseStr();

        useful_links_name_az.Html = dt.Rows[0]["useful_links_name_az"].ToParseStr();
        useful_links_name_en.Html = dt.Rows[0]["useful_links_name_en"].ToParseStr();
        OrderBy.Value = dt.Rows[0]["orderBy"].ToParseStr();
        //chkShowHomeSlider.Checked = dt.Rows[0]["show_home_slider"].ToParseStr() == "1";
        //btnSave.CommandName = dt.Rows[0]["image_url_az"].ToParseStr();
        //btnSave.CommandName = "update";
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
        ddlheaderlink.DataSource = _db.getlinkheader();
        ddlheaderlink.DataBind();


    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _infoText = "";
        Utils.MethodType _type = Utils.MethodType.Error;

     
       

        #region validation

        if (useful_links_url_az.Text.Trim().Length < 3)
        {
            _infoText += " - Başlıq qeyd olunmuyub <br/>";
        }
        if (useful_links_name_az.Html.Trim().Length < 3)
        {
            _infoText += " - Mətn qeyd olunmuyub <br/>";
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
            returnVal = _db.UsefullinkUpdate(_getIDFromQuery,
                ddlheaderlink.SelectedValue.ToParseInt(),
                useful_links_name_az.Html,
                useful_links_name_en.Html,
                useful_links_url_az.Text.Trim(),
                useful_links_url_en.Text.Trim(),
                OrderBy.Value.ToParseInt());
        }
        else
        {
            //returnVal = _db.SliderInsert(ddlheaderlink.SelectedValue.ToParseInt(),
            //    useful_links_name_az.Html,
            //    useful_links_name_en.Html,
            //    useful_links_url_az.Text.Trim(),
            //    useful_links_url_en.Text.Trim(),

            //    OrderBy.Value.ToParseInt());

            int pageId = _db.getSliderId.ToParseInt();

            SubscribeContent content = new SubscribeContent();
            content.Id = pageId;
            content.Name = useful_links_url_az.Text.Trim();
            content.Content = useful_links_name_az.Html;
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