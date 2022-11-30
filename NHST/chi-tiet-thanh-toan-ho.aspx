<%@ Page Language="C#" MasterPageFile="~/UserMasterNew.Master" AutoEventWireup="true" CodeBehind="chi-tiet-thanh-toan-ho.aspx.cs" Inherits="NHST.chi_tiet_thanh_toan_ho1" %>

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
                                        <h4><asp:Literal runat="server" ID="ltrPayID"></asp:Literal></h4>
                                    </div>
                                </div>
                                <div class="col s12">

                                    <div class="row section">
                                        <div class="col s12">
                                            <div class="order-row">
                                                <div class="left-fixed">
                                                    <p class="txt">Username:</p>
                                                </div>
                                                <div class="right-content">
                                                    <div class="row">
                                                        <div class="input-field col s12">
                                                            <asp:Literal ID="ltrIfn" runat="server"></asp:Literal>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="order-row top-justify">
                                                <div class="left-fixed">
                                                    <p class="txt">Hóa đơn thanh toán hộ:</p>
                                                </div>
                                                <div class="right-content">
                                                    <div class="list-order">
                                                        <div class="row order-wrap">
                                                            <asp:Literal ID="ltrList" runat="server"></asp:Literal>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="order-row">
                                                <div class="left-fixed">
                                                    <p class="txt">Tổng tiền Tệ (¥):</p>
                                                </div>
                                                <div class="right-content">
                                                    <div class="row">
                                                        <div class="input-field col s12">
                                                            <telerik:RadNumericTextBox runat="server" Skin="MetroTouch"
                                                                ID="pAmount" NumberFormat-DecimalDigits="0" Value="0" Enabled="false"
                                                                NumberFormat-GroupSizes="3" Width="100%">
                                                            </telerik:RadNumericTextBox>
                                                            <label class="active">Tệ (¥)</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="order-row">
                                                <div class="left-fixed">
                                                    <p class="txt">Tổng tiền (VNĐ):</p>
                                                </div>
                                                <div class="right-content">
                                                    <div class="row">
                                                        <div class="input-field col s12">
                                                            <telerik:RadNumericTextBox runat="server" Skin="MetroTouch"
                                                                ID="rVND" NumberFormat-DecimalDigits="0" Value="0"
                                                                NumberFormat-GroupSizes="3" Width="100%" Enabled="false">
                                                            </telerik:RadNumericTextBox>
                                                            <label class="active">Việt Nam Đồng (VNĐ)</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="order-row">
                                                <div class="left-fixed">
                                                    <p class="txt">Tỉ giá:</p>
                                                </div>
                                                <div class="right-content">
                                                    <div class="row">
                                                        <div class="input-field col s12">
                                                            <telerik:RadNumericTextBox runat="server" Skin="MetroTouch"
                                                                ID="rTigia" NumberFormat-DecimalDigits="0" Value="0"
                                                                NumberFormat-GroupSizes="3" Width="100%" Enabled="false">
                                                            </telerik:RadNumericTextBox>
                                                            <label class="active">Tỉ giá</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="order-row">
                                                <div class="left-fixed">
                                                    <p class="txt">Ghi chú:</p>
                                                </div>
                                                <div class="right-content">
                                                    <div class="row">
                                                        <div class="input-field col s12">
                                                            <asp:TextBox ID="txtNote" runat="server" CssClass="materialize-textarea" TextMode="MultiLine"></asp:TextBox>
                                                            <label for="textarea2">Nội dung</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="order-row">
                                                <div class="left-fixed">
                                                    <p class="txt">Trạng thái:</p>
                                                </div>
                                                <div class="right-content">
                                                    <div class="row" style="float: left">
                                                        <asp:Literal runat="server" ID="lblStatus"></asp:Literal>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="float-right mt-2">
                                                <a href="/danh-sach-thanh-toan-ho" class="btn back-order">Trở về</a>
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
</asp:Content>
