<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UserMasterNew.Master" CodeBehind="nap-tien-chuyen-khoan-theo-tai-khoan.aspx.cs" Inherits="NHST.nap_tien_chuyen_khoan_theo_tai_khoan" %>

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
                                        <h4>NẠP TIỀN CHUYỂN KHOẢN THEO TÀI KHOẢN</h4>
                                    </div>
                                    <a onclick="Check()">123</a>
                                </div>
                                <div class="col s12 mt-2">
                                    <div class="order-list-info">
                                        <div class="total-info mb-2 create-product">
                                            <div class="row section">
                                                <div class="col s12">
                                                    <div class="order-row">
                                                        <div class="col m12">Ngân hàng</div>
                                                        <div class="col m12">
                                                            <p>-Viettinbank CN Tân Bình, Hồ Chí Minh - <span class="bank-title">MONA MEDIA</span> - <span class="bank-stk">90123123XXXX</span></p>
                                                            <p>-Vietcombank CN Hồ Chí Minh - <span class="bank-title">MONA MEDIA</span> - <span class="bank-stk">34534123XXX </span></p>
                                                        </div>

                                                    </div>

                                                    <div class="order-row">
                                                        <p class="grid-col-100">
                                                            Nội dung: <span class="bank-note">
                                                                <asp:Literal runat="server" ID="ltrNote"></asp:Literal></span>
                                                        </p>
                                                        <p style="color: red" class="grid-col-100">*Lưu ý: Chuyển khoản với nội dung trên để được nạp tiền vào tài khoản trong thời gian nhanh nhất.</p>
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

        function Check() {
            var ListMainOrder = '[{"MainOrderCode":"12323534543"}, {"MainOrderCode":"4546456546"}, {"MainOrderCode":"12323534564543"}]';

            var data_nhst = {
                'ListMainOrder': '' + ListMainOrder + ''
            };

            $.ajax({
                url: "/WebService1.asmx/CheckList",
                data: data_nhst,
                method: 'POST',
                contentType: 'application/x-www-form-urlencoded',
                xhrFields: {
                    withCredentials: true
                },
                headers: { 'X-Requested-With': 'XMLHttpRequest' },
                success: function (d) {

                    console.log(d);
                    //alert((new XMLSerializer()).serializeToString(d));

                    //chrome.tabs.sendMessage(sender.tab.id, { action: request.callback, response: d }, function (response) {

                    //});
                },
                error: function (event, jqXHR, ajaxSettings, thrownError) {
                    //alert('[event:' + event + '], [jqXHR:' + jqXHR + '], [ajaxSettings:' + ajaxSettings + '], [thrownError:' + thrownError + '])');
                }
            });
        }

    </script>
</asp:Content>

