<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="SmallPackage-Detail.aspx.cs" Inherits="NHST.manager.SmallPackage_Detail" %>
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
                                    <h3 class="lb">Cập nhật mã vận đơn</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot1">
                                            Mã vận đơn
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtOrderTransactionCode" CssClass="form-control has-validate" placeholder="Mã vận đơn" Enabled="false"></asp:TextBox>
                                            <span class="error-info-show">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOrderTransactionCode" ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <div class="form-row marbot1">
                                            Bao hàng
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlPrefix" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-row marbot1">
                                            Loại hàng
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtProductType" CssClass="form-control has-validate" placeholder="Loại hàng" Enabled="false"></asp:TextBox>
                                            <span class="error-info-show">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtProductType" ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                            </span>
                                        </div>
                                        <div class="form-row marbot1">
                                            Phí ship (Tệ)
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pShip" MinValue="0" NumberFormat-DecimalDigits="2" Enabled="false"
                                                NumberFormat-GroupSizes="3" Width="100%" placeholder="Phí ship">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Cân (kg)
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pWeight" MinValue="0" NumberFormat-DecimalDigits="2" Enabled="false"
                                                NumberFormat-GroupSizes="3" Width="100%" placeholder="Cân (kg)">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Khối (m3)
                                        </div>
                                        <div class="form-row marbot2">
                                            <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                ID="pVolume" MinValue="0" NumberFormat-DecimalDigits="2" Enabled="false"
                                                NumberFormat-GroupSizes="3" Width="100%" placeholder="Khối (m3)">
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Ghi chú
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control has-validate" placeholder="Ghi chú"></asp:TextBox>
                                        </div>
                                        <div class="form-row marbot1">
                                            Hình ảnh
                                        </div>
                                        <div class="form-row marbot2">
                                            <span class="package-info">
                                                <label>
                                                    <input id="images" type="file" onchange="readFile(this);" multiple>
                                                    <span>Thêm ảnh</span>
                                                </label>
                                            </span>
                                            <ul id="gallery"></ul>
                                        </div>
                                        <div class="form-row marbot1">
                                            Trạng thái
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0" Text="Đã hủy"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Chưa nhận hàng tại TQ"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Đã về kho TQ"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Đã về kho VN"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Đã giao cho khách"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-row no-margin center-txt">
                                            <asp:Button ID="btncreateuser" runat="server" Text="Cập nhật" CssClass="btn primary-btn"
                                                OnClick="btncreateuser_Click" />
                                            <asp:Button ID="btnBack" runat="server" Text="Trở về" CssClass="btn primary-btn"
                                                OnClick="btnBack_Click" CausesValidation="false" />
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
        function Update() {
            var base64 = "";
            $("#gallery li img").each(function () {
                base64 += $(this).attr('src') + "|";
            })
            $("#<%=hdfListIMG.ClientID%>").val(base64);
            $("#<%=btncreateuser.ClientID%>").click();
        }

        var list = $("#<%=hdfListIMG.ClientID%>").val();
        if (!isEmpty(list)) {
            var IMG = list.split('|');
            var html = "";
            for (var i = 0; i < IMG.length - 1; i++) {
                html += "<li class=\"" + i + "\"><img src=\"" + IMG[i] + "\" class=\"img-thumbnail\"><a onclick=\"Delete(" + i + ")\">Xóa</a></li>";
            }
            $("#gallery").append(html);
        }

        function Delete(obj) {
            //$("li." + obj + "").remove();
            obj.parent().remove();
        }

            
        function readFile(input) {
            var k = 0;
            var counter = input.files.length;
            for (x = 0; x < counter; x++) {
                if (input.files && input.files[x]) {
                    var reader = new FileReader();
                    var t = k + x;
                    reader.onload = function (e) {
                        //$("#gallery").append('<li class=\"2' + t + '\"><img src="' + e.target.result + '" class="img-thumbnail"><a href=\"javascript:;\" onclick=\"Delete(2' + t + ')\">Xóa</a></li>');
                        $("#gallery").append('<li class=\"2' + t + '\"><img src="' + e.target.result + '" class="img-thumbnail"><a href=\"javascript:;\" onclick=\"Delete($(this))\">Xóa</a></li>');
                    };
                    reader.readAsDataURL(input.files[x]);
                    k++;
                }
            }
        }
        function isEmpty(str) {
            return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
        }
    </script>
    <asp:HiddenField runat="server" ID="hdfListIMG" />
    <style>
        #gallery li {
            width: 30%;
            margin: 0 5px;
            min-width: 150px;
        }

        ul#gallery {
            list-style: none;
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -ms-flex-wrap: wrap;
            flex-wrap: wrap;
            -webkit-box-align: center;
            -ms-flex-align: center;
            align-items: center;
        }

        .package-info label input {
            display: none;
        }

        .package-info label {
            border: 1px solid #e1e1e1;
            background-color: #fff;
            padding: 5px 20px;
            cursor: pointer;
        }

            .package-info label span {
                color: #2154b0;
                font-size: 14px;
                font-weight: 200;
            }
    </style>
</asp:Content>
