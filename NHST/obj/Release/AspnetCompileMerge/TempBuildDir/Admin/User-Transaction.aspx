<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="User-Transaction.aspx.cs" Inherits="NHST.Admin.User_Transaction" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainTit" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-white">
                <div class="panel-heading">
                    <h3 class="panel-title semi-text text-uppercase">Lịch sử nạp tiền, rút tiền của khách</h3>
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group marbot1">
                                        Username
                                    </div>
                                    <div class="form-group marbot2">
                                        <strong><asp:Label ID="lblUsername" runat="server"></asp:Label></strong>
                                    </div>
                                    <div class="form-group marbot1">
                                        Số dư
                                    </div>
                                    <div class="form-group marbot2">
                                        <strong><asp:Label ID="lblWallet" runat="server"></asp:Label></strong>
                                    </div>
                                </div>
                            </div>
                        </div>
                </div>
                <div class="table-responsive">
                    <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                        AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                        AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                        AllowSorting="True" AllowFilteringByColumn="false">
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView CssClass="table table-bordered table-hover" DataKeyNames="ID">
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Ngày giờ" HeaderStyle-Width="10%"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    SortExpression="CreatedDate" FilterControlWidth="100px" AutoPostBackOnFilter="false"
                                    CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <%#Eval("CreatedDate","{0:dd/MM/yyyy HH:mm}") %>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="HContent" HeaderText="Nội dung" HeaderStyle-Width="5%" FilterControlWidth="50px" AutoPostBackOnFilter="false"
                                    CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Số tiền" HeaderStyle-Width="10%"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <p class=""><%# Eval("Type").ToString() == "1"?"-":"+"%> <%# string.Format("{0:N0}", Convert.ToDouble(Eval("Amount"))) %> vnđ</p>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Loại giao dịch" HeaderStyle-Width="10%"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <p class=""><%# PJUtils.GetTradeType(Convert.ToInt32(Eval("TradeType"))) %></p>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Số dư" HeaderStyle-Width="10%"
                                    HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    <ItemTemplate>
                                        <p class=""><%# string.Format("{0:N0}", Convert.ToDouble(Eval("moneyleft"))) %> vnđ</p>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                                PrevPageText="← Previous" />
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
        </div>
        <!-- Modal -->
    </div>
    </div>
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
