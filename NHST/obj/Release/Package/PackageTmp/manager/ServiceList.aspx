<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="ServiceList.aspx.cs" Inherits="NHST.manager.ServiceList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <a type="button" class="btn primary-btn" href="/manager/Add-Service.aspx">Thêm Trang</a>
        <h1 class="page-title">Danh sách dịch vụ</h1>
<%--        <div class="cont900" data-css='{"margin-bottom": "20px"}'>
            <div class="pane-shadow filter-form" id="filter-form">
                <div class="grid-row">
                    <div class="grid-col-100">
                        <div class="lb">Tìm kiếm</div>
                        <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập tiêu đề để tìm"></asp:TextBox>
                    </div>
                    <div class="grid-col-100 center-txt">
                        <asp:Button runat="server" ID="btnSearch" Text="Tìm" CssClass="btn primary-btn" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
        </div>--%>
        <div class="table-rps table-responsive">
            <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                AllowSorting="True">
                <MasterTableView CssClass="normal-table" DataKeyNames="ID">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ServiceName" HeaderText="Tên dịch vụ" HeaderStyle-Width="10%">
                        </telerik:GridBoundColumn>
                           <telerik:GridTemplateColumn HeaderText="Hình ảnh" HeaderStyle-Width="5%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="IsHidden">
                            <ItemTemplate>
                                <%# PJUtils.ShowIMG(Eval("ServiceIMG").ToString()) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderText="Trạng thái" HeaderStyle-Width="5%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="IsHidden">
                            <ItemTemplate>
                                <%# PJUtils.BoolToStatusShow(Eval("IsHidden").ToString()) %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                  
                        <telerik:GridTemplateColumn HeaderText="Ngày tạo" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="CreatedDate">
                            <ItemTemplate>
                                <%#Eval("CreatedDate","{0:dd/MM/yyyy hh:mm}")%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                            <ItemTemplate>
                                <a class="btn primary-btn" href='/manager/Edit-Service.aspx?i=<%#Eval("ID") %>'>Sửa</a>
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
