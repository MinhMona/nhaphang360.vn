<%@ Page Title="Danh sách chuyên mục" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="Page-Type-List.aspx.cs" Inherits="NHST.manager.Page_Type_List" %>

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
                    <h4 class="title no-margin" style="display: inline-block;">Danh sách chuyên mục</h4>
                    <div class="right-action">
                        <a href="/manager/AddPageType.aspx" target="_blank" class="btn modal-trigger waves-effect">Thêm chuyên mục</a>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="list-staff col s12 section">
                <div class="list-table card-panel">
                    <div class="filter">
                        <div class="search-name input-field">
                            <asp:TextBox ID="search_name" type="text" placeholder="" onkeypress="myFunction()" runat="server" />
                            <label for="search_name"><span>Tên chuyên mục</span></label>
                            <span class="material-icons search-action">search</span>
                            <asp:Button Style="display: none" UseSubmitBehavior="false" ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
                        </div>
                        <div class="clearfix"></div>
                    </div>
                    <table class="table responsive-table bordered highlight">
                        <thead>
                            <tr>
                                <th>ID</th>
                                <th>Tên chuyên mục</th>
                                <th>Lần cuối thay đổi</th>
                                <th>Thao tác</th>
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
    <!-- END: Page Main-->

    <!-- BEGIN: Modal Add NV -->
    <!-- Modal Structure -->
    <div id="addStaff" class="modal">
        <div class="modal-hd">
            <span class="right"><i class="material-icons modal-close right-align">clear</i></span>
            <h4 class="no-margin center-align">Thêm chuyên mục</h4>
        </div>
        <div class="modal-bd">
            <div class="row">
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" placeholder="" ID="txtName" type="text" class="validate" data-type="text-only"></asp:TextBox>
                    <label for="add_category">Tên chuyên mục</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" placeholder="" ID="txtDetail" type="text" class="validate" data-type="text-only"></asp:TextBox>
                    <label for="add_description">Mô tả</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" placeholder="" ID="txtOGTitle" type="text" class="validate" data-type="text-only"></asp:TextBox>
                    <label for="add_og-title">OG Title</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txtOGDescription" placeholder="" type="text" class="validate" data-type="text-only"></asp:TextBox>
                    <label for="add_og-description">OG Description</label>
                </div>
                <div class="input-field file-field col s12 m6">
                    <span class="black-text">Hình ảnh</span>
                    <div style="display: inline-block; margin-left: 15px;">
                        <asp:FileUpload runat="server" ID="OGIMG" class="upload-img" type="file" onchange="previewFiles(this);" title=""></asp:FileUpload>
                        <button class="btn-upload">Upload</button>
                    </div>
                    <div class="preview-img">
                    </div>
                </div>
                <%--  <div class="input-field file-field col s12 m6">
                    <asp:FileUpload runat="server" type="file" id="OGIMG"/>
                    <label for="add_og-image" class="active">OG Image</label>
                    <div class="file-path-wrapper">
                        <input class="file-path validate" type="text">
                    </div>
                </div>--%>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txtMetaTitle" placeholder="" type="text" class="validate" data-type="text-only"></asp:TextBox>
                    <label for="add_meta-title">Meta Title</label>
                </div>
                <div class="input-field col s12 m12">
                    <asp:TextBox runat="server" ID="txtMetaDescription" placeholder="" type="text" class="validate" data-type="text-only"></asp:TextBox>
                    <label for="add_meta-description">Meta Description</label>
                </div>
                <div class="input-field col s12 m12">
                    <asp:TextBox runat="server" placeholder="Cách nhau bởi dấu phẩy VD : muahang,trungquoc,vietnam" ID="txtMetaKeyWord"
                        type="text" class="validate" data-type="text-only"></asp:TextBox>
                    <label for="add_meta-keyword">Meta Keyword</label>
                </div>
                <div class="input-field col s12 m12">
                    <div class="switch status-func">
                        <span class="mr-2">Sidebar: </span>
                        <label>
                            <asp:TextBox runat="server" onclick="CheckSidebar()" ID="txtStatus" type="checkbox"></asp:TextBox>
                            <span class="lever"></span>
                        </label>
                    </div>
                </div>
                <asp:HiddenField runat="server" Value="0" ID="hdfSidebar" />
            </div>
        </div>
        <div class="modal-ft">
            <div class="ft-wrap center-align">
                <a class="btn white-text" onclick="updateFunction()">Thêm</a>
                <%--<asp:Button Style="display: none" runat="server" ID="btnAdd" OnClick="btnAdd_Click" class="modal-action btn modal-close white-text waves-green mr-2" Text="Thêm"></asp:Button>--%>
                <a class="modal-action btn orange darken-2 modal-close waves-effect waves-green ml-2" onclick="cancelFunction()">Hủy</a>
            </div>
        </div>
    </div>
    <script>
        <%--function updateFunction() {
            var a = $('#<%=txtName.ClientID%>').val().trim();
             if (a == "" || a == null) {
                 alert('Vui lòng nhập tên chuyên mục!');
             }
             else {
                 $('#<%=btnAdd.ClientID%>').click();
            }
        }--%>
        function cancelFunction() {
            $('#<%=txtName.ClientID%>').val('');
             $('#<%=txtDetail.ClientID%>').val('');
             $('#<%=txtOGTitle.ClientID%>').val('');
             $('#<%=txtMetaDescription.ClientID%>').val('');
             $('#<%=txtOGDescription.ClientID%>').val('');
             $('#<%=txtMetaTitle.ClientID%>').val('');
             $('#<%=txtMetaKeyWord.ClientID%>').val('');
        }
        function myFunction() {
            if (event.which == 13 || event.keyCode == 13) {
                console.log($('#<%=search_name.ClientID%>').val());
                $('#<%=btnSearch.ClientID%>').click();
            }
        }
        $('.search-action').click(function () {
            console.log('dkm ngon');
            console.log($('#<%=search_name.ClientID%>').val());
            $('#<%=btnSearch.ClientID%>').click();
        })

          function CheckSidebar() {
            var a = $('#<%=hdfSidebar.ClientID%>').val();
            if (a == '0') {

                $('#<%=hdfSidebar.ClientID%>').val('1');
            }
            else {

                $('#<%=hdfSidebar.ClientID%>').val('0');
            }
            console.log($('#<%=hdfSidebar.ClientID%>').val());
        }

    </script>
</asp:Content>
