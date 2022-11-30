<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="NHST.Admin.OrderList" %>

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
                    <h3 class="panel-title semi-text text-uppercase">Danh sách đơn hàng</h3>
                </div>
                <div class="panel-body">
                    <asp:Button runat="server" ID="btnExcel" Text="Xuất file Excel báo cáo" CssClass="btn btn-success m-b-sm" OnClick="btnExcel_Click" Visible="false"/>
                    <div class="table-responsive">
                        <asp:Panel runat="server" ID="p" DefaultButton="btnSearch">
                            <table class="table table-bordered">
                                <tr>
                                    <td>Tìm kiếm</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập tên sản phẩm hoặc mã vận đơn"></asp:TextBox>
                                    </td>
                                     <td>
                                        <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="1" Text="Tìm theo Tên sản phẩm"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="Tìm theo Mã vận đơn"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 50px">
                                        <asp:Button runat="server" ID="btnSearch" Text="Tìm" CssClass="btn btn-info" OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                    <div class="table-responsive">
                        <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                            AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                            AllowSorting="True"  AllowFilteringByColumn="True" >
                            <GroupingSettings CaseSensitive="false" />
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
                                            <a class="btn btn-info btn-sm" href='/Admin/OrderDetail.aspx?id=<%#Eval("ID") %>'>Xem</a>
                                            <a class="btn btn-info btn-sm" href='/Admin/Pay-Order.aspx?id=<%#Eval("ID") %>'>Thanh toán</a>
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
