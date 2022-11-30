<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="History-Pay-Order.aspx.cs" Inherits="NHST.manager.History_Pay_Order" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <h1 class="page-title">Lịch sử thanh toán đơn hàng</h1>        
        <div class="table-rps table-responsive">
            <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                AllowSorting="True">
                <MasterTableView CssClass="normal-table" DataKeyNames="ID">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="MainOrderID" HeaderText="Mã đơn hàng" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Số tiền" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="Status">
                            <ItemTemplate>
                                <%#Eval("Amount","{0:N0}") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Username" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="Status">
                            <ItemTemplate>
                                <%# AccountController.GetByID(Convert.ToInt32(Eval("UID"))) != null ?
                                                    AccountController.GetByID(Convert.ToInt32(Eval("UID"))).Username
                                                    :"-" %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Note" HeaderText="Nội dung" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>

                        <telerik:GridTemplateColumn HeaderText="Loại giao dịch" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="Status">
                            <ItemTemplate>
                                <%#Eval("Type").ToString() == "2"?"Đặt cọc":"Thanh toán" %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Ngày tạo" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="CreatedDate">
                            <ItemTemplate>
                                <%#Eval("CreatedDate","{0:dd/MM/yyyy hh:mm}")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="CreatedBy" HeaderText="Người tạo" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>
                        <%-- <telerik:GridTemplateColumn HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                                        <ItemTemplate>
                                            <a class="btn btn-info btn-sm" href='/Admin/HistorySendWalletDetail.aspx?i=<%#Eval("ID") %>'>Sửa</a>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>--%>
                    </Columns>
                    <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                        PrevPageText="← Previous" />
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </main>
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
