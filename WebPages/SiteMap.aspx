<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SiteMap.aspx.cs" Inherits="WebPages_SiteMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">

    <section>
        <div class="container">

            <div class="panel">
                <div class="panel-body">
                    <div class="col-md-12">
                        <h3>
                            <asp:Literal ID="ltrPageTitle" Text="text" runat="server" />
                        </h3>
                        <ul class="tree-sitemap">
                            <asp:Repeater ID="rptHeaderMenu" runat="server">
                                <ItemTemplate>

                                    <li>
                                        <a
                                            href=" <%#string.Format("/{0}/{1}",Config.getLang(Page), Eval("URL")) %>">
                                            <%#Eval("Name") %>


                                            <%#Eval("SubCount").ToParseInt()>0?" <ul >":"" %>
                                            <asp:Repeater ID="rptHeaderSub" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                            <a href=" <%#string.Format("/{0}/{1}",Config.getLang(Page), Eval("URL")) %>">
                                                            <%#Eval("Name") %>
                                                        </a>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <%#Eval("SubCount").ToParseInt()>0?" </ul>":"" %>
                                        </a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>


                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

