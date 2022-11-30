<%@ Page Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="BankList.aspx.cs" Inherits="NHST.manager.BankList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <style>
        .rgSortAsc, .rgSortDesc {
            display: none;
        }

        .ab {
            height: 80px;
            width: 35%;
        }

        .ruInputs {
            padding-top: 10px;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Danh sách ngân hàng</h4>
                    <div class="right-action">
                        <a href="#addStaff" class="btn modal-trigger waves-effect">Thêm ngân hàng</a>
                    </div>

                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="list-staff col s12 section">
                <div class="list-table card-panel">
                    <%-- <table class="table responsive-table  highlight">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Tên chuyên mục</th>
                            <th>Mô tả</th>
                            <th>Lần cuối thay đổi</th>
                            <th>Thao tác</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>11808</td>
                            <td>Bảng giá</td>
                            <td>
                                <p>Hiển thị thông tin giá cả</p>
                            </td>
                            <td>17-04-2019 04:45</td>
                            <td>
                                <div class="action-table">
                                    <a href="chuyenmucbaiviet-edit.php" class="tooltipped" data-position="top"
                                        data-tooltip="Cập nhật"><i class="material-icons">edit</i></a>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>11808</td>
                            <td>Hướng dẫn</td>
                            <td>Các bài hướng dẫn</td>
                            <td>17-04-2019 04:45</td>
                            <td>
                                <div class="action-table">
                                    <a href="chuyenmucbaiviet-edit.php" class="tooltipped" data-position="top"
                                        data-tooltip="Cập nhật"><i class="material-icons">edit</i></a>
                                </div>
                            </td>
                        </tr>

                    </tbody>
                </table>
                <div class="pagi-table float-right mt-2">
                    <a href="#" class="prev-page pagi-button">Prev</a>
                    <span>
                        <a class="pagi-button current-active">1</a>
                        <a class="pagi-button">2</a>
                        <a class="pagi-button">3</a>
                        <a class="pagi-button">4</a>
                    </span>
                    <a href="#" class="next-page pagi-button">Next</a>
                </div>--%>

                    <telerik:RadGrid runat="server" ID="gr" OnNeedDataSource="r_NeedDataSource" AutoGenerateColumns="False"
                        AllowPaging="True" PageSize="20" EnableEmbeddedSkins="False" EnableEmbeddedBaseStylesheet="False"
                        AllowAutomaticUpdates="True" OnItemCommand="r_ItemCommand" OnPageIndexChanged="gr_PageIndexChanged"
                        AllowSorting="True">
                        <MasterTableView CssClass="table bordered centered highlight" DataKeyNames="ID">
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" HeaderStyle-Width="5%">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="BankName" HeaderText="Tên ngân hàng" HeaderStyle-Width="5%">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AccountHolder" HeaderText="Chủ tài khoản" HeaderStyle-Width="5%">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="BankNumber" HeaderText="Số tài khoản" HeaderStyle-Width="5%">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Branch" HeaderText="Chi nhánh" HeaderStyle-Width="5%">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Lần cuối thay đổi" HeaderStyle-Width="10%"
                                    SortExpression="CreatedDate">
                                    <ItemTemplate>
                                        <%#Eval("ModifiedDate","{0:dd/MM/yyyy hh:mm}")%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderStyle-Width="10%" AllowFiltering="False" HeaderText="Thao tác">
                                    <ItemTemplate>
                                        <div class="action-table">
                                            <a href="#EditBank" onclick="EditBank(<%#Eval("ID")%>)" class="btn modal-trigger waves-effect tooltipped" data-position="top"
                                                data-tooltip="Sửa"><i class="material-icons">edit</i></a>
                                            <a href="javascript:;" onclick="Delete(<%#Eval("ID")%>)" class="tooltipped" data-position="top"
                                                data-tooltip="Sửa"><i class="material-icons">delete</i></a>
                                        </div>
                                        <%--<a class="btn primary-btn" href='/manager/Edit-Paget-Type.aspx?i=<%#Eval("ID") %>'>Sửa</a>--%>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <PagerStyle ShowPagerText="False" Mode="NextPrevAndNumeric" NextPageText="Next →"
                                PrevPageText="← Previous" />
                        </MasterTableView>
                    </telerik:RadGrid>

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
            <h4 class="no-margin center-align">Thêm ngân hàng</h4>
        </div>
        <div class="modal-bd">
            <div class="row">
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txtBankName"></asp:TextBox>
                    <label>Tên ngân hàng</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txtAccountHolder"></asp:TextBox>
                    <label>Chủ tài khoản</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txtBankNumber"></asp:TextBox>
                    <label>Số tài khoản</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txtBranch"></asp:TextBox>
                    <label>Chi nhánh</label>
                </div>
                <div class="input-field file-field col s12 m6">
                    <span class="black-text">Hình ảnh</span>
                    <div style="display: inline-block; margin-left: 15px;">
                        <asp:FileUpload runat="server" ID="BankIMG" class="upload-img" type="file" onchange="previewFiles(this);" title=""></asp:FileUpload>
                        <button class="btn-upload">Upload</button>
                    </div>
                    <div class="preview-img-avatar">                                  

                                </div>
                </div>
            </div>
        </div>
        <div class="modal-ft">
            <div class="ft-wrap center-align">
                <asp:Button runat="server" ID="btnCreate" class="modal-action btn waves-effect waves-green mr-2" Text="Thêm" OnClick="btnCreate_Click" />
                <%--  <a href="javascript:;" onclick="Create()" class="modal-action btn modal-close waves-effect waves-green mr-2">Thêm</a>--%>
                <a href="javascript:;" class="modal-action btn orange darken-2 modal-close waves-effect waves-green ml-2">Hủy</a>
            </div>

        </div>
    </div>

    <div id="EditBank" class="modal">
        <div class="modal-hd">
            <span class="right"><i class="material-icons modal-close right-align">clear</i></span>
            <h4 class="no-margin center-align">Chỉnh sửa ngân hàng</h4>
        </div>
        <div class="modal-bd">
            <div class="row">
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" placeholder="" ID="txtEditBankName"></asp:TextBox>
                    <label class="active">Tên ngân hàng</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" placeholder="" ID="txtEditAccountHolder"></asp:TextBox>
                    <label  class="active">Chủ tài khoản</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" placeholder="" ID="txtEditBankNumber"></asp:TextBox>
                    <label  class="active">Số tài khoản</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" placeholder="" ID="txtEditBranch"></asp:TextBox>
                    <label  class="active">Chi nhánh</label>
                </div>
                <div class="input-field file-field col s12 m6">
                    <span class="black-text">Hình ảnh</span>
                    <div style="display: inline-block; margin-left: 15px;">
                        <asp:FileUpload runat="server" ID="EditBankIMG" class="upload-img" type="file" onchange="previewFiles(this);" title=""></asp:FileUpload>
                        <button class="btn-upload">Upload</button>
                    </div>
                    <div class="preview-img">
                        <asp:Image ID="BankIMGBefore" runat="server" />
                    </div>
                </div>
                <div class="input-field col s12">
                    <div class="switch status-func">
                        <span class="mr-2">Trạng thái: </span>
                        <label>
                            Ẩn
                    <asp:TextBox runat="server" ID="EditBankStatus" onclick="BankStatusFunction()" type="checkbox"></asp:TextBox>
                            <span class="lever"></span>
                            Hiện
                        </label>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal-ft">
            <div class="ft-wrap center-align">
                <asp:Button runat="server" ID="btnEdit" class="modal-action btn waves-effect waves-green mr-2" Text="Cập nhật" OnClick="btnEdit_Click" />
                <%--  <a href="javascript:;" onclick="Create()" class="modal-action btn modal-close waves-effect waves-green mr-2">Thêm</a>--%>
                <a href="javascript:;" class="modal-action btn orange darken-2 modal-close waves-effect waves-green ml-2">Hủy</a>
            </div>

        </div>
    </div>
    <asp:Button runat="server" ID="btnDelete" style="display:none" OnClick="btnDelete_Click" />
    <asp:HiddenField runat="server" ID="hdfBankID" />
    <asp:HiddenField ID="hdfEditBankStatus" runat="server" Value="0" />
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
    </telerik:RadAjaxManager>
    <telerik:RadScriptBlock ID="sc" runat="server">
        <script type="text/javascript">
            function EditBank(ID) {
                console.log(ID);
                $.ajax({
                    type: "POST",
                    url: "/manager/BankList.aspx/loadinfoBank",
                    data: '{ID: "' + ID + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = JSON.parse(msg.d);
                        console.log(data);
                        if (data != null) {
                            $('#<%=txtEditBankName.ClientID%>').val(data.BankName);
                            $('#<%=txtEditBankNumber.ClientID%>').val(data.BankNumber);
                            $('#<%=txtEditBranch.ClientID%>').val(data.Branch);
                            $('#<%=txtEditAccountHolder.ClientID%>').val(data.AccountHolder);
                            $('#<%=BankIMGBefore.ClientID%>').attr("src", data.IMG);
                            var a = data.IsHidden;
                            if (a == false) {
                                $('#<%=EditBankStatus.ClientID%>').prop('checked', true);
                            $('#<%=hdfEditBankStatus.ClientID%>').val('0');
                        }
                        else {
                            $('#<%=EditBankStatus.ClientID%>').prop('checked', false);
                            $('#<%=hdfEditBankStatus.ClientID%>').val('1');
                            }
                            $('#<%=hdfBankID.ClientID%>').val(data.ID);
                            //$('select').formSelect();
                        }
                        else
                            swal("Error", "Không thành công", "error");
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                        swal("Error", "Fail updateInfoAcc", "error");
                    }
                });
            }

            function BankStatusFunction() {
                var a = $('#<%=hdfEditBankStatus.ClientID%>').val();
                if (a == '0') {
                    $('#<%=hdfEditBankStatus.ClientID%>').val('1');
                }
                else {
                    $('#<%=hdfEditBankStatus.ClientID%>').val('0');
                }
            }

            function Delete(ID) {
                var c = confirm("Bạn muốn xóa ngân hàng này?");
                if (c) {
                    $("#<%=hdfBankID.ClientID%>").val(ID);
                    $("#<%=btnDelete.ClientID%>").click();
                }
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
