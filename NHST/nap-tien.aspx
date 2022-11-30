<%@ Page Language="C#" MasterPageFile="~/UserMasterNew.Master" AutoEventWireup="true" CodeBehind="nap-tien.aspx.cs" Inherits="NHST.nap_tien" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
                                        <h4>NẠP TIỀN VNĐ</h4>
                                    </div>
                                </div>
                                <div class="col s12">
                                    <div class="order-list-info">
                                        <div class="total-info mb-2">
                                            <div class="row section">
                                                <div class="col s12">
                                                    <p class="center-align">
                                                        Tổng tiền đã nạp: <span
                                                            class="teal-text text-darken-4 font-weight-700">
                                                            <asp:Literal runat="server" ID="lblTotalIncome"></asp:Literal>
                                                            VNĐ</span> <span class="black-text divi">|</span> Số dư hiện
                                                    tại: <span
                                                        class="teal-text text-darken-4 font-weight-700">
                                                        <asp:Literal runat="server" ID="lblAccount"></asp:Literal></span>
                                                        VNĐ
                                                    </p>

                                                    <div class="donate-guide">
                                                        <h6 class="font-weight-700 mb-2 mt-2">QUY ĐỊNH HÌNH THỨC THANH
                                                        TOÁN</h6>
                                                        <p class="pl-1">
                                                            Để kết thúc quá trình đặt hàng, quý khách
                                                        <strong>thanh toán</strong> một khoản tiền <strong>đặt
                                                            cọc trước cho NHẬP HÀNG 360 </strong> để chúng tôi thực hiện
                                                        giao dịch mua hàng theo yêu cầu trên đơn hàng.
                                                        </p>
                                                        <p class="pl-2">
                                                            - Số tiền <strong>đặt cọc</strong> trước bao gồm:
                                                        </p>
                                                        <p class="pl-3">
                                                            + <strong>Tiền hàng</strong>: giá sản phẩm trên
                                                        website đặt hàng Trung Quốc, số tiền này <strong> thu hộ cho </strong>
                                                        nhà cung cấp.
                                                        </p>
                                                        <p class="pl-3">
                                                            + <strong>Phí dịch vụ</strong>: là phí khách hàng
                                                        trả cho <strong> NHẬP HÀNG 360 </strong> để tiến hành thu mua theo đơn hàng đã đặt.
                                                        </p>
                                                        <h6 class="font-weight-700">CÓ 2 HÌNH THỨC THANH TOÁN:</h6>
                                                        <p class="pl-1">1. Thanh toán trực tiếp.</p>
                                                        <p class="pl-2">
                                                            - Khách hàng có thể đặt cọc trực tiếp tại địa chỉ:
                                                        </p>
                                                        <p class="pl-3">
                                                           <asp:Literal runat="server" ID="ltrDiaChi1"></asp:Literal>
                                                            <asp:Literal runat="server" ID="ltrDiaChi2"></asp:Literal>
                                                        </p>
                                                        <p class="pl-1">2. Chuyển khoản trực tiếp.</p>
                                                        <p class="pl-2">- Nội dung chuyển khoản theo cú pháp:</p>
                                                        <p class="pl-3">+ <strong>NAP tentaikhoan sodienthoai</strong></p>
                                                        <h6 class="font-weight-700 mb-2 mt-2">THÔNG TIN NGÂN HÀNG:</h6>
                                                        <div class="row">
                                                            <asp:Literal runat="server" ID="ltrBank"></asp:Literal>
                                                        </div>


                                                    </div>
                                                    <div class="card-panel donate-information mt-3">
                                                        <h5>Xác nhận chuyển khoản</h5>
                                                        <hr />
                                                        <div class="order-row">
                                                            <div class="left-fixed">
                                                                <span>Đã chuyển vào tài khoản:</span>
                                                            </div>
                                                            <div class="right-content">
                                                                <div class="row">
                                                                    <div class=" col s12">
                                                                        <asp:DropDownList ID="ddlBank" runat="server" DataTextField="BankInfo" CssClass="icons"
                                                                            DataValueField="ID">
                                                                        </asp:DropDownList>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="order-row">
                                                            <div class="left-fixed">
                                                                <span>Số tiền:</span>
                                                            </div>
                                                            <div class="right-content">
                                                                <div class="row">
                                                                    <div class=" col s12">
                                                                        <telerik:RadNumericTextBox runat="server" Skin="MetroTouch"
                                                                            ID="pAmount" NumberFormat-DecimalDigits="0" Value="0"
                                                                            NumberFormat-GroupSizes="3" Width="100%">
                                                                        </telerik:RadNumericTextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>                                                     

                                                        <div class="order-row">
                                                            <div class="left-fixed">
                                                                <span>Nội dung:</span>
                                                            </div>
                                                            <div class="right-content">
                                                                <div class="row">
                                                                    <div class=" col s12">
                                                                        <asp:TextBox ID="txtNote" runat="server" CssClass="materialize-textarea" TextMode="MultiLine"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                        <a href="javascript:;" onclick="AddWallet($(this))" class="btn right mt-3">Gửi xác nhận</a>
                                                        <asp:Button ID="btnSend" runat="server" Style="display: none" Text="Gửi xác nhận" CssClass="btn right mt-3" OnClick="btnSend_Click" />
                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div class="card-panel">
                                                        <h5 class=" mb-2 mt-2">Lịch sử nạp gần đây</h5>
                                                        <hr />
                                                        <div class="responsive-tb mt-3">
                                                            <table class="table    highlight bordered  centered striped">
                                                                <thead>
                                                                    <tr>
                                                                        <th class="tb-date">Ngày nạp</th>
                                                                        <th class="tb-date">Số tiền</th>
                                                                        <th class="tb-date">Nội dung</th>
                                                                        <th class="tb-date">Trạng thái</th>
                                                                        <th class="tb-date"></th>
                                                                    </tr>
                                                                </thead>
                                                                <tbody>
                                                                    <asp:Literal runat="server" ID="ltr"></asp:Literal>
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
    </div>
    <asp:Button ID="btnclear" runat="server" UseSubmitBehavior="false" OnClick="btnclear_Click" Style="display: none;" />
    <asp:HiddenField ID="hdfTradeID" runat="server" />


    <telerik:RadScriptBlock ID="sc" runat="server">
        <script type="text/javascript">
            function deleteTrade(ID) {
                var r = confirm("Bạn muốn hủy yêu cầu?");
                if (r == true) {
                    $("#<%= hdfTradeID.ClientID %>").val(ID);
                    $("#<%= btnclear.ClientID %>").click();
                } else {
                }
            }
            function AddWallet(obj) {
                var r = confirm("Xác nhận nạp tiền!");
                if (r == true) {
                    obj.removeAttr("onclick");
                    $("#<%=btnSend.ClientID%>").click();
                }
               }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
