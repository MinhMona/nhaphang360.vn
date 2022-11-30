<%@ Page Language="C#" MasterPageFile="~/UserMasterNew.Master" AutoEventWireup="true" CodeBehind="lich-su-giao-dich-tien-te.aspx.cs" Inherits="NHST.lich_su_giao_dich_tien_te1" %>

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
                                        <h4>LỊCH SỬ GIAO DỊCH TỆ</h4>
                                    </div>
                                </div>
                                <div class="col s12">
                                    <div class="order-list-info">
                                        <div class="total-info mb-2">
                                            <div class="row section">
                                                <div class="col s12">
                                                    <p class="center-align">
                                                        Số dư hiện tại: <span
                                                            class="teal-text text-darken-4 font-weight-700">
                                                            <asp:Literal runat="server" ID="lblAccount"></asp:Literal></span>
                                                    </p>
                                                    <a href="javascript:;" class="btn" id="filter-btn" style="display:none">Bộ lọc</a>
                                                    <div class="filter-wrap mb-2" style="display:none">
                                                        <div class="row mt-2 pt-2">
                                                            <div class="input-field col s6 l4">
                                                                <input type="text" class="datepicker from-date">
                                                                <label>Từ ngày</label>
                                                            </div>
                                                            <div class="input-field col s6 l4">
                                                                <input type="text" class="datepicker to-date">
                                                                <label>Đến ngày</label>
                                                                <span class="helper-text"
                                                                    data-error="Vui lòng chọn ngày bắt đầu trước"></span>
                                                            </div>
                                                            <div class="input-field col s12 l4">
                                                                <select id="status">
                                                                    <option value="">Tất cả</option>
                                                                    <option value="1">Đặt cọc</option>
                                                                    <option value="2">Nhận lại tiền cọc</option>
                                                                </select>
                                                                <label for="status">Trạng thái</label>
                                                            </div>

                                                            <div class="col s12 right-align">
                                                                <a class="btn ">Lọc kết quả</a>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="responsive-tb">
                                                        <table class="table    highlight bordered  centered    mt-2 ">
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
                                                                <asp:Literal runat="server" ID="ltr"></asp:Literal>
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
