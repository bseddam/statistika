<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UsefulLinks.aspx.cs" Inherits="WebPages_UsefulLinks" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <section>
                <div class="container main-container">
                    <div class="breadcrumb">
                        <asp:Literal ID="ltrBreadCrumb" Text="" runat="server" />
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="borderAll paddingLR10">
                                <h2 class="content-title border-bottom" style="padding-bottom: 7px;">
                                    <asp:Literal ID="ltrTitle" Text="" runat="server" />
                                </h2>
                                <ul class="useful-link-nav nav  nav-justified">
                                    <li role="presentation" data-content="tab3">
                                        <asp:LinkButton ID="btnBeynalxalq" CommandArgument="0" OnClick="btnBeynalxalq_Click"  Text="" runat="server" />
                                    </li>
                                    <li role="presentation" data-content="tab1">
                                        <asp:LinkButton ID="btnMilli" CommandArgument="1" OnClick="btnBeynalxalq_Click" Text="" runat="server" />
                                    </li>
                                </ul>

                                <br />
                                <ul class="useful-link-nav nav  nav-justified">
                                    <li role="presentation" data-content="tab3">
                                        <asp:LinkButton ID="btnTarget" CommandArgument="target" OnClick="btnTarget_Click" Text="" runat="server" />
                                    </li>
                                    <li role="presentation" data-content="tab1">
                                        <asp:LinkButton ID="btnIndicator" CommandArgument="indicator" OnClick="btnTarget_Click" Text="" runat="server" />
                                    </li>
                                </ul>
                                <div class="useful-link-goal">
                                    <ul id="slider">
                                        <asp:Repeater ID="rptGoals" runat="server">
                                            <ItemTemplate>
                                                <li>
                                                    <a href="<%#string.Format("/{0}/useful-links/{1}/{2}",Config.getLang(Page),Eval("id"),Config.Slug(Eval("name_short_"+Config.getLang(Page)).ToParseStr()))%>">
                                                        <img src="/images/goals-<%=Config.getLang(Page) %>/goal-<%#Eval("id").ToParseStr().PadLeft(2,'0') %>.png" alt="" />
                                                    </a>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>
                                </div>
                                <div class="content-goal-title border-top">
                                    <div class="myclass " style="display: inline-block; vertical-align: middle">
                                        <asp:Image ID="imgGoal" runat="server" Width="95px" />
                                    </div>
                                    <div class="myclass" style="width: 80%; display: inline-block; vertical-align: middle; font-size: 18px; color: #808080;">
                                        <span>
                                            <asp:Literal ID="lblGoalName" Text="" runat="server" />
                                        </span>
                                    </div>
                                </div>

                                <asp:Panel ID="pnlIndicator" runat="server" CssClass="grid-targets-container" Visible="false">
                                    <table style="width: 100%">
                                        <thead>
                                            <tr>
                                                <td >
                                                    <asp:Literal ID="ltrIndicatorColumn1" runat="server" />
                                                </td>
                                                <td class="border-left" style="width: 30%">
                                                    <asp:Literal ID="ltrIndicatorColumn2" runat="server" />
                                                </td>
                                                <td class="border-left" style="width: 30%">
                                                    <asp:Literal ID="ltrIndicatorColumn3" runat="server" />
                                                </td>
                                            </tr>
                                        </thead>

                                        <asp:Repeater ID="rptIndicators" runat="server">
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <%#string.Format("{0}. {1}",Config.ClearIndicatorCode(Eval("IndicatorCode").ToParseStr()), Eval("IndicatorName")) %>
                                                    </td>
                                                    <td class="border-left">
                                                        <%#Eval("concat_qurum").ToParseStr().TrimEnd(',') %>
                                                    </td>
                                                    <td class="border-left">
                                                            <%#Eval("concat_partner") %>
                                                        
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </table>
                                </asp:Panel>

                                <asp:Panel ID="pnlTargets" runat="server" CssClass="grid-targets-container">
                                    <table style="width: 100%">
                                        <thead>
                                            <tr>
                                                <td style="width: 60%">
                                                    <asp:Literal ID="lblTargetColumn1" runat="server" />
                                                </td>
                                                <td class="border-left">
                                                    <asp:Literal ID="lblTargetColumn2" runat="server" />
                                                </td>
                                            </tr>
                                        </thead>
                                        <tr>
                                            <td>
                                                <asp:Literal ID="lblTargetColumn1_result" runat="server" />
                                            </td>
                                            <td class="border-left">
                                                <asp:Repeater ID="rptTargetsColumn2" runat="server">
                                                    <ItemTemplate>
                                                        <a href='<%#Eval("link") %>' target="_blank">
                                                            <%#Eval("name_"+Config.getLang(Page)) %>
                                                        </a>
                                                        <br />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </div>

                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

