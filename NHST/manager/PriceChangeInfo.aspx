<%@ Page Title="" Language="C#" AutoEventWireup="true" MasterPageFile="~/manager/adminMaster.Master" CodeBehind="PriceChangeInfo.aspx.cs" Inherits="NHST.manager.PriceChangeInfo" %>

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
                                    <h3 class="lb">Thông tin cơ bản</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot1">
                                            <label for="exampleInputEmail">
                                                Giá tệ từ
                                                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="pPriceFromCYN" SetFocusOnError="true"
                                                     ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </label>

                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pPriceFromCYN" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            Giá tệ đến
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="pPriceToCYN" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pPriceToCYN" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>

                                        <div class="form-row marbot1" style="display: block;">
                                            Vip 0
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="pVip0" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-row marbot2" style="display: block;">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pVip0" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            Vip 1
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="pVip1" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pVip1" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            Vip 2
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="pVip2" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pVip2" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            Vip 3
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="pVip3" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pVip3" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            Vip 4
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="pVip4" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pVip4" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            Vip 5
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="pVip5" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pVip5" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            Vip 6
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="pVip6" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pVip6" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>

                                        <div class="form-row marbot1" style="display: block;">
                                            Vip 7
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="pVip7" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-row marbot2" style="display: block;">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pVip7" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>

                                        <div class="form-row marbot1" style="display: block;">
                                            Vip 8
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="pVip8" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-row marbot2" style="display: block;">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pVip8" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>



                                        <div class="form-row no-margin center-txt">
                                            <asp:Button runat="server" ID="btnSave" Text="Cập nhật" CssClass="btn primary-btn" ValidationGroup="n" OnClick="btnSave_Click" />
                                            <a href="/manager/PriceChangeList.aspx" class="btn primary-btn">Trở về</a>
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
    <%-- <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ddlDVT">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ddlDVT" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="lblUnit" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>--%>
    <telerik:RadCodeBlock runat="server">
        <script src="/App_Themes/NewUI/js/jquery-1.11.0.min.js"></script>
        <script>

            (function (global, undefined) {
                var textBox = null;

                function textBoxLoad(sender) {
                    textBox = sender;
                }

                function OpenFileExplorerDialog() {
                    global.radopen("/Admin/Dialogs/Dialog.aspx", "ExplorerWindow");
                }

                //This function is called from a code declared on the Explorer.aspx page

                global.OpenFileExplorerDialog = OpenFileExplorerDialog;
                global.OnFileSelected = OnFileSelected;
                global.textBoxLoad = textBoxLoad;
            })(window);
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
