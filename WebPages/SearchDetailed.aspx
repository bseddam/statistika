<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchDetailed.aspx.cs" Inherits="WebPages_SearchDetailed" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxHtmlEditor.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHtmlEditor" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList.Export" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/css/select2.min.css" rel="stylesheet" />
    <style>
        .tab-content {
            padding-top: 20px;
            display: none;
        }

            .tab-content.active {
                display: block;
            }


        option {
            font-size: 12px;
        }

        .radio-list label {
            padding: 5px;
        }


        .dxtc-activeTab .dxtc-link {
            color: #fff !important;
            background: #2b66ff;
            background: -webkit-linear-gradient(180deg, #2b66ff, #6f2aff);
            background: -o-linear-gradient(180deg, #2b66ff, #6f2aff);
            background: -moz-linear-gradient(180deg, #2b66ff, #6f2aff);
            background: linear-gradient( 180deg, #2b66ff, #6f2aff);
            border: 1px solid #fff !important;
        }

        .dxtc-tab:hover .dxtc-link {
            background: transparent !important;
            border: 1px solid #f0f0f0 !important;
            color: #000 !important;
        }

        .dxtcLite_Moderno > .dxtc-stripContainer .dxtc-tab, .dxtcLite_Moderno > .dxtc-stripContainer .dxtc-activeTab {
            border-width: 1px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="Server">
    <script src="/js/select2.full.min.js"></script>
    <script>

        $(document).on('click', '.nav-tabs li', function () {
            dataContent = $(this).attr('data-content');

            $('.nav-tabs li').removeClass('active');
            $(this).addClass('active');

            $('.tab-content').hide();
            $(dataContent).show();
        });

        $(function () {
            hash = window.location.hash;
            $('.nav-tabs li a[href="' + hash + '"]').parent().click();
            $('.dxtc-rightIndent,.dxtc-leftIndent,.dxtc-spacer').hide();
            setTimeout(function () {
                $('.dxtc-rightIndent,.dxtc-leftIndent,.dxtc-spacer').hide();
            }, 50)

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <section>
                <div class="container main-container">
                    <div class="breadcrumb not-printable">
                        <asp:Literal ID="ltrBreadCrumb" Text="" runat="server" />
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="borderAll paddingLR10">
                                <h2 class="content-title " style="padding-bottom: 7px;">
                                    <asp:Literal ID="ltrTitle" Text="" runat="server" />
                                </h2>
                                <div class="">
                                    <dx:ASPxPageControl ID="tabs" runat="server"
                                        ActiveTabIndex="0" Width="100%"
                                        TabStyle-HoverStyle-BackColor="White"
                                        TabAlign="Justify">
                                        <ClientSideEvents ActiveTabChanged="function(s,e){ $('.select2').select2(); }" />
                                        <TabStyle Border-BorderWidth="0"></TabStyle>
                                        <ContentStyle Border-BorderWidth="0"></ContentStyle>

                                        <TabPages>
                                            <dx:TabPage Text="Ümumi məlumat">
                                                <ContentCollection>
                                                    <dx:ContentControl runat="server">
                                                        <asp:Panel runat="server" CssClass="form-horizontal" DefaultButton="BtnSearchGoal">
                                                            <div class="row">
                                                                <div class="col-md-6">
                                                                    <div class="form-group">
                                                                        <label class=" col-sm-12">
                                                                            <asp:Literal ID="lblGoalCode" Text="" runat="server" />
                                                                        </label>
                                                                        <div class="col-sm-12">
                                                                            <asp:TextBox ID="txtGoalCode" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="form-group">
                                                                        <label class=" col-sm-12">
                                                                            <asp:Literal ID="lblGoalName" Text="" runat="server" />
                                                                        </label>
                                                                        <div class="col-sm-12">
                                                                            <asp:TextBox ID="txtGoalName" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-6 pull-right">
                                                                    <div class="form-group">
                                                                        <div class="col-sm-12" style="text-align: right">
                                                                            <asp:Label ID="lblGoalError" ForeColor="Red" Text="" runat="server" />
                                                                            <asp:Button ID="BtnSearchGoal"
                                                                                OnClick="BtnSearchGoal_Click"
                                                                                Text="" CssClass="btn btn-success" runat="server"
                                                                                OnClientClick="this.style.display='none';document.getElementById('loadingGoal').style.display='inline-block';" />
                                                                            <img id="loadingGoal" src="/images/loading.gif" style="width: 34px; display: none" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>

                                                        <asp:Panel ID="pnlGoalResult" runat="server">
                                                            <dx:ASPxGridView ID="GridGoal" runat="server"
                                                                AutoGenerateColumns="False" Width="100%"
                                                                KeyFieldName="id">
                                                                <Settings GridLines="Both" />
                                                                <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>
                                                                <Columns>
                                                                    <dx:GridViewDataColumn Caption="No" FieldName="priority" VisibleIndex="1" Width="50" Visible="false">
                                                                        <DataItemTemplate>
                                                                            <%#goal_row++ %>
                                                                        </DataItemTemplate>
                                                                    </dx:GridViewDataColumn>
                                                                    <dx:GridViewDataMemoColumn Caption="Kod" FieldName="id" VisibleIndex="3">
                                                                    </dx:GridViewDataMemoColumn>
                                                                    <dx:GridViewDataMemoColumn Caption="Başlıq" FieldName="name_az" VisibleIndex="3">
                                                                        <DataItemTemplate>
                                                                            <a href="<%#GetGoalURL(Eval("id").ToParseInt(),Eval("name_short_"+Config.getLang(Page)).ToParseStr()) %>" target="_blank">
                                                                                <%#Eval("name_"+Config.getLang(Page)) %>
                                                                            </a>
                                                                        </DataItemTemplate>
                                                                    </dx:GridViewDataMemoColumn>
                                                                </Columns>
                                                                <SettingsPager Mode="ShowAllRecords">
                                                                </SettingsPager>
                                                                <SettingsText CommandNew="Yeni" CommandDelete="Sil"
                                                                    CommandCancel="Ləğv et" CommandEdit="Yenilə"
                                                                    CommandUpdate="Yadda saxla" CustomizationWindowCaption="Sütun seçin"
                                                                    ConfirmDelete="Silmək istədiyinizə əminsiniz?" />
                                                                <SettingsDataSecurity AllowDelete="False" AllowInsert="False" />
                                                                <Styles Header-CssClass="grid-header">
                                                                    <Cell CssClass="grid-cell"></Cell>
                                                                </Styles>

                                                            </dx:ASPxGridView>
                                                        </asp:Panel>

                                                    </dx:ContentControl>
                                                </ContentCollection>
                                            </dx:TabPage>
                                            <dx:TabPage>
                                                <ContentCollection>
                                                    <dx:ContentControl runat="server">

                                                        <asp:Panel runat="server" DefaultButton="btnTargetSearch">

                                                            <div class="form-horizontal ">

                                                                <div class="row">

                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label class="col-sm-12">
                                                                                <asp:Literal ID="lblTargetCode" Text="" runat="server" />
                                                                            </label>
                                                                            <div class="col-sm-12">
                                                                                <asp:TextBox ID="txtTargetCode" runat="server" CssClass="form-control" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label class=" col-sm-12">
                                                                                <asp:Literal ID="lblTargetName" Text="" runat="server" />
                                                                            </label>
                                                                            <div class="col-sm-12">
                                                                                <asp:TextBox ID="txtTargetName" runat="server" CssClass="form-control" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <div class="col-sm-12 pull-right">
                                                                                <asp:RadioButtonList ID="rbPriotet" runat="server"
                                                                                    RepeatDirection="Horizontal"
                                                                                    CssClass="radio-list">
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6 ">
                                                                        <div class="form-group">
                                                                            <div class="col-sm-12" style="text-align: right">
                                                                                <asp:Label ID="lblTargetError" Text="" ForeColor="Red" runat="server" />
                                                                                <asp:Button ID="btnTargetSearch"
                                                                                    OnClick="btnTargetSearch_Click"
                                                                                    CssClass="btn btn-success" runat="server"
                                                                                    OnClientClick="this.style.display='none';document.getElementById('loadingTarget').style.display='inline-block';" />
                                                                                <img id="loadingTarget" src="/images/loading.gif" style="width: 34px; display: none" />

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </asp:Panel>

                                                        <asp:Panel ID="PnlTargetResult" runat="server">
                                                            <dx:ASPxGridView ID="GridTarget" runat="server"
                                                                AutoGenerateColumns="False" Width="100%"
                                                                KeyFieldName="id">
                                                                <Settings GridLines="Both" />
                                                                <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>
                                                                <Columns>
                                                                    <dx:GridViewDataColumn Caption="No" FieldName="priority" VisibleIndex="1" Width="50" Visible="false">
                                                                        <DataItemTemplate>
                                                                            <%#target_row++ %>
                                                                        </DataItemTemplate>
                                                                    </dx:GridViewDataColumn>
                                                                    <dx:GridViewDataMemoColumn Caption="Kod" FieldName="code" VisibleIndex="3">
                                                                    </dx:GridViewDataMemoColumn>
                                                                    <dx:GridViewDataMemoColumn Caption="Başlıq" FieldName="name_az" VisibleIndex="3">
                                                                        <DataItemTemplate>
                                                                            <a href="<%#GetTargetURL(Eval("goal_id").ToParseInt()) %>" target="_blank">
                                                                                <%#Eval("name_"+Config.getLang(Page)) %>
                                                                            </a>
                                                                        </DataItemTemplate>
                                                                    </dx:GridViewDataMemoColumn>
                                                                </Columns>
                                                                <SettingsPager Mode="ShowPager">
                                                                </SettingsPager>
                                                                <SettingsText CommandNew="Yeni" CommandDelete="Sil"
                                                                    CommandCancel="Ləğv et" CommandEdit="Yenilə"
                                                                    CommandUpdate="Yadda saxla" CustomizationWindowCaption="Sütun seçin"
                                                                    ConfirmDelete="Silmək istədiyinizə əminsiniz?" />
                                                                <SettingsDataSecurity AllowDelete="False" AllowInsert="False" />
                                                                <Styles Header-CssClass="grid-header">
                                                                    <Cell CssClass="grid-cell"></Cell>
                                                                </Styles>

                                                            </dx:ASPxGridView>
                                                        </asp:Panel>

                                                    </dx:ContentControl>
                                                </ContentCollection>
                                            </dx:TabPage>
                                            <dx:TabPage>
                                                <ContentCollection>
                                                    <dx:ContentControl runat="server">
                                                        <div>

                                                            <asp:Panel runat="server" DefaultButton="btnIndicatorSearch">

                                                                <div class="form-horizontal ">

                                                                    <div class="row">

                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class=" col-sm-12">
                                                                                    <asp:Literal ID="lblIndicatorCode" Text="" runat="server" />
                                                                                </label>
                                                                                <div class="col-sm-12">
                                                                                    <asp:TextBox ID="txtIndicatorCode" runat="server" CssClass="form-control" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class=" col-sm-12">
                                                                                    <asp:Literal ID="lblIndicatorName" Text="" runat="server" />
                                                                                </label>
                                                                                <div class="col-sm-12">
                                                                                    <asp:TextBox ID="txtIndicatorName" runat="server" CssClass="form-control" />
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class=" col-sm-12">
                                                                                    <asp:Literal ID="lblIndicatorQurum" Text="" runat="server" />
                                                                                </label>
                                                                                <div class="col-sm-12">
                                                                                    <asp:DropDownList ID="ddlIndicatorQurum"
                                                                                        runat="server"
                                                                                        CssClass="form-control select2">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class=" col-sm-12">
                                                                                    <asp:Literal ID="lblIndicatorPriotet" Text="" runat="server" />
                                                                                </label>
                                                                                <div class="col-sm-12">
                                                                                    <asp:RadioButtonList ID="rbIndicatorPriotet" runat="server"
                                                                                        RepeatDirection="Horizontal"
                                                                                        CssClass="radio-list">
                                                                                    </asp:RadioButtonList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class=" col-sm-12">
                                                                                    <asp:Literal ID="lblIndicatorStatus" Text="" runat="server" />
                                                                                </label>
                                                                                <div class="col-sm-12">
                                                                                    <asp:DropDownList ID="ddlIndicatorStatus"
                                                                                        runat="server"
                                                                                        CssClass="form-control select2">
                                                                                    </asp:DropDownList>

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6 ">
                                                                            <div class="form-group">
                                                                                <div class="col-sm-12" style="padding-top: 23px; text-align: right">
                                                                                    <asp:Label ID="lblIndicatorError" ForeColor="Red" runat="server" />
                                                                                    <asp:Button ID="btnIndicatorSearch"
                                                                                        OnClick="btnIndicatorSearch_Click"
                                                                                        CssClass="btn btn-success"
                                                                                        runat="server"
                                                                                        OnClientClick="this.style.display='none';document.getElementById('loadingIndicator').style.display='inline-block';" />
                                                                                    <img id="loadingIndicator" src="/images/loading.gif" style="width: 34px; display: none" />
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                            <asp:Panel ID="PnlIndicatorResult" runat="server">
                                                                <dx:ASPxGridView ID="GridIndicator" runat="server"
                                                                    AutoGenerateColumns="False" Width="100%"
                                                                    KeyFieldName="id">
                                                                    <Settings GridLines="Both" />
                                                                    <SettingsEditing Mode="EditFormAndDisplayRow"></SettingsEditing>
                                                                    <Columns>
                                                                        <dx:GridViewDataColumn Caption="No" FieldName="priority" VisibleIndex="1" Width="50" Visible="false">
                                                                            <DataItemTemplate>
                                                                                <%#indicator_row++ %>
                                                                            </DataItemTemplate>
                                                                        </dx:GridViewDataColumn>
                                                                        <dx:GridViewDataMemoColumn Caption="Kod" FieldName="code" VisibleIndex="3">
                                                                            <DataItemTemplate>
                                                                                <%#Config.ClearIndicatorCode(Eval("code").ToParseStr()) %>
                                                                            </DataItemTemplate>
                                                                        </dx:GridViewDataMemoColumn>
                                                                        <dx:GridViewDataMemoColumn Caption="Başlıq" FieldName="name_az" VisibleIndex="3">
                                                                            <DataItemTemplate>
                                                                                <a href="<%#GetIndicatorURL(Eval("id").ToParseInt(),Eval("name_"+Config.getLang(Page)).ToParseStr()) %>" target="_blank">
                                                                                    <%#Eval("name_"+Config.getLang(Page)) %>
                                                                                </a>
                                                                            </DataItemTemplate>
                                                                        </dx:GridViewDataMemoColumn>
                                                                    </Columns>
                                                                    <SettingsPager Mode="ShowPager">
                                                                    </SettingsPager>
                                                                    <SettingsText CommandNew="Yeni" CommandDelete="Sil"
                                                                        CommandCancel="Ləğv et" CommandEdit="Yenilə"
                                                                        CommandUpdate="Yadda saxla" CustomizationWindowCaption="Sütun seçin"
                                                                        ConfirmDelete="Silmək istədiyinizə əminsiniz?" />
                                                                    <SettingsDataSecurity AllowDelete="False" AllowInsert="False" />
                                                                    <Styles Header-CssClass="grid-header">
                                                                        <Cell CssClass="grid-cell"></Cell>
                                                                    </Styles>

                                                                </dx:ASPxGridView>
                                                            </asp:Panel>

                                                        </div>

                                                    </dx:ContentControl>
                                                </ContentCollection>
                                            </dx:TabPage>
                                        </TabPages>
                                    </dx:ASPxPageControl>

                                </div>



                            </div>

                        </div>

                    </div>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


