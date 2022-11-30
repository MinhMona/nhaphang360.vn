<%@ Page Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="danh-sach-phien-xuat-kho-ky-gui.aspx.cs" Inherits="NHST.manager.danh_sach_phien_yeu_cau_xuat_kho_ky_gui" %>

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
                    <h4 class="title no-margin" style="display: inline-block;">Danh sách vận chuyển hộ</h4>
                    <div class="right-action">
                        <a href="#" class="btn" id="filter-btn">Bộ lọc</a>
                    </div>
                    <div class="clearfix"></div>
                    <div class="filter-wrap" style="display: none">
                        <div class="row mt-2 pt-2">


                            <div class="input-field col s6 l6">
                                <asp:TextBox runat="server" ID="rFD" type="text" class="datepicker from-date" placeholder=""></asp:TextBox>
                                <label class="active">Từ ngày</label>
                            </div>
                            <div class="input-field col s6 l6">
                                <asp:TextBox runat="server" ID="rTD" type="text" class="datepicker to-date" placeholder=""></asp:TextBox>
                                <label class="active">Đến ngày</label>
                                <span class="helper-text" data-error="Vui lòng chọn ngày bắt đầu trước"></span>
                            </div>

                            <div class="input-field col s12 l6">
                                <asp:ListBox runat="server" ID="status">
                                    <asp:ListItem Value="-1" Selected="true" Text="Tất cả"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Đơn hàng mới"></asp:ListItem>
                                    <%--<asp:ListItem Value="2" Text="Đã duyệt"></asp:ListItem>--%>
                                    <asp:ListItem Value="3" Text="Đã về kho TQ"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="Đã về kho đích"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="Đã thanh toán"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="Đã nhận hàng"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Đơn hàng hủy"></asp:ListItem>
                                </asp:ListBox>
                                <label for="status">Trạng thái</label>
                            </div>

                            <div class="input-field col s6 left-align">
                                <asp:Button ID="search" runat="server" OnClick="btnSearch_Click" class="btn " Text="Lọc kết quả"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="list-staff col s12 section">
                <div class="list-table card-panel">
                    <%--  <div class="table-info row center-align-xs">
                        <span class="checkout col s12 m6">Tổng cân nặng : <span class="font-weight-700 black-text">300 kg</span>
                        </span>
                    </div>--%>
                    <div class="responsive-tb">
                        <table class="table  highlight bordered   mt-2">
                            <thead>
                                <tr>
                                    <th style="min-width: 100px;">ID</th>
                                    <th style="min-width: 100px;">Username</th>
                                    <th style="min-width: 100px;">Số kiện</th>
                                    <th style="min-width: 100px;">Tổng cân nặng</th>
                                    <th style="min-width: 100px;">Tổng tiền</th>
                                    <th style="min-width: 100px;">Hình thức VC</th>
                                    <th style="min-width: 130px;">Ghi chú</th>
                                    <th style="min-width: 130px;">Trạng thái thanh toán</th>
                                    <th style="min-width: 130px;">Trạng thái xuất kho</th>
                                    <th style="min-width: 100px;">Ngày yêu cầu xuất kho</th>
                                    <th style="min-width: 130px;">Ngày xuất kho</th>
                                    <th style="min-width: 120px;">Thao tác</th>
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
