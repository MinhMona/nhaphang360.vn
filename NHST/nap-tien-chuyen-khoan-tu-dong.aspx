<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UserMasterNew.Master" CodeBehind="nap-tien-chuyen-khoan-tu-dong.aspx.cs" Inherits="NHST.nap_tien_chuyen_khoan_tu_dong" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
     <style>
        .bank-title {
            font-size: 15px;
            font-weight: bold;
        }

        .bank-stk {
            font-size: 18px;
            color: darkblue;
        }

        .bank-note {
            font-size: 25px;
            color: red;
            font-weight: bold;
        }

        .grid-col-100 {
            width: 100%;
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
                                        <h4>NẠP TIỀN CHUYỂN KHOẢN TỰ ĐỘNG</h4>
                                    </div>
                                </div>
                                <div class="col s12 mt-2">
                                    <div class="order-list-info">
                                        <div class="total-info mb-2 create-product">
                                            <div class="row section">
                                                <div class="col s12">
                                                    <div class="order-row">
                                                        <div class="col m2">Chọn ngân hàng</div>
                                                        <div class="col m7">
                                                            <asp:DropDownList runat="server" ID="ddlBank" CssClass="form-control txtsearchfield">
                                                                <asp:ListItem Value="7" Text="Vietcombank"></asp:ListItem>
                                                                <asp:ListItem Value="8" Text="Viettinbank"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col m3">
                                                            <a class="btn primary-btn" onclick="GetBankInfo()" href="javascript:;">Bắt đầu</a>
                                                        </div>
                                                    </div>

                                                    <div class="order-row" style="text-align: center" id="BankContent">

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
    <asp:HiddenField ID="hdflist" runat="server" />
    <asp:HiddenField ID="hdfAmount" runat="server" />
    <script>
       function addLoading() {
            $("#loadingajax").show();
        }
        function removeLoading() {
            $("#loadingajax").hide();
        }

 function GetBankInfo() {
            var bid = $("#<%=ddlBank.ClientID%>").val();
            addLoading();
            $.ajax({
                type: "POST",
                url: "nap-tien-chuyen-khoan-tu-dong.aspx/GetBankInfo",
                data: "{BankID:'" + bid + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != null) {
                        var data = JSON.parse(msg.d);
                        var html = '';
                        html += "<div class=\"grid-col-100\">";
                        html += " <p class=\"bank-title\">Chuyển đến số tài khoản " + data.BankName + " <span class=\"bank-stk\">" + data.BankNumber + " </span>(" + data.AccountHolder + ")</p><br />";
                        html += "<p>(Vui lòng chuyển đúng <span class=\"bank-title\">NỘI DUNG</span> dưới đây. Sau khi chuyển bấm <span class=\"bank-title\">KIỂM TRA </span>để hoàn thành)</p>";
                        html += "</div>";
                        html += "<div class=\"grid-col-100\">";
                        html += "<span class=\"bank-note\">" + data.Note + "</span>";
                        html += "</div>";
                        html += "<div class=\"grid-col-100\">";
                        html += "<a class=\"btn primary-btn\" onclick=\"Check('" + data.Note + "','" + data.BankID + "')\" href=\"javascript:;\">Kiểm tra</a>";
                        html += "</div>";
                        $("#BankContent").html(html);
                    }
                    removeLoading();
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    // alert('lỗi');
                    removeLoading();
                }
            });
        }


         function Check(Note, BankID) {
            var bid = $("#<%=ddlBank.ClientID%>").val();
            addLoading();
            $.ajax({
                type: "POST",
                url: "nap-tien-chuyen-khoan-tu-dong.aspx/CheckPayment",
                data: "{BankID:'" + BankID + "',Note:'" + Note + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d != "null") {
                        swal({
                                title: "Thông báo!",
                                text: "Nạp tiền thành công!",
                                type: "success",
                                showCancelButton: false,
                                confirmButtonClass: "btn-danger",
                                confirmButtonText: "Ok",
                                closeOnConfirm: true
                            },
                                function () {
                                    location.reload();
                                });
                    }
                    else {
                        alert('Có lỗi trong quá trình xử lý, vui lòng thử lại.');
                    }
                    removeLoading();
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    // alert('lỗi');
                    removeLoading();
                }
            });
        }

    </script>
</asp:Content>
