<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="NHST.Admin.UserInfo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainTit" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-white">
                    <div class="panel-heading">
                        <h3 class="panel-title semi-text text-uppercase">Chỉnh sửa thông tin User</h3>
                    </div>
                    <div class="panel-body">
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName" ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Tên của bạn
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control has-validate" placeholder="Tên"></asp:TextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtLastName" ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPhone" ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Địa chỉ
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control has-validate" placeholder="Địa chỉ"></asp:TextBox>
                                   <%-- <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddress" ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </span>--%>
                                </div>
                                <div class="form-group marbot1">
                                    Email
                                </div>
                                <div class="form-group marbot2">
                                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                    <%--<asp:TextBox runat="server" ID="txtEmail" CssClass="form-control has-validate" placeholder="Email"></asp:TextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>--%>
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
                                <asp:Panel ID="pnAdmin" runat="server" Visible="false">
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputName">Level</label>
                                        <br />
                                        <asp:DropDownList ID="ddlLevelID" runat="server" CssClass="form-control select2" AppendDataBoundItems="true" DataTextField="LevelName"
                                            DataValueField="ID">
                                        </asp:DropDownList>
                                    </div>
                                     <div class="form-group col-md-12" style="display:none;">
                                        <label for="exampleInputName">VIP Level</label>
                                        <br />
                                        <asp:DropDownList ID="ddlVipLevel" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="0">VIP 0</asp:ListItem>
                                            <asp:ListItem Value="1">VIP 1</asp:ListItem>
                                            <asp:ListItem Value="2">VIP 2</asp:ListItem>
                                            <asp:ListItem Value="3">VIP 3</asp:ListItem>
                                            <asp:ListItem Value="4">VIP 4</asp:ListItem>
                                            <asp:ListItem Value="5">VIP 5</asp:ListItem>
                                            <asp:ListItem Value="6">VIP 6</asp:ListItem>
                                            <asp:ListItem Value="7">VIP 7</asp:ListItem>
                                            <asp:ListItem Value="8">VIP 8</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputName">Nhân viên kinh doanh</label>
                                        <br />
                                        <asp:DropDownList ID="ddlSale" runat="server" CssClass="form-control select2" AppendDataBoundItems="true" DataValueField="ID" DataTextField="Username">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputName">Nhân viên đặt hàng</label>
                                        <br />
                                        <asp:DropDownList ID="ddlDathang" runat="server" CssClass="form-control select2" AppendDataBoundItems="true" DataValueField="ID" DataTextField="Username">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputName">Quyền</label>
                                        <br />
                                        <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="1">User</asp:ListItem>
                                            <asp:ListItem Value="2">Manager</asp:ListItem>
                                            <asp:ListItem Value="3">Nhân viên đặt hàng</asp:ListItem>
                                            <asp:ListItem Value="4">Nhân viên kho TQ</asp:ListItem>
                                            <asp:ListItem Value="5">Nhân viên kho VN</asp:ListItem>
                                            <asp:ListItem Value="6">Nhân viên sale</asp:ListItem>
                                            <asp:ListItem Value="7">Nhân viên kế toán</asp:ListItem>
                                            <asp:ListItem Value="8">Nhân viên thủ kho</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputName">Trạng thái tài khoản</label>
                                        <br />
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2">
                                            <asp:ListItem Value="1">Chưa kích hoạt</asp:ListItem>
                                            <asp:ListItem Value="2">Đã kích hoạt</asp:ListItem>
                                            <asp:ListItem Value="3">Đang bị khóa</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </asp:Panel>
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
