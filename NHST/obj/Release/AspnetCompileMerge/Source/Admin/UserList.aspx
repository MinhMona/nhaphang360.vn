<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="NHST.Admin.UserList" %>

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
                    <h3 class="panel-title semi-text text-uppercase">Danh sách thành viên</h3>
                </div>
                <div class="panel-body">
                    <a type="button" class="btn btn-success m-b-sm" href="/Admin/AddUser.aspx">Thêm User</a>
                    <asp:Button runat="server" ID="btnExcel" Text="Xuất file Excel" CssClass="btn btn-success m-b-sm" OnClick="btnExcel_Click" />
                    <div class="table-responsive">
                        <asp:Panel runat="server" ID="p" DefaultButton="btnSearch">
                            <table class="table table-bordered">
                                <tr>
                                    <td>Tìm kiếm</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập Username để tìm"></asp:TextBox>
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
                                    <telerik:GridBoundColumn DataField="UserName" HeaderText="UserName" HeaderStyle-Width="15%"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Ho" HeaderText="Họ" HeaderStyle-Width="10%"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Ten" HeaderText="Tên" HeaderStyle-Width="10%"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Sodt" HeaderText="Số đt" HeaderStyle-Width="15%"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Status" HeaderText="Trạng thái" HeaderStyle-Width="10%"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Role" HeaderText="Quyền hạn" HeaderStyle-Width="15%"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Saler" HeaderText="Nhân viên kinh doanh" HeaderStyle-Width="15%"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CreatedDate" HeaderText="Ngày tạo" HeaderStyle-Width="15%"
                                        FilterControlWidth="100px" AutoPostBackOnFilter="false" CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderStyle-Width="20%" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                                        <ItemTemplate>
                                            <a class="btn btn-info btn-sm" href='/Admin/UserInfo.aspx?i=<%#Eval("ID") %>'>Sửa</a>
                                            <%#Eval("RoleID").ToString() == "1"?"<a class=\"btn btn-info btn-sm\" href=\"/Admin/UserWallet.aspx?i="+Eval("ID")+"\">Nạp tiền</a>":"" %>
                                            <%#Eval("RoleID").ToString() == "1"?"<a class=\"btn btn-info btn-sm\" href=\"/Admin/User-Transaction.aspx?i="+Eval("ID")+"\">Lịch sử giao dịch</a>":"" %>
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
