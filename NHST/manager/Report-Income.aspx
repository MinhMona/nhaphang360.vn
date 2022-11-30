<%@ Page Title="Thống kê doanh thu" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="Report-Income.aspx.cs" Inherits="NHST.manager.Report_Income" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        tr.rgFilterRow td input {
            width: 100% !important;
            text-align: center;
        }

        table, th, td {
            color: #373737;
            text-align: center;
            border: solid 1px #d4d4d4 !important;
        }

            td.rgPagerCell.NextPrevAndNumeric {
                float: right;
                width: auto;
                border: hidden !important;
            }

        tr.rgPager.pageClass td table {
            border: hidden !important;
        }

            tr.rgPager.pageClass td table thead tr {
                display: none;
            }

        div.rgWrap.rgAdvPart {
            display: none;
        }

        div.rgWrap.rgArrPart1 {
            float: left;
        }

        div.rgWrap.rgArrPart2 {
            width: auto;
            float: left;
        }

        div.rgWrap.rgNumPart {
            float: left;
        }

        input.rgPageFirst {
            display: none;
        }

        input.rgPagePrev {
            display: none;
        }

        input.rgPageNext {
            display: none;
        }

        input.rgPageLast {
            display: none;
        }

        div.rgWrap.rgNumPart a {
            margin-left: 10px;
            margin-right: 10px;
        }

        a.rgCurrentPage {
            background: #062A34;
            color: #fff;
            padding: 5px 10px;
            cursor: pointer;
            border-radius: 3px;
        }
    </style>
    <asp:Button Style="display: none" UseSubmitBehavior="false" ID="btnFilter" runat="server" OnClick="btnFilter_Click" />
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Thống kê doanh thu</h4>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="col s12">
                <div class="card-panel">
                    <div class="order-list-info">
                        <div class="total-info">
                            <div class="row section">
                                <div class="col s12 m12">
                                    <div class="filter">
                                        <div class="row">
                                            <div class="input-field col s6 m4 l3">
                                                <asp:TextBox runat="server" type="text" ID="rdatefrom" class="datetimepicker from-date"></asp:TextBox>
                                                <label>Từ ngày</label>
                                            </div>
                                            <div class="input-field col s6 m4 l3">
                                                <asp:TextBox runat="server" ID="rdateto" type="text" class="datetimepicker to-date"></asp:TextBox>
                                                <label>Đến ngày</label>
                                                <span class="helper-text"
                                                    data-error="Vui lòng chọn ngày bắt đầu trước"></span>
                                            </div>
                                             <div class="input-field col s12 m4 l3">
                                            <asp:DropDownList runat="server" ID="ddlStaffSale" AppendDataBoundItems="true"
                                                DataValueField="ID" DataTextField="Username">
                                            </asp:DropDownList>
                                            <label for="select_by">Nhân viên kinh doanh</label>
                                        </div>
                                            <div class="input-field col s12 m4 l2">
                                                <a href="javascript:;" class="btn xemthongke">Xem thống kê</a>
                                            </div>
                                            <div class="input-field col s12 m4 l2" style="float: right">
                                                <a href="javascript:;" class="btn changechart">Xem biểu đồ tổng</a>
                                            </div>
                                            <asp:Literal runat="server" ID="lbKCDL"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:Panel runat="server" ID="pnChart" Visible="false">
                            <div class="row section" style="display:none">
                                <div class="col s12 m12">
                                    <div class="card report-all-wrap bieudochitiet">
                                        <div class="card-move-up waves-effect waves-block waves-light">
                                            <div class="move-up white darken-4">
                                                <div>
                                                    <span class="chart-title black-text">Tổng tiền <span id="name-class"></span></span>
                                                    <div class="chart-revenue text-darken-2 grey-text" style="background-color: #e8e8e8 !important;">
                                                        <p class="chart-revenue-per mb-1">Tổng tất cả</p>
                                                        <p class="chart-revenue-total">0 VNĐ</p>
                                                    </div>
                                                </div>
                                                <div class="revenue-line-chart-wrapper">
                                                    <canvas id="revenue-line-chart" height="100"></canvas>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col s12 m12">
                                        <div class="chart-wrap bieudotong" hidden="hidden">
                                            <h5>Tổng tiền: <span id="totalsd">
                                                <asp:Label runat="server" ID="lbTongTien"></asp:Label>
                                                VNĐ</span></h5>
                                            <canvas id="donate-chart" height="100"></canvas>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                    </div>
                    <div class="row section">
                        <div class="col s12 report-list">
                            <div class="ad-table-report responsive-tb">
                                <table class="table tb-border  highlight striped   ">
                                    <thead>
                                        <tr>
                                            <th>Tiêu đề</th>
                                            <th>Tất cả</th>
                                            <th>Đặt cọc -> hàng về VN</th>
                                            <th>Đã thanh toán -> đã hoàn thành </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        <tr>
                                            <td>Tổng tiền ship Trung Quốc</td>
                                            <td class="no-wrap">
                                                <div style="justify-content: space-between">
                                                    <span class=" font-weight-400">
                                                        <asp:Label ID="lblFeeShipCN" runat="server"></asp:Label>
                                                        <asp:Button Style="display: none" ID="btnExcelFeeShipCN" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelFeeShipCN_Click" />
                                                    </span>
                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(7)"><i class="material-icons">file_download</i><span></span></a>
                                                </div>

                                            </td>
                                            <td class="no-wrap">
                                                <div style="justify-content: space-between">
                                                    <span class=" font-weight-400">
                                                        <asp:Label ID="lblFeeShipCN1" runat="server"></asp:Label>
                                                        <asp:Button Style="display: none" ID="btnExcelFeeShipCN1" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelFeeShipCN1_Click" />
                                                    </span>
                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(8)"><i class="material-icons">file_download</i><span></span></a>
                                                </div>


                                            </td>
                                            <td class="no-wrap">
                                                <div style="justify-content: space-between">
                                                    <span class=" font-weight-400">

                                                        <asp:Label ID="lblFeeShipCN2" runat="server"></asp:Label>
                                                        <asp:Button Style="display: none" ID="btnExcelFeeShipCN2" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelFeeShipCN2_Click" />
                                                    </span>
                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(9)"><i class="material-icons">file_download</i><span></span></a>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Tổng tiền phí mua hàng</td>
                                            <td class="no-wrap">
                                                <div style="justify-content: space-between">
                                                    <span class=" font-weight-400">
                                                        <asp:Label ID="lblFeeBuyPro" runat="server"></asp:Label>
                                                        <asp:Button Style="display: none" ID="btnExcelFeeBuyPro" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelFeeBuyPro_Click" />
                                                    </span>
                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(10)"><i class="material-icons">file_download</i><span></span></a>
                                                </div>

                                            </td>
                                            <td class="no-wrap">
                                                <div style="justify-content: space-between">
                                                    <span class=" font-weight-400">

                                                        <asp:Label ID="lblFeeBuyPro1" runat="server"></asp:Label>
                                                        <asp:Button Style="display: none" ID="btnExcelFeeBuyPro1" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelFeeBuyPro1_Click" />
                                                    </span>
                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(11)"><i class="material-icons">file_download</i><span></span></a>
                                                </div>
                                            </td>
                                            <td class="no-wrap">
                                                <div style="justify-content: space-between">
                                                    <span class=" font-weight-400">
                                                        <asp:Label ID="lblFeeBuyPro2" runat="server"></asp:Label>
                                                        <asp:Button Style="display: none" ID="btnExcelFeeBuyPro2" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelFeeBuyPro2_Click" />
                                                    </span>
                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(12)"><i class="material-icons">file_download</i><span></span></a>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Tổng tiền phí cân nặng</td>
                                            <td class="no-wrap">

                                                <div style="justify-content: space-between">
                                                    <span class=" font-weight-400">
                                                        <asp:Label ID="lblFeeWeight" runat="server"></asp:Label>
                                                        <asp:Button Style="display: none" ID="btnExcelFeeWeight" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelFeeWeight_Click" />
                                                    </span>
                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(13)"><i class="material-icons">file_download</i><span></span></a>
                                                </div>

                                            </td>
                                            <td class="no-wrap">
                                                <div style="justify-content: space-between">
                                                    <span class=" font-weight-400">
                                                        <asp:Label ID="lblFeeWeight1" runat="server"></asp:Label>
                                                        <asp:Button Style="display: none" ID="btnExcelFeeWeight1" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelFeeWeight1_Click" />
                                                    </span>
                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(14)"><i class="material-icons">file_download</i><span></span></a>
                                                </div>
                                            </td>
                                            <td class="no-wrap">
                                                <div style="justify-content: space-between">
                                                    <span class=" font-weight-400">
                                                        <asp:Label ID="lblFeeWeight2" runat="server"></asp:Label>
                                                        <asp:Button Style="display: none" ID="btnExcelFeeWeight2" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelFeeWeight2_Click" />

                                                    </span>
                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(15)"><i class="material-icons">file_download</i><span></span></a>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Tổng tiền phí kiểm đếm</td>
                                            <td class="no-wrap">

                                                <div style="justify-content: space-between">
                                                    <span class=" font-weight-400">
                                                        <asp:Label ID="lblIsCheckProductPrice" runat="server"></asp:Label>
                                                        <asp:Button Style="display: none" ID="btnExcelIsCheckProductPrice" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelIsCheckProductPrice_Click" />
                                                    </span>
                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(16)"><i class="material-icons">file_download</i><span></span></a>
                                                </div>

                                            </td>
                                            <td class="no-wrap">
                                                <div style="justify-content: space-between">
                                                    <span class=" font-weight-400">
                                                        <asp:Label ID="lblIsCheckProductPrice1" runat="server"></asp:Label>
                                                        <asp:Button Style="display: none" ID="btnExcelIsCheckProductPrice1" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelIsCheckProductPrice1_Click" />
                                                    </span>
                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(17)"><i class="material-icons">file_download</i><span></span></a>
                                                </div>
                                            </td>
                                            <td class="no-wrap">
                                                <div style="justify-content: space-between">
                                                    <span class=" font-weight-400">
                                                        <asp:Label ID="lblIsCheckProductPrice2" runat="server"></asp:Label>
                                                        <asp:Button Style="display: none" ID="btnExcelIsCheckProductPrice2" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelIsCheckProductPrice2_Click" />
                                                    </span>
                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(18)"><i class="material-icons">file_download</i><span></span></a>
                                                </div>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Tổng tiền phí đóng gỗ</td>
                                            <td class="no-wrap">
                                                <div style="justify-content: space-between">
                                                    <span class=" font-weight-400">
                                                        <asp:Label ID="lblIsPackedPrice" runat="server"></asp:Label>
                                                        <asp:Button Style="display: none" ID="btnExcelIsPackedPrice" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelIsPackedPrice_Click" />
                                                    </span>
                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(19)"><i class="material-icons">file_download</i><span></span></a>
                                                </div>

                                            </td>
                                            <td class="no-wrap">
                                                <div style="justify-content: space-between">
                                                    <span class=" font-weight-400">
                                                        <asp:Label ID="lblIsPackedPrice1" runat="server"></asp:Label>
                                                        <asp:Button Style="display: none" ID="btnExcelIsPackedPrice1" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelIsPackedPrice1_Click" />
                                                    </span>
                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(20)"><i class="material-icons">file_download</i><span></span></a>
                                                </div>
                                            </td>
                                            <td class="no-wrap">
                                                <div style="justify-content: space-between">
                                                    <span class=" font-weight-400">
                                                        <asp:Label ID="lblIsPackedPrice2" runat="server"></asp:Label>
                                                        <asp:Button Style="display: none" ID="btnExcelIsPackedPrice2" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelIsPackedPrice2_Click" />
                                                    </span>

                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(21)"><i class="material-icons">file_download</i><span></span></a>
                                                </div>

                                            </td>

                                        </tr>
                                        <tr>
                                            <td>Những đơn đã mua hàng</td>
                                            <td colspan="3">
                                                <div style="justify-content: space-between">
                                                    <asp:Label ID="lblDamuahang" runat="server"></asp:Label>
                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(1)"><i class="material-icons">file_download</i><span></span></a>

                                                </div>
                                                <asp:Button Style="display: none" ID="btnExcelDondamuahang" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelDondamuahang_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Những đơn đã hoàn thành
                                            </td>
                                            <td colspan="3">
                                                <div style="justify-content: space-between">
                                                    <asp:Label ID="lblDahoanthanh" runat="server"></asp:Label>
                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(2)"><i class="material-icons">file_download</i><span></span></a>
                                                </div>
                                                <asp:Button Style="display: none" ID="btnExcelDahoanthanh" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelDahoanthanh_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Những đơn từ lúc đã đặt cọc đến khi hoàn thành
                                            </td>
                                            <td colspan="3">

                                                <div style="justify-content: space-between">
                                                    <asp:Label ID="lbldatcocdenhoanthanh" runat="server"></asp:Label>
                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(3)"><i class="material-icons">file_download</i><span></span></a>
                                                </div>
                                                <asp:Button Style="display: none" ID="btnExcelDatcocdenhoanthanh" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelDatcocdenhoanthanh_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Tổng tiền cọc</td>
                                            <td colspan="3">

                                                <div style="justify-content: space-between">
                                                    <asp:Label ID="lblDeposit" runat="server"></asp:Label>
                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(4)"><i class="material-icons">file_download</i><span></span></a>
                                                </div>
                                                <asp:Button Style="display: none" ID="btnExcelDeposit" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelDeposit_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Tổng tiền chưa thanh toán</td>
                                            <td colspan="3">

                                                <div style="justify-content: space-between">
                                                    <asp:Label ID="lblNotPay" runat="server"></asp:Label>
                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(5)"><i class="material-icons">file_download</i><span></span></a>
                                                </div>
                                                <asp:Button Style="display: none" ID="BtnExcelNotPay" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="BtnExcelNotPay_Click" />
                                            </td>
                                        </tr>
                                        <tr style="display:none;">
                                            <td>Tổng tiền đơn hàng hỏa tốc</td>
                                            <td colspan="3">

                                                <div style="justify-content: space-between">
                                                    <asp:Label ID="lblOrderFast" runat="server"></asp:Label>
                                                    <a href="javascript:;" style="float: right" class="teal-text text-darken-4 valign-wrapper icon-valign" onclick="myFuntion(6)"><i class="material-icons">file_download</i><span></span></a>
                                                </div>
                                                <asp:Button Style="display: none" ID="btnExcelOrderFast" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelOrderFast_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Tổng tiền phí ship tận nhà</td>
                                            <td class="no-wrap" colspan="3">
                                                <div style="justify-content: space-between">
                                                    <span class=" font-weight-400">
                                                        <asp:Label ID="lblIsFastDeliveryPrice" runat="server"></asp:Label>
                                                    </span>

                                                </div>

                                            </td>

                                        </tr>
                                        <tr class="grey lighten-3">
                                            <td>
                                                <h6 class="black-text font-weight-700">Tổng tiền tất cả</h6>
                                            </td>
                                            <td colspan="3">
                                                <h6 class="red-text text-darken-4 font-weight-700">
                                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                                </h6>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>

                        </div>
                    </div>
                    <div class="row section mt-2" style="display:none">
                        <div class="input-field col s12 m4 l12"  style="justify-content: space-between">
                            <h5 class="black-text font-weight-700" style="float:left">Thống kê thanh toán</h5>
                            <a href="javascript:;" class="btn btnExport" style="float:right">Xuất Excel</a>
                        </div>
                        <div class="col s12">
                            <div class="responsive-tb mt-2">
                                <telerik:RadGrid runat="server" ID="RadGrid1" OnNeedDataSource="RadGrid1_NeedDataSource" AutoGenerateColumns="False"
                                    AllowPaging="True" PageSize="10" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#004d40"
                                    AllowAutomaticUpdates="True" OnItemCommand="RadGrid1_ItemCommand" OnPageIndexChanged="RadGrid1_PageIndexChanged"
                                    AllowSorting="True" OnPageSizeChanged="RadGrid1_PageSizeChanged">
                                    <MasterTableView CssClass="table highlight  tb-border ad-rp-checkout" DataKeyNames="MainOrderID">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="MainOrderID" HeaderText="Mã đơn hàng" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Username" HeaderText="Username" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Status" HeaderText="Loại thanh toán" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Amount" HeaderText="Số tiền" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CreatedDate" HeaderText="Ngày tạo" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CreatedBy" HeaderText="Người tạo" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <PagerStyle CssClass="pageClass" ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next"
                                            PrevPageText="Prev" />
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </div>
                    <div class="row section mt-2">
                        <div class="input-field col s12 m4 l12" style="justify-content: space-between">
                            <h5 class="black-text font-weight-700" style="float:left">Thống kê đơn hàng</h5>
                            <a href="javascript:;" class="btn btnExportExcel" style="float:right">Xuất Excel</a>
                        </div>
                        <div class="col s12">
                            <div class="responsive-tb mt-2">
                                <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                                    AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="#004d40"
                                    AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                                    AllowSorting="True" OnPageSizeChanged="gr_PageSizeChanged">
                                    <MasterTableView CssClass="table highlight  tb-border ad-rp-checkout" DataKeyNames="OrderID">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="OrderID" HeaderText="Mã đơn hàng" HeaderStyle-CssClass="no-wrap" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Username" HeaderText="Username" HeaderStyle-CssClass="no-wrap" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ShopName" HeaderText="Tên shop" HeaderStyle-CssClass="no-wrap" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Saler" HeaderText="NV kinh doanh" HeaderStyle-CssClass="no-wrap" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ShipCN" HeaderText="Phí ship TQ" HeaderStyle-CssClass="no-wrap" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="BuyPro" HeaderText="Phí mua hàng" HeaderStyle-CssClass="no-wrap" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FeeWeight" HeaderText="Phí cân nặng" HeaderStyle-CssClass="no-wrap" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ShipHome" HeaderText="Phí Giao tận nhà" HeaderStyle-CssClass="no-wrap" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CheckProduct" HeaderText="Phí kiểm kê" HeaderStyle-CssClass="no-wrap" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Package" HeaderText="Phí đóng gói" HeaderStyle-CssClass="no-wrap" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FeeReal" HeaderText="Tiền mua thật" HeaderStyle-CssClass="no-wrap" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Total" HeaderText="Tổng tiền" HeaderStyle-CssClass="no-wrap" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Deposit" HeaderText="Đặt cọc" HeaderStyle-CssClass="no-wrap" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PayLeft" HeaderText="Còn lại" HeaderStyle-CssClass="no-wrap" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Status" HeaderText="Trạng thái" ItemStyle-CssClass="no-wrap" HeaderStyle-CssClass="no-wrap" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CreatedDate" HeaderText="Ngày tạo" HeaderStyle-CssClass="no-wrap" HeaderStyle-Width="5%">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <PagerStyle CssClass="pageClass" ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next"
                                            PrevPageText="Prev" />
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="hdfRevenueDataset" />
    <asp:HiddenField runat="server" ID="hdfCountryRevenueChart" />
    <asp:Button runat="server" ID="btnExport" style="display:none" OnClick="btnExport_Click"/>
    <asp:Button runat="server" ID="btnExportExcel" style="display:none" OnClick="btnExportExcel_Click"/>
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilter">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">

            $('.btnExport').click(function () {

                $('#<%=btnExport.ClientID%>').click();
             });
            $('.btnExportExcel').click(function () {

                $('#<%=btnExportExcel.ClientID%>').click();
            }); 
            function myFuntion(ID) {
                switch (ID) {
                    case 1:
                        $('#<%=btnExcelDondamuahang.ClientID%>').click();
                        break;
                    case 2:
                        $('#<%=btnExcelDahoanthanh.ClientID%>').click();
                        break;
                    case 3:
                        $('#<%=btnExcelDatcocdenhoanthanh.ClientID%>').click();
                        break;
                    case 4:
                        $('#<%=btnExcelDeposit.ClientID%>').click();
                        break;
                    case 5:
                        $('#<%=BtnExcelNotPay.ClientID%>').click();
                        break;
                    case 6:
                        $('#<%=btnExcelOrderFast.ClientID%>').click();
                        break;
                    case 7:
                        $('#<%=btnExcelFeeShipCN.ClientID%>').click();
                        break;
                    case 8:
                        $('#<%=btnExcelFeeShipCN1.ClientID%>').click();
                        break;
                    case 9:
                        $('#<%=btnExcelFeeShipCN2.ClientID%>').click();
                        break;
                    case 10:
                        $('#<%=btnExcelFeeBuyPro.ClientID%>').click();
                        break;
                    case 11:
                        $('#<%=btnExcelFeeBuyPro1.ClientID%>').click();
                        break;
                    case 12:
                        $('#<%=btnExcelFeeBuyPro2.ClientID%>').click();
                        break;
                    case 13:
                        $('#<%=btnExcelFeeWeight.ClientID%>').click();
                        break;
                    case 14:
                        $('#<%=btnExcelFeeWeight1.ClientID%>').click();
                        break;
                    case 15:
                        $('#<%=btnExcelFeeWeight2.ClientID%>').click();
                        break;
                    case 16:
                        $('#<%=btnExcelIsCheckProductPrice.ClientID%>').click();
                        break;
                    case 17:
                        $('#<%=btnExcelIsCheckProductPrice1.ClientID%>').click();
                        break;
                    case 18:
                        $('#<%=btnExcelIsCheckProductPrice2.ClientID%>').click();
                        break;
                    case 19:
                        $('#<%=btnExcelIsPackedPrice.ClientID%>').click();
                        break;
                    case 20:
                        $('#<%=btnExcelIsPackedPrice1.ClientID%>').click();
                        break;
                    case 21:
                        $('#<%=btnExcelIsPackedPrice2.ClientID%>').click();
                        break;
                }


            }
            $('.xemthongke').click(function () {

                $('#<%=btnFilter.ClientID%>').click();
            })
            $('.changechart').click(function () {
                var a = $('.changechart').text();
                if (a == "Xem biểu đồ tổng") {
                    $('.changechart').text("Xem biểu đồ chi tiết");
                    $('.bieudochitiet').hide();
                    $('.bieudotong').show();
                }
                else {
                    $('.changechart').text("Xem biểu đồ tổng");
                    $('.bieudochitiet').show();
                    $('.bieudotong').hide();
                }
                console.log($('.changechart').text());
            })
            function OnDateSelected(sender, eventArgs) {
                var date1 = sender.get_selectedDate();
                date1.setDate(date1.getDate() + 31);
                var datepicker = $find("<%= rdateto.ClientID %>");
                datepicker.set_maxDate(date1);
            }
        </script>
        <script>
            /*
         * Card Advanced - Card
         */
            // Màu cơ sở


            window.onload = function () {

                var color = {
                    'tiencoc': '#D84315',
                    'chuathanhtoan': '#FFC107',
                    'hanghoatoc': '#4CAF50',
                    'tienship': '#2196F3',
                    'tienmuahang': '#E91E63',
                    'tiencannang': '#8f21f3',
                    'tienkiemdem': '#d913e6',
                    'tiendonggo': '#104cf5',
                    'shiptannha': '#8efffa',
                }
                var yearSwap = document.getElementById('year-month-swap');
                var totalRevenue = document.getElementsByClassName('chart-revenue-total');
                //Trending line chart
                var revenueLineChartCTX = $("#revenue-line-chart");
                var revenueLineChart;


                var monthRevenueDataset = [];
                var c = $('#<%=hdfRevenueDataset.ClientID%>').val();
                var a;
                if (c != null || c != "") {
                    a = JSON.parse($('#<%=hdfRevenueDataset.ClientID%>').val());
                    for (var i = 0; i < a.length; i++) {
                        var s = {
                            label: a[i].label,
                            data: JSON.parse(a[i].data),
                            backgroundColor: a[i].backgroundColor,
                            fill: a[i].fill,
                        }
                        monthRevenueDataset.push(s);
                    }
                }
                //var v = {
                //    label: 'Chung',
                //    data: [200000000,200000000],
                //    backgroundColor: "#8efffa",
                //    fill: false,
                //}
                //monthRevenueDataset.push(v);
                console.log(monthRevenueDataset);



                var revenueLineChartOptions = {
                    plugins: {
                        datalabels: {
                            display: false
                        }
                    },
                    responsive: true,
                    // maintainAspectRatio: false,
                    layout: {
                        padding: {
                            top: 10
                        }
                    },
                    legend: {
                        display: true,
                        labels: {
                            fontColor: '#000',
                            padding: 20

                        },
                        onClick: function (e, legendItem) {
                            //   console.log(legendItem);
                            var index = legendItem.datasetIndex;
                            var ci = this.chart;
                            var alreadyHidden = (ci.getDatasetMeta(index).hidden === null) ? false : ci.getDatasetMeta(index).hidden;
                            totalRevenue[0].innerText = updateTotalRevenue(ci.data.datasets[index].data);
                            var hiddenArr = [];
                            ci.data.datasets.forEach(function (e, i) {

                                var meta = ci.getDatasetMeta(i);

                                if (i !== index) {
                                    if (!alreadyHidden) {
                                        meta.hidden = meta.hidden === null ? !meta.hidden : null;
                                    } else if (meta.hidden === null) {
                                        meta.hidden = true;
                                    }
                                } else if (i === index) {
                                    meta.hidden = null;
                                }
                                if (meta.hidden === true) {
                                    hiddenArr.push(meta.hidden)
                                }
                            });
                            //    console.log(hiddenArr.length);
                            if (hiddenArr.length === 0) {
                                totalRevenue[0].innerText = updateTotalRevenueAll(revenueLineChartData.datasets);
                            }
                            ci.update();
                        }

                    },
                    hover: {
                        mode: "label"
                    },
                    scales: {
                        xAxes: [
                            {
                                beginAtZero: true,
                                display: true,

                                gridLines: {
                                    display: false
                                },
                                ticks: {
                                    fontColor: "#000",
                                }
                            }
                        ],
                        yAxes: [
                            {
                                display: true,
                                beginAtZero: true,
                                fontColor: "#000",
                                gridLines: {
                                    display: true,
                                    color: "rgba(000,000,000,0.15)"
                                },
                                ticks: {
                                    beginAtZero: true,
                                    fontColor: "#000",
                                    callback: function (value, index, values) {
                                        if (parseInt(value) >= 1000) {
                                            return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                        } else {
                                            return value;
                                        }
                                    }
                                }
                            }
                        ]
                    },
                    tooltips: {
                        titleFontSize: 0,
                        callbacks: {
                            label: function (tooltipItem, data) {
                                console.log(data);
                                if (parseInt(tooltipItem.yLabel) >= 1000) {
                                    return data.datasets[tooltipItem.datasetIndex].label + ': ' + tooltipItem.yLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + ' VNĐ';
                                } else {
                                    return data.datasets[tooltipItem.datasetIndex].label + ': ' + tooltipItem.yLabel + ' VNĐ';
                                }

                            }
                        }
                    }
                };

                var revenueLineChartData = {
                    labels: ["Từ đặt cọc đến khi hàng về VN", "Từ đã thanh toán đến đã hoàn thành"],
                    datasets: monthRevenueDataset
                };

                //Update total Revenue from data[0]  -- Month and year
                function updateTotalRevenue(data) {
                    const reducer = (accumulator, currentValue) => accumulator + currentValue;
                    var totalData = data.reduce(reducer).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    return totalData + ' VNĐ';
                };

                function updateTotalRevenueAll(listData) {
                    var total = 0;
                    // console.log(listData);
                    listData.forEach(dataset => {
                        // console.log(data);
                        var reducer = (acc, cur) => acc + cur;
                        var totalData = dataset.data.reduce(reducer);
                        total += totalData;
                    });
                    return total.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + ' VNĐ';
                }

                var revenueLineChartConfig = {
                    plugins: [{
                        beforeInit: function (chart, options) {
                            chart.legend.afterFit = function () {
                                this.height = this.height + 20;
                            };
                        }
                    }],
                    type: "bar",
                    options: revenueLineChartOptions,
                    data: revenueLineChartData
                };
                revenueLineChart = new Chart(revenueLineChartCTX, revenueLineChartConfig);
                totalRevenue[0].innerText = updateTotalRevenueAll(revenueLineChartData.datasets);


                function createColor(ctx, start, stop) {
                    var gradientColor = ctx.createLinearGradient(0, 0, 0, 600);
                    gradientColor.addColorStop(0, start);
                    gradientColor.addColorStop(1, stop);
                    return gradientColor;
                }

                var ctx = document.getElementById('donate-chart').getContext('2d');

                var countryRevenueChartCTX = $("#donate-chart");

                var hover_gradient = createColor(ctx, '#021925', '#0b444c');
                var countryRevenueChartOption = {
                    responsive: true,
                    // maintainAspectRatio: false,
                    plugins: {
                        datalabels: {
                            color: "#000",
                            anchor: "end",
                            align: "top",
                            offset: 1,
                            formatter: (value, ctx) => {
                                return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")
                            },
                            fontSize: 14
                        }
                    },
                    legend: {
                        display: false
                    },
                    title: {
                        display: true,
                        text: ['Biểu đồ tổng tiền '],
                        padding: '20',
                        fontSize: '16'
                    },
                    hover: {
                        mode: "label"
                    },
                    scales: {
                        xAxes: [
                            {
                                display: true,
                                scaleLabel: {
                                    display: true,
                                    labelString: 'Tiêu đề'
                                },
                                gridLines: {
                                    display: false
                                },
                                ticks: {
                                    fontColor: "#000"
                                }
                            }
                        ],
                        yAxes: [
                            {
                                display: true,
                                fontColor: "#000",
                                scaleLabel: {
                                    display: true,
                                    labelString: 'VNĐ'
                                },
                                gridLines: {
                                    display: false
                                },
                                ticks: {
                                    beginAtZero: true,
                                    fontColor: "#000",
                                    callback: function (value, index, values) {
                                        if (parseInt(value) >= 1000) {
                                            return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                        } else {
                                            return value;
                                        }
                                    }
                                }

                            }
                        ]
                    },
                    tooltips: {
                        titleFontSize: 0,
                        callbacks: {
                            label: function (tooltipItem, data) {
                                console.log(data);
                                console.log(tooltipItem)
                                if (parseInt(tooltipItem.yLabel) >= 1000) {
                                    return data.labels[tooltipItem.index] + ': ' + tooltipItem.yLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + ' VNĐ';
                                } else {
                                    return data.labels[tooltipItem.index] + ': ' + tooltipItem.yLabel + ' VNĐ';
                                }

                            }
                        }
                    }
                };
                var countryRevenueChartData = {
                    labels: [],
                    datasets: []
                };
                var dataCountry = $('#<%=hdfCountryRevenueChart.ClientID%>').val();
                if (dataCountry != null || dataCountry != "") {
                    var dataCountry2 = JSON.parse($('#<%=hdfCountryRevenueChart.ClientID%>').val());
                    countryRevenueChartData.labels = JSON.parse(dataCountry2.labels)
                    countryRevenueChartData.datasets = JSON.parse(dataCountry2.datasets);
                    countryRevenueChartData.datasets[0].hoverBackgroundColor = hover_gradient;
                    countryRevenueChartData.datasets[0].hoverBorderWidth = 2;
                    countryRevenueChartData.datasets[0].hoverBorderColor = hover_gradient;
                    console.log(countryRevenueChartData);

                }
                var countryRevenueChartConfig = {
                    type: "bar",
                    options: countryRevenueChartOption,
                    data: countryRevenueChartData
                };

                function updateTotalRevenue(data) {
                    const reducer = (accumulator, currentValue) => accumulator + currentValue;
                    var totalData = data.reduce(reducer).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                    return totalData + ' VNĐ';
                };
                var countryRevenueChart = new Chart(countryRevenueChartCTX, countryRevenueChartConfig);
            };
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
