<%@ Page Title="" Language="C#" MasterPageFile="~/User/MasterPage.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="User_Main_Add" %>


<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .dxgvCommandColumn_Moderno {
            background-color: #eaeaea;
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
                        <%--<a href="javascript:void(0);" class="close-panel" style="color: #fff" title="Bağla">X</a>--%>
                    </div>
                </div>
                <div class="portlet-body ">
                    <div class="col-md-4 margin-bottom-5">
                        <label>Menyu növü</label>
                        <%--<dx:ASPxComboBox ID="TypeID" ClientInstanceName="TypeID"
                            runat="server" NullText="Siyahıdan seçin"
                            AnimationType="Slide" EnableTheming="True"
                            IncrementalFilteringMode="Contains" LoadingPanelText="Yüklənir&amp;hellip;"
                            TextField="Name" ValueField="ID"
                            Width="100%" Height="30px">
                        </dx:ASPxComboBox>--%>
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

                        </tr>
                    </table>
                </div>
                <asp:HiddenField runat="server" ID="sizeCode" />
                <div class="portlet-body ">
                    <asp:Literal Text="" ID="ltrSize" runat="server" />
                    <br />
                    <span class="muted">
                        <i class="fa fa-info-circle"></i> Statistik məlumatların bazaya daxil edilməsi cari il üzrə müvafiq sətr və sütunların seçilməsi ilə mümkündür. Daxil edilən statistik məlumatların dürüstlüyünə əmin olunduqdan sonra təsdiq edilməlidir.
                        <br />
                        <i class="fa fa-info-circle"></i> Statistik məlumatların daxil edilmə müddəti başa çatdıqdan sonra daxil edilmiş məlumatlara düzəliş yalnız administratorun icazəsi ilə mümkündür
                        <br />
                        <i class="fa fa-info-circle"></i> Əgər məlumat mövcud deyilsə, “-”, hadisə baş verməmişdirsə, “...” işarələrini daxil edin.

                    </span>
                    <dx:ASPxGridViewExporter ID="gridExporter" runat="server" PaperKind="A4" Landscape="True"></dx:ASPxGridViewExporter>
                    <dx:ASPxGridView ID="Grid" runat="server"
                        OnBatchUpdate="Grid_BatchUpdate"
                        OnRowUpdating="Grid_RowUpdating"
                        OnParseValue="Grid_ParseValue"
                        OnRowValidating="Grid_RowValidating"
                        AutoGenerateColumns="False" Width="100%"
                        KeyFieldName="region_id">
                        <SettingsEditing Mode="Batch"></SettingsEditing>

                        <Columns>
                            <dx:GridViewDataColumn Caption="Region kodu" Width="100" Visible="false" FieldName="region_id" CellStyle-BackColor="LightYellow" VisibleIndex="2" FixedStyle="Left">
                                <EditFormSettings Visible="False" />
                                <CellStyle BackColor="LightYellow">
                                </CellStyle>
                            </dx:GridViewDataColumn>

                            <dx:GridViewDataColumn Caption="Region kodu" Width="100" FieldName="region_code" VisibleIndex="2" FixedStyle="Left">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="İstinad zonası" Width="200" FieldName="region_name" VisibleIndex="2" FixedStyle="Left">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn>
                        </Columns>
                        <Settings HorizontalScrollBarMode="Visible" VerticalScrollableHeight="100"
                            VerticalScrollBarMode="Visible" ShowFilterRow="false" GridLines="Both" />
                        <SettingsPager Mode="ShowAllRecords">
                        </SettingsPager>
                        <SettingsText CommandNew="Yeni" CommandBatchEditCancel="Ləğv et"
                            CommandBatchEditUpdate="Məlumatı təsdiqlə" CommandDelete="Sil" CommandCancel="Ləğv et" CommandEdit="Yenilə" CommandUpdate="Yadda saxla" CustomizationWindowCaption="Sütun seçin" ConfirmDelete="Silmək istəditinizə əminsiniz?" />
                        <SettingsDataSecurity AllowDelete="False" AllowInsert="False" />
                        <Styles Header-CssClass="grid-header">
                            <Header CssClass="grid-header">
                            </Header>
                        </Styles>
                    </dx:ASPxGridView>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%--   <asp:PostBackTrigger ControlID="LnkExport1" />
            <asp:PostBackTrigger ControlID="LnkExport2" />
            <asp:PostBackTrigger ControlID="LnkExport3" />
            <asp:PostBackTrigger ControlID="LnkExport4" />
            <asp:PostBackTrigger ControlID="LnkExport5" />--%>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
