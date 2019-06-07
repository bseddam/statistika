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
                <!-- Nav tabs -->
                <ul class="nav nav-tabs" role="tablist">
                    <li class="nav-item active">
                        <a class="nav-link active" data-toggle="tab" href="#home" aria-expanded="true">Məlumatın mövcudluq vəziyyəti</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link" data-toggle="tab" href="#menu1">Priotetlik vəziyyəti</a>
                    </li>
                </ul>
                <br />
                <!-- Tab panes -->
                <div class="tab-content">
                    <div id="home" class="tab-content tab-pane active in">


                        <!--GLOBAL/NATIONAL STARTS-->
                        <div class="re1">

                            <div class="row ">
                                <div class="col-md-12 ">
                                    <h5>
                                        <asp:Label ID="lblcemi" runat="server" Text=""></asp:Label>
                                        <span class="label dddd">
                                            <asp:Label ID="lblgostericicemi" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lblgostericinote" runat="server" Text=""></asp:Label></span></h5>
                                    <p>
                                        <span class="label label-success">
                                            <asp:Label ID="lblmovcuddur" runat="server" Text=""></asp:Label></span>
                                        <asp:Label ID="lblmovcuddurnote" runat="server" Text=""></asp:Label>
                                        <b>
                                            <asp:Label ID="lblmovcuddurfaiz" runat="server" Text=""></asp:Label>% </b>
                                    </p>
                                    <p>
                                        <span class="label label-warning">
                                            <asp:Label ID="lblplan" runat="server" Text=""></asp:Label></span>
                                        <asp:Label ID="lblplannote" runat="server" Text=""></asp:Label>
                                        <b>
                                            <asp:Label ID="lblplanfaiz" runat="server" Text=""></asp:Label>% </b>
                                    </p>
                                    <p>
                                        <span class="label label-danger">
                                            <asp:Label ID="lblmelumatyoxdur" runat="server" Text=""></asp:Label></span>
                                        <asp:Label ID="lblmelumatyoxdurnote" runat="server" Text=""></asp:Label>
                                        <b>
                                            <asp:Label ID="lblmelumatyoxdurfaiz" runat="server" Text=""></asp:Label>% </b>
                                    </p>
                                </div>
                            </div>

                            <div class="progress">

                                <div class="progress-bar progress-bar-success progress-bar-striped active" role="progressbar" style='width: <%=lblmovcuddurfaiz.Text+"%"%>'>
                                </div>
                                <div class="progress-bar progress-bar-warning progress-bar-striped active" role="progressbar" style='width: <%=lblplanfaiz.Text+"%"%>'>
                                </div>
                                <div class="progress-bar progress-bar-danger progress-bar-striped active" role="progressbar" style='width: <%=lblmelumatyoxdurfaiz.Text+"%"%>'>
                                </div>
                            </div>

                        </div>
                        <div class="reporg_logos">
                            <div class="row">
                                <div class="col-md-12">
                                    <h2>
                                        <asp:Label ID="lblmeqseduzrenote" runat="server" Text=""></asp:Label></h2>
                                </div>
                            </div>
                            <!--repeater-->
                            <asp:Repeater ID="rptIndicators" runat="server">
                                <ItemTemplate>
                                    <div class="row">
                                        <!-- ROW STARTS -->
                                        <div class="col-md-1 col-xs-12" style="padding: 0px;">
                                            <a href="<%#string.Format("/{0}/goals/{1}/{2}/indicators",Config.getLang(Page),Eval("goal_id"),Config.Slug(Eval("name_short_"+Config.getLang(Page)).ToParseStr()))%>">
                                                <img style="margin-left: 15px;" src="/images/goals-<%=Config.getLang(Page) %>/goal-<%#Eval("goal_id").ToParseStr().PadLeft(2,'0') %>.png" alt="" />
                                            </a>
                                            <%-- <img src="./Portal Test_files/1..png" alt="" width="100"--%>
                                        </div>
                                        <!--   	 -->
                                        <div class="col-md-11 col-xs-12 re">
                                            <div class="dfdfd">
                                                <h5><%#Eval("name_short_"+Config.getLang(Page)) %> <span class="label dddd"><%#Eval("cemisay") %> <%#DALC.GetStaticValue("indicator_value") %></span></h5>
                                                <p><span class="label label-success"><%#Eval("movcuddur")%></span> <%#Eval("movcudname_"+Config.getLang(Page)) %> <b><%#Eval("faizmovcud")%>% </b></p>
                                                <p><span class="label label-warning"><%#Eval("plan")%></span> <%#Eval("planname_"+Config.getLang(Page)) %> <b><%#Eval("faizplan")%>% </b></p>
                                                <p><span class="label label-danger"><%#Eval("arasdirilir")%></span> <%#Eval("arasdirilirname_"+Config.getLang(Page)) %> <b><%#Eval("faizarasdirilir")%>% </b></p>
                                            </div>
                                            <div class="progress">

                                                <div class="progress-bar progress-bar-success progress-bar-striped active" role="progressbar" style='width: <%#Eval("faizmovcud")+"%"%>'>
                                                </div>
                                                <div class="progress-bar progress-bar-warning progress-bar-striped active" role="progressbar" style='width: <%#Eval("faizplan")+"%"%>'>
                                                </div>
                                                <div class="progress-bar progress-bar-danger progress-bar-striped active" role="progressbar" style='width: <%#Eval("faizarasdirilir")+"%"%>'>
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

                    <div id="menu1" class="tab-content tab-pane fade">









                        <!--GLOBAL/NATIONAL STARTS-->
                        <div class="re1">

                            <div class="row ">
                                <div class="col-md-12 ">
                                    <h5>
                                        <asp:Label ID="lblcemi2" runat="server" Text=""></asp:Label>
                                        <span class="label dddd">
                                            <asp:Label ID="lblgostericicemi2" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lblgostericinote2" runat="server" Text=""></asp:Label></span></h5>
                                    <p>
                                        <span class="label label-success">
                                            <asp:Label ID="lblpiroritetdir" runat="server" Text=""></asp:Label></span>
                                        <asp:Label ID="lblpiroritetdirnote" runat="server" Text=""></asp:Label>
                                        <b>
                                            <asp:Label ID="lblpiroritetdirfaiz" runat="server" Text=""></asp:Label>% </b>
                                    </p>

                                    <p>
                                        <span class="label label-danger">
                                            <asp:Label ID="lblpiroritetdeyil" runat="server" Text=""></asp:Label></span>
                                        <asp:Label ID="lblpiroritetdeyilnote" runat="server" Text=""></asp:Label>
                                        <b>
                                            <asp:Label ID="lblpiroritetdeyilfaiz" runat="server" Text=""></asp:Label>% </b>
                                    </p>
                                </div>
                            </div>

                            <div class="progress">

                                <div class="progress-bar progress-bar-success progress-bar-striped active" role="progressbar" style='width: <%=lblpiroritetdirfaiz.Text+"%"%>'>
                                </div>

                                <div class="progress-bar progress-bar-danger progress-bar-striped active" role="progressbar" style='width: <%=lblpiroritetdeyilfaiz.Text+"%"%>'>
                                </div>
                            </div>

                        </div>
                        <div class="reporg_logos">
                            <div class="row">
                                <div class="col-md-12">
                                    <h2>
                                        <asp:Label ID="lblmeqseduzrenote2" runat="server" Text=""></asp:Label></h2>
                                </div>
                            </div>
                            <!--repeater-->
                            <asp:Repeater ID="rptIndicators2" runat="server">
                                <ItemTemplate>
                                    <div class="row">
                                        <!-- ROW STARTS -->
                                        <div class="col-md-1 col-xs-12" style="padding: 0px;">
                                            <a href="<%#string.Format("/{0}/national-priority/{1}/{2}",Config.getLang(Page),Eval("goal_id"),Config.Slug(Eval("name_short_"+Config.getLang(Page)).ToParseStr()))%>">
                 
                                                <img style="margin-left: 15px;" src="/images/goals-<%=Config.getLang(Page) %>/goal-<%#Eval("goal_id").ToParseStr().PadLeft(2,'0') %>.png" alt="" />
                                            </a>
                                        </div>
                                        <!--   	 -->
                                        <div class="col-md-11 col-xs-12 re">
                                            <div class="dfdfd">
                                                <h5><%#Eval("name_short_"+Config.getLang(Page)) %> <span class="label dddd"><%#Eval("cemisay") %> <%#DALC.GetStaticValue("indicator_value") %></span></h5>
                                                <p><span class="label label-success"><%#Eval("prioritetdir")%></span> <%#Eval("prioritetdir_"+Config.getLang(Page)) %> <b><%#Eval("faizprioritetdir")%>% </b></p>
                            
                                                <p><span class="label label-danger"><%#Eval("prioritetdeyil")%></span> <%#Eval("prioritetdeyil_"+Config.getLang(Page)) %> <b><%#Eval("faizprioritetdeyil")%>% </b></p>
                                            </div>
                                            <div class="progress">

                                                <div class="progress-bar progress-bar-success progress-bar-striped active" role="progressbar" style='width: <%#Eval("faizprioritetdir")+"%"%>'>
                                                </div>
                                                <div class="progress-bar progress-bar-danger progress-bar-striped active" role="progressbar" style='width: <%#Eval("faizprioritetdeyil")+"%"%>'>
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
            </div>
        </div>
    </section>



</asp:Content>

