<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Report-Income.aspx.cs" Inherits="NHST.Admin.Report_Income" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainTit" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">
    <%--<asp:Panel runat="server" ID="Parent">--%>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-white">
                <div class="panel-heading">
                    <h3 class="panel-title semi-text text-uppercase">Báo cáo doanh thu</h3>
                </div>
                <div class="panel-body">
                    <div class="row m-b-lg">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-5">
                                    <label for="exampleInputEmail">
                                        Từ ngày
                                    </label>
                                    <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rdatefrom" ShowPopupOnFocus="true" Width="100%" runat="server"
                                        DateInput-CssClass="radPreventDecorate">
                                        <TimeView TimeFormat="HH:mm" runat="server">
                                        </TimeView>
                                        <DateInput DisplayDateFormat="dd/MM/yyyy HH:mm" runat="server">
                                        </DateInput>
                                    </telerik:RadDateTimePicker>
                                </div>
                                <div class="col-md-5">
                                    <label for="exampleInputEmail">
                                        Đến ngày
                                    </label>
                                    <telerik:RadDateTimePicker RenderMode="Lightweight" ID="rdateto" ShowPopupOnFocus="true" Width="100%" runat="server"
                                        DateInput-CssClass="radPreventDecorate">
                                        <TimeView TimeFormat="HH:mm" runat="server">
                                        </TimeView>
                                        <DateInput DisplayDateFormat="dd/MM/yyyy HH:mm" runat="server">
                                        </DateInput>
                                    </telerik:RadDateTimePicker>
                                </div>

                                <div class="col-md-2">
                                    <asp:Button ID="btnFilter" runat="server" CssClass="btn btn-success" Text="Xem" OnClick="btnFilter_Click" Style="margin-top: 24px;"></asp:Button>
                                </div>
                            </div>
                        </div>
                        <style>
                                
                            </style>
                        <asp:Panel ID="pninfo" runat="server" Visible="false">
                            <div class="col-md-12" style="margin-top: 20px;">

                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 20px;">
                                        <span class="label-title">Tổng tiền cọc</span>
                                        <span class="label-infor">
                                            <asp:Label ID="lblDeposit" runat="server"></asp:Label></span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 20px;">
                                        <span class="label-title">Tổng tiền chưa thanh toán</span>
                                        <span class="label-infor">
                                            <asp:Label ID="lblNotPay" runat="server"></asp:Label></span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 20px;">
                                        <span class="label-title">Tổng tiền đơn hàng hoả tốc</span>
                                        <span class="label-infor">
                                            <asp:Label ID="lblOrderFast" runat="server"></asp:Label></span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 20px;">
                                        <span class="label-title">Tổng tiền ship Trung Quốc</span>
                                        <span class="label-infor">
                                            <asp:Label ID="lblFeeShipCN" runat="server"></asp:Label></span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 20px;">
                                        <span class="label-title">Tổng tiền phí mua hàng</span>
                                        <span class="label-infor">
                                            <asp:Label ID="lblFeeBuyPro" runat="server"></asp:Label></span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 20px;">
                                        <span class="label-title">Tổng tiền phí cân nặng</span>
                                        <span class="label-infor">
                                            <asp:Label ID="lblFeeWeight" runat="server"></asp:Label></span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 20px;">
                                        <span class="label-title">Tổng tiền phí kiểm đếm</span>
                                        <span class="label-infor">
                                            <asp:Label ID="lblIsCheckProductPrice" runat="server"></asp:Label></span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 20px;">
                                        <span class="label-title">Tổng tiền phí đóng gỗ</span>
                                        <span class="label-infor">
                                            <asp:Label ID="lblIsPackedPrice" runat="server"></asp:Label></span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 20px;">
                                        <span class="label-title">Tổng tiền phí ship giao tận nhà</span>
                                        <span class="label-infor">
                                            <asp:Label ID="lblIsFastDeliveryPrice" runat="server"></asp:Label></span>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 20px;">
                                        <span class="label-title">Tổng kết số tiền</span>
                                        <span class="label-infor">
                                            <strong>
                                                <asp:Label ID="lblTotal" runat="server"></asp:Label></strong></span>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 50px;">
                                <div class="row">
                                    <h1>Thống kê thanh toán</h1>
                                </div>
                                <div class="row">
                                    <telerik:RadGrid runat="server" ID="RadGrid1" OnNeedDataSource="RadGrid1_NeedDataSource" AutoGenerateColumns="False"
                                        AllowPaging="True" PageSize="10" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                                        AllowAutomaticUpdates="True" OnItemCommand="RadGrid1_ItemCommand" OnPageIndexChanged="RadGrid1_PageIndexChanged"
                                        AllowSorting="True" OnPageSizeChanged="RadGrid1_PageSizeChanged">
                                        <MasterTableView CssClass="table table-bordered table-hover" DataKeyNames="MainOrderID">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="MainOrderID" HeaderText="Mã đơn hàng" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Username" HeaderText="Username" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Status" HeaderText="Loại thanh toán" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                 <telerik:GridBoundColumn DataField="Amount" HeaderText="Số tiền" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CreatedDate" HeaderText="Ngày tạo" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CreatedBy" HeaderText="Người tạo" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                                                PrevPageText="← Previous" />
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 50px;">
                                <div class="row">
                                    <h1>Thống kê đơn hàng</h1>
                                </div>
                                <div class="row">
                                    <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                                        AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                                        AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                                        AllowSorting="True" OnPageSizeChanged="gr_PageSizeChanged">
                                        <MasterTableView CssClass="table table-bordered table-hover" DataKeyNames="OrderID">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="OrderID" HeaderText="Mã đơn hàng" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ShopID" HeaderText="ShopID" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ShopName" HeaderText="Tên shop" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FullName" HeaderText="Họ tên" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ShipCN" HeaderText="Phí ship TQ" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="BuyPro" HeaderText="Phí mua hàng" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FeeWeight" HeaderText="Phí cân nặng" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="ShipHome" HeaderText="Phí Giao tận nhà" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CheckProduct" HeaderText="Phí kiểm kê" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Package" HeaderText="Phí đóng gói" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="IsFast" HeaderText="Phí hỏa tốc" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Total" HeaderText="Tổng tiền" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Deposit" HeaderText="Đặt cọc" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PayLeft" HeaderText="Còn lại" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Status" HeaderText="Trạng thái" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CreatedDate" HeaderText="Ngày tạo" HeaderStyle-Width="5%">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                                                PrevPageText="← Previous" />
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                    <%--<div class="form-group col-md-12">
                                            <table class="pricingtable">
                                                <tr>
                                                    <th>Mã đơn hàng
                                                    </th>
                                                    <th>ShopID
                                                    </th>
                                                    <th>ShopName
                                                    </th>
                                                    <th>Họ tên
                                                    </th>
                                                    <th>Email
                                                    </th>
                                                    <th>Phone
                                                    </th>
                                                    <th>Phí chuyển tận nhà
                                                    </th>
                                                    <th>Phí kiểm kê
                                                    </th>
                                                    <th>Phí đóng gói
                                                    </th>
                                                    <th>Phí hỏa tốc
                                                    </th>
                                                    <th>Tổng tiền
                                                    </th>
                                                    <th>Đặt cọc
                                                    </th>
                                                    <th>Còn lại
                                                    </th>
                                                    <th>Trạng thái
                                                    </th>
                                                </tr>
                                                <asp:Literal ID="ltrHistory" runat="server"></asp:Literal>
                                            </table>
                                        </div>--%>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%-- </asp:Panel>--%>
    <%-- <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilter">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>--%>
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            function OnDateSelected(sender, eventArgs) {
                var date1 = sender.get_selectedDate();
                date1.setDate(date1.getDate() + 31);
                var datepicker = $find("<%= rdateto.ClientID %>");
                datepicker.set_maxDate(date1);
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
