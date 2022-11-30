<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="Tariff-TQVN-Detail.aspx.cs" Inherits="NHST.manager.Tariff_TQVN_Detail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <h1 class="page-title">Cấu hình phí vận chuyển TQ-VN</h1>
        <div class="grid-row">
            <div class="grid-col" id="main-col-wrap">
                <div class="feat-row grid-row">
                    <div class="grid-col-50 grid-row-center">
                        <asp:Panel runat="server" ID="Parent">
                            <article class="pane-primary">
                                <div class="heading">
                                    <h3 class="lb">Cấu hình phí vận chuyển TQ - VN</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <asp:Label ID="lbl_check" runat="server" EnableViewState="false" Visible="false" ForeColor="Red"></asp:Label>
                                        <div class="row">
                                            <%--<div class="form-row col-md-12">
                                                <label for="exampleInputEmail">
                                                    Chọn kho                                            
                                                </label>
                                                <asp:DropDownList ID="ddlReceivePlace" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="Kho Hà Nội" Text="Kho Hà Nội"></asp:ListItem>
                                                    <asp:ListItem Value="Kho Việt Trì" Text="Kho Việt Trì"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>--%>
                                            <div class="form-row col-md-12">
                                                <label for="exampleInputEmail">
                                                    Cân nặng từ
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="pWeightFrom" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </label>
                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                    ID="pWeightFrom" MinValue="0" NumberFormat-DecimalDigits="2"
                                                    NumberFormat-GroupSizes="3" Width="100%">
                                                </telerik:RadNumericTextBox>
                                            </div>
                                            <div class="form-row col-md-12">
                                                <label for="exampleInputEmail">
                                                    Cân nặng đến
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="pWeightTo" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </label>
                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                    ID="pWeightTo" MinValue="0" NumberFormat-DecimalDigits="2"
                                                    NumberFormat-GroupSizes="3" Width="100%">
                                                </telerik:RadNumericTextBox>
                                            </div>
                                            <div class="form-row col-md-12">
                                                <label for="exampleInputEmail">
                                                    Phí
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="pAmount" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                                </label>
                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                    ID="pAmount" MinValue="0" NumberFormat-DecimalDigits="2"
                                                    NumberFormat-GroupSizes="3" Width="100%">
                                                </telerik:RadNumericTextBox>
                                            </div>
                                            <div class="form-row no-margin center-txt">
                                                <asp:Button runat="server" ID="btnSave" Text="Lưu" CssClass="btn primary-btn" ValidationGroup="n" OnClick="btnSave_Click" />
                                                <a href="/manager/tariff-tqvn.aspx" class="btn primary-btn">Trở về</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </article>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </main>
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlRole">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlSaleGroup" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <script type="text/javascript">
        function keypress(e) {
            var keypressed = null;
            if (window.event) {
                keypressed = window.event.keyCode; //IE
            }
            else {
                keypressed = e.which; //NON-IE, Standard
            }
            if (keypressed < 48 || keypressed > 57) {
                if (keypressed == 8 || keypressed == 127) {
                    return;
                }
                return false;
            }
        }
    </script>
</asp:Content>
