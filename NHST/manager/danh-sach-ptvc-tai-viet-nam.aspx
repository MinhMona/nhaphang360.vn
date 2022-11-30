<%@ Page Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="danh-sach-ptvc-tai-viet-nam.aspx.cs" Inherits="NHST.manager.danh_sach_ptvc_tai_viet_nam" %>

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
                    <h4 class="title no-margin" style="display: inline-block;">Danh sách phương thức vận chuyển tại Việt Nam</h4>
                    <div class="right-action">
                        <a href="#" class="btn" id="filter-btn">Bộ lọc</a>
                    </div>
                    <div class="clearfix"></div>
                    <div class="filter-wrap" style="display: none">
                        <div class="row mt-2 pt-2">
                            <div class="input-field col s6 l6">
                                <asp:TextBox runat="server" ID="tSearch" type="text" class="form-control" placeholder=""></asp:TextBox>
                                <label class="active">Tiêu đề</label>
                            </div>
                            <div class="input-field col s6 left-align">
                                <asp:Button ID="search" runat="server" OnClick="btnSearch_Click" class="btn " Text="Lọc kết quả"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="list-staff col s12 section">
                <div class="list-table card-panel">
                    <%--  <div class="table-info row center-align-xs">
                        <span class="checkout col s12 m6">Tổng cân nặng : <span class="font-weight-700 black-text">300 kg</span>
                        </span>
                    </div>--%>
                    <div class="responsive-tb">
                        <table class="table  highlight bordered   mt-2">
                            <thead>
                                <tr>
                                    <th style="min-width: 100px;">ID</th>
                                    <th style="min-width: 100px;">Tiêu đề</th>
                                    <th style="min-width: 100px;">Trạng thái</th>
                                    <th style="min-width: 100px;">Ngày tạo</th>
                                    <th style="min-width: 100px;">Thao tác</th>
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

    <div id="modalEditPTVC" class="modal modal-fixed-footer">
        <div class="modal-hd">
            <span class="right"><i class="material-icons modal-close right-align">clear</i></span>
            <h4 class="no-margin center-align">Chỉnh sửa PTVC</h4>
        </div>
        <div class="modal-bd">
            <div class="row">
                <div class="input-field col s12">
                    <asp:TextBox runat="server" ID="txtTitle" placeholder="" type="text" class="validate" data-type="text-only"></asp:TextBox>
                    <label class="active" for="edit__step-name">Tiêu đề</label>
                </div>

                <div class="input-field col s12">
                    <div class="switch status-func">
                        <span class="mr-2">Trạng thái:  </span>
                        <label>
                            Ẩn<asp:TextBox ID="EditStatus" runat="server" type="checkbox" onclick="StatusPTVCFunction()"></asp:TextBox><span class="lever"></span>
                            Hiện
                        </label>

                    </div>
                </div>
            </div>
        </div>
        <div class="modal-ft">
            <div class="ft-wrap center-align">
                <a id="BtnUpdate" onclick="btnUpdate()" class="modal-action btn modal-close waves-effect waves-green mr-2">Cập nhật</a>
                <a href="#!" class="modal-action btn orange darken-2 modal-close waves-effect waves-green ml-2">Hủy</a>
            </div>
        </div>
    </div>
    <asp:Button runat="server" ID="btnUpdate" OnClick="btnUpdate_Click" Style="display: none" />
    <asp:HiddenField runat="server" ID="hdfPTVCStatus" />
    <asp:HiddenField runat="server" ID="hdfID" />
    <script>

        $(document).ready(function () {
            $('#txtSearchName').autocomplete({
                data: {
                    "Apple": null,
                    "Microsoft": null,
                    "Google": 'https://placehold.it/250x250',
                    "Asgard": null
                },
            });
        });
        function myFunction() {
            if (event.which == 13 || event.keyCode == 13) {
                $('#<%=search.ClientID%>').click();
            }
        }
        $('.search-action').click(function () {

            $('#<%=search.ClientID%>').click();
        })

        function GetInfo(ID) {
            $.ajax({
                type: "POST",
                url: "/manager/danh-sach-ptvc-tai-viet-nam.aspx/loadinfo",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = JSON.parse(msg.d);
                    if (data != null) {
                        $('#<%=txtTitle.ClientID%>').val(data.ShippingTypeVNName);
                        var a = data.IsHidden;
                        if (a == false) {
                            $('#<%=EditStatus.ClientID%>').prop('checked', true);
                            $('#<%=hdfPTVCStatus.ClientID%>').val('0');
                        }
                        else {
                            $('#<%=EditStatus.ClientID%>').prop('checked', false);
                            $('#<%=hdfPTVCStatus.ClientID%>').val('1');
                        }
                        $('#<%=hdfID.ClientID%>').val(data.ID);
                        $("#modalEditPTVC").modal('open');
                    }
                    else
                        swal("Error", "Không thành công", "error");
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    swal("Error", "Fail updateInfoAcc", "error");
                }
            });
        }

        function StatusPTVCFunction() {

            var a = $('#<%=hdfPTVCStatus.ClientID%>').val();
            if (a == '0') {

                $('#<%=hdfPTVCStatus.ClientID%>').val('1');
              }
              else {

                  $('#<%=hdfPTVCStatus.ClientID%>').val('0');
            }
            console.log($('#<%=hdfPTVCStatus.ClientID%>').val());
        }

        function btnUpdate() {
            $('#<%=btnUpdate.ClientID%>').click();
        }

    </script>
</asp:Content>

