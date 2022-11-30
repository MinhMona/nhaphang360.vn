<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Order-Transaction-Code.aspx.cs" Inherits="NHST.Admin.Order_Transaction_Code" %>

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
                    <h3 class="panel-title semi-text text-uppercase">Danh sách mã vận đơn</h3>
                </div>
                <div class="panel-body">
                    <a type="button" class="btn btn-success m-b-sm" href="/Admin/Add-smallpackage.aspx">Thêm mã vận đơn</a>
                    <div class="table-responsive">
                        <asp:Panel runat="server" ID="p" DefaultButton="btnSearch">
                            <table class="table table-bordered">
                                <tr>
                                    <td>Tìm kiếm</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập mã vận đơn để tìm"></asp:TextBox>
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
                                    <telerik:GridTemplateColumn HeaderText="Bao hàng" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="FirstName">
                                        <ItemTemplate>
                                            <%# BigPackageController.GetByID(Convert.ToInt32(Eval("BigPackageID"))) !=null?
                                                      BigPackageController.GetByID(Convert.ToInt32(Eval("BigPackageID"))).PackageCode:""   %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="OrderTransactionCode" HeaderText="Mã vận đơn" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ProductType" HeaderText="Loại hàng" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FeeShip" HeaderText="Phí ship(tệ)" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Weight" HeaderText="Cân (kg)" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Volume" HeaderText="Khối (m3)" HeaderStyle-Width="5%">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Trạng thái" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="FirstName">
                                        <ItemTemplate>
                                            <%# PJUtils.IntToStringStatusSmallPackage(Convert.ToInt32(Eval("Status"))) %>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Ngày tạo" HeaderStyle-Width="10%"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        SortExpression="CreatedDate">
                                        <ItemTemplate>
                                            <%#Eval("CreatedDate","{0:dd/MM/yyyy hh:mm}")%>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                                        <ItemTemplate>
                                            <a class="btn btn-info btn-sm" href='/Admin/SmallPackage-Detail.aspx?ID=<%#Eval("ID") %>'>Sửa</a>
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
            <telerik:AjaxSetting AjaxControlID="btnSave">
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
    </telerik:RadAjaxManager>

</asp:Content>
