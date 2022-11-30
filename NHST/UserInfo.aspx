<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="NHST.UserInfo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <div class="row">
            <div class="col-md-6 center">
                <div class="login-box panel panel-white">
                    <div class="panel-body">
                        <div class="row marbot2">
                            <div class="col-md-12">
                                <a href="javascript:;" class="logo-name text-lg">Thông tin cá nhân</a>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group marbot1">
                                   Tên đăng nhập / Nickname
                                </div>
                                <div class="form-group marbot2">
                                   <asp:Label ID="lblUsername" runat="server"></asp:Label>
                                </div>
                                <div class="form-group marbot1">
                                    Họ của bạn
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control has-validate" placeholder="Họ"></asp:TextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Tên của bạn
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control has-validate" placeholder="Tên"></asp:TextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtLastName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Số điện thoại (dùng để nhận mã kích hoạt tài khoản)
                                </div>
                                <div class="form-group marbot2">
                                    <div class="form-group-left">
                                        <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        <%--<asp:TextBox runat="server" ID="txt_mavung" CssClass="form-control " Text="+84" ReadOnly ></asp:TextBox>--%>
                                    </div>
                                    <div class="form-group-right">
                                        <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control" placeholder="Số điện thoại" onkeypress='return event.charCode >= 48 && event.charCode <= 57'
                                            MaxLength="11"></asp:TextBox>
                                    </div>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPhone" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Email
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control has-validate" placeholder="Email"></asp:TextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot2">
                                    <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rBirthday" ShowPopupOnFocus="true" Width="100%" runat="server"
                                        DateInput-CssClass="radPreventDecorate" placeholder="Ngày sinh" CssClass="date" DateInput-EmptyMessage="Ngày sinh">
                                        <DateInput DisplayDateFormat="dd/MM/yyyy" runat="server">
                                        </DateInput>
                                    </telerik:RadDateTimePicker>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rBirthday" ErrorMessage="Không để trống"
                                        Display="Dynamic" ForeColor="Red" ValidationGroup="u"></asp:RequiredFieldValidator>
                                </div>

                                <div class="form-group marbot1">
                                    <div class="lb">Giới tính</div>
                                </div>
                                <div class="form-group marbot2">
                                    <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1" Text="Nam"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Nữ"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group marbot1">
                                    Mật khẩu
                                </div>
                                <div class="form-group marbot1">
                                    <asp:TextBox runat="server" ID="txtpass" CssClass="form-control has-validate" placeholder="Mật khẩu đăng nhập" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="form-group marbot1">
                                    Xác nhận mật khẩu
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox runat="server" ID="txtconfirmpass" CssClass="form-control has-validate" placeholder="Xác nhận mật khẩu" TextMode="Password"></asp:TextBox>
                                    <span class="error-info-show">
                                        <asp:Label ID="lblConfirmpass" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                    </span>
                                </div>
                                <div class="form-group no-margin">
                                    <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn btn-success btn-block large-btn"
                                        OnClick="btncreateuser_Click" />
                                    <%--<a href="javascript:;" onclick="createuser()" class="btn btn-success btn-block small-btn right-btn">Tạo tài khoản</a>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <script type="text/javascript">
        function keypress(e) {
            var keypressed = null;
            if (window.event) {
                keypressed = window.event.keyCode; //IE
            }
            else {
                keypressed = e.which; //NON-IE, Standard
            }
            if (keypressed < 48 || keypressed > 57) {
                if (keypressed == 8 || keypressed == 127) {
                    return;
                }
                return false;
            }
        }
    </script>
</asp:Content>
