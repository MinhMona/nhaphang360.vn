<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddTafiffTQVN.aspx.cs" Inherits="NHST.Admin.AddTafiffTQVN" %>

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
                        <h3 class="panel-title semi-text text-uppercase">Thêm Phí vận chuyển Trung Quốc - Việt Nam</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row m-b-lg">
                            <div class="col-md-12">
                                <asp:Label ID="lbl_check" runat="server" EnableViewState="false" Visible="false" ForeColor="Red"></asp:Label>
                                <div class="row">
                                    <div class="form-group col-md-12">
                                        <label for="exampleInputEmail">
                                            Chọn kho                                            
                                        </label>
                                        <asp:DropDownList ID="ddlReceivePlace" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="Kho Hà Nội" Text="Kho Hà Nội"></asp:ListItem>
                                            <asp:ListItem Value="Kho Hồ Chí Minh" Text="Kho Hồ Chí Minh"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group col-md-12">
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
                                    <div class="form-group col-md-12">
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
                                    <div class="form-group col-md-12">
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
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="row">
                                    <div class="form-group col-md-12">
                                        <asp:Button runat="server" ID="btnSave" Text="Lưu" CssClass="btn btn-success" ValidationGroup="n" OnClick="btnSave_Click" />
                                        <a href="/Admin/Tariff-TQVN.aspx" class="btn btn-success">Trở về</a>
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
