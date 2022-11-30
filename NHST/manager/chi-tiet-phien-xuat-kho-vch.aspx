<%@ Page Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="chi-tiet-phien-xuat-kho-vch.aspx.cs" Inherits="NHST.manager.chi_tiet_phien_xuat_kho_vch" %>

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
                    <h4 class="title no-margin" style="display: inline-block;">Chi tiết thanh toán xuất kho ký gửi<asp:Literal runat="server" ID="ltrIDS"></asp:Literal></h4>
                    <div class="right-action">
                    </div>
                </div>
            </div>
            <div class="list-staff col s12 m12 l12 xl9 section" id="customer-export">
                <div class="list-table">
                    <div class="row">
                        <div class="export-detail col s12">
                            <div class="card-panel">
                                <div class="row">
                                    <div class="col s12">
                                        <div class="row">
                                            <div class="input-field col s12 ">
                                                <h6>Tổng tiền thanh toán: <span class="font-weight-700">
                                                    <asp:Label runat="server" ID="txtTotalPrice1" Text="0"></asp:Label>
                                                    VNĐ</span></h6>
                                            </div>
                                            <div class="input-field col s12 m4">
                                                <asp:TextBox placeholder="" runat="server" ID="txtFullname" type="text" class="validate" value="Huỳnh Duy Khoa"></asp:TextBox>
                                                <label for="full_name">Họ tên người nhận</label>
                                            </div>
                                            <div class="input-field col s12 m4">
                                                <asp:TextBox placeholder="" runat="server" ID="txtPhone" type="text" class="validate" value="0987654321"></asp:TextBox>
                                                <label for="phone_number">Số điện thoại người nhận</label>
                                            </div>
                                            <div class="input-field col s12">
                                                <div class="action-wrap checkout">
                                                    <asp:Button ID="btncreateuser" runat="server" Text="Thanh toán bằng tiền mặt" Style="margin-right: 10px" CssClass="btn" OnClick="btncreateuser_Click" UseSubmitBehavior="false" />
                                                    <asp:Button ID="btnPayByWallet" runat="server" Text="Thanh toán bằng ví" Style="margin-right: 5px" CssClass="btn" OnClick="btnPayByWallet_Click" UseSubmitBehavior="false" />
                                                    <a href="/manager/danh-sach-phien-xuat-kho-ky-gui" class="btn ">Trở về</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col s12">
                                        <asp:Literal runat="server" ID="lrtListPackage"></asp:Literal>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <!-- END: Page Main-->

    <script src="assets/js/lightgallery/js/lightgallery-all.min.js" type="text/javascript"></script>
    <script>
        $(document).ready(function () {
            $('.bg-barcode').on('click', function () {
                alert('BarCode Open !');
            });
        })
        function getcode(obj) {
            var val = obj.val();
            //alert(val);
            val += ";";
            obj.val(val);
        }
        function VoucherSourcetoPrint(source) {
            var r = "<html><head><link rel=\"stylesheet\" href=\"/App_Themes/AdminNew/css/style.css\" type=\"text/css\"/><link rel=\"stylesheet\" href=\"/App_Themes/AdminNew/css/style-p.css\" type=\"text/css\"/><script>function step1(){\n" +
                "setTimeout('step2()', 10);}\n" +
                "function step2(){window.print();window.close()}\n" +
                "</scri" + "pt></head><body onload='step1()'>\n" +
                "" + source + "</body></html>";
            return r;
        }
        function VoucherPrint(source) {
            Pagelink = "about:blank";
            var pwa = window.open(Pagelink, "_new");
            pwa.document.open();
            pwa.document.write(VoucherSourcetoPrint(source));
            pwa.document.close();
        }
    </script>
</asp:Content>
