using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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


        LtrPageTitle.Text = "Şəxsi məlumatlar";

        txtName.Text = DALC.UserInfo.Name;
        txtPhoneMob.Text = DALC.UserInfo.PhoneMob;
        txtPhoneWork.Text = DALC.UserInfo.PhoneWork;
        txtPosition.Text = DALC.UserInfo.Position;

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
        Utils.MethodType _type = Utils.MethodType.Error;

        if (txtPosition.Text.Trim().Length < 2 ||
            txtPhoneWork.Text.Trim().Length < 2 ||
            txtPhoneMob.Text.Trim().Length < 2 ||
            txtName.Text.Trim().Length < 2)
        {
            _showPanel(" - Məlumatları boş saxlamaq olmaz <br/>", _type);
            return;
        }

        Utils.MethodType returnVal = _db.UserInfoUpdate(txtName.Text,
            txtPosition.Text,
            txtPhoneWork.Text,
            txtPhoneMob.Text
            );


        if (returnVal == Utils.MethodType.Error)
        {
            _showPanel(" XƏTA ! Yadda saxlamaq mümkün olmadı", _type);
            return;
        }
        int USerId = DALC.UserInfo.ID;

        DataTable dt = new DataTable();
        try
        {
            MySqlDataAdapter da = new MySqlDataAdapter(@"SELECT   id 
                                , org_id                            
                                , name 
                                , position 
                                , phone_work 
                                , phone_mob 
                                , email 
                            FROM users  where id=@id and is_active=1", DALC.SqlConn);

            da.SelectCommand.Parameters.AddWithValue("@id", USerId);
            da.Fill(dt);
        }
        catch (Exception ex)
        {
            new DALC().LogInsert(Utils.Tables.users, Utils.LogType.select, String.Format("UserInfo"), ex.Message, true);
            dt = null;
        }
        DALC.StrukturUserInfo data = new DALC.StrukturUserInfo();

        if (dt == null || dt.Rows.Count < 1) 
            return;

        DataRow dr = dt.Rows[0];

        if (dr == null) 
            return ;

        data.isLogin = true;
        data.ID = int.Parse(Convert.ToString(dr["id"]));

        data.OrgId = int.Parse(Convert.ToString(dr["org_id"]));

        data.Email = Convert.ToString(dr["email"]);
        data.Name = Convert.ToString(dr["name"]);
        data.Position = Convert.ToString(dr["position"]);
        data.PhoneMob = Convert.ToString(dr["phone_mob"]);
        data.PhoneWork = Convert.ToString(dr["phone_work"]);

        HttpContext.Current.Session["UserInfoStruktur"] = data;



        _showPanel(" Məlumat yadda saxlanıldı", Utils.MethodType.Succes);
    }
}