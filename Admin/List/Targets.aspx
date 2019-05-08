<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Targets.aspx.cs" Inherits="Admin_List_Targets" %>


<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>



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
                        <label>Məqsədlər</label>
                        <dx:ASPxComboBox ID="GoalList" ClientInstanceName="GoalList"
                            runat="server" NullText="Siyahıdan seçin"
                            AnimationType="Slide" EnableTheming="True"
                            IncrementalFilteringMode="Contains" LoadingPanelText="Yüklənir&amp;hellip;"
                            TextField="name" ValueField="Id"
                            Width="100%" Height="30px">
                        </dx:ASPxComboBox>
                    </div>

                    <div class="col-md-4 ">
                        <label>
                            <asp:Label ID="lblError" ForeColor="Red" runat="server" /></label>
                        <div id="search-actions">
                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn green" Text="Axtar" OnClick="BtnSearch_Click"
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
            <asp:Panel ID="PnlContent" runat="server" class="portlet box blue-madison">
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
                                            <asp:LinkButton ID="btnAdd" CssClass="btn_add" runat="server" CommandArgument="add"><i class="fa fa-plus"></i> Yeni</asp:LinkButton>
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
                    <dx:ASPxGridView ID="Grid" runat="server"
                        OnRowUpdating="Grid_RowUpdating"
                        OnRowDeleting="Grid_RowDeleting"
                        OnRowInserting="Grid_RowInserting"
                        AutoGenerateColumns="False" Width="100%"
                        KeyFieldName="id" SettingsBehavior-ConfirmDelete="true">
                        <Settings GridLines="Both" />
                        <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>
                        <Columns>

                            <dx:GridViewDataColumn Caption="ID" FieldName="id" VisibleIndex="1" Width="50">
                                <EditFormSettings VisibleIndex="0" Visible="false" />
                            </dx:GridViewDataColumn>

                            <dx:GridViewDataColumn Caption="Hədəfin kodu" FieldName="code" VisibleIndex="2" Width="200">
                                <EditFormSettings VisibleIndex="0" Visible="true" RowSpan="2" ColumnSpan="2" />
                            </dx:GridViewDataColumn>

                            <dx:GridViewDataMemoColumn Caption="Hədəfin adı (AZ)" FieldName="name_az" VisibleIndex="3">
                                <EditFormSettings VisibleIndex="1" RowSpan="2" ColumnSpan="2" />
                            </dx:GridViewDataMemoColumn>

                            <dx:GridViewDataMemoColumn Caption="Hədəfin adı  (EN)" FieldName="name_en" VisibleIndex="4">
                                <EditFormSettings VisibleIndex="2" RowSpan="2" ColumnSpan="2" />
                            </dx:GridViewDataMemoColumn>
                            <dx:GridViewDataCheckColumn Caption="Milli priotet" FieldName="milli_priotet" VisibleIndex="4" Visible="false">
                                <EditFormSettings VisibleIndex="2" RowSpan="2" ColumnSpan="2"  Visible="True"/>
                            </dx:GridViewDataCheckColumn>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" VisibleIndex="5" HeaderStyle-CssClass="treelist-new-column">
                                <EditButton Visible="True" />
                                <DeleteButton Visible="True" />
                            </dx:GridViewCommandColumn>

                        </Columns>
                        <Settings ShowFilterRow="true" />
                        <SettingsPager>
                            <PageSizeItemSettings Visible="true"></PageSizeItemSettings>
                        </SettingsPager>
                        <SettingsText CommandNew="Yeni" CommandDelete="Sil" CommandCancel="Ləğv et"
                             CommandEdit="Yenilə" CommandUpdate="Yadda saxla" 
                            CustomizationWindowCaption="Sütun seçin" 
                            ConfirmDelete="Silmək istədiyinizə əminsiniz?" />
                        <SettingsDataSecurity />
                        <Styles Header-CssClass="grid-header"></Styles>

                    </dx:ASPxGridView>
                </div>
            </asp:Panel>
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








