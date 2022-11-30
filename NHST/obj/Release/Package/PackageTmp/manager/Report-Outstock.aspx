<%@ Page Title="Thống kê đơn mua hộ đã xuất kho" Language="C#" AutoEventWireup="true" CodeBehind="Report-Outstock.aspx.cs" MasterPageFile="~/manager/adminMasterNew.Master" Inherits="NHST.manager.Report_Outstock" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button Style="display: none" UseSubmitBehavior="false" ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Danh Sách Đơn Hàng Mua Hộ Đã Giao Khách</h4>
                    <div class="right-action">
                        <a href="#" class="btn" id="filter-btn">Bộ lọc</a>
                    </div>
                    <div class="clearfix"></div>
                    <div class="filter-wrap">
                        <div class="row mt-2 pt-2">
                            <div class="input-field col s12 l4">
                                <asp:TextBox ID="search_name" name="txtSearchName" type="text" onkeypress="myFunction()" runat="server" /><label>Tìm Username</label>
                            </div>
                            <div class="input-field col s12 l4">
                                <asp:TextBox ID="search_makh" name="txtSearchKH" type="text" onkeypress="myFunction()" runat="server" /><label>Tìm mã khách hàng</label>
                            </div>
                            <div class="input-field col s12 l4">
                                <asp:TextBox ID="search_madh" name="txtSearchDH" type="text" onkeypress="myFunction()" runat="server" /><label>Tìm mã đơn hàng</label>
                            </div>
                            <div class="input-field col s4">
                                <asp:DropDownList runat="server" ID="ddlStatus">
                                    <asp:ListItem Text="Tất cả" Value="-1"></asp:ListItem>
                                    <asp:ListItem Text="Chưa xử lý" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Đã xử lý" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                                <label for="status">Trạng thái</label>
                            </div>
                            <div class="input-field col s4">
                                <asp:TextBox runat="server" ID="rFD" type="text" class="datetimepicker from-date"></asp:TextBox>
                                <label>Từ ngày</label>
                            </div>
                            <div class="input-field col s4">
                                <asp:TextBox runat="server" ID="rTD" type="text" class="datetimepicker to-date"></asp:TextBox>
                                <label>Đến ngày</label>
                                <span class="helper-text" data-error="Vui lòng chọn ngày bắt đầu trước"></span>
                            </div>
                            <div class="col s12 right-align">
                                <span class="search-action btn">Lọc kết quả</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="list-staff col s12 section">
                <div class="list-table card-panel">
                    <div class="responsive-tb">
                        <table class="table bordered highlight mt-2">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Mã khách hàng</th>
                                    <th>Mã đơn hàng</th>
                                    <th>Username</th>
                                    <th>Tổng tiền</th>
                                    <th style="min-width: 100px;">Ngày tạo</th>
                                    <th style="min-width: 130px;">Trạng thái</th>
                                    <th style="min-width: 100px;">Thao tác</th>
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
        function myFunction() {
            if (event.which == 13 || event.keyCode == 13) {
                console.log($('#<%=search_name.ClientID%>').val());
                $('#<%=btnSearch.ClientID%>').click();
            }
        }
        $('.search-action').click(function () {
            console.log('dkm ngon');
            console.log($('#<%=search_name.ClientID%>').val());
            $('#<%=btnSearch.ClientID%>').click();
        })
    </script>
</asp:Content>
