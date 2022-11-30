<%@ Page Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="danh-sach-yeu-cau-giao-hang.aspx.cs" Inherits="NHST.manager.danh_sach_yeu_cau_giao_hang" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button Style="display: none" UseSubmitBehavior="false" ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Danh sách yêu cầu giao hàng</h4>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="list-staff col s12 section">
                <div class="list-table card-panel">
                    <div class="filter">
                        <div class="search-name input-field">
                            <asp:TextBox ID="search_name" type="text" placeholder="" onkeypress="myFunction()" runat="server" />
                            <label for="search_name"><span>Phone</span></label>
                            <span class="material-icons search-action">search</span>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <table class="table responsive-table bordered highlight">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Mã đơn hàng</th>
                                <th>Họ tên người nhận</th>
                                <th>Phone</th>
                                <th>Địa chỉ</th>
                                <th>Ghi chú</th>
                                <th>Trạng thái</th>
                                <th>Ngày tạo</th>
                                <th>Thao tác</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                        </tbody>
                    </table>
                    <div class="pagi-table float-right mt-2">
                        <%this.DisplayHtmlStringPaging1();%>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="bg-overlay"></div>
            <!-- Edit mode -->
            <div class="detail-fixed col s12 m12 l8 xl8 section">
                <div class="rp-detail card-panel row">
                    <div class="col s12">
                        <div class="page-title">
                            <h5>Chi tiết yêu cầu #<asp:Label runat="server" ID="labelID" Text=""></asp:Label></h5>
                            <a href="#!" class="close-editmode top-right valign-wrapper"><i
                                class="material-icons">close</i>Close</a>
                        </div>
                    </div>
                    <div class="col s12">
                        <div class="row pb-2 border-bottom-1 ">
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" ID="txtMainOrderID" BackColor="LightGray" type="text" value="1006" class="validate" Enabled="false"></asp:TextBox>
                                <label for="rp_shopid">Mã đơn hàng</label>
                            </div>

                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" ID="txtFullname" type="text" class="validate" Enabled="false" value="100.000.000"></asp:TextBox>
                                <label for="rp_vnd">Họ tên người nhận</label>
                            </div>
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" ID="txtPhone" type="text" class="validate" Enabled="false" value="30.000"></asp:TextBox>
                                <label for="rp_yuan">SĐT</label>
                            </div>
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" ID="txtAddress" type="text" class="validate" Enabled="false" value="30.000"></asp:TextBox>
                                <label for="rp_yuan">Địa chỉ</label>
                            </div>

                            <div class="input-field col s12">
                                <asp:TextBox runat="server" BackColor="LightGray" disabled TextMode="MultiLine" ID="txtNote" placeholder=""
                                    CssClass="materialize-textarea"></asp:TextBox>
                                <label class="active">Nội dung</label>
                            </div>
                            <div class="input-field col s12">
                                <asp:ListBox runat="server" ID="lbStatus">
                                    <asp:ListItem Value="1">Chờ duyệt</asp:ListItem>
                                    <asp:ListItem Value="2">Đã duyệt</asp:ListItem>
                                    <asp:ListItem Value="3">Đã hủy</asp:ListItem>
                                </asp:ListBox>
                                <label>Trạng thái</label>
                            </div>
                        </div>
                        <div class="row section mt-2">
                            <div class="col s12">
                                <asp:Button runat="server" ID="btnUpdate" OnClick="btnUpdate_Click" UseSubmitBehavior="false" class="btn" Text="Cập nhật"></asp:Button>
                                <a href="#" class="btn close-editmode">Trở về</a>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdfID" runat="server" />
    <asp:HiddenField ID="hdfUserName" runat="server" />
    <script>

        function YcgInfo(ID) {
            $('.list-img').empty();

            $.ajax({
                type: "POST",
                url: "/manager/danh-sach-yeu-cau-giao-hang.aspx/loadinfoYCG",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = JSON.parse(msg.d);
                    if (data != null) {
                        $('#<%=labelID.ClientID%>').text(ID);
                        $('#<%=txtMainOrderID.ClientID%>').val(data.MainOrderID);
                        $('#<%=txtFullname.ClientID%>').val(data.FullName);
                        $('#<%=txtPhone.ClientID%>').val(data.Phone);
                        $('#<%=txtAddress.ClientID%>').val(data.Address);
                        $('#<%=txtNote.ClientID%>').val(data.Note);
                        $('#<%=hdfID.ClientID%>').val(ID);
                        $('#<%=lbStatus.ClientID%>').val(data.Status);
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
        $(document).ready(function () {
            $('#search_name').autocomplete({
                data: {
                    "Apple": null,
                    "Microsoft": null,
                    "Google": 'https://placehold.it/250x250',
                    "Asgard": null
                },
            });
        });
    </script>

</asp:Content>
