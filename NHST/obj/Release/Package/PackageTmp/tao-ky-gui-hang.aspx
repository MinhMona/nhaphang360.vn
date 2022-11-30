<%@ Page Title="" Language="C#" MasterPageFile="~/dqgMasterLogined.Master" AutoEventWireup="true" CodeBehind="tao-ky-gui-hang.aspx.cs" Inherits="NHST.tao_ky_gui_hang" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/UC/uc_Sidebar.ascx" TagName="SideBar" TagPrefix="uc" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-row-left {
            width: 25%;
        }

        .form-row-right {
            width: 70%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main>
        <div id="primary" class="index">
            <section id="firm-services" class="sec sec-padd-50">
                <div class="container text-center container-800">
                    <h3 class="sec-tit text-center">
                        <span class="sub">Ký gửi hàng hóa</span>
                    </h3>
                    <div class="primary-form">
                        <div class="order-tool clearfix">
                            <div class="primary-form custom-width">
                                <div class="step-income">
                                    <asp:Panel ID="pn" runat="server">
                                        <div class="form-row">
                                            <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                        </div>
                                        <div class="form-row">
                                            <div class="form-row-left">
                                                <div class="lb width-not-full">Danh sách mã kiện: </div>
                                            </div>
                                            <div class="form-row-right">
                                                <asp:TextBox ID="txtListOrderCode" runat="server" CssClass="form-control full-width" TextMode="MultiLine" Height="200px" style="padding:10px;"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNote" Display="Dynamic"
                                                    ErrorMessage="Không để trống" CssClass="text-align-left" ForeColor="Red" ValidationGroup="khieunai"></asp:RequiredFieldValidator>
                                                <span style="float:left;color:red">Vui lòng xuống dòng cho mỗi mã vận đơn và thêm dấu phẩy sau mỗi mã.</span>                                                
                                            </div>
                                        </div>
                                        <div class="form-row">
                                            <div class="form-row-left">
                                                <div class="lb width-not-full">Ghi chú: </div>
                                            </div>
                                            <div class="form-row-right">
                                                <asp:TextBox ID="txtNote" runat="server" CssClass="form-control full-width" TextMode="MultiLine" Height="200px" Style="padding: 10px;"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-row">
                                            <asp:Button ID="btnSend" runat="server" Text="Gửi yêu cầu" CssClass="btn pill-btn primary-btn admin-btn mar-top3 main-btn hover"
                                                Style="padding: 0px 20px; text-transform: uppercase;" OnClick="btnSend_Click" ValidationGroup="khieunai" />
                                        </div>
                                    </asp:Panel>
                                </div>
                                <div class="step-income">
                                    <h2 class="content-title">Danh sách yêu cầu</h2>
                                    <div class="step-income">
                                        <table class="customer-table mar-top1 full-width center-data">
                                            <tr>
                                                <th width="20%" style="text-align: center">Ngày giờ</th>
                                                <th width="20%" style="text-align: center">Mã vận đơn</th>
                                                <th width="20%" style="text-align: center">Trạng thái</th>
                                                <th width="20%" style="text-align: center"></th>
                                            </tr>
                                            <asp:Literal ID="ltr" runat="server"></asp:Literal>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                    </div>
                </div>
            </section>
        </div>
        <asp:HiddenField ID="hdfID" runat="server" />
        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Style="display: none" />
    </main>
    <style>
        .width-not-full {
            float: left;
            width: auto;
            margin: 10px 20px 0 0;
        }
    </style>
    <telerik:RadAjaxLoadingPanel ID="rxLoading" runat="server" Skin="">
        <div class="loading1">
            <asp:Image ID="Image1" runat="server" ImageUrl="/App_Themes/NHST/loading1.gif" AlternateText="loading" />
        </div>
    </telerik:RadAjaxLoadingPanel>
    <!-- END CONTENT -->
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSend">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Button1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadCodeBlock runat="server">
        <script src="/App_Themes/NewUI/js/jquery.min.js"></script>
        <script type="text/javascript">
            function cancelwithdraw(ID) {
                var c = confirm("Bạn muốn hủy yêu cầu này?");
                if (c == true) {
                    $("#<%=hdfID.ClientID%>").val(ID);
                    $("#<%=btnCancel.ClientID%>").click();
                }
            }
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
    </telerik:RadCodeBlock>
    <style>
        .RadUpload_Metro .ruFakeInput {
            float: left;
            width: 60%;
        }

        .page.account-management .right-content .right-side {
            padding-left: 20px;
        }

        div.RadUploadSubmit, div.RadUpload_Metro .ruButton {
            padding: 0;
        }
    </style>
</asp:Content>
