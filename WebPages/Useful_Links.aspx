<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Useful_Links.aspx.cs" Inherits="WebPages_UsefulLinks" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <section>
          
                <div class="container main-container">
                    <div class="breadcrumb">
                        <asp:Literal ID="ltrBreadCrumb" Text="" runat="server" />
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="borderAll paddingLR10">
                                <!-- Nav tabs -->
                                <h2>
                                    <asp:Label ID="lbltitle" runat="server"></asp:Label></h2>
                                <hr>
                                <ul class="nav nav-tabs" role="tablist">
                                    <asp:Repeater ID="rplinkheader" runat="server">
                                        <ItemTemplate>
                                            <li class="nav-item<%#Eval("active")%>">
                                                <a class="nav-link<%#Eval("active")%>" 
                                                    data-toggle="tab" href="#h<%#Eval("id")%>" aria-expanded="<%#Eval("truefalse")%>">
                                                    <%#Eval("useful_links_header_"+Config.getLang(Page))%>
                                                </a>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>


                                </ul>
                                <!-- Tab panes -->
                                <div class="tab-content" style="min-height:300px;">
                                    <asp:Literal ID="lbllinkler" runat="server"></asp:Literal>
                                  

                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

