using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string lang = Config.getLang(Page);
        lblTitle.Text = DALC.GetStaticValue("register_page_name");
        Page.Title = lblTitle.Text + " - " + Config.GetAppSetting("ProjectName");
        if (IsPostBack)
        {
            return;
        }

        lblAd.Text = txtAd.Attributes["placeholder"] = DALC.GetStaticValue("register_person_name");
        lblSoyad.Text = txtSoyad.Attributes["placeholder"] = DALC.GetStaticValue("register_person_surname");
        lblAtaAdi.Text = txtAtaAdi.Attributes["placeholder"] = DALC.GetStaticValue("register_person_patronymic");
        lblVezife.Text = txtvezife.Attributes["placeholder"] = DALC.GetStaticValue("register_position");
        lblTelM.Text = txtTelMobil.Attributes["placeholder"] = DALC.GetStaticValue("register_phone_mob");
        lblTelIsh.Text = txtTelIsh.Attributes["placeholder"] = DALC.GetStaticValue("register_phone_work");
        lblEmail.Text = txtEmail.Attributes["placeholder"] = DALC.GetStaticValue("register_email");
        lblPass.Text = txtPass.Attributes["placeholder"] = DALC.GetStaticValue("register_pass");
        lblPass2.Text = txtPass2.Attributes["placeholder"] = DALC.GetStaticValue("register_pass2");
       // lblPassHelper.Text = DALC.GetStaticValue("register_pass_helper");
        lblQurum.Text = txtQurumKod.Attributes["placeholder"] = DALC.GetStaticValue("register_org_code");
       // lblQurumHelper.Text = DALC.GetStaticValue("register_org_code_helper");


        checkEmail.ErrorMessage = DALC.GetStaticValue("register_email_error");
        passCompare.ErrorMessage = DALC.GetStaticValue("register_pass_dont_match");

        btnLogin.Text = DALC.GetStaticValue("register_button");
       

    }
    DALC _db = new DALC();
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        lblError.ForeColor = System.Drawing.Color.Red;
        txtPass2.Text = txtPass2.Text.Trim();
        txtPass.Text = txtPass.Text.Trim();

        if (txtEmail.Text.Length < 2 || txtPass.Text.Length < 2 ||
            txtPass2.Text.Length < 2 || txtQurumKod.Text.Length < 2 ||
            txtTelIsh.Text.Length < 2 || txtAd.Text.Length < 2 ||
            txtTelMobil.Text.Length < 2 || txtvezife.Text.Length < 2)
        {
            lblError.Text = DALC.GetStaticValue("register_form_fill_error");
            return;
        }
        if (txtPass.Text.Length<5)
        {
            lblError.Text = DALC.GetStaticValue("register_pass_lenght_error");
            return;
        }

        if (txtPass2.Text != txtPass.Text )
        {
            lblError.Text = DALC.GetStaticValue("register_pass_dont_match_error");
            return;
        }


        string orgId = _db.GetQurumByCode(txtQurumKod.Text.Trim());

        if (orgId == "-1")
        {
            lblError.Text = DALC.GetStaticValue("register_db_problem");
            return;
        }
        if (orgId == "0")
        {
            lblError.Text = DALC.GetStaticValue("register_org_not_found_error");
            return;
        }

        string checkUser = _db.CheckUser(orgId);
        if (checkUser == "-1")
        {
            lblError.Text = DALC.GetStaticValue("register_db_problem");
            return;
        }
        if (checkUser != "0")
        {
            lblError.Text = DALC.GetStaticValue("register_user_exists");
            return;
        }


        Utils.MethodType chk = _db.UserInsert(orgId.ToParseInt(),
            txtAd.Text,
            txtSoyad.Text,
            txtAtaAdi.Text,
            txtvezife.Text,
            txtTelMobil.Text,
            txtTelIsh.Text,
            txtEmail.Text,
            txtPass.Text.Trim()
            );

        if (chk == Utils.MethodType.Succes)
        {
            lblError.Text = DALC.GetStaticValue("register_user_success");
            lblError.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lblError.Text = DALC.GetStaticValue("register_user_error");
           
        }

    }
}