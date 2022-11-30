<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMaster.Master" AutoEventWireup="true" CodeBehind="thong-tin-nguoi-dung.aspx.cs" Inherits="NHST.thong_tin_nguoi_dung" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="all">
            <div class="main">
                <div class="sec form-sec">
                    <div class="sec-tt">
                        <h2 class="tt-txt">Thông tin người dùng</h2>
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
                            <div class="lb">Tên đăng nhập / Nickname</div>
                            <asp:Label ID="lblUsername" runat="server"></asp:Label>
                        </div>
                        <div class="form-row">
                            <div class="lb">Họ của bạn</div>
                            <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control has-validate" placeholder="Họ"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="form-row">
                            <div class="lb">Tên của bạn</div>
                            <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control has-validate" placeholder="Tên"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtLastName" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
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
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPhone" ForeColor="Red" ErrorMessage="Không được để trống số điện thoại."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="form-row">
                            <div class="lb">Địa chỉ</div>
                            <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control has-validate" placeholder="Địa chỉ"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddress" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="form-row">
                            <div class="lb">Email</div>
                            <asp:Label ID="lblEmail" runat="server"></asp:Label>
                            <%--<asp:TextBox runat="server" ID="txtEmail" CssClass="form-control has-validate" placeholder="Địa chỉ"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>--%>
                        </div>
                        <div class="form-row">
                            <div class="lb">Mật khẩu</div>
                            <asp:TextBox runat="server" ID="txtpass" CssClass="form-control has-validate" placeholder="Mật khẩu đăng nhập" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="form-row">
                            <div class="lb">Xác nhận mật khẩu</div>
                            <asp:TextBox runat="server" ID="txtconfirmpass" CssClass="form-control has-validate" placeholder="Xác nhận mật khẩu" TextMode="Password"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:Label ID="lblConfirmpass" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            </span>
                        </div>
                        <div class="form-row btn-row">
                            <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn btn-success btn-block pill-btn primary-btn"
                                OnClick="btncreateuser_Click" />                           
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
    
    <%--<telerik:radajaxmanager id="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:radajaxmanager>--%>
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
