<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PageList.aspx.cs" Inherits="WebPages_News" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>


            <section class="container main-container">
                <div class="breadcrumb">
                    <asp:Literal ID="ltrBreadCrumb" Text="" runat="server" />
                </div>
                <h2 class="border-bottom" style="padding-bottom: 5px">
                    <asp:Literal ID="lblTitle" Text="" runat="server" />
                </h2>
                <div class="page-list">
                    <ul>
                        <asp:Repeater ID="rptNews" runat="server">
                            <ItemTemplate>
                                <li>
                                    <a href="<%#"/"+Config.getLang(Page)+"/"+getPageType.ToParseStr().ToLower()+"/"+Eval("id")+"/"+Eval("slug_"+Config.getLang(Page))%>">
                                        <span class="page-item-title">
                                            <%#Eval("title_"+Config.getLang(Page)) %>                                                </span>
                                        <span class="page-item-date">
                                            <i class="fa fa-clock-o" aria-hidden="true"></i>
                                            <%#Eval("pageDt") %>
                                        </span>
                                    </a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>

                    </ul>
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
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

