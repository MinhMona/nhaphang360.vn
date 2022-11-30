<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Report-Orders.aspx.cs" Inherits="NHST.Admin.Report_Orders" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainTit" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-white">
                    <div class="panel-heading">
                        <h3 class="panel-title semi-text text-uppercase">Báo cáo đơn hàng</h3>
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
                                            <span class="label-title">Tổng đơn hàng chưa cọc</span>
                                            <span class="label-infor">
                                                <asp:Label ID="lblNotDeposit" runat="server"></asp:Label></span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" style="margin-top: 20px;">
                                            <span class="label-title">Tổng đơn hàng đã cọc</span>
                                            <span class="label-infor">
                                                <asp:Label ID="lblOrderDeposit" runat="server"></asp:Label></span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" style="margin-top: 20px;">
                                            <span class="label-title">Tổng đơn hàng chờ duyệt đơn</span>
                                            <span class="label-infor">
                                                <asp:Label ID="lblWaiting" runat="server"></asp:Label></span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" style="margin-top: 20px;">
                                            <span class="label-title">Tổng đơn hàng đã duyệt đơn</span>
                                            <span class="label-infor">
                                                <asp:Label ID="lblOrderChecked" runat="server"></asp:Label></span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" style="margin-top: 20px;">
                                            <span class="label-title">Tổng đơn hàng đã đặt hàng</span>
                                            <span class="label-infor">
                                                <asp:Label ID="lblOrderIsOrder" runat="server"></asp:Label></span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" style="margin-top: 20px;">
                                            <span class="label-title">Tổng đơn hàng đã nhận tại TQ</span>
                                            <span class="label-infor">
                                                <asp:Label ID="lblOrderTQ" runat="server"></asp:Label></span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" style="margin-top: 20px;">
                                            <span class="label-title">Tổng đơn hàng đã nhận hàng tại VN</span>
                                            <span class="label-infor">
                                                <asp:Label ID="lblOrderVN" runat="server"></asp:Label></span>
                                        </div>
                                    </div>
                                     <div class="row">
                                        <div class="col-md-12" style="margin-top: 20px;">
                                            <span class="label-title">Tổng đơn hàng chờ thanh toán</span>
                                            <span class="label-infor">
                                                <asp:Label ID="lblOrderWaitingPayment" runat="server"></asp:Label></span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" style="margin-top: 20px;">
                                            <span class="label-title">Tổng đơn hàng thành công</span>
                                            <span class="label-infor">
                                                <asp:Label ID="lblOrderSuccess" runat="server"></asp:Label></span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" style="margin-top: 20px;">
                                            <span class="label-title">Tổng đơn hàng đã hủy</span>
                                            <span class="label-infor">
                                                <asp:Label ID="lblOrderCancel" runat="server"></asp:Label></span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12" style="margin-top: 20px;">
                                            <span class="label-title">Tổng số đơn hàng</span>
                                            <span class="label-infor">
                                                <strong>
                                                    <asp:Label ID="lblOrderTotal" runat="server"></asp:Label></strong></span>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12" style="margin-top: 50px;">
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
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
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
