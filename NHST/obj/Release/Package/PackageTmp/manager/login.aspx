<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="NHST.manager.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng nhập hệ thống</title>

    <meta content="width=device-width, initial-scale=1" name="viewport" />
    <meta charset="UTF-8">

    <!-- Styles -->
    <link href='http://fonts.googleapis.com/css?family=Ubuntu:300,400,500,700' rel='stylesheet' type='text/css'>
    <link href="/App_Themes/NewUI/css/pace-master/pace-theme-flash.css" rel="stylesheet" />
    <%--<link href="assets/plugins/uniform/css/uniform.default.min.css" rel="stylesheet"/>--%>
    <link href="/App_Themes/NewUI/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/App_Themes/NewUI/css/font-awesome.css" rel="stylesheet" type="text/css" />
    <link href="/App_Themes/NewUI/css/simple-line-icons.css" rel="stylesheet" type="text/css" />
    <link href="/App_Themes/NewUI/css/waves.min.css" rel="stylesheet" type="text/css" />
    <link href="/App_Themes/NewUI/css/switchery.min.css" rel="stylesheet" type="text/css" />
    <%--<link href="assets/plugins/3d-bold-navigation/css/style.css" rel="stylesheet" type="text/css"/>	--%>
    <link href="/App_Themes/NewUI/js/sweet/sweet-alert.css" rel="stylesheet" type="text/css" />
    <!-- Theme Styles -->
    <link href="/App_Themes/NewUI/css/modern.css" rel="stylesheet" type="text/css" />
    <%--<link href="App_Themes/NewUI/css/modern.min.css" rel="stylesheet" type="text/css" />--%>
    <link href="/App_Themes/NewUI/css/custom.css" rel="stylesheet" type="text/css" />

    <script src="/App_Themes/NewUI/js/modernizr.js" type="text/javascript"></script>


    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->

    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
        <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
        <![endif]-->
</head>
<body class="page-login login-alt">
    <form runat="server">

        <main class="page-content">
            <div class="page-inner">
                <div id="main-wrapper">
                    <div class="row">
                        <div class="col-md-6 center">
                            <div class="login-box panel panel-white">
                                <div class="panel-body">
                                    <div class="row">
                                        <%--<div class="col-md-6">
                                            <a href="index.html" class="logo-name text-lg">Phân bón</a>
                                            <p class="m-t-md">
                                                Quản lý
                                            </p>
                                        </div>--%>

                                        <div class="col-md-6 col-md-offset-3">
                                            <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red" Visible="false"></asp:Label>
                                            <div>
                                                <div class="form-group">
                                                    Tên đăng nhập
                                                            <asp:RequiredFieldValidator runat="server" ID="r1"
                                                                ControlToValidate="rUser" ValidationGroup="log" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" ID="rUser" CssClass="form-control"></asp:TextBox>
                                                    <%--<input type="email" class="form-control" placeholder="Email" required>--%>
                                                </div>
                                                <div class="form-group">
                                                    Mật khẩu 
                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1"
                                                                ControlToValidate="rPass" ValidationGroup="log" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <asp:TextBox runat="server" TextMode="Password" ID="rPass" CssClass="form-control"></asp:TextBox>
                                                    <%--<input type="password" class="form-control" placeholder="Password" required>--%>
                                                </div>
                                                <asp:Button runat="server" ID="btnLogin"
                                                    ValidationGroup="log" CssClass="btn btn-success btn-block" Text="Đăng nhập" OnClick="btnLogin_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Row -->
                </div>
                <!-- Main Wrapper -->
            </div>
            <!-- Page Inner -->
        </main>
        <!-- Page Content -->
    </form>

    <!-- Javascripts -->
    <script src="/App_Themes/NewUI/js/jquery/jquery-2.1.4.min.js" type="text/javascript"></script>
    <script src="/App_Themes/NewUI/js/jquery-ui/jquery-ui.min.js" type="text/javascript"></script>
    <script src="/App_Themes/NewUI/js/pace-master/pace.min.js" type="text/javascript"></script>
    <script src="/App_Themes/NewUI/js/sweet/sweet-alert.js" type="text/javascript"></script>
    <script src="/App_Themes/NewUI/js/jquery-blockui/jquery.blockui.js" type="text/javascript"></script>
    <script src="/App_Themes/NewUI/js/bootstrap/bootstrap.min.js" type="text/javascript"></script>
    <script src="/App_Themes/NewUI/js/slimscroll/jquery.slimscroll.min.js" type="text/javascript"></script>
    <script src="/App_Themes/NewUI/js/switchery/switchery.min.js" type="text/javascript"></script>
    <%--<script src="assets/plugins/uniform/jquery.uniform.min.js"></script>--%>
    <script src="/App_Themes/NewUI/js/classie.js" type="text/javascript"></script>
    <script src="/App_Themes/NewUI/js/waves/waves.min.js" type="text/javascript"></script>
    <script src="/App_Themes/NewUI/js/modern.js" type="text/javascript"></script>
</body>
</html>
