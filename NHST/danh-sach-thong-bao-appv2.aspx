<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/App.Master" CodeBehind="danh-sach-thong-bao-appv2.aspx.cs" Inherits="NHST.danh_sach_thong_bao_appv2" %>

<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <main id="main-wrap">
        <div class="container">
            <asp:Panel ID="pnMobile" runat="server" Visible="false">
                <div class="sec-block">
                    <div class="history-list">
                        <asp:Literal runat="server" ID="ltrHis"></asp:Literal>
                    </div>
                    <div class="pagination">
                        <ul class="pagi-list">
                            <%this.DisplayHtmlStringPaging1();%>
                        </ul>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnShowNoti" runat="server" Visible="false">
                <div class="page-body">
                    <div class="all donhang transport_list">
                        <h4 class="title_page logout">Bạn vui lòng đăng xuất và đăng nhập lại!</h4>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </main>
</asp:Content>
