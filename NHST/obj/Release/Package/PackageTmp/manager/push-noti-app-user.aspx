<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="push-noti-app-user.aspx.cs" Inherits="NHST.manager.push_noti_app_user" %>

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
                                    <h3 class="lb">Thông báo</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <asp:Label ID="lbl_check" runat="server" EnableViewState="false" Visible="false" ForeColor="Red"></asp:Label>
                                        <div class="form-row marbot1">
                                            Username
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtUsername" Enabled="true" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            Tiêu đề
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtTitle" Enabled="true" CssClass="form-control"></asp:TextBox>
                                        </div>

                                        <div class="form-row marbot1">
                                            Nội dung thông báo
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtMessage" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <div class="form-row no-margin center-txt">
                                            <asp:Button runat="server" ID="btnSave" Text="Tạo mới" CssClass="btn primary-btn" ValidationGroup="n" OnClick="btnSave_Click" />
                                            <a href="/manager/Noti-app-list.aspx" class="btn primary-btn">Trở về</a>
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
    </telerik:RadAjaxManager>
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
    <telerik:RadCodeBlock runat="server">
        <script src="/App_Themes/NewUI/js/jquery.min.js"></script>

    </telerik:RadCodeBlock>

</asp:Content>
