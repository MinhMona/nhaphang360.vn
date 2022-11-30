<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMaster.Master" AutoEventWireup="true" CodeBehind="OTP.aspx.cs" Inherits="NHST.OTP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="all">
            <div class="main">
                <div class="sec form-sec">
                    <div class="sec-tt">
                        <h2 class="tt-txt">Kích hoạt tài khoản</h2>
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
                        <asp:Panel ID="pnActive" runat="server" Visible="false">
                            
                            <div class="form-row">
                                <div class="lb">Nhập mã OTP</div>
                                <asp:TextBox runat="server" ID="txtotp" CssClass="form-control" placeholder="Mã OTP" MaxLength="6"></asp:TextBox>
                                <div class="clearfix"></div>
                                <span class="error-info-show">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtotp" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                                </span>
                            </div>
                            <div class="form-row btn-row">
                                <asp:Button ID="btnotp" runat="server" Text="Kích hoạt" CssClass="btn btn-success btn-block pill-btn primary-btn"
                                    OnClick="btnotp_Click" />
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="pnOk" runat="server" Visible="false">
                            <div class="form-row">
                                Tài khoản của bạn đã được kích hoạt. Hệ thống sẽ tự động chuyển hướng. Nếu không muốn chờ thì hãy click <a href="/trang-chu">vào đây</a>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </main>    
    <!-- Row -->
</asp:Content>
