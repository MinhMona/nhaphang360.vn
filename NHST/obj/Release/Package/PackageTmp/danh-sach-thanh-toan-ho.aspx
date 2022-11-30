<%@ Page Language="C#" MasterPageFile="~/UserMasterNew.Master" AutoEventWireup="true" CodeBehind="danh-sach-thanh-toan-ho.aspx.cs" Inherits="NHST.danh_sach_thanh_toan_ho" %>

<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
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
                                        <h4>DANH SÁCH YÊU CẦU THANH TOÁN HỘ</h4>
                                    </div>
                                </div>
                                <div class="col s12">
                                    <div class="order-list-info">
                                        <div class="total-info mb-2">
                                            <div class="row section">
                                                <div class="col s12">

                                                    <div class="responsive-tb">
                                                        <table class="table    highlight bordered  centered    mt-2">
                                                            <thead>
                                                                <tr>
                                                                    <th class="tb-date">Ngày gửi</th>
                                                                    <th>Tổng tiền (¥)</th>
                                                                    <th>Tổng tiền (VNĐ)</th>
                                                                    <th>Tỉ giá</th>
                                                                    <th>Trạng thái</th>
                                                                    <th>Action</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                                                            </tbody>
                                                        </table>
                                                    </div>

                                                    <div class="pagi-table float-right mt-2">
                                                        <%this.DisplayHtmlStringPaging1();%>
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
    <asp:HiddenField ID="hdfTradeID" runat="server" />
    <asp:HiddenField ID="hdflist" runat="server" />
    <asp:HiddenField ID="hdfAmount" runat="server" />
    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Style="display: none" />
    <asp:Button ID="btnPayment" runat="server" OnClick="btnPayment_Click" Style="display: none" />
    <script type="text/javascript">
        function deleteTrade(ID) {
            var r = confirm("Bạn muốn hủy yêu cầu này?");
            if (r == true) {
                $("#<%=hdfTradeID.ClientID%>").val(ID);
                $("#<%=btnCancel.ClientID%>").click();
            }
            else {

            }
        }
        function paymoney_old(ID) {
            var r = confirm("Bạn muốn thanh toán yêu cầu này?");
            if (r == true) {
                $("#<%=hdfTradeID.ClientID%>").val(ID);
                $("#<%=btnPayment.ClientID%>").click();
            }
            else {

            }

        }
        function paymoney(obj, ID) {
            var r = confirm("Bạn muốn thanh toán yêu cầu này?");
            if (r == true) {
                obj.removeAttr("onclick");
                $("#<%=hdfTradeID.ClientID%>").val(ID);
                $("#<%=btnPayment.ClientID%>").click();
            }
            else {

            }

        }

    </script>
</asp:Content>
