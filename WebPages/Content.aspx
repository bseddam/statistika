<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Content.aspx.cs" Inherits="WebPages_Content" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        @media print {
            .border-left {
                border: 0;
            }

            .item {
                padding: 10px;
            }
            /*#content {
               padding:0;
            }
            .owl-item {
                display:none;
            }
            .owl-item.active {
                display:block;
                width:100%;
            }*/
        }
    </style>
</asp:Content>
<asp:Content ID="fdf" ContentPlaceHolderID="script" runat="server">
    <script>
        //owl-item active

        $(document).on('click', '.print-page-content', function () {
            $('.print-content').html($('.owl-item.active .item').html());
            $('.owl-carousel').hide();
            window.print();
            $('.print-content').hide();
            $('.owl-carousel').show();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <section id="slider-content">
        <div class="container main-container">
            <div class="breadcrumb not-printable">
                <asp:Literal ID="ltrBreadCrumb" Text="" runat="server" />
            </div>
            <div class="row">
                <div class="col-md-3 not-printable">
                    <div class="borderAll paddingLR10" id="leftbar" style="overflow: hidden">
                        <div class="border-bottom">
                            <img src="/images/about-title-<%=Config.getLang(Page) %>.png" alt="" />
                        </div>
                        <ul class="slider-goal-list">
                            <asp:Repeater ID="rptGoals" runat="server">
                                <ItemTemplate>
                                    <li><a href="<%="/"+Config.getLang(Page) %>/content/<%#Eval("GoalId")+"/"+Config.Slug(Eval("GoalName").ToParseStr()) %>">

                                        <div class="content-side-image text-center">
                                            <img src="/uploads/slider/<%#Eval("image_url") %>" class="img-responsive" />
                                        </div>
                                        <div class="content-side-title">
                                            <%#DALC.GetStaticValue("goal_value")+" "+Eval("priority")+". "+ Eval("GoalName") %>
                                        </div>
                                    </a></li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <div id="loader" class="loader down">
                        <img src="/images/arrow-down.png" class="img-down" alt="load more" />
                        <img src="/images/arrow-up.png" class="img-up" alt="load more" />
                    </div>
                </div>
                <div class="col-md-9 border-left " id="content">
                    <div class="borderAll paddingLR10">
                        <h2 style="font-size: 24px;">
                            <asp:Literal ID="lblPageTitle" Text="text" runat="server" />
                        </h2>
                        <div class="content-goal-title">
                            <div class="myclass border-right" style="display: inline-block; vertical-align: middle">
                                <asp:Image ID="imgGoal" runat="server" Width="128" />
                            </div>
                            <div class="myclass" style="width: 75%; display: inline-block; vertical-align: middle; padding-left: 20px;">
                                <span>
                                    <asp:Literal ID="lblGoalName" Text="text" runat="server" />
                                </span>
                            </div>
                        </div>
                        <div class="owl-carousel owl-theme">
                            <asp:Repeater ID="rptSlider" runat="server">
                                <ItemTemplate>
                                    
                                    <div class="item">
                                        <div class="content-title" style="margin-top: 10px;">
                                            <%#Eval("title_"+Config.getLang(Page)) %>
                                        </div>
                                        <br />
                                        <img class="content-slider-image" alt='image-<%#Config.Slug( Eval("title_"+Config.getLang(Page)).ToParseStr()) %>' src="/uploads/slider/<%#Eval("image_url_"+Config.getLang(Page)) %>" />

                                        <div class="content-desc">
                                            <%#Eval("content_"+Config.getLang(Page)) %>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                        </div>
                        <div class="print-content"></div>
                        <div class="share-box">
                            <asp:Literal ID="share_text" Text="" runat="server" />
                            <div class="social-icons">
                                <ul>
                                    <li>
                                        <a href="javascript:void(0)" class="print-page-content">
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

