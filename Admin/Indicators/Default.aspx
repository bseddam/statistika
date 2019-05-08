<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Indicators_Default" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .table-column {
            width: 50%;
            padding: 10px;
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
                        <%-- <a href="javascript:void(0);" class="close-panel" style="color: #fff" title="Bağla">X</a>--%>
                    </div>
                </div>
                <div class="portlet-body ">
                    <div class="col-md-4 margin-bottom-5">
                        <label>Məqsədlərin siyahısı</label>
                        <dx:ASPxComboBox ID="GoalList" ClientInstanceName="GoalList"
                            runat="server" NullText="Siyahıdan seçin"
                            AnimationType="Slide" EnableTheming="True"
                            IncrementalFilteringMode="Contains" LoadingPanelText="Yüklənir&amp;hellip;"
                            TextField="name" ValueField="id"
                            Width="100%" Height="30px">
                        </dx:ASPxComboBox>
                    </div>

                    <div class="col-md-4 ">
                        <label>
                            <asp:Label ID="lblError" ForeColor="Red" runat="server" /></label>
                        <div id="search-actions" style="padding-top: 7px;">

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
            <asp:Panel runat="server" ID="PnlContent" CssClass="portlet box blue-madison">
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
                                            <asp:LinkButton ID="lnkColumn" Text="Sütun seç" OnClientClick="grid.ShowCustomizationWindow(); return false;" runat="server" />
                                        </li>
                                        <li>
                                            <asp:LinkButton ID="btnAdd" runat="server" CommandArgument="add" OnClick="LnkPnlMenu_Click"><i class="fa fa-plus"></i> Yeni</asp:LinkButton>
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

                <asp:Panel runat="server" CssClass="portlet-body ">
                    <dx:ASPxGridViewExporter ID="gridExporter" runat="server" PaperKind="A4" Landscape="True"></dx:ASPxGridViewExporter>
                    <dx:ASPxGridView ID="Grid" runat="server"
                        OnRowUpdating="Grid_RowUpdating"
                        OnRowDeleting="Grid_RowDeleting"
                        OnRowInserting="Grid_RowInserting"
                        OnRowValidating="Grid_RowValidating"
                        ClientInstanceName="grid"
                        AutoGenerateColumns="False"
                        Width="100%"
                        SettingsBehavior-ConfirmDelete="true"
                        SettingsBehavior-EnableCustomizationWindow="true"
                        KeyFieldName="id">
                        <ClientSideEvents ColumnResized="function(s,e){ grid.PerformCallback();}" />
                        <Settings GridLines="Both" />
                        <SettingsBehavior ColumnResizeMode="NextColumn" />
                        <SettingsPopup EditForm-AllowResize="True" EditForm-Modal="true" EditForm-HorizontalAlign="Center" EditForm-VerticalAlign="Above" EditForm-Height="400px"></SettingsPopup>
                        <SettingsEditing Mode="PopupEditForm"></SettingsEditing>
                        <SettingsPopup CustomizationWindow-Height="400" CustomizationWindow-Width="500"></SettingsPopup>
                        <SettingsCookies
                            Enabled="true"
                            Version="1.2"
                            CookiesID="indicators"
                            StoreColumnsWidth="true"
                            StoreColumnsVisiblePosition="true" />
                        <Columns>
                            <dx:GridViewDataSpinEditColumn Caption="ID" PropertiesSpinEdit-MinValue="0"
                                FieldName="id" VisibleIndex="0" Width="50">
                                <EditFormSettings Visible="false" />
                            </dx:GridViewDataSpinEditColumn>
                            <%-- <dx:GridViewDataColumn Caption="ID" FieldName="id" VisibleIndex="0" Width="50">
                                <EditFormSettings Visible="true" />
                            </dx:GridViewDataColumn>--%>


                            <dx:GridViewDataComboBoxColumn Caption="Statusu" FieldName="status_id" VisibleIndex="2">
                                <EditFormSettings ColumnSpan="2" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataComboBoxColumn Caption="Tipi" FieldName="type_id" VisibleIndex="2">
                                <EditFormSettings ColumnSpan="2" />
                            </dx:GridViewDataComboBoxColumn>

                            <dx:GridViewDataColumn Caption="Kodu" FieldName="code" VisibleIndex="3" Width="100">
                                <EditFormSettings ColumnSpan="2" />
                                <DataItemTemplate>
                                    <%#Config.ClearIndicatorCode(Eval("code").ToParseStr()) %>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>

                            <dx:GridViewDataColumn Caption="Göstəricinin adı (AZ)" FieldName="name_az" VisibleIndex="3">
                                <EditFormSettings ColumnSpan="2" Visible="True" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Göstəricinin adı (EN)" FieldName="name_en" VisibleIndex="4" Width="50" Visible="false">
                                <EditFormSettings ColumnSpan="2" Visible="True" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataComboBoxColumn Caption="Göstəriciyə cavabdeh qurum" FieldName="qurum_id"
                                VisibleIndex="4" Visible="true" Settings-AllowAutoFilter="True">
                                <EditFormSettings Visible="True" ColumnSpan="2" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataComboBoxColumn Caption="Ölçü vahidi" FieldName="size_id" VisibleIndex="4" Width="50" Visible="false">
                                <EditFormSettings Visible="True" ColumnSpan="2" />
                            </dx:GridViewDataComboBoxColumn>

                            <dx:GridViewDataComboBoxColumn Caption="Milli prioritetlərə uyğunluğu " FieldName="uygunluq_id" Width="50" VisibleIndex="4" Visible="false">
                                <EditFormSettings Visible="True" ColumnSpan="2" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataColumn Caption="Təkrarlanan göstərici kodu" FieldName="tekrarlanan_gosderici_kod" Width="50" VisibleIndex="4" Visible="false">
                                <EditFormSettings Visible="True" ColumnSpan="2" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataComboBoxColumn Caption="Göstəricinin aid olduğu səviyyə" FieldName="seviye" Width="50" VisibleIndex="4" Visible="false">
                                <EditFormSettings Visible="True" ColumnSpan="2" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataMemoColumn Caption="Göstəriciyə dair əlavə məlumat  (AZ)" FieldName="info_az" Width="50" VisibleIndex="4" Visible="false">
                                <EditFormSettings ColumnSpan="2" Visible="True" />
                            </dx:GridViewDataMemoColumn>
                            <dx:GridViewDataMemoColumn Caption="Göstəriciyə dair əlavə məlumat (EN)" FieldName="info_en" Width="50" VisibleIndex="4" Visible="false">
                                <EditFormSettings ColumnSpan="2" Visible="True" />
                            </dx:GridViewDataMemoColumn>
                            <dx:GridViewDataSpinEditColumn Caption="Göstərici üzrə statistik məlumatın təqdim olunmasının ilkin müddəti (gün)" Width="50" PropertiesSpinEdit-MinValue="1" PropertiesSpinEdit-MaxValue="31" FieldName="teqdim_olunma_bas_gun" VisibleIndex="4" Visible="false">
                                <EditFormSettings Visible="True" ColumnSpan="2" />
                            </dx:GridViewDataSpinEditColumn>
                            <dx:GridViewDataSpinEditColumn Caption="Göstərici üzrə statistik məlumatın təqdim olunmasının ilkin müddəti (ay)" Width="50" PropertiesSpinEdit-MinValue="1" PropertiesSpinEdit-MaxValue="12" FieldName="teqdim_olunma_bas_ay" VisibleIndex="4" Visible="false">
                                <EditFormSettings Visible="True" ColumnSpan="2" />
                            </dx:GridViewDataSpinEditColumn>
                            <dx:GridViewDataSpinEditColumn Caption="Göstərici üzrə statistik məlumatın təqdim olunmasının son müddəti (gün)" Width="50" PropertiesSpinEdit-MinValue="1" PropertiesSpinEdit-MaxValue="31" FieldName="teqdim_olunma_son_gun" VisibleIndex="4" Visible="false">
                                <EditFormSettings Visible="True" ColumnSpan="2" />
                            </dx:GridViewDataSpinEditColumn>
                            <dx:GridViewDataSpinEditColumn Caption="Göstərici üzrə statistik məlumatın təqdim olunmasının son müddəti (ay)" Width="50" PropertiesSpinEdit-MinValue="1" PropertiesSpinEdit-MaxValue="12" FieldName="teqdim_olunma_son_ay" VisibleIndex="4" Visible="false">
                                <EditFormSettings Visible="True" ColumnSpan="2" />
                            </dx:GridViewDataSpinEditColumn>
                            <dx:GridViewDataSpinEditColumn Caption="Göstərici üzrə statistik məlumatın təqdim olunmasının uzadılma müddəti (gün)" Width="50" PropertiesSpinEdit-MinValue="1" PropertiesSpinEdit-MaxValue="12" FieldName="elave_gun" VisibleIndex="4" Visible="false">
                                <EditFormSettings Visible="True" ColumnSpan="2" />
                            </dx:GridViewDataSpinEditColumn>


                            <dx:GridViewDataColumn Caption="Mənbə (AZ)" FieldName="source_az" VisibleIndex="4" Width="50" Visible="false">
                                <EditFormSettings Visible="True" ColumnSpan="2" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataMemoColumn Caption="Qeyd (az)" FieldName="note_az" VisibleIndex="4" Width="50" Visible="false">
                                <EditFormSettings Visible="True" ColumnSpan="2" />
                            </dx:GridViewDataMemoColumn>

                            <dx:GridViewDataColumn VisibleIndex="5">
                                <DataItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" OnClick="lnkEdit_Click" CommandArgument='<%#Eval("id") %>' Text="Yenilə" runat="server" />

                                    <asp:LinkButton ID="lnkDelete" OnClick="lnkDelete_Click" OnClientClick="return confirm('Silmək istədiyinizə əminsinizmi?');" CommandArgument='<%#Eval("id") %>' Text="Sil" runat="server" />
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>

                            <%--   <dx:GridViewCommandColumn Caption="#" Width="70" VisibleIndex="7">
                                <EditButton Visible="true" />
                                <DeleteButton Visible="true" />
                            </dx:GridViewCommandColumn>--%>
                        </Columns>
                        <Settings ShowFilterRow="true" />
                        <SettingsPager>
                            <PageSizeItemSettings Visible="true"></PageSizeItemSettings>
                        </SettingsPager>
                        <SettingsText CommandNew="Yeni" PopupEditFormCaption="Göstəricilər üzrə məlumatlar forması" CommandDelete="Sil"
                            CommandCancel="Ləğv et" CommandEdit="Yenilə" CommandUpdate="Yadda saxla"
                            CustomizationWindowCaption="Sütun seçin" ConfirmDelete="Silmək istəditinizə əminsiniz?" />
                        <SettingsDataSecurity AllowDelete="true" />
                        <Styles Header-CssClass="grid-header"></Styles>

                    </dx:ASPxGridView>

                    <dx:ASPxPopupControl ID="popupEdit"
                        runat="server" 
                        ClientInstanceName="popup"
                        AllowDragging="true"
                        AllowResize="true"
                        Modal="true"
                        DragElement="Header"
                        Width="600"
                        HeaderText="Göstəricilər üzrə məlumatlar forması"
                        PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter">
                        <ContentCollection>
                            <dx:PopupControlContentControl>
                                <dx:ASPxPageControl ID="tabs" runat="server" ActiveTabIndex="0" Width="100%"
                                    EnableTabScrolling="true" TabAlign="Justify">
                                    <TabPages>
                                        <dx:TabPage Text="Ümumi məlumat">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">
                                                    <div style="height: 400px; overflow-y: auto">
                                                        <table style="width: 100%">
                                                            <tr>
                                                                <td class="table-column">
                                                                    <asp:Label Text="Statusu" runat="server" />
                                                                    <dx:ASPxComboBox ID="StatusList" Width="100%" IncrementalFilteringMode="Contains" runat="server"></dx:ASPxComboBox>
                                                                </td>
                                                                <td class="table-column">
                                                                    <asp:Label Text="Tipi" runat="server" />
                                                                    <dx:ASPxComboBox ID="TypeList" Width="100%" runat="server" IncrementalFilteringMode="Contains"></dx:ASPxComboBox>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="table-column">
                                                                    <asp:Label Text="Göstəriciyə cavabdeh qurum" runat="server" />
                                                                    <dx:ASPxComboBox ID="qurumList" Width="100%" runat="server" IncrementalFilteringMode="Contains"></dx:ASPxComboBox>
                                                                </td>
                                                                <td class="table-column">
                                                                    <asp:Label Text="Ölçü vahidi	" runat="server" />
                                                                    <dx:ASPxComboBox ID="sizeList" Width="100%" runat="server" IncrementalFilteringMode="Contains"></dx:ASPxComboBox>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="table-column">
                                                                    <asp:Label Text="Milli prioritetlərə uyğunluğu" runat="server" />
                                                                    <dx:ASPxComboBox ID="uygunluqList" Width="100%" runat="server" IncrementalFilteringMode="Contains"></dx:ASPxComboBox>
                                                                </td>
                                                                <td class="table-column">
                                                                    <asp:Label Text="Göstəricinin aid olduğu səviyyə	" runat="server" />
                                                                    <dx:ASPxComboBox ID="seviyeList" Width="100%" runat="server" IncrementalFilteringMode="Contains"></dx:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" class="table-column">
                                                                    <asp:Label Text="Parent göstərici" runat="server" />
                                                                    <dx:ASPxComboBox ID="Parent_id" Width="100%" IncrementalFilteringMode="Contains" runat="server"></dx:ASPxComboBox>
                                                                </td>

                                                            </tr>
                                                            <tr>
                                                                <td class="table-column">
                                                                    <asp:Label Text="Kodu" runat="server" />
                                                                    <dx:ASPxTextBox ID="Kod" runat="server" Width="100%"></dx:ASPxTextBox>
                                                                </td>
                                                                <td class="table-column">
                                                                    <asp:Label Text="Təkrarlanan göstərici kodu	" runat="server" />
                                                                    <dx:ASPxTextBox ID="tekrarKod" Width="100%" runat="server"></dx:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="table-column">
                                                                    <asp:Label Text="Göstəricinin adı (AZ)	" runat="server" />
                                                                    <dx:ASPxMemo ID="Name_az" Width="100%" runat="server" Height="50px"></dx:ASPxMemo>
                                                                </td>
                                                                <td class="table-column">
                                                                    <asp:Label Text="Göstəricinin adı (EN)	" runat="server" />
                                                                    <dx:ASPxMemo ID="Name_en" Width="100%" runat="server" Height="50px"></dx:ASPxMemo>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td colspan="2">Göstərici üzrə statistik məlumatın təqdim olunmasının ilkin müddəti
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="table-column">
                                                                    <asp:Label Text="Gün" runat="server" />
                                                                    <dx:ASPxSpinEdit ID="baslama_tarix_gun" Width="100%" runat="server" MinValue="0" MaxValue="31"></dx:ASPxSpinEdit>
                                                                </td>
                                                                <td class="table-column">
                                                                    <asp:Label Text="Ay" runat="server" />
                                                                    <dx:ASPxSpinEdit ID="baslama_tarix_ay" Width="100%" runat="server" MinValue="0" MaxValue="12"></dx:ASPxSpinEdit>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">Göstərici üzrə statistik məlumatın təqdim olunmasının son müddəti
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="table-column">
                                                                    <asp:Label Text="Gün" runat="server" />
                                                                    <dx:ASPxSpinEdit ID="son_tarix_gun" Width="100%" runat="server" MinValue="0" MaxValue="31"></dx:ASPxSpinEdit>
                                                                </td>
                                                                <td class="table-column">
                                                                    <asp:Label Text="Ay" runat="server" />
                                                                    <dx:ASPxSpinEdit ID="son_tarix_ay" Width="100%" runat="server" MinValue="0" MaxValue="12"></dx:ASPxSpinEdit>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td colspan="2" class="table-column">
                                                                    <asp:Label Text="Göstərici üzrə statistik məlumatın təqdim olunmasının uzadılma müddəti(gün)" runat="server" />
                                                                    <dx:ASPxSpinEdit ID="gunSayi" Width="100%" runat="server" MinValue="0"></dx:ASPxSpinEdit>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>

                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Text="Göstəriciyə dair əlavə məlumat (AZ)">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">
                                                    <dx:ASPxHtmlEditor ID="info_az" AutoResizeWithContainer="True" Height="200px"
                                                        runat="server"
                                                        SettingsImageUpload-UploadImageFolder="~/uploads/pages">
                                                        <Toolbars>
                                                            <dx:HtmlEditorToolbar Name="StandardToolbar1">
                                                                <Items>
                                                                    <dx:ToolbarCutButton>
                                                                    </dx:ToolbarCutButton>
                                                                    <dx:ToolbarCopyButton>
                                                                    </dx:ToolbarCopyButton>
                                                                    <dx:ToolbarPasteButton>
                                                                    </dx:ToolbarPasteButton>
                                                                    <dx:ToolbarPasteFromWordButton>
                                                                    </dx:ToolbarPasteFromWordButton>
                                                                    <dx:ToolbarUndoButton BeginGroup="True">
                                                                    </dx:ToolbarUndoButton>
                                                                    <dx:ToolbarRedoButton>
                                                                    </dx:ToolbarRedoButton>
                                                                    <dx:ToolbarRemoveFormatButton BeginGroup="True">
                                                                    </dx:ToolbarRemoveFormatButton>
                                                                    <dx:ToolbarSuperscriptButton BeginGroup="True">
                                                                    </dx:ToolbarSuperscriptButton>
                                                                    <dx:ToolbarSubscriptButton>
                                                                    </dx:ToolbarSubscriptButton>
                                                                    <dx:ToolbarInsertOrderedListButton BeginGroup="True">
                                                                    </dx:ToolbarInsertOrderedListButton>
                                                                    <dx:ToolbarInsertUnorderedListButton>
                                                                    </dx:ToolbarInsertUnorderedListButton>
                                                                    <dx:ToolbarIndentButton BeginGroup="True">
                                                                    </dx:ToolbarIndentButton>
                                                                    <dx:ToolbarOutdentButton>
                                                                    </dx:ToolbarOutdentButton>
                                                                    <dx:ToolbarInsertLinkDialogButton BeginGroup="True">
                                                                    </dx:ToolbarInsertLinkDialogButton>
                                                                    <dx:ToolbarUnlinkButton>
                                                                    </dx:ToolbarUnlinkButton>
                                                                    <dx:ToolbarInsertImageDialogButton>
                                                                    </dx:ToolbarInsertImageDialogButton>
                                                                    <dx:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                                                        <Items>
                                                                            <dx:ToolbarInsertTableDialogButton BeginGroup="True">
                                                                            </dx:ToolbarInsertTableDialogButton>
                                                                            <dx:ToolbarTablePropertiesDialogButton BeginGroup="True">
                                                                            </dx:ToolbarTablePropertiesDialogButton>
                                                                            <dx:ToolbarTableRowPropertiesDialogButton>
                                                                            </dx:ToolbarTableRowPropertiesDialogButton>
                                                                            <dx:ToolbarTableColumnPropertiesDialogButton>
                                                                            </dx:ToolbarTableColumnPropertiesDialogButton>
                                                                            <dx:ToolbarTableCellPropertiesDialogButton>
                                                                            </dx:ToolbarTableCellPropertiesDialogButton>
                                                                            <dx:ToolbarInsertTableRowAboveButton BeginGroup="True">
                                                                            </dx:ToolbarInsertTableRowAboveButton>
                                                                            <dx:ToolbarInsertTableRowBelowButton>
                                                                            </dx:ToolbarInsertTableRowBelowButton>
                                                                            <dx:ToolbarInsertTableColumnToLeftButton>
                                                                            </dx:ToolbarInsertTableColumnToLeftButton>
                                                                            <dx:ToolbarInsertTableColumnToRightButton>
                                                                            </dx:ToolbarInsertTableColumnToRightButton>
                                                                            <dx:ToolbarSplitTableCellHorizontallyButton BeginGroup="True">
                                                                            </dx:ToolbarSplitTableCellHorizontallyButton>
                                                                            <dx:ToolbarSplitTableCellVerticallyButton>
                                                                            </dx:ToolbarSplitTableCellVerticallyButton>
                                                                            <dx:ToolbarMergeTableCellRightButton>
                                                                            </dx:ToolbarMergeTableCellRightButton>
                                                                            <dx:ToolbarMergeTableCellDownButton>
                                                                            </dx:ToolbarMergeTableCellDownButton>
                                                                            <dx:ToolbarDeleteTableButton BeginGroup="True">
                                                                            </dx:ToolbarDeleteTableButton>
                                                                            <dx:ToolbarDeleteTableRowButton>
                                                                            </dx:ToolbarDeleteTableRowButton>
                                                                            <dx:ToolbarDeleteTableColumnButton>
                                                                            </dx:ToolbarDeleteTableColumnButton>
                                                                        </Items>
                                                                    </dx:ToolbarTableOperationsDropDownButton>
                                                                    <dx:ToolbarFullscreenButton BeginGroup="True">
                                                                    </dx:ToolbarFullscreenButton>
                                                                </Items>
                                                            </dx:HtmlEditorToolbar>
                                                            <dx:HtmlEditorToolbar Name="StandardToolbar2">
                                                                <Items>
                                                                    <dx:ToolbarParagraphFormattingEdit Width="120px">
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Text="Normal" Value="p" />
                                                                            <dx:ToolbarListEditItem Text="Heading  1" Value="h1" />
                                                                            <dx:ToolbarListEditItem Text="Heading  2" Value="h2" />
                                                                            <dx:ToolbarListEditItem Text="Heading  3" Value="h3" />
                                                                            <dx:ToolbarListEditItem Text="Heading  4" Value="h4" />
                                                                            <dx:ToolbarListEditItem Text="Heading  5" Value="h5" />
                                                                            <dx:ToolbarListEditItem Text="Heading  6" Value="h6" />
                                                                            <dx:ToolbarListEditItem Text="Address" Value="address" />
                                                                            <dx:ToolbarListEditItem Text="Normal (DIV)" Value="div" />
                                                                        </Items>
                                                                    </dx:ToolbarParagraphFormattingEdit>
                                                                    <dx:ToolbarFontNameEdit>
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman" />
                                                                            <dx:ToolbarListEditItem Text="Tahoma" Value="Tahoma" />
                                                                            <dx:ToolbarListEditItem Text="Verdana" Value="Verdana" />
                                                                            <dx:ToolbarListEditItem Text="Arial" Value="Arial" />
                                                                            <dx:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif" />
                                                                            <dx:ToolbarListEditItem Text="Courier" Value="Courier" />
                                                                        </Items>
                                                                    </dx:ToolbarFontNameEdit>
                                                                    <dx:ToolbarFontSizeEdit>
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Text="1 (8pt)" Value="1" />
                                                                            <dx:ToolbarListEditItem Text="2 (10pt)" Value="2" />
                                                                            <dx:ToolbarListEditItem Text="3 (12pt)" Value="3" />
                                                                            <dx:ToolbarListEditItem Text="4 (14pt)" Value="4" />
                                                                            <dx:ToolbarListEditItem Text="5 (18pt)" Value="5" />
                                                                            <dx:ToolbarListEditItem Text="6 (24pt)" Value="6" />
                                                                            <dx:ToolbarListEditItem Text="7 (36pt)" Value="7" />
                                                                        </Items>
                                                                    </dx:ToolbarFontSizeEdit>
                                                                    <dx:ToolbarBoldButton BeginGroup="True">
                                                                    </dx:ToolbarBoldButton>
                                                                    <dx:ToolbarItalicButton>
                                                                    </dx:ToolbarItalicButton>
                                                                    <dx:ToolbarUnderlineButton>
                                                                    </dx:ToolbarUnderlineButton>
                                                                    <dx:ToolbarStrikethroughButton>
                                                                    </dx:ToolbarStrikethroughButton>
                                                                    <dx:ToolbarJustifyLeftButton BeginGroup="True">
                                                                    </dx:ToolbarJustifyLeftButton>
                                                                    <dx:ToolbarJustifyCenterButton>
                                                                    </dx:ToolbarJustifyCenterButton>
                                                                    <dx:ToolbarJustifyRightButton>
                                                                    </dx:ToolbarJustifyRightButton>
                                                                    <dx:ToolbarJustifyFullButton>
                                                                    </dx:ToolbarJustifyFullButton>
                                                                    <dx:ToolbarBackColorButton BeginGroup="True">
                                                                    </dx:ToolbarBackColorButton>
                                                                    <dx:ToolbarFontColorButton>
                                                                    </dx:ToolbarFontColorButton>
                                                                </Items>
                                                            </dx:HtmlEditorToolbar>
                                                        </Toolbars>
                                                        <SettingsImageUpload UploadImageFolder="~/uploads/pages/"></SettingsImageUpload>
                                                    </dx:ASPxHtmlEditor>

                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Text="Göstəriciyə dair əlavə məlumat (EN)">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">
                                                    <dx:ASPxHtmlEditor ID="info_en" AutoResizeWithContainer="True" Height="200px"
                                                        runat="server"
                                                        SettingsImageUpload-UploadImageFolder="~/uploads/pages">
                                                        <Toolbars>
                                                            <dx:HtmlEditorToolbar Name="StandardToolbar1">
                                                                <Items>
                                                                    <dx:ToolbarCutButton>
                                                                    </dx:ToolbarCutButton>
                                                                    <dx:ToolbarCopyButton>
                                                                    </dx:ToolbarCopyButton>
                                                                    <dx:ToolbarPasteButton>
                                                                    </dx:ToolbarPasteButton>
                                                                    <dx:ToolbarPasteFromWordButton>
                                                                    </dx:ToolbarPasteFromWordButton>
                                                                    <dx:ToolbarUndoButton BeginGroup="True">
                                                                    </dx:ToolbarUndoButton>
                                                                    <dx:ToolbarRedoButton>
                                                                    </dx:ToolbarRedoButton>
                                                                    <dx:ToolbarRemoveFormatButton BeginGroup="True">
                                                                    </dx:ToolbarRemoveFormatButton>
                                                                    <dx:ToolbarSuperscriptButton BeginGroup="True">
                                                                    </dx:ToolbarSuperscriptButton>
                                                                    <dx:ToolbarSubscriptButton>
                                                                    </dx:ToolbarSubscriptButton>
                                                                    <dx:ToolbarInsertOrderedListButton BeginGroup="True">
                                                                    </dx:ToolbarInsertOrderedListButton>
                                                                    <dx:ToolbarInsertUnorderedListButton>
                                                                    </dx:ToolbarInsertUnorderedListButton>
                                                                    <dx:ToolbarIndentButton BeginGroup="True">
                                                                    </dx:ToolbarIndentButton>
                                                                    <dx:ToolbarOutdentButton>
                                                                    </dx:ToolbarOutdentButton>
                                                                    <dx:ToolbarInsertLinkDialogButton BeginGroup="True">
                                                                    </dx:ToolbarInsertLinkDialogButton>
                                                                    <dx:ToolbarUnlinkButton>
                                                                    </dx:ToolbarUnlinkButton>
                                                                    <dx:ToolbarInsertImageDialogButton>
                                                                    </dx:ToolbarInsertImageDialogButton>
                                                                    <dx:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                                                        <Items>
                                                                            <dx:ToolbarInsertTableDialogButton BeginGroup="True">
                                                                            </dx:ToolbarInsertTableDialogButton>
                                                                            <dx:ToolbarTablePropertiesDialogButton BeginGroup="True">
                                                                            </dx:ToolbarTablePropertiesDialogButton>
                                                                            <dx:ToolbarTableRowPropertiesDialogButton>
                                                                            </dx:ToolbarTableRowPropertiesDialogButton>
                                                                            <dx:ToolbarTableColumnPropertiesDialogButton>
                                                                            </dx:ToolbarTableColumnPropertiesDialogButton>
                                                                            <dx:ToolbarTableCellPropertiesDialogButton>
                                                                            </dx:ToolbarTableCellPropertiesDialogButton>
                                                                            <dx:ToolbarInsertTableRowAboveButton BeginGroup="True">
                                                                            </dx:ToolbarInsertTableRowAboveButton>
                                                                            <dx:ToolbarInsertTableRowBelowButton>
                                                                            </dx:ToolbarInsertTableRowBelowButton>
                                                                            <dx:ToolbarInsertTableColumnToLeftButton>
                                                                            </dx:ToolbarInsertTableColumnToLeftButton>
                                                                            <dx:ToolbarInsertTableColumnToRightButton>
                                                                            </dx:ToolbarInsertTableColumnToRightButton>
                                                                            <dx:ToolbarSplitTableCellHorizontallyButton BeginGroup="True">
                                                                            </dx:ToolbarSplitTableCellHorizontallyButton>
                                                                            <dx:ToolbarSplitTableCellVerticallyButton>
                                                                            </dx:ToolbarSplitTableCellVerticallyButton>
                                                                            <dx:ToolbarMergeTableCellRightButton>
                                                                            </dx:ToolbarMergeTableCellRightButton>
                                                                            <dx:ToolbarMergeTableCellDownButton>
                                                                            </dx:ToolbarMergeTableCellDownButton>
                                                                            <dx:ToolbarDeleteTableButton BeginGroup="True">
                                                                            </dx:ToolbarDeleteTableButton>
                                                                            <dx:ToolbarDeleteTableRowButton>
                                                                            </dx:ToolbarDeleteTableRowButton>
                                                                            <dx:ToolbarDeleteTableColumnButton>
                                                                            </dx:ToolbarDeleteTableColumnButton>
                                                                        </Items>
                                                                    </dx:ToolbarTableOperationsDropDownButton>
                                                                    <dx:ToolbarFullscreenButton BeginGroup="True">
                                                                    </dx:ToolbarFullscreenButton>
                                                                </Items>
                                                            </dx:HtmlEditorToolbar>
                                                            <dx:HtmlEditorToolbar Name="StandardToolbar2">
                                                                <Items>
                                                                    <dx:ToolbarParagraphFormattingEdit Width="120px">
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Text="Normal" Value="p" />
                                                                            <dx:ToolbarListEditItem Text="Heading  1" Value="h1" />
                                                                            <dx:ToolbarListEditItem Text="Heading  2" Value="h2" />
                                                                            <dx:ToolbarListEditItem Text="Heading  3" Value="h3" />
                                                                            <dx:ToolbarListEditItem Text="Heading  4" Value="h4" />
                                                                            <dx:ToolbarListEditItem Text="Heading  5" Value="h5" />
                                                                            <dx:ToolbarListEditItem Text="Heading  6" Value="h6" />
                                                                            <dx:ToolbarListEditItem Text="Address" Value="address" />
                                                                            <dx:ToolbarListEditItem Text="Normal (DIV)" Value="div" />
                                                                        </Items>
                                                                    </dx:ToolbarParagraphFormattingEdit>
                                                                    <dx:ToolbarFontNameEdit>
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman" />
                                                                            <dx:ToolbarListEditItem Text="Tahoma" Value="Tahoma" />
                                                                            <dx:ToolbarListEditItem Text="Verdana" Value="Verdana" />
                                                                            <dx:ToolbarListEditItem Text="Arial" Value="Arial" />
                                                                            <dx:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif" />
                                                                            <dx:ToolbarListEditItem Text="Courier" Value="Courier" />
                                                                        </Items>
                                                                    </dx:ToolbarFontNameEdit>
                                                                    <dx:ToolbarFontSizeEdit>
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Text="1 (8pt)" Value="1" />
                                                                            <dx:ToolbarListEditItem Text="2 (10pt)" Value="2" />
                                                                            <dx:ToolbarListEditItem Text="3 (12pt)" Value="3" />
                                                                            <dx:ToolbarListEditItem Text="4 (14pt)" Value="4" />
                                                                            <dx:ToolbarListEditItem Text="5 (18pt)" Value="5" />
                                                                            <dx:ToolbarListEditItem Text="6 (24pt)" Value="6" />
                                                                            <dx:ToolbarListEditItem Text="7 (36pt)" Value="7" />
                                                                        </Items>
                                                                    </dx:ToolbarFontSizeEdit>
                                                                    <dx:ToolbarBoldButton BeginGroup="True">
                                                                    </dx:ToolbarBoldButton>
                                                                    <dx:ToolbarItalicButton>
                                                                    </dx:ToolbarItalicButton>
                                                                    <dx:ToolbarUnderlineButton>
                                                                    </dx:ToolbarUnderlineButton>
                                                                    <dx:ToolbarStrikethroughButton>
                                                                    </dx:ToolbarStrikethroughButton>
                                                                    <dx:ToolbarJustifyLeftButton BeginGroup="True">
                                                                    </dx:ToolbarJustifyLeftButton>
                                                                    <dx:ToolbarJustifyCenterButton>
                                                                    </dx:ToolbarJustifyCenterButton>
                                                                    <dx:ToolbarJustifyRightButton>
                                                                    </dx:ToolbarJustifyRightButton>
                                                                    <dx:ToolbarJustifyFullButton>
                                                                    </dx:ToolbarJustifyFullButton>
                                                                    <dx:ToolbarBackColorButton BeginGroup="True">
                                                                    </dx:ToolbarBackColorButton>
                                                                    <dx:ToolbarFontColorButton>
                                                                    </dx:ToolbarFontColorButton>
                                                                </Items>
                                                            </dx:HtmlEditorToolbar>
                                                        </Toolbars>
                                                        <SettingsImageUpload UploadImageFolder="~/uploads/pages/"></SettingsImageUpload>
                                                    </dx:ASPxHtmlEditor>

                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Text="Mənbə (AZ)">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">
                                                    <dx:ASPxHtmlEditor ID="source_az" AutoResizeWithContainer="True" Height="200px"
                                                        runat="server"
                                                        SettingsImageUpload-UploadImageFolder="~/uploads/pages">
                                                        <Toolbars>
                                                            <dx:HtmlEditorToolbar Name="StandardToolbar1">
                                                                <Items>
                                                                    <dx:ToolbarCutButton>
                                                                    </dx:ToolbarCutButton>
                                                                    <dx:ToolbarCopyButton>
                                                                    </dx:ToolbarCopyButton>
                                                                    <dx:ToolbarPasteButton>
                                                                    </dx:ToolbarPasteButton>
                                                                    <dx:ToolbarPasteFromWordButton>
                                                                    </dx:ToolbarPasteFromWordButton>
                                                                    <dx:ToolbarUndoButton BeginGroup="True">
                                                                    </dx:ToolbarUndoButton>
                                                                    <dx:ToolbarRedoButton>
                                                                    </dx:ToolbarRedoButton>
                                                                    <dx:ToolbarRemoveFormatButton BeginGroup="True">
                                                                    </dx:ToolbarRemoveFormatButton>
                                                                    <dx:ToolbarSuperscriptButton BeginGroup="True">
                                                                    </dx:ToolbarSuperscriptButton>
                                                                    <dx:ToolbarSubscriptButton>
                                                                    </dx:ToolbarSubscriptButton>
                                                                    <dx:ToolbarInsertOrderedListButton BeginGroup="True">
                                                                    </dx:ToolbarInsertOrderedListButton>
                                                                    <dx:ToolbarInsertUnorderedListButton>
                                                                    </dx:ToolbarInsertUnorderedListButton>
                                                                    <dx:ToolbarIndentButton BeginGroup="True">
                                                                    </dx:ToolbarIndentButton>
                                                                    <dx:ToolbarOutdentButton>
                                                                    </dx:ToolbarOutdentButton>
                                                                    <dx:ToolbarInsertLinkDialogButton BeginGroup="True">
                                                                    </dx:ToolbarInsertLinkDialogButton>
                                                                    <dx:ToolbarUnlinkButton>
                                                                    </dx:ToolbarUnlinkButton>
                                                                    <dx:ToolbarInsertImageDialogButton>
                                                                    </dx:ToolbarInsertImageDialogButton>
                                                                    <dx:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                                                        <Items>
                                                                            <dx:ToolbarInsertTableDialogButton BeginGroup="True">
                                                                            </dx:ToolbarInsertTableDialogButton>
                                                                            <dx:ToolbarTablePropertiesDialogButton BeginGroup="True">
                                                                            </dx:ToolbarTablePropertiesDialogButton>
                                                                            <dx:ToolbarTableRowPropertiesDialogButton>
                                                                            </dx:ToolbarTableRowPropertiesDialogButton>
                                                                            <dx:ToolbarTableColumnPropertiesDialogButton>
                                                                            </dx:ToolbarTableColumnPropertiesDialogButton>
                                                                            <dx:ToolbarTableCellPropertiesDialogButton>
                                                                            </dx:ToolbarTableCellPropertiesDialogButton>
                                                                            <dx:ToolbarInsertTableRowAboveButton BeginGroup="True">
                                                                            </dx:ToolbarInsertTableRowAboveButton>
                                                                            <dx:ToolbarInsertTableRowBelowButton>
                                                                            </dx:ToolbarInsertTableRowBelowButton>
                                                                            <dx:ToolbarInsertTableColumnToLeftButton>
                                                                            </dx:ToolbarInsertTableColumnToLeftButton>
                                                                            <dx:ToolbarInsertTableColumnToRightButton>
                                                                            </dx:ToolbarInsertTableColumnToRightButton>
                                                                            <dx:ToolbarSplitTableCellHorizontallyButton BeginGroup="True">
                                                                            </dx:ToolbarSplitTableCellHorizontallyButton>
                                                                            <dx:ToolbarSplitTableCellVerticallyButton>
                                                                            </dx:ToolbarSplitTableCellVerticallyButton>
                                                                            <dx:ToolbarMergeTableCellRightButton>
                                                                            </dx:ToolbarMergeTableCellRightButton>
                                                                            <dx:ToolbarMergeTableCellDownButton>
                                                                            </dx:ToolbarMergeTableCellDownButton>
                                                                            <dx:ToolbarDeleteTableButton BeginGroup="True">
                                                                            </dx:ToolbarDeleteTableButton>
                                                                            <dx:ToolbarDeleteTableRowButton>
                                                                            </dx:ToolbarDeleteTableRowButton>
                                                                            <dx:ToolbarDeleteTableColumnButton>
                                                                            </dx:ToolbarDeleteTableColumnButton>
                                                                        </Items>
                                                                    </dx:ToolbarTableOperationsDropDownButton>
                                                                    <dx:ToolbarFullscreenButton BeginGroup="True">
                                                                    </dx:ToolbarFullscreenButton>
                                                                </Items>
                                                            </dx:HtmlEditorToolbar>
                                                            <dx:HtmlEditorToolbar Name="StandardToolbar2">
                                                                <Items>
                                                                    <dx:ToolbarParagraphFormattingEdit Width="120px">
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Text="Normal" Value="p" />
                                                                            <dx:ToolbarListEditItem Text="Heading  1" Value="h1" />
                                                                            <dx:ToolbarListEditItem Text="Heading  2" Value="h2" />
                                                                            <dx:ToolbarListEditItem Text="Heading  3" Value="h3" />
                                                                            <dx:ToolbarListEditItem Text="Heading  4" Value="h4" />
                                                                            <dx:ToolbarListEditItem Text="Heading  5" Value="h5" />
                                                                            <dx:ToolbarListEditItem Text="Heading  6" Value="h6" />
                                                                            <dx:ToolbarListEditItem Text="Address" Value="address" />
                                                                            <dx:ToolbarListEditItem Text="Normal (DIV)" Value="div" />
                                                                        </Items>
                                                                    </dx:ToolbarParagraphFormattingEdit>
                                                                    <dx:ToolbarFontNameEdit>
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman" />
                                                                            <dx:ToolbarListEditItem Text="Tahoma" Value="Tahoma" />
                                                                            <dx:ToolbarListEditItem Text="Verdana" Value="Verdana" />
                                                                            <dx:ToolbarListEditItem Text="Arial" Value="Arial" />
                                                                            <dx:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif" />
                                                                            <dx:ToolbarListEditItem Text="Courier" Value="Courier" />
                                                                        </Items>
                                                                    </dx:ToolbarFontNameEdit>
                                                                    <dx:ToolbarFontSizeEdit>
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Text="1 (8pt)" Value="1" />
                                                                            <dx:ToolbarListEditItem Text="2 (10pt)" Value="2" />
                                                                            <dx:ToolbarListEditItem Text="3 (12pt)" Value="3" />
                                                                            <dx:ToolbarListEditItem Text="4 (14pt)" Value="4" />
                                                                            <dx:ToolbarListEditItem Text="5 (18pt)" Value="5" />
                                                                            <dx:ToolbarListEditItem Text="6 (24pt)" Value="6" />
                                                                            <dx:ToolbarListEditItem Text="7 (36pt)" Value="7" />
                                                                        </Items>
                                                                    </dx:ToolbarFontSizeEdit>
                                                                    <dx:ToolbarBoldButton BeginGroup="True">
                                                                    </dx:ToolbarBoldButton>
                                                                    <dx:ToolbarItalicButton>
                                                                    </dx:ToolbarItalicButton>
                                                                    <dx:ToolbarUnderlineButton>
                                                                    </dx:ToolbarUnderlineButton>
                                                                    <dx:ToolbarStrikethroughButton>
                                                                    </dx:ToolbarStrikethroughButton>
                                                                    <dx:ToolbarJustifyLeftButton BeginGroup="True">
                                                                    </dx:ToolbarJustifyLeftButton>
                                                                    <dx:ToolbarJustifyCenterButton>
                                                                    </dx:ToolbarJustifyCenterButton>
                                                                    <dx:ToolbarJustifyRightButton>
                                                                    </dx:ToolbarJustifyRightButton>
                                                                    <dx:ToolbarJustifyFullButton>
                                                                    </dx:ToolbarJustifyFullButton>
                                                                    <dx:ToolbarBackColorButton BeginGroup="True">
                                                                    </dx:ToolbarBackColorButton>
                                                                    <dx:ToolbarFontColorButton>
                                                                    </dx:ToolbarFontColorButton>
                                                                </Items>
                                                            </dx:HtmlEditorToolbar>
                                                        </Toolbars>
                                                        <SettingsImageUpload UploadImageFolder="~/uploads/pages/"></SettingsImageUpload>
                                                    </dx:ASPxHtmlEditor>

                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Text="Mənbə (EN)">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">
                                                    <dx:ASPxHtmlEditor ID="source_en" AutoResizeWithContainer="True" Height="200px"
                                                        runat="server"
                                                        SettingsImageUpload-UploadImageFolder="~/uploads/pages">
                                                        <Toolbars>
                                                            <dx:HtmlEditorToolbar Name="StandardToolbar1">
                                                                <Items>
                                                                    <dx:ToolbarCutButton>
                                                                    </dx:ToolbarCutButton>
                                                                    <dx:ToolbarCopyButton>
                                                                    </dx:ToolbarCopyButton>
                                                                    <dx:ToolbarPasteButton>
                                                                    </dx:ToolbarPasteButton>
                                                                    <dx:ToolbarPasteFromWordButton>
                                                                    </dx:ToolbarPasteFromWordButton>
                                                                    <dx:ToolbarUndoButton BeginGroup="True">
                                                                    </dx:ToolbarUndoButton>
                                                                    <dx:ToolbarRedoButton>
                                                                    </dx:ToolbarRedoButton>
                                                                    <dx:ToolbarRemoveFormatButton BeginGroup="True">
                                                                    </dx:ToolbarRemoveFormatButton>
                                                                    <dx:ToolbarSuperscriptButton BeginGroup="True">
                                                                    </dx:ToolbarSuperscriptButton>
                                                                    <dx:ToolbarSubscriptButton>
                                                                    </dx:ToolbarSubscriptButton>
                                                                    <dx:ToolbarInsertOrderedListButton BeginGroup="True">
                                                                    </dx:ToolbarInsertOrderedListButton>
                                                                    <dx:ToolbarInsertUnorderedListButton>
                                                                    </dx:ToolbarInsertUnorderedListButton>
                                                                    <dx:ToolbarIndentButton BeginGroup="True">
                                                                    </dx:ToolbarIndentButton>
                                                                    <dx:ToolbarOutdentButton>
                                                                    </dx:ToolbarOutdentButton>
                                                                    <dx:ToolbarInsertLinkDialogButton BeginGroup="True">
                                                                    </dx:ToolbarInsertLinkDialogButton>
                                                                    <dx:ToolbarUnlinkButton>
                                                                    </dx:ToolbarUnlinkButton>
                                                                    <dx:ToolbarInsertImageDialogButton>
                                                                    </dx:ToolbarInsertImageDialogButton>
                                                                    <dx:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                                                        <Items>
                                                                            <dx:ToolbarInsertTableDialogButton BeginGroup="True">
                                                                            </dx:ToolbarInsertTableDialogButton>
                                                                            <dx:ToolbarTablePropertiesDialogButton BeginGroup="True">
                                                                            </dx:ToolbarTablePropertiesDialogButton>
                                                                            <dx:ToolbarTableRowPropertiesDialogButton>
                                                                            </dx:ToolbarTableRowPropertiesDialogButton>
                                                                            <dx:ToolbarTableColumnPropertiesDialogButton>
                                                                            </dx:ToolbarTableColumnPropertiesDialogButton>
                                                                            <dx:ToolbarTableCellPropertiesDialogButton>
                                                                            </dx:ToolbarTableCellPropertiesDialogButton>
                                                                            <dx:ToolbarInsertTableRowAboveButton BeginGroup="True">
                                                                            </dx:ToolbarInsertTableRowAboveButton>
                                                                            <dx:ToolbarInsertTableRowBelowButton>
                                                                            </dx:ToolbarInsertTableRowBelowButton>
                                                                            <dx:ToolbarInsertTableColumnToLeftButton>
                                                                            </dx:ToolbarInsertTableColumnToLeftButton>
                                                                            <dx:ToolbarInsertTableColumnToRightButton>
                                                                            </dx:ToolbarInsertTableColumnToRightButton>
                                                                            <dx:ToolbarSplitTableCellHorizontallyButton BeginGroup="True">
                                                                            </dx:ToolbarSplitTableCellHorizontallyButton>
                                                                            <dx:ToolbarSplitTableCellVerticallyButton>
                                                                            </dx:ToolbarSplitTableCellVerticallyButton>
                                                                            <dx:ToolbarMergeTableCellRightButton>
                                                                            </dx:ToolbarMergeTableCellRightButton>
                                                                            <dx:ToolbarMergeTableCellDownButton>
                                                                            </dx:ToolbarMergeTableCellDownButton>
                                                                            <dx:ToolbarDeleteTableButton BeginGroup="True">
                                                                            </dx:ToolbarDeleteTableButton>
                                                                            <dx:ToolbarDeleteTableRowButton>
                                                                            </dx:ToolbarDeleteTableRowButton>
                                                                            <dx:ToolbarDeleteTableColumnButton>
                                                                            </dx:ToolbarDeleteTableColumnButton>
                                                                        </Items>
                                                                    </dx:ToolbarTableOperationsDropDownButton>
                                                                    <dx:ToolbarFullscreenButton BeginGroup="True">
                                                                    </dx:ToolbarFullscreenButton>
                                                                </Items>
                                                            </dx:HtmlEditorToolbar>
                                                            <dx:HtmlEditorToolbar Name="StandardToolbar2">
                                                                <Items>
                                                                    <dx:ToolbarParagraphFormattingEdit Width="120px">
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Text="Normal" Value="p" />
                                                                            <dx:ToolbarListEditItem Text="Heading  1" Value="h1" />
                                                                            <dx:ToolbarListEditItem Text="Heading  2" Value="h2" />
                                                                            <dx:ToolbarListEditItem Text="Heading  3" Value="h3" />
                                                                            <dx:ToolbarListEditItem Text="Heading  4" Value="h4" />
                                                                            <dx:ToolbarListEditItem Text="Heading  5" Value="h5" />
                                                                            <dx:ToolbarListEditItem Text="Heading  6" Value="h6" />
                                                                            <dx:ToolbarListEditItem Text="Address" Value="address" />
                                                                            <dx:ToolbarListEditItem Text="Normal (DIV)" Value="div" />
                                                                        </Items>
                                                                    </dx:ToolbarParagraphFormattingEdit>
                                                                    <dx:ToolbarFontNameEdit>
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman" />
                                                                            <dx:ToolbarListEditItem Text="Tahoma" Value="Tahoma" />
                                                                            <dx:ToolbarListEditItem Text="Verdana" Value="Verdana" />
                                                                            <dx:ToolbarListEditItem Text="Arial" Value="Arial" />
                                                                            <dx:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif" />
                                                                            <dx:ToolbarListEditItem Text="Courier" Value="Courier" />
                                                                        </Items>
                                                                    </dx:ToolbarFontNameEdit>
                                                                    <dx:ToolbarFontSizeEdit>
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Text="1 (8pt)" Value="1" />
                                                                            <dx:ToolbarListEditItem Text="2 (10pt)" Value="2" />
                                                                            <dx:ToolbarListEditItem Text="3 (12pt)" Value="3" />
                                                                            <dx:ToolbarListEditItem Text="4 (14pt)" Value="4" />
                                                                            <dx:ToolbarListEditItem Text="5 (18pt)" Value="5" />
                                                                            <dx:ToolbarListEditItem Text="6 (24pt)" Value="6" />
                                                                            <dx:ToolbarListEditItem Text="7 (36pt)" Value="7" />
                                                                        </Items>
                                                                    </dx:ToolbarFontSizeEdit>
                                                                    <dx:ToolbarBoldButton BeginGroup="True">
                                                                    </dx:ToolbarBoldButton>
                                                                    <dx:ToolbarItalicButton>
                                                                    </dx:ToolbarItalicButton>
                                                                    <dx:ToolbarUnderlineButton>
                                                                    </dx:ToolbarUnderlineButton>
                                                                    <dx:ToolbarStrikethroughButton>
                                                                    </dx:ToolbarStrikethroughButton>
                                                                    <dx:ToolbarJustifyLeftButton BeginGroup="True">
                                                                    </dx:ToolbarJustifyLeftButton>
                                                                    <dx:ToolbarJustifyCenterButton>
                                                                    </dx:ToolbarJustifyCenterButton>
                                                                    <dx:ToolbarJustifyRightButton>
                                                                    </dx:ToolbarJustifyRightButton>
                                                                    <dx:ToolbarJustifyFullButton>
                                                                    </dx:ToolbarJustifyFullButton>
                                                                    <dx:ToolbarBackColorButton BeginGroup="True">
                                                                    </dx:ToolbarBackColorButton>
                                                                    <dx:ToolbarFontColorButton>
                                                                    </dx:ToolbarFontColorButton>
                                                                </Items>
                                                            </dx:HtmlEditorToolbar>
                                                        </Toolbars>
                                                        <SettingsImageUpload UploadImageFolder="~/uploads/pages/"></SettingsImageUpload>
                                                    </dx:ASPxHtmlEditor>

                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Text="QEYD (AZ)">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">
                                                    <dx:ASPxHtmlEditor ID="note_az" AutoResizeWithContainer="True" Height="200px"
                                                        runat="server"
                                                        SettingsImageUpload-UploadImageFolder="~/uploads/pages">
                                                        <Toolbars>
                                                            <dx:HtmlEditorToolbar Name="StandardToolbar1">
                                                                <Items>
                                                                    <dx:ToolbarCutButton>
                                                                    </dx:ToolbarCutButton>
                                                                    <dx:ToolbarCopyButton>
                                                                    </dx:ToolbarCopyButton>
                                                                    <dx:ToolbarPasteButton>
                                                                    </dx:ToolbarPasteButton>
                                                                    <dx:ToolbarPasteFromWordButton>
                                                                    </dx:ToolbarPasteFromWordButton>
                                                                    <dx:ToolbarUndoButton BeginGroup="True">
                                                                    </dx:ToolbarUndoButton>
                                                                    <dx:ToolbarRedoButton>
                                                                    </dx:ToolbarRedoButton>
                                                                    <dx:ToolbarRemoveFormatButton BeginGroup="True">
                                                                    </dx:ToolbarRemoveFormatButton>
                                                                    <dx:ToolbarSuperscriptButton BeginGroup="True">
                                                                    </dx:ToolbarSuperscriptButton>
                                                                    <dx:ToolbarSubscriptButton>
                                                                    </dx:ToolbarSubscriptButton>
                                                                    <dx:ToolbarInsertOrderedListButton BeginGroup="True">
                                                                    </dx:ToolbarInsertOrderedListButton>
                                                                    <dx:ToolbarInsertUnorderedListButton>
                                                                    </dx:ToolbarInsertUnorderedListButton>
                                                                    <dx:ToolbarIndentButton BeginGroup="True">
                                                                    </dx:ToolbarIndentButton>
                                                                    <dx:ToolbarOutdentButton>
                                                                    </dx:ToolbarOutdentButton>
                                                                    <dx:ToolbarInsertLinkDialogButton BeginGroup="True">
                                                                    </dx:ToolbarInsertLinkDialogButton>
                                                                    <dx:ToolbarUnlinkButton>
                                                                    </dx:ToolbarUnlinkButton>
                                                                    <dx:ToolbarInsertImageDialogButton>
                                                                    </dx:ToolbarInsertImageDialogButton>
                                                                    <dx:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                                                        <Items>
                                                                            <dx:ToolbarInsertTableDialogButton BeginGroup="True">
                                                                            </dx:ToolbarInsertTableDialogButton>
                                                                            <dx:ToolbarTablePropertiesDialogButton BeginGroup="True">
                                                                            </dx:ToolbarTablePropertiesDialogButton>
                                                                            <dx:ToolbarTableRowPropertiesDialogButton>
                                                                            </dx:ToolbarTableRowPropertiesDialogButton>
                                                                            <dx:ToolbarTableColumnPropertiesDialogButton>
                                                                            </dx:ToolbarTableColumnPropertiesDialogButton>
                                                                            <dx:ToolbarTableCellPropertiesDialogButton>
                                                                            </dx:ToolbarTableCellPropertiesDialogButton>
                                                                            <dx:ToolbarInsertTableRowAboveButton BeginGroup="True">
                                                                            </dx:ToolbarInsertTableRowAboveButton>
                                                                            <dx:ToolbarInsertTableRowBelowButton>
                                                                            </dx:ToolbarInsertTableRowBelowButton>
                                                                            <dx:ToolbarInsertTableColumnToLeftButton>
                                                                            </dx:ToolbarInsertTableColumnToLeftButton>
                                                                            <dx:ToolbarInsertTableColumnToRightButton>
                                                                            </dx:ToolbarInsertTableColumnToRightButton>
                                                                            <dx:ToolbarSplitTableCellHorizontallyButton BeginGroup="True">
                                                                            </dx:ToolbarSplitTableCellHorizontallyButton>
                                                                            <dx:ToolbarSplitTableCellVerticallyButton>
                                                                            </dx:ToolbarSplitTableCellVerticallyButton>
                                                                            <dx:ToolbarMergeTableCellRightButton>
                                                                            </dx:ToolbarMergeTableCellRightButton>
                                                                            <dx:ToolbarMergeTableCellDownButton>
                                                                            </dx:ToolbarMergeTableCellDownButton>
                                                                            <dx:ToolbarDeleteTableButton BeginGroup="True">
                                                                            </dx:ToolbarDeleteTableButton>
                                                                            <dx:ToolbarDeleteTableRowButton>
                                                                            </dx:ToolbarDeleteTableRowButton>
                                                                            <dx:ToolbarDeleteTableColumnButton>
                                                                            </dx:ToolbarDeleteTableColumnButton>
                                                                        </Items>
                                                                    </dx:ToolbarTableOperationsDropDownButton>
                                                                    <dx:ToolbarFullscreenButton BeginGroup="True">
                                                                    </dx:ToolbarFullscreenButton>
                                                                </Items>
                                                            </dx:HtmlEditorToolbar>
                                                            <dx:HtmlEditorToolbar Name="StandardToolbar2">
                                                                <Items>
                                                                    <dx:ToolbarParagraphFormattingEdit Width="120px">
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Text="Normal" Value="p" />
                                                                            <dx:ToolbarListEditItem Text="Heading  1" Value="h1" />
                                                                            <dx:ToolbarListEditItem Text="Heading  2" Value="h2" />
                                                                            <dx:ToolbarListEditItem Text="Heading  3" Value="h3" />
                                                                            <dx:ToolbarListEditItem Text="Heading  4" Value="h4" />
                                                                            <dx:ToolbarListEditItem Text="Heading  5" Value="h5" />
                                                                            <dx:ToolbarListEditItem Text="Heading  6" Value="h6" />
                                                                            <dx:ToolbarListEditItem Text="Address" Value="address" />
                                                                            <dx:ToolbarListEditItem Text="Normal (DIV)" Value="div" />
                                                                        </Items>
                                                                    </dx:ToolbarParagraphFormattingEdit>
                                                                    <dx:ToolbarFontNameEdit>
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman" />
                                                                            <dx:ToolbarListEditItem Text="Tahoma" Value="Tahoma" />
                                                                            <dx:ToolbarListEditItem Text="Verdana" Value="Verdana" />
                                                                            <dx:ToolbarListEditItem Text="Arial" Value="Arial" />
                                                                            <dx:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif" />
                                                                            <dx:ToolbarListEditItem Text="Courier" Value="Courier" />
                                                                        </Items>
                                                                    </dx:ToolbarFontNameEdit>
                                                                    <dx:ToolbarFontSizeEdit>
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Text="1 (8pt)" Value="1" />
                                                                            <dx:ToolbarListEditItem Text="2 (10pt)" Value="2" />
                                                                            <dx:ToolbarListEditItem Text="3 (12pt)" Value="3" />
                                                                            <dx:ToolbarListEditItem Text="4 (14pt)" Value="4" />
                                                                            <dx:ToolbarListEditItem Text="5 (18pt)" Value="5" />
                                                                            <dx:ToolbarListEditItem Text="6 (24pt)" Value="6" />
                                                                            <dx:ToolbarListEditItem Text="7 (36pt)" Value="7" />
                                                                        </Items>
                                                                    </dx:ToolbarFontSizeEdit>
                                                                    <dx:ToolbarBoldButton BeginGroup="True">
                                                                    </dx:ToolbarBoldButton>
                                                                    <dx:ToolbarItalicButton>
                                                                    </dx:ToolbarItalicButton>
                                                                    <dx:ToolbarUnderlineButton>
                                                                    </dx:ToolbarUnderlineButton>
                                                                    <dx:ToolbarStrikethroughButton>
                                                                    </dx:ToolbarStrikethroughButton>
                                                                    <dx:ToolbarJustifyLeftButton BeginGroup="True">
                                                                    </dx:ToolbarJustifyLeftButton>
                                                                    <dx:ToolbarJustifyCenterButton>
                                                                    </dx:ToolbarJustifyCenterButton>
                                                                    <dx:ToolbarJustifyRightButton>
                                                                    </dx:ToolbarJustifyRightButton>
                                                                    <dx:ToolbarJustifyFullButton>
                                                                    </dx:ToolbarJustifyFullButton>
                                                                    <dx:ToolbarBackColorButton BeginGroup="True">
                                                                    </dx:ToolbarBackColorButton>
                                                                    <dx:ToolbarFontColorButton>
                                                                    </dx:ToolbarFontColorButton>
                                                                </Items>
                                                            </dx:HtmlEditorToolbar>
                                                        </Toolbars>
                                                        <SettingsImageUpload UploadImageFolder="~/uploads/pages/"></SettingsImageUpload>
                                                    </dx:ASPxHtmlEditor>

                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Text="QEYD (EN)">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">
                                                    <dx:ASPxHtmlEditor ID="note_en" AutoResizeWithContainer="True" Height="200px"
                                                        runat="server"
                                                        SettingsImageUpload-UploadImageFolder="~/uploads/pages">
                                                        <Toolbars>
                                                            <dx:HtmlEditorToolbar Name="StandardToolbar1">
                                                                <Items>
                                                                    <dx:ToolbarCutButton>
                                                                    </dx:ToolbarCutButton>
                                                                    <dx:ToolbarCopyButton>
                                                                    </dx:ToolbarCopyButton>
                                                                    <dx:ToolbarPasteButton>
                                                                    </dx:ToolbarPasteButton>
                                                                    <dx:ToolbarPasteFromWordButton>
                                                                    </dx:ToolbarPasteFromWordButton>
                                                                    <dx:ToolbarUndoButton BeginGroup="True">
                                                                    </dx:ToolbarUndoButton>
                                                                    <dx:ToolbarRedoButton>
                                                                    </dx:ToolbarRedoButton>
                                                                    <dx:ToolbarRemoveFormatButton BeginGroup="True">
                                                                    </dx:ToolbarRemoveFormatButton>
                                                                    <dx:ToolbarSuperscriptButton BeginGroup="True">
                                                                    </dx:ToolbarSuperscriptButton>
                                                                    <dx:ToolbarSubscriptButton>
                                                                    </dx:ToolbarSubscriptButton>
                                                                    <dx:ToolbarInsertOrderedListButton BeginGroup="True">
                                                                    </dx:ToolbarInsertOrderedListButton>
                                                                    <dx:ToolbarInsertUnorderedListButton>
                                                                    </dx:ToolbarInsertUnorderedListButton>
                                                                    <dx:ToolbarIndentButton BeginGroup="True">
                                                                    </dx:ToolbarIndentButton>
                                                                    <dx:ToolbarOutdentButton>
                                                                    </dx:ToolbarOutdentButton>
                                                                    <dx:ToolbarInsertLinkDialogButton BeginGroup="True">
                                                                    </dx:ToolbarInsertLinkDialogButton>
                                                                    <dx:ToolbarUnlinkButton>
                                                                    </dx:ToolbarUnlinkButton>
                                                                    <dx:ToolbarInsertImageDialogButton>
                                                                    </dx:ToolbarInsertImageDialogButton>
                                                                    <dx:ToolbarTableOperationsDropDownButton BeginGroup="True">
                                                                        <Items>
                                                                            <dx:ToolbarInsertTableDialogButton BeginGroup="True">
                                                                            </dx:ToolbarInsertTableDialogButton>
                                                                            <dx:ToolbarTablePropertiesDialogButton BeginGroup="True">
                                                                            </dx:ToolbarTablePropertiesDialogButton>
                                                                            <dx:ToolbarTableRowPropertiesDialogButton>
                                                                            </dx:ToolbarTableRowPropertiesDialogButton>
                                                                            <dx:ToolbarTableColumnPropertiesDialogButton>
                                                                            </dx:ToolbarTableColumnPropertiesDialogButton>
                                                                            <dx:ToolbarTableCellPropertiesDialogButton>
                                                                            </dx:ToolbarTableCellPropertiesDialogButton>
                                                                            <dx:ToolbarInsertTableRowAboveButton BeginGroup="True">
                                                                            </dx:ToolbarInsertTableRowAboveButton>
                                                                            <dx:ToolbarInsertTableRowBelowButton>
                                                                            </dx:ToolbarInsertTableRowBelowButton>
                                                                            <dx:ToolbarInsertTableColumnToLeftButton>
                                                                            </dx:ToolbarInsertTableColumnToLeftButton>
                                                                            <dx:ToolbarInsertTableColumnToRightButton>
                                                                            </dx:ToolbarInsertTableColumnToRightButton>
                                                                            <dx:ToolbarSplitTableCellHorizontallyButton BeginGroup="True">
                                                                            </dx:ToolbarSplitTableCellHorizontallyButton>
                                                                            <dx:ToolbarSplitTableCellVerticallyButton>
                                                                            </dx:ToolbarSplitTableCellVerticallyButton>
                                                                            <dx:ToolbarMergeTableCellRightButton>
                                                                            </dx:ToolbarMergeTableCellRightButton>
                                                                            <dx:ToolbarMergeTableCellDownButton>
                                                                            </dx:ToolbarMergeTableCellDownButton>
                                                                            <dx:ToolbarDeleteTableButton BeginGroup="True">
                                                                            </dx:ToolbarDeleteTableButton>
                                                                            <dx:ToolbarDeleteTableRowButton>
                                                                            </dx:ToolbarDeleteTableRowButton>
                                                                            <dx:ToolbarDeleteTableColumnButton>
                                                                            </dx:ToolbarDeleteTableColumnButton>
                                                                        </Items>
                                                                    </dx:ToolbarTableOperationsDropDownButton>
                                                                    <dx:ToolbarFullscreenButton BeginGroup="True">
                                                                    </dx:ToolbarFullscreenButton>
                                                                </Items>
                                                            </dx:HtmlEditorToolbar>
                                                            <dx:HtmlEditorToolbar Name="StandardToolbar2">
                                                                <Items>
                                                                    <dx:ToolbarParagraphFormattingEdit Width="120px">
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Text="Normal" Value="p" />
                                                                            <dx:ToolbarListEditItem Text="Heading  1" Value="h1" />
                                                                            <dx:ToolbarListEditItem Text="Heading  2" Value="h2" />
                                                                            <dx:ToolbarListEditItem Text="Heading  3" Value="h3" />
                                                                            <dx:ToolbarListEditItem Text="Heading  4" Value="h4" />
                                                                            <dx:ToolbarListEditItem Text="Heading  5" Value="h5" />
                                                                            <dx:ToolbarListEditItem Text="Heading  6" Value="h6" />
                                                                            <dx:ToolbarListEditItem Text="Address" Value="address" />
                                                                            <dx:ToolbarListEditItem Text="Normal (DIV)" Value="div" />
                                                                        </Items>
                                                                    </dx:ToolbarParagraphFormattingEdit>
                                                                    <dx:ToolbarFontNameEdit>
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Text="Times New Roman" Value="Times New Roman" />
                                                                            <dx:ToolbarListEditItem Text="Tahoma" Value="Tahoma" />
                                                                            <dx:ToolbarListEditItem Text="Verdana" Value="Verdana" />
                                                                            <dx:ToolbarListEditItem Text="Arial" Value="Arial" />
                                                                            <dx:ToolbarListEditItem Text="MS Sans Serif" Value="MS Sans Serif" />
                                                                            <dx:ToolbarListEditItem Text="Courier" Value="Courier" />
                                                                        </Items>
                                                                    </dx:ToolbarFontNameEdit>
                                                                    <dx:ToolbarFontSizeEdit>
                                                                        <Items>
                                                                            <dx:ToolbarListEditItem Text="1 (8pt)" Value="1" />
                                                                            <dx:ToolbarListEditItem Text="2 (10pt)" Value="2" />
                                                                            <dx:ToolbarListEditItem Text="3 (12pt)" Value="3" />
                                                                            <dx:ToolbarListEditItem Text="4 (14pt)" Value="4" />
                                                                            <dx:ToolbarListEditItem Text="5 (18pt)" Value="5" />
                                                                            <dx:ToolbarListEditItem Text="6 (24pt)" Value="6" />
                                                                            <dx:ToolbarListEditItem Text="7 (36pt)" Value="7" />
                                                                        </Items>
                                                                    </dx:ToolbarFontSizeEdit>
                                                                    <dx:ToolbarBoldButton BeginGroup="True">
                                                                    </dx:ToolbarBoldButton>
                                                                    <dx:ToolbarItalicButton>
                                                                    </dx:ToolbarItalicButton>
                                                                    <dx:ToolbarUnderlineButton>
                                                                    </dx:ToolbarUnderlineButton>
                                                                    <dx:ToolbarStrikethroughButton>
                                                                    </dx:ToolbarStrikethroughButton>
                                                                    <dx:ToolbarJustifyLeftButton BeginGroup="True">
                                                                    </dx:ToolbarJustifyLeftButton>
                                                                    <dx:ToolbarJustifyCenterButton>
                                                                    </dx:ToolbarJustifyCenterButton>
                                                                    <dx:ToolbarJustifyRightButton>
                                                                    </dx:ToolbarJustifyRightButton>
                                                                    <dx:ToolbarJustifyFullButton>
                                                                    </dx:ToolbarJustifyFullButton>
                                                                    <dx:ToolbarBackColorButton BeginGroup="True">
                                                                    </dx:ToolbarBackColorButton>
                                                                    <dx:ToolbarFontColorButton>
                                                                    </dx:ToolbarFontColorButton>
                                                                </Items>
                                                            </dx:HtmlEditorToolbar>
                                                        </Toolbars>
                                                        <SettingsImageUpload UploadImageFolder="~/uploads/pages/"></SettingsImageUpload>
                                                    </dx:ASPxHtmlEditor>

                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                    </TabPages>
                                </dx:ASPxPageControl>
                                <div>
                                    <asp:Label Text="" ForeColor="Red" ID="lblPopError" runat="server" />
                                </div>
                                <div style="width: 100%; text-align: right; padding: 5px">

                                    <dx:ASPxButton ID="btnSave" runat="server" CssClass="btn btn-success" Text="Yadda saxla" OnClick="btnSave_Click">
                                        <ClientSideEvents Click="function (s, e) { popup.Hide(); e.processOnServer = true; }" />
                                    </dx:ASPxButton>
                                    <dx:ASPxButton ID="btnCancel" runat="server" CssClass="btn btn-default" Text="Ləğv et" OnClick="btnCancel_Click">
                                        <ClientSideEvents Click="function (s, e) { popup.Hide();  }" />
                                    </dx:ASPxButton>
                                </div>

                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                </asp:Panel>
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

