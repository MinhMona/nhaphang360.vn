<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMaster.Master" AutoEventWireup="true" CodeBehind="lich-su-nap-tien.aspx.cs" Inherits="NHST.lich_su_nap_tien" %>

<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="all">
            <div class="main">
                <div class="sec form-sec">
                    <div class="sec-tt">
                        <h2 class="tt-txt">Lịch sử giao dịch</h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                        </p>
                    </div>
                    <div class="primary-form custom-width">
                        <table class="customer-table mar-bot3 full-width font-size-16">
                            <tr>
                                <td>Số dư tài khoản
                                </td>
                                <td>
                                    <asp:Label ID="lblAccount" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div class="step-income">
                            <table class="customer-table mar-top1 full-width">
                                <tr>
                                    <th width="20%" style="text-align: center">Ngày giờ</th>
                                    <th width="20%" style="text-align: center">Nội dung</th>
                                    <th width="20%" style="text-align: center">Số tiền</th>
                                    <th width="20%" style="text-align: center">Loại giao dịch</th>
                                    <th width="20%" style="text-align: center">Số dư</th>
                                </tr>
                                <asp:Literal ID="ltr" runat="server"></asp:Literal>

                                <%--<asp:Repeater ID="rpt" runat="server" EnableViewState="false">
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: center"><%#Eval("CreatedDate","{0:dd/MM/yyyy HH:mm}") %></td>
                                            <td style="text-align: center"><%#Eval("HContent") %></td>
                                            <td style="text-align: center">
                                                <strong class="font-size-16"><%#Eval("Type").ToString() == %>
                                                    <%#Eval("Amount","{0:N0}") %> VNĐ</strong><br />
                                            </td>
                                            <td style="text-align: center">
                                                <strong class="font-size-16"><%# PJUtils.GetTradeType(Convert.ToInt32(Eval("TradeType"))) %></strong><br />
                                            </td>
                                            <td style="text-align: center">
                                                <strong class="font-size-16">
                                                    <%# string.Format("{0:N0}", Eval("MoneyLeft").ToString().ToFloat(0)) %> VNĐ</strong><br />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>--%>
                            </table>
                            <div class="pagination">
                                <%this.DisplayHtmlStringPaging1();%>                               
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>

</asp:Content>
