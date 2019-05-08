﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class WebPages_About : System.Web.UI.Page
{

    DALC _db = new DALC();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }
        string lang = Config.getLang(Page);
        DataTable dt = _db.GetPage(Utils.PageType.SummaryGoals, lang);
        if (dt.Rows.Count < 1)
        {
            Config.Rd("/error?2");
        }

        ltrTitle.Text = dt.Rows[0]["title"].ToParseStr();
        ltrContent.Text = dt.Rows[0]["content"].ToParseStr();
        // ltrDate.Text = dt.Rows[0]["pageDt"].ToParseStr();
        if (dt.Rows[0]["type_id"].ToParseInt() == (int)Utils.PageType.Videos)
        {

            string videoId = dt.Rows[0]["video_url"].ToParseStr().Split(new string[] { "v=" }, StringSplitOptions.None)[1];
            string url = "https://www.youtube.com/embed/" + videoId;
            ltrVideo.Text = " <div class='embed-responsive embed-responsive-16by9'><iframe class='embed-responsive-item' src='" + url + "' allowfullscreen></iframe> </div>";

        }


        Page.Title = Config.HtmlRemoval.StripTagsRegex(dt.Rows[0]["title"].ToParseStr()) + " - " + Config.GetAppSetting("ProjectName");

        rptGoals.DataSource = _db.GetGoals();
        rptGoals.DataBind();

        ltrBreadCrumb.Text = string.Format("<a href=\"/{0}/home\"> {1}</a> / {2} ",
            lang,
            DALC.GetStaticValue("home_breadcrumb_title"),
           Config.HtmlRemoval.StripTagsRegex(ltrTitle.Text));


        shareBox(Config.HtmlRemoval.StripTagsRegex(ltrTitle.Text),
         "summary goals",
         ltrContent.Text,
         "");

    }

    private void shareBox(string pageTitle, string PageType, string Description, string image_url)
    {
        Description = HttpUtility.HtmlDecode(Config.HtmlRemoval.StripTagsRegex(Description));

        string pageUrl = Request.Url.ToString();
        share_text.Text = DALC.GetStaticValue("share_text");
        shareFb.NavigateUrl = "http://www.facebook.com/sharer.php?u=" + pageUrl;
        shareTwt.NavigateUrl = "https://twitter.com/share?url=" + pageUrl;
        shareMail.NavigateUrl = string.Format("mailto:?subject={0}&body={1}", pageTitle, pageUrl);
        shareLinkedin.NavigateUrl = string.Format("http://www.linkedin.com/shareArticle?mini=true&url={0}&title={1}&summary={2}&source={0}", pageUrl, pageTitle, Description);


        #region Google

        //Header.Controls.Add(new HtmlMeta
        //{
        //    Name = "keywords",
        //    Content = ""
        //});

        Header.Controls.Add(new HtmlMeta
        {
            Name = "description",
            Content = Description
        });

        #endregion

        #region Facebook

        Header.Controls.Add(new HtmlMeta
        {
            Name = "og:title",
            Content = pageTitle
        });

        Header.Controls.Add(new HtmlMeta
        {
            Name = "og:type",
            Content = PageType
        });

        Header.Controls.Add(new HtmlMeta
        {
            Name = "og:image",
            Content = image_url
        });
        Header.Controls.Add(new HtmlMeta
        {
            Name = "og:description",
            Content = Description
        });



        #endregion

        #region Twitter

        Header.Controls.Add(new HtmlMeta
        {
            Name = "twitter:card",
            Content = "summary"
        });

        Header.Controls.Add(new HtmlMeta
        {
            Name = "twitter:title",
            Content = pageTitle
        });

        Header.Controls.Add(new HtmlMeta
        {
            Name = "twitter:description",
            Content = Description
        });
        Header.Controls.Add(new HtmlMeta
        {
            Name = "twitter:image",
            Content = image_url
        });



        #endregion

    }
}