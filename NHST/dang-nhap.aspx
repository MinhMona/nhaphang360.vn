<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dang-nhap.aspx.cs" Inherits="NHST.dang_nhap" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>
<html class="loading" lang="en" data-textdirection="ltr">
<!-- BEGIN: Head-->
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui">
    <meta name="description" content="Materialize is a Material Design Admin Template,It's modern, responsive and based on Material Design by Google.">
    <meta name="keywords" content="materialize, admin template, dashboard template, flat admin template, responsive admin template, eCommerce dashboard, analytic dashboard">
    <meta name="author" content="ThemeSelect">
    <title>Đăng nhập</title>
    <link rel="apple-touch-icon" href="App_Themes/AdminNew45/assets/images/favicon/apple-touch-icon-152x152.png">
    <link rel="shortcut icon" href="/favicon.ico" type="image/x-icon">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <!-- BEGIN: VENDOR CSS-->
    <link rel="stylesheet" type="text/css" href="App_Themes/AdminNew45/assets/vendors/vendors.min.css">
    <!-- END: VENDOR CSS-->
    <!-- BEGIN: Page Level CSS-->
    <link rel="stylesheet" type="text/css" href="App_Themes/AdminNew45/assets/css/materialize.css">
    <link rel="stylesheet" type="text/css" href="App_Themes/AdminNew45/assets/css/style.css">
    <link rel="stylesheet" type="text/css" href="App_Themes/AdminNew45/assets/css/pages/login.css">
    <link href="/App_Themes/NewUI/js/sweet/sweet-alert.css" rel="stylesheet" type="text/css" />
    <!-- END: Page Level CSS-->
    <!-- BEGIN: Custom CSS-->
    <link rel="stylesheet" type="text/css" href="App_Themes/AdminNew45/assets/css/custom/custom.css">
    <!-- END: Custom CSS-->
    <script src="/App_Themes/AdminNew/js/jquery-1.9.1.min.js"></script>
