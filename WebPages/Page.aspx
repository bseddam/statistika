<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Page.aspx.cs" Inherits="WebPages_Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <section>
        <div class="container main-container">
            <div class="breadcrumb not-printable">
                <asp:Literal ID="ltrBreadCrumb" Text="" runat="server" />
            </div>
            <div class="row">
                <div class="col-md-12">
                    <div class="borderAll paddingLR10">
                        <h2 class="content-title border-bottom" style="padding-bottom: 7px;">
                            <asp:Literal ID="ltrTitle" Text="" runat="server" />
                        </h2>
                        <div class="content-date">
                            <asp:Literal ID="ltrDate" Text="" runat="server" />
                        </div>
                        <div class="content-desc">
                            <asp:Literal ID="ltrContent" Text="" runat="server" />
                        </div>
                        <div class="col-md-6">
                            <asp:Literal ID="ltrVideo" Text="" runat="server" />
                        </div>
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
