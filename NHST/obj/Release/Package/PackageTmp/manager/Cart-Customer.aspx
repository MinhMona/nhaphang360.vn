<%@ Page Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="Cart-Customer.aspx.cs" Inherits="NHST.manager.Cart_Customer" %>

<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="main">
        <div class="row">
            <div class="content-wrapper-before blue-grey lighten-5"></div>
            <div class="col s12">
                <div class="container">
                    <div class="all">
                        <div class="card-panel mt-3 no-shadow">
                            <div class="row">
                                <div class="col s12">
                                    <div class="page-title mt-2 center-align">
                                       <%-- <h4>Giỏ hàng</h4>--%>
                                        <asp:Literal runat="server" ID="ltrGiohang"></asp:Literal>
                                    </div>
                                </div>
                                <div class="col s12 order-info">
                                    <div class="step-wrap hide-on-med-and-down">
                                        <ul class="process-list">
                                            <li class="process-item active"><i class="material-icons">shopping_cart</i>
                                                <p>GIỎ HÀNG</p>
                                            </li>
                                            <li class="process-item"><i class="material-icons">map</i>
                                                <p>CHỌN ĐỊA CHỈ NHẬN HÀNG</p>
                                            </li>
                                            <li class="process-item"><i class="material-icons">attach_money</i>
                                                <p>ĐẶT CỌC VÀ KẾT ĐƠN</p>
                                            </li>
                                        </ul>
                                    </div>
                                    <div class="order-notice">
                                        <div class="notice-wrap grey lighten-4">
                                            <p>Sản phẩm trong giỏ sẽ tự động xóa trong vòng 30 ngày. Người bán trên website 1688.com thường có quy định về số lượng mua tối thiểu, bội số mỗi sản phẩm, giá trị đơn hàng tối thiểu và sẽ từ chối bán nếu không đáp ứng. Trong trường hợp đó <asp:Literal runat="server" ID="ltrTitle"></asp:Literal> sẽ hủy những đơn hàng này và không báo trước.</p>
                                        </div>
                                    </div>
                                </div>
                                <div class="col s12">

                                    <asp:Literal runat="server" ID="ltrNotProduct"></asp:Literal>

                                    <div class="total-info mb-2">
                                        <asp:Literal runat="server" ID="ltr_sub"></asp:Literal>

                                    </div>
                                    <div class="list-order">

                                        <div class="order-item-wrap">

                                            <asp:Literal ID="ltr_cart" runat="server"></asp:Literal>

                                        </div>
                                        <div class="total-checkout">
                                            <asp:Literal runat="server" ID="ltr_total_pay"></asp:Literal>
                                            <%-- <div class="total-wrap right-align">
                                                <div class="total-all">
                                                    <strong class="black-text">Tổng tiền các đơn đã chọn:</strong>
                                                    <strong class="price-all red-text">5,600,000đ</strong>
                                                </div>
                                                <div class="total-all">
                                                    <strong class="black-text">Tổng tiền tất cả đơn hàng:</strong>
                                                    <strong class="price-all red-text">5,600,000đ</strong>
                                                </div>
                                                <div class="checkout-all">
                                                    <a href="javascript:;" class="btn checkout-select mr-1">Đặt hàng đơn đã chọn (2)</a>
                                                    <a href="javascript:;" class="btn checkout-all">Đặt hàng tất cả đơn</a>
                                                </div>
                                            </div>--%>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>

    <asp:Button ID="checkoutallorder" runat="server" Text="chaeckdall" OnClick="checkoutallorder_Click" Style="display: none" CausesValidation="false" />
    <asp:Button ID="checkout1order" runat="server" Text="chaeckdall" OnClick="checkout1order_Click" Style="display: none" CausesValidation="false" />
    <asp:HiddenField ID="hdforderlistall" runat="server" />
    <asp:HiddenField ID="hdfallnote" runat="server" />
    <asp:HiddenField ID="hdfallorderid" runat="server" />
    <asp:HiddenField ID="hdfProductBrand" runat="server" />
    <asp:HiddenField ID="hdfListOrderTempID" runat="server" />
    <script type="text/javascript">

        //      $(".up-downControl").each(function() {
        //  var $this = $(this);
        //  var step = parseInt($this.attr("data-step"));
        //  var min = parseInt($this.attr("data-min"));
        //  var max = parseInt($this.attr("data-max"));

        //  $this.find(".btn").on("click", function(e) {
        //    e.preventDefault();
        //    var value = parseInt($this.find(".value").val());
        //    if ($(this).hasClass("minus")) {
        //      value = value - step;
        //      if (value < min) return;
        //    }
        //    if ($(this).hasClass("plus")) {
        //      value = value + step;
        //      if (value > max) return;
        //    }
        //    console.log(value);
        //    $this.find(".value").val(value);
        //  });
        //});

        $(document).ready(function () {
            $('.list-order').on('change', '.select-checkout input[type="checkbox"]', function () {
                var $this = $(this);
                if ($this.prop('checked')) {
                    $this.parents('.order-item').css('border', '1.5px solid #000');
                    $this.next().find('.checked').css('display', 'inline-block');
                }
                else {
                    $this.parents('.order-item').css({
                        'border': 'none'
                    });
                    $this.next().find('.checked').css('display', 'none');
                }

            });
        });

        function updatequantity1(ID, obj, shopid) {
            var quantity = obj.val();
            if (quantity == 0) {
                deleteordertemp(ID, shopid);
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/manager/Cart-Customer.aspx/UpdateQuantityOrderTemp",
                    data: "{ID:'" + ID + "',quantity:'" + quantity + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == "1") {
                            location.reload();
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        // alert('lỗi');
                    }
                });
            }

        }
        function updatequantity(ID, obj, shopid) {
            var quantity = obj.parent().parent().find(".quantitiofpro").val();
            var brand = obj.parent().parent().parent().find(".notebrand").val();
            if (quantity == 0) {
                deleteordertemp(ID, shopid);
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/manager/Cart-Customer.aspx/UpdateQuantityOrderTemp",
                    data: "{ID:'" + ID + "',quantity:'" + quantity + "',brand:'" + brand + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == "1") {
                            location.reload();
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        // alert('lỗi');
                    }
                });
            }

        }
        function deleteordertemp(ID, shopid) {
            var r = confirm("Bạn muốn xóa sản phẩm này?");
            if (r == true) {
                $.ajax({
                    type: "POST",
                    url: "/manager/Cart-Customer.aspx/deleteOrderTemp",
                    data: "{ID:'" + ID + "',shopID:'" + shopid + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == "1") {
                            location.reload();
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        //alert('lỗi');
                    }
                });
            }
            else {
            }
        }
        function deleteordershoptemp(ID) {
            var r = confirm("Bạn muốn xóa cửa hàng này và tất cả sản phẩm hiện có ?");
            if (r == true) {
                $.ajax({
                    type: "POST",
                    url: "/manager/Cart-Customer.aspx/deleteOrderShopTemp",
                    data: "{ID:'" + ID + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == "1") {
                            location.reload();
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        //alert('lỗi');
                    }
                });
            }
            else {
            }
        }

        function updatecheck(obj, ID, fieldUpdate) {
            $("body").append("<div class=\"loading\"></div>");
            var chk = obj.is(':checked');
            $.ajax({
                type: "POST",
                url: "/manager/Cart-Customer.aspx/UpdateField",
                data: "{ID:'" + ID + "',chk:'" + chk + "',field:'" + fieldUpdate + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var ret = msg.d;
                    if (ret == 1) {

                    }
                    else if (ret == "wronguser") {
                        alert("Không đúng tài khoản.");
                    }
                    else if (ret == "null") {
                        alert("Không tồn tại.");
                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    //alert(errorthrow);
                }
            });
            $(".loading").remove();
        }

        function checkoutAll(listorderid) {
            $("#<%=hdfListOrderTempID.ClientID%>").val(listorderid);
             var striplist = listorderid.split('|');
             var html = "";
             var brandpro = "";
             if (striplist.length > 0) {
                 for (var i = 0; i < striplist.length - 1; i++) {

                     var ID = striplist[i];
                     var objo = $("#" + ID + "_checkproductselect");
                     //updatecheck(objo, ID, "IsCheckProduct");
                     //var chk_isfast = $("#chk_fast_" + ID + "").is(':checked');

                     var note = $("#order_temp_" + ID + "").val();
                     var priceVND = $("#priceVND_" + ID + "").attr("data-price");

                     if (note == null || note == "") {
                         note = "";
                     }
                     if (isEmpty(priceVND)) {
                         alert('lỗi priceVND');
                         return;
                     }
                     html += ID + "," + note + "," + priceVND + "|";
                 }
                 $(".brand-name-product").each(function () {
                     var idpro = $(this).find(".notebrand").attr("data-item-id");
                     var brandtext = $(this).find(".notebrand").val();
                     brandpro += idpro + "," + brandtext + "|";
                 });
                 $("#<%= hdfProductBrand.ClientID %>").val(brandpro);
                $("#<%= hdforderlistall.ClientID %>").val(html);
                $("#<%= checkoutallorder.ClientID %>").click();
                //window.location.href = "/thanh-toan/all";
            }
        }
        function checkoutAllSelect() {
            var listorderid = $("#<%=hdfListOrderTempID.ClientID%>").val();
            var striplist = listorderid.split('|');
            var html = "";
            var brandpro = "";
            if (striplist.length - 1 > 0) {
                for (var i = 0; i < striplist.length - 1; i++) {

                    var ID = striplist[i];
                    var objo = $("#" + ID + "_checkproductselect");
                    //updatecheck(objo, ID, "IsCheckProduct");
                    //var chk_isfast = $("#chk_fast_" + ID + "").is(':checked');

                    var note = $("#order_temp_" + ID + "").val();
                    var priceVND = $("#priceVND_" + ID + "").attr("data-price");

                    if (note == null || note == "") {
                        note = "";
                    }
                    if (isEmpty(priceVND)) {
                        alert('lỗi priceVND');
                        return;
                    }
                    html += ID + "," + note + "," + priceVND + "|";
                }

                $(".brand-name-product").each(function () {
                    var idpro = $(this).find(".notebrand").attr("data-item-id");
                    var brandtext = $(this).find(".notebrand").val();
                    brandpro += idpro + "," + brandtext + "|";
                });
                $("#<%= hdfProductBrand.ClientID %>").val(brandpro);
                $("#<%= hdforderlistall.ClientID %>").val(html);
                $("#<%= checkoutallorder.ClientID %>").click();
            }
        }

        function isEmpty(str) {
            return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
        }

        function getCartToSuccess(obj) {

            var countOrderCheck = 0;
            var list = "";
            if (obj.is(":checked")) {
                obj.parent().parent().addClass("select");
            }
            else {
                obj.parent().parent().removeClass("select");
            }
            $(".shop-cart").each(function () {
                if ($(this).hasClass("select")) {
                    countOrderCheck += 1;
                    list += $(this).attr("data-item-id") + "|";
                }
            });
            $("#<%=hdfListOrderTempID.ClientID%>").val(list);
            if (countOrderCheck > 0) {
                $(".getallOrder").attr("style", "display:inline-block;").find(".numofOrder").html(countOrderCheck);
            }
            else {
                $(".getallOrder").removeAttr("style").hide().find(".numofOrder").html(countOrderCheck);
            }
        }

        $(document).ready(function () {
            var countOrderCheck = 0;
            var list = "";
            $(".shop-cart").each(function () {
                if ($(this).hasClass("select")) {
                    countOrderCheck += 1;
                    list += $(this).attr("data-item-id") + "|";
                }
            });
            $("#<%=hdfListOrderTempID.ClientID%>").val(list);
            if (countOrderCheck > 0) {
                $(".getallOrder").attr("style", "display:inline-block;").find(".numofOrder").html(countOrderCheck);
            }
            else {
                $(".getallOrder").removeAttr("style").hide().find(".numofOrder").html(countOrderCheck);
            }
        });

        function checkout(ID) {
            var infor = "";
            var brandpro = "";
            var chk_isfast = $("#chk_fast_" + ID + "").is(':checked');
            var note = $("#order_temp_" + ID + "").val();
            if (note == null || note == "") {
                note = "";
            }

            var priceVND = $("#priceVND_" + ID + "").attr("data-price");
            var objo = $("#" + ID + "_checkproductselect");
            infor += ID + "," + note + "," + priceVND + "|";

            $(".brand-name-product").each(function () {
                var parent = $(this).attr("data-parent");
                if (parent == ID) {
                    var idpro = $(this).find(".notebrand").attr("data-item-id");
                    var brandtext = $(this).find(".notebrand").val();
                    brandpro += idpro + "," + brandtext + "|";
                }
            });

            $("#<%= hdforderlistall.ClientID %>").val(infor);
             $("#<%= hdfProductBrand.ClientID %>").val(brandpro);
             $("#<%= checkout1order.ClientID %>").click();

        }

    </script>

    <style>
        .process-list {
    list-style: none;
    padding: 0;
    position: relative;
    max-width: 800px;
    margin: 20px auto 26px;
    border: none;
    z-index: 0;
    padding-left: 50px !important;
}
        .process-list .process-item.active {
    color: #ffff;
}
        .process-list .process-item {
    display: inline-block;
    width: 30%;
    text-align: center;
    float: none;
    position: relative;
}
        .process-list .process-item.active i {
    background: #F64302;
    border-color: #fff;
    border-style: solid;
}
        .process-list .process-item i {
    display: block;
    height: 68px;
    width: 68px;
    position: relative;
    text-align: center;
    margin: 0 auto;
    background: #f5f6f7;
    border: 2px solid #e5e5e5;
    line-height: 65px;
    font-size: 30px;
    border-radius: 50%;
    border-style: dashed;
}
        .process-list .process-item.active p {
    color: #F64302;
    font-weight: bold;
}
        .process-list .process-item p {
    font-size: 14px;
    margin-top: 11px;
}
        .process-list .process-item.active:after {
    background: #F64302;
}
        .process-list .process-item:after {
    content: '';
    display: block;
    height: 3px;
    background: #e5e5e5;
    width: 100%;
    position: absolute;
    top: 33px;
    z-index: -1;
    right: -100px;
}

    </style>
</asp:Content>
