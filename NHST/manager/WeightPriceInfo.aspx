<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="WeightPriceInfo.aspx.cs" Inherits="NHST.manager.WeightPriceInfo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <main id="main-wrap">
            <div class="grid-row">
                <div class="grid-col" id="main-col-wrap">
                    <div class="feat-row grid-row">
                        <div class="grid-col-50 grid-row-center">
                            <article class="pane-primary">
                                <div class="heading">
                                    <h3 class="lb">Cập nhật phí trọng lượng</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot2">
                                            <asp:Label ID="lbl_check" runat="server" EnableViewState="false" Visible="false" ForeColor="Red"></asp:Label>
                                        </div>
                                        <div class="form-row col-md-12">
                                            <label for="exampleInputEmail">
                                                Trọng lượng từ
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="pWeightFrom" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </label>
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pWeightFrom" MinValue="0" NumberFormat-DecimalDigits="0"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row col-md-12">
                                            <label for="exampleInputEmail">
                                                Trọng lượng đến
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="pWeightTo" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </label>
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pWeightTo" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row col-md-12">
                                            <label for="exampleInputEmail">
                                                Vip 1
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="pVip1" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </label>
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pVip1" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row col-md-12">
                                            <label for="exampleInputEmail">
                                                Vip 2
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="pVip2" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </label>
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pVip2" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row col-md-12">
                                            <label for="exampleInputEmail">
                                                Vip 3
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="pVip3" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </label>
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pVip3" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row col-md-12">
                                            <label for="exampleInputEmail">
                                                Vip 4
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="pVip4" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </label>
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pVip4" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row col-md-12">
                                            <label for="exampleInputEmail">
                                                Vip 5
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="pVip5" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </label>
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pVip5" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row col-md-12">
                                            <label for="exampleInputEmail">
                                                Vip 6
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="pVip6" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </label>
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pVip6" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row col-md-12">
                                            <label for="exampleInputEmail">
                                                Khu vực                                            
                                            </label>
                                            <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Hà Nội" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Việt Trì" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-row col-md-12">
                                            <label for="exampleInputEmail">
                                                Loại
                                            </label>
                                            <asp:DropDownList ID="ddlfs" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Nhanh" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Chậm" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-row no-margin center-txt">
                                            <asp:Button runat="server" ID="btnSave" Text="Lưu" CssClass="btn primary-btn" ValidationGroup="n" OnClick="btnSave_Click" />
                                            <a href="/manager/WeigtPricehList.aspx" class="btn primary-btn" style="padding: 10px 0;">Trở về</a>
                                        </div>
                                    </div>
                                </div>
                            </article>
                        </div>
                    </div>
                </div>
            </div>
        </main>
    </asp:Panel>
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
