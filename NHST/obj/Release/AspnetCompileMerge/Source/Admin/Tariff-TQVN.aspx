<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Tariff-TQVN.aspx.cs" Inherits="NHST.Admin.Tariff_TQVN" %>

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
                    <h3 class="panel-title semi-text text-uppercase">Danh sách Phí vận chuyển Trung Quốc - Việt Nam</h3>
                </div>
                <div class="panel-body">
                    <%--<a type="button" class="btn btn-success m-b-sm" href="/Admin/AddTafiffTQVN.aspx">Thêm Phí vận chuyển Trung Quốc - Việt Nam </a>--%>
                    <div class="table-responsive">
                        <asp:Panel runat="server" ID="p" DefaultButton="btnSearch">
                            <table class="table table-bordered">
                                <tr>
                                    <td>Tìm kiếm</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập kho để tìm"></asp:TextBox>
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
                            AllowSorting="True">
                            <MasterTableView CssClass="table table-bordered table-hover" DataKeyNames="ID">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ReceivePlace" HeaderText="Kho" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    
                                    <telerik:GridTemplateColumn HeaderText="Cân nặng từ" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="WeightFrom">
                                        <ItemTemplate>
                                            <%# Eval("WeightFrom","{0:0.##}") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Cân nặng đến" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="WeightTo">
                                        <ItemTemplate>
                                            <%# Eval("WeightTo","{0:0.##}") %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>                                    
                                    <telerik:GridTemplateColumn HeaderText="Phí" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="Amount">
                                        <ItemTemplate>
                                            <%# Eval("Amount","{0:N0}") %> VNĐ
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                                        <ItemTemplate>
                                            <a class="btn btn-info btn-sm" href='/Admin/Tariff-TQVN-Detail.aspx?ID=<%#Eval("ID") %>'>Sửa</a>
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
