<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="WebPages_News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <section>
        <div class="container main-container">
            <div class="breadcrumb not-printable">
                <asp:Literal ID="ltrBreadCrumb" Text="" runat="server" />
            </div>
            <div class="row">
                <div class="col-md-3 not-printable">
                    <div class="borderAll paddingLR10">
                        <div class="news-content" style="border-right: unset; padding-top: 10px;">
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
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="col-md-9">
                    <div class="borderAll paddingLR10">
                        <div class="row content-popup-image">
                            <div class="col-md-12">
                                <h2 class="content-title">
                                    <asp:Literal ID="ltrTitle" Text="" runat="server" />
                                </h2>
                                <div class="content-date">
                                    <asp:Literal ID="ltrDate" Text="" runat="server" />
                                </div>
                                <div class="content-desc">
                                    <asp:Literal ID="ltrContent" Text="" runat="server" />
                                </div>
                                <div class="col-md-6">
                                    <asp:Literal ID="ltrVideo" Text="" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="page-gallery-container">
                            <asp:Panel ID="pnlImages" runat="server">

                                <h5 class="border-bottom" style="padding-bottom: 10px;">
                                    <asp:Literal ID="PagesImagesTitle" Text="" runat="server" />

                                </h5>

                                <asp:Repeater ID="rptImages" runat="server">
                                    <ItemTemplate>
                                        <a class="page-gallery-item" href="/uploads/pages/<%#Eval("name") %>">
                                            <img src="/uploads/pages/<%#Eval("name") %>" />
                                        </a>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <br />
                                <br />
                            </asp:Panel>

                        </div>
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
            </div>


        </div>
    </section>
</asp:Content>

