<%@ Page Title="Cấu hình phí vận chuyển TQ - VN" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="Tariff-TQVN.aspx.cs" Inherits="NHST.manager.Tariff_TQVN" %>

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
                    <h4 class="title no-margin" style="display: inline-block;">CẤU HÌNH PHÍ VẬN CHUYỂN TQ - VN</h4>
                </div>
            </div>
            <div class="list-staff col s12 section">
                <div class="list-table card-panel">
                    <div class="filter">
                        <div class="search-name input-field">
                            <asp:TextBox ID="search_name" placerholder="" type="text" onkeypress="myFunction()" runat="server" />
                            <label for="search_name"><span>Tên kho</span></label>
                            <span class="material-icons search-action">search</span>
                            <asp:Button Style="display: none" UseSubmitBehavior="false" ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                    <div class="responsive-tb">
                        <table class="table highlight bordered  ">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Từ kho</th>
                                    <th>Đến kho</th>
                                    <th>Cân nặng từ</th>
                                    <th>Cân nặng đến</th>
                                    <th>Giá (VNĐ)</th>
                                    <th>Hình thức VC</th>
                                    <th>Loại đơn hàng</th>
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
            <h4 class="no-margin center-align">CẤU HÌNH PHÍ VẬN CHUYỂN TQ - VN</h4>
        </div>
        <div class="modal-bd">
            <div class="row">
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txtEditWareFrom" type="text" class="validate" data-type="text-only" value="TQ 1"
                        disabled></asp:TextBox>
                    <label for="edit_from_warehouse">Từ kho</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txtEditWareTo" type="text" class="validate" data-type="text-only" value="Ha Noi"
                        disabled></asp:TextBox>
                    <label for="edit_to_warehouse">đến kho</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txtEditWeightFrom" type="number" class="validate" data-type="text-only" value="0"></asp:TextBox>
                    <label for="edit_weight_from">Cân nặng từ</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txtEditWeighTo" type="number" class="validate" data-type="text-only" value="0"></asp:TextBox>
                    <label for="edit_weight_to">Cân nặng đến</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:ListBox runat="server" name="method" ID="lbEditShippingName">
                        <asp:ListItem Value="1">Đi thường</asp:ListItem>
                        <asp:ListItem Value="4">Đi nhanh</asp:ListItem>
                    </asp:ListBox>
                    <label for="edit_method">Hình thức vận chuyển</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:ListBox runat="server" name="method" ID="lbType">
                        <asp:ListItem Value="1">Đơn kí gửi</asp:ListItem>
                        <asp:ListItem Value="0">Đơn mua hộ</asp:ListItem>
                    </asp:ListBox>
                    <label for="edit_type">Loại đơn hàng</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txtEditFee" type="text" class="validate" data-type="text-only" value="0"></asp:TextBox>
                    <label for="edit_fee">Phí</label>
                </div>
            </div>
        </div>
        <div class="modal-ft">
            <div class="ft-wrap center-align">
                <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" UseSubmitBehavior="false" class="white-text btn  mr-2" Text="Cập nhật"></asp:Button>
                <a href="#!" class="modal-action btn orange darken-2 modal-close waves-effect waves-green ml-2">Hủy</a>
            </div>

        </div>
    </div>
    <asp:HiddenField ID="hdfID" runat="server" />
    <script>
        function EditFunction(ID) {
            $.ajax({
                type: "POST",
                url: "/manager/Tariff-TQVN.aspx/loadinfo",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = JSON.parse(msg.d);
                    if (data != null) {
                        $('#<%=hdfID.ClientID%>').val(ID);
                        $('#<%=txtEditWareFrom.ClientID%>').val(data.WFromName);
                        $('#<%=txtEditWareTo.ClientID%>').val(data.WToName);
                         $('#<%=txtEditWeightFrom.ClientID%>').val(data.WeightFrom);
                         $('#<%=txtEditWeighTo.ClientID%>').val(data.WeightTo);
                         $('#<%=txtEditFee.ClientID%>').val(data.Price);
                         $('#<%=lbType.ClientID%>').val(data.IsHelpMoving);
                         $('#<%=lbEditShippingName.ClientID%>').val(data.ShippingTypeToWareHouseID);
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
        function myFunction() {
            if (event.which == 13 || event.keyCode == 13) {
                console.log($('#<%=search_name.ClientID%>').val());
                $('#<%=btnSearch.ClientID%>').click();
            }
        }
        $('.search-action').click(function () {
            console.log('dkm ngon');
            console.log($('#<%=search_name.ClientID%>').val());
            $('#<%=btnSearch.ClientID%>').click();
        })
    </script>
    <style>
        .modal.modal-fixed-footer {
            height: auto;
        }
    </style>
</asp:Content>
