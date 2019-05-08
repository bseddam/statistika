<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <section class="goals-section">
        <div class="container  main-container">
            <div class="goals-section-container">
                <div class="goal-information">
                    <h3>
                        <asp:Literal ID="home_goal_title" runat="server" />
                    </h3>
                    <h5>
                        <asp:Literal ID="home_goal_subtitle" runat="server" />
                    </h5>
                </div>

                <div class="row1 clearfix">
                    <asp:Repeater ID="rptGoals" runat="server">
                        <ItemTemplate>
                            <div class="col-md-2 col-sm-4 col-xs-6">
                                <a href="<%#string.Format("/{0}/goals/{1}/{2}/indicators", Config.getLang(Page),Eval("id"),Config.Slug( Eval("name_short_"+Config.getLang(Page)).ToParseStr())) %>">
                                    <img id="goalimagee" src="/images/goals-<%=Config.getLang(Page) %>/goal-<%#Eval("id").ToParseStr().PadLeft(2,'0') %>.png" class="img-responsive" />
                                </a>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <div class="col-md-2 col-sm-4 col-xs-6">
                        <a href="#">
                            <img id="goalimagee" src="/images/goals-<%=Config.getLang(Page) %>/goals-logo.png" class="img-responsive" />
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </section>


    <section class="boxes">
        <div class="container main-container">
            <div class="row">
                <div class="col-md-3 col-sm-6  col-xs-12">
                    <a href="<%=string.Format("/{0}/national-priority/1",Config.getLang(Page))%>" class="mybutton"><%=DALC.GetStaticValue("home_box_1") %></a>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <a href="<%=string.Format("/{0}/interesting-sides",Config.getLang(Page))%>" class="mybutton"><%=DALC.GetStaticValue("home_box_2") %></a>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">
                    <a href="<%=string.Format("/{0}/national-reports",Config.getLang(Page))%>" class="mybutton"><%=DALC.GetStaticValue("home_box_3") %></a>
                </div>
                <div class="col-md-3 col-sm-6 col-xs-12">

                    <a href="<%=string.Format("/{0}/publication-research",Config.getLang(Page))%>" class="mybutton"><%=DALC.GetStaticValue("home_box_4") %></a>
                </div>
            </div>
        </div>
    </section>

    <section class="goals">
        <div class="container main-container">
            <div class="row goal-row">
                <div class="col-md-6 col-sm-12 col-xs-12 " style="margin: 0 -3px; border-radius: 5px; border: 2px solid #f7faff; padding: 5px; padding-top: 17px;">
                    <div class="info-title text-center info-title-bg" style="font-size: 13px; padding: 5px 2px;">
                        <asp:Literal ID="home_slider_title" Text="" runat="server" />
                    </div>
                    <section id="slider">
                        <div class="">
                            <div class="owl-carousel owl-theme my-carusel">
                                <asp:Repeater ID="rptSlider" runat="server">
                                    <ItemTemplate>
                                        <div class="item" data-color='<%#Eval("color") %>'>
                                            <a href="<%="/"+Config.getLang(Page) %>/content/<%#Eval("goal_id")+"/"+Config.Slug(Eval("GoalName").ToParseStr()) %>">
                                                <img class="slider-image" src="/uploads/slider/<%#Eval("image_url_"+Config.getLang(Page)) %>" />
                                                <div class="slider-caption">
                                                    <%#DALC.GetStaticValue("goal_value")+" "+ Eval("priority")+". "+Eval("GoalName") %>
                                                </div>
                                            </a>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>
                    </section>
                </div>
                <div class="col-md-6 col-sm-12 col-xs-12 " style="padding: 0; margin-left: 5px;">
                    <div class="goal-info">
                        <div class="info-title text-center info-title-bg" style="font-size: 15px; padding: 3px 0px;">
                            <%=DALC.GetStaticValue("home_about_title") %>
                        </div>
                        <div class="info-desc" style="text-align: justify">
                            <%=DALC.GetStaticValue("home_about_content").Substring(0,950)+"..." %>
                        </div>
                        <div class="text-right">
                            <a href="<%="/"+Config.getLang(Page)+"/summary-goals" %>" class="more-info">
                                <%=DALC.GetStaticValue("read_more") %>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section id="news">
        <div class="container main-container">
            <div class="news-container">
                <div class="row">
                    <div class="col-md-6">
                        <h5 class="news-title">
                            <a href="<%="/"+Config.getLang(Page)+"/news/list" %>"><%=DALC.GetStaticValue("home_news") %> </a>
                        </h5>
                        <div class="news-content" style="border-right:none;" >
                            <ul>
                                <asp:Repeater ID="rptNews" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <a href="<%#"/"+Config.getLang(Page)+"/news/"+Eval("id") %>">
                                                <span class="news-item-title">
                                                    <%#Config.HtmlRemoval.StripTagsRegex( Eval("Title_"+Config.getLang(Page)).ToParseStr()) %>                                                </span>
                                                <span class="news-item-date">
                                                    <i class="fa fa-clock-o" aria-hidden="true"></i>
                                                    <%#Eval("pageDt") %>
                                                </span>
                                            </a>
                                        </li>
										<hr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <h5 class="news-title">
                            <a href="<%="/"+Config.getLang(Page)+"/videos/list" %>"><%=DALC.GetStaticValue("home_videos") %> </a>
                        </h5>
                        <div class="video-content">
                            <asp:HyperLink ID="VideoUrl" runat="server" />
                            <div class="embed-responsive embed-responsive-16by9">
                                <asp:Literal ID="ltrEmbed" Text="" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

