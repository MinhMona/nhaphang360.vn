<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/manager/adminMasterNew.Master" CodeBehind="import_smallpackage_vn.aspx.cs" Inherits="NHST.manager.import_smallpackage_vn" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <main id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="col s12 page-title">
                <div class="card-panel">
                    <div class="title-flex">
                        <h4 class="title no-margin">Import Mã Vận Đơn Kho Việt Nam</h4>
                    </div>
                </div>
            </div>
            <div class="list-staff col s12 m12 l12 section">
                <div class="list-table card-panel">
                    <div class="row">
                        <div class="filter">
                            <div class="input-field col s6 m4 l2">
                                <div class="lb">Chọn file</div>
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </div>
                            <div class="input-field col s6 m4 l2">
                                <asp:Button ID="btnImport" runat="server" CssClass="btn primary-btn" Text="Import" OnClick="btnImport_Click" ></asp:Button>                                
                            </div>
                             <div class="input-field col s6 m4 l2">
                                 <asp:Button ID="btnExport" runat="server" CssClass="btn primary-btn" Text="Xuất file mẫu" OnClick="btnExport_Click" ></asp:Button>                             
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>        
    </main>
</asp:Content>