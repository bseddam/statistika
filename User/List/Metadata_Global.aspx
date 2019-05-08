<%@ Page Title="" Language="C#" MasterPageFile="~/User/MasterPage.master" AutoEventWireup="true" CodeFile="Metadata_Global.aspx.cs" Inherits="User_List_Metadata_Global" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style>
        .treelist-new-column a {
            visibility: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" Runat="Server">
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
                        <label>Göstəricilərin siyahısı</label>
                        <dx:ASPxComboBox ID="Indicator" ClientInstanceName="Indicator"
                            runat="server" NullText="Siyahıdan seçin"
                            AnimationType="Slide" EnableTheming="True"
                            IncrementalFilteringMode="Contains" LoadingPanelText="Yüklənir&amp;hellip;"
                            TextField="name_az" ValueField="id"
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
                                            <%-- <asp:LinkButton ID="btnAdd" runat="server" CommandArgument="add" OnClick="LnkPnlMenu_Click"><i class="fa fa-plus"></i> Yeni</asp:LinkButton>--%>
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
                        AutoGenerateColumns="False" Width="100%" SettingsBehavior-ConfirmDelete="true"
                        KeyFieldName="id">
                        <Settings GridLines="Both" />
                        <SettingsPopup EditForm-AllowResize="True" EditForm-Modal="true" EditForm-HorizontalAlign="Center" EditForm-VerticalAlign="Above" EditForm-Height="400px"></SettingsPopup>
                        <SettingsEditing Mode="PopupEditForm"></SettingsEditing>
                        <Columns>
                            <dx:GridViewDataColumn Caption="Maddə" FieldName="id" VisibleIndex="1" Width="70">
                                <EditFormSettings Visible="False" />
                                <DataItemTemplate>
                                    <%= _no++ %>
                                </DataItemTemplate>
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataComboBoxColumn Caption="Anlayışın adı" ReadOnly="true" FieldName="list_id" VisibleIndex="2">
                                <EditFormSettings ColumnSpan="2" />
                            </dx:GridViewDataComboBoxColumn>
                            <dx:GridViewDataTextColumn Caption="Anlayışın təsviri (AZ)" PropertiesTextEdit-EncodeHtml="false"  FieldName="name_az" VisibleIndex="3">
                                <EditFormSettings ColumnSpan="2" Visible="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Anlayışın təsviri (EN)" PropertiesTextEdit-EncodeHtml="false" FieldName="name_en" VisibleIndex="4">
                                <EditFormSettings ColumnSpan="2" Visible="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewCommandColumn Caption="#" Width="70" VisibleIndex="7">
                                <EditButton Visible="true" />
                                <DeleteButton Visible="false" />
                            </dx:GridViewCommandColumn>
                        </Columns>
                        <Settings ShowFilterRow="true" VerticalScrollableHeight="300" VerticalScrollBarMode="Visible" />
                        <SettingsPager Mode="ShowAllRecords">
                            <PageSizeItemSettings Visible="true"></PageSizeItemSettings>
                        </SettingsPager>
                        <SettingsText CommandNew="Yeni" PopupEditFormCaption="Form" CommandDelete="Sil"
                            CommandCancel="Ləğv et" CommandEdit="Yenilə" CommandUpdate="Yadda saxla"
                            CustomizationWindowCaption="Sütun seçin" ConfirmDelete="Silmək istəditinizə əminsiniz?" />
                        <SettingsDataSecurity AllowDelete="true" />
                        <Styles Header-CssClass="grid-header"></Styles>
                        <Templates>
                            <EditForm>
                                <dx:ASPxPageControl ID="tabs" runat="server" ActiveTabIndex="1" Width="100%">
                                    <TabPages>
                                        <dx:TabPage Text="Ümumi məlumat">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">
                                                    <dx:ASPxGridViewTemplateReplacement runat="server"
                                                        ReplacementType="EditFormEditors" />
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Text="Anlayışın təsviri (AZ)">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">
                                                    <dx:ASPxHtmlEditor ID="name_az" AutoResizeWithContainer="True" Height="200px"
                                                        Html='<%#Eval("name_az") %>'
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
                                        <dx:TabPage Text="Anlayışın təsviri (EN)">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">
                                                    <dx:ASPxHtmlEditor ID="name_en" AutoResizeWithContainer="True" Height="200px"
                                                        Html='<%#Eval("name_en") %>'
                                                        runat="server" SettingsImageUpload-UploadImageFolder="~/uploads/pages">
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
                                <div style="text-align: right; padding: 10px;">
                                    <dx:ASPxGridViewTemplateReplacement runat="server" ReplacementType="EditFormUpdateButton" />
                                    <dx:ASPxGridViewTemplateReplacement runat="server" ReplacementType="EditFormCancelButton" />
                                </div>
                            </EditForm>
                        </Templates>
                    </dx:ASPxGridView>
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

