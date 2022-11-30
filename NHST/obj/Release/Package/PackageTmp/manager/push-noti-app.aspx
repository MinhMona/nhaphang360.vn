<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="push-noti-app.aspx.cs" Inherits="NHST.manager.push_noti_app" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Thông báo</h4>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="list-staff col s12 m8 l4 xl4 section">
                <div class="rp-detail card-panel row">
                    <div class="col s12">
                        <div class="row pb-2 border-bottom-1 ">
                            <div class="input-field col s12">
                                <asp:TextBox runat="server" placerholder="" ID="txtTitle" type="text" class="validate"></asp:TextBox>
                                <label for="rp_username">Tiêu đề</label>
                            </div>

                            <div class="input-field col s12">
                                <asp:TextBox runat="server" ID="txtMessage" TextMode="MultiLine"
                                    class="materialize-textarea"></asp:TextBox>
                                <label for="rp_textarea">Nội dung</label>
                            </div>
                            <div class="row section mt-2">
                                <div class="col s12">
                                    <asp:Button ID="btnSave" runat="server" Text="Tạo mới" CssClass="btn" OnClick="btnSave_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
