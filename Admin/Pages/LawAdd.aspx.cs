using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Pages_LawAdd : System.Web.UI.Page
{
    DALC _db = new DALC();
    Utils.Tables _pageTable = Utils.Tables.pages;
    protected void Page_Load(object sender, EventArgs e)
    {

        Config.isLogin(Page);

        if (IsPostBack) return;



        // _loadDropDowns();

        LtrPageTitle.Text = "Yeni səhifə";

        int _dataID = _getIDFromQuery;

        if (_dataID > 0)
        {
            _loadDataByID(_dataID);

            LtrPageTitle.Text = "Səhifə məlumatlarını yenilə";
        }

        pnlVideo.Visible = (getPageType == Utils.PageType.Videos);

        //Config.checkPermission(_pageTable, Utils.LogType.insert);
        //btnBack.NavigateUrl = "/admin/pages/?typeid=" + Request.QueryString["typeid"];
        pnlDate.Visible = true;
        if (getPageType == Utils.PageType.SummaryGoals)
        {
            pnlDate.Visible = false;
        }


        // pnlDate.Visible = !(getPageType == Utils.PageType.About);

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

        DataTable dt = _db.GetPage1(DataID);

        if (dt == null && dt.Rows.Count < 1) return;

        title_az.Html = dt.Rows[0]["title_az"].ToParseStr();
        title_en.Html = dt.Rows[0]["title_en"].ToParseStr();

        Content_az.Html = dt.Rows[0]["content_az"].ToParseStr();
        Content_en.Html = dt.Rows[0]["content_en"].ToParseStr();

        txtVideoURL.Text = dt.Rows[0]["video_url"].ToParseStr();

        page_dt.Date = DateTime.Parse(dt.Rows[0]["page_dt"].ToParseStr());
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

    Utils.PageType getPageType
    {
        get
        {
            Utils.PageType _type = Utils.PageType.Content;
            switch (Request.QueryString["typeid"])
            {
                case "news": _type = Utils.PageType.News; break;
                case "content": _type = Utils.PageType.Content; break;
                case "videos": _type = Utils.PageType.Videos; break;
                case "law": _type = Utils.PageType.Laws; break;
                case "summarygoals": _type = Utils.PageType.SummaryGoals; break;

            }
            return _type;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _infoText = "";
        Utils.MethodType _type = Utils.MethodType.Error;

        #region validation

        if (title_az.Html.Trim().Length < 3)
        {
            _infoText += " - Başlıq qeyd olunmuyub <br/>";
        }
        //if (Content_az.Html.Trim().Length < 10)
        //{
        //    _infoText += " - Mətn qeyd olunmuyub <br/>";
        //}

        //if (page_dt.Value == null)
        //{
        //    _infoText += " - Tarix seçilməyib <br/>";
        //}


        if (Utils.PageType.Videos == getPageType)
        {
            if (txtVideoURL.Text.Trim().Length < 2)
            {
                _infoText += " - Video URL qeyd olunmuyub <br/>";
            }
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
            //returnVal = _db.PageUpdate(_getIDFromQuery, (int)getPageType,
            //  title_az.Html.Trim(),
            //  Config.Slug(title_az.Html.Trim()),
            //  title_en.Html.Trim(),
            //  Config.Slug(title_en.Html.Trim()),
            //  Content_az.Html,
            //  Content_en.Html,
            //  DateTime.Now,
            //  txtVideoURL.Text,0,"");
        }
        else
        {


            //returnVal = _db.PageInsert((int)getPageType,
            //    title_az.Html.Trim(),
            //    Config.Slug(title_az.Html.Trim()),
            //    title_en.Html.Trim(),
            //    Config.Slug(title_en.Html.Trim()),
            //    Content_az.Html,
            //    Content_en.Html,
            //      DateTime.Now,
            //    txtVideoURL.Text,0,"");

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