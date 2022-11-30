<%@ Page Title="Thiết lập thông báo" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="thiet-lap-thong-bao.aspx.cs" Inherits="NHST.manager.thiet_lap_thong_bao" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <style>
        .modal.modal-fixed-footer {
            height: auto;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">CẤU HÌNH THÔNG BÁO</h4>
                </div>
            </div>
            <div class="list-staff col s12 section">
                <div class="notice-list card-panel">
                    <div class="responsive-tb">
                        <table class="table highlight bordered  ">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Tên thông báo</th>
                                    <th class="tb-date">Thông báo Admin</th>
                                    <th class="tb-date">Thông báo User</th>
                                    <th class="tb-date">Gửi mail Admin</th>
                                    <th class="tb-date">Gửi mail User</th>
                                    <th class="tb-date">Action</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                            </tbody>
                        </table>
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
            <h4 class="no-margin center-align">CẤU HÌNH THÔNG BÁO#<asp:Label runat="server" ID="lbID"></asp:Label></h4>
        </div>
        <div class="modal-bd">
            <div class="row padding-2">
                <div class="input-field col s12">
                    <asp:TextBox runat="server" ID="txtName" type="text" class="validate" data-type="text-only" value="Đăng ký" disabled></asp:TextBox>
                    <label>Tên thông báo</label>
                </div>
                <div class="input-field col s12 m6">
                    <p>
                        <label>
                            <input id="cbAdmin" type="checkbox" data-id="1" class="chk-check-option">
                            <span>Thông báo admin</span>
                        </label>
                    </p>
                </div>
                <div class="input-field col s12 m6">
                    <p>
                        <label>
                            <input id="cbUser" type="checkbox" data-id="2" class="chk-check-option">
                            <span>Thông báo user</span>
                        </label>
                    </p>
                </div>
                <div class="input-field col s12 m6">
                    <p>
                        <label>
                            <input id="cbEmailAdmin" type="checkbox" data-id="3" class="chk-check-option">
                            <span>Email admin</span>
                        </label>
                    </p>
                </div>
                <div class="input-field col s12 m6">
                    <p>
                        <label>
                            <input id="cbEmailUser" type="checkbox" data-id="4" class="chk-check-option">
                            <span>Email user</span>
                        </label>
                    </p>
                </div>

            </div>
        </div>
        <div class="modal-ft">
            <div class="ft-wrap center-align">
                <a href="javascript:;" id="btn-check-noti" class=" btn white-text mr-2">Cập nhật</a>             
                <a href="#!" class="modal-action btn orange darken-2 modal-close waves-effect waves-green ml-2">Hủy</a>
            </div>

        </div>
    </div>
     <asp:Button runat="server" style="display:none" ID="btnSave" class=" btn white-text mr-2" Text="Cập nhật" UseSubmitBehavior="false" OnClick="btnSave_Click"></asp:Button>
    <asp:HiddenField runat="server" ID="hdfID" />
    <asp:HiddenField runat="server" ID="hdfCheck" />
     <script>
        function EditFunction(ID) {
            $.ajax({
                type: "POST",
                url: "/manager/thiet-lap-thong-bao.aspx/loadinfo",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = JSON.parse(msg.d);
                    if (data != null) {
                        $('#<%=hdfID.ClientID%>').val(ID);
                        $('#<%=lbID.ClientID%>').text(ID);
                        $('#<%=txtName.ClientID%>').val(data.NotiName);
                        $('#cbAdmin').prop('checked',data.IsSentNotiAdmin);
                        $('#cbUser').prop('checked',data.IsSentNotiUser);
                        $('#cbEmailAdmin').prop('checked',data.IsSentEmailAdmin);
                        $('#cbEmailUser').prop('checked',data.IsSendEmailUser);
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
         $('#btn-check-noti').click(function () {
             var chuoi = "";
            $('.chk-check-option').each(function () {
                var id = $(this).attr('data-id');
              
                if ($(this).prop("checked") == true) {
                    chuoi += id + "," + "1|";
                }
                else {
                    chuoi += id + "," + "0|";
                }                
            });
             $('#<%=hdfCheck.ClientID%>').val(chuoi);
             $('#<%=btnSave.ClientID%>').click();
          
        })
    </script>
</asp:Content>