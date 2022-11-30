<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMaster.Master" AutoEventWireup="true" CodeBehind="danh-sach-don-hang.aspx.cs" Inherits="NHST.danh_sach_don_hang" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/App_Themes/NewUI/css/custom.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="all">
            <div class="main">
                <div class="sec form-sec">
                    <div class="sec-tt">
                        <h2 class="tt-txt">Danh sách đơn hàng</h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                        </p>
                    </div>
                    <div class="content-text">
                        <div class="table-panel">
                            <div class="table-panel-main full-width">
                                <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                                    AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                                    AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                                    AllowSorting="True" AllowFilteringByColumn="false">
                                    <MasterTableView CssClass="table table-bordered table-hover" DataKeyNames="ID">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%" FilterControlWidth="50px" AutoPostBackOnFilter="false"
                                                CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Ảnh sản phẩm" HeaderStyle-Width="10%"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <img src="<%#Eval("ProductImage") %>" width="100%" alt />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="ShopName" HeaderText="Tên Shop" HeaderStyle-Width="15%"
                                                FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Site" HeaderText="Website" HeaderStyle-Width="15%"
                                                FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Tổng tiền" HeaderStyle-Width="10%"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" 
                                                FilterDelay="2000" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <p class=""><%# string.Format("{0:N0}", Convert.ToDouble(Eval("TotalPriceVND"))) %> vnđ</p>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Số Tiền phải cọc" HeaderStyle-Width="10%"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("AmountDeposit"))) %> vnđ</p>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn HeaderText="Tiền đã cọc" HeaderStyle-Width="10%"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("Deposit"))) %> vnđ</p>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>

                                            <telerik:GridTemplateColumn HeaderText="Ngày đặt hàng" HeaderStyle-Width="10%"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                SortExpression="CreatedDate" FilterControlWidth="100px" AutoPostBackOnFilter="false"
                                                CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                                <ItemTemplate>
                                                    <%#Eval("CreatedDate","{0:dd/MM/yyyy HH:mm}") %>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="statusstring" HeaderText="Trạng thái đơn hàng" HeaderStyle-Width="15%"
                                                FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center"
                                                ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                                                <ItemTemplate>
                                                    <a href="/chi-tiet-don-hang/<%#Eval("ID") %>" class="viewmore-orderlist">Chi tiết</a>
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>

                                        <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                                            PrevPageText="← Previous" />
                                    </MasterTableView>
                                </telerik:RadGrid>
                                <%--<table>
                                    <tr>
                                        <th class="id">Mã đơn hàng</th>
                                        <th class="pro">Ảnh sản phẩm</th>
                                        <th class="pro">Tên Shop</th>
                                        <th class="qty">Website</th>
                                        <th class="price">Tổng tiền</th>
                                        <th class="price">Số Tiền phải cọc</th>
                                        <th class="price">Tiền đã cọc</th>
                                        <th class="date">Ngày đặt hàng</th>
                                        <th class="status">Trạng thái đơn hàng</th>
                                        <th class="status"></th>
                                    </tr>
                                    <asp:Repeater ID="rpt" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td class="id"><%#Eval("ID") %></td>
                                                <td class="pro">
                                                    <img src='<%# PJUtils.GetFirstProductIMG(Eval("ID").ToString()) %>' width="100%" alt="" />
                                                </td>
                                                <td class="pro">
                                                    <%#Eval("ShopName") %>
                                                </td>
                                                <td class="pro">
                                                    <%#Eval("Site") %>
                                                </td>

                                                <td class="price">
                                                    <p class=""><%# string.Format("{0:N0}", Convert.ToDouble(Eval("TotalPriceVND"))) %> đ</p>
                                                </td>
                                                <td class="price">
                                                    <p class=""><%# string.Format("{0:N0}", Convert.ToDouble(Eval("AmountDeposit"))) %> đ</p>
                                                </td>
                                                <td class="price">
                                                    <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("Deposit"))) %> đ</p>
                                                </td>
                                                <td class="date"><%#Eval("CreatedDate","{0:dd/MM/yyyy HH:mm}") %></td>
                                                <td class="status"><%# NHST.Bussiness.PJUtils.IntToRequestClient(Convert.ToInt32(Eval("Status"))) %></td>
                                                <td class="status"><a href="/chi-tiet-don-hang/<%#Eval("ID") %>" class="viewmore">Xem chi tiết</a></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
    <style>
        .table.table-bordered > thead > tr > th, .table.table-bordered > tbody > tr > th, .table.table-bordered > tfoot > tr > th, .table.table-bordered > thead > tr > td, .table.table-bordered > tbody > tr > td, .table.table-bordered > tfoot > tr > td {
            padding: 10px 0;
        }

        .rgPager table, .rgPager table:hover {
            border: none !important;
        }

            .rgPager table th {
                background: none;
            }
    </style>
    <%--<telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="pEdit" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnUpdate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pEdit" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>    --%>
</asp:Content>
