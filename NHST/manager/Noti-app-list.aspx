<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="Noti-app-list.aspx.cs" Inherits="NHST.manager.Noti_app_list" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Danh sách thông báo app + mail</h4>
                    <div class="right-action">
                        <a type="button" class="btn primary-btn" href="/manager/push-noti-app.aspx">Thêm thông báo</a>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="list-staff col s12 section">
                <div class="list-table card-panel">
                    <div class="table responsive-table bordered highlight">
                        <table class="table responsive-table bordered highlight">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Tiêu đề</th>
                                    <th>Nội dung</th>
                                    <th>Người tạo</th>            
                                    <th>Ngày tạo</th>                                   
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                            </tbody>
                        </table>
                        <div class="pagi-table float-right mt-2">
                            <%this.DisplayHtmlStringPaging1();%>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--   <main id="main-wrap">
        <a type="button" class="btn primary-btn" href="/manager/push-noti-app.aspx">Thêm thông báo</a>
        <a type="button" class="btn primary-btn" href="/manager/push-noti-app-user.aspx">Thêm thông báo cá nhân</a>
        <h1 class="page-title">Danh sách thông báo app</h1>
        <div class="table-rps table-responsive">
            <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                AllowSorting="True" AllowFilteringByColumn="false">
                <GroupingSettings CaseSensitive="false" />
                <MasterTableView CssClass="normal-table" DataKeyNames="ID">
                    <Columns>
                        <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%" FilterControlWidth="50px" AutoPostBackOnFilter="false"
                            CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AppNotiTitle" HeaderText="Tiêu đề" HeaderStyle-Width="10%" FilterControlWidth="50px" AutoPostBackOnFilter="false"
                            CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="AppNotiMessage" HeaderText="Nội dung thông báo" HeaderStyle-Width="10%" FilterControlWidth="50px" AutoPostBackOnFilter="false"
                            CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        </telerik:GridBoundColumn>

                          <telerik:GridTemplateColumn HeaderText="Loại thông báo" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="CreatedDate" FilterControlWidth="100px" AutoPostBackOnFilter="false"
                            CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <%#Eval("Type").ToString() == "1"?"Cá nhân":"Tất cả" %>
                            </ItemTemplate>
                        </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="Username" HeaderText="Username" HeaderStyle-Width="10%" FilterControlWidth="50px" AutoPostBackOnFilter="false"
                            CurrentFilterFunction="EqualTo" FilterDelay="2000" ShowFilterIcon="false"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        </telerik:GridBoundColumn>

                        <telerik:GridTemplateColumn HeaderText="Ngày tạo" HeaderStyle-Width="10%"
                            HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                            SortExpression="CreatedDate" FilterControlWidth="100px" AutoPostBackOnFilter="false"
                            CurrentFilterFunction="Contains" FilterDelay="2000" ShowFilterIcon="false">
                            <ItemTemplate>
                                <%#Eval("CreatedDate","{0:dd/MM/yyyy HH:mm}") %>
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
    </telerik:RadAjaxManager>--%>
</asp:Content>
