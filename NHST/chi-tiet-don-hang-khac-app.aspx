<%@ Page Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="chi-tiet-don-hang-khac-app.aspx.cs" Inherits="NHST.chi_tiet_don_hang_khac_app" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" href="/App_Themes/App/css/style-NA.css" media="all">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">

    <main id="main-wrap">
        <div class="page-wrap">
             <asp:Panel ID="pnMobile" runat="server" Visible="false">
            <div class="page-body">
                <div class="all donhang donhang_list">
                    <h2 class="title_page">CHI TIẾT ĐƠN HÀNG TMĐT KHÁC</h2>
                    <asp:Label ID="ltr_info" runat="server" Visible="false" CssClass="inforshow"></asp:Label>
                    <asp:Literal runat="server" ID="ltrProduct"></asp:Literal>
                </div>
                <div class="all donhang donhang_list">
                    <div class="order-panel">
                        <div class="title" style="text-align: center;">Liên hệ</div>
                        <asp:Literal ID="ltrComment" runat="server"></asp:Literal>
                    </div>
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

    <asp:Button ID="btn_cancel" runat="server" UseSubmitBehavior="false" CssClass="btn pill-btn primary-btn main-btn hover" CausesValidation="false" Text="Hủy đơn hàng" Style="display: none;" OnClick="btn_cancel_Click" />
    <asp:Button ID="btnDeposit" runat="server" UseSubmitBehavior="false" CssClass="btn pill-btn primary-btn" Style="display: none" CausesValidation="false" Text="Đặt cọc" OnClick="btnDeposit_Click" />
    <asp:Button ID="btnPayAll" runat="server" UseSubmitBehavior="false" CssClass="btn pill-btn primary-btn main-btn hover" CausesValidation="false" Text="Thanh toán" Style="display: none;" OnClick="btnPayAll_Click" />
    <script type="text/javascript">
        function cancelOrder() {
            var r = confirm("Bạn muốn hủy đơn hàng này?");
            if (r == true) {
                $("#<%= btn_cancel.ClientID%>").click();
            }
            else {
            }
        }

        function payallorder() {
            var r = confirm("Bạn muốn thanh toán đơn hàng này?");
            if (r == true) {
                $("#<%= btnPayAll.ClientID%>").click();
            }
            else {
            }
        }

        function depositOrder() {
            var r = confirm("Bạn muốn đặt cọc đơn hàng này?");
            if (r == true) {
                $("#<%= btnDeposit.ClientID%>").click();
            }
            else {
            }
        }

        $(function () {
            var chat = $.connection.chatHub;
            $.connection.hub.start().done(function () {
                $('#sendnotecomment').click(function () {
                    var obj = $('#sendnotecomment');
                    var parent = obj.parent();
                    var commentext = parent.find(".comment-text").val();
                    var shopid = obj.attr("order");
                    var uid = obj.attr("uid");
                    if (commentext == "" || commentext == null) {
                        alert("Vui lòng không để trống nội dung");
                    }
                    else {
                        $.ajax({
                            type: "POST",
                            url: "/chi-tiet-don-hang-khac-app.aspx/PostComment",
                            data: "{commentext:'" + commentext + "',shopid:'" + shopid + "',UID:'" + uid + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (msg) {
                                var data = msg.d;
                                if (data != "0") {
                                    var outdata = data.split('|');
                                    var id = outdata[0];
                                    var message = outdata[1];
                                    swal({
                                        title: 'Thông báo',
                                        text: 'Gửi nội dung thành công',
                                        type: 'success'
                                    },
                                        function () {
                                            window.location.replace(window.location.href);
                                        });
                                    chat.server.send("Đánh giá",
                                        "<li role=\"presentation\"><a href=\"javascript:;\" onclick=\"acceptdaxem('" + id + "','" + shopid + "','2')\">" + message + "</a></li>");
                                }
                            },
                            error: function (xmlhttprequest, textstatus, errorthrow) {
                                //alert('lỗi post comment');
                            }
                        });
                    }
                });
            });
        });

    </script>
    <style>
        .pane-primary .heading {
            background-color: #366136 !important;
        }

        .float-left-tb {
            float: left;
            width: 50%;
        }

        .comment_content {
            min-height: 0px;
            text-align: left;
            vertical-align: top;
            border: 1px solid #E3E3E3;
            background: #FFFFF0;
            color: #000;
            padding: 10px;
            font-size: 16px;
            max-height: 300px;
            overflow-y: scroll;
        }

        .user-comment {
            color: black;
            font-weight: bold;
        }

        .font-size-10 {
            font-size: 10px;
        }

        .comment-text {
            float: left;
            width: 85%;
            padding: 5px 10px;
        }

        input, select {
            border: 1px solid #e1e1e1;
            background: #fff;
            padding: 10px;
            height: 40px;
            line-height: 20px;
            color: #000;
            display: block;
            width: 100%;
            border-radius: 0;
        }

        #sendnotecomment {
            padding: 0px 10px;
            float: right;
            line-height: 40px;
        }

        .order-panel {
            float: left;
            width: 100%;
            margin-bottom: 30px;
            padding: 10px;
            line-height: 1.6;
            box-shadow: 0px 0px 5px 0px rgba(0, 0, 0, 0.2);
            background: #fff;
        }

            .order-panel .title {
                text-transform: uppercase;
                color: #2b2e4a;
                font-weight: bold;
                font-size: 16px;
                padding-bottom: 5px;
                border-bottom: solid 1px #fff;
                margin-bottom: 10px;
            }

            .order-panel .cont {
                display: block;
                width: 100%;
            }

            .order-panel .bottom {
                border-top: solid 1px #fff;
                padding-top: 10px;
                margin-top: 10px;
                text-align: right;
            }

                .order-panel .bottom .btn {
                    text-transform: uppercase;
                    font-weight: bold;
                }

            .order-panel dl {
                float: none;
            }

                .order-panel dl dt {
                    width: 60%;
                }

            .order-panel textarea {
                width: 100%;
                height: 60px;
            }

            .order-panel table.ratting-tb {
                width: 100%;
            }

                .order-panel table.ratting-tb th {
                    height: 40px;
                    font-weight: bold;
                    border-bottom: solid 2px #ebebeb;
                    vertical-align: middle;
                }

                .order-panel table.ratting-tb td {
                    height: 40px;
                    vertical-align: middle;
                }

            .order-panel table.tb-product {
                width: 100%;
            }

                .order-panel table.tb-product caption {
                    color: #ff7e67;
                    text-transform: uppercase;
                    font-size: 16px;
                    text-align: left;
                    line-height: 50px;
                    font-weight: bold;
                }

                .order-panel table.tb-product th {
                    vertical-align: middle;
                    background-color: #f8f8f8;
                }

                .order-panel table.tb-product td {
                    vertical-align: top;
                }

                .order-panel table.tb-product td, .order-panel table.tb-product th {
                    padding: 10px;
                    border: solid 1px #ebebeb;
                }

                .order-panel table.tb-product .qty input {
                    display: block;
                    margin: 0 auto;
                    text-align: center;
                }

            .order-panel .table-wrap + .table-wrap {
                margin-top: 20px;
            }

            .order-panel .table-wrap {
                width: 100%;
                overflow: auto;
            }

                .order-panel .table-wrap table {
                    min-width: 600px;
                }

            .order-panel + .order-panel {
                margin-left: 30px;
            }

            .order-panel.greypn {
                background-color: #eeeeee;
            }

        .green {
            color: green;
        }
    </style>

</asp:Content>
