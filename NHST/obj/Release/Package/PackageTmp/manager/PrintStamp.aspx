<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="PrintStamp.aspx.cs" Inherits="NHST.manager.PrintStamp" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .stamp-row {
            float: left;
            width: 100%;
            margin: 5px 0;
            color: #e84545;
            font-size: 20px;
            font-weight: bold;
        }

        .label-print {
            float: left;
            width: 40%;
        }

        .label-text {
            float: left;
            width: 60%;
        }

        @media print {
            .stamp-row {
                font-size: 20px;
                font-weight: bold;
            }

            .label-print {
                float: left;
                width: 40%;
                font-size: 20px;
                font-weight: bold;
            }

            .label-text {
                float: left;
                width: 60%;
                font-size: 20px;
                font-weight: bold;
            }

            .font-size-25 {
                font-size: 25px;
                font-weight: bold;
            }
        }
    </style>
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
                                    <h3 class="lb">Thông tin Tem</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot1">
                                            <a href="javascript:;" onclick="printDiv()" class="btn primary-btn">In tem</a>
                                        </div>
                                        <div class="form-rowm marbot2">
                                            <div style="width: 340px; height: 189px; margin-top: 20px;" id="printcontent">
                                                <div class="stamp-row">
                                                    <span class="label-print font-size-25">Username: </span>
                                                    <span class="label-text font-size-25">
                                                        <asp:Label ID="lblUsername" runat="server"></asp:Label></span>
                                                </div>
                                                <div class="stamp-row">
                                                    <span class="label-print font-size-25">Đơn hàng: </span>
                                                    <span class="label-text font-size-25">
                                                        <asp:Label ID="lblOrderID" runat="server"></asp:Label></span>
                                                </div>
                                                <div class="stamp-row">
                                                    <span class="label-print">Số lượng: </span>
                                                    <span class="label-text">
                                                        <asp:Label ID="lblProductCount" runat="server"></asp:Label>
                                                        sản phẩm</span>
                                                </div>
                                                <div class="stamp-row">
                                                    <span class="label-print">Trọng lượng: </span>
                                                    <span class="label-text">
                                                        <asp:Label ID="lblWeight" runat="server"></asp:Label>
                                                        kg</span>
                                                </div>
                                                <div class="stamp-row">
                                                    <span class="label-print">Mã vận đơn: </span>
                                                    <span class="label-text">
                                                        <asp:Label ID="lblOrderCodeTrans" runat="server"></asp:Label>
                                                    </span>
                                                </div>
                                            </div>
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
        function printDiv() {
            var html = "";

            $('link').each(function () { // find all <link tags that have
                if ($(this).attr('rel').indexOf('stylesheet') != -1) { // rel="stylesheet"
                    html += '<link rel="stylesheet" href="' + $(this).attr("href") + '" />';
                }
            });
            html += '<body onload="window.focus(); window.print()">' + $("#printcontent").html() + '</body>';
            var w = window.open("", "print");
            if (w) { w.document.write(html); w.document.close() }
        }
    </script>
</asp:Content>
