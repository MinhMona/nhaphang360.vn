<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="trade-history.aspx.cs" Inherits="NHST.manager.trade_history" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <h1 class="page-title">Lịch sử giao dịch của <strong>
            <asp:Label ID="lblUsername" runat="server"></asp:Label></strong></h1>
        <div class="cont900" data-css='{"margin-bottom": "20px"}'>
            <div class="pane-shadow filter-form" id="filter-form">
                <div class="grid-row">
                    <div class="grid-col-100">
                        <div class="lb">Tìm kiếm</div>
                        <asp:DropDownList ID="ddltype" runat="server">
                            <asp:ListItem Value="0">Tất cả</asp:ListItem>
                            <asp:ListItem Value="1">Tiền đặt cọc đơn hàng</asp:ListItem>
                            <asp:ListItem Value="2">Nhận lại tiền cọc</asp:ListItem>
                            <asp:ListItem Value="3">Thanh toán hóa đơn</asp:ListItem>
                            <asp:ListItem Value="4">Nạp tiền</asp:ListItem>
                            <asp:ListItem Value="5">Rút tiền</asp:ListItem>
                            <asp:ListItem Value="6">Hủy Rút tiền</asp:ListItem>
                            <asp:ListItem Value="7">Tiền khiếu nại</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="grid-col-100">
                        <div class="lb">Từ ngày</div>
                        <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rFD" ShowPopupOnFocus="true" Width="100%" runat="server"
                            DateInput-CssClass="radPreventDecorate" placeholder="Từ ngày" CssClass="date" DateInput-EmptyMessage="Từ ngày">
                            <DateInput DisplayDateFormat="dd/MM/yyyy HH:mm" runat="server">
                            </DateInput>
                        </telerik:RadDateTimePicker>
                    </div>
                    <div class="grid-col-100">
                        <div class="lb">Đến ngày</div>
                        <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rTD" ShowPopupOnFocus="true" Width="100%" runat="server"
                            DateInput-CssClass="radPreventDecorate" placeholder="Đến ngày" CssClass="date" DateInput-EmptyMessage="Đến ngày">
                            <DateInput DisplayDateFormat="dd/MM/yyyy HH:mm" runat="server">
                            </DateInput>
                        </telerik:RadDateTimePicker>
                    </div>
                    <div class="grid-col-100 center-txt">
                        <asp:Button runat="server" ID="btnSearch" Text="Tìm" CssClass="btn primary-btn" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="table-rps table-responsive">
            <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                AllowSorting="True" AllowFilteringByColumn="false">
                <GroupingSettings CaseSensitive="false" />
                <MasterTableView CssClass="normal-table" DataKeyNames="ID">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%" FilterControlWidth="50px" AutoPostBackOnFilter="false"
                            CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="UserName" HeaderText="UserName" HeaderStyle-Width="15%"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="OrderCode" HeaderText="Order Code" HeaderStyle-Width="10%"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Số tiền" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <%--<p class=""><%#Eval("OrderPrice")!=null? string.Format("{0:N0}", Convert.ToDouble(Eval("OrderPrice"))).Replace(",","."):"0" %> </p>--%>
                                <p class=""><%#PJUtils.ReturnSymbol(Convert.ToInt32(Eval("Type"))) %><%# string.Format("{0:N0}",Eval("Amount")).Replace(",",".") %> </p>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="HContent" HeaderText="Nội dung" HeaderStyle-Width="10%"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Số dư" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <%--<p class=""><%#Eval("OrderPrice")!=null? string.Format("{0:N0}", Convert.ToDouble(Eval("OrderPrice"))).Replace(",","."):"0" %> </p>--%>
                                <p class=""><%# string.Format("{0:N0}",Eval("MoneyLeft")).Replace(",",".") %> </p>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Ngày tạo" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <p class=""><%# string.Format("{0:dd/MM/yyyy}", Eval("CreatedDate")) %></p>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                        PrevPageText="← Previous" />
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </main>
    <telerik:RadAjaxLoadingPanel ID="rxLoading" runat="server" Skin="">
        <div class="loading">
            <asp:Image ID="Image1" runat="server" ImageUrl="/App_Themes/NewUI/images/loading.gif" AlternateText="loading" />
        </div>
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="rAjax" runat="server">
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
    </telerik:RadAjaxManager>
</asp:Content>
