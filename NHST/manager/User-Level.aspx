<%@ Page Title="Cấu hình phí người dùng" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="User-Level.aspx.cs" Inherits="NHST.manager.User_Level" %>
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
                <h4 class="title no-margin" style="display:inline-block;">CẤU HÌNH PHÍ NGƯỜI DÙNG</h4>

            </div>
        </div>
        <div class="col s12 m12 section">
            <div class="list-table card-panel">
                <div class="responsive-tb">
                    <table class="table bordered centered highlight  ">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Cấp người dùng</th>
                                <th class="tb-date">Chiếc khấu phí mua hàng</th>
                                <th class="tb-date">Chiếc khấu phí vận chuyển TQ - VN</th>
                                <th>Đặt cọc tối thiểu</th>
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
<!-- END: Page Main-->


<!-- BEGIN: Modal Add NV -->
<!-- Modal Structure -->
<div id="modalEditFee" class="modal modal-fixed-footer">
    <div class="modal-hd">
        <span class="right"><i class="material-icons modal-close right-align">clear</i></span>
        <h4 class="no-margin center-align">CẤU HÌNH PHÍ NGƯỜI DÙNG#<asp:Label runat="server" ID="lbID"></asp:Label></h4>
    </div>
    <div class="modal-bd">
        <div class="row">
            <div class="input-field col s12">
                <asp:TextBox runat="server" id="txtLevelName" type="text" class="validate" data-type="text-only" value="0"></asp:TextBox>
                <label for="edit_from_warehouse">Cấp người dùng</label>
            </div>
            <div class="input-field col s12">
                <asp:TextBox runat="server" id="txtFeeBuyPro" type="text" class="validate" data-type="text-only" value="3,000,000"></asp:TextBox>
                <label for="edit_to_warehouse">Chiết khấu phí mua hàng </label>
            </div>
            <div class="input-field col s12">
                <asp:TextBox runat="server" id="txtFeeWeight" type="text" class="validate" data-type="text-only" value="0"></asp:TextBox>
                <label for="edit_type">Chiết khấu phí vận chuyển TQ-VN</label>
            </div>
            <div class="input-field col s12">
                <asp:TextBox runat="server" id="txtLessDeposit" type="text" class="validate" data-type="text-only" value="0"></asp:TextBox>
                <label for="edit_fee">Đặt cọc tối thiểu</label>
            </div>
        </div>
    </div>
    <div class="modal-ft">
        <div class="ft-wrap center-align">
            <asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" UseSubmitBehavior="false" class="white-text btn mr-2" Text="Cập nhật"></asp:Button>
            <a href="#!" class="modal-action btn orange darken-2 modal-close waves-effect waves-green ml-2">Hủy</a>
        </div>

    </div>
</div>
    <asp:HiddenField runat="server" ID="hdfID" />

    <script>
        function EditFunction(ID) {
            $.ajax({
                type: "POST",
                url: "/manager/User-Level.aspx/loadinfo",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = JSON.parse(msg.d);
                    if (data != null) {
                        $('#<%=hdfID.ClientID%>').val(ID);
                        $('#<%=lbID.ClientID%>').text(ID);
                        $('#<%=txtLevelName.ClientID%>').val(data.LevelName);
                        $('#<%=txtFeeBuyPro.ClientID%>').val(data.FeeBuyPro);
                        $('#<%=txtFeeWeight.ClientID%>').val(data.FeeWeight);
                        $('#<%=txtLessDeposit.ClientID%>').val(data.LessDeposit);
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
</asp:Content>
