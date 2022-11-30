<%@ Page Title="" Language="C#" MasterPageFile="~/PDVMasterLogined.Master" AutoEventWireup="true" CodeBehind="Cart1.aspx.cs" Inherits="NHST.Cart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content clearfix">
        <div class="container">
            <div class="breadcrumb clearfix">
                <p><a href="/trang-chu" class="color-black">Trang chủ</a> - <span>Giỏ hàng</span></p>
                <img src="/App_Themes/pdv/assets/images/car.png" alt="#">
            </div>
            <%-- <h2 class="content-title">Giỏ hàng</h2>--%>
            <div class="order-tool clearfix">
                <div class="tool-detail">
                    <div class="sec step-sec">
                        <div class="sec-tt">
                            <h2 class="content-title">Đơn hàng</h2>
                        </div>
                        <div class="steps">
                            <div class="step active">
                                <div class="step-img">
                                    <img src="/App_Themes/NHST/images/order-step-1.png" alt="">
                                </div>
                                <h4 class="title">Giỏ hàng</h4>
                            </div>
                            <div class="step">
                                <div class="step-img">
                                    <img src="/App_Themes/NHST/images/order-step-2.png" alt="">
                                </div>
                                <h4 class="title">Chọn địa chỉ nhận hàng</h4>
                            </div>
                            <div class="step">
                                <div class="step-img">
                                    <img src="/App_Themes/NHST/images/order-step-3.png" alt="">
                                </div>
                                <h4 class="title">Đặt cọc và kết đơn</h4>
                            </div>
                        </div>
                    </div>
                    <div class="sec warning-sec">
                        <div class="sec-tt">
                            <h2 class="content-title">Chú ý</h2>
                            <img src="/App_Themes/pdv/assets/images/car.png" alt="#">
                        </div>
                        <div class="clear"></div>
                        <p class="warning-txt">
                            Sản phẩm trong giỏ sẽ tự động xóa trong vòng 30 ngày. Người bán trên website 1688.com thường có quy định về số lượng mua tối thiểu, bội số mỗi sản phẩm, giá trị đơn hàng 
                        tối thiểu và sẽ từ chối bán nếu không đáp ứng. Trong trường hợp đó 1688 Express sẽ hủy những đơn hàng này và không báo trước.
                        </p>
                        <div class="clear"></div>
                    </div>
                    <div class="sec gray-area">
                        <div class="sec-tt">
                            <h2 class="content-title">ĐẶT HÀNG BẰNG CÁCH NHẬP LINK SẢN PHẨM</h2>
                        </div>
                        <div class="clear"></div>
                        <asp:UpdatePanel ID="upd" runat="server">
                            <ContentTemplate>
                                <div class="search-url-box">
                                    <asp:TextBox ID="txt_link" runat="server" CssClass="form-control" placeholder="Nhập link sản phẩm: taobao, 1688, tmall."></asp:TextBox>
                                    <asp:Button ID="btn_search" runat="server" CssClass="pill-btn btn btn-search" Text="Tìm thông tin sản phẩm" OnClick="btn_search_Click" />
                                    <asp:RequiredFieldValidator ID="rq1" runat="server" ControlToValidate="txt_link" CssClass="error-validate" ErrorMessage="Không để trống link sản phẩm." ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>
                                <div class="clear"></div>
                                <asp:Panel ID="pn_productview" runat="server" Visible="false">
                                    <div class="search-url-result">
                                        <div class="img">
                                            <asp:Literal ID="ltr_image" runat="server"></asp:Literal>
                                        </div>
                                        <div class="info">
                                            <div class="pv-att title">
                                                <asp:Literal ID="ltr_title_origin" runat="server"></asp:Literal>
                                            </div>
                                            <br />
                                            <div class="pv-att price">
                                                <asp:Literal ID="ltr_price_origin" runat="server"></asp:Literal>
                                            </div>
                                            <div class="pv-att price">
                                                <asp:Literal ID="ltr_price_vnd" runat="server"></asp:Literal>
                                            </div>
                                            <br />
                                            <div class="pv-att">
                                                <asp:Literal ID="ltr_attribute" runat="server"></asp:Literal>
                                            </div>
                                            <br />
                                            <div class="pv-att title">
                                                <asp:Literal ID="ltr_material" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Literal ID="ltr_content" runat="server"></asp:Literal>
                                <asp:Literal ID="ltr_demo" runat="server"></asp:Literal>
                                <asp:HiddenField ID="ltr_full" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hdf_product_ok" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hdf_title_origin" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hdf_price_origin" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="ltr_price_promotion" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="ltr_property" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="ltr_data_value" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="ltr_shop_id" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="ltr_shop_name" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="ltr_seller_id" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="ltr_wangwang" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="ltr_stock" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="ltr_location_sale" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="ltr_site" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="ltr_item_id" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="ltr_link_origin" runat="server"></asp:HiddenField>
                                <asp:HiddenField ID="hdf_image_prod" runat="server"></asp:HiddenField>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upd">
                            <ProgressTemplate>
                                <div class="modal">
                                    <div class="center">
                                        <img alt="" src="/App_Themes/NHST/loading.gif" width="80px" />
                                    </div>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>

                        <div class="clear"></div>
                        <div class="search-internal-box">
                            <select class="form-control _select_category pill-control savedb" data-loaded="1">
                                <option value="0">Chọn danh mục</option>
                                <option value="2">Áo nữ</option>
                                <option value="3">Áo nam</option>
                                <option value="4">Quần nữ</option>
                                <option value="5">Quần nam</option>
                                <option value="6">Quần áo trẻ em</option>
                                <option value="7">Váy</option>
                                <option value="8">Giày nam</option>
                                <option value="9">Giày nữ</option>
                                <option value="10">Giày trẻ em</option>
                                <option value="11">Phụ kiện thời trang</option>
                                <option value="12">Túi xách</option>
                                <option value="13">Ví</option>
                                <option value="14">Mỹ phẩm</option>
                                <option value="15">Vải vóc</option>
                                <option value="16">Tóc giả</option>
                                <option value="17">Đồ chơi</option>
                                <option value="18">Trang sức</option>
                                <option value="19">Phụ tùng ô tô, xe máy</option>
                                <option value="20">Thiết bị điện tử</option>
                                <option value="21">Linh kiện điện tử</option>
                                <option value="22">Phụ kiện điện tử</option>
                                <option value="23">Sách báo, tranh ảnh, đồ sưu tập</option>
                                <option value="24">Quà tặng</option>
                                <option value="25">Đồ gia dụng</option>
                                <option value="-1">Khác</option>
                            </select>
                            <input type="text" class="form-control pill-control txt-brand-product" id="brand-name" placeholder="Ghi chú sản phẩm." />
                            <a class="btn-add-to-cart pill-btn btn " onclick="add_to_cart();" style="font-size: 13px;">Thêm vào giỏ hàng</a>
                        </div>
                    </div>
                    <div class="sec table-price-sec">
                        <div class="tbp-top">
                            <asp:Literal ID="ltr_sub" runat="server"></asp:Literal>
                            <asp:Panel ID="pn_search" runat="server">
                                <div class="search-form-wrap" style="display: none;">
                                    <div>
                                        <input type="text" class="form-control" placeholder="Tìm tên người bán">
                                        <a class="btn"><i class="fa fa-search"></i></a>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <asp:Literal ID="ltr_cart" runat="server"></asp:Literal>
                        <asp:Literal ID="ltr_total_pay" runat="server"></asp:Literal>
                        <asp:Button ID="checkoutallorder" runat="server" Text="chaeckdall" OnClick="checkoutallorder_Click" Style="display: none" CausesValidation="false" />
                        <asp:Button ID="checkout1order" runat="server" Text="chaeckdall" OnClick="checkout1order_Click" Style="display: none" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>
    </section>
    <input id="hdf_mat_choose_prop" type="hidden" />
    <input id="hdf_mat_choose_valu" type="hidden" />
    <asp:HiddenField ID="hdforderlistall" runat="server" />
    <asp:HiddenField ID="hdfallnote" runat="server" />
    <asp:HiddenField ID="hdfallorderid" runat="server" />
    <asp:HiddenField ID="hdfProductBrand" runat="server" />
    <style>
        .brand-name-product {
            float: left;
            width: 100%;
            margin: 10px 0 40px 0;
        }

            .brand-name-product input {
                float: left;
                width: 100%;
            }
    </style>
    <script type="text/javascript">
        function activemate(obj, matename) {
            obj.parent().find("li").removeClass("tb-selected");
            obj.addClass("tb-selected");

            var valu = "";
            var prop = "";
            $(".J_SKU").each(function () {
                if ($(this).hasClass("tb-selected")) {
                    valu += $(this).attr("data-pv") + ";";
                    prop += $(this).find("a").find("span").html() + ";";
                }
            });
            $("#hdf_mat_choose_valu").val(valu);
            $("#hdf_mat_choose_prop").val(prop);
        }
        function updatequantity(ID, obj, shopid) {
            var quantity = obj.val();
            if (quantity == 0) {
                deleteordertemp(ID, shopid);
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "Cart.aspx/UpdateQuantityOrderTemp",
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
        function deleteordertemp(ID, shopid) {
            var r = confirm("Bạn muốn xóa sản phẩm này?");
            if (r == true) {
                $.ajax({
                    type: "POST",
                    url: "Cart.aspx/deleteOrderTemp",
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
                    url: "Cart.aspx/deleteOrderShopTemp",
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
        function checkselectfull() {
            var kt = false;
            if ($(".material-product").length > 0) {
                $(".material-product").each(function () {
                    kt = false;
                    $(this).find("li").each(function () {
                        if ($(this).hasClass("tb-selected")) {
                            kt = true;
                        }
                    });
                    if (kt == false)
                        return kt;
                });
                return kt;
            }
            else
                return true;
        }
        function add_to_cart() {
            var kt = checkselectfull();
            if (kt == false) {
                alert("Vui lòng chọn đầy đủ thuộc tính của SP.");
                return false;
            }

            var title = $("#<%= hdf_title_origin.ClientID%>").val();
            if (title == "" || title == null) {
                alert("Vui lòng nhập link sản phẩm cần mua");
                return;
            }
            var price = $("#<%= hdf_price_origin.ClientID%>").val();
            var image_pro = $("#<%= hdf_image_prod.ClientID%>").val();
            var shop_id = $("#<%= ltr_shop_id.ClientID%>").val();
            var shop_name = $("#<%= ltr_shop_name.ClientID%>").val();
            var seller_id = $("#<%= ltr_seller_id.ClientID%>").val();
            var wangwang = $("#<%= ltr_wangwang.ClientID%>").val();
            var site = $("#<%= ltr_site.ClientID%>").val();
            var item_id = $("#<%= ltr_item_id.ClientID%>").val();
            var link_origin = $("#<%= ltr_link_origin.ClientID%>").val();

            var select_category = $('.savedb');
            var category_id = select_category.val();
            //if (category_id == 0) {
            //    alert('Vui lòng chọn danh mục sản phẩm.');
            //    return false;
            //}
            //var category_name = $(".savedb [value='" + category_id + "']").text();
            var category_name = "Đang cập nhật";

            var brandname = $("#brand-name").val();
            //if (brandname == "" || brandname == null) {
            //    alert('Vui lòng nhập thương hiệu.');
            //    return false;
            //}
            var tool = 'Addon';
            var value_mat = $("#hdf_mat_choose_valu").val();
            var prop_mat = $("#hdf_mat_choose_prop").val();
            var data_nhst = {
                'title_origin': '' + title + '',
                'title_translated': '' + title + '',
                'price_origin': '' + price + '',
                'price_promotion': '' + price + '',
                'property_translated': '' + prop_mat + '',
                'property': '' + prop_mat + '',
                'data_value': '' + value_mat + '',
                'image_model': '' + image_pro + '',
                'image_origin': '' + image_pro + '',
                'shop_id': '' + shop_id + '',
                'shop_name': '' + shop_name + '',
                'seller_id': '' + seller_id + '',
                'wangwang': '' + wangwang + '',
                'quantity': '1',
                'stock': '',
                'location_sale': '',
                'site': '' + site + '',
                'comment': '',
                'item_id': '' + item_id + '',
                'link_origin': '' + link_origin + '',
                'outer_id': '',
                'error': '0',
                'weight': '0',
                'step': '1',
                'brand': '' + brandname + '',
                'category_name': '' + category_name + '',
                'category_id': '' + category_id + '',
                'tool': 'PasteLink',
                'version': '4.11.88',
                'is_translate': 'false'
            };

            $.ajax({
                url: "/WebService1.asmx/receiverequest",
                data: data_nhst,
                method: 'POST',
                contentType: 'application/x-www-form-urlencoded',
                xhrFields: {
                    withCredentials: true
                },
                headers: { 'X-Requested-With': 'XMLHttpRequest' },
                success: function (d) {

                    //alert((new XMLSerializer()).serializeToString(d));
                    location.reload();
                    chrome.tabs.sendMessage(sender.tab.id, { action: request.callback, response: d }, function (response) {

                    });
                },
                error: function (event, jqXHR, ajaxSettings, thrownError) {
                    //alert('[event:' + event + '], [jqXHR:' + jqXHR + '], [ajaxSettings:' + ajaxSettings + '], [thrownError:' + thrownError + '])');
                }
            });
        }

        function updatecheck(obj, ID, fieldUpdate) {
            $("body").append("<div class=\"loading\"></div>");
            var chk = obj.is(':checked');
            $.ajax({
                type: "POST",
                url: "Cart.aspx/UpdateField",
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
            //$("body").append("<div class=\"loading\"></div>");

            //updatecheck(objo, ID, "IsCheckProduct");
            //$.ajax({
            //    type: "POST",
            //    url: "Cart.aspx/UpdateNoteFastPriceVND",
            //    data: "{ID:'" + ID + "',note:'" + note + "',priceVND:'" + priceVND + "'}",
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    success: function (msg) {
            //        var ret = msg.d;
            //        //alert(ret);
            //        //if (ret == "1") {
            //        window.location.href = "/thanh-toan/" + ID + "";
            //        //}
            //    },
            //    error: function (xmlhttprequest, textstatus, errorthrow) {
            //        //alert('lỗi');
            //    }
            //});
            //$(".loading").remove();
        }
        function checkoutAll(listorderid) {
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

                    //$.ajax({
                    //    type: "POST",
                    //    url: "Cart.aspx/UpdateNoteFastPriceVND",
                    //    data: "{ID:'" + ID + "',note:'" + note + "',priceVND:'" + priceVND + "'}",
                    //    contentType: "application/json; charset=utf-8",
                    //    dataType: "json",
                    //    success: function (msg) {
                    //        var ret = msg.d;
                    //        if (ret != "ok") {
                    //            alert('Có lỗi trong quá trình xử lý. Vui lòng kiểm tra lại.');
                    //            return;
                    //        }
                    //    },
                    //    error: function (event, jqXHR, ajaxSettings, thrownError) {
                    //        //alert(ID + " - " + chk_isfast + " - " + note + " - " + priceVND);
                    //        //alert('[event:' + event + '], [jqXHR:' + jqXHR + '], [ajaxSettings:' + ajaxSettings + '], [thrownError:' + thrownError + '])');
                    //    }
                    //});
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
        function isEmpty(str) {
            return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
        }
    </script>
</asp:Content>
