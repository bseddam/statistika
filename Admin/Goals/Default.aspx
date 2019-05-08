<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Goals_Default" %>

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
                                            <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn_add" CommandArgument="add" OnClick="LnkPnlMenu_Click"><i class="fa fa-plus"></i> Yeni</asp:LinkButton>
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
                    <dx:ASPxGridView ID="treeList" runat="server"
                        OnRowUpdating="treeList_RowUpdating"
                        OnRowInserting="treeList_RowInserting"
                        Styles-Cell-Wrap="True"
                        AutoGenerateColumns="false" Width="100%"
                        KeyFieldName="id">
                        <Settings GridLines="Both" />
                        <SettingsLoadingPanel Text="Yüklənir…" />
                        <SettingsEditing Mode="PopupEditForm" />
                        <SettingsPager Mode="ShowAllRecords"></SettingsPager>
                        <SettingsPopup EditForm-Width="600" EditForm-Modal="True" EditForm-VerticalAlign="Middle"
                            EditForm-HorizontalAlign="WindowCenter" EditForm-AllowResize="True" />
                        <Columns>
                            <dx:GridViewDataSpinEditColumn Caption="Sıra No" FieldName="priority" Width="40" PropertiesSpinEdit-MinValue="0">
                                <PropertiesSpinEdit DisplayFormatString="g">
                                </PropertiesSpinEdit>
                                <EditFormSettings VisibleIndex="4" />
                            </dx:GridViewDataSpinEditColumn>
                            <dx:GridViewDataColumn Caption="Məqsədin tam adı (AZ)" FieldName="name_az">
                                <EditFormSettings VisibleIndex="0" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Məqsədin tam adı (EN)" FieldName="name_en">
                                <EditFormSettings VisibleIndex="1" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Məqsədin qısa adı (AZ)" FieldName="name_short_az" Width="100">
                                <EditFormSettings VisibleIndex="2" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="Məqsədin qısa adı (EN)" FieldName="name_short_en" Width="100">
                                <EditFormSettings VisibleIndex="3" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="" Visible="false" FieldName="description_az" EditFormSettings-Visible="False"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="" Visible="false" FieldName="description_en" EditFormSettings-Visible="False"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="" Visible="false" FieldName="facts_az" EditFormSettings-Visible="False"></dx:GridViewDataColumn>
                            <dx:GridViewDataColumn Caption="" Visible="false" FieldName="facts_en" EditFormSettings-Visible="False"></dx:GridViewDataColumn>
                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" HeaderStyle-CssClass="treelist-new-column" Width="100">
                                <EditButton Visible="true" />
                                <NewButton Visible="false" />
                                <DeleteButton Visible="false" />
                                <HeaderStyle CssClass="treelist-new-column" />
                            </dx:GridViewCommandColumn>
                        </Columns>
                        <Templates>
                            <EditForm>
                                <dx:ASPxPageControl ID="tabs" runat="server" ActiveTabIndex="0" Width="100%"
                                    EnableTabScrolling="true" TabAlign="Justify">
                                    <TabPages>
                                        <dx:TabPage Text="Ümumi məlumat">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">
                                                    <dx:ASPxGridViewTemplateReplacement runat="server" ReplacementType="EditFormEditors" />
                                                </dx:ContentControl>
                                            </ContentCollection>
                                        </dx:TabPage>
                                        <dx:TabPage Text="Məqsəd haqqında məlumat (AZ)">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">
                                                    <dx:ASPxHtmlEditor ID="Description_az" AutoResizeWithContainer="True" Height="200px"
                                                        Html='<%#Eval("description_az") %>'
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
                                        <dx:TabPage Text="Məqsəd haqqında məlumat (EN)">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">
                                                    <dx:ASPxHtmlEditor ID="Description_en" AutoResizeWithContainer="True" Height="200px"
                                                        Html='<%#Eval("description_en") %>'
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
                                        <dx:TabPage Text="Faktlar və rəqəmlər (AZ)">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">

                                                    <dx:ASPxHtmlEditor ID="Facts_az" AutoResizeWithContainer="True" Height="200px"
                                                        Html='<%# Eval("facts_az") %>'
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
                                        <dx:TabPage Text="Faktlar və rəqəmlər (AZ)">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">

                                                    <dx:ASPxHtmlEditor ID="Facts_en" AutoResizeWithContainer="True" Height="200px"
                                                        Html='<%# Eval("facts_en") %>'
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
                                         <dx:TabPage Text="Məlumat priotet səhifəsi üçün (AZ)">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">

                                                    <dx:ASPxHtmlEditor ID="priority_desc_az" AutoResizeWithContainer="True" Height="200px"
                                                        Html='<%# Eval("priority_desc_az") %>'
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
                                         <dx:TabPage Text="Məlumat priotet səhifəsi üçün (EN)">
                                            <ContentCollection>
                                                <dx:ContentControl runat="server">

                                                    <dx:ASPxHtmlEditor ID="priority_desc_en" AutoResizeWithContainer="True" Height="200px"
                                                        Html='<%# Eval("priority_desc_en") %>'
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
                        <Styles Header-CssClass="grid-header">
                            <Header CssClass="grid-header">
                            </Header>
                            <Cell Wrap="True">
                            </Cell>
                        </Styles>

                        <SettingsText CommandNew="Yeni" CommandDelete="Sil" CommandCancel="Ləğv et"
                            CommandEdit="Yenilə" CommandUpdate="Yadda saxla"
                            CustomizationWindowCaption="Sütun seçin" ConfirmDelete="Silmək istəditinizə əminsiniz?" />

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






