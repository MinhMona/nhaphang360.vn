<%@ Page Language="C#" MasterPageFile="~/UserMasterNew.Master" AutoEventWireup="true" CodeBehind="nap-tien-te.aspx.cs" Inherits="NHST.nap_tien_te1" %>

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
                                        <h4>Nạp tiền tệ (¥)</h4>

                                    </div>
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col s12 m12 l5">
                                    <div class="card-panel draw-yuan">
                                        <h5>Yêu cầu nạp tệ</h5>
                                        <hr />
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <span>Số tiền nạp:</span>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class=" col s12">
                                                        <telerik:RadNumericTextBox runat="server" Skin="MetroTouch"
                                                            ID="pAmount" NumberFormat-DecimalDigits="3" Value="0"
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
                                        <asp:Button ID="btnSend" runat="server" Text="GỬI YÊU CẦU" CssClass="btn right mt-3" OnClick="btnSend_Click" />
                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                                <div class="col s12 m12 l7">
                                    <div class="card-panel">
                                        <h5>Lịch sử nạp tệ</h5>
                                        <hr />
                                        <div class="responsive-tb">
                                            <table class="table   hightlight">
                                                <thead>
                                                    <tr>
                                                        <th>Ngày nạp</th>
                                                        <th class="tb-date">Số tiền nạp</th>
                                                        <th class="tb-date">Nội dung</th>
                                                        <th>Trạng thái</th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="pagi-table float-right mt-3">
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
    <asp:Button ID="btnclear" runat="server" OnClick="btnclear_Click" Style="display: none;" />
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
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
