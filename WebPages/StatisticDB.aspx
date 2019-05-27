<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StatisticDB.aspx.cs" Inherits="WebPages_StatisticDB" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList.Export" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        /*.dxWeb_edtCheckBoxUnchecked_MetropolisBlue,
        .dxWeb_edtCheckBoxChecked_MetropolisBlue {
            background-color: unset;
            margin-top: -2px;
        }

        */
          .btn-select {
            margin-top: 10px;
            border-radius: 5px;
            padding: 5px 10px;
            background-color: green;
            display: inline-block;
            color: #fff;
            font-size: 10px;
        }

            .btn-select:hover {
                color: #fff;
            }
        .dxtlSelectionCell_MetropolisBlue {
            padding-left: 1px;
        }

        .year-list label {
            display: inline;
            font-weight: normal;
            margin-right: 10px;
            margin-left: 5px;
        }

        .info-column {
            display: inline-block;
            vertical-align: bottom;
        }

        .info-column-3 {
            width: 40%;
        }

        .info-column-9 {
            width: 59%;
        }

        .grid-row:hover {
            background-color: #fafafa;
        }

        .dropbtn {
            background-color: #fff;
            border: 1px solid #d0d0d0;
            color: #7a7a7a;
            border-radius: 5px;
            font-size: 16px;
            height: 30px;
            width: 100%;
        }

        .dropdown {
            position: relative;
            /*display: inline-block;*/
        }

        .dropdown-content {
            display: none;
            position: absolute;
            background-color: #f1f1f1;
            min-width: 125px;
            box-shadow: 0px 8px 16px 0px rgba(0,0,0,0.2);
            z-index: 1;
        }

            .dropdown-content a {
                color: black;
                padding: 12px 16px;
                text-decoration: none;
                display: block;
            }

                .dropdown-content a:hover {
                    background-color: #ddd;
                }

        .dropdown:hover .dropdown-content {
            display: block;
        }

        .dropdown:hover .dropbtn {
            background-color: #fefdfd;
        }

        .txtSearch {
            padding-left: 5px;
            padding-right: 5px;
            outline: 0;
            width: 100%;
            border-radius: 5px;
            border: 0;
            height: 30px;
            background-color: #ededed;
        }

        .btn-select-all {
            margin-bottom: 10px;
            padding: 1px;
            display: block;
            text-align: left;
            padding-left: 44%;
            background-color: green;
        }

            .btn-select-all:hover {
                background-color: #009400;
            }

        .btn-unselect-all {
            padding: 1px;
            display: block;
            text-align: left;
            padding-left: 44%;
            background-color: #a92a2a;
        }

            .btn-unselect-all:hover {
                background-color: #d03a3a;
            }
    </style>

