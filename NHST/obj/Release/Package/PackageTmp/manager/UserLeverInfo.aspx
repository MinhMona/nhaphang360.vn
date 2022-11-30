<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="UserLeverInfo.aspx.cs" Inherits="NHST.manager.UserLeverInfo" %>
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
                                    <h3 class="lb">Thông tin cấp người dùng</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">                                        
                                        <div class="form-row marbot2">
                                            <asp:Label ID="lbl_check" runat="server" EnableViewState="false" Visible="false" ForeColor="Red"></asp:Label>
                                        </div>
                                        <div class="form-row marbot2">
                                            <label for="exampleInputEmail">
                                                Tên cấp
                                            <asp:RequiredFieldValidator runat="server" ID="rq" ControlToValidate="txtLevelName" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </label>
                                            <asp:TextBox runat="server" ID="txtLevelName" CssClass="form-control" placeholder="Tên cấp"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot2">
                                            <label for="exampleInputEmail">
                                                Chiết khấu phí mua hàng
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="pFeeBuyPro" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </label>
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pFeeBuyPro" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row marbot2">
                                            <label for="exampleInputEmail">
                                                Chiết khấu phí vận chuyển TQ-VN
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="pFeeWeight" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </label>
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pFeeWeight" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row marbot2">
                                            <label for="exampleInputEmail">
                                                Đặt cọc tối thiểu
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="pLessDeposit" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </label>
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pLessDeposit" MinValue="0" NumberFormat-DecimalDigits="2"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row no-margin center-txt">
                                            <asp:Button runat="server" ID="btnSave" Text="Lưu" CssClass="btn primary-btn" ValidationGroup="n" OnClick="btnSave_Click" />
                                            <a href="/manager/User-Level.aspx" class="btn primary-btn">Trở về</a>
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
