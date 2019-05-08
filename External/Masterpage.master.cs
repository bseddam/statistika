using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class External_Masterpage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack) return;

        string _projectName = Config.GetAppSetting("ProjectName");

        ltrFooter.Text = string.Format("{0} &copy; {1}", DateTime.Now.Year, _projectName);
        //ImgUser.ImageUrl = string.Format("/uploads/managers/{0}.png", DALC.ManagerInfo.ID);
        //LtrUserName.Text = DALC.UserInfo.Name + DALC.UserInfo.Surname;




        Page.Title = _projectName;
    }
}
