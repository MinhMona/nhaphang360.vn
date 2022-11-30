<%@ Page Title="Chi tiết thanh toán xuất kho" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="view-outstock-session.aspx.cs" Inherits="NHST.manager.view_outstock_session" %>

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
                <h4 class="title no-margin" style="display:inline-block;">Chi tiết thanh toán xuất kho<asp:Literal runat="server" ID="ltrIDS"></asp:Literal></h4>
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
                                            <h6>Tổng tiền thanh toán: <span class="font-weight-700"><asp:Label runat="server" id="txtTotalPrice1" Text="0"></asp:Label> VNĐ</span></h6>
                                        </div>
                                        <div class="input-field col s12 m4">
                                            <asp:TextBox placeholder="" runat="server" id="txtFullname" type="text" class="validate" value="Huỳnh Duy Khoa"></asp:TextBox>
                                            <label for="full_name">Họ tên người nhận</label>
                                        </div>
                                        <div class="input-field col s12 m4">
                                            <asp:TextBox placeholder="" runat="server" id="txtPhone" type="text" class="validate" value="0987654321"></asp:TextBox>
                                            <label for="phone_number">Số điện thoại người nhận</label>
                                        </div>
                                        <div class="input-field col s12">
                                            <div class="action-wrap checkout">                                              
                                                <asp:Button ID="btncreateuser" runat="server" Text="Thanh toán bằng tiền mặt" style="margin-right:10px" CssClass="btn" OnClick="btncreateuser_Click" UseSubmitBehavior="false" />
                                                <asp:Button ID="btnPayByWallet" runat="server" Text="Thanh toán bằng ví" style="margin-right:5px" CssClass="btn" OnClick="btnPayByWallet_Click" UseSubmitBehavior="false" />                                        
                                                <a href="/manager/accountant-outstock-payment" class="btn ">Trở về</a>
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
        <div class="print-section" id="customer-export-print">
            <div class="print-header">
                <div class="row">
                    <div class="col s6">
                        <div class="hd-left">
                            <div class="content">
                                <p class="name">Heaven Order</p>
                                <p class="address">Địa chỉ: 169/ Phạm Văn Đồng, TP. Hồ Chí Minh</p>
                            </div>

                        </div>
                    </div>
                    <div class="col s6">
                        <div class="hd-right">
                            <div class="content">
                                <p>Mẫu số 01 - TT</p>
                                <p>Ban hành theo thông tư số 113/2019 - BTC ngày 26/8/2016 của Bộ Tài
                                    Chính</p>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col s12">
                        <div class="print-title">
                            <h1>Phiếu Xuất Kho</h1>
                            <p>Ngày 11 tháng 6 năm 2019</p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="print-content">
                <div class="content-wrap">
                    <div class="row-dots">
                        <p>Họ và tên người đến nhận: <span>Nguyễn Hường Ly</span></p>
                        <p>Số điện thoại người nhận: <span>0123456789</span></p>
                    </div>

                    <div class="responsive-tb package-item">
                        <p class="owner">Đơn hàng mua hộ #2535</p>
                        <table class="table   bordered list-item-package">
                            <thead>
                                <tr>
                                    <th>Ảnh sản phẩm</th>
                                    <th>Số lượng</th>
                                    <th>Giá NDT</th>
                                    <th>Giá VNĐ</th>
                                    <th>Thuộc tính</th>
                                    <th>Ghi chú</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <img src="assets/images/avatar/avatar-1.png" alt="image" class="materialboxed">
                                    </td>
                                    <td><span>2</span></td>
                                    <td>¥ <span>78</span></td>
                                    <td><span>1,200,000</span> VNĐ</td>
                                    <td>M(200) cái quần đùi màu xanh x2 ML</td>
                                    <td>Hàng TQ, đã chuyển khoản</td>
                                </tr>

                                <tr>
                                    <td>
                                        <img src="assets/images/avatar/avatar-1.png" alt="image" class="materialboxed">
                                    </td>
                                    <td><span>2</span></td>
                                    <td>¥ <span>78</span></td>
                                    <td><span>1,200,000</span> VNĐ</td>
                                    <td>M(200) cái quần đùi màu xanh x2 ML</td>
                                    <td>Hàng TQ, đã chuyển khoản</td>
                                </tr>
                                <tr>
                                    <td>
                                        <img src="assets/images/avatar/avatar-1.png" alt="image" class="materialboxed">
                                    </td>
                                    <td><span>2</span></td>
                                    <td>¥ <span>78</span></td>
                                    <td><span>1,200,000</span> VNĐ</td>
                                    <td>M(200) cái quần đùi màu xanh x2 ML</td>
                                    <td>Hàng TQ, đã chuyển khoản</td>
                                </tr>
                                <tr>
                                    <td>
                                        <img src="assets/images/avatar/avatar-1.png" alt="image" class="materialboxed">
                                    </td>
                                    <td><span>2</span></td>
                                    <td>¥ <span>78</span></td>
                                    <td><span>1,200,000</span> VNĐ</td>
                                    <td>M(200) cái quần đùi màu xanh x2 ML</td>
                                    <td>Hàng TQ, đã chuyển khoản</td>
                                </tr>




                            </tbody>
                        </table>
                    </div>
                    <div class="package-item">
                        <table class="table   bordered ">
                            <thead>
                                <tr class="teal darken-4">
                                    <th>Mã kiện</th>
                                    <th>Cân nặng (kg)</th>
                                    <th>Ngày lưu kho (Ngày)</th>
                                    <th>Trạng thái</th>
                                    <th>Tiền lưu kho</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td><span>123412341234</span></td>
                                    <td><span>120</span></td>
                                    <td><span>20</span></td>
                                    <td> <span class="white-text badge teal darken-2">Đã về kho
                                            đích</span></td>
                                    <td>8.000.000 VNĐ</td>
                                </tr>
                                <tr>
                                    <td><span>123123422</span></td>
                                    <td><span>220</span></td>
                                    <td><span>50</span></td>
                                    <td> <span class="white-text badge teal darken-2">Đã về kho
                                            đích</span></td>
                                    <td>12.000.000 VNĐ</td>
                                </tr>
                                <tr>
                                    <td colspan="4"><span class="black-text font-weight-500">Tiền hàng</span></td>
                                    <td><span class="black-text">6.000.000 VNĐ</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4"><span class="black-text font-weight-500">Ship nội địa</span></td>
                                    <td><span class="black-text">20.000 VNĐ</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4"><span class="black-text font-weight-500">Phí mua hàng
                                        </span></td>
                                    <td><span class="red-text">500,000 VNĐ</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4"><span class="black-text font-weight-500">Phí tùy chọn
                                        </span></td>
                                    <td><span class="red-text">50,000 VNĐ</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4"><span class="black-text font-weight-500">Phí VC TQ-VN
                                        </span></td>
                                    <td><span class="red-text">5,000 VNĐ</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4"><span class="black-text font-weight-700">Tổng tiền
                                            đơn hàng</span></td>
                                    <td><span class="red-text font-weight-700">15.520.000 VNĐ</span>
                                    </td>
                                </tr>
                            </tbody>

                        </table>
                    </div>
                    <div class="package-item">
                        <table class="table bordered  ">
                            <thead>
                                <tr>
                                    <th style="max-width:300px;">
                                        Username
                                    </th>
                                    <th>Nội dung</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>chipcop106</td>
                                    <td>
                                        <span class="time">[ 16/04/2019 10:30 AM ] </span><span
                                            class="usr-content">Lorem ipsum dolor sit amet consectetur adipisicing elit.
                                            Atque, esse dolorum officia unde aut vero nisi similique provident ipsa
                                            accusantium amet soluta nostrum sequi ad id ut reiciendis temporibus
                                            modi.</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>chipcop106</td>
                                    <td>
                                        <span class="time">[ 16/04/2019 10:30 AM ] </span><span
                                            class="usr-content">Lorem ipsum dolor sit amet consectetur adipisicing elit.
                                            Atque, esse dolorum officia unde aut vero nisi similique provident ipsa
                                        </span>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div class="print-footer">
                <div class="signature">
                    <div class="col">
                        <p class="bold">Người xuất hàng</p>
                        <p>(Ký,họ tên)</p>
                    </div>
                    <div class="col">
                        <p class="bold">Người nhận hàng</p>
                        <p>(Ký,họ tên)</p>
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
