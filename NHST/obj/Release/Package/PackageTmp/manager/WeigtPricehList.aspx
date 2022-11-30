<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="WeigtPricehList.aspx.cs" Inherits="NHST.manager.WeigtPricehList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <a type="button" class="btn primary-btn" href="/manager/AddWeightPrice.aspx">Thêm mới</a>
        <h1 class="page-title">Danh sách giá cân nặng</h1>
        <div class="cont900" data-css='{"margin-bottom": "20px"}'>
            <div class="pane-shadow filter-form" id="filter-form">
                <div class="grid-row">
                    <div class="grid-col-50">
                        <div class="lb">Giá tiền ở Hà Nội</div>
                    </div>
                    <div class="grid-col-50">
                        <asp:Label ID="lblPriceHN" runat="server"></asp:Label>
                        VNĐ
                    </div>
                    <div class="grid-col-50">
                        <div class="lb">Giá tiền ở Việt Trì</div>
                    </div>
                    <div class="grid-col-50">
                        <asp:Label ID="lblPriceSG" runat="server"></asp:Label>
                        VNĐ
                    </div>
                </div>
            </div>
        </div>
        <div class="table-rps table-responsive">
            <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                AllowSorting="True">
                <MasterTableView CssClass="normal-table" DataKeyNames="ID">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="WeightFrom" HeaderText="Trọng lượng từ" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="WeightTo" HeaderText="Trọng lượng đến" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Khu vực">
                            <ItemTemplate>
                                <%#PJUtils.ReturnPlace(Convert.ToInt32(Eval("Place"))) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Loại">
                            <ItemTemplate>
                                <%#PJUtils.ReturnTypeFastSlow(Convert.ToInt32(Eval("TypeFastSlow"))) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Vip 1">
                            <ItemTemplate>
                                <%#Eval("Vip1","{0:N0}") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Vip 2">
                            <ItemTemplate>
                                <%#Eval("Vip2","{0:N0}") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Vip 3">
                            <ItemTemplate>
                                <%#Eval("Vip3","{0:N0}") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Vip 4">
                            <ItemTemplate>
                                <%#Eval("Vip4","{0:N0}") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Vip 5">
                            <ItemTemplate>
                                <%#Eval("Vip5","{0:N0}") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Vip 6">
                            <ItemTemplate>
                                <%#Eval("Vip6","{0:N0}") %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                            <ItemTemplate>
                                <a class="btn primary-btn" target="_blank" href='/manager/WeightPriceInfo.aspx?ID=<%#Eval("ID") %>'>Sửa</a>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
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
