﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Constitution.aspx.cs" Inherits="WebPages_Constitution" %>


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
        //    $('.slider-cons-list li a').eq(0).click();
        //});
        //$(document).on('click', '.slider-cons-list li a', function () {
        //    _dataId = $(this).attr('data-id');
        //    $('.content-tab').removeClass('active');
        //    $('.slider-cons-list li').removeClass('active');
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
                        <ul class="slider-cons-list" style="height: 400px; overflow-y: scroll;">
                            <asp:Repeater ID="rptPosts" runat="server">
                                <ItemTemplate>
                                    <li>
                                        <a href="<%#string.Format("/{0}/constitution/{1}/{2}",Config.getLang(Page),Eval("id"),Config.Slug(Eval("title_"+Config.getLang(Page)).ToParseStr())) %>" data-id="<%#Eval("id") %>">
                                            <div>
                                                <%#Eval("title_"+Config.getLang(Page)) %>
                                            </div>
                                        </a>

                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                </div>
                <div class="col-md-9 ">
                    <div class="borderAll paddingLR10 ">
                        <h2 class="title-bottom" style="font-size: 24px; padding: 4px 5px 5px 5px;">
                            <asp:Literal ID="ltrPageTitle" Text="" runat="server" />
                        </h2>
                        <div class="title-line"></div>
                        <div class="content-tab active">
                            <h2 style="font-size: 14px; font-weight: bold">
                                <asp:Literal ID="ltrTitle" Text="" runat="server" />
                            </h2>
                            <div class="">
                                <asp:Literal ID="ltrContent" Text="" runat="server" />
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
            </div>
        </div>
    </section>


</asp:Content>




