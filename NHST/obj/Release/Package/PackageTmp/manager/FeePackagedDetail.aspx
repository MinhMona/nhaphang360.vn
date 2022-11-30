<%@ Page Language="C#" Title="Phí đóng gỗ" AutoEventWireup="true" MasterPageFile="~/manager/adminMasterNew.Master" CodeBehind="FeePackagedDetail.aspx.cs" Inherits="NHST.manager.FeePackagedDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Phí kiểm đếm</h4>
                    <%--  <div class="right-action">
                        <a href="#addStaff" class="btn  modal-trigger waves-effect">Thêm phí kiểm đêm</a>
                    </div>--%>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="list-staff col s6 section">
                <div class="list-table card-panel">
                    <div class="row">
                        <div class="input-field col s12 m6">
                            <asp:TextBox runat="server" ID="txtFirstKG" type="text" placeholder="" data-type="currency"></asp:TextBox>
                            <label for="addAmountFrom">Kg đầu tiên</label>
                        </div>
                        <div class="input-field col s12 m6">
                            <asp:TextBox runat="server" ID="txtNextKG" type="text" placeholder="" data-type="currency"></asp:TextBox>
                            <label for="addAmountTo">Kg tiếp theo</label>
                        </div>
                         <div class="modal-ft">
                            <div class="ft-wrap center-align">
                                <a onclick="Update()" class="modal-action btn modal-close waves-effect waves-green mr-2">Cập nhật</a>                           
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Button runat="server" ID="btnUpdate" UseSubmitBehavior="false" OnClick="btnUpdate_Click" style="display:none"/>
    <script>
        function Update() {
            $('#<%=btnUpdate.ClientID%>').click();
        }
    </script>
</asp:Content>