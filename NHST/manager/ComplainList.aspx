<%@ Page Title="Khiếu nại" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="ComplainList.aspx.cs" Inherits="NHST.manager.ComplainList1" %>

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
                    <h4 class="title no-margin" style="display: inline-block;">Khiếu nại</h4>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="list-staff col s12 section">
                <div class="list-table card-panel">
                    <div class="filter">
                        <div class="row">
                            <div class="input-field col s6 m4 l2">
                                <asp:TextBox runat="server" type="text" ID="search_name" placeholder="" class=""></asp:TextBox>
                                <label>Username</label>
                            </div>
                            <div class="input-field col s6 m4 l2">
                                <asp:DropDownList runat="server" ID="ddlStatus">
                                    <asp:ListItem Value="-1" Text="Tất cả"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Đã hủy"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Chưa duyệt"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Đang xử lý"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Đã xử lý"></asp:ListItem>
                                </asp:DropDownList>
                                <label>Trạng thái</label>
                            </div>
                            <div class="input-field col s6 m4 l2">
                                <asp:TextBox runat="server" type="text" ID="rdatefrom" placeholder="" class="datetimepicker from-date"></asp:TextBox>
                                <label>Từ ngày</label>
                            </div>
                            <div class="input-field col s6 m4 l2">
                                <asp:TextBox runat="server" ID="rdateto" type="text" placeholder="" class="datetimepicker to-date"></asp:TextBox>
                                <label>Đến ngày</label>
                                <span class="helper-text"
                                    data-error="Vui lòng chọn ngày bắt đầu trước"></span>
                            </div>
                            <div class="input-field col s12 m4 l2">
                                <a href="javascript:;" onclick="myFunction()" class="btn btnsearch">Lọc</a>
                            </div>
                        </div>

                        <div class="search-name input-field" style="display: none">
                            <%--  <asp:TextBox ID="search_name" type="text" placeholder="" onkeypress="myFunction()" runat="server" />
                            <label for="search_name"><span>Username</span></label>
                            <span class="material-icons search-action">search</span>--%>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <table class="table responsive-table bordered highlight">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Username</th>
                                <th>Mã đơn hàng</th>
                                <th>Số tiền</th>
                                <th style="width: 500px !important">Nội dung</th>
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
                            <h5>Chi tiết khiếu nại #<asp:Label runat="server" ID="labelID" Text=""></asp:Label></h5>
                            <a href="#!" class="close-editmode top-right valign-wrapper"><i
                                class="material-icons">close</i>Close</a>
                        </div>
                    </div>
                    <div class="col s12">
                        <div class="row pb-2 border-bottom-1 ">
                            <div class="input-field col s12 m3">
                                <asp:TextBox runat="server" ID="txtUserName" BackColor="LightGray" type="text" class="validate" value="chipcop106" Enabled="false"></asp:TextBox>
                                <label for="rp_username">Username</label>
                            </div>
                            <div class="input-field col s12 m3">
                                <asp:TextBox runat="server" ID="txtShopID" BackColor="LightGray" type="text" value="1006" class="validate" Enabled="false"></asp:TextBox>
                                <label for="rp_shopid">Mã đơn hàng</label>
                            </div>
                            <div class="input-field col s12 m3">
                                <asp:TextBox runat="server" ID="txtAmountCYN" BackColor="LightGray" type="text" class="validate" Enabled="false" value="30.000"></asp:TextBox>
                                <label for="rp_yuan">Số tiền (¥)</label>
                            </div>
                            <div class="input-field col s12 m3">
                                <asp:TextBox runat="server" BackColor="LightGray" ID="txtCurrence" type="text" class="validate" value="3.506" Enabled="false"></asp:TextBox>
                                <label for="rp_exchange">Tỉ giá</label>
                            </div>
                            <div class="input-field col s12">
                                <asp:TextBox runat="server" ID="txtAmountVND" type="text" class="validate" value="100.000.000"></asp:TextBox>
                                <label for="rp_vnd">Số tiền (VNĐ)</label>
                            </div>
                            <div class="input-field col s12">
                                <asp:TextBox runat="server" BackColor="LightGray" Enabled="false" TextMode="MultiLine" ID="txtComplainText" placeholder=""
                                    CssClass="materialize-textarea"></asp:TextBox>
                                <label class="active">Nội dung khiếu nại của khách hàng</label>
                            </div>
                            <div class="input-field col s12">
                                <asp:ListBox runat="server" ID="lbStatus">
                                    <asp:ListItem Value="0">Đã hủy</asp:ListItem>
                                    <asp:ListItem Value="1">Chưa duyệt</asp:ListItem>
                                    <asp:ListItem Value="2">Đang xử lý</asp:ListItem>
                                    <asp:ListItem Value="3">Đã xử lý</asp:ListItem>
                                </asp:ListBox>
                                <label>Trạng thái</label>
                            </div>
                            <div class="input-field col s12">
                                <p>Ảnh sản phẩm:</p>
                                <div class="list-img">
                                </div>
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
        function Complain(ID) {
            $('.list-img').empty();
            $.ajax({
                type: "POST",
                url: "/manager/ComplainList.aspx/loadinfoComplain",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = JSON.parse(msg.d);
                    if (data != null) {
                        $('#<%=labelID.ClientID%>').text(ID);
                        $('#<%=txtUserName.ClientID%>').val(data.UserName);
                        $('#<%=hdfUserName.ClientID%>').val(data.UserName);
                        $('#<%=txtShopID.ClientID%>').val(data.ShopID);
                        $('#<%=txtAmountVND.ClientID%>').val(data.AmountVND);
                        $('#<%=txtAmountCYN.ClientID%>').val(data.AmountCNY);
                        $('#<%=txtCurrence.ClientID%>').val(data.TiGia);
                        $('#<%=txtComplainText.ClientID%>').val(data.ComplainText);
                        $('#<%=hdfID.ClientID%>').val(ID);
                        $('#<%=lbStatus.ClientID%>').val(data.Status);
                        var listIMG = data.ListIMG;
                        if (listIMG != null) {
                            for (var i = 0; i < listIMG.length; i++) {
                                console.log(listIMG[i]);
                                if (listIMG[i] != "") {
                                    var a = "<div class=\"img-block\" style><img class=\"materialboxed\" src =\"" + listIMG[i] + "\" width =\"200\"></div>";
                                    $(".list-img").append(a);
                                }
                            }
                        }

                        $(".materialboxed").materialbox({
                            inDuration: 150,
                            onOpenStart: function (element) {
                                $(element).parents('.material-placeholder').attr('style', 'overflow:visible !important;');
                            },
                            onCloseStart: function (element) {
                                $(element).parents('.material-placeholder').attr('style', '');
                            }
                        });
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
            $('#<%=btnSearch.ClientID%>').click();
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