</head>
<!-- END: Head-->
<body class="horizontal-layout page-header-light horizontal-menu 1-column login-bg  blank-page blank-page" data-open="click" data-menu="horizontal-menu" data-col="1-column">
    <form id="form1" runat="server" class="login-form" enctype="multipart/form-data">
        <div class="row">
            <div class="col s12">
                <div class="container">
                    <div id="login-page" class="row">
                        <div class="col s12 m6 l4 z-depth-4 card-panel border-radius-6 login-card bg-opacity-8">
                            <asp:ScriptManager runat="server" ID="scr">
                            </asp:ScriptManager>
                            <div class="row">
                                <div class="input-field col s12 align-center">
                                    <span class="img logo-login">
                                        <a href="/">
                                            <img src="/App_Themes/NH360/images/logo.png" alt="logo"></a></span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="input-field col s12">
                                    <h5 class="align-center">Đăng nhập</h5>
                                </div>
                            </div>
                            <div class="row">
                                <div class="input-field col s12">
                                    <i class="material-icons prefix">person_outline</i>
                                    <asp:TextBox runat="server" placeholder="Tài khoản" ID="txtUsername" type="text"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="input-field col s12">
                                    <i class="material-icons prefix">lock_outline</i>
                                    <asp:TextBox runat="server" placeholder="Mật khẩu" ID="txtPassword" type="password"></asp:TextBox>
                                </div>
                            </div>
                            <%--<div class="row">
                                <div class="input-field col s12">
                                    <telerik:radcaptcha id="RadCaptcha1" runat="server"
                                        errormessage="Sai mã captcha." forecolor="Red"
                                        validationgroup="Group" enablerefreshimage="false"
                                        captchatextboxlabel="" captchatextboxcssclass="custom-width-1">
                                        <CaptchaImage ImageCssClass="captimageClass"></CaptchaImage>
                                    </telerik:radcaptcha>
                                </div>
                                <div class="input-field col s12">
                                    <asp:Label ID="lblTxtRadCaptcha" Text="Custom Captcha Textbox" Visible="false" runat="server" />
                                    <asp:TextBox ID="txtRadCaptcha" runat="server" Visible="false" MaxLength="5"></asp:TextBox>
                                </div>
                            </div>--%>
                            <div class="row">
                                <div class="col s12 m12 l12 ml-1">
                                    <p>
                                        <label>
                                            <input id="checkremember" onclick="myFunction()" type="checkbox" />
                                            <span>Ghi nhớ tài khoản</span>
                                        </label>
                                    </p>
                                </div>
                            </div>
                            <div class="row">
                                <div class="input-field col s12">
                                    <a class="btn waves-effect waves-light border-round bg-dark-gradient col s12" onclick="login()">Đăng nhập</a>
                                </div>
                            </div>
                            <div class="row">
                                <div class="input-field col s6 m6 l6">
                                    <p class="margin medium-small"><a href="dang-ky">Đăng ký ngay !</a></p>
                                </div>
                                <div class="input-field col s6 m6 l6">
                                    <p class="margin right-align medium-small"><a href="quen-mat-khau">Quên mật khẩu ?</a></p>
                                </div>
                            </div>
                            <asp:HiddenField runat="server" ID="hdfCB" Value="0" />
                            <asp:Button runat="server" ID="btnLogin" OnClick="btnLogin_Click" Style="display: none"
                                ValidationGroup="Group" UseSubmitBehavior="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script>

            $(document).ready(function () {
                $('#txtUsername').on("keypress", function (e) {
                    if (e.keyCode == 13) {
                        login();
                    }
                });

                $('#txtPassword').on("keypress", function (e) {
                    if (e.keyCode == 13) {
                        login();
                    }
                });
            });


            function login() {
                $("#<%= btnLogin.ClientID%>").click();
            }
        <%--  $("#<%= hdfCB.ClientID%>").val(0);--%>
            function myFunction() {
                var a = $("#<%= hdfCB.ClientID%>").val();
                if (a == "1") {
                    $("#<%= hdfCB.ClientID%>").val("0");
                $('#checkremember').prop("checked", false);
                console.log($("#<%= hdfCB.ClientID%>").val());
            }
            else {
                $("#<%= hdfCB.ClientID%>").val("1");
                $('#checkremember').prop("checked", true);
                console.log($("#<%= hdfCB.ClientID%>").val());
                }
            }
        </script>
        <!-- BEGIN VENDOR JS-->
        <script src="App_Themes/AdminNew45/assets/js/vendors.min.js" type="text/javascript"></script>
        <script src="/App_Themes/NewUI/js/sweet/sweet-alert.js" type="text/javascript"></script>
        <!-- BEGIN VENDOR JS-->
        <!-- BEGIN PAGE VENDOR JS-->
        <!-- END PAGE VENDOR JS-->
        <!-- BEGIN THEME  JS-->
        <script src="App_Themes/AdminNew45/assets/js/plugins.js" type="text/javascript"></script>
        <!-- END THEME  JS-->
        <!-- BEGIN PAGE LEVEL JS-->
        <!-- END PAGE LEVEL JS-->
       <%-- <script type="text/javascript">
            function pageLoad() {
                //Disable autoComplete
                var customCaptchaTextbox = $get("<%=txtRadCaptcha.ClientID %>");
                if (customCaptchaTextbox)
                    customCaptchaTextbox.setAttribute("autoComplete", "off");
            }
        </script>
        <telerik:radajaxmanager id="ajaxManager" runat="server" defaultloadingpanelid="RadAjaxLoadingPanel1">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="cbCustomTextBox" EventName="CheckedChanged">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="txtRadCaptcha"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="lblTxtRadCaptcha"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="RadCaptcha1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:radajaxmanager>--%>
        <style>
            #RadCaptcha1_SpamProtectorPanel {
                width: 100%;
                float: left;
                text-align: center;
            }

            .captimageClass {
                height: 50px;
                width: 180px;
                text-align: center;
                display: initial !important;
            }

            .custom-width-1 {
                text-align: center;
            }            
        </style>
    </form>
</body>
</html>
