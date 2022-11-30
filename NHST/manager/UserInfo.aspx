<%@ Page Title="Thông tin tài khoản" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="UserInfo.aspx.cs" Inherits="NHST.manager.UserInfo" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Thông tin tài khoản</h4>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="list-staff col s12 m12 l8 xl8 section">
                <div class="rp-detail card-panel row">
                    <div class="col s12">
                        <div class="row pb-2 border-bottom-1 ">
                            <div class="col s12">
                                <h5>Cấu hình tài khoản</h5>
                            </div>
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" ID="txtUsername" placeholder="" type="text" disabled></asp:TextBox>
                                <label for="rp_username">Username</label>
                            </div>
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" ID="txtAddress" placeholder="" class="validate" required type="text"></asp:TextBox>
                                <label for="rp_full_name"><span class="red-text">*</span>Địa chỉ</label>
                                <span class="helper-text" data-error="Vui lòng nhập địa chỉ"></span>
                            </div>
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" ID="txtFirstName" class="validate" required placeholder="" type="text"></asp:TextBox>
                                <label for="rp_full_name"><span class="red-text">*</span>Họ</label>
                                <span class="helper-text" data-error="Vui lòng nhập họ"></span>
                            </div>
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" placeholder="" class="validate" required ID="txtLastName" type="text"></asp:TextBox>
                                <label for="rp_full_name"><span class="red-text">*</span>Tên</label>
                                <span class="helper-text" data-error="Vui lòng nhập tên"></span>
                            </div>
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" placeholder="" ID="txtBirthday" type="text" class="datetimepicker date-only" value="20/10/1996"></asp:TextBox>
                                <label for="rp_birthday">Ngày sinh</label>
                            </div>
                            <div class="input-field col s12 m6">
                                <asp:DropDownList runat="server" ID="ddlGender">
                                    <asp:ListItem Value="1" Selected="True">Nam</asp:ListItem>
                                    <asp:ListItem Value="2">Nữ</asp:ListItem>
                                </asp:DropDownList>
                                <label>Giới tính</label>
                            </div>
                            <div class="input-field col s12">
                                <asp:TextBox runat="server" placeholder="" ID="txtEmail" type="email" class="" ReadOnly="true"></asp:TextBox>
                                <label for="rp_email">Email</label>
                            </div>
                            <asp:Literal ID="ltrPass" runat="server"></asp:Literal>
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" placeholder="" ID="txtpass" type="password" class=""></asp:TextBox>
                                <label class="active" for="rp_pass">Mật khẩu mới</label>
                            </div>
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" placeholder="" ID="txtconfirmpass" type="password" class=""></asp:TextBox>
                                <label class="active" for="rp_repass">Xác nhận mật khẩu</label>
                            </div>
                            <div class="input-field col s12">
                                <asp:TextBox runat="server" placeholder="" ID="txtPhone" type="text" class="validate" required MaxLength="11" onkeypress='return event.charCode >= 48 && event.charCode <= 57'></asp:TextBox>
                                <label for="rp_phone"><span class="red-text">*</span>Số điện thoại (dùng để nhận mã kích hoạt tài khoản)</label>
                                <span class="helper-text" data-error="Vui lòng nhập số điện thoại"></span>
                            </div>
                        </div>
                        <asp:Panel ID="pnAdmin" runat="server" Visible="true">
                            <div class="row border-bottom-1">
                                <div class="col s12">
                                    <h5>Cấu hình phí user</h5>
                                </div>
                                <div class="input-field col s12 m6">
                                    <asp:TextBox placeholder="" runat="server" ID="rCurrency" TextMode="Number"></asp:TextBox>
                                    <label for="rp_email">Tỉ giá riêng (Tệ)</label>
                                </div>
                                <div class="input-field col s12 m6">
                                    <asp:TextBox placeholder="" runat="server" ID="txtFeebuypro" TextMode="Number"></asp:TextBox>
                                    <label for="rp_email">Phí mua hàng riêng (%)</label>
                                </div>
                                <div class="input-field col s12 m6">
                                    <asp:TextBox placeholder="" runat="server" ID="txtFeeWeight" TextMode="Number"></asp:TextBox>
                                    <label for="rp_phone">Phí cân nặng riêng (VND/Kg)</label>
                                </div>
                                <div class="input-field col s12 m6" style="display: none">
                                    <asp:TextBox placeholder="" runat="server" ID="rDeposit" TextMode="Number"></asp:TextBox>
                                    <label for="rp_email">Phần trăm đặt cọc</label>
                                </div>
                                <div class="input-field col s12 m6">
                                    <asp:ListBox runat="server" ID="ddlLevelID"></asp:ListBox>
                                    <label>Cấp người dùng</label>
                                </div>
                                <div class="input-field col s12 m6" style="display: none">
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

                                    <label>VIP Level</label>
                                </div>
                                <div class="input-field col s12 m6">
                                    <asp:DropDownList ID="ddlRole" runat="server">
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
                                    <asp:DropDownList ID="ddlSale" runat="server" AppendDataBoundItems="true"
                                        DataValueField="ID" DataTextField="Username">
                                    </asp:DropDownList>
                                    <label>Nhân viên kinh doanh</label>
                                </div>
                                <div class="input-field col s12 m6">
                                    <asp:DropDownList ID="ddlDathang" runat="server" AppendDataBoundItems="true" DataValueField="ID" DataTextField="Username">
                                    </asp:DropDownList>
                                    <label>Nhân viên đặt hàng</label>
                                </div>
                                <div class="input-field col s12 m6">
                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                        <asp:ListItem Value="1">Chưa kích hoạt</asp:ListItem>
                                        <asp:ListItem Value="2">Đã kích hoạt</asp:ListItem>
                                        <asp:ListItem Value="3">Đang bị khóa</asp:ListItem>
                                    </asp:DropDownList>
                                    <label>Trạng thái tài khoản</label>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnWareHouseTQ" Visible="false">
                            <div class="input-field col s12 m6">
                                <asp:DropDownList ID="ddlWareHouseTQ" runat="server" AppendDataBoundItems="true"
                                    DataTextField="WareHouseName" DataValueField="ID">
                                </asp:DropDownList>
                                <label>Kho TQ</label>
                            </div>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="pnWareHouseVN" Visible="false">
                            <div class="input-field col s12 m6">
                                <asp:DropDownList ID="ddlWareHouseVN" runat="server" AppendDataBoundItems="true"
                                    DataTextField="WareHouseName" DataValueField="ID">
                                </asp:DropDownList>
                                <label>Kho VN</label>
                            </div>
                        </asp:Panel>
                        <div class="row section mt-2">
                            <div class="col s12">
                                <a href="#" class="btn btnUpdateUserInfo">Cập nhật</a>
                                <a href="#" class="btn btnBackLink">Trở về</a>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>
    <asp:Button runat="server" ID="btnBackLink" OnClick="btnBackLink_Click" UseSubmitBehavior="false" Style="display: none" />
    <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn primary-btn" UseSubmitBehavior="false" Style="display: none"
        OnClick="btncreateuser_Click" />
    <!-- END: Page Main-->
    <script>
        $('.btnUpdateUserInfo').click(function () {
            $('#<%=btncreateuser.ClientID%>').click();
        });
        $('.btnBackLink').click(function () {
            $('#<%=btnBackLink.ClientID%>').click();
        });
    </script>
</asp:Content>
