<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMaster.Master" AutoEventWireup="true" CodeBehind="dang-ky.aspx.cs" Inherits="NHST.dang_ky" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="all">
            <div class="main">
                <div class="sec form-sec">
                    <div class="sec-tt">
                        <h2 class="tt-txt">Đăng ký</h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                        </p>
                    </div>
                    <div class="primary-form">
                        <div class="form-row">
                            <div class="lb">
                                <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                        </div>

                        <div class="form-row">
                            <div class="lb">Họ của bạn</div>
                            <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control has-validate" placeholder="Họ"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFirstName" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="form-row">
                            <div class="lb">Tên của bạn</div>
                            <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control" placeholder="Tên"></asp:TextBox>
                            <div class="clearfix"></div>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtLastName" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="form-row">
                            <div class="lb">Số điện thoại (dùng để nhận mã kích hoạt tài khoản)</div>
                            <div class="form-group-left">
                                <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                <%--<asp:TextBox runat="server" ID="txt_mavung" CssClass="form-control " Text="+84" ReadOnly ></asp:TextBox>--%>
                            </div>
                            <div class="form-group-right">
                                <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control" placeholder="Số điện thoại" onkeypress='return event.charCode >= 48 && event.charCode <= 57'
                                    MaxLength="11"></asp:TextBox>

                            </div>
                            <div class="clearfix"></div>
                            <span class="mar-top1" style="float:left;width:100%;">Quý khách vui lòng bỏ số 0 ở đầu dãy số di động. VD: +84-91XXX</span>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPhone" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="form-row">
                            <div class="lb">Email</div>
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control has-validate" placeholder="Email"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                            <div class="clearfix"></div>
                            <asp:Label ID="lblcheckemail" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            <div class="clearfix"></div>
                            <span class="error-info-show">
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="txtEmail"
                                    ErrorMessage="Sai định dạng Email" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" />
                            </span>
                        </div>
                        <div class="form-row">
                            <div class="lb">Tên đăng nhập / Nickname:</div>
                            <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control has-validate" placeholder="Tên đăng nhập / Nickname"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUsername" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                            <div class="clearfix"></div>
                            <asp:Label ID="Label1" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                        </div>
                        <div class="form-row">
                            <div class="lb">Mật khẩu</div>
                            <asp:TextBox runat="server" ID="txtpass" CssClass="form-control has-validate" placeholder="Mật khẩu đăng nhập" TextMode="Password"
                                onkeyup="return passwordChanged();"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="rq" runat="server" ControlToValidate="txtpass" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                            <span id="strength"></span>
                        </div>
                        <div class="form-row">
                            <div class="lb">Xác nhận mật khẩu</div>
                            <asp:TextBox runat="server" ID="txtconfirmpass" CssClass="form-control has-validate" placeholder="Xác nhận mật khẩu" TextMode="Password"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtconfirmpass" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                                <br />
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ForeColor="Red" ErrorMessage="Không trùng với mật khẩu." ControlToCompare="txtpass" ControlToValidate="txtconfirmpass"></asp:CompareValidator>
                            </span>
                        </div>
                        <div class="form-row btn-row">
                            <asp:Button ID="btncreateuser" runat="server" Text="Đăng ký" CssClass="btn btn-success btn-block pill-btn primary-btn"
                                OnClick="btncreateuser_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>

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
