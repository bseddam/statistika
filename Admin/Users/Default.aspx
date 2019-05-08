<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Users_Default" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .treelist-new-column a {
            visibility: hidden;
        }
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PnlSearch" runat="server" CssClass="portlet box green filter-panel">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-search"></i>
                        <asp:Literal ID="lblFilter" runat="server"></asp:Literal>
                    </div>
                    <div class="tools">
                        <%--                        <a href="javascript:void(0);" class="close-panel" style="color: #fff" title="Bağla">X</a>--%>
                    </div>
                </div>
                <div class="portlet-body ">
                    <div class="col-md-4 margin-bottom-5">
                        <label>Status</label>
                        <dx:ASPxComboBox ID="Status" ClientInstanceName="TypeID"
                            runat="server" NullText="Siyahıdan seçin"
                            AnimationType="Slide" EnableTheming="True"
                            IncrementalFilteringMode="Contains" LoadingPanelText="Yüklənir&amp;hellip;"
                            TextField="Name" ValueField="ID"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="Status_SelectedIndexChanged"
                            Width="100%" Height="30px">
                            <Items>
                                <dx:ListEditItem Selected="true" Value="0" Text="Təsdiq gözləyənlər" />
                                <dx:ListEditItem Value="1" Text="Təsdiq olunmuşlar" />
                            </Items>
                        </dx:ASPxComboBox>
                    </div>
                    <div class="col-md-4 ">
                        <label>
                            <asp:Label ID="lblError" ForeColor="Red" runat="server" /></label>
                        <div id="search-actions">
                            <asp:Button ID="BtnSearch" runat="server" Visible="false" CssClass="btn green" Text="Axtar" OnClick="BtnSearch_Click"
                                OnClientClick="document.getElementById('search-actions').style.display='none';document.getElementById('search-loading').style.display='block'" />
                        </div>

                        <div id="search-loading" style="display: none; width: 98%">
                            <div class="progress progress-striped active">
                                <div class="progress-bar progress-bar-success" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                                    <span class="sr-only">20% Complete </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row margin-bottom-5">
                    </div>
                </div>
            </asp:Panel>
            <div class="portlet box blue-madison">
                <div class="portlet-title">
                    <table style="width: 100%; margin-top: 10px;">
                        <tr>
                            <td class="caption-menu-column1">
                                <div class="caption">
                                    <i class="fa fa-list"></i>
                                    <asp:Literal ID="ltrPageTitle" runat="server"></asp:Literal>
                                </div>
                            </td>
                            <td>
                                <%--    <label style="margin-right: 10px">
                                    <i class="fa fa-bar-chart-o"></i>
                                    <asp:Literal ID="LtrFilterCount" runat="server" Text=""></asp:Literal>
                                </label>--%>
                            </td>
                            <td>
                                <div class="icons-panel tools">
                                    <ul>
                                        <li>
                                            <%--                                            <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn_add" CommandArgument="add" OnClick="LnkPnlMenu_Click"><i class="fa fa-plus"></i> Yeni</asp:LinkButton>--%>
                                            <asp:LinkButton ID="lnkColumn" Text="Sütun seç" runat="server" />
                                        </li>
                                        <li>
                                            <asp:LinkButton ID="lnkSearch" runat="server" CssClass="show-filter" Visible="true" CommandArgument="search"><i class="fa fa-filter"></i> Axtarış</asp:LinkButton>
                                        </li>

                                        <li>
                                            <%-- <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument="export" OnClick="LnkPnlMenu_Click"><i class="fa fa-file-o"></i> Məlumatı yüklə</asp:LinkButton>--%>
                                            <div class="btn-group">
                                                <a id="btnGroupVerticalDrfop1" class="dropdown-toggle" data-toggle="dropdown" data-delay="1000" data-close-others="true" style="color: #fff;"><i class="fa fa-file-o"></i>Məlumatı yüklə <i class="fa fa-angle-down"></i>
                                                </a>
                                                <ul class="dropdown-menu" role="menu" aria-labelledby="btnGroupVerticalDrfop1">
                                                    <li style="margin-left: 0;">
                                                        <asp:LinkButton ID="LnkExport1" runat="server" CommandArgument="export"
                                                            CommandName="ex_xls" OnClick="LnkPnlMenu_Click"><i class="fa fa-file-excel-o"></i> Excel (.xls)</asp:LinkButton>
                                                    </li>
                                                    <li style="margin-left: 0;">
                                                        <asp:LinkButton ID="LnkExport2" runat="server" CommandArgument="export"
                                                            CommandName="ex_xlsx" OnClick="LnkPnlMenu_Click"><i class="fa fa-file-excel-o"></i> Excel (.xlsx)</asp:LinkButton>
                                                    </li>
                                                    <li style="margin-left: 0;">
                                                        <asp:LinkButton ID="LnkExport3" Visible="false" runat="server" CommandArgument="export"
                                                            CommandName="ex_csv" OnClick="LnkPnlMenu_Click"><i class="fa fa-file-excel-o"></i> Excel (.csv)</asp:LinkButton>
                                                    </li>
                                                    <li class="divider"></li>
                                                    <li style="margin-left: 0;">
                                                        <asp:LinkButton ID="LnkExport4" runat="server" CommandArgument="export"
                                                            CommandName="ex_pdf" OnClick="LnkPnlMenu_Click"><i class="fa fa-file-pdf-o"></i> PDF (.pdf)</asp:LinkButton>
                                                    </li>
                                                    <li style="margin-left: 0;">
                                                        <asp:LinkButton ID="LnkExport5" runat="server" CommandArgument="export"
                                                            CommandName="ex_rtf" OnClick="LnkPnlMenu_Click"><i class="fa fa-file-word-o"></i> Word&nbsp;(.rtf)</asp:LinkButton>
                                                    </li>
                                                </ul>
                                            </div>


                                        </li>
                                    </ul>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="portlet-body ">
                    <dx:ASPxGridViewExporter ID="gridExporter" runat="server" PaperKind="A4" Landscape="True"></dx:ASPxGridViewExporter>
                    <span>-Tam icazəsi olan qeydiyyatdan keçənlər bütün illər üzrə məlumat daxil edə bilərlər</span>
                    <dx:ASPxGridView ID="treeList" runat="server"
                        OnRowUpdating="treeList_RowUpdating"
                        OnRowDeleting="treeList_RowDeleting"
                        AutoGenerateColumns="false" Width="100%"
                        KeyFieldName="id"
                        ClientInstanceName="treeList">
                        <Settings GridLines="Both" ShowFilterRow="true" />
                        <SettingsBehavior EnableCustomizationWindow="true" ConfirmDelete="true" />
                        <Columns>

                            <dx:GridViewDataColumn Caption="No" FieldName="id">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataComboBoxColumn Caption="Qurumun adı" FieldName="org_id" Settings-AllowAutoFilter="True">
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn Caption="Ad" FieldName="name"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Soyad" FieldName="surname"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Atasının adı" FieldName="patronymic" Visible="false"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Vəzifə" FieldName="position"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Tel(iş)" FieldName="phone_work"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Tel(mob)" FieldName="phone_mob"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="E-poçt" FieldName="email"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Şifrə" FieldName="password">
                            </dx:GridViewDataColumn>

                            <dx:GridViewDataColumn Caption="#" FieldName="is_active">
                                <EditFormSettings Visible="False" />
                                <DataItemTemplate>
                                    <asp:LinkButton ID="btnConfirm" CommandArgument='<%#Eval("id") %>' OnClientClick="return confirm('Əminsinizmi ? ');" OnClick="btnConfirm_Click" Text="Təsdiqlə" runat="server" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewCommandColumn ShowEditButton="true" ShowDeleteButton="false" Caption="#">
                            </dx:GridViewCommandColumn>
                        </Columns>
                        <Styles Header-CssClass="grid-header">
                        </Styles>
                        <SettingsCookies
                            Enabled="true"
                            Version="1.0"
                            CookiesID="user1"
                            StoreColumnsWidth="true"
                            StoreColumnsVisiblePosition="true" />
                        <SettingsText CommandNew="Yeni" CommandDelete="Deaktiv et" CommandCancel="Ləğv et" CommandEdit="Yenilə"
                            CommandUpdate="Yadda saxla" CustomizationWindowCaption="Sütun seçin"
                            ConfirmDelete="Deaktiv etmək istədiyinizə əminsiniz?" />

                    </dx:ASPxGridView>
                    <dx:ASPxGridView ID="treeList2" runat="server"
                        OnRowUpdating="treeList_RowUpdating" Visible="false"
                        OnRowDeleting="treeList_RowDeleting"
                        AutoGenerateColumns="false" Width="100%"
                        KeyFieldName="id"
                        ClientInstanceName="treeList2">
                        <Settings GridLines="Both" ShowFilterRow="true" />
                        <SettingsBehavior EnableCustomizationWindow="true" ConfirmDelete="true" />
                        <Columns>

                            <dx:GridViewDataColumn Caption="No" FieldName="id">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataComboBoxColumn Caption="Qurumun adı" FieldName="org_id" Settings-AllowAutoFilter="True">
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn Caption="Ad" FieldName="name"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Soyad" FieldName="surname"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Atasının adı" FieldName="patronymic" Visible="false"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Vəzifə" FieldName="position"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Tel(iş)" FieldName="phone_work"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Tel(mob)" FieldName="phone_mob"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="E-poçt" FieldName="email"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Şifrə" FieldName="password">
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Tam icazə" FieldName="full_access">
                                <EditFormSettings Visible="False" />
                                <DataItemTemplate>
                                    <asp:LinkButton ID="btnFullAccess"
                                        CommandArgument='<%#Eval("id") %>'
                                        OnClientClick="return confirm('Əminsinizmi ? ');"
                                        OnClick="btnFullAccess_Click"
                                        Visible='<%#Eval("full_access").ToParseStr()=="0"?true:false %>'
                                        Text="Aktivləşdir" runat="server" />

                                    <asp:LinkButton ID="BtnCancelFullAcess"
                                        CommandArgument='<%#Eval("id") %>'
                                        OnClientClick="return confirm('Əminsinizmi ? ');"
                                        OnClick="BtnCancelFullAcess_Click"
                                        Visible='<%#Eval("full_access").ToParseStr()=="1"?true:false %>'
                                        Text="İcazəsi var.Deaktiv et" runat="server" />

                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewCommandColumn ShowEditButton="true" ShowDeleteButton="true" Caption="#">
                            </dx:GridViewCommandColumn>
                        </Columns>
                        <SettingsCookies
                            Enabled="true"
                            Version="1.0"
                            CookiesID="user2"
                            StoreColumnsWidth="true"
                            StoreColumnsVisiblePosition="true" />
                        <Styles Header-CssClass="grid-header">
                        </Styles>
                        <SettingsText CommandNew="Yeni" CommandDelete="Deaktiv et" CommandCancel="Ləğv et" CommandEdit="Yenilə"
                            CommandUpdate="Yadda saxla" CustomizationWindowCaption="Sütun seçin"
                            ConfirmDelete="Deaktiv etmək istədiyinizə əminsiniz?" />

                    </dx:ASPxGridView>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="LnkExport1" />
            <asp:PostBackTrigger ControlID="LnkExport2" />
            <asp:PostBackTrigger ControlID="LnkExport3" />
            <asp:PostBackTrigger ControlID="LnkExport4" />
            <asp:PostBackTrigger ControlID="LnkExport5" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>





