<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UserLeverInfo.aspx.cs" Inherits="NHST.Admin.UserLeverInfo" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainTit" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-white">
                    <div class="panel-heading">
                        <h3 class="panel-title semi-text text-uppercase">Thông tin cấp người dùng</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row m-b-lg">
                            <div class="col-md-12">
                                <asp:Label ID="lbl_check" runat="server" EnableViewState="false" Visible="false" ForeColor="Red"></asp:Label>
                                <div class="row">
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Tên cấp
                                            <asp:RequiredFieldValidator runat="server" ID="rq" ControlToValidate="txtLevelName" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </label>
                                        <asp:TextBox runat="server" ID="txtLevelName" CssClass="form-control" placeholder="Tên cấp"></asp:TextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Chiết khấu phí mua hàng
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="pFeeBuyPro" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </label>
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pFeeBuyPro" MinValue="0" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Chiết khấu phí vận chuyển TQ-VN
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="pFeeWeight" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </label>
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pFeeWeight" MinValue="0" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Đặt cọc tối thiểu
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="pLessDeposit" SetFocusOnError="true"
                                                ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </label>
                                        <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                            ID="pLessDeposit" MinValue="0" NumberFormat-DecimalDigits="0"
                                            NumberFormat-GroupSizes="3" Width="100%">
                                        </telerik:RadNumericTextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-12">
                                        <asp:Button runat="server" ID="btnSave" Text="Lưu" CssClass="btn btn-success" ValidationGroup="n" OnClick="btnSave_Click" />
                                        <a href="/Admin/User-Level.aspx" class="btn btn-success" >Trở về</a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
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
