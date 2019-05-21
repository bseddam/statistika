<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        System.Web.Routing.RouteCollection routes = System.Web.Routing.RouteTable.Routes;
        routes.MapPageRoute("home", "{lang}/home", "~/default.aspx");
        routes.MapPageRoute("homeOptional", "home", "~/default.aspx");
        routes.MapPageRoute("sitemap", "{lang}/sitemap", "~/webpages/sitemap.aspx");
        routes.MapPageRoute("sitemapOptional", "sitemap", "~/webpages/sitemap.aspx");

        routes.MapPageRoute("page", "{lang}/page/{pageid}", "~/webpages/page.aspx");
        routes.MapPageRoute("pageOptional", "page/{pageid}", "~/webpages/page.aspx");

        routes.MapPageRoute("reportingstatus", "{lang}/reportingstatus", "~/webpages/ReportingStatus.aspx");
        routes.MapPageRoute("reportingstatusOptional", "reportingstatus", "~/webpages/ReportingStatus.aspx");

        routes.MapPageRoute("error", "error", "~/webpages/error.aspx");
        routes.MapPageRoute("SummaryGoals", "{lang}/summary-goals", "~/webpages/SummaryGoals.aspx");

        routes.MapPageRoute("about", "{lang}/about", "~/webpages/about.aspx");
        routes.MapPageRoute("contact", "{lang}/contact", "~/webpages/contact.aspx");


        routes.MapPageRoute("compare", "{lang}/compare", "~/webpages/compare.aspx");


        routes.MapPageRoute("videoLst", "{lang}/videos/list", "~/webpages/videolist.aspx");
        routes.MapPageRoute("videoContent", "{lang}/video/{videoid}", "~/webpages/video.aspx");
        routes.MapPageRoute("videoContent1", "{lang}/video/{videoid}/{video_slug}", "~/webpages/video.aspx");

        routes.MapPageRoute("news", "{lang}/news/list", "~/webpages/newslist.aspx");
        routes.MapPageRoute("newsContent", "{lang}/news/{pageid}", "~/webpages/news.aspx");
        routes.MapPageRoute("newsContent1", "{lang}/news/{pageid}/{page_slug}", "~/webpages/news.aspx");


        routes.MapPageRoute("newsContentlist", "{lang}/order/list", "~/webpages/orderlist.aspx");
        routes.MapPageRoute("newsContent2", "{lang}/order/{pageid}", "~/webpages/order.aspx");
        routes.MapPageRoute("newsContent13", "{lang}/order/{pageid}/{page_slug}", "~/webpages/order.aspx");




        routes.MapPageRoute("sliderContent", "{lang}/content/{goalId}", "~/webpages/content.aspx");
        routes.MapPageRoute("sliderContentOpt", "{lang}/content/{goalId}/{goalName}", "~/webpages/content.aspx");

        routes.MapPageRoute("law", "{lang}/law/{pageid}/{page_slug}", "~/webpages/law.aspx");
        routes.MapPageRoute("law1", "{lang}/law/{pageid}", "~/webpages/law.aspx");



        routes.MapPageRoute("goalContent", "{lang}/goals/{goalid}/{goalname}/indicators", "~/webpages/goalcontent.aspx");
        routes.MapPageRoute("goalContent1", "{lang}/goals/{goalid}/indicators", "~/webpages/goalcontent.aspx");

        routes.MapPageRoute("indicatorsInfor", "{lang}/indicators/{indicatorid}", "~/webpages/indicatorinfo.aspx");
        routes.MapPageRoute("indicatorsInfor1", "{lang}/indicators/{indicatorid}/{indicatorname}", "~/webpages/indicatorinfo.aspx");

        routes.MapPageRoute("international", "{lang}/international-challenge/{id}/{slug}", "~/webpages/internationalchallenge.aspx");
        routes.MapPageRoute("international1", "{lang}/international-challenge", "~/webpages/internationalchallenge.aspx");

        routes.MapPageRoute("constitution", "{lang}/constitution", "~/webpages/constitution.aspx");
        routes.MapPageRoute("constitution2", "{lang}/constitution/{id}/{name}", "~/webpages/constitution.aspx");



        routes.MapPageRoute("national-priority", "{lang}/national-priority/{goalId}", "~/webpages/nationalpriority.aspx");
        routes.MapPageRoute("national-priority1", "{lang}/national-priority/{goalId}/{goalname}", "~/webpages/nationalpriority.aspx");

        routes.MapPageRoute("goal-info", "{lang}/goal-info/{goalId}", "~/webpages/goalinfo.aspx");
        routes.MapPageRoute("goal-info1", "{lang}/goal-info/{goalId}/{goalname}", "~/webpages/goalinfo.aspx");

        //routes.MapPageRoute("mechanismDefault", "{lang}/national-implementation-mechanism", "~/webpages/MechanismProgress.aspx");
        //routes.MapPageRoute("mechanism", "{lang}/national-implementation-mechanism/{id}/{slug}", "~/webpages/MechanismProgress.aspx");

        //routes.MapPageRoute("mechanismDefault", "{lang}/national-implementation-mechanism/{pageid}/{page_slug}/{id}", "~/webpages/MechanismProgress.aspx");
        //routes.MapPageRoute("mechanism", "{lang}/national-implementation-mechanism/{pageid}/{id}", "~/webpages/MechanismProgress.aspx");

        routes.MapPageRoute("mechanismDefault", "{lang}/national-implementation-mechanism", "~/webpages/nationalmechamism.aspx");
        routes.MapPageRoute("mechanism", "{lang}/national-implementation-mechanism/{id}/{slug}", "~/webpages/nationalmechamism.aspx");

        routes.MapPageRoute("mechanismDefault1", "{lang}/national-implementation-mechanism1/{pageid}/{page_slug}", "~/webpages/MechanismProgress.aspx");
        routes.MapPageRoute("mechanism1", "{lang}/national-implementation-mechanism1/{pageid}", "~/webpages/MechanismProgress.aspx");




        routes.MapPageRoute("interesting-sides", "{lang}/interesting-sides", "~/webpages/interestingsides.aspx");
        routes.MapPageRoute("interesting-sides-cat", "{lang}/interesting-sides/{catId}/{catname}", "~/webpages/interestingsides.aspx");
        routes.MapPageRoute("interesting-sides-content", "{lang}/interesting-sides/{catId}/{contentId}/{contentname}", "~/webpages/interestingsides.aspx");

        routes.MapPageRoute("national-reports", "{lang}/national-reports", "~/webpages/nationalreports.aspx");
        routes.MapPageRoute("national-reports-1", "{lang}/national-reports/{id}/{name}", "~/webpages/nationalreports.aspx");

        routes.MapPageRoute("publication", "{lang}/publication", "~/webpages/publication.aspx");
        routes.MapPageRoute("publication2", "{lang}/publication/list/{typeid}", "~/webpages/publication_list.aspx");
        routes.MapPageRoute("publication1", "{lang}/publication/{contentId}/{name}", "~/webpages/publication.aspx");

        routes.MapPageRoute("research", "{lang}/research", "~/webpages/research.aspx");
        routes.MapPageRoute("research1", "{lang}/research/{contentId}/{name}", "~/webpages/research.aspx");

        routes.MapPageRoute("register", "{lang}/register", "~/webpages/register.aspx");

        routes.MapPageRoute("PublicationResearch", "{lang}/publication-research", "~/webpages/PublicationResearch.aspx");

        routes.MapPageRoute("usefullinks", "{lang}/useful-links", "~/webpages/usefullinks.aspx");
        routes.MapPageRoute("usefullinks1", "{lang}/useful-links/{goalId}/{goalname}", "~/webpages/usefullinks.aspx");

        routes.MapPageRoute("map", "{lang}/map", "~/webpages/map.aspx");
        routes.MapPageRoute("statisticDb", "{lang}/statical-database", "~/webpages/statisticdb.aspx");

        routes.MapPageRoute("subscribe", "{lang}/subscribe", "~/webpages/subscribe.aspx");
        routes.MapPageRoute("unsubscribe", "{lang}/unsubscribe", "~/webpages/unsubscribe.aspx");

        routes.MapPageRoute("searchGeneral", "{lang}/search/general", "~/webpages/searchgeneral.aspx");
        routes.MapPageRoute("searchDetailed", "{lang}/search/detailed", "~/webpages/searchdetailed.aspx");




    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

</script>
