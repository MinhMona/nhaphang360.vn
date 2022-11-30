<%@ Page Language="C#" MasterPageFile="~/UserMasterNew.Master" AutoEventWireup="true" CodeBehind="rut-tien.aspx.cs" Inherits="NHST.rut_tien" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
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
                                        <h4>Rút tiền</h4>

                                    </div>
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col s12 m12 l5">
                                    <div class="card-panel draw-yuan">
                                        <h5>Yêu cầu rút tiền</h5>
                                        <hr />
                                        <div class="order-row">
                                            <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <span>Số dư hiện tại:</span>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class=" col s12">
                                                        <p class="black-text font-weight-700">
                                                            <asp:Literal runat="server" ID="lblAccount"></asp:Literal>
                                                        </p>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <span>Số tiền rút:</span>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class=" col s12">
                                                        <telerik:RadNumericTextBox runat="server" Skin="MetroTouch"
                                                            ID="pAmount" NumberFormat-DecimalDigits="0" MinValue="100000"
                                                            NumberFormat-GroupSizes="3" Width="100%" placeholder="Số tiền muốn rút" Value="100000">
                                                        </telerik:RadNumericTextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <span>Số tài khoản:</span>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class=" col s12">
                                                        <asp:TextBox ID="txtBankNumber" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <span>Người hưởng:</span>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class=" col s12">
                                                        <asp:TextBox ID="txtBeneficiary" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <span>Ngân hàng:</span>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class=" col s12">
                                                        <asp:TextBox ID="txtBankAddress" runat="server"></asp:TextBox>
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
                                                        <asp:TextBox ID="txtContent" runat="server" CssClass="materialize-textarea" TextMode="MultiLine" Height="150px"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <a class="btn right mt-3" onclick="confirmrutien()">Gửi yêu cầu</a>
                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                                <div class="col s12 m12 l7">
                                    <div class="card-panel">
                                        <h5>Lịch sử rút tiền</h5>
                                        <hr />
                                        <div class="responsive-tb">
                                            <table class="table   hightlight">
                                                <thead>
                                                    <tr>
                                                        <th>Ngày rút</th>
                                                        <th class="tb-date">Số tiền rút</th>
                                                        <th>Nội dung</th>
                                                        <th>Trạng thái</th>
                                                        <th></th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <asp:Literal ID="ltr" runat="server"></asp:Literal>
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
    <asp:Button ID="btnCreate" runat="server" Text="Tạo lệnh rút tiền" UseSubmitBehavior="false" CssClass="btn btn-success btn-block pill-btn primary-btn"
        OnClick="btnCreate_Click" Style="display: none" />

    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnCreate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pn" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlRole">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlSaleGroup" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadScriptBlock ID="s" runat="server">
        <script type="text/javascript">
            function confirmrutien() {
                var r = confirm("Bạn muốn tạo lệnh rút tiền?");
                if (r == true) {
                    $("#<%=btnCreate.ClientID%>").click();
                }
                else {
                }
            }
            function cancelwithdraw(id) {
                var r = confirm("Bạn muốn hủy lệnh rút tiền?");
                if (r == true) {
                    $.ajax({
                        type: "POST",
                        url: "rut-tien.aspx/cancelwithdraw",
                        data: "{ID:'" + id + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            var ret = msg.d;
                            if (ret == "1") {
                                location.reload();
                            }
                            else {
                                alert('Có lỗi trong quá trình xử lý, vui lòng thử lại sau');
                            }
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            //alert('lỗi');
                        }
                    });
                }
                else {
                }

            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>

