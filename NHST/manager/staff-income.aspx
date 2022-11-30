<%@ Page Title="Quản lý hoa hồng" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="staff-income.aspx.cs" Inherits="NHST.manager.staff_income" %>

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
                    <h4 class="title no-margin" style="display: inline-block;">Quản lý hoa hồng</h4>
                    <div class="right-action">
                        <a href="#" class="btn" id="filter-btn">Bộ lọc</a>
                    </div>
                    <div class="clearfix"></div>
                    <div class="filter-wrap">
                        <div class="row mt-2 pt-2">
                            <div class="input-field col s12 l2">
                                <asp:ListBox runat="server" id="select_by">
                                    <asp:ListItem value="0" Selected="True">Tất cả</asp:ListItem>
                                    <asp:ListItem  value="1">Chưa thanh toán</asp:ListItem>
                                    <asp:ListItem  value="2">Đã thanh toán</asp:ListItem>
                                </asp:ListBox>
                                <label for="select_by">Trạng thái</label>
                            </div>

                            <div class="input-field col s12 l4">
                                <asp:TextBox runat="server" id="search_name" type="text" class="validate autocomplete"></asp:TextBox>
                                <label for="search_name">Username</label>
                            </div>

                            <div class="input-field col s6 l3">
                                <asp:TextBox runat="server" id="rFD" type="text" class="datepicker from-date"></asp:TextBox>
                                <label>Từ ngày</label>
                            </div>
                            <div class="input-field col s6 l3">
                                <asp:TextBox runat="server" ID="rTD" type="text" class="datepicker to-date"></asp:TextBox>
                                <label>Đến ngày</label>
                                <span class="helper-text" data-error="Vui lòng chọn ngày bắt đầu trước"></span>
                            </div>
                            <div class="col s12 right-align">
                                <asp:Button runat="server" class="btn" id="search" OnClick="search_Click" Text="Lọc kết quả"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="list-commission col s12 section">
                <div class="list-table card-panel">
                    <div class="table-info row center-align-xs">
                        <div class="checkout col s12 m6">
                            <p class="">
                                Tổng tiền đã thanh toán: 
                                <asp:Literal runat="server" ID="ltrTienDaThanhToan"></asp:Literal>
                            </p>
                            <p class="">
                                Tổng tiền chưa thanh toán:
                                <asp:Literal runat="server" ID="ltrTienChuaThanhToan"></asp:Literal>
                            </p>
                        </div>
                    </div>

                    <div class="responsive-tb mt-2">
                        <table class="table bordered highlight  ">
                            <thead>
                            <tr>
                                <th style=" min-width: 100px;">Mã đơn hàng</th>
                                <th style=" min-width: 100px;">Phần trăm</th>
                                <th style=" min-width: 100px;">Hoa hồng</th>
                                <th style=" min-width: 100px;">Username</th>
                                <th style=" min-width: 100px;">Quyền hạn</th>
                                <th style=" min-width: 130px;">Trạng thái</th>
                                <th style=" min-width: 120px;">Ngày giờ</th>
                                <th style=" min-width: 100px;">Thao tác</th>
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
    </div>
    <script>
        $(document).ready(function () {
            $('#search_name').autocomplete({
                data: {
                    "Apple": null,
                    "Microsoft": null,
                    "Google": 'https://placehold.it/250x250',
                    "Asgard": null
                },
            });
        });
    </script>
</asp:Content>
