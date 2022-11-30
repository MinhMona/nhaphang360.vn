<%@ Page Title="" Language="C#" MasterPageFile="~/camthachMasterLogined.Master" AutoEventWireup="true" CodeBehind="bang-tich-luy-diem.aspx.cs" Inherits="NHST.bang_tich_luy_diem1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section id="firm-services" class="services">
            <div class="all custom-width-800">
                <h4 class="sec__title center-txt">Bảng tích lũy điểm thành viên</h4>
                <div class="primary-form">
                    <div class="order-tool clearfix">
                        <div class="primary-form custom-width">
                            <table class="customer-table mar-bot3 full-width font-size-16">
                                <tr>
                                    <td>Tổng giao dịch đã tích lũy
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblPoint" runat="server"></asp:Label>
                                            VNĐ</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Cấp độ thành viên
                                    </td>
                                    <td>
                                        <strong>
                                            <asp:Label ID="lblVip" runat="server"></asp:Label></strong>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

        </section>
    </main>

</asp:Content>
