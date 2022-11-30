<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="refundDetail.aspx.cs" Inherits="NHST.manager.refundDetail" %>

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
                                    <h3 class="lb">Yêu cầu nạp tiền tệ</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot1">
                                            Username
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Số tiền (tệ)
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control" Skin="MetroTouch"
                                                ID="pAmount" MinValue="0" NumberFormat-DecimalDigits="3" NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Ghi chú
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtNote" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                           Trạng thái
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control select2">
                                                <asp:ListItem Value="1">Chưa Duyệt</asp:ListItem>
                                                <asp:ListItem Value="2">Đã duyệt</asp:ListItem>
                                                <asp:ListItem Value="3">Hủy</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-row no-margin center-txt">
                                            <asp:Button runat="server" ID="btnSave" Text="Cập nhật" CssClass="btn btn-success" ValidationGroup="n" OnClick="btnSave_Click" />
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
