using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_Default : System.Web.UI.Page
{
    DALC _db = new DALC();

    const string _defaultUrl = "/user/main";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        string _projectName = Config.GetAppSetting("ProjectName");
        LtrFooter.Text = string.Format("{0} &copy; {1}", DateTime.Now.Year, Config.GetAppSetting("ProjectOwner"));
        Page.Title = ltrTitle.Text = _projectName;

        if (Request.Cookies["userlogin1"] != null)
        {
            UserLogin.Text = Request.Cookies["userlogin1"].Value;
        }

        if (DALC.UserInfo.isLogin)
        {
            Config.Rd(_defaultUrl);
        }

        Response.Cookies["r_user1"].Expires = DateTime.Now.AddDays(-1);
    }
    protected void LnkEnter_Click(object sender, EventArgs e)
    {

        UserLogin.Text = UserLogin.Text.Trim();
        PassWord.Text = PassWord.Text.Trim();

        if (UserLogin.Text.Length < 3)
        {
            _showPanel("İstifadəçi adını daxil edin!", Utils.MethodType.Error);
            return;
        }
        if (PassWord.Text.Length < 3)
        {
            _showPanel("Şifrəni daxil edin!", Utils.MethodType.Error);
            return;
        }


        string _id = _db.GetUser(UserLogin.Text, PassWord.Text);

        if (_id == "-1")
        {
            _showPanel("Xəta! Sistemdə yüklənmə var.", Utils.MethodType.Error);
            return;
        }
        if (_id.Length < 1)
        {
            _showPanel("İstifadəçi adı və ya şifrə yanlışdır !", Utils.MethodType.Error);
            return;
        }

        DataTable dt = _db.GetUser(_id.ToParseInt());

        if (dt.Rows.Count == 0)
        {
            _showPanel("Məlumat bazasına qoşulmada problem yarandı!", Utils.MethodType.Error);
            return;
        }
        if (dt.Rows[0]["is_active"].ToParseStr() == "0")
        {
            _showPanel("Sistemə daxil ola bilməniz üçün hesabınız admin tərəfindən təsdiq olunmalıdır!", Utils.MethodType.Error);
            return;
        }


        Session["UserID"] = _id;
        _db.LogInsert(Utils.Tables.users, Utils.LogType.select, String.Format("User ( Login: {0} ) ", UserLogin.Text), "", "İstifadəçi sistemə daxil oldu ( İD :" + _id + ")", false);

        if (ChkRemember.Checked)
        {
            HttpCookie Cook = new HttpCookie("userlogin1", UserLogin.Text);
            Cook.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Add(Cook);
        }




        _db.ManagerLastDtUpdate();

        if (Config.Cs(Request.QueryString["return"]).Length > 1)
        {
            Config.Rd(Request.QueryString["return"]);
        }
        else
        {
            //Config.Rd(_db.GetMenuDefaultPageUrl);
            Config.Rd(_defaultUrl);
        }
    }
    private void _showPanel(string text, Utils.MethodType _type)
    {
        PnlInfo.Visible = true
           ;
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
        LblError.Text = text;
    }
}