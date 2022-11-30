<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMaster.Master" AutoEventWireup="true" CodeBehind="bang-tich-luy-diem.aspx.cs" Inherits="NHST.bang_tich_luy_diem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="all">
            <div class="main">
                <div class="sec form-sec">
                    <div class="sec-tt">
                        <h2 class="tt-txt">Bảng tích lũy điểm thành viên</h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                        </p>
                    </div>
                    <div class="primary-form custom-width">
                        <table class="customer-table mar-bot3 full-width font-size-16">
                            <tr>
                                <td>Tổng giao dịch đã tích lũy
                                </td>
                                <td>
                                    <asp:Label ID="lblPoint" runat="server"></asp:Label> VNĐ
                                </td>
                            </tr>
                            <tr>
                                <td>Cấp độ thành viên
                                </td>
                                <td>
                                    <asp:Label ID="lblVip" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>                        
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
