<%@ Page Language="C#" MasterPageFile="~/UserMasterNew.Master" AutoEventWireup="true" CodeBehind="thong-ke-cuoc-ky-gui.aspx.cs" Inherits="NHST.thong_ke_cuoc_ky_gui" %>

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
                                        <h4>Thống kê cước ký gửi</h4>
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
                                                                    <th>ID</th>
                                                                    <th class="tb-date">Ngày YCXK</th>
                                                                    <th>Ngày XK</th>
                                                                    <th>Tổng số kiện</th>
                                                                    <th>Tổng số kg</th>
                                                                    <th>Tổng tiền</th>
                                                                    <th>HTVC</th>
                                                                    <th>Trạng thái TT</th>
                                                                    <th>Trạng thái XK</th>
                                                                    <th>Ghi chú</th>
                                                                     <th>Thao tác</th>
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
    <asp:HiddenField runat="server" ID="hdfID" />
    <asp:Button runat="server" ID="btnPay" style="display:none" OnClick="btnPay_Click" UseSubmitBehavior="false" />
    <script type="text/javascript">
         function Pay(obj, ID) {
            var c = confirm('Bạn muốn thanh toán trực tiếp?');
            if (c) {
                $("#<%=hdfID.ClientID%>").val(ID);
                $("#<%=btnPay.ClientID%>").click();
            }
        }
</script>
</asp:Content>
