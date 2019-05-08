using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebPages_SearchGeneral : System.Web.UI.Page
{
    DALC _db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }

        string lang = Config.getLang(Page);
        string searchText = Request.QueryString["q"];

        ltrPageName.Text = DALC.GetStaticValue("search_general_title");


        if (searchText.Length<3)
        {
            pnlSearch.Visible = false;
            lblSearchInfo.Text = DALC.GetStaticValue("search_general_info_text");
            return;
        }

        ltrSearchLabel.Text = DALC.GetStaticValue("search_text_label");
        ltrSearchText.Text = searchText;




        ltrSearchResultLabel.Text = DALC.GetStaticValue("search_result_label");

        Page.Title = Config.HtmlRemoval.StripTagsRegex(ltrPageName.Text) + " - " + Config.GetAppSetting("ProjectName");
        ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / {2} ",
                lang,
                DALC.GetStaticValue("home_breadcrumb_title"),
               Config.HtmlRemoval.StripTagsRegex(ltrPageName.Text));


        DataTable dt = _db.GetSearchGeneral_Groups(searchText);
        DataTable dtPubRes = _db.GetSearchGeneral_Pub_Res(searchText);

        if (dtPubRes.Rows.Count > 0)
        {
            DataRow dr = dt.NewRow();
            dr["group_id"] = 100;
            dr["group_name_az"] = DALC.GetStaticValue("home_box_4");
            dr["group_name_en"] = DALC.GetStaticValue("home_box_4");
            dt.Rows.Add(dr);
        }

        rptSearchResult.DataSource = dt;
        rptSearchResult.DataBind();

        int search_result = 0;

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            Repeater rptSearchItems = rptSearchResult.Items[i].FindControl("rptSearchItems") as Repeater;
            int grpId = dt.Rows[i]["group_id"].ToParseInt();
            DataTable dtItems = new DataTable();
            if (grpId == 100)
            {
                dtItems = dtPubRes;
            }
            else
            {
                dtItems = _db.GetSearchGeneral_Items(grpId, searchText);
            }

            search_result += dtItems.Rows.Count;

            rptSearchItems.DataSource = dtItems;
            rptSearchItems.DataBind();
        }

        ltrSearchResult.Text = search_result + "";
    }

    public string ContentText(string text)
    {
        text = Config.HtmlRemoval.StripTagsRegex(text);

        if (text.Length > 200)
        {
            return text.Substring(0, 197) + "...";
        }

        return text;

    }

    public string GenerateURL(int id, string name, string type_id, int page_id, int goal_id)
    {
        Utils.PageType ptype = (Utils.PageType)Enum.Parse(typeof(Utils.PageType), type_id);
        string _url = "";

        string lang = Config.getLang(Page);

        switch (ptype)
        {
            case Utils.PageType.News:
                _url = string.Format("/{0}/news/{1}", lang, id);
                break;
            case Utils.PageType.Videos:
                _url = string.Format("/{0}/video/{1}", lang, id);
                break;
            case Utils.PageType.Content:
                _url = string.Format("/{0}/page/{1}", lang, id);
                break;
            case Utils.PageType.Laws:
                _url = string.Format("/{0}/law/{1}/{2}", lang, goal_id, Config.Slug(name));
                break;
            case Utils.PageType.SummaryGoals:
                _url = string.Format("/{0}/summary-goals", lang);
                break;
            case Utils.PageType.International:
                _url = string.Format("/{0}/international-challenge", lang);
                break;
            case Utils.PageType.Konstitusiya:
                _url = string.Format("/{0}/constitution/{1}/{2}", lang, id, Config.Slug(name));
                break;
            case Utils.PageType.KonstitusiyaDefaultPage:
                _url = string.Format("/{0}/constitution", lang);
                break;
            case Utils.PageType.MechanismDefaultPage:
                _url = string.Format("/{0}/national-implementation-mechanism", lang);
                break;
            case Utils.PageType.MechanismOtherPages:
                _url = string.Format("/{0}/national-implementation-mechanism/{1}/{2}", lang, id, Config.Slug(name));
                break;
            case Utils.PageType.MechanismGundelik:
                _url = string.Format("/{0}/national-implementation-mechanism/10/s", lang);
                break;
           
            case Utils.PageType.Terefdaslar:
                _url = string.Format("/{0}/interesting-sides/{1}/{2}/{3}", lang, page_id, id, Config.Slug(name));
                break;
            case Utils.PageType.About:
                _url = string.Format("/{0}/about", lang);
                break;
            case Utils.PageType.Order:
                _url = string.Format("/{0}/order/{1}/{2}", lang, id, Config.Slug(name));
                break;
            case Utils.PageType.Researches:
                _url = string.Format("/{0}/research/{1}/{2}", lang, id, Config.Slug(name));
                break;
            case Utils.PageType.Publications:
                _url = string.Format("/{0}/publication/{1}/{2}", lang, id, Config.Slug(name));
                break;
        }

        return _url;

    }
}