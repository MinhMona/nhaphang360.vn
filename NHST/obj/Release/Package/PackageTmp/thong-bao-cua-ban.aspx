<%@ Page Language="C#" MasterPageFile="~/UserMasterNew.Master" AutoEventWireup="true" CodeBehind="thong-bao-cua-ban.aspx.cs" Inherits="NHST.thong_bao_cua_ban1" %>

<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="main">
        <div class="row">
            <div class="content-wrapper-before blue-grey lighten-5"></div>
            <div class="col s12">
                <div class="container">
                    <div class="all">
                        <div class="card-panel mt-3">
                            <div class="row">
                                <div class="col s12">
                                    <div class="page-title mt-2 center-align">
                                        <h4>Danh sách thông báo</h4>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="app-wrapper">
                            <div class="card card card-default border-radius-6">
                                <div class="card-content">
                                    <div class="app-search" style="display:none">
                                        <i class="material-icons mr-2 search-icon">search</i>
                                        <input type="text" placeholder="Tìm kiếm thông báo" class="app-filter" id="todo_filter">
                                    </div>
                                    <ul class="collection todo-collection">
                                      <asp:Literal ID="ltr" runat="server"></asp:Literal>
                                    </ul>
                                    <div class="pagi-table mt-2 center-align">
                                        <%this.DisplayHtmlStringPaging1();%>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
