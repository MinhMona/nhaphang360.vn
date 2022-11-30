<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dang-ky.aspx.cs" Inherits="NHST.dang_ky2" %>

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
    <meta name="description" content="" />
    <meta name="keywords" content="" />
    <meta property="og:title" content="" />
    <meta property="og:type" content="website" />
    <meta property="og:url" content="" />
    <meta property="og:image" content="" />
    <meta property="og:site_name" content="" />
    <meta property="og:description" content="" />
    <title>Đăng ký</title>
    <link rel="apple-touch-icon" href="App_Themes/AdminNew45/assets/images/favicon/apple-touch-icon-152x152.png">
    <link rel="shortcut icon" href="/favicon.ico" type="image/x-icon">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <!-- BEGIN: VENDOR CSS-->
    <link rel="stylesheet" type="text/css" href="/App_Themes/AdminNew45/assets/vendors/vendors.min.css">
    <!-- END: VENDOR CSS-->
    <!-- BEGIN: Page Level CSS-->
    <link rel="stylesheet" type="text/css" href="/App_Themes/AdminNew45/assets/css/materialize.css">
    <link rel="stylesheet" type="text/css" href="/App_Themes/AdminNew45/assets/css/style.css">
    <link rel="stylesheet" type="text/css" href="/App_Themes/AdminNew45/assets/css/pages/login.css">
    <!-- END: Page Level CSS-->
    <!-- BEGIN: Custom CSS-->
    <link rel="stylesheet" type="text/css" href="/App_Themes/AdminNew45/assets/css/custom/custom.css" media="all">
    <link rel="stylesheet" type="text/css" href="/App_Themes/AdminNew45/assets/datepicker/jquery.datetimepicker.css" />
    <link href="/App_Themes/NewUI/js/sweet/sweet-alert.css" rel="stylesheet" type="text/css" />
    <!-- END: Custom CSS-->
