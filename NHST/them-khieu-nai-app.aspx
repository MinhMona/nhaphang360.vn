<%@ Page Title="Thêm khiếu nại app" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="them-khieu-nai-app.aspx.cs" Inherits="NHST.them_khieu_nai_app" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" href="/App_Themes/App/css/style-NA.css" media="all">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <main id="main-wrap">

        <div class="page-wrap">
            <asp:Panel ID="pnMobile" runat="server" Visible="false">
                <div class="page-body">
                    <div class="all donhang">
                        <h2 class="title_page">THÊM KHIẾU NẠI</h2>
                        <div class="content_page">
                            <div class="content_create_order complain_order">
                                <div class="bottom_order">
                                    <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                    <ul>
                                        <li>Mã đơn hàng:</li>
                                        <li>
                                            <asp:TextBox ID="txtOrderID" runat="server" CssClass="input_control readonly_input" Enabled="false"></asp:TextBox>
                                    </ul>
                                    <ul>
                                        <li>Số tiền bồi thường:</li>
                                        <li>
                                            <telerik:RadNumericTextBox runat="server" CssClass="input_control" Skin="MetroTouch"
                                                ID="pAmount" NumberFormat-DecimalDigits="0" MinValue="0"
                                                NumberFormat-GroupSizes="3" Width="100%">
                                            </telerik:RadNumericTextBox>
                                            <asp:RequiredFieldValidator ID="re" runat="server" ControlToValidate="pAmount" Display="Dynamic"
                                                ErrorMessage="Không để trống" ForeColor="Red" ValidationGroup="khieunai"></asp:RequiredFieldValidator>
                                    </ul>
                                    <ul>
                                        <li>Ảnh đổi chiếu:</li>
                                        <li>
                                            <telerik:RadAsyncUpload Skin="Metro" runat="server" ID="hinhDaiDien" ChunkSize="0"
                                                Localization-Select="Chọn ảnh" AllowedFileExtensions=" .jpeg,.jpg,.png"
                                                MultipleFileSelection="Disabled" MaxFileInputsCount="1" OnClientFileSelected="OnClientFileSelected">
                                            </telerik:RadAsyncUpload>
                                            <asp:Image runat="server" ID="imgDaiDien" Width="200" />
                                            <asp:HiddenField runat="server" ID="listImg" ClientIDMode="Static" />
                                            <%-- <input class="input_control" type="file" name="pic" accept="image/*">--%>
                                        </li>
                                    </ul>
                                    <ul>
                                        <li>Nội dung: </li>
                                        <li>
                                            <asp:TextBox ID="txtNote" runat="server" CssClass="input_control message" TextMode="MultiLine" Height="200px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNote" Display="Dynamic"
                                                ErrorMessage="Không để trống" ForeColor="Red" ValidationGroup="khieunai"></asp:RequiredFieldValidator>
                                            <%--<textarea class="input_control message" type="text" name=""></textarea>--%>
                                        </li>
                                    </ul>
                                </div>
                                <p class="btn_order">
                                    <asp:Button ID="btnSend" runat="server" Text="Tạo khiếu nại"
                                        CssClass="btn_ordersp" OnClick="btnSend_Click" ValidationGroup="khieunai" />
                                    <%--<a class="" href="#">GỬI KHIẾU NẠI</a>--%>
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="page-bottom-toolbar">
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnShowNoti" runat="server" Visible="false">
                <div class="page-body">
                    <div class="heading-search">
                        <h4 class="page-title">Bạn vui lòng đăng xuất và đăng nhập lại!</h4>
                    </div>
                </div>
            </asp:Panel>
        </div>

    </main>
    <telerik:RadCodeBlock runat="server">
        <script src="/App_Themes/NewUI/js/jquery.min.js"></script>
       
        <script>

            function DelRow(that, link) {

                $(that).parent().parent().remove();
                var myHidden = $("#<%= listImg.ClientID %>");
                var tempF = myHidden.value;
                myHidden.value = tempF.replace(link, '');
            }
            (function (global, undefined) {
                var textBox = null;

                function textBoxLoad(sender) {
                    textBox = sender;
                }

                function OpenFileExplorerDialog() {
                    global.radopen("/Dialogs/Dialog.aspx", "ExplorerWindow");
                }

                //This function is called from a code declared on the Explorer.aspx page
                function OnFileSelected(fileSelected) {
                    if (textBox) {
                        {
                            var myHidden = document.getElementById('<%= listImg.ClientID %>');
                            var tempF = myHidden.value;

                            tempF = tempF + '#' + fileSelected;
                            myHidden.value = tempF;

                            $('.hidImage').append('<tr><td><img height="100px" src="' + fileSelected + '"/></td><td style="text-align:center"><a class="btn btn-success" onclick="DelRow(this,\'' + fileSelected + '\')">Xóa</a></td></li>');
                            //alert(fileSelected);
                            textBox.set_value(fileSelected);
                        }
                    }
                }

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
