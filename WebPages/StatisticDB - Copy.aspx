<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StatisticDB - Copy.aspx.cs" Inherits="WebPages_StatisticDB" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <style>
        #goal-list .mb-0 {
            margin: 0;
        }

        #goal-list .card-header {
            border-bottom: 1px solid #c0c0c0;
            position: relative;
        }


            #goal-list .card-header a {
                color: #000;
                text-decoration: none;
                font-weight: bold;
                padding: 10px 22px 10px 10px;
                display: block;
                white-space: nowrap;
                text-overflow: ellipsis;
                overflow: hidden;
            }

            #goal-list .card-header .active {
                background-color: #f5f5f5;
            }

        #goal-list .arrow {
            right: 10px;
            top: 10px;
            position: absolute;
        }

        .indicator-list label {
            display: inline;
            font-weight: normal;
            margin-bottom: 5px;
        }

        .year-list label {
            display: inline;
            font-weight: normal;
            margin-right: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="fdf" ContentPlaceHolderID="script" runat="server">
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
                        <br />
                        <div class="row">
                            <div class="col-md-3">
                                <h3>
                                    <asp:Literal ID="lblGoalTitle" Text="" runat="server" />
                                </h3>
                                <div style="overflow: auto; height: 650px" class="form-control">

                                    <div id="goal-list" role="tablist">
                                        <asp:Repeater ID="rptGoals" runat="server">
                                            <ItemTemplate>
                                                <div class="card">
                                                    <div class="card-header" role="tab" id="headingOne">
                                                        <h5 class="mb-0">
                                                            <asp:LinkButton ID="lnkGoal" runat="server"
                                                                CommandArgument='<%#Eval("id") %>' OnClick="lnkGoal_Click">
                                                                 <%#Eval("name_short_"+Config.getLang(Page))%>
                                                            </asp:LinkButton>
                                                            <%--  <a data-toggle="collapse" href="#collapse-<%#Eval("id")%>" role="button"
                                                                aria-expanded="true"
                                                                aria-controls="collapse-<%#Eval("id")%>">
                                                                <%#Eval("name_short_"+Config.getLang(Page))%>
                                                            </a>--%>

                                                            <span class="arrow">
                                                                <i class="fa fa-arrow-right"></i>
                                                            </span>
                                                        </h5>
                                                    </div>

                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>



                                    </div>
                                </div>
                            </div>
                            <div class="col-md-9">
                                <div class="row">
                                    <div class="col-md-6">
                                        <h3>
                                            <asp:Literal ID="lblIndicatorTitle" Text="" runat="server" /></h3>

                                        <div class="form-control" style="height: 400px; overflow: auto">
                                            <asp:CheckBoxList ID="rptIndicators" CssClass="indicator-list" DataValueField="id" runat="server">
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <h3>
                                            <asp:Literal ID="lblRegionTitle" Text="" runat="server" /></h3>

                                        <asp:Panel runat="server" ID="pnlRegion" Height="400px" Style="overflow-x: auto;" class="form-control">
                                            <asp:CheckBoxList ID="RegionList" Font-Size="12px" CssClass=" region-list"
                                                DataValueField="id" runat="server">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="row">

                                    <div class="col-md-12">
                                        <h3>
                                            <asp:Literal ID="lblYearTitle" Text="" runat="server" /></h3>


                                        <asp:CheckBoxList runat="server" ID="chkYears"
                                            RepeatDirection="Horizontal"
                                            CssClass="year-list form-control"
                                            DataTextField="year" DataValueField="year">
                                        </asp:CheckBoxList>
                                    </div>
                                    <div class="col-md-12" style="text-align: right">
                                        <br />
                                        <asp:Button ID="btnHesabat" CssClass="btn btn-success" OnClick="btn_Click" Text="" runat="server" />
                                    </div>
                                </div>

                            </div>

                        </div>

                        <div class="row">

                            <div class="col-md-12">
                                <br />
                                <dx:ASPxGridView ID="Grid" runat="server"
                                    AutoGenerateColumns="False" Width="100%" SettingsBehavior-ConfirmDelete="true"
                                    KeyFieldName="id">
                                    <Settings GridLines="Both" />
                                    <SettingsPopup EditForm-AllowResize="True" EditForm-Modal="true" EditForm-HorizontalAlign="Center" EditForm-VerticalAlign="Above" EditForm-Height="400px"></SettingsPopup>
                                    <SettingsEditing Mode="PopupEditForm"></SettingsEditing>
                                    <Columns>

                                        <dx:GridViewDataColumn Caption="No" FieldName="id" VisibleIndex="1" Width="50">
                                            <EditFormSettings Visible="False" />
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataComboBoxColumn Caption="Tipi" FieldName="type_id" VisibleIndex="2">
                                            <EditFormSettings ColumnSpan="2" />
                                        </dx:GridViewDataComboBoxColumn>
                                        <dx:GridViewDataColumn Caption="Kodu" FieldName="code" VisibleIndex="3" Width="100">
                                            <EditFormSettings ColumnSpan="2" />
                                        </dx:GridViewDataColumn>

                                    </Columns>
                                    <Settings ShowFilterRow="false" />
                                    <SettingsPager>
                                        <PageSizeItemSettings Visible="true"></PageSizeItemSettings>
                                    </SettingsPager>
                                    <SettingsText CommandNew="Yeni" PopupEditFormCaption="Form" CommandDelete="Sil"
                                        CommandCancel="Ləğv et" CommandEdit="Yenilə" CommandUpdate="Yadda saxla"
                                        CustomizationWindowCaption="Sütun seçin" ConfirmDelete="Silmək istəditinizə əminsiniz?" />
                                    <SettingsDataSecurity AllowDelete="true" />
                                    <Styles Header-CssClass="grid-header"></Styles>

                                </dx:ASPxGridView>
                            </div>


                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-9">
                            <div id="chart_div"></div>
                            <hr />
                            <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">

                                <asp:Repeater ID="rptMetaData" runat="server">
                                    <ItemTemplate>
                                        <div class="panel panel-default">
                                            <div class="panel-heading" role="tab" id="headingOne">
                                                <h4 class="panel-title">
                                                    <a role="button" data-toggle="collapse" data-parent="#accordion"
                                                        href="#collapse-<%#Eval("id") %>"
                                                        aria-expanded="true" aria-controls="collapseOne">
                                                        <%#Eval("l_name") %>
                                                    </a>
                                                </h4>
                                            </div>
                                            <div id="collapse-<%#Eval("id") %>" class="panel-collapse collapse "
                                                role="tabpanel" aria-labelledby="headingOne">
                                                <div class="panel-body">
                                                    <asp:Literal ID="ltrInfo" Text="" runat="server" />

                                                    <table class="table">
                                                        <tbody>
                                                            <asp:Repeater ID="rptMetadataSub" runat="server">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <th><%#Eval("l_name") %></th>
                                                                        <th><%#Eval("name_"+Config.getLang(Page)) %></th>
                                                                    </tr>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>


                            </div>
                        </div>
                    </div>
                </div>
                </div>
            </section>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>



