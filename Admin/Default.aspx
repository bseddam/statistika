<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>


<!DOCTYPE html>
<!--[if IE 8]> <html lang="en" class="ie8 no-js"> <![endif]-->
<!--[if IE 9]> <html lang="en" class="ie9 no-js"> <![endif]-->
<!--[if !IE]><!-->

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<!--<![endif]-->
<!-- BEGIN HEAD -->
<head runat="server">
    <meta charset="utf-8" />
    <title></title>
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta content="" name="description" />
    <meta content="" name="author" />
    <!-- BEGIN GLOBAL MANDATORY STYLES -->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700&subset=all" rel="stylesheet" type="text/css" />


    <link href="/admin/assets/global/plugins/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="/admin/assets/global/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/admin/assets/global/plugins/uniform/css/uniform.default.css" rel="stylesheet" type="text/css" />
    <link href="/admin/assets/global/plugins/bootstrap-switch/css/bootstrap-switch.min.css" rel="stylesheet" type="text/css" />
    <!-- END GLOBAL MANDATORY STYLES -->
    <!-- BEGIN PAGE LEVEL STYLES -->
    <link href="/admin/assets/admin/pages/css/blog.css" rel="stylesheet" />
    <link href="/admin/assets/global/plugins/select2/select2.css" rel="stylesheet" type="text/css" />
    <link href="/admin/assets/admin/pages/css/login-soft.css" rel="stylesheet" type="text/css" />
    <!-- END PAGE LEVEL SCRIPTS -->
    <!-- BEGIN THEME STYLES -->
    <link href="/admin/assets/global/css/components.css" rel="stylesheet" type="text/css" />
    <link href="/admin/assets/global/css/plugins.css" rel="stylesheet" type="text/css" />

    <link id="style_color" href="/admin/assets/admin/layout/css/themes/default.css" rel="stylesheet" type="text/css" />
    <link href="/admin/assets/admin/layout/css/custom.css" rel="stylesheet" type="text/css" />
    <!-- my css -->
    <link href="/admin/assets/admin/pages/css/custom.css" rel="stylesheet" />
    <!-- END THEME STYLES -->
    <link rel="shortcut icon" href="favicon.ico" />
</head>
<body class="login">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <!-- BEGIN LOGO -->
        <div class="logo">
            <a href="javascript:void(0);">
                <%--     <img src="/pics/gberb.png" alt="logo" style="width: 150px;" />--%>
            </a>
        </div>
        <div class="menu-toggler sidebar-toggler">
        </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" DefaultButton="LnkEnter">
                    <div class="content" style="height: 450px">
                        <h3 class="form-title" style="color: black; font-family: Arial">
                            <asp:Literal ID="ltrTitle" runat="server" Text=""></asp:Literal>
                        </h3>
                        <asp:Panel ID="PnlInfo" runat="server" CssClass="alert" Visible="false">
                            <button class="close" data-close="alert"></button>
                            <asp:Label ID="LblError" runat="server" Text="Label"></asp:Label>

                        </asp:Panel>
                        <div class="form-group">
                            <!--ie8, ie9 does not support html5 placeholder, so we just show field title for that-->
                            <label class="control-label visible-ie8 visible-ie9"></label>
                            <div class="input-icon">
                                <i class="fa fa-user"></i>
                                <asp:TextBox ID="UserLogin" runat="server" CssClass="form-control placeholder-no-fix" MaxLength="50"
                                    placeholder="İstifadəçi adı"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label visible-ie8 visible-ie9">Şifrə</label>
                            <div class="input-icon">
                                <i class="fa fa-lock"></i>
                                <asp:TextBox ID="PassWord" runat="server" CssClass="form-control placeholder-no-fix"
                                    MaxLength="50"
                                    placeholder="Şifrə"
                                         Text=""
                                    TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-actions">
                            <asp:CheckBox ID="ChkRemember" runat="server" Text="Yadda saxla" Checked="true" />

                        </div>
                        <div id="actions" class="form-actions">

                            <div class="col-md-12" style="padding: 0;">
                                <asp:LinkButton ID="LnkEnter" runat="server" CssClass="btn blue pull-right" Style="background-color: #229DA5" OnClick="LnkEnter_Click"
                                    OnClientClick="document.getElementById('actions').style.display='none';document.getElementById('loading').style.display='block'"
                                    Width="100%">
                                Daxil ol <i class="m-icon-swapright m-icon-white"></i></asp:LinkButton>
                            </div>
                            <%--   <div class="col-md-12" style="margin-top: 5px; padding: 0;">
                                <asp:HyperLink ID="LinkButton1" runat="server" CssClass="btn green pull-right" NavigateUrl="/eimza"
                                    Width="100%">
                                E-imza ilə daxil olmaq&nbsp;<i class="fa fa-credit-card"></i></asp:HyperLink>
                            </div>--%>
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>

                        </div>
                        <div id="loading" style="display: none; height: 50px">
                            <%--      <img src="/pics/loadingbig.gif" style="width: 100%; margin-top: 22px;" />--%>
                            <div class="progress progress-striped active">
                                <div class="progress-bar progress-bar-info" role="progressbar" aria-valuenow="100" aria-valuemin="0" aria-valuemax="100" style="width: 100%">
                                    <span class="sr-only">20% Complete </span>
                                </div>
                            </div>
                        </div>
                </asp:Panel>
            </ContentTemplate>

        </asp:UpdatePanel>
        <!-- BEGIN COPYRIGHT -->
        <div class="copyright">
            <asp:Literal ID="LtrFooter" runat="server"></asp:Literal>
        </div>
        <!-- END COPYRIGHT -->
        <!-- BEGIN JAVASCRIPTS(Load javascripts at bottom, this will reduce page load time) -->
        <!-- BEGIN CORE PLUGINS -->
        <!--[if lt IE 9]>
