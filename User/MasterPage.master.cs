using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_MasterPage : System.Web.UI.MasterPage
{
    DALC _db = new DALC();
    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    DevExpress.Web.ASPxClasses.ASPxWebControl.GlobalTheme = "Moderno";
    //}
    //string _access_login = Utils.accessLoginSearchPerson;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack) return;

        string _projectName = Config.GetAppSetting("ProjectName");

        ltrFooter.Text = string.Format("{0} &copy; {1}", DateTime.Now.Year, _projectName);
        //ImgUser.ImageUrl = string.Format("/uploads/managers/{0}.png", DALC.ManagerInfo.ID);
        LtrUserName.Text = DALC.UserInfo.Name +" "+ DALC.UserInfo.Surname;

       


        Page.Title = _projectName;

    }

    
}
