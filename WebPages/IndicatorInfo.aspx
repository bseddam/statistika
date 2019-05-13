<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="IndicatorInfo.aspx.cs" Inherits="WebPages_IndicatorInfo" %>

<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <style>
        .visible-none {
            visibility: collapse;
        }

        #content_treeList1_HDR th:first-child {
            visibility: hidden;
        }

        .info-box {
            border: 1px solid #eaeaea;
            padding: 10px;
            margin-bottom: 10px;
        }
        /*.chart-down {
            font-size: 10px;
        }*/

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

        .google-visualization-tooltip div {
            padding: 5px;
        }

        .export-item {
            padding: 5px;
            color: #000;
            text-decoration: none;
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
        $(document).on('click', '.print-diaqram', function () {
            var prtContent = document.getElementById("chart_div");

            var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=400,toolbar=0,scrollbars=0,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            return false;
        });
        $(document).on('click', '.metadata-list tr.parent', function () {
            var _id = $(this).attr('data-id');
            $('.metadata-list tr[data-id="' + _id + '"]').removeClass('visible-none');
        });

        $(document).on('click', '.indicator-nav li', function () {
            var _id = $(this).attr('data-content');
            var prent = $(this).parents('.indicator-nav-wrapper');
            prent.find('.indicator-nav li').removeClass('active');
            $(this).addClass('active');
            //class="indicator-tab"
            prent.find('.indicator-tab').hide();
            $(_id).show();
            if (_id == '#datatable') {
                $('.indicator-size').hide();
            } else {
                $('.indicator-size').show();
            }

        });
        $(document).on('click', '.chart-btn', function () {
            _type = $(this).attr('data-type');
            drawChart(_type);
            return false;
        });
                    //$(document).on('click', '.chart-down', function () {
                    //    var url = img.src.replace(/^data:image\/[^;]+/, 'data:application/octet-stream');
                    //    window.open(url);
                    //});

                </script>

    <style>
        #chkIndicators label {
            display: unset;
        }
    </style>
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
                    <div class="borderAll paddingLR10 paddingTB10">
                        <div class="row">
                            <div class="col-md-12 ">
                                <div>
                                    <div class="content-goal-title" style="position: relative">
                                        <div class="myclass border-right" style="display: inline-block; vertical-align: middle">
                                            <asp:Image ID="imgGoal" runat="server" Width="128" />
                                        </div>
                                        <div class="myclass titlem">
                                            <span style="margin-bottom: 10px; display: inline-block; position: absolute; top: 5px;">
                                                <asp:Label ID="lblGoalName" Text="text" runat="server" />
                                            </span>
                                            <br />
                                            <asp:Label ID="lblIndicatorTitle" Font-Bold="false" Text="text"
                                                runat="server" />
                                        </div>
                                    </div>
                                    <br />
                                    <p>
                                        <asp:Literal Text="" ID="LtrIndicatorInfo" runat="server" />
                                    </p>
                                    <asp:Panel runat="server" ID="pnlInfo">
                                        <div>
                                            <asp:Label ID="lblSourceLabel1" Text="" runat="server" />
                                            <asp:Label ID="lblSource1" Text="" runat="server" />
                                        </div>
                                        <div>
                                            <asp:Label ID="lblNoteLabel1" Text="" runat="server" />
                                            <asp:Label ID="lblNote1" Text="" runat="server" />
                                        </div>
                                        <div class="share-box" style="margin-top: 10px">
                                            <asp:Literal ID="share_text" Text="" runat="server" />
                                            <div class="social-icons">
                                                <ul>
                                                    <li>
                                                        <a href="javascript:void(0)" class="print-page">
                                                            <i class="fa fa-print" aria-hidden="true"></i>
                                                        </a>
                                                    </li>
                                                    <li>

                                                        <asp:HyperLink ID="shareLinkedin" runat="server" Target="_blank">
                                                                        <i class="fa fa-linkedin" aria-hidden="true"></i>
                                                                    </asp:HyperLink>

                                                    </li>
                                                    <li>

                                                        <asp:HyperLink ID="shareFb" runat="server" Target="_blank">
                                                                        <i class="fa fa-facebook" aria-hidden="true"></i>
                                                                    </asp:HyperLink>

                                                    </li>
                                                    <li>
                                                        <asp:HyperLink ID="shareTwt" runat="server" Target="_blank">
                                                                        <i class="fa fa-twitter" aria-hidden="true"></i>
                                                                    </asp:HyperLink>
                                                    </li>
                                                    <li>
                                                        <asp:HyperLink ID="shareMail" runat="server">
                                                                        <i class="fa fa-envelope-o" aria-hidden="true"></i>
                                                                    </asp:HyperLink>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                </div>
                            </div>
                        </div>
                        <hr />
                        <asp:Panel runat="server" ID="pnlDiaqramTable">
                            <div class="row">
                                <asp:Panel runat="server" ID="pnlSubIndicator" class="col-md-4" Style="margin-bottom: 10px">
                                    <asp:Label ID="lblSelectIndicator" Text="" runat="server" />
                                    <asp:Panel runat="server" ID="pnlRegion" class="form-control" Height="330px" Style="overflow-y: auto;">

                                        <%-- <asp:CheckBoxList ID="chkIndicators"
                                        AutoPostBack="true" ClientIDMode="Static"
                                        OnSelectedIndexChanged="treeList1_SelectionChanged"
                                        runat="server">
                                    </asp:CheckBoxList>--%>
                                        <asp:Button Text="text" ID="btnIndicator" CssClass="btnIndicator" runat="server" OnClick="btnIndicator_Click" Style="display: none;" />
                                        <dx:ASPxTreeList ID="treeList1" runat="server" AutoGenerateColumns="false" Width="100%" Styles-Cell-Wrap="True" KeyFieldName="id" ParentFieldName="parent_id" Theme="MetropolisBlue">

                                            <ClientSideEvents SelectionChanged="function(s,e){$('.btnIndicator').click();}" />
                                            <Settings VerticalScrollBarMode="Auto" ScrollableHeight="300" ShowColumnHeaders="true" GridLines="Both" />
                                            <SettingsBehavior ExpandCollapseAction="NodeDblClick" />
                                            <SettingsEditing Mode="EditFormAndDisplayNode" AllowNodeDragDrop="false" ConfirmDelete="true" />
                                            <SettingsPopupEditForm Width="500" />
                                            <SettingsSelection AllowSelectAll="false" Enabled="true" />
                                            <Columns>
                                                <dx:TreeListComboBoxColumn Caption=" " FieldName="name_az">
                                                    <DataCellTemplate>
                                                        <%#getIndicatorName(Eval("code").ToParseStr(),Eval("name_"+Config.getLang(Page)).ToParseStr(),Eval("parent_id").ToParseInt()) %>
                                                    </DataCellTemplate>
                                                </dx:TreeListComboBoxColumn>
                                            </Columns>
                                            <Styles Header-CssClass="grid-header">
                                                <TreeLineRoot BackColor="#F3F3F3"></TreeLineRoot>
                                            </Styles>

                                            <SettingsText CommandNew="Yeni" CommandDelete="Sil" CommandCancel="Ləğv et" CommandEdit="Yenilə" CommandUpdate="Yadda saxla" LoadingPanelText="Yüklənir&hellip;" CustomizationWindowCaption="Sütun seçin" ConfirmDelete="Silmək istəditinizə əminsiniz?" />

                                        </dx:ASPxTreeList>
                                    </asp:Panel>

                                    <asp:LinkButton ID="lnkSelectAll" Text="" OnClick="lnkSelectAll_Click" CssClass="btn-select" runat="server" Style="width: 49%">
                                                    <i class="fa fa-check-circle-o" aria-hidden="true"></i>
                                                    <%=DALC.GetStaticValue("indicator_select_all") %>
                                                </asp:LinkButton>
                                    <asp:LinkButton ID="lnkUnselectAll" Text="" OnClick="lnkUnselectAll_Click" CssClass="btn-select" runat="server" Style="width: 49%; background-color: #a92a2a;">
                                                    <i class="fa fa-times-circle-o" aria-hidden="true"></i>
                                                    <%=DALC.GetStaticValue("indicator_unselect_all") %>
                                                </asp:LinkButton>

                                </asp:Panel>
                                <asp:Panel runat="server" ID="pnlContent" class="col-md-8">
                                    <div class="indicator-nav-wrapper">
                                        <ul class="indicator-nav">
                                            <li class="active" data-content="#chart">
                                                <asp:Label ID="lblTabChart" Text="" runat="server" />
                                            </li>
                                            <li data-content="#datatable">
                                                <asp:Label ID="lblTabTable" Text="" runat="server" />
                                            </li>
                                        </ul>
                                        <div id="chart" class="indicator-tab">
                                            <div class="chart-styles clear">
                                                <a href="#" class="chart-btn" data-type="1">
                                                    <img src="/images/chart_1.png" alt="" />
                                                </a>
                                                <a href="#" class="chart-btn" data-type="3">
                                                    <img src="/images/chart_2.png" alt="" />
                                                </a>
                                                <a href="#" class="chart-btn" data-type="2">
                                                    <img src="/images/chart_3.png" alt="" />
                                                </a>

                                            </div>
                                            <div class="chart-container">
                                                <div id="chart_div"></div>
                                                <div class="chart-menu">
                                                    <div class="chart-menu-container">
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

                                            <asp:Literal ID="chart_script" Text="" runat="server" />
                                        </div>
                                        <div id="datatable" class="indicator-tab indicator-datatable" style="display: none">
                                            <div class="indicator-datatable-container">
                                                <asp:Literal ID="ltrTable" Text="" runat="server" />
                                            </div>
                                        </div>
                                        <div>
                                            <br />
                                            <div class="indicator-size">
                                                <asp:Label ID="lblSizeLabel" Text="" runat="server" />
                                                <asp:Label ID="lblSize" Text="" runat="server" />
                                            </div>
                                            <div>
                                                <asp:Label ID="lblSourceLabel" Text="" runat="server" />
                                                <asp:Label ID="lblSource" Text="" runat="server" />
                                            </div>
                                            <div>
                                                <asp:Label ID="lblNoteLabel" Text="" runat="server" />
                                                <asp:Label ID="lblNote" Text="" runat="server" />
                                            </div>

                                        </div>
                                    </div>


                                    <br />



                                </asp:Panel>
                            </div>
                        </asp:Panel>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="alert alert-warning" role="alert">
                                    <i class="fa fa-info"></i>
                                    <asp:Label ID="lblMetaInfo" Text="" runat="server" />

                                </div>

                                <div class="indicator-nav-wrapper">
                                    <ul class="indicator-nav">
                                        <li class="active" data-content="#national-meta">
                                            <asp:Label ID="lblNationalMetadata" Text="" runat="server" />
                                        </li>
                                        <li data-content="#global-meta">
                                            <asp:Label ID="lblGlobalMetadata" Text="" runat="server" Visible="false" />
                                        </li>
                                    </ul>
                                    <div id="national-meta" class="indicator-tab">
                                        <div class="info-box">
                                            <asp:Label ID="indicator_national_metadata_info" Text="" runat="server" />
                                        </div>

                                        <div style="text-align: right;">
                                            <asp:LinkButton ID="btnExport" CssClass="export-item" OnClick="LnkExport_Click" CommandArgument="pdf" Text="" runat="server">
                                                            <i class="fa fa-file-o"></i> PDF
                                                        </asp:LinkButton>
                                            <asp:LinkButton ID="btnExport2" CssClass="export-item" OnClick="LnkExport_Click" CommandArgument="doc" Text="" runat="server">
                                                            <i class="fa fa-file-o"></i> Doc
                                                        </asp:LinkButton>
                                        </div>
                                        <asp:Panel runat="server" ID="PnlExport">
                                            <table class="table table-bordered" style="width: 100%">
                                                <tbody>
                                                    <tr>
                                                        <td colspan="3">
                                                            <asp:Label ID="lblIndicatorName" Text="" runat="server" />
                                                            <asp:Label ID="lblIndicatorName_1" Text="" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <asp:Repeater ID="rptMetaData" runat="server">
                                                        <ItemTemplate>
                                                            <tr data-id="<%#Eval(" list_id ") %>" class="parent" style="background-color: #DCEAF3; font-weight: bold;">
                                                                <td style="width: 50px">
                                                                    <%=++_noM%>
                                                                    <span style="display: none"><%=_noM_Sub = 1%></span>
                                                                </td>
                                                                <td style="width: 300px">
                                                                    <%#Eval("l_name")%>
                                                                            </td>
                                                                <td>
                                                                    <%#Eval("name_"+Config.getLang(Page)) %>
                                                                            </td>
                                                            </tr>
                                                            <asp:Repeater ID="rptMetadataSub" runat="server">
                                                                <ItemTemplate>
                                                                    <tr data-id="<%#Eval(" sub_id ") %>">
                                                                        <td style="width: 50px">
                                                                            <%=string.Format("{0}.{1}", _noM,_noM_Sub++)%>
                                                                                    </td>
                                                                        <td style="width: 300px">
                                                                            <%#Eval("l_name") %>
                                                                                    </td>
                                                                        <td>
                                                                            <%#Eval("name_"+Config.getLang(Page)) %>
                                                                                    </td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </tbody>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                    <div id="global-meta" class="indicator-tab" style="display: none">
                                        <div class="info-box">
                                            <asp:Label ID="indicator_global_metadata_info" Text="" runat="server" />
                                        </div>
                                        <table class="table table-bordered" style="width: 100%">
                                            <tbody>

                                                <asp:Repeater ID="rptGlobalMetada" runat="server">
                                                    <ItemTemplate>
                                                        <tr data-id="<%#Eval(" list_id ") %>" class="parent">
                                                            <td style="font-weight: bold; width: 175px;">
                                                                <%#Eval("l_name")%>
                                                                        </td>
                                                            <td>
                                                                <%#Eval("name_"+Config.getLang(Page)) %>
                                                                        </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </section>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExport" />
            <asp:PostBackTrigger ControlID="btnExport2" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
