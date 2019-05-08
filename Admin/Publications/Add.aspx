<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Add.aspx.cs" Inherits="Admin_Publications_Add" %>


<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxSpellChecker.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxSpellChecker" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="content" runat="Server">
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
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
                            <asp:Literal ID="Literal1" runat="server" Text="Kateqoriya"></asp:Literal>
                        </label>
                        <div class="col-md-4">
                            <dx:ASPxComboBox ID="Category" ClientInstanceName="Category"
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
                            <asp:Literal ID="Literal10" runat="server" Text="Başlıq  (AZ)"></asp:Literal>
                        </label>
                        <div class="col-md-4">
                            <dx:ASPxHtmlEditor ID="title_az" runat="server" SettingsImageUpload-UploadImageFolder="~/uploads/pages">
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
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">
                            <asp:Literal ID="Literal11" runat="server" Text="Başlıq  (EN)"></asp:Literal>
                        </label>
                        <div class="col-md-4">
                            <dx:ASPxHtmlEditor ID="title_en" runat="server" SettingsImageUpload-UploadImageFolder="~/uploads/pages">
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
                            </dx:ASPxHtmlEditor>
                        </div>
                    </div>


                    <div class="form-group">
                        <label class="col-md-3 control-label">
                            <asp:Literal ID="Literal12" runat="server" Text="Mətn  (AZ)"></asp:Literal>
                        </label>
                        <div class="col-md-4">
                            <dx:ASPxHtmlEditor ID="Content_az" runat="server" SettingsImageUpload-UploadImageFolder="~/uploads/pages">
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
                            </dx:ASPxHtmlEditor>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">
                            <asp:Literal ID="Literal13" runat="server" Text="Mətn  (EN)"></asp:Literal>
                        </label>
                        <div class="col-md-4">
                            <dx:ASPxHtmlEditor ID="Content_en" runat="server" SettingsImageUpload-UploadImageFolder="~/uploads/pages">
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
                            </dx:ASPxHtmlEditor>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">
                            <asp:Literal ID="Literal14" runat="server" Text="Tarix"></asp:Literal>
                        </label>
                        <div class="col-md-4">
                            <dx:ASPxDateEdit ID="page_dt" runat="server" DisplayFormatString="dd.MM.yyyy" EditFormat="Custom" EditFormatString="dd.MM.yyyy"
                                Width="100%" Height="30px">
                            </dx:ASPxDateEdit>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label">
                            <asp:Literal ID="Literal3" runat="server" Text="Şəkil"></asp:Literal>
                        </label>
                        <div class="col-md-4">
                            <asp:FileUpload runat="server" ID="fuImage" />
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label">
                            <asp:Literal ID="Literal2" runat="server" Text="Tam material"></asp:Literal>
                        </label>
                        <div class="col-md-4">
                            <asp:FileUpload runat="server" ID="fuTamMaterial" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">
                            <asp:Literal ID="Literal4" runat="server" Text="Qısa material"></asp:Literal>
                        </label>
                        <div class="col-md-4">
                            <asp:FileUpload runat="server" ID="fuShortMaterial" />
                        </div>
                    </div>

                    <asp:HiddenField ID="hf_tam_material" runat="server" />
                    <asp:HiddenField ID="hf_short_material" runat="server" />
                    <asp:HiddenField ID="hf_image_filename" runat="server" />

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

    <%--     </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>