<script src="/admin/assets/global/plugins/respond.min.js"></script>
<script src="/admin/assets/global/plugins/excanvas.min.js"></script> 
<![endif]-->
        <script>
            var jsLangText = new Array();
            jsLangText['username'] = '@Resources.DefaultAdmin.loginuserNameRequired'
            jsLangText['password'] = '@Resources.DefaultAdmin.loginPasswordRequired'
            jsLangText['loading'] = '@Resources.DefaultAdmin.loginLoading'
            jsLangText['globalerror'] = '@Resources.DefaultAdmin.loginErrorGlobal'
        </script>
        <script src="/admin/assets/global/plugins/jquery-1.11.0.min.js" type="text/javascript"></script>
        <script src="/admin/assets/global/plugins/jquery-migrate-1.2.1.min.js" type="text/javascript"></script>
        <!-- IMPORTANT! Load jquery-ui-1.10.3.custom.min.js before bootstrap.min.js to fix bootstrap tooltip conflict with jquery ui tooltip -->
        <script src="/admin/assets/global/plugins/jquery-ui/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
        <script src="/admin/assets/global/plugins/bootstrap/js/bootstrap.min.js" type="text/javascript"></script>
        <script src="/admin/assets/global/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js" type="text/javascript"></script>
        <script src="/admin/assets/global/plugins/jquery-slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
        <script src="/admin/assets/global/plugins/jquery.blockui.min.js" type="text/javascript"></script>
        <script src="/admin/assets/global/plugins/jquery.cokie.min.js" type="text/javascript"></script>
        <script src="/admin/assets/global/plugins/uniform/jquery.uniform.min.js" type="text/javascript"></script>
        <script src="/admin/assets/global/plugins/bootstrap-switch/js/bootstrap-switch.min.js" type="text/javascript"></script>
        <!-- END CORE PLUGINS -->
        <!-- BEGIN PAGE LEVEL PLUGINS -->
        <script src="/admin/assets/global/plugins/jquery-validation/js/jquery.validate.min.js" type="text/javascript"></script>
        <script src="/admin/assets/global/plugins/backstretch/jquery.backstretch.min.js" type="text/javascript"></script>
        <script type="text/javascript" src="/admin/assets/global/plugins/select2/select2.min.js"></script>
        <!-- END PAGE LEVEL PLUGINS -->
        <!-- BEGIN PAGE LEVEL SCRIPTS -->
        <script src="/admin/assets/global/scripts/metronic.js" type="text/javascript"></script>
        <script src="/admin/assets/admin/layout/scripts/layout.js" type="text/javascript"></script>
        <script src="/admin/assets/admin/layout/scripts/quick-sidebar.js" type="text/javascript"></script>
        <script src="/admin/assets/admin/layout/scripts/demo.js" type="text/javascript"></script>

        <!-- END PAGE LEVEL SCRIPTS -->
        <script>
            jQuery(document).ready(function () {
                Metronic.init(); // init metronic core components
                Layout.init(); // init current layout
                QuickSidebar.init(); // init quick sidebar
                Demo.init(); // init demo features

                // init background slide images
                $.backstretch([
                 "/admin/assets/admin/pages/media/bg/111.jpg",
                 //"/admin/assets/admin/pages/media/bg/411.jpg"
                ], {
                    fade: 1000,
                    duration: 8000
                }
             );
            });

        </script>

    </form>
</body>
</html>
