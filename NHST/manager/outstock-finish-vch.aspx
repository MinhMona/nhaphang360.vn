<%@ Page Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="outstock-finish-vch.aspx.cs" Inherits="NHST.manager.outstock_finish_vch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <div id="main" class="main-full">
            <div class="row">
                <div class="content-wrapper-before bg-dark-gradient"></div>
                <div class="page-title">
                    <div class="card-panel">
                        <h4 class="title no-margin" style="display: inline-block;">Chi tiết phiên xuất kho ký gửi</h4>
                        <div class="right-action" style="display:none">
                            <a href="javascript:;" class="btn btn-print">In phiếu
                        xuất kho</a>
                        </div>
                    </div>
                </div>
                <div class="list-staff col s12 m12 l12 xl9 section">
                    <div class="list-table" id="export-all">
                        <div class="row print-section">
                            <div class="col s12">
                                <div class="print-header">
                                    <div class="row">
                                        <div class="col s6">
                                            <div class="hd-left">
                                                <div class="content">
                                                    <p class="name">NHẬP HÀNG 360</p>
                                                    <p class="address">Địa chỉ: Đang cập nhật</p>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col s6">
                                            <div class="hd-right">
                                                <div class="content">
                                                    <p>Mẫu số 01 - TT</p>
                                                    <p>
                                                        Ban hành theo thông tư số 113/2019 - BTC ngày 26/8/2016 của Bộ Tài
                                                Chính
                                                    </p>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col s12">
                                            <div class="print-title">
                                                <h1>Phiếu Xuất Kho</h1>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="export-detail col s12">
                                <div class="card-panel">
                                    <div class="row">
                                        <div class="col s12">
                                            <div class="row">
                                                <div class="input-field col s12 total-all hide-on-print" style="display:none">
                                                    <h6>Tổng tiền hàng: <span class="font-weight-700">40.000.000 VNĐ</span>
                                                    </h6>
                                                </div>
                                                <div class="input-field col s12 m4">
                                                    <asp:TextBox runat="server" ID="txtFullname" CssClass="form-control has-validate"
                                                        placeholder="Họ tên người nhận">
                                                    </asp:TextBox>
                                                    <span class="error-info-show">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                            ControlToValidate="txtFullname"
                                                            Display="Dynamic" ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <%--   <input id="full_name" type="text" class="validate" value="Lê Thiên An">--%>
                                                    <label for="full_name">Họ tên người nhận</label>
                                                </div>
                                                <div class="input-field col s12 m4">
                                                    <asp:TextBox runat="server" ID="txtPhone" CssClass="form-control has-validate"
                                                        placeholder="Số điện thoại">
                                                    </asp:TextBox>
                                                    <span class="error-info-show">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                            ControlToValidate="txtPhone"
                                                            Display="Dynamic" ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                    </span>
                                                    <%--  <input id="phone_number" type="text" class="validate" value="0886706289">--%>
                                                    <label for="phone_number">Số điện thoại người nhận</label>
                                                </div>
                                                <div class="input-field col s12 m4">
                                                    <div class="action-wrap">
                                                        <asp:Panel ID="pnButton" runat="server" Visible="false">
                                                             <asp:Button ID="btncreateuser" runat="server" Text="Xuất kho" CssClass="btn"
                                                    OnClick="btncreateuser_Click" UseSubmitBehavior="false" />
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnrefresh" runat="server" Visible="false">
                                                          <asp:Button ID="btnRefresh" runat="server" Text="Reload" CssClass="btn"
                                                    OnClick="btnRefresh_Click" UseSubmitBehavior="false" />
                                                        </asp:Panel>
                                                        <%--  <button class="btn" type="button">Đã xử lý</button>--%>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col s12">
                                            <asp:Literal ID="ltrList" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col s12">
                                <div class="print-footer">
                                    <div class="signature">
                                        <div class="col">
                                            <p class="bold">Người xuất hàng</p>
                                            <p>(Ký,họ tên)</p>
                                        </div>
                                        <div class="col">
                                            <p class="bold">Người xuất hàng</p>
                                            <p>(Ký,họ tên)</p>
                                        </div>
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
            <telerik:AjaxSetting AjaxControlID="btnSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
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
        function getcode(obj) {
            var val = obj.val();
            //alert(val);
            val += ";";
            obj.val(val);
        }
        function VoucherSourcetoPrint(source) {
            var r = "<html><head><link rel=\"stylesheet\" href=\"/App_Themes/AdminNew/css/style.css\" type=\"text/css\"/><link rel=\"stylesheet\" href=\"/App_Themes/AdminNew/css/style-p.css\" type=\"text/css\"/><script>function step1(){\n" +
                "setTimeout('step2()', 10);}\n" +
                "function step2(){window.print();window.close()}\n" +
                "</scri" + "pt></head><body onload='step1()'>\n" +
                "" + source + "</body></html>";
            return r;
        }
        function VoucherPrint(source) {
            Pagelink = "about:blank";
            var pwa = window.open(Pagelink, "_new");
            pwa.document.open();
            pwa.document.write(VoucherSourcetoPrint(source));
            pwa.document.close();
        }
    </script>
    <style>
        .text-align-right {
            text-align: right;
        }

        .form-control {
            background: #fff;
        }
    </style>
</asp:Content>

