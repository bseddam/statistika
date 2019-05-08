using DevExpress.Web.ASPxHtmlEditor;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_Research_Add : System.Web.UI.Page
{
    DALC _db = new DALC();
    Utils.Tables _pageTable = Utils.Tables.pages;

    protected void Page_Load(object sender, EventArgs e)
    {

        Config.isLogin(Page);

        if (IsPostBack) return;


        LtrPageTitle.Text = "Yeni səhifə";

        int _dataID = _getIDFromQuery;

        if (_dataID > 0)
        {
            _loadDataByID(_dataID);

            LtrPageTitle.Text = "Səhifə məlumatlarını yenilə";
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
        DataTable dt = _db.GetResearchesById(DataID);

        if (dt == null && dt.Rows.Count < 1) return;

        title_az.Html = dt.Rows[0]["title_az"].ToParseStr();
        title_en.Html = dt.Rows[0]["title_en"].ToParseStr();
        Content_az.Html = dt.Rows[0]["content_az"].ToParseStr();
        Content_en.Html = dt.Rows[0]["content_en"].ToParseStr();

        page_dt.Date = DateTime.Parse(dt.Rows[0]["date"].ToParseStr());

        hf_short_material.Value = dt.Rows[0]["short_material"].ToParseStr();
        hf_tam_material.Value = dt.Rows[0]["full_material"].ToParseStr();
        btnSave.CommandArgument = "update";
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

        string full_material = "full-material-" + DateTime.Now.Ticks;
        string short_material = "short_material-" + DateTime.Now.Ticks;
        string image_filename = "image-" + DateTime.Now.Ticks;
        #region validation

        if (title_az.Html.Trim().Length < 3)
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

        #endregion

        if (_infoText.Length > 2)
        {
            _showPanel(_infoText, _type);
            return;
        }
        int pageId = 0;
        Utils.MethodType returnVal = Utils.MethodType.Error;


        string rootFolder = Server.MapPath("~/uploads/pages/");



        if (fuTamMaterial.HasFile)
        {
            full_material += Path.GetExtension(fuTamMaterial.FileName);
            fuTamMaterial.SaveAs(rootFolder + full_material);
        }
        else
        {
            full_material = hf_tam_material.Value;
        }

        if (fuShortMaterial.HasFile)
        {
            short_material += Path.GetExtension(fuShortMaterial.FileName);
            fuShortMaterial.SaveAs(rootFolder + short_material);
        }
        else
        {
            short_material = hf_short_material.Value;
        }


        if (btnSave.CommandArgument == "update")
        {
            returnVal = _db.ResearchUpdate(_getIDFromQuery, title_az.Html,
                title_en.Html,
                Content_az.Html,
                Content_en.Html,
                page_dt.Date,
                full_material,
                short_material
                );
        }
        else
        {
            returnVal = _db.ResearchInsert(title_az.Html,
                title_en.Html,
                Content_az.Html,
                Content_en.Html,
                page_dt.Date,
                full_material,
                short_material
                );

            pageId = _db.getLastResearchId.ToParseInt();

            SubscribeContent content = new SubscribeContent();
            content.Id = pageId;
            content.Name = title_az.Html.Trim();
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