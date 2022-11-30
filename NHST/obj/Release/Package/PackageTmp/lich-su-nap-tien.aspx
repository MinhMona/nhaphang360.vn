<%@ Page Language="C#" MasterPageFile="~/UserMasterNew.Master" AutoEventWireup="true" CodeBehind="lich-su-nap-tien.aspx.cs" Inherits="NHST.lich_su_nap_tien" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="main">
        <div class="row">
            <div class="content-wrapper-before blue-grey lighten-5"></div>
            <div class="col s12">
                <div class="container">
                    <div class="all">
                        <div class="card-panel mt-3 no-shadow">
                            <div class="row">
                                <div class="col s12">
                                    <div class="page-title mt-2 center-align">
                                        <h4>LỊCH SỬ GIAO DỊCH</h4>
                                    </div>
                                </div>
                                <div class="col s12">
                                    <div class="order-list-info">
                                        <div class="total-info mb-2">
                                            <div class="row section">
                                                <div class="col s12">
                                                    <p class="center-align">
                                                        Tổng tiền đã nạp: <span
                                                            class="teal-text text-darken-4 font-weight-700">
                                                            <asp:Literal runat="server" ID="lblTotalIncome"></asp:Literal>
                                                            VNĐ</span> <span class="black-text divi">|</span> Số dư hiện
                                                    tại: <span
                                                        class="teal-text text-darken-4 font-weight-700">
                                                        <asp:Literal runat="server" ID="lblAccount"></asp:Literal></span>
                                                        VNĐ
                                                    </p>
                                                    <a href="javascript:;" class="btn" id="filter-btn">Bộ lọc</a>
                                                    <div class="filter-wrap mb-2">
                                                        <div class="row">
                                                            <div class="input-field col s6 l4">
                                                                <asp:TextBox runat="server" ID="FD" placeholder="" CssClass="datetimepicker from-date"></asp:TextBox>
                                                                <label>Từ ngày</label>
                                                            </div>
                                                            <div class="input-field col s6 l4">
                                                                <asp:TextBox runat="server" placeholder="" ID="TD" CssClass="datetimepicker to-date"></asp:TextBox>
                                                                <label>Đến ngày</label>
                                                                <span class="helper-text"
                                                                    data-error="Vui lòng chọn ngày bắt đầu trước"></span>
                                                            </div>
                                                            <div class="input-field col s12 l4">
                                                                <asp:DropDownList runat="server" ID="ddlStatus">
                                                                    <asp:ListItem Value="-1" Text="Tất cả"></asp:ListItem>
                                                                    <asp:ListItem Value="1" Text="Đặt cọc"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="Thanh toán"></asp:ListItem>
                                                                    <asp:ListItem Value="3" Text="Cộng tiền"></asp:ListItem>
                                                                    <asp:ListItem Value="4" Text="Trừ tiền"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <label for="status">Trạng thái</label>
                                                            </div>

                                                            <div class="col s12 right-align">
                                                                <asp:Button ID="btnSear" runat="server"
                                                                    CssClass="btn" OnClick="btnSear_Click" Text="Lọc kết quả" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="responsive-tb">
                                                        <table class="table    highlight bordered  centered    mt-2">
                                                            <thead>
                                                                <tr>
                                                                    <th class="tb-date">Ngày giờ</th>
                                                                    <th>Nội dung</th>
                                                                    <th>Số tiền</th>
                                                                    <th>Loại giao dịch</th>
                                                                    <th>Số dư</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Literal ID="ltr" runat="server"></asp:Literal>
                                                            </tbody>
                                                        </table>
                                                    </div>

                                                    <div class="pagi-table float-right mt-2">
                                                        <%this.DisplayHtmlStringPaging1();%>
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
    </div>
</asp:Content>
