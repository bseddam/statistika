<%@ Page Title="" Language="C#" MasterPageFile="~/User/MasterPage.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="User_Main_Add" %>


<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

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
                    <div class="form-horizontal">
                        <div class="form-body">
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    <asp:Literal ID="Literal3" runat="server" Text="Hesabat ili"></asp:Literal>
                                </label>
                                <div class="col-md-4">
                                    <asp:Label ID="lblyear" Text="" runat="server" Style="padding-top: 8px; display: block;" />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    <asp:Literal ID="Literal1" runat="server" Text="Göstərici"></asp:Literal>
                                </label>
                                <div class="col-md-4">
                                    <dx:ASPxComboBox ID="Indicator" ClientInstanceName="Indicator"
                                        runat="server" NullText="Siyahıdan seçin"
                                        AnimationType="Slide" EnableTheming="True"
                                        IncrementalFilteringMode="Contains" LoadingPanelText="Yüklənir&amp;hellip;"
                                        TextField="name_az" ValueField="id"
                                        Width="100%" Height="30px">
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    <asp:Literal ID="Literal2" runat="server" Text="Region"></asp:Literal>
                                </label>
                                <div class="col-md-4">
                                    <dx:ASPxComboBox ID="Region" ClientInstanceName="Region"
                                        runat="server" NullText="Siyahıdan seçin"
                                        AnimationType="Slide" EnableTheming="True"
                                        IncrementalFilteringMode="Contains" LoadingPanelText="Yüklənir&amp;hellip;"
                                        TextField="name_az" ValueField="id"
                                        Width="100%" Height="30px">
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">
                                    <asp:Literal ID="Literal4" runat="server" Text="Dəyər"></asp:Literal>
                                </label>
                                <div class="col-md-4">
                                    <dx:ASPxSpinEdit ID="txtValue" MinValue="0" MaxValue="1000" runat="server" CssClass="" />
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

                                        <%--                                        <asp:HyperLink ID="btnBack" CssClass="btn default" Text="Geri" runat="server" />--%>
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




