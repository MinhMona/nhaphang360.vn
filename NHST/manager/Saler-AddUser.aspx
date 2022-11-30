<%@ Page Title="Thêm khách hàng" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="Saler-AddUser.aspx.cs" Inherits="NHST.manager.Saler_AddUser" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Thêm khách hàng</h4>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="list-staff col s12 section m6">
                <div class="list-table card-panel">
                    <div class="modal-bd">
                        <div>
                            <div class="row">
                                <div class="input-field col s12 m6">
                                    <asp:TextBox runat="server" ID="txtFirstName" placeholder="" type="text" class="validate" data-type="text-only"></asp:TextBox>
                                    <label for="full_name">
                                        Họ<asp:RequiredFieldValidator runat="server" ID="rq" ControlToValidate="txtFirstName" SetFocusOnError="true"
                                            ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator></label>
                                </div>
                                <div class="input-field col s12 m6">
                                    <asp:TextBox runat="server" ID="txtLastName" placeholder="" type="text" class="validate" data-type="text-only"></asp:TextBox>
                                    <label for="full_name">
                                        Tên<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtLastName" SetFocusOnError="true"
                                            ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator></label>
                                </div>
                                <div class="input-field col s12 m6">
                                    <asp:TextBox runat="server" ID="txtPhone" onkeypress='return event.charCode >= 48 && event.charCode <= 57' placeholder="" MaxLength="11" type="text" data-type="phone-number" class="validate"></asp:TextBox>
                                    <label for="add_phone_number">
                                        Số điện thoại<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtPhone" SetFocusOnError="true"
                                            ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator></label>
                                </div>
                                <div class="input-field col s12 m6">
                                    <asp:TextBox runat="server" ID="txtEmail" placeholder="" type="email" class="validate"></asp:TextBox>
                                    <label for="add_email">Email<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtEmail" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator></label>
                                    <span class="helper-text" data-error="Vui lòng nhập đúng định dạng Email"></span>
                                </div>
                                     <div class="input-field col s12 m6">
                                    <asp:TextBox runat="server" placeholder="" ID="txtUsername" type="text" class="validate"></asp:TextBox>
                                    <label for="add_username">Tên đăng nhập / Nickname <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtUsername" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator></label>
                                </div>
                                <div class="input-field col s12 m6">
                                    <asp:DropDownList ID="ddlGender" placeholder="" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="1" Text="Nam"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Nữ"></asp:ListItem>
                                    </asp:DropDownList>
                                    <label>Giới tính</label>
                                </div>
                           

                                <div class="input-field col s12 m6">
                                    <asp:TextBox runat="server" placeholder="" ID="txt_Password" type="password" class=""></asp:TextBox>
                                    <label for="txt_Password">Mật khẩu<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txt_Password" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator></label>
                                    <span class="helper-text" data-error="Vui lòng nhập mật khẩu"></span>
                                </div>
                                <div class="input-field col s12 m6">
                                    <asp:TextBox runat="server" placeholder="" ID="txtConfirmPassword" type="password" class=""></asp:TextBox>
                                    <label for="txtConfirmPassword">Nhập lại mật khẩu<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtConfirmPassword" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </label>
                                     <span class="helper-text">
                                         <asp:CompareValidator ID="CompareValidator1" runat="server" SetFocusOnError="true" ValidationGroup="n" ForeColor="Red" ErrorMessage="(Không trùng khớp với mật khẩu)" ControlToCompare="txt_Password" ControlToValidate="txtConfirmPassword"></asp:CompareValidator>
                                     </span>
                                </div>
                                    <div class="input-field col s12 m6">
                               <asp:DropDownList ID="ddlLevelID" runat="server" CssClass="form-control select2" AppendDataBoundItems="true" DataTextField="LevelName"
                                                DataValueField="ID">
                                            </asp:DropDownList>
                                    <label>Level</label>
                                </div>
                            
                                <div class="input-field col s12 m6">
                                  <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Value="1">Chưa kích hoạt</asp:ListItem>
                                                <asp:ListItem Value="2">Đã kích hoạt</asp:ListItem>
                                                <asp:ListItem Value="3">Đang bị khóa</asp:ListItem>
                                            </asp:DropDownList>
                                    <label>Trạng thái tài khoản</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-ft">
                        <div class="ft-wrap center-align">
                            <a class="modal-action btn modal-close waves-effect waves-green mr-2" onclick="add()">Thêm</a>
<%--                            <a href="#!" class="modal-action btn orange darken-2 modal-close waves-effect waves-green ml-2">Hủy</a>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
   <asp:Button runat="server" ID="btnSave" Text="Lưu" CssClass="btn primary-btn" ValidationGroup="n" OnClick="btnSave_Click" UseSubmitBehavior="false"/>
    <script>
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
        function add() {
            $('#<%=btnSave.ClientID%>').click();
        }
    </script>

</asp:Content>