<%@ Page Language="C#" MasterPageFile="~/UserMasterNew.Master" AutoEventWireup="true" CodeBehind="thong-tin-nguoi-dung.aspx.cs" Inherits="NHST.thong_tin_nguoi_dung" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="main">
        <div class="row">
            <div class="content-wrapper-before blue-grey lighten-5"></div>
            <div class="col s12">
                <div class="container">
                    <div class="all">
                        <div class="card-panel mt-3 no-shadow">
                            <div class="customer-profile">
                                <div class="row">
                                    <div class="col s12 l4">
                                        <div id="profile-card" class="card" style="overflow: hidden; margin-top: 0.5rem;">
                                            <asp:Literal runat="server" ID="ltrProfile"></asp:Literal>
                                        </div>
                                    </div>
                                    <div class="col s12 l8 ">
                                        <div class="customer-change-wrap">
                                            <div class="card-panel clear">
                                                <div class="customer-change">
                                                    <div class="cs-title mb-3 border-bottom-1">
                                                        <h5>Thông tin tài khoản</h5>
                                                    </div>
                                                    <div class="cs-account clear">
                                                        <div class="input-field col s12">
                                                            <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red" Visible="false"></asp:Label>
                                                        </div>
                                                        <div class="input-field col s12 m6">
                                                            <asp:TextBox runat="server" ID="txtUsername" Enabled="false"></asp:TextBox>
                                                            <label for="username">Tên đăng nhập</label>
                                                        </div>
                                                        <div class="input-field col s12 m6">
                                                            <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control has-validate full-width" placeholder="Họ"></asp:TextBox>
                                                            <span class="error-info-show">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFirstName" Display="Dynamic" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                                                            </span>
                                                            <label for="fullName">Họ của bạn</label>
                                                        </div>
                                                        <div class="input-field col s12 m6">
                                                            <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control has-validate full-width" placeholder="Tên"></asp:TextBox>
                                                            <span class="error-info-show">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtLastName" Display="Dynamic"
                                                                    ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                                                            </span>
                                                            <label for="fullName">Tên của bạn</label>
                                                        </div>
                                                        <div class="input-field col s12 m3">
                                                            <asp:DropDownList ID="ddlGender" runat="server">
                                                                <asp:ListItem Value="1" Text="Nam"></asp:ListItem>
                                                                <asp:ListItem Value="2" Text="Nữ"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <label>Giới tính</label>
                                                        </div>
                                                        <div class="input-field col s12 m3">
                                                            <asp:TextBox runat="server" ID="txtBirthDay" CssClass="datetimepicker"></asp:TextBox>
                                                            <label for="dateofbirth">Ngày sinh</label>
                                                        </div>
                                                        <div class="input-field col s12 m6">
                                                            <asp:TextBox runat="server" ID="txtEmail" Enabled="false"></asp:TextBox>
                                                            <label for="email">Địa chỉ Email</label>
                                                        </div>
                                                        <div class="input-field col s12 m6">
                                                            <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control full-width" placeholder="Số điện thoại"
                                                                onkeypress='return event.charCode >= 48 && event.charCode <= 57'
                                                                MaxLength="11"></asp:TextBox>
                                                            <span class="error-info-show">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPhone" ForeColor="Red"
                                                                    Display="Dynamic" ErrorMessage="Không được để trống số điện thoại."></asp:RequiredFieldValidator>
                                                            </span>
                                                            <label for="phoneNumber">Số điện thoại</label>
                                                        </div>

                                                        <div class="input-field col s12 m6">
                                                            <asp:DropDownList ID="ddlWareHouseFrom" runat="server" DataTextField="WareHouseName"
                                                                DataValueField="ID">
                                                            </asp:DropDownList>
                                                            <label>Kho TQ</label>
                                                        </div>

                                                        <div class="input-field col s12 m6">
                                                            <asp:DropDownList ID="ddlWareHouse" runat="server" DataTextField="WareHouseName"
                                                                DataValueField="ID">
                                                            </asp:DropDownList>
                                                            <label>Kho VN</label>
                                                        </div>

                                                        <div class="input-field col s12 m6">
                                                            <asp:DropDownList ID="ddlShipping" runat="server" DataTextField="ShippingTypeName"
                                                                DataValueField="ID">
                                                            </asp:DropDownList>
                                                            <label>Hình thức VC</label>
                                                        </div>

                                                        <div class="input-field col s12 m6">
                                                            <span class="black-text">Ảnh đại diện</span>
                                                            <div style="display: inline-block; margin-left: 15px;">
                                                                <asp:FileUpload ID="UpIMG" runat="server" class="upload-img" type="file" onchange="previewFiles(this);" title="" />
                                                                <button class="btn-upload" type="button">Upload</button>

                                                            </div>
                                                            <div class="preview-img">
                                                                <asp:Image ID="UpIMGBefore" runat="server" />
                                                            </div>
                                                        </div>

                                                        <div class="input-field col s12">
                                                            <asp:TextBox runat="server" ID="txtAddress2"></asp:TextBox>
                                                            <span class="error-info-show">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddress2" ForeColor="Red" ErrorMessage="Không được để trống."
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </span>
                                                            <label for="address">Địa chỉ</label>
                                                        </div>

                                                        <div class="input-field col s6">
                                                            <asp:TextBox runat="server" ID="txtpass" placeholder="Mật khẩu đăng nhập" TextMode="Password"></asp:TextBox>
                                                            <label for="password">Mật khẩu mới</label>
                                                        </div>
                                                        <div class="input-field col s6">
                                                            <asp:TextBox runat="server" ID="txtconfirmpass" CssClass="form-control has-validate full-width" placeholder="Xác nhận mật khẩu" TextMode="Password"></asp:TextBox>
                                                            <span class="error-info-show">
                                                                <asp:Label ID="lblConfirmpass" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                                            </span>
                                                            <label for="repassword">Nhập lại mật khẩu mới</label>
                                                        </div>

                                                        <div class="input-field col s6">
                                                            <a class="btn btn-primary waves-effect" onclick="Update()">Cập nhật thông tin</a>
                                                            <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật thông tin" Style="display: none" BackColor="White" CssClass="btn btn-primary waves-effect"
                                                                OnClick="btncreateuser_Click" />
                                                        </div>

                                                    </div>

                                                </div>
                                                <div class="clearfix"></div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        function Update() {
            $("#<%=btncreateuser.ClientID%>").click();
        }
    </script>
</asp:Content>

