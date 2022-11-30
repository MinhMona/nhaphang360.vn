<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="RequestShipDetail.aspx.cs" Inherits="NHST.manager.RequestShipDetail" %>
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
                                    <h3 class="lb">Thông tin yêu cầu ký gửi</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot1">
                                            Username
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:Label ID="lblUsername" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-row marbot1">
                                            Phone
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                        </div>
                                        <div class="form-row marbot1">
                                            Danh sách mã vận đơn
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox ID="txtListOrderCode" runat="server" CssClass="form-control" Height="200px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Ghi chú
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" Height="200px" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Trạng thái
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                <asp:ListItem Text="Đang chờ" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Duyệt" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Hủy" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>                                        
                                        <div class="form-row no-margin center-txt">
                                            <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn primary-btn"
                                                OnClick="btncreateuser_Click" />
                                            <a href="/manager/request-ship-list.aspx" class="btn primary-btn">Trở về</a>
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
            <telerik:AjaxSetting AjaxControlID="btncreateuser">
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
