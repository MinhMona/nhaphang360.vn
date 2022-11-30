<%@ Page Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="request-outstock-vch.aspx.cs" Inherits="NHST.manager.request_outstock_vch" %>

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
                                <asp:TextBox runat="server" ID="tSearchName" type="text" class="" placeholder=""></asp:TextBox>
                                <label class="active">Từ ngày</label>
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
                                    <th style="min-width: 100px;">Mã vận đơn</th>
                                    <th style="min-width: 100px;">Cân nặng</th>

                                    <th style="min-width: 100px;">Mã đơn hàng VC hộ</th>
                                    <th style="min-width: 100px;">Tài khoản khách hàng</th>
                                    <th style="min-width: 100px;">Ngày về kho đích</th>
                                    <th style="min-width: 130px;">Ngày xuất kho</th>
                                    <th style="min-width: 130px;">Trạng thái</th>
                                    <th style="min-width: 130px;">Ngày yêu cầu XK</th>
                                    <th style="min-width: 130px;">HTVC</th>
                                    <th style="min-width: 120px;">Ghi chú</th>
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

          function Updatestatus(obj, id) {
                add_loading();
                $.ajax({
                    type: "POST",
                    url: "/manager/request-outstock-vch.aspx/updateStatus",
                    data: "{ID:'" + id + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var parent = obj.parent().parent();
                        var stt = parent.find(".statusre").removeClass("bg-red").addClass("bg-blue").html("Đã xuất");
                        obj.remove();
                        remove_loading();
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        //alert('lỗi checkend');
                    }
                });
            }

    </script>
</asp:Content>
