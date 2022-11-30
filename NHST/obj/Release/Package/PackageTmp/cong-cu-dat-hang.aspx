<%@ Page Title="" Language="C#" MasterPageFile="~/TruongThanhMaster.Master" AutoEventWireup="true" CodeBehind="cong-cu-dat-hang.aspx.cs" Inherits="NHST.cong_cu_dat_hang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .bbb .btn.pill-btn {
            float: left;
            width: 100%;
            text-transform: uppercase;
            font-weight: bold;
            text-align: center;
            background-color: #ffcd38;
            border: none;
            color: white;
            padding: 0px 20px;
            cursor: pointer;
            line-height: 40px;
        }

        .search-internal-box.nne input {
            float: left;
            width: calc(97% - 193px);
            margin-right: 1%;
            margin-left: 0;
            padding: 0 10px;
        }

        .btn-add-to-cart {
            width: 20%;
            margin: 0;
        }

        ul.attributes-list {
            float: left;
            margin: 0 15px;
            width: 100%;
        }

            ul.attributes-list li {
                float: left;
                width: 100%;
                line-height: 20px;
            }

        .form-search-left {
            float: left;
            width: 79%;
        }

        .form-search-right {
            float: right;
            width: 20%;
        }

        .form-control {
            height: 43px;
        }

        .services {
            background: #f8f8f8;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="home">
        <section id="firm-services" class="services">
            <div class="all">
                <div class="breakcrum">
                    <a href="/" class="brc-home"><i class="fa fa-home"></i>Trang chủ</a>
                    <span class="brc-seperate"><i class="fa fa-arrow-right" style="font-size: 10px;"></i></span>
                    Đặt hàng nhanh
                </div>
                <h4 class="sec__title center-txt">Đặt hàng nhanh</h4>
                <div class="primary-form custom-width">
                    <div class="order-tool clearfix">
                        <div class="tool-detail">
                            <div class="sec gray-area">
                                <div class="clear"></div>
                                <asp:UpdatePanel ID="upd" runat="server">
                                    <ContentTemplate>
                                        <div class="form-search-product bbb">
                                            <div class="form-search-left">
                                                <asp:TextBox ID="txt_link" runat="server" CssClass="form-control txt-search-product" placeholder="Nhập link sản phẩm: taobao, 1688, tmall."></asp:TextBox>
                                                <div class="clear"></div>
                                                <asp:RequiredFieldValidator ID="rq1" runat="server" ControlToValidate="txt_link" CssClass="error-validate" ErrorMessage="Không để trống link sản phẩm." ForeColor="Red"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="form-search-right">
                                                <asp:Button ID="btn_search" runat="server" CssClass="pill-btn btn btn-search btn main-btn hover" Text="Tìm thông tin sản phẩm" OnClick="btn_search_Click" />
                                            </div>
                                        </div>
                                        <div class="clear"></div>
                                        <asp:Panel ID="pn_productview" runat="server" Visible="false">
                                            <div class="product-view">
                                                <div class="pv-left">
                                                    <asp:Literal ID="ltr_image" runat="server"></asp:Literal>
                                                </div>
                                                <div class="pv-right">
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
                                <div class="search-internal-box nne">

                                    <select class="form-control _select_category savedb" data-loaded="1" style="display: none;">
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
                                    <input type="text" class="form-control txt-brand-product" id="brand-name" placeholder="Ghi chú sản phẩm." />
                                    <a class="btn-add-to-cart" onclick="add_to_cart();" style="font-size: 13px;">Thêm vào giỏ hàng</a>
                                </div>
                            </div>
                        </div>
                        <%--<div class="cmt">
                    <img src="/App_Themes/pdv/assets/images/cmt.jpg" alt="#">
                </div>--%>
                    </div>
                </div>
            </div>
        </section>
    </main>
    <%--<section class="content clearfix">
        <div class="container">
            <div class="breadcrumb clearfix">
                <p><a href="/trang-chu" class="color-black">Trang chủ</a> - <span>Đặt hàng nhanh</span></p>
                <img src="/App_Themes/pdv/assets/images/car.png" alt="#">
            </div>
        </div>
    </section>--%>
    <asp:HiddenField ID="hdfCheckLogin" runat="server" />
    <input id="hdf_mat_choose_prop" type="hidden" />
    <input id="hdf_mat_choose_valu" type="hidden" />
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
            var login = $("#<%= hdfCheckLogin.ClientID%>").val();
            if (login == "notlogin") {
                alert("Vui lòng đăng nhập trước khi đặt hàng.");
            }
            else {
                //var kt = checkselectfull();
                //if (kt == false) {
                //    alert("Vui lòng chọn đầy đủ thuộc tính của SP.");
                //    return false;
                //}

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
                var category_name = $(".savedb [value='" + category_id + "']").text();

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
                    'pricestep': '',
                    'brand': '' + brandname + '',
                    'category_name': '' + category_name + '',
                    'category_id': '' + category_id + '',
                    'tool': 'Pastelink',
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
                        //location.reload();
                        window.location.assign("/gio-hang");
                        chrome.tabs.sendMessage(sender.tab.id, { action: request.callback, response: d }, function (response) {

                        });
                    },
                    error: function (event, jqXHR, ajaxSettings, thrownError) {
                        //alert('[event:' + event + '], [jqXHR:' + jqXHR + '], [ajaxSettings:' + ajaxSettings + '], [thrownError:' + thrownError + '])');
                    }
                });
            }

        }
    </script>
    <style>
        .pv-left {
            float: left;
            width: 50%;
        }

        .pv-right {
            float: left;
            width: 50%;
            padding-left: 20px;
            text-align: left;
        }

        .gray-area {
            margin: 0;
        }
    </style>
</asp:Content>
