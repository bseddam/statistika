﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class User_Account_Default : System.Web.UI.Page
{
    DALC _db = new DALC();

    protected void Page_Load(object sender, EventArgs e)
    {
        Config.isUserLogin(Page);

        if (IsPostBack) return;


        LtrPageTitle.Text = "Şifrəni yenilə";




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
    private void _hidePanel()
    {
        PnlInfo.CssClass = "";
        LblInfo.Text = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        _hidePanel();
        string _infoText = "";
        Utils.MethodType _type = Utils.MethodType.Error;

        if (Pass.Text.Trim().Length < 4)
        {
            _showPanel(" - Şifrə ən azı 4 simvol olmalıdır <br/>", _type);
            return;
        }
        if (Pass.Text.Trim() != Pass2.Text.Trim())
        {
            _showPanel(" - Şifrə uyğun gəlmir <br/>", _type);
            return;
        }

        Utils.MethodType returnVal = _db.UserPassUpdate(Pass.Text.Trim());


        if (returnVal == Utils.MethodType.Error)
        {
            _showPanel(" XƏTA ! Yadda saxlamaq mümkün olmadı", _type);
            return;
        }

        _showPanel(" Məlumat yadda saxlanıldı", Utils.MethodType.Succes);




    }
}