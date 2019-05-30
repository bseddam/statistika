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
        string pageTitle = DALC.GetStaticValue("goal_info_tab3_title");
        Page.Title = pageTitle + " - " + Config.GetAppSetting("ProjectName");

        if (IsPostBack)
        {
            return;
        }
        lbltitle.Text= DALC.GetStaticValue("goal_info_tab3_title");




        DataTable dtlinkheader = _db.getlinkheader();
        rplinkheader.DataSource = dtlinkheader;
        rplinkheader.DataBind();
        
        string linkstr = "";
        string linkler = "";
        for (int i = 0; i < dtlinkheader.Rows.Count; i++)
        {
            DataTable dtlinkler = _db.GetUsefullLinksforheaderid(dtlinkheader.Rows[i]["id"].ToParseInt());
            for (int j = 0; j < dtlinkler.Rows.Count; j++)
            {
                linkler = linkler+ "<li><a href = '" + dtlinkler.Rows[j]["useful_links_url_"+ lang] + "'>" + dtlinkler.Rows[j]["useful_links_name_"+ lang] + "</a></li>";
            }
            linkstr = linkstr + @"<div id='h" + dtlinkheader.Rows[i]["id"] + @"' class='container tab-pane" + dtlinkheader.Rows[i]["activefade"] + @"'>
                                        <br />
                                        <div class='row'>
                                            <div class='col-md-12'>
                                                <ul class='list-unstyled'>
                                                   "+ linkler + @"
                                                </ul>
                                            </div>
                                        </div>
                                    </div>";
            linkler = "";
        }
        lbllinkler.Text = linkstr;

        ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / {2} ",
            lang,
            DALC.GetStaticValue("home_breadcrumb_title"),
            pageTitle);

    }

}