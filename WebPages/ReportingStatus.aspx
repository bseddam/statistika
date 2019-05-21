<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ReportingStatus.aspx.cs" Inherits="WebPages_Law" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="Server">
    <style>
        .progress {
            max-height: 10px;
        }


        .re1 p {
            margin: 10px 0;
            display: inline-block;
        }

        .re p {
            margin: 10px 0;
            display: inline-block;
            position: relative;
            font-size: 13px;
        }

        .re {
            padding-left: 40px;
        }

        .dddd {
            background-color: #fff;
            border: 1px solid #D0D0D0;
            color: #555;
        }

        .reporting li {
            width: 33.333%;
            text-align: center;
        }
    </style>
    <section id="slider-content">
        <div class="container main-container">
            <div class="breadcrumb">
                <asp:Literal ID="ltrBreadCrumb" Text="" runat="server" />
            </div>
            <div class="borderAll paddingLR10 paddingTB10">

                <div class="tab-content">
                    <!-- ------------------------------------------------------------------------------------------------------------------------ -->
                    <div id="globlandnationl" class="tab-pane fade active in">
                        <!--GLOBAL/NATIONAL STARTS-->
                        <div class="re1">
                            <div class="row ">
                                <div class="col-md-12 ">
                                    <h5>Cəmi <span class="label dddd">245 göstərici</span></h5>
                                    <p><span class="label label-success">129</span> Məlumat mövcuddur <b>53% </b></p>
                                    <p><span class="label label-warning">31</span> Məlumat toplanması  planlaşdırılır <b>13% </b></p>
                                    <p><span class="label label-danger">84</span> Məlumat mənbəyi araşdırlır <b>34% </b></p>
                                </div>
                            </div>
                            <div class="progress">
                                <div class="progress-bar progress-bar-success progress-bar-striped active" role="progressbar" style="width: 53%">
                                </div>
                                <div class="progress-bar progress-bar-warning progress-bar-striped active" role="progressbar" style="width: 13%">
                                </div>
                                <div class="progress-bar progress-bar-danger progress-bar-striped active" role="progressbar" style="width: 34%">
                                </div>
                            </div>
                        </div>
                        <div class="reporg_logos">
                                  <div class="row">
                                        <div class="col-md-12">
                                            <h2>Məqsədlər üzrə cari vəziyyət</h2>
                                        </div>
                                   </div>
                            <!--repeater-->
                            <asp:Repeater ID="rptIndicators" runat="server">
                                <ItemTemplate>
                                    <div class="row">
                                        <!-- ROW STARTS -->
                                        <div class="col-md-1 col-xs-12" style="padding:0px;">
                                            <img style="margin-left:15px;" src="/images/goals-<%=Config.getLang(Page) %>/goal-<%#Eval("goal_id").ToParseStr().PadLeft(2,'0') %>.png" alt=""  />
                                           <%-- <img src="./Portal Test_files/1..png" alt="" width="100"--%>
                                        </div>
                                        <!--   	 -->
                                        <div class="col-md-11 col-xs-12 re">
                                            <div class="dfdfd">
                                                <h5><%#Eval("name_short_"+Config.getLang(Page)) %> <span class="label dddd"><%#Eval("cemisay") %> göstərici</span></h5>
                                                <p><span class="label label-success"><%#Eval("movcuddur")%></span> <%#Eval("movcudname_"+Config.getLang(Page)) %> <b><%#Eval("faizmovcud")%>% </b></p>
                                                <p><span class="label label-warning"><%#Eval("plan")%></span> <%#Eval("planname_"+Config.getLang(Page)) %> <b><%#Eval("faizplan")%>% </b></p>
                                                <p><span class="label label-danger"><%#Eval("arasdirilir")%></span> <%#Eval("arasdirilirname_"+Config.getLang(Page)) %> <b><%#Eval("faizarasdirilir")%>% </b></p>
                                            </div>
                                            <div class="progress">

                                                <div class="progress-bar progress-bar-success progress-bar-striped active" role="progressbar" style='width:<%#Eval("faizmovcud")+"%"%>'>
                                                </div>
                                                <div class="progress-bar progress-bar-warning progress-bar-striped active" role="progressbar" style='width:<%#Eval("faizplan")+"%"%>'>
                                                </div>
                                                <div class="progress-bar progress-bar-danger progress-bar-striped active" role="progressbar" style='width:<%#Eval("faizarasdirilir")+"%"%>'>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- ROW ENDS -->
                                    <br />
                                </ItemTemplate>
                            </asp:Repeater>
                            <!--repeater-->
                        </div>
                    </div>



                    <!--NATIONAL ENDS-->
                </div>


            </div>
        </div>
    </section>



</asp:Content>

