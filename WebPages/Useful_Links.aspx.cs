using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class WebPages_UsefulLinks : System.Web.UI.Page
{
    DALC _db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {
        string lang = Config.getLang(Page);
        string pageTitle = DALC.GetStaticValue("usefullinks_page_title");
        Page.Title = pageTitle + " - " + Config.GetAppSetting("ProjectName");

        if (IsPostBack)
        {
            return;
        }





        DataTable dt = _db.getlinkheader();
        rplinkheader.DataSource = dt;
        rplinkheader.DataBind();
        string linkstr = "";
        for (int i = 0; i < dt.Rows.Count; i++)
        {

      
         linkstr = linkstr+ @"<div id='h" + dt.Rows[i]["id"] + @"' class='container tab-pane"+ dt.Rows[i]["activefade"]+ @"'>
                                        <br />
                                        <div class='row'>
                                            <div class='col-md-12'>
                                                <ul class='list-unstyled'>
                                                    <li><a href = ''>aaaa</a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>";
        }
        lbllinkler.Text = linkstr;

        ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / {2} ",
            lang,
            DALC.GetStaticValue("home_breadcrumb_title"),
            pageTitle);

    }

}