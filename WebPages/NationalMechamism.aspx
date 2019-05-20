<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NationalMechamism.aspx.cs" Inherits="WebPages_NationalMechamism" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .content-tab {
            display: none;
        }

            .content-tab.active {
                display: block;
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
                <div class="col-md-3 not-printable">
                    <div class="borderAll paddingLR10">
                        <div>
                            <h2 style="font-size: 22px; text-align: center; padding: 5px; background-color: #C4D0FF;">
                                <asp:Literal ID="ltrLeftBarTitle" Text="" runat="server" />
                            </h2>
                            <div class="title-line"></div>
                        </div>
                        <ul class="slider-post-list">
                            <asp:Repeater ID="rptPosts" runat="server">
                                <ItemTemplate>
                                    <li class='<%#getPageId==Eval("id").ToParseInt()?"active":"" %>'>
                                        <a href="<%#string.Format("/{0}/national-implementation-mechanism/{1}/{2}",Config.getLang(Page),Eval("id"),Config.Slug(Eval("title_"+Config.getLang(Page)).ToParseStr())) %>" data-id="<%#Eval("id") %>">
                                            <div>
                                                <%#Eval("title_"+Config.getLang(Page)) %>
                                            </div>
                                        </a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                            <li class='<%=getPageId==MechanismGundelikKod?"active":"" %>'>
                                <a href="<%=string.Format("/{0}/national-implementation-mechanism1/1",Config.getLang(Page)) %>">
                                    <div>
                                        <asp:Literal ID="ltrGundelik" Text="" runat="server" />
                                    </div>
                                </a>
                            </li>

                        </ul>
                    </div>
                </div>
                <div class="col-md-9 border-left">
                    <div class="borderAll paddingLR10">
                        <%-- <div class="content-tab active" data-id="<%#Eval("id") %>">
                            <h2 style="font-size: 24px; margin-top: 24px; margin-bottom: 14px;">
                                <asp:Literal ID="lblPageTitle" Text="" runat="server" />
                            </h2>
                            <div class="title-line"></div>
                            <div class="">
                                <asp:Literal ID="lblPageContent" Text="" runat="server" />
                            </div>
                        </div>--%>
                        <asp:Repeater ID="rptPosts1" runat="server">
                            <ItemTemplate>
                                <div class=" content-popup-image" data-id="<%#Eval("id") %>">
                                    <h2 style="font-size: 24px; margin-top: 24px; margin-bottom: 14px;">
                                        <%#Eval("title_"+Config.getLang(Page)) %>
                                    </h2>
                                    <div class="title-line"></div>
                                    <div class="">
                                        <%#Eval("content_"+Config.getLang(Page)) %>
                                    </div>
                                </div>

                            </ItemTemplate>
                        </asp:Repeater>

                        <asp:Panel ID="PnlShareBox" runat="server">
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
                        </asp:Panel>

                        <asp:Panel ID="PnlGundelik" runat="server">
                            <div class=" " data-id="national-progress">
                                <h2 style="font-size: 24px; margin-top: 24px; margin-bottom: 14px;">
                                    <asp:Literal ID="Literal1" Text="" runat="server" />
                                </h2>
                                <div class="title-line"></div>
                                <div class="">
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
                                                    <div id="collapse-<%#Eval("id") %>" class="panel-collapse collapse " role="tabpanel" aria-labelledby="headingOne">
                                                        <div class="panel-body">
                                                            <%#Eval("content_"+Config.getLang(Page)) %>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>


                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </section>


</asp:Content>



