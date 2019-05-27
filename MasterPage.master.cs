using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    DALC db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {
        string lang = Config.getLang(Page);
        if (IsPostBack)
        {
            return;
        }




        if (lang == "en")
        {
            ltrLink.Text = string.Format("<a href='{0}'><submit class='btn btn-az'>AZ</submit></a> " +
                "<a href='{1}'><submit class='btn btn-en' style='background-color: #026BA2;color:white;'>EN</submit></a>", getURLaz, getURLen);
        }
        else
        {
            ltrLink.Text = string.Format("<a href='{0}'><submit class='btn btn-az' style='background-color: #026BA2;color:white;'>AZ</submit></a> " +
                 "<a href='{1}'><submit class='btn btn-en'>EN</submit></a>", getURLaz, getURLen);
        }

        //if (lang == "en")
        //{
        //    ltrLink.Text = string.Format("<a class='btn btn-az' href='{0}'>AZ</a> " +
        //        "<a class='btn btn-en' href='{1}' >EN</a>", getURL, getURL);
        //}
        //else
        //{
        //    ltrLink.Text = string.Format("<a class='btn btn-az' href='{0}'>AZ</a> " +
        //        "<a class='btn btn-en' href='{1}'>EN</a>", getURL, getURL);
        //}
        DataTable dtHeader = db.GetMenuClient(Utils.MenuType.Header, lang);
        rptHeaderMenu.DataSource = dtHeader;
        rptHeaderMenu.DataBind();

        for (int i = 0; i < dtHeader.Rows.Count; i++)
        {
            if (dtHeader.Rows[i]["SubCOunt"].ToParseInt() > 0)
            {
                Repeater rptHeaderSub = rptHeaderMenu.Items[i].FindControl("rptHeaderSub") as Repeater;
                rptHeaderSub.DataSource = db.GetMenuClient(Utils.MenuType.Header, lang, dtHeader.Rows[i]["Id"].ToParseInt());
                rptHeaderSub.DataBind();
            }
        }


        DataTable dtMain = db.GetMenuClient(Utils.MenuType.Main, lang);
        rptMain.DataSource = dtMain;
        rptMain.DataBind();

        for (int i = 0; i < dtMain.Rows.Count; i++)
        {
            if (dtMain.Rows[i]["SubCOunt"].ToParseInt() > 0)
            {
                Repeater rptMainSub = rptMain.Items[i].FindControl("rptMainSub") as Repeater;
                rptMainSub.DataSource = db.GetMenuClient(Utils.MenuType.Main, lang, dtMain.Rows[i]["Id"].ToParseInt());
                rptMainSub.DataBind();
            }
        }

        rptFooterMenu.DataSource = db.GetMenuClient(Utils.MenuType.Footer, lang);
        rptFooterMenu.DataBind();

        txtSearch.Attributes["placeholder"] = DALC.GetStaticValue("search");


        //rptFooterMenu2.DataSource = db.GetMenuClient(Utils.MenuType.Footer2, lang);
        //rptFooterMenu2.DataBind();


        rptPartners.DataSource = db.GetPartners();
        rptPartners.DataBind();


        //ltrFollow.Text = DALC.GetStaticValue("social_text");

        hp_face.NavigateUrl = DALC.GetStaticValue_Url("social_fb_url");
        hp_linkedin.NavigateUrl = DALC.GetStaticValue_Url("social_linkedin_url");
        hp_twt.NavigateUrl = DALC.GetStaticValue_Url("social_twt_url");
        hp_youtube.NavigateUrl = DALC.GetStaticValue_Url("social_ytb_url");

        hpSearch.NavigateUrl = string.Format("/{0}/search/detailed", lang);
        hpSearch.Text = DALC.GetStaticValue_Url("search_detailed");
    }
    public string getURLaz
    {
        get
        {
            string url = Request.Url.ToString();
            string result = "";
            if (url.IndexOf("/en/") > -1)
            {
                result = url.Replace("/en/", "/az/");
            }
            else
            {
                result = url.Replace("/az/", "/az/");
            }
            return result;
        }
    }
    public string getURLen
    {
        get
        {
            string url = Request.Url.ToString();
            string result = "";
            if (url.IndexOf("/en/") > -1)
            {
                result = url.Replace("/en/", "/en/");
            }
            else
            {
                result = url.Replace("/az/", "/en/");
            }
            return result;
        }
    }
}
