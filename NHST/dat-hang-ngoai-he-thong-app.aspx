<%@ Page Title="Đặt hàng ngoài hệ thống app" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="dat-hang-ngoai-he-thong-app.aspx.cs" Inherits="NHST.dat_hang_ngoai_he_thong_app" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" href="/App_Themes/App/css/style-NA.css" media="all">
    <style>
        .row-left {
            float: left;
            width: 48%;
        }

        .list-order {
            width: 96%;
            display: inline-block;
            margin-left: 2%;
        }

        .content_page h3 {
            color: #f37421;
        }

        .delete-all {
            color: #da0000;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <main id="main-wrap">
        <div class="page-wrap">
            <asp:Panel ID="pnMobile" runat="server" Visible="false">
                <div class="page-body">
                    <div class="all dathangngoaihethong">
                        <h2 class="title_page">TẠO ĐƠN HÀNG TỪ CÁC TRANG THƯƠNG MẠI ĐIỆN TỬ</h2>
                        <div class="content_page">
                            <p class="user">
                                <asp:Literal runat="server" ID="ltrUsername"></asp:Literal>
                                <a class="item-user"><i class="fa fa-user"></i></a>
                            </p>
                            <div class="list-order">
                                <div class="row-left">
                                    <span>Kho TQ</span>
                                </div>
                                <div class="row-right">
                                    <asp:DropDownList runat="server" CssClass="input_control" ID="ddlKhoTQ" AppendDataBoundItems="true" DataTextField="WareHouseName" DataValueField="ID"></asp:DropDownList>
                                </div>
                            </div>

                             <div class="list-order">
                                <div class="row-left">
                                    <span>Kho VN</span>
                                </div>
                                <div class="row-right">
                                     <asp:DropDownList runat="server" CssClass="input_control" ID="ddlKhoVN" AppendDataBoundItems="true" DataTextField="WareHouseName" DataValueField="ID"></asp:DropDownList>
                                </div>
                            </div>

                             <div class="list-order">
                                <div class="row-left">
                                    <span>PTVC</span>
                                </div>
                                <div class="row-right">
                                     <asp:DropDownList runat="server" CssClass="input_control" ID="ddlShipping" AppendDataBoundItems="true" DataTextField="ShippingTypeName" DataValueField="ID"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="list-order">
                                <div class="row-left">
                                    <h3>Danh sách sản phẩm</h3>
                                </div>
                                <div class="row-left">
                                    <a href="javascript:;" onclick="deleteAllProduct()" id="deleteAllProduct" class="delete-all" style="float: right; margin-right: 5px; display: none; width: auto;">
                                        <i class="fa fa-close"></i>Xóa hết sản phẩm</a>
                                </div>
                            </div>


                            <div class="product-list">
                                <div class="form-input products">
                                    <ul class="quantity_input">
                                        <li style="width: 55%;">Ảnh sản phẩm
                                        </li>
                                        <li>
                                            <input type='file' class="productimage" onchange="imagepreview(this,$(this));" name="productimage1" />
                                            <img class="imgpreview" style="width: 40%;" alt="" />
                                            <a href="javascript:;" class="remove" style="display: none">Xóa</a>
                                        </li>
                                    </ul>

                                    <input class="input_control product-link" type="text" name="" placeholder="Link sản phẩm">
                                    <input class="input_control product-name" type="text" name="" placeholder="Tên sản phẩm">
                                    <input class="input_control product-colorsize" type="text" name="" placeholder="Màu sắc, kích thước">
                                    <ul class="quantity_input">
                                        <li style="width: 55%;">
                                            <p>Số lượng:</p>
                                        </li>
                                        <li>
                                            <input class="input_control_number product-quantity" type="number" value="1" name="quantity" min="1">
                                        </li>
                                    </ul>
                                    <input class="input_control message product-request" type="text" name="" placeholder="Yêu cầu thêm">
                                    <div class="btn_delete">
                                        <a onclick="deleteProduct($(this))">
                                            <img src="/App_Themes/App/images/icon-remove-red.png"></a>
                                    </div>
                                </div>
                            </div>
                            <div class="group_btn">
                                <p class="btn_new">
                                    <a class="btn_addnew" onclick="addProduct()"><i class="icon_addnew fa fa-plus"></i>THÊM SẢN PHẨM</a>
                                </p>
                                <p class="btn_order">
                                    <a href="javascript:;" onclick="CreateOrder()" class="btn_ordersp">Tạo đơn hàng</a>

                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="page-bottom-toolbar">
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnShowNoti" runat="server" Visible="false">
                <div class="page-body">
                    <div class="heading-search">
                        <h4 class="page-title">Bạn vui lòng đăng xuất và đăng nhập lại!</h4>
                    </div>
                </div>
            </asp:Panel>
        </div>

    </main>
    <asp:HiddenField ID="hdfProductList" runat="server" />
    <asp:HiddenField ID="hdfcountimage" runat="server" Value="1" />
    <script type="text/javascript">
        function addProduct() {
            var countimage = parseFloat($("#<%=hdfcountimage.ClientID%>").val());
            countimage += 1;
            $("#<%= hdfcountimage.ClientID %>").val(countimage);
            var html = "";
            html += "<div class=\"form-input products\">";

            html += "  <ul class=\"quantity_input\">";
            html += " <li>  Ảnh sản phẩm </li>";
            html += "   <li>";
            html += "   <input type=\'file\' class=\"productimage\" onchange=\"imagepreview(this, $(this));\" name=\"productimage" + countimage + "\" />";
            html += "   <img class=\"imgpreview\" style=\"width:40%;\" alt=\"\" />";
            html += "     <a href=\"javascript:;\" class=\"remove\" style=\"display:none\">Xóa</a>";
            html += "   </li>";
            html += " </ul>";

            html += "  <input class=\"input_control product-link\" type=\"text\" name=\"\" placeholder=\"Link sản phẩm\">";
            html += "  <input class=\"input_control product-name\" type=\"text\" name=\"\" placeholder=\"Tên sản phẩm\">";
            html += "   <input class=\"input_control product-colorsize\" type=\"text\" name=\"\" placeholder=\"Màu sắc, kích thước\">";
            html += "     <ul class=\"quantity_input\">";
            html += "        <li style=\"width:55%;\">";
            html += "    <p>Số lượng:</p>";
            html += "     </li";
            html += "     <li>";
            html += "  <input class=\"input_control_number product-quantity\" type=\"number\" value=\"1\" name=\"quantity\" min=\"1\">";
            html += "    </li>";
            html += " </ul>";
            html += "    <input class=\"input_control message product-request\" type=\"text\" name=\"\" placeholder=\"Yêu cầu thêm\">";
            html += "        <div class=\"btn_delete\">";
            html += "   <a onclick=\"deleteProduct($(this))\">";
            html += "               <img src=\"/App_Themes/App/images/icon-remove-red.png\"></a>";
            html += "   </div>";
            html += "    </div>";
            $(".product-list").append(html);
            checkShowButton();
        }
        function checkShowButton() {
            if ($(".products").length > 0) {
                $("#deleteAllProduct").show();
            }
            else {
                $("#deleteAllProduct").hide();
            }
        }
        function deleteProduct(obj) {
            var c = confirm('Bạn muốn xóa sản phẩm này?');
            if (c == true) {
                var countimage = parseFloat($("#<%=hdfcountimage.ClientID%>").val());
                if (countimage > 0) {
                    countimage = countimage - 1;
                    $("#<%=hdfcountimage.ClientID%>").val(countimage);
                }
                obj.parent().parent().remove();
            }
            checkShowButton();
        }
        function deleteAllProduct() {
            var c = confirm('Bạn muốn xóa tất cả sản phẩm?');
            if (c == true) {
                $(".products").remove();
                $("#<%=hdfcountimage.ClientID%>").val("0");
            }
            checkShowButton();
        }
        function keypress(e) {
            var keypressed = null;
            if (window.event) {
                keypressed = window.event.keyCode; //IE
            }
            else {
                keypressed = e.which; //NON-IE, Standard
            }
            if (keypressed < 48 || keypressed > 57) {
                if (keypressed == 8 || keypressed == 127) {
                    return;
                }
                return false;
            }
        }
        function CreateOrder() {
             var khotq = $("#<%=ddlKhoTQ.ClientID%>").val();
            var khovn = $("#<%=ddlKhoVN.ClientID%>").val();
            var shipping = $("#<%=ddlShipping.ClientID%>").val();
            if (khotq == 0) {
                alert('Vui lòng chọn kho TQ');
                return;
            }
            if (khovn == 0) {
                alert('Vui lòng chọn kho VN');
                return;
            }
            if (shipping == 0) {
                alert('Vui lòng chọn phương thức VC');
                return;
            }

            if ($(".products").length > 0) {
                var html = "";
                var check = false;
                $(".products").each(function () {
                    var item = $(this);
                    var productlink_obj = item.find(".product-link");
                    var productlink = item.find(".product-link").val();

                    var productname_obj = item.find(".product-name");
                    var productname = item.find(".product-name").val();

                    var productsizecolor_obj = item.find(".product-colorsize");
                    var productsizecolor = item.find(".product-colorsize").val();

                    var productquantity_obj = item.find(".product-quantity");
                    var productquantity = item.find(".product-quantity").val();
                    var productquantityfloat = parseFloat(item.find(".product-quantity").val());

                    var productrequest = item.find(".product-request").val();
                    if (isBlank(productlink)) {
                        //alert('Vui lòng nhập link sản phẩm');
                        check = true;
                    }
                    if (isBlank(productname)) {
                        //alert('Vui lòng nhập tên sản phẩm');

                        check = true;
                    }
                    if (isBlank(productsizecolor)) {
                        //alert('Vui lòng nhập màu sắc, kích thước sản phẩm');

                        check = true;
                    }
                    if (isBlank(productquantity)) {
                        //alert('Vui lòng số lượng cần mua, và số lượng phải lớn hơn 0');

                        check = true;
                    }
                    else if (productquantityfloat <= 0) {

                        check = true;
                    }

                    validateText(productlink_obj);
                    validateText(productname_obj);
                    validateText(productsizecolor_obj);
                    validateText(productquantity_obj);
                    validateNumber(productquantity_obj);
                });
                if (check == true) {
                    alert('Vui lòng điền đầy đủ thông tin từng sản phẩm');
                }
                else {
                    $(".products").each(function () {
                        var item = $(this);
                        var productlink = item.find(".product-link").val();
                        var productname = item.find(".product-name").val();
                        var productsizecolor = item.find(".product-colorsize").val();
                        var productquantity = item.find(".product-quantity").val();
                        var productrequest = item.find(".product-request").val();
                        var image = item.find(".productimage").attr("name");
                        html += productlink + "]" + productname + "]" + productsizecolor + "]" + productquantity + "]" + productrequest + "]" + image + "|";
                    });
                    $("#<%=hdfProductList.ClientID%>").val(html);
                        $("#<%=btncreateuser.ClientID%>").click();
                }
            }
            else {
                alert('Vui lòng nhập sản phẩm');
            }
        }
        function validateText(obj) {
            var value = obj.val();
            if (isBlank(value)) {
                obj.addClass("border-select");
            }
            else {
                obj.removeClass("border-select");
            }
        }
        function validateNumber(obj) {
            var value = parseFloat(obj.val());
            if (value <= 0)
                obj.addClass("border-select");
            else
                obj.removeClass("border-select");
        }
        function isBlank(str) {
            return (!str || /^\s*$/.test(str));
        }

        function imagepreview(input, obj) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();

                reader.onload = function (e) {
                    obj.parent().find('.imgpreview').attr('src', e.target.result);
                    //$('.imgpreview').attr('src', e.target.result);

                    $(".remove").show();
                    obj.parent().find(".remove").click(function () {
                        obj.parent().find('.imgpreview').attr('src', "");
                        obj.parent().find('input:file').val("");
                        $(this).hide();
                    });
                }
                reader.readAsDataURL(input.files[0]);
            }
        }

    </script>
    <style>
        .table-panel-main table td {
            padding: 20px 10px;
        }

        .form-row-right {
            line-height: 40px;
        }

        .custom-padding-display {
            display: inline-block;
            padding: 10px 40px !important;
        }

        .border-select {
            border: solid 2px red;
        }

        .delete-all {
            font-size: 14px;
        }

        .form-row-left {
            width: 40%;
        }

        .form-row-right {
            width: 60%;
        }
    </style>
    <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn btn-success btn-block pill-btn primary-btn main-btn hover"
        OnClick="btncreateuser_Click" Style="display: none" />
</asp:Content>
