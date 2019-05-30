using DevExpress.Web.ASPxHtmlEditor;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Admin_Publications_Add : System.Web.UI.Page
{
    DALC _db = new DALC();
    Utils.Tables _pageTable = Utils.Tables.pages;

    protected void Page_Load(object sender, EventArgs e)
    {

        Config.isLogin(Page);

        if (IsPostBack) return;


        LtrPageTitle.Text = "Yeni səhifə";

        _loadCategory();
        int _dataID = _getIDFromQuery;

        if (_dataID > 0)
        {
            _loadDataByID(_dataID);

            LtrPageTitle.Text = "Səhifə məlumatlarını yenilə";
        }

    }

    private void _loadCategory()
    {
        Category.DataSource = _db.GetPublicationCategory();
        Category.DataBind();
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
        DataTable dt = _db.GetPublicationsById(DataID);

        if (dt == null && dt.Rows.Count < 1) return;

        title_az.Html = dt.Rows[0]["title_az"].ToParseStr();
        title_en.Html = dt.Rows[0]["title_en"].ToParseStr();
        Content_az.Html = dt.Rows[0]["content_az"].ToParseStr();
        Content_en.Html = dt.Rows[0]["content_en"].ToParseStr();

        page_dt.Date = DateTime.Parse(dt.Rows[0]["date"].ToParseStr());

        hf_image_filename_az.Value = dt.Rows[0]["image_filename_az"].ToParseStr();
        hf_image_filename_en.Value = dt.Rows[0]["image_filename_en"].ToParseStr();
        hf_short_material_az.Value = dt.Rows[0]["short_material_az"].ToParseStr();
        hf_short_material_en.Value = dt.Rows[0]["short_material_en"].ToParseStr();
        hf_tam_material_az.Value = dt.Rows[0]["full_material_az"].ToParseStr();
        hf_tam_material_en.Value = dt.Rows[0]["full_material_en"].ToParseStr();
        Category.Value = dt.Rows[0]["category_id"].ToParseStr();
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

        string full_material_az = "full-material-az-" + DateTime.Now.Ticks;
        string full_material_en = "full-material-en-" + DateTime.Now.Ticks;
        string short_material_az = "short_material-az-" + DateTime.Now.Ticks;
        string short_material_en = "short_material-en-" + DateTime.Now.Ticks;
        string image_filename_az = "image-az-" + DateTime.Now.Ticks;
        string image_filename_en = "image-en-" + DateTime.Now.Ticks;
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



        if (fuTamMaterialAz.HasFile)
        {
            full_material_az += Path.GetExtension(fuTamMaterialAz.FileName);
            fuTamMaterialAz.SaveAs(rootFolder + full_material_az);
        }
        else
        {
            full_material_az = hf_tam_material_az.Value;
        }

        if (fuTamMaterialEn.HasFile)
        {
            full_material_en += Path.GetExtension(fuTamMaterialEn.FileName);
            fuTamMaterialEn.SaveAs(rootFolder + full_material_en);
        }
        else
        {
            full_material_en = hf_tam_material_en.Value;
        }




        if (fuShortMaterialAz.HasFile)
        {
            short_material_az += Path.GetExtension(fuShortMaterialAz.FileName);
            fuShortMaterialAz.SaveAs(rootFolder + short_material_az);
        }
        else
        {
            short_material_az = hf_short_material_az.Value;
        }
        if (fuShortMaterialEn.HasFile)
        {
            short_material_en += Path.GetExtension(fuShortMaterialEn.FileName);
            fuShortMaterialEn.SaveAs(rootFolder + short_material_en);
        }
        else
        {
            short_material_en = hf_short_material_en.Value;
        }






        if (fuImageaz.HasFile)
        {
            image_filename_az += Path.GetExtension(fuImageaz.FileName);
            fuImageaz.SaveAs(rootFolder + image_filename_az);
        }
        else
        {
            image_filename_az = hf_image_filename_az.Value;
        }
        if (fuImageen.HasFile)
        {
            image_filename_en += Path.GetExtension(fuImageen.FileName);
            fuImageen.SaveAs(rootFolder + image_filename_en);
        }
        else
        {
            image_filename_en = hf_image_filename_en.Value;
        }







        if (btnSave.CommandArgument == "update")
        {
            returnVal = _db.PublicationUpdate(_getIDFromQuery, title_az.Html,
                title_en.Html,
                Content_az.Html,
                Content_en.Html,
                page_dt.Date,
                full_material_az,
                full_material_en,
                short_material_az,
                short_material_en,
                image_filename_az,
                image_filename_en,
                Category.Value.ToParseInt());
        }
        else
        {
            returnVal = _db.PublicationInsert(title_az.Html,
                title_en.Html,
                Content_az.Html,
                Content_en.Html,
                page_dt.Date,
                full_material_az,
                full_material_en,
                short_material_az,
                short_material_en,
                image_filename_az,
                image_filename_en,
                Category.Value.ToParseInt());

            pageId = _db.getLastPublicationId.ToParseInt();

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