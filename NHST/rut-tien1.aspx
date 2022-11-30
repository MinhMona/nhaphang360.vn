<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMasterNew.Master" AutoEventWireup="true" CodeBehind="rut-tien1.aspx.cs" Inherits="NHST.rut_tien1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="grid-row">
            <div class="grid-col" id="main-col-wrap">
                <div class="feat-row grid-row">
                    <div class="grid-col-80 grid-row-center">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Rút tiền</h3>
                            </div>
                            <div class="cont">
                                <table class="customer-table mar-top1 full-width normal-table">
                                    <tr style="font-weight: bold">
                                        <td>Số dư tài khoản
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAccount" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <div class="inner">
                                    <div class="form-row marbot2">
                                        <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                    </div>
                                    <div class="form-row marbot1">
                                        Số tiền cần rút:
                                    </div>
                                    <div class="form-row marbot2">
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                            ID="pAmount" NumberFormat-DecimalDigits="0" MinValue="100000"
                                            NumberFormat-GroupSizes="3" Width="100%" placeholder="Số tiền muốn rút" Value="100000">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                     <div class="form-row marbot1">
                                        Số tài khoản:
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox ID="txtBankNumber" runat="server" CssClass="form-control full-width"></asp:TextBox>
                                    </div>
                                     <div class="form-row marbot1">
                                        Người hưởng:
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox ID="txtBeneficiary" runat="server" CssClass="form-control full-width"></asp:TextBox>
                                    </div>
                                     <div class="form-row marbot1">
                                        Ngân hàng:
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox ID="txtBankAddress" runat="server" CssClass="form-control full-width"></asp:TextBox>
                                    </div>
                                    <div class="form-row marbot1">
                                        Nội dung:
                                    </div>
                                    <div class="form-row marbot2">
                                        <asp:TextBox ID="txtContent" runat="server" CssClass="form-control full-width" TextMode="MultiLine" Height="150px"></asp:TextBox>
                                    </div>
                                    <div class="form-row no-margin center-txt">
                                        <a href="javascript:;" onclick="confirmrutien()" class="pill-btn btn order-btn main-btn hover">Tạo lệnh rút tiền</a>
                                    </div>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
            </div>
        </div>
        <div class="grid-row">
            <div class="grid-col" id="main-col-wrap">
                <div class="feat-row grid-row">
                    <div class="grid-col-80 grid-row-center">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Danh sách rút tiền</h3>
                            </div>
                            <div class="cont">
                                <div class="step-income">
                                    <table class="customer-table mar-top1 full-width normal-table">
                                        <tr>
                                            <th width="20%" style="text-align: center">Ngày giờ</th>
                                            <th width="20%" style="text-align: center">Số tiền</th>
                                            <th width="20%" style="text-align: center">Trạng thái</th>
                                            <th width="20%" style="text-align: center"></th>
                                        </tr>
                                        <asp:Literal ID="ltr" runat="server"></asp:Literal>
                                    </table>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
            </div>
        </div>
    </main>
    <%--<main>
        <div id="primary" class="index">
            <section id="firm-services" class="sec sec-padd-50">
                <div class="container">
                    <h3 class="sec-tit text-center">
                        <span class="sub">Rút tiền</span>
                    </h3>
                    
                </div>
            </section>
        </div>
    </main>--%>
    <asp:Button ID="btnCreate" runat="server" Text="Tạo lệnh rút tiền" CssClass="btn btn-success btn-block pill-btn primary-btn"
        OnClick="btnCreate_Click" Style="display: none" />
    <style>
        .width-not-full {
            float: left;
            width: auto;
            margin: 10px 20px 0 0;
        }

        .center-data th, .center-data td {
            text-align: center;
        }
    </style>
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
