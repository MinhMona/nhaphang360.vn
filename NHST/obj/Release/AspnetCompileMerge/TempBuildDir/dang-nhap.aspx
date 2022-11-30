<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMaster.Master" AutoEventWireup="true" CodeBehind="dang-nhap.aspx.cs" Inherits="NHST.dang_nhap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="all">
            <div class="main">
                <div class="sec form-sec">
                    <div class="sec-tt">
                        <h2 class="tt-txt">Đăng nhập</h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                        </p>
                    </div>
                    <div class="primary-form">
                        
                        <div class="form-row">
                            <div class="lb">
                                <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red" Visible="false"></asp:Label></div>                            
                        </div>
                        
                        <div class="form-row">
                            <div class="lb">Tên đăng nhập / Nickname / Email</div>
                            <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" placeholder="Tên đăng nhập / Nickname / Email"></asp:TextBox>
                            <div class="clearfix"></div>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="form-row">
                            <div class="lb">Mật khẩu đăng nhập</div>
                            <asp:TextBox runat="server" ID="txtpass" CssClass="form-control" placeholder="Mật khẩu đăng nhập" TextMode="Password"></asp:TextBox>
                            <div class="clearfix"></div>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtpass" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                        </div>
                        <div class="form-row">
                            <a href="/quen-mat-khau" title="Lấy lại pass bằng email" style="margin-right: 15px;">Lấy lại pass bằng Email</a>
                            |
                                    <a href="/dang-ky" style="margin-left: 15px" title="Đăng ký tài khoản mới">Đăng ký tài khoản mới</a>
                        </div>                       
                        <div class="form-row btn-row">
                            <asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" CssClass="btn btn-success btn-block pill-btn primary-btn"
                                OnClick="btnLogin_Click" />                           
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
