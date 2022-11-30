<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UserWallet.aspx.cs" Inherits="NHST.Admin.UserWallet" %>

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
                        <h3 class="panel-title semi-text text-uppercase">Nạp tiền vào wallet User</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group marbot1">
                                    Tên đăng nhập / Nickname
                                </div>
                                <div class="form-group marbot2">
                                    <asp:Label ID="lblUsername" runat="server"></asp:Label>
                                </div>
                                <div class="form-group marbot1">
                                    Số tiền
                                </div>
                                <div class="form-group marbot2">
                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                        ID="pWallet" MinValue="0" NumberFormat-DecimalDigits="0"
                                        NumberFormat-GroupSizes="3" Width="100%">
                                    </telerik:RadNumericTextBox>
                                </div>
                                <div class="form-group marbot1">
                                    Nội dung
                                </div>
                                <div class="form-group marbot2">
                                    <telerik:RadEditor runat="server" ID="pContent" Width="100%"
                                        Height="600px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro"
                                        DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd" AutoResizeHeight="True">
                                        <ImageManager ViewPaths="~/Uploads/Images" UploadPaths="~/Uploads/Images" DeletePaths="~/Uploads/Images" />
                                    </telerik:RadEditor>
                                </div>
                                <asp:Panel ID="pbadmin" runat="server">
                                    <div class="form-group marbot1">
                                        Trạng thái
                                    </div>
                                    <div class="form-group marbot2">
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="1" Text="Đang chờ"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Duyệt chuyển"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Hủy"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </asp:Panel>
                                <div class="form-group no-margin">
                                    <asp:Button ID="btncreateuser" runat="server" Text="Nạp tiền" CssClass="btn btn-success btn-block small-btn"
                                        OnClick="btncreateuser_Click" />
                                    <a href="/admin/userlist.aspx" class="btn btn-success btn-block small-btn">Hủy</a>
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
