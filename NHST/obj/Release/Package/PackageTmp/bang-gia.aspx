<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMaster.Master" AutoEventWireup="true" CodeBehind="bang-gia.aspx.cs" Inherits="NHST.bang_gia" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="all">
            <div class="main">
                <div class="sec form-sec">
                    <div class="sec-tt">
                        <h2 class="tt-txt">
                            <asp:Label ID="lblTitle" runat="server"></asp:Label></h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                        </p>
                    </div>
                    <div class="content-text">
                        <asp:Literal ID="ltr_content" runat="server"></asp:Literal>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
