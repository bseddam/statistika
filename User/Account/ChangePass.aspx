<%@ Page Title="" Language="C#" MasterPageFile="~/User/MasterPage.master" AutoEventWireup="true" CodeFile="ChangePass.aspx.cs" Inherits="User_Account_Default" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="portlet box green-haze">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-gift"></i>
                        <asp:Literal ID="LtrPageTitle" runat="server"></asp:Literal>
                    </div>
                </div>
                <div class="portlet-body form">
                    <!-- BEGIN FORM-->
                    <div class="form-horizontal">

                        <div class="form-body">

                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    <asp:Literal ID="Literal4" runat="server">Şifrə<span class="required" aria-required="true">* </span></asp:Literal>
                                </label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="Pass" runat="server" CssClass="form-control" MaxLength="20" TextMode="Password"></asp:TextBox>

                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    <asp:Literal ID="Literal6" runat="server">Şifrə ( təkrar )<span class="required" aria-required="true">* </span></asp:Literal>
                                </label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="Pass2" runat="server" CssClass="form-control" MaxLength="20" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group has-success">
                                <label class="col-md-3 control-label"></label>
                                <div class="col-md-4">
                                    <div class="input-icon right">
                                        <asp:Panel ID="PnlInfo" runat="server" class="alert">
                                            <asp:Label ID="LblInfo" runat="server" Text=""></asp:Label>
                                        </asp:Panel>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-actions">
                            <div class="row">
                                <div class="col-md-offset-3 col-md-9">
                                    <div id="actions">
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn blue" Text="Yadda saxla" OnClick="btnSave_Click" ValidationGroup="save"
                                            OnClientClick="document.getElementById('actions').style.display='none';document.getElementById('loading1').style.display='block'" />
                                       
                                    </div>
                                    <div id="loading1" style="display: none">
                                        <img src="/images/loadingbig.gif" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <!-- END FORM-->
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>




