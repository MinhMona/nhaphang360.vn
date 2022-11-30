<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="MenuList.aspx.cs" Inherits="NHST.manager.MenuList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">        
        <h1 class="page-title">Danh sách Hỗ trợ</h1>
        <div class="cont900" data-css='{"margin-bottom": "20px"}'>
            <div class="pane-shadow filter-form" id="filter-form">
                <div class="grid-row">
                    <div class="grid-col-100">
                        <div class="lb">Tìm kiếm</div>
                        <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập tên để tìm"></asp:TextBox>
                    </div>
                    <div class="grid-col-100 center-txt">
                        <asp:Button runat="server" ID="btnSearch" Text="Tìm" CssClass="btn btn-info" OnClick="btnSearch_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="table-rps table-responsive">
            <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                AllowSorting="True">
                <MasterTableView CssClass="table table-bordered table-hover" DataKeyNames="ID">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MenuName" HeaderText="Tên" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MenuLink" HeaderText="Link" HeaderStyle-Width="5%">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Ẩn" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="IsHidden">
                            <ItemTemplate>
                                <%#PJUtils.ReturnHidden(Convert.ToBoolean(Eval("IsHidden")))%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>                       
                        <telerik:GridTemplateColumn HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center"
                            ItemStyle-HorizontalAlign="Center" AllowFiltering="False" HeaderText="Thao tác">
                            <ItemTemplate>
                                <a class="btn primary-btn" href='/manager/EditMenu.aspx?i=<%#Eval("ID") %>'>Sửa</a>
                                <%--<a class="btn btn-info btn-sm bg-red" href="javascript:;" onclick="DeleteMenu('<%#Eval("ID") %>')">Xóa</a>--%>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                        PrevPageText="← Previous" />
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </main>
    
    <asp:HiddenField ID="hdfID" runat="server" />
    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" Style="display: none" />
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
            <telerik:AjaxSetting AjaxControlID="btnDelete">
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
    <telerik:RadScriptBlock ID="sc" runat="server">
        <script type="text/javascript">
            function DeleteSocialSupport(ID) {
                var r = confirm('Bạn muốn xóa menu này?');
                if (r == true) {
                    $("#<%= hdfID.ClientID%>").val(ID);
                    $("#<%= btnDelete.ClientID%>").click();
                }
                else {

                }
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
