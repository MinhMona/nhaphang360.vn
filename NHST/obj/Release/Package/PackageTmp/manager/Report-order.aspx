<%@ Page Title="Thống ke đơn hàng kho" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="Report-order.aspx.cs" Inherits="NHST.manager.Report_order" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div id="main" class="main-full">
    <div class="row">
        <div class="content-wrapper-before bg-dark-gradient"></div>
        <div class="page-title">
            <div class="card-panel">
                <h4 class="title no-margin" style="display:inline-block;">Thống kê đơn hàng kho</h4>
                <div class="clearfix"></div>
            </div>
        </div>
        <div class="col s12">
            <div class="card-panel">

                <div class="order-list-info">
                    <div class="total-info">
                        <div class="row section">
                            <div class="col s12 m6">
                                <div class="responsive-tb section">
                                    <div class="table table-info   ">
                                        <table class="table tb-border">
                                            <tbody>
                                                
                                                <tr>
                                                    <td colspan="2"
                                                        class="white font-weight-700 table-title teal darken-4 white-text">
                                                        ĐÃ MUA HÀNG</td>
                                                </tr>
                                                <asp:Literal runat="server" ID="ltrDaMuaHang"></asp:Literal>                                                

                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="col s12 m6">
                                <div class="responsive-tb section">
                                    <div class="table table-info   ">
                                        <table class="table tb-border">
                                            <tbody>
                                                <tr>
                                                    <td colspan="2"
                                                        class="white font-weight-700 table-title teal darken-4 white-text">
                                                        ĐÃ VỀ KHO TQ</td>
                                                </tr>
                                              <asp:Literal runat="server" ID="ltrDaVeKhoTQ"></asp:Literal>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col s12 m6">
                                <div class="responsive-tb section">
                                    <div class="table table-info   ">
                                        <table class="table tb-border">
                                            <tbody>
                                                <tr>
                                                    <td colspan="2"
                                                        class="white font-weight-700 table-title teal darken-4 white-text">
                                                        ĐÃ VỀ KHO VN</td>
                                                </tr>
                                                <asp:Literal runat="server" ID="ltrDaVeKhoVN"></asp:Literal>

                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <div class="col s12 m6">
                                <div class="responsive-tb section">
                                    <div class="table table-info   ">
                                        <table class="table tb-border">
                                            <tbody>
                                                <tr>
                                                    <td colspan="2"
                                                        class="white font-weight-700 table-title teal darken-4 white-text">
                                                        KHÁCH ĐÃ THANH TOÁN

                                                    </td>
                                                </tr>
                                                <asp:Literal runat="server" ID="ltrDaThanhToan"></asp:Literal>

                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
