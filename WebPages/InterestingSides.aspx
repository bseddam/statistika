<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InterestingSides.aspx.cs" Inherits="WebPages_InterestingSides" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .content-tab {
            display: none;
        }

            .content-tab.active {
                display: block;
            }

        .active {
            background-color: #f7f7f7;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script>
        //$(function () {
        //    $('.slider-post-list li a').eq(0).click();
        //});
        //$(document).on('click', '.slider-post-list li a', function () {
        //    _dataId = $(this).attr('data-id');
        //    $('.content-tab').removeClass('active');
        //    $('.slider-post-list li').removeClass('active');
        //    $('.content-tab[data-id="' + _dataId + '"]').addClass('active');
        //    $(this).parent().addClass('active');
        //    return false;
        //});
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <section>
        <div class="container main-container">
            <div class="breadcrumb not-printable">
                <asp:Literal ID="ltrBreadCrumb" Text="" runat="server" />
            </div>
            <div class="row">
                <div class="col-md-3 borderAll not-printable">
                    <div class=" paddingLR10">


                        <div>
                            <h2 style="font-size: 22px; text-align: center; padding: 5px; background-color: #C4D0FF;">
                                <asp:Literal ID="ltrLeftBarTitle" Text="" runat="server" />
                            </h2>
                            <div class="title-line"></div>
                        </div>
                        <ul class="slider-post-list">
                            <asp:Repeater ID="rptCategory" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <a
                                            class="<%# getCategoryID==Eval("id").ToParseInt()?"active":"" %>"
                                            href="<%# string.Format("/{0}/interesting-sides/{1}/{2}",Config.getLang(Page), Eval("id"),Config.Slug( Eval("title_"+Config.getLang(Page)).ToParseStr())) %>">
                                            <%#Eval("title_"+Config.getLang(Page)) %>
                                        </a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>

                        </ul>
                        <div class="left-box-info">
                            <asp:Literal ID="ltrLeftInfo" Text="" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="col-md-9 borderAll border-left">
                    <div class=" paddingLR10">

                        <h2 style="font-size: 24px; margin-top: 24px; margin-bottom: 14px;">
                            <asp:Literal ID="lblPageTitle" Text="" runat="server" />
                        </h2>
                        <div class="title-line"></div>

                        <div>
                            <h2>
                                <asp:Literal ID="lblContentTitle" Text="" runat="server" />
                            </h2>
                            <div class="col-md-6" style="padding: 0">
                                <asp:Image ID="imgContent" runat="server" CssClass="img-responsive" />
                            </div>
                            <div class="col-md-12" style="padding: 0">
                                <asp:Literal ID="lblContent" Text="" runat="server" />

                                <br />
                                <asp:Panel ID="pnlShare" runat="server" class="share-box">
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
                                </asp:Panel>
                            </div>


                        </div>


                        <div class="">
                            <asp:Repeater ID="rptPosts" runat="server">
                                <ItemTemplate>
                                    <div class="row">
                                        <a
                                            href="<%# string.Format("/{0}/interesting-sides/{1}/{2}/{3}",Config.getLang(Page), Eval("page_id"), Eval("id"),Config.Slug( Eval("title_"+Config.getLang(Page)).ToParseStr())) %>">
                                            <div class="col-md-2">
                                                <img class="img-responsive" src="/uploads/pages/<%#Eval("Filename") %>" />
                                            </div>
                                            <div class="col-md-10">
                                                <div style="font-size: 20px;"><%# Eval("title_"+Config.getLang(Page)) %></div>
                                                <p>
                                                    <%# getContent(Eval("content_"+Config.getLang(Page)).ToParseStr()) %>
                                                </p>
                                            </div>
                                        </a>
                                    </div>
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
</asp:Content>


