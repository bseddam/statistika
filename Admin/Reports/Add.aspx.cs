using DevExpress.Web.ASPxHtmlEditor;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Admin_Reports_Add : System.Web.UI.Page
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
        DataTable dt = _db.GetNationalReportsById(DataID);

        if (dt == null && dt.Rows.Count < 1) return;

        title_az.Html = dt.Rows[0]["title_az"].ToParseStr();
        title_en.Html = dt.Rows[0]["title_en"].ToParseStr();
        Content_az.Html = dt.Rows[0]["content_az"].ToParseStr();
        Content_en.Html = dt.Rows[0]["content_en"].ToParseStr();

        content_short_az.Html = dt.Rows[0]["content_short_az"].ToParseStr();
        content_short_en.Html = dt.Rows[0]["content_short_en"].ToParseStr();

        page_dt.Date = DateTime.Parse(dt.Rows[0]["date"].ToParseStr());

        hf_image_filename_az.Value = dt.Rows[0]["image_filename_az"].ToParseStr();
        hf_image_filename_en.Value = dt.Rows[0]["image_filename_en"].ToParseStr();

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
        string image_filename_az = "image-" + DateTime.Now.Ticks;
        string image_filename_en = "image-" + DateTime.Now.Ticks;
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

        if (fuImage_az.HasFile)
        {
            image_filename_az += Path.GetExtension(fuImage_az.FileName);
            fuImage_az.SaveAs(rootFolder + image_filename_az);
        }
        else
        {
            image_filename_az = hf_image_filename_az.Value;
        }
        if (fuImage_en.HasFile)
        {
            image_filename_en += Path.GetExtension(fuImage_en.FileName);
            fuImage_en.SaveAs(rootFolder + image_filename_en);
        }
        else
        {
            image_filename_en = hf_image_filename_en.Value;
        }


        if (btnSave.CommandArgument == "update")
        {
            returnVal = _db.NationalReportsUpdate(_getIDFromQuery, title_az.Html,
                title_en.Html,
                Content_az.Html,
                Content_en.Html,
                page_dt.Date,

                full_material,
                short_material,
                image_filename_az,
                image_filename_en,
                content_short_az.Html,
                content_short_en.Html);
        }
        else
        {
            returnVal = _db.NationalReportInsert(title_az.Html,
                title_en.Html,
                Content_az.Html,
                Content_en.Html,
                page_dt.Date,

                full_material,
                short_material,
                image_filename_az,
                image_filename_en,
                content_short_az.Html,
                content_short_en.Html);

            pageId = _db.getLastPageId.ToParseInt();
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

    void saveFile(FileUpload fu, ref string filename, bool update)
    {
        if (update != true)
        {
            filename += System.IO.Path.GetExtension(fu.FileName);
        }

        if (fu.HasFile)
        {
            fu.SaveAs(Server.MapPath("~/uploads/pages/" + filename));
        }
    }

}