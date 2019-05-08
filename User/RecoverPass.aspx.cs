using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_RecoverPass : System.Web.UI.Page
{
    DALC _db = new DALC();

    const string _defaultUrl = "/user/main";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;

        string _projectName = Config.GetAppSetting("ProjectName");
        LtrFooter.Text = string.Format("{0} &copy; {1}", DateTime.Now.Year, Config.GetAppSetting("ProjectOwner"));
        Page.Title = ltrTitle.Text = _projectName;

        //check token

        if (Application[GetToken].ToParseStr().Length==0)
        {
            MyView.ActiveViewIndex = 2;
        }

    }
    string GetToken
    {
        get
        {
            return Request.QueryString.ToParseStr();
        }
    }
    int GetUserId
    {

        get
        {
            try
            {
                string token = GetToken;
                string decrypted = Cryptography.Decrypt(token);

                if (decrypted == "-1")
                {
                    MyView.ActiveViewIndex = 2;
                    return -1;
                }

                //check date
                int dayOfYear = decrypted.Split(';')[1].ToParseInt();
                if ((DateTime.Now.DayOfYear - dayOfYear) > 2)
                {
                    MyView.ActiveViewIndex = 2;
                    return -1;
                }
                return decrypted.Split(';')[0].ToParseInt();
            }
            catch
            {
                MyView.ActiveViewIndex = 2;
                return -1;
            }
        }
    }

    protected void LnkEnter_Click(object sender, EventArgs e)
    {
        int userId = GetUserId;
        if (userId == -1)
        {
            return;
        }

        Password.Text = Password.Text.Trim();
        Password2.Text = Password2.Text.Trim();

        if (Password.Text.Length < 5)
        {
            _showPanel("Yeni şifrə daxil edin!", Utils.MethodType.Error);
            return;
        }

        if (Password2.Text.Length < 5)
        {
            _showPanel("Yeni şifrəni təkrar daxil edin!", Utils.MethodType.Error);
            return;
        }
        if (Password2.Text != Password2.Text)
        {
            _showPanel("Yeni şifrə və təkrarı eyni deyil!", Utils.MethodType.Error);
            return;
        }

        Utils.MethodType val = _db.UserPassUpdate(userId, Password2.Text.Trim());
        if (val == Utils.MethodType.Error)
        {
            _showPanel("Məlumat bazasına qoşulmada problem yarandı!", Utils.MethodType.Error);
            return;
        }

        MyView.ActiveViewIndex = 1;

        Application.Remove(GetToken);
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
        content = content.Replace("{pass-link}", passLink);
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