<%@ Page Title="" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="tao-don-hang-van-chuyen-app.aspx.cs" Inherits="NHST.tao_don_hang_van_chuyen_app" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <style>
        /*.ip-with-sufix .fcontrol {
            background-color: #fff;
        }

        .thanhtoanho-list {
            margin-bottom: 15px;
        }

        table.tb-wlb {
            margin-bottom: 5px;
        }

        .page-title {
            text-align: center;
            padding: 10px 20px;
            font-size: 20px;
        }*/
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <main id="main-wrap">
        <div class="page-wrap">
            <asp:Panel ID="pnMobile" runat="server" Visible="false">
                <div class="page-body">
                    <div class="all">
                        <div class="white-nooffset-cont account-page">

                            <div class="form-wrap350">
                                <h1 class="page-title">Tạo đơn hàng vận chuyển hộ</h1>
                                <div class="frow">
                                    <div class="lb">Tên đăng nhập</div>
                                    <asp:Label runat="server" ID="lbUsername" Style="font-weight: 500;"></asp:Label>
                                    <%--<input type="text" class="fcontrol" value="Rxathu9999">--%>
                                </div>
                                <div class="frow">
                                    <p class="lb">Chọn kho Trung Quốc</p>
                                    <div class="ip-with-sufix">
                                        <asp:DropDownList ID="ddlWarehouseFrom" runat="server" CssClass="fcontrol"
                                            DataValueField="ID" DataTextField="WareHouseName">
                                        </asp:DropDownList>
                                        <span class="sufix"><i class="fa fa-caret-down hl-txt"></i></span>
                                    </div>
                                </div>
                                <div class="frow">
                                    <p class="lb">Chọn kho đích</p>
                                    <div class="ip-with-sufix">
                                        <asp:DropDownList ID="ddlReceivePlace" runat="server" CssClass="fcontrol"
                                            DataValueField="ID" DataTextField="WareHouseName">
                                        </asp:DropDownList>
                                        <span class="sufix"><i class="fa fa-caret-down hl-txt"></i></span>
                                    </div>
                                </div>
                                <div class="frow">
                                    <p class="lb">Chọn phương thức vận chuyển</p>
                                    <div class="ip-with-sufix">
                                        <asp:DropDownList ID="ddlShippingType" runat="server" CssClass="fcontrol"
                                            DataValueField="ID" DataTextField="ShippingTypeName">
                                        </asp:DropDownList>
                                        <span class="sufix"><i class="fa fa-caret-down hl-txt"></i></span>
                                    </div>
                                </div>
                                <div class="frow">
                                    <p class="lb">Danh sách kiện</p>
                                    <table class="tb-kienhang tr-removeable">
                                        <thead>
                                            <tr>
                                                <th style="width: 170px">Mã kiện</th>
                                                <th style="width: 120px">Cân nặng</th>
                                                <th style="width: 41px"></th>
                                            </tr>
                                        </thead>
                                        <tbody class="product-list">
                                            <tr class="product-item">
                                                <td>
                                                    <input type="text" class="gray-bg fcontrol mvd-ip product-link" placeholder="Nhập mã vận đơn"></td>
                                                <td>
                                                    <input type="number" class="gray-bg fcontrol cn-ip product-quantity" min="0" value="0"></td>
                                                <td><a href="javascript:;" onclick="deleteProduct($(this))" class="rm-action">
                                                    <img src="/App_Themes/App/images/icon-remove-red.png" alt=""></a></td>
                                            </tr>
                                        </tbody>

                                    </table>
                                    <p class="right-txt"><a href="javascript:;" onclick="addProduct()" class="xanhreu-txt upercase bold themkien-btn">+ Thêm kiện hàng</a></p>
                                </div>
                                <div class="frow">
                                    <p class="lb">Ghi chú</p>
                                    <asp:TextBox ID="txtNote" runat="server" CssClass="fcontrol gray-bg" TextMode="MultiLine"></asp:TextBox>
                                </div>
                                <a href="#" onclick="CreateOrder()" class="btn primary-btn fw-btn">Tạo đơn hàng</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="page-bottom-toolbar">
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
    <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn btn-success btn-block pill-btn primary-btn main-btn hover"
        OnClick="btncreateuser_Click" Style="display: none" />

    <style>
        .pane-primary .heading {
            background-color: #366136 !important;
        }

        .btn.payment-btn {
            background-color: #3f8042;
            color: white;
            padding: 10px;
            height: 40px;
            width: 49%;
        }

        .btn.cancel-btn {
            background-color: #f84a13;
            color: white;
            padding: 10px;
            height: 40px;
            width: 49%;
        }

        .pagenavi {
            float: right;
            margin-top: 20px;
        }

            .pagenavi a,
            .pagenavi span {
                width: 30px;
                height: 35px;
                line-height: 40px;
                text-align: center;
                color: #959595;
                font-weight: bold;
                background: #f8f8f8;
                display: inline-block;
                font-weight: bold;
                margin-right: 1px;
            }

                .pagenavi .current,
                .pagenavi a:hover {
                    background: #ea1f28;
                    color: #fff;
                }

        .pagenavi {
            float: right;
            margin-top: 20px;
        }

            .pagenavi a,
            .pagenavi span {
                width: 30px;
                height: 35px;
                line-height: 40px;
                text-align: center;
                color: #959595;
                font-weight: bold;
                background: #f8f8f8;
                display: inline-block;
                font-weight: bold;
                margin-right: 1px;
            }

                .pagenavi .current,
                .pagenavi a:hover {
                    background: #ea1f28;
                    color: #fff;
                }

        .filters {
            background: #ebebeb;
            border: 1px solid #e1e1e1;
            font-weight: bold;
            padding: 20px;
            margin-bottom: 20px;
        }

            /*.page.orders-list .filters .lbl {
            padding-right: 50px;
        }*/

            .filters ul li {
                display: inline-block;
                text-align: center;
                padding-right: 2px;
            }

            .filters ul li {
                padding-right: 4px;
            }

        select.form-control {
            appearance: none;
            -webkit-appearance: none;
            -moz-appearance: none;
            -ms-appearance: none;
            -o-appearance: none;
            background: #fff url(/App_Themes/NHST/images/icon-select.png) no-repeat right 15px center;
            padding-right: 25px;
            padding-left: 15px;
            line-height: 40px;
        }
    </style>

    <script type="text/javascript">

        function addProduct() {
            var html = "";
            html += "<tr class=\"product-item\">";
            html += " <td><input type=\"text\" class=\"gray-bg fcontrol mvd-ip product-link\" placeholder=\"Nhập mã vận đơn\"></td>";
            html += " <td><input type=\"number\" class=\"gray bg fcontrol cn-ip product-quantity\" placeholder=\"Cân nặng\" value=\"0\" min=\"0\"/></td>";
            html += "<td><a href=\"javascript:;\" onclick=\"deleteProduct($(this))\" class=\"rm-action\"><img src=\"/App_Themes/App/images/icon-remove-red.png\" alt=\"\"></a></td>";
            html += "</tr>";
            $(".product-list").append(html);
        }

        function deleteProduct(obj) {
            var c = confirm('Bạn muốn xóa kiện này?');
            if (c == true) {
                obj.parent().parent().remove();
            }
        }

        function CreateOrder() {
            //if ($(".product-item").length > 0) {

            //}
            //else {
            //    alert('Vui lòng nhập mã vận đơn');
            //}
            var html = "";
            var check = false;
            $(".product-item").each(function () {
                var item = $(this);
                var productlink_obj = item.find(".product-link");
                var productlink = item.find(".product-link").val();

                var productquantity_obj = item.find(".product-quantity");
                var productquantity = item.find(".product-quantity").val();
                var productquantityfloat = parseFloat(item.find(".product-quantity").val());

                if (isBlank(productlink)) {
                    //alert('Vui lòng nhập link sản phẩm');
                    check = true;
                }

                if (isBlank(productquantity)) {
                    //alert('Vui lòng số lượng cần mua, và số lượng phải lớn hơn 0');

                    check = true;
                }
                else if (productquantityfloat < 0) {

                    check = true;
                }

                validateText(productlink_obj);
                //validateText(productname_obj);
                //validateText(productsizecolor_obj);
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
                    var productquantity = item.find(".product-quantity").val();
                    html += productlink + "]" + productquantity + "|";
                });

            }
            $("#<%=hdfProductList.ClientID%>").val(html);
            $("#<%=btncreateuser.ClientID%>").click();
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

        //jQuery(document).ready(function () {
        //    //        new WOW().init();
        //    var $tbKienhang = $('.tb-kienhang');
        //    $('.themkien-btn').on('click', function (e) {
        //        e.preventDefault();
        //        var $tbody = $tbKienhang.find('tbody');
        //        $tbody.append(`<tr class=\"product-item\">
        //                                <td><input type="text" class="gray-bg fcontrol mvd-ip" placeholder="Nhập mã vận đơn"></td>
        //                                <td><input type="text" class="gray-bg fcontrol cn-ip" placeholder="Cân nặng"></td>
        //                                <td><a href="javascript:;" class="rm-action"><img src="images/icon-remove-red.png" alt=""></a></td>
        //                            </tr>`);
        //    });
        //    $tbKienhang.on('click', '.rm-action', function (e) {
        //        e.preventDefault();
        //        $(this).closest('tr').remove();
        //    });

        //});
    </script>

</asp:Content>
