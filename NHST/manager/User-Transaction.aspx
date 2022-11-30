<%@ Page Title="Lịch sử giao dịch" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="User-Transaction.aspx.cs" Inherits="NHST.manager.User_Transaction" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Lịch sử giao dịch</h4>
                    <div class="right-action">
                        <a href="#" class="btn" id="filter-btn">Bộ lọc</a>
                    </div>
                    <div class="right-action" style="margin-right:20px">
                        <asp:Button runat="server"  class="btn" OnClick="btnExcel_Click" Text="Xuất Excel"></asp:Button>
                    </div>
                    <div class="clearfix"></div>

                    <div class="filter-wrap">
                        <div class="row mt-2 pt-2">
                            <div class="input-field col s6 l6">
                                <asp:TextBox runat="server" ID="rFD" type="text" class="datetimepicker from-date"></asp:TextBox>
                                <label>Từ ngày</label>
                            </div>
                            <div class="input-field col s6 l6">
                                <asp:TextBox runat="server" ID="rTD" type="text" class="datetimepicker to-date"></asp:TextBox>
                                <label>Đến ngày</label>
                                <span class="helper-text" data-error="Vui lòng chọn ngày bắt đầu trước"></span>
                            </div>
                             <div class="input-field col s6 l6">
                                <asp:ListBox runat="server" ID="status">
                                    <asp:ListItem Value="-1" Selected="true" Text="Tất cả"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Đặt cọc"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Nhận lại tiền đặt cọc"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Thanh toán hóa đơn"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Admin chuyển tiền"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="Rút tiền"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Hủy lệnh rút tiền"></asp:ListItem>
                                     <asp:ListItem Value="7" Text="Hoàn tiền khiếu nại"></asp:ListItem>
                                    <asp:ListItem Value="9" Text="Thanh toán hộ"></asp:ListItem>
                                </asp:ListBox>
                                <label for="status">Loại giao dịch</label>
                            </div>
                            <div class="col s12 right-align">
                                <asp:Button runat="server" ID="search" class="btn" OnClick="btnSearch_Click" Text="Lọc kết quả"></asp:Button>
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
                                Username: <span class="black-text font-weight-500">
                                    <asp:Literal runat="server" ID="ltrUsername"></asp:Literal></span>
                            </p>
                            <p class="">
                                Tổng số dư: <span class="black-text font-weight-500">
                                    <asp:Literal runat="server" ID="ltrTongSoDu"></asp:Literal></span>
                            </p>
                        </div>

                    </div>
                    <div class="responsive-tb mt-2">
                        <table class="table bordered highlight  ">
                            <thead>
                                <tr>
                                    <th style="min-width: 100px;">Ngày giờ</th>
                                    <th style="min-width: 100px;">Nội dung</th>
                                    <th style="min-width: 100px;">Số tiền</th>
                                    <th style="min-width: 100px;">Loại giao dịch</th>
                                    <th style="min-width: 100px;">Số dư</th>
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
            $('#txtSearchName').autocomplete({
                data: {
                    "Apple": null,
                    "Microsoft": null,
                    "Google": 'https://placehold.it/250x250',
                    "Asgard": null
                },
            });
        });
        function myFunction() {
            if (event.which == 13 || event.keyCode == 13) {
                $('#<%=search.ClientID%>').click();
            }
        }
        $('.search-action').click(function () {

            $('#<%=search.ClientID%>').click();
        })
    </script>
</asp:Content>
