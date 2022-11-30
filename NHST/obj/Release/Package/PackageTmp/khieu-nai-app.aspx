<%@ Page Title="Khiếu nại app" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="khieu-nai-app.aspx.cs" Inherits="NHST.khieu_nai_app" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <style>
        .ip-with-sufix .fcontrol {
            background-color: #fff;
        }

        .thanhtoanho-list {
            margin-bottom: 15px;
        }

        .page-title {
            text-align: center;
            padding: 10px 20px;
            font-size: 20px;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <main id="main-wrap">
        <div class="page-wrap">
            <asp:Panel ID="pnMobile" runat="server" Visible="false">
                <div class="page-body">
                    <div class="all">
                        <h1 class="page-title">KHIẾU NẠI</h1>
                    </div>
                    <asp:Literal runat="server" ID="ltrComplain"></asp:Literal>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnShowNoti" runat="server" Visible="false">
                <div class="page-body">
                    <div class="heading-search">
                        <h4 class="page-title">Bạn vui lòng đăng xuất và đăng nhập lại!</h4>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </main>
</asp:Content>
