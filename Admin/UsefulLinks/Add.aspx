﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Admin_Slider_Add" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
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
                            <asp:Literal ID="Literal2" runat="server" Text="Link başlığı"></asp:Literal>
                        </label>
                        <div class="col-md-4">
                            <asp:DropDownList runat="server" ID="ddlheaderlink"
                                DataTextField="useful_links_header_az"
                                DataValueField="id" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>


                    <div class="form-group">
                        <label class="col-md-3 control-label">
                            <asp:Literal ID="Literal12" runat="server" Text="Link adı  (AZ)"></asp:Literal>
                        </label>
                        <div class="col-md-4">
                            <dx:ASPxHtmlEditor ID="useful_links_name_az" runat="server" SettingsImageUpload-UploadImageFolder="~/uploads/pages">
                            </dx:ASPxHtmlEditor>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">
                            <asp:Literal ID="Literal13" runat="server" Text="Link adı  (EN)"></asp:Literal>
                        </label>
                        <div class="col-md-4">
                            <dx:ASPxHtmlEditor ID="useful_links_name_en" runat="server" SettingsImageUpload-UploadImageFolder="~/uploads/pages">
                            </dx:ASPxHtmlEditor>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">
                            <asp:Literal ID="Literal10" runat="server" Text="Keçid linki  (AZ)"></asp:Literal>
                        </label>
                        <div class="col-md-4">
                            <asp:TextBox ID="useful_links_url_az" runat="server" CssClass="form-control " data-type="note" MaxLength="1000"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">
                            <asp:Literal ID="Literal11" runat="server" Text="Keçid linki  (EN)"></asp:Literal>
                        </label>
                        <div class="col-md-4">
                            <asp:TextBox ID="useful_links_url_en" runat="server" CssClass="form-control " data-type="note" MaxLength="1000"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">
                            <asp:Literal ID="Literal6" runat="server" Text="Sıra nömrəsi"></asp:Literal>
                        </label>
                        <div class="col-md-4">
                            <dx:ASPxSpinEdit ID="OrderBy" runat="server" MinValue="0" AllowMouseWheel="false"
                                Width="100%" Height="30px">
                            </dx:ASPxSpinEdit>
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

                                <asp:HyperLink ID="btnBack" CssClass="btn default" NavigateUrl="~/Admin/UsefulLinks/Default.aspx" Text="Geri" runat="server" />
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


</asp:Content>



