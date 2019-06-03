<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchGeneral.aspx.cs" Inherits="WebPages_SearchGeneral" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">

    <section id="slider-content">
        <div class="container main-container">
            <div class="breadcrumb ">
                <asp:Literal ID="ltrBreadCrumb" Text="" runat="server" />
            </div>
            <div class="row">

                <div class="col-md-12">
                    <div class="borderAll paddingLR10">
                        <div class="accordion-option">
                            <div class="clearfix">
                                <h3 class="title" style="font-size: 24px">
                                    <asp:Literal ID="ltrPageName" Text="" runat="server" />
                                </h3>
                            </div>
                            <asp:Panel ID="pnlSearch" runat="server">
                                <br />
                                <asp:Literal ID="ltrSearchLabel" Text=" " runat="server" />
                                <asp:Label ID="ltrSearchText" Text="" runat="server" Font-Bold="true" />
                                <br />
                                <asp:Literal ID="ltrSearchResultLabel" Text=" " runat="server" Visible="false" />
                                <asp:Label ID="ltrSearchResult" Text="" runat="server" Font-Bold="true" Visible="false"/>
                            </asp:Panel>
                            <br /><br />
                             <asp:Label ID="lblSearchInfo" Text="" runat="server"  ForeColor="red" />

                        </div>
                        <div class="clearfix"></div>
                        <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">


                            <asp:Repeater ID="rptSearchResult" runat="server">
                                <ItemTemplate>
                                    <div class="panel panel-default">
                                        <div class="panel-heading" role="tab" id="headingOne">
                                            <h4 class="panel-title">
                                                <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapse-<%#Eval("group_id") %>"
                                                    aria-expanded="true" aria-controls="collapseOne"><%#Eval("group_name_"+Config.getLang(Page)) %>
                                                </a>
                                            </h4>
                                        </div>
                                        <div id='collapse-<%#Eval("group_id") %>' class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne">
                                            <div class="panel-body">
                                                <ul>
                                                    <asp:Repeater runat="server" ID="rptSearchItems">
                                                        <ItemTemplate>
                                                            <li style="margin-bottom: 10px;">
                                                                <a href='<%#GenerateURL(Eval("id").ToParseInt(),Eval("title_"+Config.getLang(Page)).ToParseStr(),Eval("type_id").ToParseStr(),Eval("page_id").ToParseInt(),Eval("goal_id").ToParseInt()) %>'>
                                                                    <%#Config.HtmlRemoval.StripTagsRegex( Eval("title_"+Config.getLang(Page)).ToParseStr()) %>
                                                                </a>
                                                                <div>
                                                                    <%# ContentText( Eval("content_"+Config.getLang(Page)).ToParseStr()) %>
                                                                </div>
                                                            </li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ul>
                                            </div>
                                        </div>
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

