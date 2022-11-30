<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="yeu-cau-giao.aspx.cs" Inherits="NHST.manager.yeu_cau_giao" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <asp:Button runat="server" ID="btnExcel" Text="Xuất file Excel báo cáo" CssClass="btn primary-btn" OnClick="btnExcel_Click" Visible="false" />
        <h1 class="page-title">Danh sách đơn hàng</h1>
        <div class="cont900" data-css='{"margin-bottom": "20px"}'>
            <div class="pane-shadow filter-form" id="filter-form">
                <div class="grid-row">
                    <div class="grid-col-100">
                        <div class="lb">Tìm kiếm</div>
                        <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập tên sản phẩm hoặc mã vận đơn"></asp:TextBox>
                    </div>
                    <div class="grid-col-100">
                        <div class="lb">Loại</div>
                        <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                            <asp:ListItem Value="1" Text="Tìm theo Tên sản phẩm"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Tìm theo Mã vận đơn"></asp:ListItem>
                        </asp:DropDownList>
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
                AllowSorting="True" AllowFilteringByColumn="True">
                <GroupingSettings CaseSensitive="false" />
                <MasterTableView CssClass="normal-table" DataKeyNames="ID">
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
                        <%-- <telerik:GridBoundColumn DataField="ShopName" HeaderText="Tên Shop" HeaderStyle-Width="15%"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>--%>
                        <telerik:GridTemplateColumn HeaderText="Tổng tiền" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <p class=""><%# string.Format("{0:N0}", Convert.ToDouble(Eval("TotalPriceVND"))) %> vnđ</p>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Tiền đã cọc" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("Deposit"))) %> vnđ</p>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="username" HeaderText="User đặt hàng" HeaderStyle-Width="15%"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="dathang" HeaderText="Nhân viên đặt hàng" HeaderStyle-Width="15%"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="kinhdoanh" HeaderText="Nhân viên kinh doanh" HeaderStyle-Width="15%"
                            FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridBoundColumn DataField="khotq" HeaderText="Nhân viên kho TQ" HeaderStyle-Width="15%"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="khovn" HeaderText="Nhân viên kho VN" HeaderStyle-Width="15%"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>--%>
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
                                <a class="btn primary-btn" href='/manager/OrderDetail.aspx?id=<%#Eval("ID") %>'>Xem</a>
                                <a class="btn primary-btn" href='/manager/Pay-Order.aspx?id=<%#Eval("ID") %>'>Thanh toán</a>

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
