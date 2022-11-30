<%@ Page Title="Quản lý bao hàng" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="Warehouse-Management.aspx.cs" Inherits="NHST.manager.Warehouse_Management" %>

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
                    <h4 class="title no-margin" style="display: inline-block;">Quản lý bao hàng</h4>
                </div>
            </div>
            <div class="list-staff col s12 section">
                <div class="list-table card-panel">
                    <div class="filter">
                        <div class="search-name input-field no-margin">
                            <asp:TextBox runat="server" placeholder="Nhập mã bao hàng" ID="search_name" type="text"></asp:TextBox>
                            <span class="material-icons search-action">search</span>
                            <asp:Button runat="server" ID="btnSearch" Style="display: none" OnClick="btnSearch_Click" />
                            <asp:Button ID="btnExcel" runat="server" OnClick="btnExcel_Click" Style="display: none" />
                        </div>
                        <asp:HyperLink runat="server" href="#addBadge" style="display:none" ID="hyperAdd" class="btn waves-effect modal-trigger mt-1">Thêm bao hàng</asp:HyperLink>
                    </div>
                    <div class="list-package-wrap  mt-2">
                        <div class="package-wrap accent-2">
                            <div class="row">
                                <div class="col s12">
                                    <div class="list-bag">
                                        <div class="responsive-tb">
                                            <table class="table highlight bordered ">
                                                <thead>
                                                    <tr>
                                                        <th>ID</th>
                                                        <th>Mã bao hàng</th>
                                                        <th>Cân nặng (kg)</th>
                                                        <th>Khối (m<sup>3</sup>)</th>
                                                        <th>Trạng thái</th>
                                                        <th>Ngày tạo</th>
                                                        <th style="width: 100px;">Action</th>
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
                    </div>
                </div>
            </div>
        </div>

    </div>
    <%--<div id="addBadge" class="modal add-package">
        <div class="modal-hd">
            <span class="right"><i class="material-icons modal-close right-align">clear</i></span>
            <h4 class="no-margin center-align">Thêm bao hàng mới</h4>
        </div>
        <div class="modal-bd">
            <div class="row">
                <div class="input-field col s12">
                    <asp:TextBox runat="server" ID="package_id" type="text" class="validate"></asp:TextBox>
                    <label for="kg_weight">Mã bao hàng</label>
                    <span class="error-info-show">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="package_id" Display="Dynamic"
                            ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </span>
                </div>
                <div class="input-field col s6">
                    <telerik:RadNumericTextBox runat="server" CssClass="validate" Skin="MetroTouch" BorderColor="Black"
                        ID="pWeight" MinValue="0" NumberFormat-DecimalDigits="2"
                        NumberFormat-GroupSizes="3" Width="100%" Value="0">
                    </telerik:RadNumericTextBox>
                    <label for="kg_weight" class="active">Cân nặng (kg)</label>
                </div>
                <div class="input-field col s6">
                    <telerik:RadNumericTextBox runat="server" CssClass="validate" Skin="MetroTouch" BorderColor="Black"
                        ID="pVolume" MinValue="0" NumberFormat-DecimalDigits="2"
                        NumberFormat-GroupSizes="3" Width="100%" Value="0">
                    </telerik:RadNumericTextBox>
                    <label for="m2_weigth" class="active">Khối (m<sup>3</sup>)</label>
                </div>
            </div>
        </div>
        <div class="modal-ft">
            <div class="ft-wrap center-align">
                <asp:Button ID="btncreateuser" OnClick="btncreateuser_Click" runat="server" class="modal-action btn white-text waves-green mr-2" Text="Thêm"></asp:Button>
                <a class="modal-action btn orange darken-2 modal-close waves-effect waves-green ml-2">Hủy</a>
            </div>
        </div>
    </div>--%>
    <asp:HiddenField ID="hdfExcel" runat="server" />
    
    <script src="assets/js/lightgallery/js/lightgallery-all.min.js" type="text/javascript"></script>
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
        $(document).ready(function () {
            $('.view-gallery').on('click', function () {
                var $this = $(this);
                var arr = [];
                var listImg = $this.parent().find('.list-img  img');
                console.log(listImg);
                $(listImg).each(function (i, element) {
                    var $src = $(element).attr('src');
                    arr[i] = {
                        src: $src,
                        thumb: $(element).attr('data-thumb') || $src
                    }
                });
                $this.lightGallery({
                    dynamic: true,
                    dynamicEl: arr,
                    download: false,
                    actualSize: false,
                    fullScreen: false,
                    autoplay: false,
                    share: false,
                    hideBarsDelay: 3000,
                });
            });

            $('.table-warehouse').on('click', '.select-trigger', function () {
                var content = $(this).parent().find('.dropdown-content');
                var dropDownTop = $(this).offset().top + $(this).outerHeight();
                content.css('top', dropDownTop + 'px');
                content.css('left', $(this).offset().left + 'px');
            });

            $('.bg-barcode').on('click', function () {
                alert('BarCode Open !');
            });

        });

        function XuatExcel(orderID) {
                $("#<%=hdfExcel.ClientID%>").val(orderID);
                $('#<%=btnExcel.ClientID%>').click();
        }
    </script>

</asp:Content>
