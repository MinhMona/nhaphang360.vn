<%@ Page Title="" Language="C#" MasterPageFile="~/PDVMasterLogined.Master" AutoEventWireup="true" CodeBehind="bang-tich-luy-diem1.aspx.cs" Inherits="NHST.bang_tich_luy_diem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content clearfix">
        <div class="container">
            <div class="breadcrumb clearfix">
                <p><a href="/trang-chu" class="color-black">Trang chủ</a> - <span>Bảng tích lũy điểm thành viên</span></p>
                <img src="/App_Themes/pdv/assets/images/car.png" alt="#">
            </div>
            <h2 class="content-title">Bảng tích lũy điểm thành viên</h2>
            <div class="order-tool clearfix">
                <div class="primary-form custom-width">
                    <table class="customer-table mar-bot3 full-width font-size-16">
                        <tr>
                            <td>Tổng giao dịch đã tích lũy
                            </td>
                            <td >
                                <strong><asp:Label ID="lblPoint" runat="server"></asp:Label>
                                VNĐ</strong>
                            </td>
                        </tr>
                        <tr>
                            <td>Cấp độ thành viên
                            </td>
                            <td>
                                <strong><asp:Label ID="lblVip" runat="server"></asp:Label></strong>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
