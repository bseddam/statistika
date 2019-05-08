<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GoalInfo.aspx.cs" Inherits="WebPages_GoalInfo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script>
        $(function () {
            hash = window.location.hash;
            $('.goal-info-nav li a[href="' + hash + '"]').click();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <section>
        <div class="container main-container">
            <div class="breadcrumb not-printable">
                <asp:Literal ID="ltrBreadCrumb" Text="" runat="server" />
            </div>
            <div class="row">
                <div class="col-md-8 ">
                    <div class="borderAll paddingLR10">
                        <div class="content-title" style="font-size: 24px; padding-bottom: 18px;">
                            <asp:Literal ID="ltrTitle" Text="" runat="server" />
                        </div>
                        <div class="content-goal-title border-top">

                            <div class="myclass " style="display: inline-block; vertical-align: middle">
                                <asp:Image ID="imgGoal" runat="server" Width="95px" Style="padding: 2px;" />
                            </div>
                            <div class="myclass" style="width: 80%; display: inline-block; vertical-align: middle; font-size: 18px; color: #808080;">
                                <span>
                                    <asp:Literal ID="lblGoalName" Text="" runat="server" />
                                </span>
                            </div>

                        </div>
                        <div class="goal-info-container">
                            <asp:Literal ID="lblGoalInfo" Text="" runat="server" />

                        </div>

                        <div class="goal-info-tab-container">
                            <ul class="goal-info-nav nav  nav-justified">
                                <li role="presentation" class="active" data-content="tab3">
                                    <a href="#tab3">
                                        <asp:Literal ID="ltrtab3" Text="" runat="server" />
                                    </a>
                                </li>
                                <li role="presentation" data-content="tab1">
                                    <a href="#tab1">
                                        <asp:Literal ID="ltrtab1" Text="" runat="server" />
                                    </a>
                                </li>
                                <li role="presentation" data-content="tab2">
                                    <a href="#tab2">
                                        <asp:Literal ID="ltrtab2" Text="" runat="server" />
                                    </a>
                                </li>

                            </ul>
                            <div data-content="tab3" class="tab-content active ">
                                <asp:Literal ID="lblFacts" Text="" runat="server" />
                            </div>
                            <div data-content="tab1" class="tab-content  ">
                                <ul>
                                    <asp:Repeater ID="rptTargets" runat="server">
                                        <ItemTemplate>
                                            <li>
                                                <%#string.Format("{0}.{1}",Eval("code"),Eval("name_"+Config.getLang(Page))) %>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                            <div data-content="tab2" class="tab-content ">
                                <ul>
                                    <asp:Repeater ID="rptIndicators" runat="server">
                                        <ItemTemplate>
                                            <li>
                                                <%#string.Format("{0} {1}",Config.ClearIndicatorCode( Eval("code").ToParseStr()), Eval("name_"+Config.getLang(Page))) %>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                        </div>
                        <br />
                        <div class="share-box">
                            <asp:Literal ID="share_text" Text="" runat="server" />
                            <div class="social-icons">
                                <ul>
                                    <li>
                                        <a href="javascript:void(0)" class="print-page">
                                            <i class="fa fa-print" aria-hidden="true"></i>
                                        </a>
                                    </li>
                                    <li>

                                        <asp:HyperLink ID="shareLinkedin" runat="server" Target="_blank">
                                                <i class="fa fa-linkedin" aria-hidden="true"></i>
                                        </asp:HyperLink>

                                    </li>
                                    <li>

                                        <asp:HyperLink ID="shareFb" runat="server" Target="_blank">
                                                <i class="fa fa-facebook" aria-hidden="true"></i>
                                        </asp:HyperLink>

                                    </li>
                                    <li>
                                        <asp:HyperLink ID="shareTwt" runat="server" Target="_blank">
                                            <i class="fa fa-twitter" aria-hidden="true"></i>
                                        </asp:HyperLink>
                                    </li>
                                    <li>
                                        <asp:HyperLink ID="shareMail" runat="server">
                                            <i class="fa fa-envelope-o" aria-hidden="true"></i>
                                        </asp:HyperLink>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>

                </div>
                <div class="col-md-4 not-printable">
                    <div class="borderAll paddingLR10">
                        <div class="about-side-bar-title">
                            <img src="/images/about-title-<%=Config.getLang(Page) %>.png" alt="" />
                        </div>
                        <div class="about-side-goal-list" style="height: 900px; overflow-y: scroll">
                            <ul>
                                <asp:Repeater ID="rptGoals" runat="server">
                                    <ItemTemplate>
                                        <li class="<%#Eval("id").ToParseStr()==Page.RouteData.Values["goalId"].ToParseStr()?"active":"" %>">
                                            <a href="<%#string.Format("/{0}/goal-info/{1}/{2}",Config.getLang(Page),Eval("id"),Config.Slug(Eval("name_short_"+Config.getLang(Page)).ToParseStr()))%>">
                                                <div class="row goal-list-item-container">

                                                    <div class="col-md-4">
                                                        <div class="about-side-img">
                                                            <img class="img-responsive" src="/images/goals-<%=Config.getLang(Page) %>/goal-<%#Eval("id").ToParseStr().PadLeft(2,'0') %>.png" alt="" />
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
            </div>
        </div>
    </section>
</asp:Content>



