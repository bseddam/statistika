<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NationalReports.aspx.cs" Inherits="WebPages_NationalReports" %>

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
    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>

            <section>
                <div class="container main-container ">
                    <div class="breadcrumb not-printable">
                        <asp:Literal ID="ltrBreadCrumb" Text="" runat="server" />
                    </div>
                    <div class="borderAll paddingLR10">
                        <div class="row">
                            <div class="col-md-12  ">
                                <h2 style="font-size: 24px; margin-top: 24px; margin-bottom: 14px;">
                                    <asp:Literal ID="lblPageTitle" Text="" runat="server" />


                                </h2>
                                <div class="title-line"></div>
                            </div>
                        </div>
                        <asp:Panel ID="pnlContent" runat="server">

                            <div style="font-size: 20px; margin-bottom: 20px;">
                                <asp:Literal ID="lblContentTitle" Text="" runat="server" />
                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Image ID="imgContent" runat="server" CssClass="img-responsive" />
                                    <div class="share-box" style="background: transparent; padding-left: 0;">
                                                <div class="social-icons" style="float: initial">
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
                                <div class="col-md-9">
                                    <div style="margin-bottom: 10px">
                                        <asp:Label ID="lblDate" Text="" runat="server" Style="color: #7a7a7a; margin-bottom: 5px" />

                                    </div>
                                    <asp:Literal ID="lblContent" Text="" runat="server" />

                                    <div style="margin: 10px 0">
                                        <asp:Label ID="linkDownloadReport" Text="Hesabatı yüklə" runat="server" Font-Size="16px" />
                                    </div>
                                    <asp:Panel runat="server" ID="pnlTamMaterial" Style="margin-bottom: 10px">
                                        <i class="fa fa-download"></i>
                                        <asp:HyperLink ID="linkTamMaterial" Text="Tam material" runat="server" ForeColor="#000000" />
                                    </asp:Panel>

                                    <asp:Panel runat="server" ID="pnlShortMaterial" Style="margin-bottom: 10px">
                                        <i class="fa fa-download"></i>
                                        <asp:HyperLink ID="linkShortMaterial" Text="Xülasə" runat="server" ForeColor="#000000" />
                                    </asp:Panel>
                                </div>
                            </div>


                        </asp:Panel>

                        <div class="col-md-10 col-md-push-1 ">
                            <asp:Repeater ID="rptPosts" runat="server">
                                <ItemTemplate>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <img class="img-responsive" src="/uploads/pages/<%#Eval("image_filename_"+Config.getLang(Page)) %>" />
                                        </div>
                                        <div class="col-md-10">
                                            <div style="font-size: 20px;"><%# Eval("title_"+Config.getLang(Page)) %></div>
                                            <p>
                                                <%# getContent(Eval("content_"+Config.getLang(Page)).ToParseStr()) %>
                                            </p>
                                        </div>
                                        <div class="col-md-12" style="text-align: right">
                                            <a 
                                                href="<%# string.Format("/{0}/national-reports/{1}/{2}",Config.getLang(Page), Eval("id"),Config.Slug( Eval("title_"+Config.getLang(Page)).ToParseStr())) %>">

                                                <div class="list-more-btn" style="background: linear-gradient( 59deg, #168BCD, #36D1DC);">
                                                    <%=DALC.GetStaticValue("publication_more") %>
                                                </div>
                                            </a>
                                        </div>
                                        <div class="col-md-12 border-bottom" style="margin-top: 5px;"></div>

                                    </div>
                                    <br />
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <div class="clearfix"></div>
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


            </section>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



