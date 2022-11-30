<%@ Page Title="Danh sách nhân viên" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="stafflist.aspx.cs" Inherits="NHST.manager.stafflist1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .modal.modal-fixed-footer {
            height: auto;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button Style="display: none" UseSubmitBehavior="false" ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Danh sách nhân viên</h4>
                    <div class="right-action">
                        <a href="#addStaff" class="btn modal-trigger waves-effect">Thêm nhân viên</a>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="list-staff col s12 section">
                <div class="list-table card-panel">
                    <div class="filter">
                        <div class="row">
                            <div class="input-field col s6 m4 l2">
                                <asp:TextBox runat="server" type="text" ID="search_name" placeholder="" class=""></asp:TextBox>
                                <label>Username</label>
                            </div>
                            <div class="input-field col s6 m4 l2">
                                <asp:TextBox runat="server" type="text" ID="txtSearchPhone" placeholder="" class=""></asp:TextBox>
                                <label>Số điện thoại</label>
                            </div>
                            <div class="input-field col s6 m4 l2">
                                <asp:DropDownList runat="server" ID="ddlSearchRole">
                                    <asp:ListItem Value="-1" Text="Tất cả"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Manager"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="NV đặt hàng"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="NV Kho TQ"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="NV Kho VN"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="NV Kinh Doanh"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="Kế Toán"></asp:ListItem>
                                </asp:DropDownList>
                                <label>Quyền hạn</label>
                            </div>
                            <div class="input-field col s6 m4 l2">
                                <asp:DropDownList runat="server" ID="ddlSearchStatus">
                                    <asp:ListItem Value="-1" Text="Tất cả"></asp:ListItem>
                                    <asp:ListItem Value="1">Chưa kích hoạt</asp:ListItem>
                                    <asp:ListItem Value="2">Đã kích hoạt</asp:ListItem>
                                    <asp:ListItem Value="3">Đang bị khóa</asp:ListItem>
                                </asp:DropDownList>
                                <label>Trạng thái</label>
                            </div>
                            <div class="input-field col s12 m4 l2">
                                <a href="javascript:;" onclick="myFunction()" class="btn btnsearch">Lọc</a>
                            </div>
                        </div>
                        <%--  <div class="search-name input-field">
                            <asp:TextBox ID="search_name" placeholder="" name="txtSearchName" type="text" onkeypress="myFunction()" runat="server" />
                            <label for="search_name"><span>Username</span></label>
                            <span class="material-icons search-action">search</span>
                        </div>--%>
                    </div>
                    <div class="table responsive-table bordered highlight">
                        <table class="table responsive-table bordered highlight">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Username</th>
                                    <th>Họ và tên</th>
                                    <th>Số điện thoại</th>
                                    <th>Số dư</th>
                                    <th>Trạng thái</th>
                                    <th>Quyền hạn</th>
                                    <th>Ngày tạo</th>
                                    <th>Thao tác</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                            </tbody>
                        </table>
                        <div class="pagi-table float-right mt-2">
                            <%this.DisplayHtmlStringPaging1();%>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="addStaff" class="modal modal-fixed-footer">
        <div class="modal-hd">
            <span class="right"><i class="material-icons modal-close right-align">clear</i></span>
            <h4 class="no-margin center-align">Thêm nhân viên</h4>
        </div>
        <div class="modal-bd">
            <div class="row">
                <div class="input-field col s12 m3">
                    <asp:TextBox runat="server" ID="txtFirstName" type="text" placeholder="" class="validate" data-type="text-only"></asp:TextBox>
                    <label for="full_name">
                        Họ<asp:RequiredFieldValidator runat="server" ID="rq" ControlToValidate="txtFirstName" SetFocusOnError="true"
                            ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator></label>
                </div>
                <div class="input-field col s12 m3">
                    <asp:TextBox runat="server" ID="txtLastName" type="text" placeholder="" class="validate" data-type="text-only"></asp:TextBox>
                    <label for="full_name">
                        Tên<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtLastName" SetFocusOnError="true"
                            ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator></label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txtPhone" onkeypress='return event.charCode >= 48 && event.charCode <= 57'
                        MaxLength="11" type="text" placeholder="" data-type="phone-number" class="validate"></asp:TextBox>
                    <label for="add_phone_number">
                        Số điện thoại<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtPhone" SetFocusOnError="true"
                            ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator></label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="rBirthday" type="text" placeholder="" class="datetimepicker date-only"></asp:TextBox>
                    <label for="add_dateofbirth">Ngày sinh</label>
                </div>
                <div class="col s6 m6">
                    <label>Giới tính</label>
                    <p>
                        <label>
                            <input name="group1" id="nam" class="with-gap" type="radio" checked />
                            <span>Nam</span>
                        </label>
                        <label>
                            <input name="group1" id="nu" class="with-gap" type="radio" />
                            <span>Nữ</span>
                        </label>
                    </p>
                </div>
                <div class="clearfix"></div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txtUsername" type="text" placeholder="" class="validate"></asp:TextBox>
                    <label for="add_username">
                        Tên đăng nhập / Nick name
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtUsername" SetFocusOnError="true"
                            ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator></label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txtEmail" type="email" placeholder="" class="validate"></asp:TextBox>
                    <label for="add_email">
                        Email<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtEmail" SetFocusOnError="true"
                            ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator></label>
                    <span class="helper-text">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ValidationExpression="^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$" ForeColor="Red" ErrorMessage="(Sai định dạng Email)" SetFocusOnError="true"></asp:RegularExpressionValidator></span>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txt_Password" placeholder="" type="password" class=""></asp:TextBox>
                    <label for="add_password">
                        Mật khẩu
                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txt_Password" SetFocusOnError="true"
                            ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator></label>
                    <span class="helper-text" data-error="Vui lòng nhập mật khẩu"></span>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txtConfirmPassword" placeholder="" type="password" class=""></asp:TextBox>
                    <label for="add_repassword">
                        Nhập lại mật khẩu<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtConfirmPassword" SetFocusOnError="true"
                            ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator></label>
                    <span class="helper-text">
                        <asp:CompareValidator ID="CompareValidator1" runat="server" SetFocusOnError="true" ValidationGroup="n" ForeColor="Red" ErrorMessage="(Không trùng khớp với mật khẩu)" ControlToCompare="txt_Password" ControlToValidate="txtConfirmPassword"></asp:CompareValidator></span>
                </div>
                <div class="input-field col s6 m6">
                    <asp:DropDownList ID="ddlLevelID" runat="server" CssClass="form-control select2" AppendDataBoundItems="true" DataTextField="LevelName"
                        DataValueField="ID">
                    </asp:DropDownList>
                    <label>Level</label>
                </div>
                <div class="input-field col s6 m3" style="display:none">
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
                    <label>Vip level</label>
                </div>
                <div class="input-field col s12 m3">
                    <asp:DropDownList ID="ddlSale" runat="server" CssClass="form-control select2" AppendDataBoundItems="true" DataValueField="ID" DataTextField="Username">
                    </asp:DropDownList>
                    <label>Nhân viên kinh doanh</label>
                </div>
                <div class="input-field col s12 m3">
                    <asp:DropDownList ID="ddlDathang" runat="server" CssClass="form-control select2" AppendDataBoundItems="true" DataValueField="ID" DataTextField="Username">
                    </asp:DropDownList>
                    <label>Nhân viên đặt hàng</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control select2">
                        <asp:ListItem Value="1">User</asp:ListItem>
                        <asp:ListItem Value="2">Manager</asp:ListItem>
                        <asp:ListItem Value="3">Nhân viên đặt hàng</asp:ListItem>
                        <asp:ListItem Value="4">Nhân viên kho TQ</asp:ListItem>
                        <asp:ListItem Value="5">Nhân viên kho đích</asp:ListItem>
                        <asp:ListItem Value="6">Nhân viên sale</asp:ListItem>
                        <asp:ListItem Value="7">Nhân viên kế toán</asp:ListItem>
                        <%--<asp:ListItem Value="8">Nhân viên thủ kho</asp:ListItem>--%>
                    </asp:DropDownList>
                    <label>Quyền hạn</label>
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
        <div class="modal-ft">
            <div class="ft-wrap center-align">
                <a href="#!" class="modal-action btn waves-effect waves-green mr-2" onclick="Save()">Thêm</a>
                <a class="modal-action btn orange darken-2 modal-close waves-effect waves-green ml-2">Hủy</a>
            </div>
        </div>
    </div>
    <asp:Button runat="server" ID="btnExcel" OnClick="btnExcel_Click" UseSubmitBehavior="false" Style="display: none" />
    <asp:Button runat="server" ID="btnSave" Text="Lưu" CssClass="btn primary-btn" ValidationGroup="n" OnClick="btnSave_Click" UseSubmitBehavior="false" Style="display: none" />
    <asp:HiddenField runat="server" ID="gender" Value="1" />
    <script>
        var grid = $('#test_grid').data('kendoGrid');
        var pager = grid.pager;
        pager.bind('change', test_pagechange);
        function test_pagechange(e) {
            console.log(e);
        }

          function isEmpty(str) {
            return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
        }

        function myFunction() {
        <%--    if (event.which == 13 || event.keyCode == 13) {
                console.log($('#<%=search_name.ClientID%>').val());--%>
            $('#<%=btnSearch.ClientID%>').click();
            //}
        }
        $('.search-action').click(function () {
            console.log('dkm ngon');
            console.log($('#<%=search_name.ClientID%>').val());
            $('#<%=btnSearch.ClientID%>').click();
        })

        function Save() {
             if (isEmpty($("#<%=rBirthday.ClientID%>").val())) {
                alert('Vui lòng nhập ngày sinh');
                return;
            }

            $(".with-gap").each(function () {
                var check = $(this).is(':checked');
                if (check) {
                    $("#<%=gender.ClientID%>").val($(this).val());
                    console.log($(this).val());
                }
            })

            $("#<%=btnSave.ClientID%>").click();
        }

        $(document).ready(function () {
            $('#add_repassword').on('change', function () {

                var pass = $('#add_password');
                var repass = $(this);
                if (pass.val() == '') {
                    pass.addClass('invalid');
                    repass.val('');
                    pass.focus();
                } else {
                    pass.removeClass('invalid').addClass('valid');
                }
                if (pass.val() != repass.val())

                    repass.addClass('invalid');
                else {
                    repass.removeClass('invalid').addClass('valid');
                }
            });
            $('#search_name').autocomplete({
                data: {
                    "Apple": null,
                    "Microsoft": null,
                    "Google": 'https://placehold.it/250x250',
                    "Asgard": null
                },

            });
        });
    </script>

</asp:Content>
