<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="WebPages_Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="container main-container">
                <div class="borderAll">


                    <div class="panel panel-login" style="margin-bottom: 0">
                        <div class="panel-heading">
                            <div class="row">
                                <div class="col-xs-12 " style="font-size: 20px">
                                    <asp:Label ID="lblTitle" Text="" runat="server" />
                                </div>
                            </div>
                            <hr />
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblAd" Text="" runat="server" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                    ControlToValidate="txtAd"
                                                    runat="server"
                                                    ErrorMessage="*"
                                                    SetFocusOnError="True"
                                                    ForeColor="red"></asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtAd" TabIndex="1" CssClass="form-control" placeholder="saa" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblTelIsh" Text="" runat="server" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                                                    ControlToValidate="txtTelIsh"
                                                    runat="server"
                                                    ErrorMessage="*"
                                                    SetFocusOnError="True"
                                                    ForeColor="red"></asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtTelIsh" TabIndex="2" CssClass="form-control" placeholder="Password" />

                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblSoyad" Text="" runat="server" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9"
                                                    ControlToValidate="txtSoyad"
                                                    runat="server"
                                                    ErrorMessage="*"
                                                    SetFocusOnError="True"
                                                    ForeColor="red"></asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtSoyad" TabIndex="1" CssClass="form-control" placeholder="saa" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblTelM" Text="" runat="server" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                                    ControlToValidate="txtTelMobil"
                                                    runat="server"
                                                    ErrorMessage="*"
                                                    SetFocusOnError="True"
                                                    ForeColor="red"></asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtTelMobil" TabIndex="2" CssClass="form-control" placeholder="Password" />

                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblAtaAdi" Text="" runat="server" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10"
                                                    ControlToValidate="txtAtaAdi"
                                                    runat="server"
                                                    ErrorMessage="*"
                                                    SetFocusOnError="True"
                                                    ForeColor="red"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtAtaAdi" runat="server" TabIndex="2" CssClass="form-control" placeholder="Password" />
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblEmail" Text="sdfsd" runat="server" />
                                                <span data-toggle="tooltip" data-placement="right" style="color: #4c9de3;" title="<%=DALC.GetStaticValue("register_email_info") %>">
                                                    <i class="fa fa-info-circle"></i>
                                                </span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6"
                                                    ControlToValidate="txtEmail"
                                                    runat="server"
                                                    ErrorMessage="*"
                                                    SetFocusOnError="True"
                                                    ForeColor="red"></asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtEmail" TabIndex="2" CssClass="form-control" placeholder="Password" />
                                                <asp:RegularExpressionValidator ID="checkEmail" runat="server" ControlToValidate="txtEmail" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblVezife" Text="" runat="server" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                                    ControlToValidate="txtvezife"
                                                    runat="server"
                                                    ErrorMessage="*"
                                                    SetFocusOnError="True"
                                                    ForeColor="red"></asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtvezife" runat="server" TabIndex="2" CssClass="form-control" placeholder="Password" />

                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblPass" Text="sdfsd" runat="server" />
                                                <span data-toggle="tooltip" data-placement="right" style="color: #4c9de3;" title="<%=DALC.GetStaticValue("register_pass_info") %>">
                                                    <i class="fa fa-info-circle"></i>
                                                </span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7"
                                                    ControlToValidate="txtPass"
                                                    runat="server"
                                                    ErrorMessage="*"
                                                    SetFocusOnError="True"
                                                    ForeColor="red"></asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtPass" TabIndex="2" CssClass="form-control"
                                                    placeholder="Password"
                                                    TextMode="Password" />
                                                <%--                                                <asp:Label ID="lblPassHelper" CssClass="text-muted" Visible="false" runat="server" />--%>
                                            </div>

                                        </div>

                                    </div>

                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblQurum" Text="" runat="server" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                                                    ControlToValidate="txtQurumKod"
                                                    runat="server"
                                                    ErrorMessage="*"
                                                    SetFocusOnError="True"
                                                    ForeColor="red"></asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtQurumKod" TabIndex="2" CssClass="form-control" placeholder="Password" />
                                                <%--                                                <asp:Label ID="lblQurumHelper" CssClass="text-muted" Visible="false" runat="server" />--%>
                                            </div>

                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="lblPass2" Text="" runat="server" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8"
                                                    ControlToValidate="txtPass2"
                                                    runat="server"
                                                    ErrorMessage="*"
                                                    SetFocusOnError="True"
                                                    ForeColor="red"></asp:RequiredFieldValidator>
                                                <asp:TextBox runat="server" ID="txtPass2" TextMode="Password" TabIndex="2" CssClass="form-control" placeholder="Password" />
                                                <asp:CompareValidator ID="passCompare" runat="server" ControlToCompare="txtPass" ControlToValidate="txtPass2" ForeColor="Red" SetFocusOnError="True"></asp:CompareValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-12 ">
                                                <asp:Label ID="lblError" runat="server" CssClass="has-error" />
                                            </div>
                                        </div>
                                    </div>


                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-sm-12 ">
                                                <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click"
                                                    TabIndex="4" CssClass="form-control btn btn-primary" Text="Log In"
                                                    OnClientClick="this.style.display='none';document.getElementById('loading').style=''this.style.display='block';" />

                                                <div id="loading" style="display: none; text-align: center;">
                                                    <img src="/Images/LoadingBig.gif" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>



                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


