<%@ Page Title="Phí kiểm đếm" Language="C#" AutoEventWireup="true"  MasterPageFile="~/manager/adminMasterNew.Master" CodeBehind="FeecheckproductList.aspx.cs" Inherits="NHST.manager.FeecheckproductList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Phí kiểm đếm</h4>
                    <%--  <div class="right-action">
                        <a href="#addStaff" class="btn  modal-trigger waves-effect">Thêm phí kiểm đêm</a>
                    </div>--%>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="list-staff col s12 section">
                <div class="list-table card-panel">
                    <div class="responsive-tb">
                        <table class="table   bordered highlight">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Số lượng từ</th>
                                    <th>Số lượng đến</th>
                                    <th>Phí</th>
                                    <th>Loại</th>
                                    <th>Thao tác</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal runat="server" ID="ltr"></asp:Literal>
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
     <div id="addStaff" class="modal ">
        <div class="modal-hd">
            <span class="right"><i class="material-icons modal-close right-align">clear</i></span>
            <h4 class="no-margin center-align">Thêm phí kiểm đếm</h4>
        </div>
        <div class="modal-bd">
            <div class="row">
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="addAmountFrom" type="text" placeholder="" data-type="number"></asp:TextBox>
                    <label for="addAmountFrom">Số lượng từ</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="addAmountTo" type="text" placeholder="" data-type="number"></asp:TextBox>
                    <label for="addAmountTo">Số lượng đến</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="addPrice" type="text" placeholder="" data-type="currency"></asp:TextBox>
                    <label for="addPrice">Phí (VND)</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:DropDownList runat="server" ID="ddlType">
                        <asp:ListItem Selected="True" Value="1">Dưới 10 tệ</asp:ListItem>
                        <asp:ListItem Value="2">Trên 10 tệ</asp:ListItem>
                    </asp:DropDownList>
                    <label>Loại</label>
                </div>
            </div>
        </div>
        <div class="modal-ft">
            <div class="ft-wrap center-align">
                <a onclick="Add()" class="modal-action btn modal-close waves-effect waves-green mr-2">Thêm</a>
                <a  class="modal-action btn orange darken-2 modal-close waves-effect waves-green ml-2">Hủy</a>
            </div>
        </div>
    </div>
    <!-- END: Modal Add NV -->
    <div class="row">
        <div class="bg-overlay"></div>
        <!-- Edit mode -->
        <div class="detail-fixed  col s12 m6">
            <div class="rp-detail card-panel row">
                <div class="col s12">
                    <div class="page-title">
                        <h5>Phí kiểm đếm #<asp:Label runat="server" ID="lbID"></asp:Label></h5>
                        <a href="#!" class="close-editmode top-right valign-wrapper"><i
                            class="material-icons">close</i>Close</a>
                    </div>
                </div>
                <div class="col s12">
                    <div class="row">
                        <div class="input-field col s12 m6">
                            <asp:TextBox runat="server" ID="txtEditAmountFrom" type="text" placeholder="" ReadOnly="true" data-type="number"></asp:TextBox>
                            <label for="txtEditAmountFrom">Số lượng từ</label>
                        </div>
                        <div class="input-field col s12 m6">
                            <asp:TextBox runat="server" ID="txtEditAmountTo" type="text" placeholder="" ReadOnly="true" data-type="number"></asp:TextBox>
                            <label for="txtEditAmountTo">Số lượng đến</label>
                        </div>
                        <div class="input-field col s12 m6">
                            <asp:TextBox runat="server" ID="txtEditPrice" type="text" placeholder="" data-type="currency"></asp:TextBox>
                            <label for="txtEditPrice">Phí (VND)</label>
                        </div>
                        <div class="input-field col s12 m6">
                            <asp:DropDownList runat="server" Enabled="false" ID="ddlEditType">
                                <asp:ListItem Selected="True" Value="1">Dưới 10 tệ</asp:ListItem>
                                <asp:ListItem Value="2">Trên 10 tệ</asp:ListItem>
                            </asp:DropDownList>
                            <label>Loại</label>
                        </div>
                        <div class="modal-ft">
                            <div class="ft-wrap center-align">
                                <a onclick="Update()" class="modal-action btn modal-close waves-effect waves-green mr-2">Cập nhật</a>
                                <a class=" btn orange darken-2 close-editmode waves-effect waves-green ml-2">Trở về</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- END : Edit mode -->
    <%--<asp:Button runat="server" ID="btnAdd" OnClick="btnAdd_Click" UseSubmitBehavior="false" Style="display: none" />--%>
    <asp:Button runat="server" ID="btnUpdate" OnClick="btnUpdate_Click" UseSubmitBehavior="false" Style="display: none" />
    <asp:HiddenField runat="server" ID="hdfIDFee" />
    <script>
        function Update() {
            $('#<%=btnUpdate.ClientID%>').click();
        }
        function EditFunction(ID) {
            $.ajax({
                type: "POST",
                url: "/manager/Feecheckproductlist.aspx/loadinfo",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = JSON.parse(msg.d);
                    if (data != null) {
                        $('#<%=lbID.ClientID%>').text(ID);
                        $('#<%=hdfIDFee.ClientID%>').val(ID);
                        $('#<%=txtEditAmountFrom.ClientID%>').val(data.AmountFrom);
                        $('#<%=txtEditAmountTo.ClientID%>').val(data.AmountTo);
                        $('#<%=txtEditPrice.ClientID%>').val(data.Price);
                        $('#<%=ddlEditType.ClientID%>').val(data.Type);
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

        function Add() {

        }
    </script>
</asp:Content>

