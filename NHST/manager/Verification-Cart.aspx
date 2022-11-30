<%@ Page Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="Verification-Cart.aspx.cs" Inherits="NHST.manager.Verification_Cart" %>

<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="main">
        <div class="row">
            <div class="content-wrapper-before blue-grey lighten-5"></div>
            <div class="col s12">
                <div class="container">
                    <div class="all">
                        <div class=" mt-3 ">
                            <div class="row">
                                <div class="col s12">
                                    <div class="card-panel no-shadow">
                                        <div class="row">
                                            <div class="col s12">
                                                <div class="page-title mt-2 center-align">
                                                    <h4>THANH TOÁN</h4>
                                                </div>
                                            </div>
                                            <div class="col s12 order-info ">
                                                <div class="step-wrap hide-on-med-and-down ">
                                                    <ul class="process-list">
                                                        <li class="process-item active"><i
                                                            class="material-icons">shopping_cart</i>
                                                            <p>GIỎ HÀNG</p>
                                                        </li>
                                                        <li class="process-item active"><i class="material-icons">map</i>
                                                            <p>CHỌN ĐỊA CHỈ NHẬN HÀNG</p>
                                                        </li>
                                                        <li class="process-item"><i class="material-icons">attach_money</i>
                                                            <p>ĐẶT CỌC VÀ KẾT ĐƠN</p>
                                                        </li>
                                                    </ul>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row pb-5">
                                <div class="col s12 m12 l7">
                                    <div class="address-wrapper card-panel">
                                        <div class="title-block">
                                            <h5>NGƯỜI ĐẶT HÀNG</h5>
                                        </div>
                                        <div class="address-block">
                                            <div class="row">
                                                <div class="input-field col s12 m12 l12">
                                                    <asp:TextBox ID="txt_Fullname" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    <label for="info_name" class="active">Họ và tên</label>
                                                </div>
                                                <div class="input-field col s12 m12 l12">
                                                    <asp:TextBox ID="txt_Phone" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    <label for="info_number" class="active">Số điện thoại</label>
                                                </div>
                                                <div class="input-field col s12 m12 l12">
                                                    <asp:TextBox ID="txt_Email" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    <label for="info_email" class="active">Email</label>
                                                </div>
                                                <div class="input-field col s12 m12 l12">
                                                    <asp:TextBox ID="txt_Address" runat="server" ReadOnly="true" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                    <label for="info_address" class="active">Địa chỉ</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="address-wrapper card-panel">
                                        <div class="title-block">
                                            <h5>THÔNG TIN NHẬN HÀNG</h5>
                                        </div>
                                        <div class="address-block">
                                            <div class="row">
                                                <div class="input-field col s12 m12 l12">
                                                    <asp:TextBox ID="txt_DFullname" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rq1" ControlToValidate="txt_DFullname" runat="server"
                                                        ErrorMessage="Không được để rỗng." ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <label for="info_name" class="active">Họ và tên</label>
                                                </div>
                                                <div class="input-field col s12 m12 l12">
                                                    <asp:TextBox ID="txt_DPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="Dynamic" ControlToValidate="txt_DPhone" runat="server" ErrorMessage="Không được để rỗng." ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <label for="info_number" class="active">Số điện thoại</label>
                                                </div>
                                                <div class="input-field col s12 m12 l12">
                                                    <asp:TextBox ID="txt_DEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txt_DEmail" runat="server" ErrorMessage="Không được để rỗng." ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <label for="info_email" class="active">Email</label>
                                                </div>
                                                <div class="input-field col s12 m12 l12">
                                                    <asp:TextBox ID="txt_DAddress" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" ControlToValidate="txt_DAddress" runat="server" ErrorMessage="Không được để rỗng." ForeColor="Red"></asp:RequiredFieldValidator>
                                                    <label for="info_address" class="active">Địa chỉ nhận hàng</label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col s12 m12 l5">
                                    <div class="list-product-item card-panel">
                                        <asp:Literal runat="server" ID="ltr_pro"></asp:Literal>
                                    </div>
                                    <div class="total-checkout-step card-panel">
                                        <div class="list-fee">
                                            <asp:Literal runat="server" ID="ltrTotal"></asp:Literal>
                                            <div class="fee-row">
                                                <label>
                                                    <input type="checkbox" id="chk_DK" />
                                                    <span>Tôi đồng ý với các <a href="/chuyen-muc/chinh-sach/chinh-sach-khieu-nai" style="color: blue;" target="_blank">điều khoản đặt hàng</a> của
                                                        <asp:Literal runat="server" ID="ltrTitle"></asp:Literal></span>
                                                </label>
                                            </div>
                                            <div class="fee-row warning" style="display: none">
                                                <span style="color: red">Vui lòng xác nhận trước khi hoàn tất đơn hàng.</span>
                                            </div>
                                            <div class="checkout-btn">
                                                <a href="javascript:;" id="finishorder" class="btn">Hoàn tất</a>
                                                <asp:Button ID="btn_saveOrder" runat="server" OnClick="btn_saveOrder_Click" Text="HOÀN TẤT" Style="display: none;"
                                                    CssClass="right btn pill-btn primary-btn  main-btn hover" />
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
    <asp:HiddenField ID="hdfTeamWare" runat="server" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('select').formSelect();
        });

        $("#finishorder").click(function () {
            var check = $("#chk_DK").is(':checked');
            if (check) {
                var html = "";
                var warehouse = true;
                $(".shop-item-wrap").each(function () {
                    var obj = $(this);
                    var id = obj.attr("data-id");
                    var warehouseID = obj.find(".warehoseselect").val();
                    var warehousefromID = obj.find(".warehosefromselect").val();
                    var shippingtype = obj.find(".shippingtypesselect").val();

                    if (warehouseID == 0 || warehousefromID == 0 || shippingtype == 0) {
                        warehouse = false;
                    }
                    //var shippingtype = "1";
                    html += id + ":" + warehouseID + "-" + shippingtype + "-" + warehousefromID + "|";
                });

                if (warehouse == false) {
                    alert('Bạn chưa chọn kho TQ hoặc kho VN hoặc phương thức VC!');
                }
                else {
                    $("#<%= hdfTeamWare.ClientID%>").val(html);
                    $("#<%= btn_saveOrder.ClientID%>").click();
                }
            }
            else {
                $(".warning").attr('style', 'display:block');
            }
        });

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

        .shop-item-wrap .shop-name {
            padding-top: 5px;
            padding-bottom: 5px;
            font-weight: bold;
        }

        .shop-item-wrap .list-product .product {
            padding-top: 15px;
            padding-bottom: 15px;
            border-bottom: 1px solid #ccc;
        }

            .shop-item-wrap .list-product .product .pd-top {
                display: -webkit-box;
                display: -ms-flexbox;
                display: flex;
                -ms-flex-wrap: nowrap;
                flex-wrap: nowrap;
                -webkit-box-align: start;
                -ms-flex-align: start;
                align-items: flex-start;
            }

                .shop-item-wrap .list-product .product .pd-top .product-img {
                    width: 50px;
                }

                    .shop-item-wrap .list-product .product .pd-top .product-img img {
                        width: 100%;
                    }

                .shop-item-wrap .list-product .product .pd-top .product-name {
                    -webkit-box-flex: 1;
                    flex: 1;
                    -webkit-flex: 1;
                    -ms-flex: 1;
                    padding-left: 15px;
                    font-size: 14px;
                    font-weight: 500;
                }

            .shop-item-wrap .list-product .product .pd-middle {
                display: -webkit-box;
                display: -ms-flexbox;
                display: flex;
                -webkit-box-pack: justify;
                -ms-flex-pack: justify;
                justify-content: space-between;
                -ms-flex-wrap: nowrap;
                flex-wrap: nowrap;
                padding-left: 65px;
            }

        .shop-item-wrap .services-collect .title-service {
            margin: 0.5rem 0;
        }

        .shop-item-wrap .services-collect .list-services-wrap {
            display: flex;
            flex-flow: wrap;
        }

            .shop-item-wrap .services-collect .list-services-wrap label {
                margin: 5px 0;
                width: 50%;
            }

        .shop-item-wrap .fee-item {
            border: 0;
            -webkit-box-shadow: none;
            box-shadow: none;
            background: #d9d9d9;
            padding: 15px;
            margin-top: 1rem;
        }

            .shop-item-wrap .fee-item .collapsible-header {
                border: 0;
                padding: 0;
                text-align: center;
                display: block;
                font-weight: 700;
                border-radius: 4px;
            }

            .shop-item-wrap .fee-item .collapsible-body {
                padding: 0;
                border-bottom: 0;
            }

        .shop-item-wrap .list-fee {
            padding-top: 15px;
        }

        .fee-row {
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -webkit-box-pack: justify;
            -ms-flex-pack: justify;
            justify-content: space-between;
            padding-top: 5px;
            padding-bottom: 5px;
        }

            .fee-row .col-fix:last-child {
                -ms-flex-wrap: nowrap;
                flex-wrap: nowrap;
            }

        .total-checkout-step {
            padding: 15px 24px;
        }

        .fee-row {
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -webkit-box-pack: justify;
            -ms-flex-pack: justify;
            justify-content: space-between;
            padding-top: 5px;
            padding-bottom: 5px;
        }

        .total-checkout-step .checkout-btn .btn {
            width: 100%;
        }
    </style>
</asp:Content>

