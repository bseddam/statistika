using DevExpress.Web.ASPxHtmlEditor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Pages_Add : System.Web.UI.Page
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

        Utils.PageType pageType = Config.getPageType;

        _loadDropDowns();

        LtrPageTitle.Text = "Yeni səhifə";

        int _dataID = _getIDFromQuery;

        if (_dataID > 0)
        {
            _loadDataByID(_dataID);

            LtrPageTitle.Text = "Səhifə məlumatlarını yenilə";
        }

        pnlVideo.Visible = (pageType == Utils.PageType.Videos);

        //Config.checkPermission(_pageTable, Utils.LogType.insert);
        //btnBack.NavigateUrl = "/admin/pages/?typeid=" + Request.QueryString["typeid"];
        pnlDate.Visible = true;
        pnlLaw.Visible = false;
        pnlImages.Visible = false;
        pnlMoreUrl.Visible = false;
        pnlCategory.Visible = false;
        PnlOrderBy.Visible = false;

        if (pageType == Utils.PageType.SummaryGoals || pageType == Utils.PageType.About)
        {
            pnlDate.Visible = false;
        }
        if (pageType == Utils.PageType.International
            || pageType == Utils.PageType.Konstitusiya
            || pageType == Utils.PageType.KonstitusiyaDefaultPage
            || pageType == Utils.PageType.MechanismDefaultPage)
        {
            pnlDate.Visible = false;
        }
        if (pageType == Utils.PageType.Laws)
        {
            pnlLaw.Visible = true;
            pnlDate.Visible = false;
            pnlMoreUrl.Visible = true;

        }

        if (pageType == Utils.PageType.News)
        {
            pnlImages.Visible = true;
        }
        if (pageType == Utils.PageType.Terefdaslar)
        {
            pnlCategory.Visible = true;
            pnlImages.Visible = true;
            fileUpload.AllowMultiple = false;
            lblImageHelper.Visible = false;
        }


        if (pageType == Utils.PageType.MechanismOtherPages ||
            pageType == Utils.PageType.Laws ||
             pageType == Utils.PageType.MechanismGundelik ||
            pageType == Utils.PageType.Konstitusiya)
        {
            PnlOrderBy.Visible = true;
            if (pageType == Utils.PageType.MechanismGundelik)
            { 
                pnlLaw.Visible = true;
                pnlMoreUrl.Visible = true;
            }
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

        title_az.Text = dt.Rows[0]["title_az"].ToParseStr();
        title_en.Text = dt.Rows[0]["title_en"].ToParseStr();

        Content_az.Html = dt.Rows[0]["content_az"].ToParseStr();
        Content_en.Html = dt.Rows[0]["content_en"].ToParseStr();

        txtMoreUrl.Text = dt.Rows[0]["more_url"].ToParseStr();
        txtVideoURL.Text = dt.Rows[0]["video_url"].ToParseStr();

        page_dt.Date = DateTime.Parse(dt.Rows[0]["page_dt"].ToParseStr());

        GoalList.Value = dt.Rows[0]["goal_id"].ToParseStr();
        PageId.Value = dt.Rows[0]["page_id"].ToParseStr();
        OrderBy.Value = dt.Rows[0]["orderBy"].ToParseStr();

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
        GoalList.DataSource = _db.GetGoals();
        GoalList.DataBind();

        PageId.DataSource = _db.GetPages(Utils.PageType.TerefdaslarCategory);
        PageId.DataBind();
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        string _infoText = "";
        Utils.MethodType _type = Utils.MethodType.Error;

        #region validation

        if (title_az.Text.Trim().Length < 3)
        {
            _infoText += " - Başlıq qeyd olunmuyub <br/>";
        }
        if (Content_az.Html.Trim().Length < 10)
        {
            _infoText += " - Mətn qeyd olunmuyub <br/>";
        }

        if (page_dt.Value == null)
        {
            page_dt.Value = DateTime.Now;
            //_infoText += " - Tarix seçilməyib <br/>";
        }


        if (Utils.PageType.Videos == Config.getPageType)
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
        int pageId = 0;
        Utils.MethodType returnVal = Utils.MethodType.Error;

        if (btnSave.CommandArgument == "update")
        {
            returnVal = _db.PageUpdate(_getIDFromQuery, (int)Config.getPageType,
              title_az.Text.Trim(),
              Config.Slug(title_az.Text.Trim()),
              title_en.Text.Trim(),
              Config.Slug(title_en.Text.Trim()),
              Content_az.Html,
              Content_en.Html,
              page_dt.Date,
              txtVideoURL.Text,
              GoalList.Value.ToParseInt(),
              PageId.Value.ToParseInt(),
              txtMoreUrl.Text.Trim(),
              OrderBy.Value.ToParseInt());
            pageId = _getIDFromQuery;
        }
        else
        {
            returnVal = _db.PageInsert((int)Config.getPageType,
                title_az.Text.Trim(),
                Config.Slug(title_az.Text.Trim()),
                title_en.Text.Trim(),
                Config.Slug(title_en.Text.Trim()),
                Content_az.Html,
                Content_en.Html,
                page_dt.Date,
                txtVideoURL.Text,
                GoalList.Value.ToParseInt(),
                PageId.Value.ToParseInt(),
                txtMoreUrl.Text.Trim(),
                OrderBy.Value.ToParseInt());

            pageId = _db.getLastPageId.ToParseInt();

            Utils.PageType pageType = Config.getPageType;

            if (pageType == Utils.PageType.News || pageType == Utils.PageType.Videos )
            {
                SubscribeContent content = new SubscribeContent();
                content.Id = pageId;
                content.Name = title_az.Text.Trim();
                content.Content = Content_az.Html;
                content.Type = pageType;

                SubscribeEmailSender emailSender = new SubscribeEmailSender(content);
                emailSender.Send();
            }
        }



        if (fileUpload.HasFiles)
        {
            _db.PageContentDelete(pageId);
        }
        foreach (HttpPostedFile item in fileUpload.PostedFiles)
        {
            string filename = "image-" + DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(item.FileName);
            item.SaveAs(Server.MapPath("~/uploads/pages/" + filename));
            _db.PageContentInsert(pageId, filename);

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