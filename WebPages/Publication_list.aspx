<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Publication_list.aspx.cs" Inherits="WebPages_Publication_list" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .content-tab {
            display: none;
        }

            .content-tab.active {
                display: block;
            }

        /*.active {
            background-color: #f7f7f7;
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
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
                        <div class="col-md-3">
                            <div class="borderAll paddingLR10">
                                <ul class="publication-list">
                                    <li>
                                        <asp:HyperLink ID="linkResearch" runat="server" />
                                    </li>
                                    <li class="active">
                                        <asp:HyperLink ID="linkPublications" runat="server" />
                                        <ul class="publication-category">
                                            <asp:Repeater ID="rptPubCategory" runat="server">
                                                <ItemTemplate>
                                                    <li><a href="<%#string.Format("/{0}/publication/list/{1}",Config.getLang(Page),Eval("id")) %>"><%#Eval("name_"+Config.getLang(Page)) %></a></li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </li>
                                </ul>
                                <div class="left-box-info">
                                    <asp:Literal ID="ltrLeftInfo" Text="" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-9 border-left">
                            <div class="borderAll paddingLR10">
                                <h2 style="font-size: 24px; margin-top: 24px; margin-bottom: 14px;">
                                    <asp:Literal ID="lblPageTitle" Text="" runat="server" />
                                </h2>
                                <div class="title-line"></div>


                                <div class="publication-data-list">
                                    <asp:Repeater ID="rptPosts" runat="server">
                                        <ItemTemplate>

                                            <a
                                                href="<%# string.Format("/{0}/publication/{1}/{2}",Config.getLang(Page),  Eval("id"),Config.Slug( Eval("title_"+Config.getLang(Page)).ToParseStr())) %>">
                                                <div class="row">
                                                    <div class="col-md-2">
                                                        <img class="img-responsive" src="/uploads/pages/<%#Eval("image_filename_"+Config.getLang(Page)) %>" alt="" />
                                                    </div>
                                                    <div class="col-md-10">
                                                        <div style="font-size: 20px;"><%# Eval("title_"+Config.getLang(Page)) %></div>
                                                        <div style="font-size: 14px; color: #c4c4c4"><%# Eval("pageDt") %></div>
                                                        <p>
                                                            <%# getContent(Eval("content_"+Config.getLang(Page)).ToParseStr()) %>
                                                        </p>
                                                    </div>
                                                    <div class="col-md-12" style="text-align: right">
                                                        <div class="more-btn">
                                                            <%=DALC.GetStaticValue("publication_more") %>
                                                        </div>
                                                    </div>
                                                </div>
                                            </a>

                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                                <div class="pagination-wrapper">
                                    <ul class="pagination">
                                        <asp:Repeater ID="rptPager" runat="server">
                                            <ItemTemplate>
                                                <li>
                                                    <asp:LinkButton ID="lnkPager" OnClick="lnkPager_Click" CssClass='<%#Eval("cssClass") %>' Text='<%#Eval("name") %>' CommandArgument='<%#Eval("num") %>' runat="server" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



