<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Unsubscribe.aspx.cs" Inherits="WebPages_Unsubscribe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
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

                        <div class="content-desc">
                            <asp:Literal ID="ltrContent" Text="" runat="server" />
                        </div>
                        <asp:Label ID="lblError" Text="" runat="server" />
                        <asp:Label ID="lblSuccess" Text="" runat="server" />
                        <asp:Button ID="BtnConfirm" Text="" OnClick="BtnConfirm_Click" runat="server" CssClass="btn btn-success" />
                        <br />
                        <br />
                    </div>

                </div>

            </div>
        </div>
    </section>
</asp:Content>


