<%@ Page Title="Lịch sử giao dịch app" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="lich-su-giao-dich-app.aspx.cs" Inherits="NHST.lich_su_giao_dich_app" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <style>
        .ip-with-sufix .fcontrol {
            background-color: #fff;
        }

        .thanhtoanho-list {
            margin-bottom: 15px;
        }

        .page-title {
            text-align: center;
            padding: 10px 20px;
            font-size: 20px;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <main id="main-wrap">
        <div class="page-wrap">
            <asp:Panel ID="pnMobile" runat="server" Visible="false">
                <div class="page-body">
                    <div class="all">
                        <h1 class="page-title">LỊCH SỬ GIAO DỊCH</h1>
                    </div>
                    <div class="thanhtoanho-list">
                        <div class="all">
                            <div class="order-group offset15">
                                <div class="heading">
                                    <p class="left-lb">
                                        <span class="circle-icon">
                                            <img src="/App_Themes/App/images/icon-budget.png" style="height: 14px" alt=""></span> Số dư
                                    </p>
                                    <p class="right-meta bold hl-txt">
                                        <span class="price">
                                            <asp:Literal runat="server" ID="ltrAccount"></asp:Literal></span>
                                    </p>
                                </div>
                                <ul class="stripe-ul">
                                    <asp:Literal runat="server" ID="ltrHis"></asp:Literal>
                                </ul>
                            </div>

                        </div>
                    </div>


                    <div class="tbl-footer clear">
                        <div class="subtotal fr">
                            <asp:Literal ID="ltrTotal" runat="server"></asp:Literal>
                        </div>
                        <div class="all">
                            <div class="pagenavi fl">
                                <%this.DisplayHtmlStringPaging1();%>
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
    <asp:HiddenField ID="hdfTradeID" runat="server" />
    <asp:HiddenField ID="hdflist" runat="server" />
    <asp:HiddenField ID="hdfAmount" runat="server" />

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

        $(".btn_seemore").click(function () {
            if ($(this).parents().parents().children(".table_pay").css("display") == "none")
                $(this).parents().parents().children(".table_pay").css("display", "");
            else
                $(this).parents().parents().children(".table_pay").css("display", "none");
        });


        $('.navbar-toggle').on('click', function (e) {
            $(this).toggleClass('open');
            $('body').toggleClass('menuin');
        });
        $('.nav-overlay').on('click', this, function (e) {
            $('.navbar-toggle').trigger('click');
        });
        $('.dropdown').on('click', '.dropdown-toggle', function (e) {

            var $this = $(this);
            var parent = $this.parent('.dropdown');
            var submenu = parent.find('.sub-menu-wrap');
            parent.toggleClass('open').siblings().removeClass('open');
            e.stopPropagation();

            submenu.click(function (e) {
                e.stopPropagation();
            });


        });
        $('body,html').on('click', function () {

            if ($('.dropdown').hasClass('open')) {

                $('.dropdown').removeClass('open');
            }
        });
        $(document).on('click', '.block-toggle', function (e) {
            e.preventDefault();
            var target = $(this).attr('href');
            if (!target) return;
            $(target).slideToggle();
        });
    </script>

</asp:Content>
