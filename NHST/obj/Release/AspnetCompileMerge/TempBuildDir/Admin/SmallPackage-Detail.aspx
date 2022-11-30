<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="SmallPackage-Detail.aspx.cs" Inherits="NHST.Admin.SmallPackage_Detail" %>
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
                        <h3 class="panel-title semi-text text-uppercase">Cập nhật mã vận đơn</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group marbot1">
                                    Mã vận đơn
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox runat="server" ID="txtOrderTransactionCode" CssClass="form-control has-validate" placeholder="Mã vận đơn" Enabled="false"></asp:TextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOrderTransactionCode" ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Bao hàng
                                </div>
                                <div class="form-group marbot2">
                                    <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="form-control select2">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group marbot1">
                                    Loại hàng
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox runat="server" ID="txtProductType" CssClass="form-control has-validate" placeholder="Loại hàng" Enabled="false"></asp:TextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtProductType" ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Phí ship (Tệ)
                                </div>
                                <div class="form-group marbot2">
                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                        ID="pShip" MinValue="0" NumberFormat-DecimalDigits="2" Enabled="false"
                                        NumberFormat-GroupSizes="3" Width="100%" placeholder="Phí ship">
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div class="form-group marbot1">
                                    Cân (kg)
                                </div>
                                <div class="form-group marbot2">
                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                        ID="pWeight" MinValue="0" NumberFormat-DecimalDigits="2" Enabled="false"
                                        NumberFormat-GroupSizes="3" Width="100%" placeholder="Cân (kg)" >
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div class="form-group marbot1">
                                    Khối (m3)
                                </div>
                                <div class="form-group marbot2">
                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                        ID="pVolume" MinValue="0" NumberFormat-DecimalDigits="2" Enabled="false"
                                        NumberFormat-GroupSizes="3" Width="100%" placeholder="Khối (m3)">
                                    </telerik:RadNumericTextBox>
                                </div>
                                 <div class="form-group marbot1">
                                    Trạng thái
                                </div>
                                <div class="form-group marbot2">
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">                                        
                                        <asp:ListItem Value="1" Text="Đã nhận hàng tại TQ"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Đang chuyển về VN"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Đã nhận hàng tại VN"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Đã giao cho khách"></asp:ListItem>                                       
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group no-margin">
                                    <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn btn-success btn-block small-btn"
                                        OnClick="btncreateuser_Click" />
                                    <asp:Button ID="btnBack" runat="server" Text="Trở về" CssClass="btn btn-success btn-block small-btn"
                                        OnClick="btnBack_Click" CausesValidation="false" />
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
