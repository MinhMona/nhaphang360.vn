<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="BigPackageDetail.aspx.cs" Inherits="NHST.manager.BigPackageDetail" %>

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
                                    <h3 class="lb">Chỉnh sửa package</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot1">
                                            Ngày gửi
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadDatePicker RenderMode="Lightweight" ID="rSendDate" ShowPopupOnFocus="true" Width="100%" runat="server"
                                                DateInput-CssClass="radPreventDecorate">
                                            </telerik:RadDatePicker>
                                            <span class="error-info-show">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic" ControlToValidate="rSendDate"
                                                    ErrorMessage="*" ValidationGroup="create"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <div class="form-row marbot1">
                                            Ngày đến
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadDatePicker RenderMode="Lightweight" ID="rArrivedDate" ShowPopupOnFocus="true" Width="100%" runat="server"
                                                DateInput-CssClass="radPreventDecorate">
                                            </telerik:RadDatePicker>
                                            <span class="error-info-show">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="rArrivedDate"
                                                    ErrorMessage="*" ValidationGroup="create"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <div class="form-row marbot1">
                                            Mã Package
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox ID="txtPackageCode" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Phí tăng giảm: 
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pAdditionFee" NumberFormat-DecimalDigits="0" Value="0" MinValue="0"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Mô tả
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadEditor runat="server" ID="pContent" Width="100%"
                                                Height="600px" ToolsFile="~/FilesResources/ToolContent.xml" Skin="Metro"
                                                DialogHandlerUrl="~/Telerik.Web.UI.DialogHandler.axd" AutoResizeHeight="True">
                                                <ImageManager ViewPaths="~/Uploads/Images" UploadPaths="~/Uploads/Images" DeletePaths="~/Uploads/Images" />
                                            </telerik:RadEditor>
                                        </div>
                                        <div class="form-row marbot1">
                                            Khu vực
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlPlace" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1" Text="Hà Nội"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Việt Trì"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-row marbot1">
                                            Đơn hàng đang ở
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="1" Text="Quảng Châu"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Hà Nội"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Việt Trì"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-row marbot1">
                                            Đơn hàng chậm:
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:CheckBox ID="chskIsSlow" runat="server" />
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:Literal ID="ltrsm" runat="server"></asp:Literal>
                                        </div>
                                        <div class="form-row marbot2">
                                            <a href="javascript:;" class="btn primary-btn" style="width: 15%;" onclick="addMorePackage();">Thêm Package nhỏ</a>
                                        </div>
                                        <div class="form-row no-margin center-txt">
                                            <a href="javascript:;" class="btn primary-btn" onclick="createNewPackage()" style="margin-bottom: 10px;" onkeydown="return (event.keyCode!=13)">Cập nhật</a>
                                            <a href="javascript:;" class="btn  primary-btn" onclick="sendMail()" style="margin-bottom: 10px;" onkeydown="return (event.keyCode!=13)">Gửi mail</a>
                                            <a href="/admin/BigPackageList.aspx" class="btn  primary-btn" onkeydown="return (event.keyCode!=13)">Hủy</a>
                                            <asp:Button ID="btncreateuser" runat="server" Text="Tạo mới" CssClass="btn  primary-btn" onkeydown="return (event.keyCode!=13)"
                                                OnClick="btncreateuser_Click" Style="display: none;" ValidationGroup="create" UseSubmitBehavior="false" />
                                            <asp:Button ID="btnSendmail" runat="server" Text="Send Mail" CssClass="btn  primary-btn"
                                                OnClick="btnSendmail_Click" Style="display: none;" ValidationGroup="create" UseSubmitBehavior="false" onkeydown="return (event.keyCode!=13)" />
                                        </div>
                                    </div>
                                </div>
                            </article>
                        </div>
                        <div class="grid-col-100">
                            <article class="pane-primary">
                                <div class="heading">
                                    <h3 class="lb">Lịch sử thay đổi nơi đến</h3>
                                </div>
                                <div class="cont">
                                    <div class="table-rps table-responsive">
                                        <table class="tb-product normal-table">
                                            <tr>
                                                <th class="pro">Ngày thay đổi</th>
                                                <th class="pro">Trạng thái cũ</th>
                                                <th class="pro">Trạng thái mới</th>
                                                <th class="qty">Người đổi</th>
                                            </tr>
                                            <asp:Repeater ID="rptPayment" runat="server">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="pro">
                                                            <%#Eval("CreatedDate","{0:dd/MM/yyyy}") %>
                                                        </td>
                                                        <td class="pro">
                                                            <%# Eval("OldeStatusText").ToString() %>
                                                        </td>
                                                        <td class="pro">
                                                            <%#Eval("NewStatusText").ToString() %>
                                                        </td>
                                                        <td class="qty"><%#Eval("Username") %>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Literal ID="ltrpa" runat="server"></asp:Literal>
                                        </table>
                                    </div>
                                </div>
                            </article>
                        </div>
                    </div>
                </div>
            </div>
        </main>
        <asp:HiddenField ID="hdflistpackage" runat="server" />
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
    <telerik:RadScriptBlock ID="sc" runat="server">
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
            function addMorePackage() {
                var html = "";
                html += "<div class=\"row smallpackage\" style=\"padding: 20px 0;\">";
                html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\">Mã vận đơn:</div>";
                html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\"><input type=\"text\" class=\"packageCode form-control\"/></div>";
                html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\">User Phone:</div>";
                html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\"><input type=\"text\" class=\"packageUserPhone form-control\" /></div>";
                html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\">Trọng lượng:</div>";
                html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\"><input class=\"packageWeight form-control\" /></div>";
                html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\">Ghi chú:</div>";
                html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\"><textarea class=\"packageNote form-control\" onchange=\"getcode($(this))\"></textarea></div>";
                html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\">Trạng thái nhận hàng:</div>";
                html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\"><select class=\"packageStatusReceived\" disabled><option value=\"0\">Chưa nhận hàng</option><option value=\"1\">Đã nhận hàng</option></select></div>";
                html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\">Trạng thái thanh toán:</div>";
                html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\"><select class=\"packageStatusPayment\" disabled><option value=\"0\">Chưa thanh toán</option><option value=\"1\">Đã thanh toán</option></select></div>";
                html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\">Ghi chú khách hàng:</div>";
                html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\"><textarea class=\"packageNoteCus form-control\"></textarea></div>";
                html += "</div>";
                $(".packageitem").append(html);
                //$.ajax({
                //    type: "POST",
                //    url: "/Admin/AddBigPackage.aspx/GetBarCode",
                //    //data: "{count:'" + count + "',type:'" + type + "',projectid:'" + id + "'}",
                //    contentType: "application/json; charset=utf-8",
                //    dataType: "json",
                //    success: function (msg) {
                //        var data = msg.d;
                //        var html = "";
                //        html += "<div class=\"row smallpackage\" style=\"padding: 20px 0;\">";
                //        html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\">Mã vận đơn:</div>";
                //        html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\"><input disabled type=\"text\" class=\"packageCode form-control\" value=\"" + data + "\"/></div>";
                //        html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\">User Phone:</div>";
                //        html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\"><input type=\"text\" class=\"packageUserPhone form-control\" /></div>";
                //        html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\">Trọng lượng:</div>";
                //        html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\"><input class=\"packageWeight form-control\" /></div>";
                //        html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\">Ghi chú:</div>";
                //        html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\"><textarea class=\"packageNote form-control\" onchange=\"getcode($(this))\"></textarea></div>";
                //        html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\">Trạng thái nhận hàng:</div>";
                //        html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\"><select class=\"packageStatusReceived\" disabled><option value=\"0\">Chưa nhận hàng</option><option value=\"1\">Đã nhận hàng</option></select></div>";
                //        html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\">Trạng thái thanh toán:</div>";
                //        html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\"><select class=\"packageStatusPayment\" disabled><option value=\"0\">Chưa thanh toán</option><option value=\"1\">Đã thanh toán</option></select></div>";
                //        html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\">Ghi chú khách hàng:</div>";
                //        html += "<div class=\"col-md-25\" style=\"padding-top: 5px;padding-bottom:5px;\"><textarea class=\"packageNoteCus form-control\"></textarea></div>";
                //        html += "</div>";
                //        $(".packageitem").append(html);
                //    },
                //    error: function (xmlhttprequest, textstatus, errorthrow) {
                //        //alert('lỗi checkend');
                //    }
                //});
            }
            function createNewPackage() {
                var list = "";
                var check = false;
                //if ($(".smallpackage").length > 0) {
                $(".smallpackage").each(function () {
                    var code = $(this).find(".packageCode").val();
                    if (isEmpty(code)) {
                        check = true;
                    }
                    var phone = $(this).find(".packageUserPhone").val();
                    var weight = $(this).find(".packageWeight").val();
                    var note = $(this).find(".packageNote").val();
                    var noteCus = $(this).find(".packageNoteCus").val();
                    var StatusReceived = $(this).find(".packageStatusReceived").val();
                    var StatusPayment = $(this).find(".packageStatusPayment").val();
                    list += code + "," + phone + "," + weight + "," + note + "," + StatusReceived + "," + StatusPayment + "," + noteCus + "|";
                });
                if (check == false) {
                    $("#<%=hdflistpackage.ClientID%>").val(list);
                    $("#<%=btncreateuser.ClientID%>").click();
                }
                else {
                    alert('Có kiện nhỏ chưa có mã vận đơn, vui lòng kiểm tra lại.');
                }
                //}
                //else {
                //    alert('Vui lòng add thêm package nhỏ');
                //}
            }
            function sendMail() {
                var list = "";
                //if ($(".smallpackage").length > 0) {
                $(".smallpackage").each(function () {
                    var code = $(this).find(".packageCode").val();
                    var phone = $(this).find(".packageUserPhone").val();
                    var weight = $(this).find(".packageWeight").val();
                    var note = $(this).find(".packageNote").val();
                    var noteCus = $(this).find(".packageNoteCus").val();
                    var StatusReceived = $(this).find(".packageStatusReceived").val();
                    var StatusPayment = $(this).find(".packageStatusPayment").val();
                    list += code + "," + phone + "," + weight + "," + note + "," + StatusReceived + "," + StatusPayment + "," + noteCus + "|";
                });
                $("#<%=hdflistpackage.ClientID%>").val(list);
                $("#<%=btnSendmail.ClientID%>").click();
            }
            function getcode(obj) {
                var val = obj.val();
                val += "\n";
                obj.val(val);
            }
        </script>
    </telerik:RadScriptBlock>

</asp:Content>
