<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Video.aspx.cs" Inherits="WebPages_Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <section>
        <div class="container main-container">
            <div class="breadcrumb">
                <asp:Literal ID="ltrBreadCrumb" Text="" runat="server" />
            </div>
            <div class="row">
                <div class="col-md-8">
                    <div class="content-title">
                        <asp:Literal ID="ltrTitle" Text="" runat="server" />
                    </div>
                    <div class="content-date">
                        <asp:Literal ID="ltrDate" Text="" runat="server" />
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Literal ID="ltrVideo" Text="" runat="server" />
                        </div>
                    </div>

                    <div class="content-desc">
                        <asp:Literal ID="ltrContent" Text="" runat="server" />
                    </div>

                </div>

                <div class="col-md-4 ">
                    <div class="related-video-bar-title">
                        <asp:Literal ID="lblRealtedTitle" Text="" runat="server" />
                    </div>
                    <div class="row related-video-bar-list">
                        <div class="col-md-12">
                            <asp:Repeater ID="rptVideos" runat="server">
                                <ItemTemplate>
                                    <div class="row ">
                                        <a target="_blank" href="<%#"/"+Config.getLang(Page)+"/video/"+Eval("id")+"/"+Eval("slug_"+Config.getLang(Page))%>">
                                            <div class="col-md-5">
                                                <div class='embed-responsive embed-responsive-4by3'>
                                                    <iframe class='embed-responsive-item' src='<%#getIframeURL(Eval("video_url").ToParseStr()) %>' allowfullscreen></iframe>
                                                </div>
                                            </div>
                                            <div class="col-md-7">
                                                <%#Eval("title_"+Config.getLang(Page)) %>
                                            </div>
                                        </a>


                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

