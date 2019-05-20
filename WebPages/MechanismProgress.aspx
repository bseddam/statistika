<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MechanismProgress.aspx.cs" Inherits="WebPages_Law" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">

    <section id="slider-content">
        <div class="container main-container">

            <div class="breadcrumb">
                <asp:Literal ID="ltrBreadCrumb" Text="" runat="server" />
            </div>
            <div class="row">
                <div class="col-md-4">
                    <div class="borderAll paddingLR10">

                        <div class="law-side-bar-title">
                            <img src="/images/about-title-<%=Config.getLang(Page) %>.png" alt="" />
                        </div>
                        <div class="law-side-goal-list" style="height: 900px; overflow-y: scroll">


                            <ul>
                                <asp:Repeater ID="rptGoals" runat="server">
                                    <ItemTemplate>
                                        <li class="<%#Eval("id").ToParseStr()==Page.RouteData.Values["pageid"].ToParseStr()?"active":"" %>">
                                            <a href="<%#string.Format("/{0}/national-implementation-mechanism1/{1}/{2}",Config.getLang(Page),Eval("id"),Config.Slug(Eval("name_"+Config.getLang(Page)).ToParseStr())) %>">
                                                <div class="row goal-list-item-container">
                                                    <div class="col-md-4">
                                                        <div class="about-side-img">
                                                            <img src="/images/goals-<%=Config.getLang(Page) %>/goal-<%#Eval("id").ToParseStr().PadLeft(2,'0') %>.png" alt="" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-8">
                                                        <div class="about-side-title">
                                                            <%#Eval("name_"+Config.getLang(Page)) %>
                                                        </div>
                                                    </div>
                                                </div>


                                            </a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>

                </div>


                <div class="col-md-8">
                    <div class="borderAll paddingLR10">


                        <div class="accordion-option">
                            <div class="clearfix">
                                <h3 class="title" style="font-size: 24px">
                                    <asp:Literal ID="ltrPageName" Text="" runat="server" />
                                </h3>
                            </div>
                            <br />
                            <br />

                            <div class="content-goal-title">

                                <div class="myclass border-right" style="display: inline-block; vertical-align: middle; padding: 5px">
                                    <asp:Image ID="imgGoal" runat="server" Width="80" />
                                </div>
                                <div class="myclass" style="width: 80%; display: inline-block; vertical-align: middle; font-size: 14px;">
                                    <span>
                                        <asp:Literal ID="lblGoalName" Text="text" runat="server" />
                                    </span>
                                </div>

                            </div>
                        </div>
                        <div class="clearfix"></div>
                        <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                            <asp:Repeater ID="rptContent" runat="server">
                                <ItemTemplate>
                                    <div class="panel panel-default">
                                        <div class="panel-heading" role="tab" id="headingOne">
                                            <h4 class="panel-title">
                                                <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapse-<%#Eval("id") %>"
                                                    aria-expanded="true" aria-controls="collapseOne">
                                                    <%#Eval("title_"+Config.getLang(Page)) %>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id='collapse-<%#Eval("id") %>' class="panel-collapse collapse " role="tabpanel" aria-labelledby="headingOne">
                                            <div class="panel-body">
                                                <%#Eval("content_"+Config.getLang(Page)) %>

                                                <div style="text-align: right">
                                                    <a target="_blank" href="<%#Eval("more_url") %>"><%=DALC.GetStaticValue("law_more_info") %></a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>


                        </div>
                    </div>
                </div>
            </div>

        </div>
    </section>

</asp:Content>

