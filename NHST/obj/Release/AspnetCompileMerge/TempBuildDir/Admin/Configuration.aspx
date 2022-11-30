<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Configuration.aspx.cs" Inherits="NHST.Admin.Configuration" %>

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
                        <h3 class="panel-title semi-text text-uppercase">Thiết lập</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group marbot1">
                                    Tên website
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox runat="server" ID="txtWebsitename" CssClass="form-control has-validate" placeholder="Tên website"></asp:TextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtWebsitename" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Địa chỉ
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control has-validate" placeholder="Địa chỉ"></asp:TextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtAddress" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Email hỗ trợ
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox runat="server" ID="txtEmailSupport" CssClass="form-control has-validate" placeholder="Email hỗ trợ"></asp:TextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmailSupport" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailSupport" ForeColor="Red" ValidationExpression="^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$" ErrorMessage="(Sai định dạng Email)" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Email liên hệ
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox runat="server" ID="txtEmailContact" CssClass="form-control has-validate" placeholder="Email liên hệ"></asp:TextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmailContact" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmailContact" ForeColor="Red" ValidationExpression="^\w+[\w-\.]*\@\w+((-\w+)|(\w*))\.[a-z]{2,3}$" ErrorMessage="(Sai định dạng Email)" SetFocusOnError="true"></asp:RegularExpressionValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Hotline
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox runat="server" ID="txtHotline" CssClass="form-control has-validate" placeholder="Hotline"></asp:TextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtHotline" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Facebook
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox runat="server" ID="txtFacebook" CssClass="form-control has-validate" placeholder="Facebook"></asp:TextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtFacebook" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Twitter
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox runat="server" ID="txtTwitter" CssClass="form-control has-validate" placeholder="Twitter"></asp:TextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTwitter" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Instagram
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox runat="server" ID="txtInstagram" CssClass="form-control has-validate" placeholder="Vimeo"></asp:TextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtInstagram" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Skype
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox runat="server" ID="txtSkype" CssClass="form-control has-validate" placeholder="Skype"></asp:TextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtSkype" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Giờ hoạt động
                                </div>
                                <div class="form-group marbot2">
                                    <asp:TextBox runat="server" ID="txtTimeWork" CssClass="form-control has-validate" placeholder="Giờ hoạt động"></asp:TextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtTimeWork" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Tỷ giá
                                </div>
                                <div class="form-group marbot2">
                                    <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                        ID="pCurrency" MinValue="1000" NumberFormat-DecimalDigits="0"
                                        NumberFormat-GroupSizes="3" Width="100%">
                                    </telerik:RadNumericTextBox>
                                    <span class="error-info-show">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="pCurrency" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </span>
                                </div>
                                <div class="form-group marbot1">
                                    Thông báo
                                </div>
                                <div class="form-group marbot2">
                                    <telerik:RadEditor runat="server" ID="pContent" Width="100%"
                                        Height="600px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro"
                                        DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd" AutoResizeHeight="True">
                                        <ImageManager ViewPaths="~/Uploads/Images" UploadPaths="~/Uploads/Images" DeletePaths="~/Uploads/Images" />
                                    </telerik:RadEditor>
                                </div>
                                <div class="form-group no-margin">
                                    <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn btn-success btn-block large-btn"
                                        OnClick="btncreateuser_Click" />
                                    <%--<a href="javascript:;" onclick="createuser()" class="btn btn-success btn-block small-btn right-btn">Tạo tài khoản</a>--%>
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
