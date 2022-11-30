<%@ Page Language="C#" MasterPageFile="~/UserMasterNew.Master" AutoEventWireup="true" CodeBehind="them-khieu-nai.aspx.cs" Inherits="NHST.them_khieu_nai" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>

<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="main">
        <div class="row">
            <div class="content-wrapper-before blue-grey lighten-5"></div>
            <div class="col s12">
                <div class="container">
                    <div class="all">
                        <div class="card-panel mt-3 no-shadow">
                            <div class="row">
                                <div class="col s12">
                                    <div class="page-title mt-2 center-align">
                                        <h4>Tạo khiếu nại mới</h4>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-2 mb-2">
                                <div class="input-field col s12 m6">
                                    <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
                                </div>

                                <div class="input-field col s12">
                                    <asp:TextBox ID="txtOrderID" runat="server" CssClass="validate" Enabled="false"></asp:TextBox>
                                    <label for="rp_shopid">Mã Shop</label>
                                </div>

                                <div class="input-field col s12">
                                    <telerik:RadNumericTextBox runat="server" CssClass="validate" Skin="MetroTouch"
                                        ID="pAmount" NumberFormat-DecimalDigits="0" MinValue="0"
                                        NumberFormat-GroupSizes="3" Width="100%">
                                    </telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="re" runat="server" ControlToValidate="pAmount" Display="Dynamic"
                                        ErrorMessage="Không để trống" ForeColor="Red" ValidationGroup="khieunai"></asp:RequiredFieldValidator>
                                    <label for="rp_vnd" class="active">Số tiền (VNĐ)</label>
                                </div>

                                <div class="input-field col s12 m12 l12">
                                    <asp:TextBox ID="txtNote" runat="server" CssClass="materialize-textarea" TextMode="MultiLine" Height="200px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNote" Display="Dynamic"
                                        ErrorMessage="Không để trống" ForeColor="Red" ValidationGroup="khieunai"></asp:RequiredFieldValidator>
                                    <label for="rp_textarea">Nội dung</label>
                                </div>
                                <div class="col s12 m12">
                                    <span class="black-text">Hình ảnh</span>
                                    <div style="display: inline-block; margin-left: 15px;">
                                        <input class="upload-img" type="file" onchange="previewFiles(this);" multiple title="">
                                        <button type="button" class="btn-upload">Upload</button>
                                    </div>
                                    <div class="preview-img">
                                    </div>

                                    <%--  <telerik:RadAsyncUpload Skin="Metro" runat="server" ID="hinhDaiDien" ChunkSize="0"
                                        Localization-Select="Chọn ảnh" AllowedFileExtensions=" .jpeg,.jpg,.png"
                                        MultipleFileSelection="Disabled" MaxFileInputsCount="1" OnClientFileSelected="OnClientFileSelected">
                                    </telerik:RadAsyncUpload>
                                    <asp:Image runat="server" ID="imgDaiDien" Width="200" />
                                    <asp:HiddenField runat="server" ID="listImg" ClientIDMode="Static" />--%>
                                </div>

                            </div>
                            <hr />
                            <div class="action-btn mt-2 mb-2 center-align">
                                <a class="btn" href="javascript:;" onclick="AddComplain()">Tạo khiếu nại</a>
                                <asp:Button ID="btnSend" runat="server" UseSubmitBehavior="false" Style="display: none" Text="Tạo khiếu nại"
                                    CssClass="btn" OnClick="btnSend_Click" ValidationGroup="khieunai" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="hdfListIMG" />
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

        <script>

            function AddComplain() {
                var base64 = "";
                $(".preview-img img").each(function () {
                    base64 += $(this).attr('src') + "|";
                })
                $("#<%=hdfListIMG.ClientID%>").val(base64);
                $("#<%=btnSend.ClientID%>").click();
            }

           <%-- function DelRow(that, link) {

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
            })(window);--%>
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
