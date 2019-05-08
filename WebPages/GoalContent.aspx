<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GoalContent.aspx.cs" Inherits="WebPages_GoalContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <section style="">
        <div class="container main-container">



            <div class="row3">
                <div class="breadcrumb">
                    <asp:Literal ID="ltrBreadCrumb" Text="" runat="server" />
                </div>

            </div>
            <div class="row indicator-container borderAll">
                <div class="col-md-12">
                    <div class="content-goal-title">

                        <div class="myclass border-right" style="display: inline-block; vertical-align: middle">
                            <asp:Image ID="imgGoal" runat="server" Width="96" />
                        </div>
                        <div class="myclass" style="width: 80%; display: inline-block; vertical-align: middle">
                            <span>
                                <asp:Literal ID="lblGoalName" Text="text" runat="server" />

                            </span>
                        </div>

                    </div>
                </div>
                <div class="col-md-12">
                    <ul >
                        <asp:Repeater ID="rptIndicators" runat="server">
                            <ItemTemplate>
                                <li class="indicator-ul">
                                    <a  href="<%#string.Format("/{0}/indicators/{1}/{2}", Config.getLang(Page),Eval("id"),Config.Slug( Eval("name_"+Config.getLang(Page)).ToParseStr())) %>">
                                        <div class="indicator-code clear">
                                            <%#Config.ClearIndicatorCode( Eval("code").ToParseStr()) %>

                                            <span class='data-collect <%#Eval("key") %>'> 
                                                <%#Eval("status_name_"+Config.getLang(Page)) %>
                                            </span>
                                        </div>
                                        <div class="indicator-line"></div>
                                        <div class="indicator-name">
                                            <%#Eval("name_"+Config.getLang(Page)) %>
                                        </div>
                                    </a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>

                    </ul>
                </div>

            </div>
        </div>
    </section>
</asp:Content>