</asp:Content>
<asp:Content ID="fdf" ContentPlaceHolderID="script" runat="server">
    <script>
        function grid_cell() {
            setTimeout(function () {
                $('.grid-cell').css('border-bottom-width', '');
            }, 500);
            setTimeout(function () {
                $('.grid-cell').css('border-bottom-width', '');
            }, 500);
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <asp:ScriptManager runat="server" />
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <section>
                <div class="container main-container">
                    <div class="breadcrumb">
                        <asp:Literal ID="ltrBreadCrumb" Text="" runat="server" />
                    </div>
                    <div class="borderAll paddingLR10 paddingTB10">
                        <div class="content-title" style="font-size: 24px;">
                            <asp:Literal ID="ltrTitle" Text="xx" runat="server" />
                        </div>

                        <div class="row1">
                            <div class=" info-column info-column-3">
                                <h4>
                                    <asp:Literal ID="lblGoalTitle" Text="" runat="server" />
                                </h4>
                                <div class="row">
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="ddlGoals"
                                            DataValueField="id" DataTextField="name"
                                            CssClass="form-control" runat="server">
                                        </asp:DropDownList>

                                    </div>
                                    <div class="col-md-3">
                                        <asp:Button ID="BtnSearch" Text="Axtar"
                                            CssClass="btn btn-success"
                                            Width="100%"
                                            OnClick="BtnSearch_Click" runat="server" />
                                    </div>

                                </div>

                            </div>
                            <div class=" info-column info-column-9" style="padding-left: 15px;">
                                <%-- <h4>
                                    &nbsp;
                                </h4>--%>
                                <br />
                                <div class="borderAll" style="padding: 5px">
                                    <asp:Label ID="lblDesc" Font-Size="12px" Text="" runat="server" />
                                </div>
                            </div>
                        </div>


                        <asp:Panel ID="pnlIndicator" runat="server">
                            <div class="row">
                                <div class="col-md-12">
                                    <br />
                                    <div style="font-size: 18px;">
                                        <asp:Literal ID="lblIndicatorTitle" Text="xx" runat="server" />
                                    </div>
                                    <dx:ASPxTreeList ID="treeList1" runat="server"
                                        AutoGenerateColumns="false" Width="100%" Styles-Cell-Wrap="True"
                                        KeyFieldName="id" ParentFieldName="parent_id" Theme="MetropolisBlue">
                                        <Settings VerticalScrollBarMode="Auto" ScrollableHeight="300" ShowColumnHeaders="false" GridLines="Both" />
                                        <SettingsBehavior ExpandCollapseAction="NodeDblClick" />
                                        <SettingsEditing Mode="EditFormAndDisplayNode" AllowNodeDragDrop="false" ConfirmDelete="true" />
                                        <SettingsPopupEditForm Width="500" />
                                        <SettingsSelection AllowSelectAll="false" Enabled="true" />
                                        <Columns>
                                            <dx:TreeListComboBoxColumn Caption=" " FieldName="name_az">
                                                <DataCellTemplate>
                                                    <%#GetDataTemplate(Eval("parent_id").ToParseInt(),Eval("code").ToParseStr(),Eval("name_"+Config.getLang(Page)).ToParseStr(),Eval("id").ToParseInt(),Eval("descr").ToParseStr()) %>
                                                </DataCellTemplate>
                                            </dx:TreeListComboBoxColumn>
                                        </Columns>
                                        <Styles Header-CssClass="grid-header">
                                            <TreeLineRoot BackColor="#F3F3F3"></TreeLineRoot>
                                        </Styles>

                                        <SettingsText CommandNew="Yeni" CommandDelete="Sil" CommandCancel="Ləğv et" CommandEdit="Yenilə" CommandUpdate="Yadda saxla" LoadingPanelText="Yüklənir&hellip;" CustomizationWindowCaption="Sütun seçin" ConfirmDelete="Silmək istəditinizə əminsiniz?" />

                                    </dx:ASPxTreeList>
                                    <div>


                                        <div class="btn btn-success btn-select-all">
                                            <asp:LinkButton ID="btnSelectAll"
                                                OnClick="btnSelectAll_Click"
                                                runat="server"
                                                ForeColor="White"
                                                Style="display: block; padding: 1px; font-size: 14px;">
                                            </asp:LinkButton>
                                        </div>
                                        <div class="btn btn-danger btn-unselect-all">

                                            <asp:LinkButton ID="btnUnselectAll"
                                                Style="display: block; font-size: 14px;"
                                                OnClick="btnUnselectAll_Click"
                                                ForeColor="White"
                                                runat="server">
                                            </asp:LinkButton>

                                        </div>
                                    </div>
                                </div>


                            </div>
                            <div class="row">

                                <div class="col-md-4">
                                    <br />
                                    <h4>
                                        <asp:Literal ID="lblYearTitle" Text="" runat="server" />
                                    </h4>
                                    <asp:CheckBoxList runat="server" ID="chkYears"
                                        RepeatDirection="Horizontal"
                                        CssClass="year-list form-control"
                                        DataTextField="year" DataValueField="year">
                                    </asp:CheckBoxList>
                                    <asp:LinkButton ID="lnkselectallchk" Text="" OnClick="lnkchkSelectAll_Click" CssClass="btn-select" runat="server" Style="width: 49%">
                                                    <i class="fa fa-check-circle-o" aria-hidden="true"></i>
                                                    <%=DALC.GetStaticValue("indicator_select_all") %>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lnkunselectallchk" Text="" OnClick="lnkchkUnselectAll_Click" CssClass="btn-select" runat="server" Style="width: 49%; background-color: #a92a2a;">
                                                    <i class="fa fa-times-circle-o" aria-hidden="true"></i>
                                                    <%=DALC.GetStaticValue("indicator_unselect_all") %>
                                    </asp:LinkButton>
                                </div>
                                   <div class="col-md-8">

                                   </div>
                            </div>
                            <div class="row">
                                <div class="col-md-8">
                                    <asp:Label ID="lblError" Style="color: red" Text="" runat="server" />
                                </div>
                                <div class="col-md-4" style="text-align: right">
                                    <br />
                                    <div id="loading" style="display: none">
                                        <img src="/images/loadingbig.gif" alt="loading" />
                                    </div>
                                    <asp:Button ID="btnHesabat" CssClass="btn btn-success" OnClick="btn_Click" Text=""
                                        OnClientClick="this.style.display='none';document.getElementById('loading').style.display='block';"
                                        runat="server" />
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="pnlResult" runat="server">
                            <div class="row">
                                <div class="col-md-9">
                                    <div class="myclass border-right" style="display: inline-block; vertical-align: middle">
                                        <asp:Image ID="imgGoal" runat="server" Width="96" />
                                    </div>
                                    <div class="myclass" style="width: 80%; display: inline-block; vertical-align: middle; font-size: 16px; font-weight: bold;">
                                        <span>
                                            <asp:Literal ID="lblGoalName" Text="text" runat="server" />
                                        </span>
                                    </div>
                                </div>

                                <div class="col-md-3" style="padding-top: 25px;">
                                    <div class="row">
                                        <div class="col-md-6" style="padding: 0 5px 0 5px;">
                                            <asp:TextBox ID="txtSearch"
                                                runat="server"
                                                CssClass="txtSearch"
                                                AutoPostBack="true"
                                                OnTextChanged="txtSearch_TextChanged" />
                                        </div>
                                        <div class="col-md-6" style="padding-left: 0;">

                                            <div class="dropdown">
                                                <button class="dropbtn"><i class='fa fa-download'></i></button>
                                                <div class="dropdown-content">
                                                    <asp:LinkButton ID="LnkExportCsv" Text="CSV" CommandArgument="csv" runat="server" OnClick="LnkExport_Click" />
                                                    <asp:LinkButton ID="LnkExportPdf" Text="PDF" CommandArgument="pdf" runat="server" OnClick="LnkExport_Click" />
                                                    <asp:LinkButton ID="LnkExportExc" Text="MS-Excel" CommandArgument="exc" runat="server" OnClick="LnkExport_Click" />

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-md-12">

                                    <dx:ASPxGridViewExporter ID="gridExporter" runat="server"
                                        Landscape="true" PaperKind="A4">
                                    </dx:ASPxGridViewExporter>
                                    <dx:ASPxGridView ID="Grid" runat="server"
                                        AutoGenerateColumns="False"
                                        Width="100%"
                                        SettingsBehavior-ConfirmDelete="true"
                                        KeyFieldName="id">
                                        <ClientSideEvents BeginCallback="grid_cell" />
                                        <Settings GridLines="Both" />
                                        <SettingsEditing Mode="PopupEditForm"></SettingsEditing>
                                        <Columns>

                                            <dx:GridViewDataColumn Caption="No" FieldName="id" VisibleIndex="1" Width="50">
                                                <EditFormSettings Visible="False" />
                                            </dx:GridViewDataColumn>
                                            <dx:GridViewDataColumn Caption="Kodu" FieldName="code" VisibleIndex="3" Width="100">
                                                <EditFormSettings ColumnSpan="2" />
                                            </dx:GridViewDataColumn>

                                        </Columns>
                                        <Settings
                                            HorizontalScrollBarMode="Auto"
                                            GridLines="Both" />
                                        <SettingsPager>
                                            <PageSizeItemSettings Visible="true"></PageSizeItemSettings>
                                        </SettingsPager>
                                        <SettingsText CommandNew="Yeni"
                                            CommandDelete="Sil"
                                            CommandCancel="Ləğv et" CommandEdit="Yenilə" CommandUpdate="Yadda saxla"
                                            CustomizationWindowCaption="Sütun seçin" ConfirmDelete="Silmək istəditinizə əminsiniz?" />
                                        <SettingsDataSecurity AllowDelete="true" />
                                        <Styles Header-CssClass="grid-header">
                                            <Row CssClass="grid-row"></Row>
                                            <Cell CssClass="grid-cell"></Cell>
                                        </Styles>

                                    </dx:ASPxGridView>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:Label ID="lblNote" Text="" Font-Bold="true" runat="server" />
                                    <ul>
                                        <asp:Literal ID="footnote" Text="" runat="server" />
                                    </ul>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                </div>
            </section>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="LnkExportCsv" />
            <asp:PostBackTrigger ControlID="LnkExportPdf" />
            <asp:PostBackTrigger ControlID="LnkExportExc" />

        </Triggers>
    </asp:UpdatePanel>
</asp:Content>



