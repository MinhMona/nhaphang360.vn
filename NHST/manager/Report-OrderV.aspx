<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/manager/adminMasterNew.Master" CodeBehind="Report-OrderV.aspx.cs" Inherits="NHST.manager.Report_OrderV" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Thống kê doanh thu</h4>
                    <div class="clearfix"></div>
                </div>
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

                                        <div class="input-field col s12 l2">
                                            <asp:DropDownList runat="server" ID="ddlType">
                                                <asp:ListItem Value="0" Selected="True">Tất cả</asp:ListItem>
                                                <asp:ListItem Value="1">Username khách</asp:ListItem>
                                                <asp:ListItem Value="2">Username sale</asp:ListItem>
                                                <asp:ListItem Value="3">Username đặt hàng</asp:ListItem>
                                            </asp:DropDownList>
                                            <label for="select_by">Tìm kiếm theo</label>
                                        </div>

                                        <div class="input-field col s6 m4 l4">
                                            <asp:TextBox runat="server" type="text" ID="tSearchName" onkeypress="myFunction()"></asp:TextBox>
                                            <label>Nhập tìm kiếm</label>
                                        </div>

                                        <div class="input-field col s6 m4 l2">
                                            <asp:TextBox runat="server" type="text" ID="rdatefrom" class="datetimepicker from-date"></asp:TextBox>
                                            <label>Từ ngày</label>
                                        </div>

                                        <div class="input-field col s6 m4 l2">
                                            <asp:TextBox runat="server" ID="rdateto" type="text" class="datetimepicker to-date"></asp:TextBox>
                                            <label>Đến ngày</label>
                                            <span class="helper-text"
                                                data-error="Vui lòng chọn ngày bắt đầu trước"></span>
                                        </div>

                                        <div class="input-field col s12 m4 l2">
                                            <a href="javascript:;" class="btn xemthongke">Lọc thống kê</a>
                                            <a href="javascript:;" class="btn xuatexcel" style="background-color: green; margin-left: 10px">Xuất Excel</a>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row section">
                    <div class="col s12 report-list">
                        <div class="ad-table-report responsive-tb">
                            <table class="table tb-border  highlight striped   ">
                                <thead>
                                    <tr>
                                        <th>Tiêu đề</th>
                                        <th>Tất cả</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>Tổng tiền ship Trung Quốc</td>
                                        <td class="no-wrap">
                                            <div style="justify-content: space-between">
                                                <span class=" font-weight-400">
                                                    <asp:Label ID="lblFeeShipCN" runat="server"></asp:Label>
                                                    <%--<asp:Button Style="display: none" ID="btnExcelFeeShipCN" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelFeeShipCN_Click" />--%>
                                                </span>                                               
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Tổng tiền phí mua hàng</td>
                                        <td class="no-wrap">
                                            <div style="justify-content: space-between">
                                                <span class=" font-weight-400">
                                                    <asp:Label ID="lblFeeBuyPro" runat="server"></asp:Label>
                                                    <%--<asp:Button Style="display: none" ID="btnExcelFeeShipCN" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelFeeShipCN_Click" />--%>
                                                </span>                                                
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Tổng tiền phí cân nặng</td>
                                        <td class="no-wrap">
                                            <div style="justify-content: space-between">
                                                <span class=" font-weight-400">
                                                    <asp:Label ID="lblFeeWeight" runat="server"></asp:Label>
                                                    <%--<asp:Button Style="display: none" ID="btnExcelFeeShipCN" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelFeeShipCN_Click" />--%>
                                                </span>                                               
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Tổng tiền phí kiểm hàng</td>
                                        <td class="no-wrap">
                                            <div style="justify-content: space-between">
                                                <span class=" font-weight-400">
                                                    <asp:Label ID="lblFeeCheck" runat="server"></asp:Label>
                                                    <%--<asp:Button Style="display: none" ID="btnExcelFeeShipCN" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelFeeShipCN_Click" />--%>
                                                </span>                                                
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Tổng tiền phí đóng gỗ</td>
                                        <td class="no-wrap">
                                            <div style="justify-content: space-between">
                                                <span class=" font-weight-400">
                                                    <asp:Label ID="lblFeePacked" runat="server"></asp:Label>
                                                    <%--<asp:Button Style="display: none" ID="btnExcelFeeShipCN" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelFeeShipCN_Click" />--%>
                                                </span>                                               
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Tổng tiền phí ship tận nhà</td>
                                        <td class="no-wrap">
                                            <div style="justify-content: space-between">
                                                <span class=" font-weight-400">
                                                    <asp:Label ID="lblFeeShiphome" runat="server"></asp:Label>
                                                    <%--<asp:Button Style="display: none" ID="btnExcelFeeShipCN" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelFeeShipCN_Click" />--%>
                                                </span>                                               
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Tổng tiền đã đặt cọc</td>
                                        <td class="no-wrap">
                                            <div style="justify-content: space-between">
                                                <span class=" font-weight-400">
                                                    <asp:Label ID="lblDeposit" runat="server"></asp:Label>
                                                    <%--<asp:Button Style="display: none" ID="btnExcelFeeShipCN" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelFeeShipCN_Click" />--%>
                                                </span>                                                
                                            </div>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>Tổng tiền mua thật</td>
                                        <td class="no-wrap">
                                            <div style="justify-content: space-between">
                                                <span class=" font-weight-400">
                                                    <asp:Label ID="lblFeeReal" runat="server"></asp:Label>
                                                    <%--<asp:Button Style="display: none" ID="btnExcelFeeShipCN" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelFeeShipCN_Click" />--%>
                                                </span>                                                
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Tổng tiền đơn đã mua hàng</td>
                                        <td class="no-wrap">
                                            <div style="justify-content: space-between">
                                                <span class=" font-weight-400">
                                                    <asp:Label ID="lblDamuahang" runat="server"></asp:Label>
                                                    <%--<asp:Button Style="display: none" ID="btnExcelFeeShipCN" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelFeeShipCN_Click" />--%>
                                                </span>                                                
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Tổng tiền đơn đã hoàn thành</td>
                                        <td class="no-wrap">
                                            <div style="justify-content: space-between">
                                                <span class=" font-weight-400">
                                                    <asp:Label ID="lblDahoanthanh" runat="server"></asp:Label>
                                                    <%--<asp:Button Style="display: none" ID="btnExcelFeeShipCN" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelFeeShipCN_Click" />--%>
                                                </span>                                                
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Tổng tiền tất cả đơn</td>
                                        <td class="no-wrap">
                                            <div style="justify-content: space-between">
                                                <span class=" font-weight-400">
                                                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                                    <%--<asp:Button Style="display: none" ID="btnExcelFeeShipCN" runat="server" CssClass="btn btn-success" Text="Xuất Excel" OnClick="btnExcelFeeShipCN_Click" />--%>
                                                </span>                                               
                                            </div>
                                        </td>
                                    </tr>

                                </tbody>
                            </table>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <div class="list-staff col s12 section">
            <div class="list-table card-panel">
                <div class="responsive-tb">
                    <table class="table bordered highlight striped ">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Username</th>
                                <th>NV Sales</th>
                                <th>NV Đặt hàng</th>
                                <th>Tổng tiền</th>
                                <th>Đã trả</th>
                                <th>Còn lại</th>
                                <th>Tiền hàng web</th>
                                <th>Phí dịch vụ</th>
                                <th>Phí ship TQ</th>
                                <th>Phí cân nặng</th>
                                <th>Phí kiểm hàng</th>
                                <th>Phí đóng gỗ</th>
                                <th>Phí bảo hiểm</th>
                                <th>Phí giao hàng </br> tận nhà</th>
                                <th>Phụ phí</th>
                                <th>Tiền mua thật</th>
                                <th>Trạng thái</th>
                                <th>Ngày tạo</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
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
    <asp:Button Style="display: none" UseSubmitBehavior="false" ID="btnFilter" runat="server" OnClick="btnFilter_Click" />
    <asp:Button Style="display: none" UseSubmitBehavior="false" ID="btnExcel" runat="server" OnClick="btnExcel_Click" />
    <script type="text/javascript">
        $('.xemthongke').click(function () {

            $('#<%=btnFilter.ClientID%>').click();
        })
        $('.xuatexcel').click(function () {

            $('#<%=btnExcel.ClientID%>').click();
        })
    </script>
</asp:Content>
