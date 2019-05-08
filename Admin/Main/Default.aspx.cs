using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Main_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Config.isLogin(Page);

        if (IsPostBack) return;
    }
}