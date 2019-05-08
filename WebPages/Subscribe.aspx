<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Subscribe.aspx.cs" Inherits="WebPages_Subscribe" %>



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
                        <div class="col-md-6 ">
                            <div class="borderAll  contact-section" style="height: 370px;">
                                <div class="contact-section-title">
                                    <i class="fa fa-envelope"></i>
                                    <asp:Literal ID="ltrSendLetter" Text="" runat="server" />
                                </div>
                                <div class="contact-form">
                                    <asp:TextBox runat="server" ID="txtEmail" placeholder="Email" CssClass="form-control" ValidationGroup="email" />
                                    <asp:RegularExpressionValidator ID="CheckEmail" runat="server" ValidationGroup="email" ControlToValidate="txtEmail" ErrorMessage="RegularExpressionValidator" ForeColor="Red" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                    <asp:TextBox runat="server" ID="txtname" placeholder="" CssClass="form-control" />
                                    <asp:TextBox runat="server" ID="txtsurname" placeholder="" CssClass="form-control" />
                                    <asp:TextBox runat="server" ID="txtAtaadi" placeholder="" CssClass="form-control" />

                                    <asp:Label ID="lblMessage" Text="" runat="server" />
                                    <div style="text-align: right">
                                        <asp:Button ID="btnSend" Text="Send"
                                            runat="server"
                                            CssClass="btn btn-primary"
                                            OnClientClick="this.style.display='none';document.getElementById('loading').style.display='block';this.style.display='block';"
                                            OnClick="btnSend_Click" ValidationGroup="email" />
                                        <div id="loading" style="display: none">
                                            <img src="/images/loading.gif" alt="loading " style="max-height: 50px;" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6 ">
                            <div class="borderAll  contact-section" style="height: 370px;">
                                <asp:Label ID="LocationText" style="margin-top:150px;display:inline-block" Text="" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>




