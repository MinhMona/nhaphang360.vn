<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="tariff-feeBuyPro-detail.aspx.cs" Inherits="NHST.manager.tariff_feeBuyPro_detail" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="grid-row">
            <div class="grid-col" id="main-col-wrap">
                <div class="feat-row grid-row">
                    <div class="grid-col-50 grid-row-center">
                        <article class="pane-primary">
                            <div class="heading">
                                <h3 class="lb">Cấu hình phí mua hàng</h3>
                            </div>
                            <div class="cont">
                                <div class="inner">
                                    <div class="form-row col-md-12">
                                        <asp:Label ID="lbl_check" runat="server" EnableViewState="false" Visible="false" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div class="form-row col-md-12">
                                        <label for="exampleInputEmail">
                                            Giá từ
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="pPriceFrom" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </label>
                                        <telerik:radnumerictextbox runat="server" cssclass="form-control width-notfull" skin="MetroTouch"
                                            id="pPriceFrom" minvalue="0" numberformat-decimaldigits="2"
                                            numberformat-groupsizes="3" width="100%">
                                        </telerik:radnumerictextbox>
                                    </div>
                                    <div class="form-row col-md-12">
                                        <label for="exampleInputEmail">
                                            Giá đến
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="pPriceTo" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </label>
                                        <telerik:radnumerictextbox runat="server" cssclass="form-control width-notfull" skin="MetroTouch"
                                            id="pPriceTo" minvalue="0" numberformat-decimaldigits="2"
                                            numberformat-groupsizes="3" width="100%">
                                        </telerik:radnumerictextbox>
                                    </div>
                                    <div class="form-row col-md-12">
                                        <label for="exampleInputEmail">
                                            Phí dịch vụ (%)
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="pFeeservice" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </label>
                                        <telerik:radnumerictextbox runat="server" cssclass="form-control width-notfull" skin="MetroTouch"
                                            id="pFeeservice" minvalue="0" numberformat-decimaldigits="2"
                                            numberformat-groupsizes="3" width="100%">
                                        </telerik:radnumerictextbox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Phí dịch vụ (¥)
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="rFeeMoney" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </label>
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="rFeeMoney" MinValue="0" NumberFormat-DecimalDigits="2"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                    <div class="form-row no-margin center-txt">
                                        <asp:Button runat="server" ID="btnSave" Text="Lưu" CssClass="btn primary-btn" ValidationGroup="n" OnClick="btnSave_Click" />
                                        <a href="/manager/Tariff-Buypro.aspx" class="btn primary-btn">Trở về</a>
                                    </div>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
