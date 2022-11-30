<%@ Page Title="Cấu hình phí dịch vụ mua hàng" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="Tariff-Buypro.aspx.cs" Inherits="NHST.manager.Tariff_Buypro" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">CẤU HÌNH PHÍ DỊCH VỤ MUA HÀNG</h4>
                </div>
            </div>
            <div class="col s12 m12 l8 section">
                <div class="list-table card-panel">
                    <div class="responsive-tb">
                        <table class="table highlight   bordered">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Giá từ</th>
                                    <th>Giá đến</th>
                                    <th>Phần trăm</th>
                                    <th>Thao tác</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                            </tbody>
                        </table>

                    </div>
                    <div class="pagi-table float-right mt-2">
                        <%this.DisplayHtmlStringPaging1();%>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>

    </div>
    <div id="modalEditFee" class="modal modal-fixed-footer">
        <div class="modal-hd">
            <span class="right"><i class="material-icons modal-close right-align">clear</i></span>
            <h4 class="no-margin center-align">CẤU HÌNH PHÍ DỊCH VỤ MUA HÀNG#<asp:Label runat="server" ID="lbID"></asp:Label></h4>
        </div>
        <div class="modal-bd">
            <div class="row">
                <div class="input-field col s12 m6">
                    <asp:TextBox placeholder="" runat="server" ID="EditAmountFrom" type="number" class="validate"></asp:TextBox>
                    <label class="active" for="EditAmountFrom"><span class="red-text">*</span> Giá từ:</label>
                    <span class="helper-text" data-error="Vui lòng nhập giá từ"></span>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox placeholder="" runat="server" ID="EditAmountTo" type="number" class="validate"></asp:TextBox>
                    <label class="active" for="EditAmountTo"><span class="red-text">*</span> Giá đến:</label>
                    <span class="helper-text" data-error="Vui lòng nhập giá đến"></span>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox placeholder="" runat="server" ID="EditFeePercent" type="number" class="validate"></asp:TextBox>
                    <label class="active" for="EditFeePercent"><span class="red-text">*</span> Phí dịch vụ (%):</label>
                    <span class="helper-text" data-error="Vui lòng nhập phí dịch vụ"></span>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox placeholder="" runat="server" ID="EditFeeMoney" type="number" class="validate" data-type="text-only"></asp:TextBox>
                    <label class="active" for="">Phí (¥)</label>
                </div>
            </div>
        </div>
        <div class="modal-ft">
            <div class="ft-wrap center-align">
                <asp:Button runat="server" ID="btnUpdate" OnClick="btnUpdate_Click" UseSubmitBehavior="false" class=" btn white-text mr-2" Text="Cập nhật"></asp:Button>
                <a href="#!" class="modal-action btn orange darken-2 modal-close waves-effect waves-green ml-2">Hủy</a>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdfID" runat="server" />
    <script>
        function EditFunction(ID) {
            $.ajax({
                type: "POST",
                url: "/manager/Tariff-Buypro.aspx/loadinfo",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = JSON.parse(msg.d);
                    if (data != null) {
                        $('#<%=hdfID.ClientID%>').val(ID);
                         $('#<%=lbID.ClientID%>').text(ID);
                         $('#<%=EditAmountFrom.ClientID%>').val(data.AmountFrom);
                         $('#<%=EditAmountTo.ClientID%>').val(data.AmountTo);
                         $('#<%=EditFeePercent.ClientID%>').val(data.FeePercent);
                         $('#<%=EditFeeMoney.ClientID%>').val(data.FeeMoney);


                         $('select').formSelect();
                     }
                     else
                         swal("Error", "Không thành công", "error");
                 },
                 error: function (xmlhttprequest, textstatus, errorthrow) {
                     swal("Error", "Fail updateInfoAcc", "error");
                 }
             });
        }
    </script>
    <style>
        .modal.modal-fixed-footer {
            height: auto;
        }
    </style>
</asp:Content>
