<%@ Page Language="C#" MasterPageFile="~/UserMasterNew.Master" AutoEventWireup="true" CodeBehind="tao-don-hang-khac.aspx.cs" Inherits="NHST.tao_don_hang_khac1" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <style>
        .border-select {
            border: solid 2px red !important;
        }
    </style>
</asp:Content>
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
                                        <h4>TẠO ĐƠN HÀNG TRANG TMĐT KHÁC</h4>
                                    </div>
                                </div>
                                <div class="col s12 mt-2">
                                    <div class="order-list-info">
                                        <div class="total-info mb-2 create-product">
                                            <div class="row section">

                                                <div class="col s12">
                                                    <div class="float-left mt-1">
                                                        <div class="search-name input-field col s12 l12">
                                                            <asp:DropDownList runat="server" ID="ddlKhoTQ" AppendDataBoundItems="true" DataTextField="WareHouseName" DataValueField="ID"></asp:DropDownList>
                                                            <label for="search_name"><span>Kho TQ</span></label>
                                                        </div>
                                                    </div>
                                                    <div class="float-left mt-1">
                                                        <div class="search-name input-field col s12 l12">
                                                            <asp:DropDownList runat="server" ID="ddlKhoVN" AppendDataBoundItems="true" DataTextField="WareHouseName" DataValueField="ID"></asp:DropDownList>
                                                            <label for="search_name"><span>Kho đích</span></label>
                                                        </div>
                                                    </div>
                                                    <div class="float-left mt-1" >
                                                        <div class="search-name input-field col s12 l12">
                                                            <asp:DropDownList runat="server" ID="ddlShipping" AppendDataBoundItems="true" DataTextField="ShippingTypeName" DataValueField="ID"></asp:DropDownList>
                                                            <label for="search_name"><span>Phương thức VC</span></label>
                                                        </div>
                                                    </div>
                                                    <div class="float-right mt-2">
                                                        <a href="javascript:;" class="btn add-product valign-wrapper" style="display: flex"><i class="material-icons">add</i><span>Sản phẩm</span></a>
                                                    </div>
                                                    <div class="clearfix"></div>
                                                    <div class="responsive-tb">
                                                        <table class="table    highlight bordered  centered    mt-2">
                                                            <thead>
                                                                <tr>
                                                                    <th class="tb-date">Ảnh
                                                                        <br />
                                                                        sản phẩm</th>
                                                                    <th class="tb-date">Link sản phẩm</th>
                                                                    <th class="tb-date">Tên sản phẩm</th>
                                                                    <th class="tb-date">Giá sản phẩm</th>
                                                                    <th class="no-wrap">Màu sắc / kích thước</th>
                                                                    <th class="tb-date">Số lượng</th>
                                                                    <th class="tb-date">Ghi chú thêm</th>
                                                                    <th class="no-wrap">Thao tác</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody class="list-product">
                                                                <tr class="slide-up product-item">
                                                                    <td>
                                                                        <div>
                                                                            <input class="upload-img productimage" type="file" name="productimage1" onchange="imagepreview(this,$(this))" title="">
                                                                            <button type="button" class="btn-upload">Upload</button>
                                                                            <img class="imgpreview" style="width: 40%;" alt="" />
                                                                            <a href="javascript:;" class="remove" style="display: none">Xóa</a>
                                                                        </div>
                                                                        <div class="preview-img"></div>
                                                                    </td>
                                                                    <td>
                                                                        <input class="product-link" type="text" value=""></td>
                                                                    <td>
                                                                        <input class="product-name" type="text" value=""></td>
                                                                    <td>
                                                                        <input class="product-price" type="text" value=""></td>
                                                                     <td>
                                                                        <input class="product-colorsize" type="text" value=""></td>
                                                                    <td>
                                                                        <input class="product-quantity" type="number" value="1"></td>
                                                                    <td>
                                                                        <input class="product-request" type="text" value=""></td>
                                                                    <td class="no-wrap">
                                                                        <!-- Dropdown Trigger -->
                                                                        <a href='javascript:;' class="remove-product"><i class="material-icons valign-center">remove_circle</i></a>

                                                                    </td>
                                                                </tr>
                                                            </tbody>
                                                        </table>
                                                    </div>
                                                    <div class="float-right mt-2">
                                                        <a href="javascript:;" onclick="CreateOrder()" class="btn create-order">Tạo đơn hàng</a>
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
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdfProductList" runat="server" />
    <asp:HiddenField ID="hdfcountimage" runat="server" Value="1" />

    <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn btn-success btn-block pill-btn primary-btn main-btn hover"
        OnClick="btncreateuser_Click" Style="display: none" />

    <script type="text/javascript">
        $(document).ready(function () {
            $('.create-product .add-product').on('click', function () {
                var countimage = parseFloat($("#<%=hdfcountimage.ClientID%>").val());
                countimage += 1;
                $("#<%= hdfcountimage.ClientID %>").val(countimage);
                var tableHTML = $('.create-product table .list-product');
                var html = '';
                html += '<tr class="slide-up product-item">';
                html += '<td>  ';
                html += '<div>';
                html += '<input class="upload-img productimage" type="file" onchange="imagepreview(this,$(this))" name="productimage' + countimage + '" title="">';
                html += ' <button type="button" class="btn-upload">Upload</button>';
                html += ' <img class="imgpreview" style="width: 40%;" alt="" /> ';
                html += ' <a href="javascript:;" class="remove" style="display: none">Xóa</a> ';
                html += ' </div> ';
                html += ' <div class="preview-img"></div>';
                html += ' </td>';
                html += ' <td><input class="product-link" type="text" value=""></td>';
                html += ' <td><input class="product-name" type="text" value=""></td> ';
                html += ' <td><input class="product-price" type="text" value=""></td> ';
                html += ' <td><input class="product-colorsize" type="text" value=""></td> ';
                html += ' <td><input class="product-quantity" type="number" value="1"></td>';
                html += ' <td><input class="product-request" type="text" value=""></td> ';
                html += ' <td class="no-wrap">';

                html += '<a href="javascript:;" class="remove-product"><i class="material-icons valign-center">remove_circle</i></a> ';
                html += ' </td>';
                html += ' </tr>';
                tableHTML.append(html);
            });
            $('.create-product').on('click', '.remove-product', function () {
                var c = confirm('Bạn muốn xóa sản phẩm này?');
                if (c == true) {
                    var countimage = parseFloat($("#<%=hdfcountimage.ClientID%>").val());
                    if (countimage > 0) {
                        countimage = countimage - 1;
                        $("#<%=hdfcountimage.ClientID%>").val(countimage);
                    }
                    $(this).parent().parent().fadeOut(function () {
                        $(this).remove();
                    });
                }
            });
        });

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
           <%-- var khotq = $("#<%=ddlKhoTQ.ClientID%>").val();
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
            }--%>

            if ($(".product-item").length > 0) {
                var html = "";
                var check = false;
                $(".product-item").each(function () {
                    var item = $(this);
                    var productlink_obj = item.find(".product-link");
                    var productlink = item.find(".product-link").val();

                    var productname_obj = item.find(".product-name");
                    var productname = item.find(".product-name").val();

                    var productprice_obj = item.find(".product-price");
                    var productprice = item.find(".product-price").val();

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
                    if (isBlank(productprice)) {
                        //alert('Vui lòng nhập gia sản phẩm');
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
                    validateText(productprice_obj);
                    validateText(productsizecolor_obj);
                    validateText(productquantity_obj);
                    validateNumber(productquantity_obj);
                });
                if (check == true) {
                    alert('Vui lòng điền đầy đủ thông tin từng sản phẩm');
                }
                else {
                    $(".product-item").each(function () {
                        var item = $(this);
                        var productlink = item.find(".product-link").val();
                        var productname = item.find(".product-name").val();
                        var productprice = item.find(".product-price").val();
                        var productsizecolor = item.find(".product-colorsize").val();
                        var productquantity = item.find(".product-quantity").val();
                        var productrequest = item.find(".product-request").val();
                        var image = item.find(".imgpreview").attr("src");

                        html += productlink + "]" + productname + "]" + productprice + "]" + productsizecolor + "]" + productquantity + "]" + productrequest
                            + "]" + image + "|";
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
</asp:Content>

