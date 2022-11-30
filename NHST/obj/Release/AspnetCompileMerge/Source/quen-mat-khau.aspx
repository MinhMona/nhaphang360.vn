<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMaster.Master" AutoEventWireup="true" CodeBehind="quen-mat-khau.aspx.cs" Inherits="NHST.quen_mat_khau" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="all">
            <div class="main">
                <div class="sec form-sec">
                    <div class="sec-tt">
                        <h2 class="tt-txt">Quên mật khẩu</h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                        </p>
                    </div>
                    <div class="primary-form">
                        <div class="form-tt center"></div>
                        <div class="form-row">
                            <div class="lb">
                                <asp:Label ID="lblError" runat="server" EnableViewState="false" ForeColor="Red" Visible="false"></asp:Label>
                            </div>
                        </div>

                        <div class="form-row">
                            <div class="lb">Email</div>
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control has-validate" placeholder="Email để lấy lại Mật khẩu"></asp:TextBox>
                            <span class="error-info-show">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Không được để trống."></asp:RequiredFieldValidator>
                            </span>
                            <div class="clearfix"></div>
                            <span class="error-info-show">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                    ValidationExpression="^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$" ForeColor="Red" ErrorMessage="Sai định dạng Email" SetFocusOnError="true">
                                </asp:RegularExpressionValidator>
                            </span>
                        </div>
                        <div class="form-row btn-row">
                            <asp:Button ID="btngetpass" runat="server" Text="Gửi mật khẩu vào mail" CssClass="btn btn-success btn-block pill-btn primary-btn"
                                OnClick="btngetpass_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
    
    <!-- Row -->
</asp:Content>
