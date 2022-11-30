<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="NHST.Admin.AddUser" %>

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
                        <h3 class="panel-title semi-text text-uppercase">Thêm mới</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row m-b-lg">
                            <div class="col-md-12">
                                <asp:Label ID="lbl_check" runat="server" EnableViewState="false" Visible="false" ForeColor="Red"></asp:Label>
                                <div class="row">
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Họ
                                            <asp:RequiredFieldValidator runat="server" ID="rq" ControlToValidate="txtFirstName" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control" placeholder="Họ"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Tên
                                                       <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtLastName" SetFocusOnError="true"
                                                           ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control" placeholder="Tên"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Số điện thoại (dùng để nhận mã kích hoạt tài khoản)
                                                       <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtPhone" SetFocusOnError="true"
                                                           ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </label>
                                        <div class="lb"></div>
                                        <div class="form-group-left">
                                            <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="form-control select2"></asp:DropDownList>
                                        </div>
                                        <div class="form-group-right">
                                            <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control" placeholder="Số điện thoại" onkeypress='return event.charCode >= 48 && event.charCode <= 57'
                                                MaxLength="11"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Email
                                                       <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtEmail" SetFocusOnError="true"
                                                           ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" placeholder="Email đăng nhập"></asp:TextBox>
                                        <div class="clearfix"></div>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ValidationExpression="^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$" ForeColor="Red" ErrorMessage="(Sai định dạng Email)" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Tên đăng nhập / Nickname
                                                       <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtUsername" SetFocusOnError="true"
                                                           ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" placeholder="Tên đăng nhập / Nickname"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Mật khẩu
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txt_Password" SetFocusOnError="true"
                                                            ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox runat="server" ID="txt_Password" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Xác nhận Mật khẩu
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtConfirmPassword" SetFocusOnError="true"
                                                            ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" SetFocusOnError="true" ValidationGroup="n" ForeColor="Red" ErrorMessage="(Không trùng khớp với mật khẩu)" ControlToCompare="txt_Password" ControlToValidate="txtConfirmPassword"></asp:CompareValidator>
                                        </label>
                                        <asp:TextBox runat="server" ID="txtConfirmPassword" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                    </div>
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
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-12">
                                        <asp:Button runat="server" ID="btnSave" Text="Lưu" CssClass="btn btn-success" ValidationGroup="n" OnClick="btnSave_Click" />
                                    </div>
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
            <telerik:AjaxSetting AjaxControlID="ddlRole">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlSaleGroup" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
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
