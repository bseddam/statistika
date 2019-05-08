<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VideoList.aspx.cs" Inherits="WebPages_News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <section class="container main-container">
        <div class="breadcrumb">
            <asp:Literal ID="ltrBreadCrumb" Text="" runat="server" />
        </div>
        <h2>
            <asp:Literal ID="lblTitle" Text="" runat="server" />
        </h2>
        <div class="page-list">
            <div class="row page-list">


                <asp:Repeater ID="rptNews" runat="server">
                    <ItemTemplate>
                        <div class="col-md-6 video-item">
                            <a href="<%#"/"+Config.getLang(Page)+"/video/"+Eval("id")+"/"+Eval("slug_"+Config.getLang(Page))%>">
                                <span class="page-item-title">
                                    <%#Eval("title_"+Config.getLang(Page)) %>                                                </span>
                                <span class="page-item-date">
                                    <i class="fa fa-clock-o" aria-hidden="true"></i>
                                    <%#Eval("pageDt") %>
                                </span>

                                <div class='embed-responsive embed-responsive-16by9'>
                                    <iframe class='embed-responsive-item' src='<%#getIframeURL(Eval("video_url").ToParseStr()) %>' allowfullscreen></iframe>
                                </div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>

</asp:Content>