</head>
<!-- END: Head-->
<body class="horizontal-layout page-header-light horizontal-menu 1-column login-bg  blank-page blank-page" data-open="click" data-menu="horizontal-menu" data-col="1-column">
    <form id="form1" runat="server" class="login-form" enctype="multipart/form-data">
        <div class="row">
            <div class="col s12">
                <div class="container">
                    <div id="login-page" class="row">
                        <div class="col s12 m8 z-depth-4 card-panel border-radius-6 login-card bg-opacity-8">
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
                                    <h5 class="">Đăng ký</h5>
                                </div>
                            </div>    
                            <div class="row">
                                <div class="input-field col s12">
                                    <i class="material-icons prefix ">perm_identity</i>
                                    <asp:TextBox runat="server" placeholder="Tên đăng nhập" ID="txtUsername" type="text"></asp:TextBox>
                                    <span class="helper-text">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUsername"
                                            ForeColor="Red" Display="Dynamic" ErrorMessage="Không được để trống." ValidationGroup="Group"></asp:RequiredFieldValidator>
                                    </span>
                                    <span class="helper-text">
                                        <asp:RegularExpressionValidator
                                            ID="RegularExpressionValidator1"
                                            runat="server"
                                            ErrorMessage="Username ít nhất 6 ký tự." Display="Dynamic"
                                            ControlToValidate="txtUsername" ForeColor="Red"
                                            ValidationExpression="[0-9a-zA-Z]{6,}" ValidationGroup="Group" />
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationExpression="[a-zA-Z0-9]{6}" ControlToValidate="txtUsername" ForeColor="Red"
                                            Display="Dynamic" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>--%>
                                    </span>
                                </div>
                            </div>

                            <div class="row">
                                <div class="input-field col s12 m6">
                                    <i class="material-icons prefix ">text_fields</i>
                                    <asp:TextBox runat="server" placeholder="Họ của bạn" ID="txtFirstName" type="text"></asp:TextBox>
                                      <span class="helper-text">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFirstName" ForeColor="Red"
                                            Display="Dynamic" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="input-field col s12 l6">
                                    <i class="material-icons prefix ">text_fields</i>
                                    <asp:TextBox runat="server" placeholder="Tên của bạn" ID="txtLastName" type="text"></asp:TextBox>
                                     <span class="helper-text">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtLastName" ForeColor="Red"
                                            Display="Dynamic" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                            </div>

                            <div class="row">
                                <div class="input-field col s12">
                                    <i class="material-icons prefix ">mail_outline</i>
                                    <asp:TextBox runat="server" placeholder="Địa chỉ Email" ID="txtEmail" type="email"></asp:TextBox>
                                    <span class="helper-text">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
                                            ForeColor="Red" Display="Dynamic" ErrorMessage="Không được để trống." ValidationGroup="Group"></asp:RequiredFieldValidator>
                                    </span>
                                    <span class="helper-text">
                                        <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator4" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEmail"
                                            ErrorMessage="Sai định dạng Email" ValidationGroup="Group"
                                            ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" />
                                    </span>
                                </div>
                                <div class="input-field col s12">
                                    <i class="material-icons prefix ">local_phone</i>
                                    <asp:TextBox runat="server" placeholder="Số điện thoại" ID="txtPhone" type="text"></asp:TextBox>
                                    <span class="helper-text">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPhone" ForeColor="Red"
                                            Display="Dynamic" ErrorMessage="Không được để trống." ValidationGroup="Group"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                            </div>

                            <div class="row" style="display: none">
                                <div class="input-field col s12 m6">
                                    <i class="material-icons prefix ">event</i>
                                    <asp:TextBox runat="server" placeholder="Ngày sinh" class="datetimepicker date-only" ID="rBirthday" type="text"></asp:TextBox>
                                    <%--   <span class="helper-text">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="rBirthday" ForeColor="Red"
                                            Display="Dynamic" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                                    </span>--%>
                                </div>
                                <div class="input-field col s12 m6">
                                    <i class="material-icons prefix ">group</i>
                                    <asp:DropDownList runat="server" name="sex" ID="ddlGender">
                                        <asp:ListItem Value="1">Nam</asp:ListItem>
                                        <asp:ListItem Value="2">Nữ</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="row">
                                <div class="input-field col s12">
                                    <i class="material-icons prefix ">business</i>
                                    <asp:TextBox runat="server" placeholder="Địa chỉ" ID="txtAddress" type="text"></asp:TextBox>                                   
                                </div>
                            </div>
                             <div class="row">
                                <div class="input-field col s12 m6">
                                    <i class="material-icons prefix ">lock_outline</i>
                                    <asp:TextBox runat="server" placeholder="Mật khẩu" ID="txtpass" type="password"></asp:TextBox>
                                    <span class="helper-text">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtpass" ForeColor="Red"
                                            Display="Dynamic" ErrorMessage="Không được để trống." ValidationGroup="Group"></asp:RequiredFieldValidator>
                                    </span>
                                    <span class="helper-text">
                                        <asp:RegularExpressionValidator
                                            ID="RegularExpressionValidator2"
                                            runat="server"
                                            ErrorMessage="Mật khẩu ít nhất 8 ký tự." Display="Dynamic"
                                            ControlToValidate="txtpass" ForeColor="Red"
                                            ValidationExpression="[0-9a-zA-Z]{8,}" ValidationGroup="Group" />
                                    </span>
                                </div>
                                <div class="input-field col s12 m6">
                                    <i class="material-icons prefix ">lock_outline</i>
                                    <asp:TextBox runat="server" placeholder="Nhập lại mật khẩu" ID="txtconfirmpass" type="password"></asp:TextBox>
                                    <span class="helper-text">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtconfirmpass" ForeColor="Red"
                                            Display="Dynamic" ErrorMessage="Không được để trống." ValidationGroup="Group"></asp:RequiredFieldValidator>
                                    </span>
                                    <span class="helper-text">
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ForeColor="Red" Display="Dynamic"
                                            ErrorMessage="Không trùng với mật khẩu." ControlToCompare="txtpass"
                                            ControlToValidate="txtconfirmpass" ValidationGroup="Group"></asp:CompareValidator>
                                    </span>
                                </div>
                            </div>
                            <div class="row" style="display: none">
                                <div class="input-field col s12">
                                    <i class="material-icons prefix ">perm_identity</i>
                                    <asp:TextBox runat="server" placeholder="Tài khoản nhân viên Kinh Doanh" ID="txtSalerUsername" type="text"></asp:TextBox>
                                    <span class="helper-text">
                                        <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtSalerUsername" ForeColor="Red"
                                            Display="Dynamic" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>--%>
                                    </span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="input-field col s12">
                                    <i class="material-icons prefix ">dns</i>
                                    <asp:DropDownList ID="ddlWarehouseFrom" runat="server"
                                        DataValueField="ID" DataTextField="WareHouseName">
                                        <asp:ListItem Value="" Selected disabled>Kho TQ</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="input-field col s12">
                                    <i class="material-icons prefix ">dns</i>

                                    <asp:DropDownList ID="ddlReceivePlace" runat="server" CssClass="form-control"
                                        DataValueField="ID" DataTextField="WareHouseName">
                                        <asp:ListItem Value="" Selected disabled>Kho đích</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                                <div class="input-field col s12">
                                    <i class="material-icons prefix ">dns</i>

                                    <asp:DropDownList ID="ddlShippingType" runat="server" CssClass="form-control"
                                        DataValueField="ID" DataTextField="ShippingTypeName">
                                        <asp:ListItem Value="" Selected disabled>Phương thức VC</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <div class="row">
                                <div class="input-field col s12">
                                    <telerik:RadCaptcha ID="RadCaptcha1" runat="server"
                                        ErrorMessage="Sai mã captcha." ForeColor="Red"
                                        ValidationGroup="Group" EnableRefreshImage="false"
                                        CaptchaTextBoxLabel="" CaptchaTextBoxCssClass="custom-width-1">
                                        <CaptchaImage ImageCssClass="captimageClass"></CaptchaImage>
                                    </telerik:RadCaptcha>
                                </div>
                                <div class="input-field col s12">
                                    <asp:Label ID="lblTxtRadCaptcha" Text="Custom Captcha Textbox" Visible="false" runat="server" />
                                    <asp:TextBox ID="txtRadCaptcha" runat="server" Visible="false" MaxLength="5"></asp:TextBox>
                                    <%--<p class="recaptcha">
                                <div id="capcha"></div>
                                <span class="error-info" id="error-capcha" style="color: red;"></span>
                            </p>--%>
                                </div>
                            </div>
                            <div class="row">
                                <div class="input-field col s12">
                                    <a class="btn waves-effect waves-light border-round gradient-45deg-purple-deep-orange col s12" onclick="register()">Đăng ký</a>
                                </div>
                            </div>
                            <div class="row">
                                <div class="input-field col s12">
                                    <p class="margin medium-small"><a href="dang-nhap">Bạn đã có tại khoản ? Đăng nhập tại đây</a></p>
                                </div>
                            </div>
                            <asp:Button ID="btncreateuser" runat="server" Text="Đăng ký"
                                Style="display: none" UseSubmitBehavior="false"
                                OnClick="btncreateuser_Click" ValidationGroup="Group" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- BEGIN VENDOR JS-->
        <script>
            function register() {
                $("#<%=btncreateuser.ClientID%>").click();
            }
        </script>
        <script src="/App_Themes/AdminNew45/assets/js/vendors.min.js" type="text/javascript"></script>
        <!-- BEGIN VENDOR JS-->
        <!-- BEGIN PAGE VENDOR JS-->
        <!-- END PAGE VENDOR JS-->
        <!-- BEGIN THEME  JS-->
        <script src="/App_Themes/NewUI/js/sweet/sweet-alert.js" type="text/javascript"></script>
        <script src="/App_Themes/AdminNew45/assets/js/plugins.js" type="text/javascript"></script>
        <script src="/App_Themes/AdminNew45/assets/js/custom/custom-script.js" type="text/javascript"></script>
        <script src="/App_Themes/AdminNew45/assets/datepicker/jquery.datetimepicker.full.js"></script>
        <!-- END THEME  JS-->
        <!-- BEGIN PAGE LEVEL JS-->
        <!-- END PAGE LEVEL JS-->
        <script type="text/javascript">
            function pageLoad() {
                //Disable autoComplete
                var customCaptchaTextbox = $get("<%=txtRadCaptcha.ClientID %>");
                if (customCaptchaTextbox)
                    customCaptchaTextbox.setAttribute("autoComplete", "off");
            }
        </script>
        <telerik:RadAjaxManager ID="ajaxManager" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="cbCustomTextBox" EventName="CheckedChanged">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="txtRadCaptcha"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="lblTxtRadCaptcha"></telerik:AjaxUpdatedControl>
                        <telerik:AjaxUpdatedControl ControlID="RadCaptcha1"></telerik:AjaxUpdatedControl>
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
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
        
            #login-page .card-panel.border-radius-6.login-card {
                overflow: auto;
            }
        </style>
    </form>
</body>
</html>
