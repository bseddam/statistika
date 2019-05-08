using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_ForgotPass : System.Web.UI.Page
{
    DALC _db = new DALC();

    const string _defaultUrl = "/user/main";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        string _projectName = Config.GetAppSetting("ProjectName");
        LtrFooter.Text = string.Format("{0} &copy; {1}", DateTime.Now.Year, Config.GetAppSetting("ProjectOwner"));
        Page.Title = ltrTitle.Text = _projectName;



        if (DALC.UserInfo.isLogin)
        {
            Config.Rd(_defaultUrl);
        }

    }
    protected void LnkEnter_Click(object sender, EventArgs e)
    {

        UserLogin.Text = UserLogin.Text.Trim();

        if (UserLogin.Text.Length < 3)
        {
            _showPanel("Qurumun kodunu daxil edin!", Utils.MethodType.Error);
            return;
        }





        MyView.ActiveViewIndex = 1;


        DataTable dtUsers = _db.GetUserByOrgCode(UserLogin.Text);

        if (dtUsers == null)
        {
            _showPanel("Məlumat bazasına qoşulmada problem yarandı!", Utils.MethodType.Error);
            return;
        }
        if (dtUsers.Rows[0]["is_active"].ToParseStr() == "0")
        {
            _showPanel("Hesabınız admin tərəfindən təsdiq olunmayıb!", Utils.MethodType.Error);
            return;
        }

        string email = dtUsers.Rows[0]["email"].ToParseStr();

        string token = Cryptography.Encrypt(
                        string.Format("{0};{1}",
                            dtUsers.Rows[0]["id"].ToParseStr(),
                            DateTime.Now.DayOfYear
                            ));

        string passLink = string.Format("/user/recoverpass.aspx?{0}", token);
        string template = GetEmailContent(passLink);

        Config.SendMail(email, "Şifrənin bərpası", template);
        Application[token] = DateTime.Now.Ticks;
    }
    private string GetValueFromConfig(string key)
    {
        return System.Configuration.ConfigurationManager.AppSettings[key];
    }
    private string GetEmailContent(string passLink)
    {
        StreamReader rd = new StreamReader(HttpContext.Current.Server.MapPath("~/email-templates/forgot-pass.html"));
        string content = rd.ReadToEnd();
        rd.Close();

        content = content.Replace("{url}", GetValueFromConfig("WebSiteURL"));
        content = content.Replace("{pass-link}", GetValueFromConfig("WebSiteURL")+passLink);
        return content;
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