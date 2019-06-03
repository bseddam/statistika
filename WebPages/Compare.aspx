<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Compare.aspx.cs" Inherits="WebPages_Compare" %>


<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
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
        $(document).on('click', '.chart-btn', function () {
            _type = $(this).attr('data-type');
            drawChart(_type);
            return false;
        });
        function treeReports_GetNodeValues(rowValues) {
            //if (rowValues.length>2) {
            //    alert('Müqayisə üçün max say 2');
            //    return false;
            //}

            for (var i = 0; i < rowValues.length; i++) {
                console.log(rowValues[i]);
                if (rowValues[i] == 35) {
                    alert('<%=DALC.GetStaticValue("statistical_database_keyfiyyet")%>');
                    g_sender.SelectNode(g_sender.GetVisibleSelectedNodeKeys(), false);
                    break;
                }
            }
            console.log(rowValues);

        }
        var g_sender;
        function treeReports_NodeDblClick(sender, eventArgs) {
            var colNames = "size_code";
            g_sender = sender;
            sender.GetSelectedNodeValues(colNames, treeReports_GetNodeValues);
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
                            <div class="info-column info-column-3">
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
                            <div class="info-column info-column-9" style="padding-left: 15px;">
                                <%-- <h4>
                                    &nbsp;
                                </h4>--%>
                                <br />
                                <div class="borderAll" style="padding: 5px">
                                    <asp:Label ID="lblDesc" Font-Bold="false" Font-Size="12px" Text="" runat="server" />
                                </div>
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="pnlIndicator">

                            <div class="row">
                                <div class="col-md-12">
                                    <div style="font-size: 18px;">
                                        <asp:Literal ID="lblIndicatorTitle" Text="xx" runat="server" />
                                    </div>
                                    <dx:ASPxTreeList ID="treeList1" runat="server"
                                        ClientInstanceName="treeList"
                                        AutoGenerateColumns="false"
                                        Width="100%"
                                        Settings-ShowColumnHeaders="false"
                                        Styles-Cell-Wrap="True"
                                        KeyFieldName="id"
                                        ParentFieldName="parent_id"
                                        Theme="MetropolisBlue">
                                        <ClientSideEvents SelectionChanged="function(s,e){treeReports_NodeDblClick(s,e);}" />
                                        <Settings VerticalScrollBarMode="Auto" ScrollableHeight="300"
                                            ShowColumnHeaders="false" GridLines="Both" />
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

                        <asp:Panel ID="pnlResult" runat="server" CssClass="row">
                            <%--   <div class="col-md-12">
                                <asp:Label ID="lblMuqayiseEdilenlerLabel" Text="" runat="server" Style="font-weight: bold; margin-bottom: 10px; display: inline-block; font-size: 16px;" />
                                <br />

                            </div>
                            <div class="col-md-4" style="text-align: right">
                                <div style="width: 91%">
                                    <asp:Label ID="lblSelectedGoal" Text="" runat="server" />
                                </div>

                                <asp:Label ID="lblMuqayise1" Text="dddd" runat="server" Style="display: inline-block; width: 90%" />
                                <span style="border: 1px solid #c0c0c0; vertical-align: top; padding: 5px 2px 5px 5px; color: #919191; margin-left: 3px; display: inline-block;">
                                    <i class="fa fa-arrow-right" aria-hidden="true"></i>
                                </span>
                            </div>
                            <div class="col-md-4">
                                <div>
                                    &nbsp;
                                </div>
                                <span style="border: 1px solid #c0c0c0; padding: 5px 2px 5px 5px; vertical-align: top; color: #919191; margin-right: 3px; display: inline-block;">
                                    <i class="fa fa-arrow-left" aria-hidden="true"></i>
                                </span>
                                <asp:Label ID="lblMuqayise2" Text="rrr" runat="server" Style="display: inline-block; width: 90%" />
                            </div>
                            <div class="col-md-4">
                            </div>
                            --%>
                            <div class="col-md-12">
                                <br />
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
                                            <div class="chart-menu-icon">
                                                <i class="fa fa-bars"></i>
                                            </div>
                                            <div class="chart-menu-list-container">
                                                <span>
                                                    <asp:Literal ID="compare_download_label" runat="server" />
                                                </span>
                                                <ul class="chart-menu-list">
                                                    <li>
                                                        <a href="#" class="print-diaqram">
                                                            <asp:Literal ID="compare_download_print" runat="server" />
                                                        </a>
                                                    </li>
                                                    <li>
                                                        <a href="" download="indicator.jpg" class="chart-down-indicator">
                                                            <asp:Literal ID="compare_download_jpg" runat="server" />

                                                        </a>
                                                    </li>
                                                    <li>
                                                        <a href="" download="indicator.png" class="chart-down-indicator">
                                                            <asp:Literal ID="compare_download_png" runat="server" />

                                                        </a>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <%--  <asp:Literal ID="chart_script" Text="" runat="server" />--%>
                                <%-- <a href="" download="compare.jpg" class="chart-down" id="chart-down" style="display: none;">
                                    <asp:Label ID="lblDownload" Text="" runat="server" />
                                </a>--%>
                            </div>
                        </asp:Panel>
                    </div>
              <br />
                    <div class="share-box">
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
                </div>
            </section>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>




