<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="EditMenu.aspx.cs" Inherits="NHST.manager.EditMenu" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Chi tiết menu</h4>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="list-staff col s12 m8 l4 xl4 section">
                <div class="rp-detail card-panel row">
                    <div class="col s12">
                        <div class="row pb-2 border-bottom-1 ">
                            <div class="input-field col s12">
                                <asp:TextBox runat="server" ID="txtTitle" type="text" class="validate"></asp:TextBox>
                                <label for="rp_vnd">
                                    Tên menu <span class="require">(*)</span>
                                    <asp:RequiredFieldValidator runat="server" ID="rq" ControlToValidate="txtTitle"
                                        ValidationGroup="n" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator></label>
                            </div>

                            <div class="input-field col s12">
                                <asp:TextBox runat="server" ID="txtLinkMenu" type="text" class="validate"></asp:TextBox>
                                <label for="rp_vnd">
                                    Link menu
                                </label>
                            </div>
                            <div class="input-field col s12" style="display:none">
                                <asp:TextBox runat="server" ID="pPosition" type="text" class="validate"></asp:TextBox>
                                <label for="rp_vnd">
                                    Vị trí
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="pPosition" SetFocusOnError="true"
                                        ValidationGroup="n" ErrorMessage="(*)" ForeColor="Red"></asp:RequiredFieldValidator></label>
                            </div>

                            <div class="input-field col s12">
                                <div class="switch status-func">
                                    <span class="mr-2">Trạng thái: </span>
                                    <label>
                                        Ẩn
                                          <asp:TextBox runat="server" onclick="CheckHidden()" ID="txtIsHidden" type="checkbox"></asp:TextBox>
                                        <span class="lever"></span>
                                    </label>
                                </div>
                            </div>
                            <asp:HiddenField runat="server" Value="0" ID="hdfIsHidden" />

                            <div class="input-field col s12">
                                <div class="switch status-func">
                                    <span class="mr-2">Mở trang mới: </span>
                                    <label>
                                        <asp:TextBox runat="server" onclick="CheckTarget()" ID="txtTarget" type="checkbox"></asp:TextBox>
                                        <span class="lever"></span>
                                    </label>
                                </div>
                            </div>
                            <asp:HiddenField runat="server" Value="0" ID="hdfTarget" />
                        </div>
                        <div class="row section mt-2">
                            <div class="col s12">
                                <asp:Button ID="btnSave" runat="server" Text="Cập nhật" CssClass="btn" OnClick="btnSave_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        $(document).ready(function () {
            M.textareaAutoResize($('#rp_textarea'));
        });


        function myFunction() {
            var a = $('#<%=hdfIsHidden.ClientID%>').val();
            if (a == 0) {
                $('#<%=txtIsHidden.ClientID%>').prop('checked', true);
            }
            else {
                $('#<%=hdfIsHidden.ClientID%>').prop('checked', false);

            }

            var a = $('#<%=hdfTarget.ClientID%>').val();
            if (a == 0) {
                $('#<%=hdfTarget.ClientID%>').prop('checked', true);
            }
            else {
                $('#<%=hdfTarget.ClientID%>').prop('checked', false);
            }
        }

        $(window).load(function () {
            myFunction();
        });


        function CheckHidden() {
            var a = $('#<%=hdfIsHidden.ClientID%>').val();
            if (a == '0') {
                $('#<%=hdfIsHidden.ClientID%>').val('1');
            }
            else {
                $('#<%=hdfIsHidden.ClientID%>').val('0');
            }
            console.log($('#<%=hdfIsHidden.ClientID%>').val());
        }

        function CheckTarget() {
            var a = $('#<%=hdfTarget.ClientID%>').val();
            if (a == '0') {
                $('#<%=hdfTarget.ClientID%>').val('1');
            }
            else {
                $('#<%=hdfTarget.ClientID%>').val('0');
            }
            console.log($('#<%=hdfTarget.ClientID%>').val());
        }

    </script>
</asp:Content>
