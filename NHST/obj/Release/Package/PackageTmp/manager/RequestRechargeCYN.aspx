<%@ Page Title="Lịch sử nạp tệ" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="RequestRechargeCYN.aspx.cs" Inherits="NHST.manager.RequestRechargeCYN" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button Style="display: none" UseSubmitBehavior="false" ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Lịch sử nạp tệ</h4>
                    <div class="right-action">
                        <a href="#" class="btn" id="filter-btn">Bộ lọc</a>
                    </div>
                    <div class="clearfix"></div>

                    <div class="filter-wrap">
                        <div class="row mt-2 pt-2">
                            <div class="search-name input-field col s12 l6">
                                <asp:TextBox ID="search_name" name="txtSearchName" type="text" onkeypress="myFunction()" runat="server" />
                                <label for="search_name"><span>Username</span></label>
                            </div>

                            <div class="input-field col s12 l6">
                                <asp:ListBox runat="server" ID="ddlStatus">
                                    <asp:ListItem Value="" Selected="True">Tất cả</asp:ListItem>
                                    <asp:ListItem Value="1">Chờ duyệt</asp:ListItem>
                                    <asp:ListItem Value="2">Đã duyệt</asp:ListItem>
                                    <asp:ListItem Value="3">Đã hủy</asp:ListItem>
                                </asp:ListBox>
                                <label for="select_by">Trạng thái</label>
                            </div>
                            <%--      <div class="input-field col s6 l6">
                                <asp:TextBox runat="server" ID="rFD" type="text" class="datetimepicker from-date"></asp:TextBox>
                                 <label>Từ ngày</label>
                            </div>
                            <div class="input-field col s6 l6">
                                <asp:TextBox runat="server" ID="rTD" type="text" class="datetimepicker to-date"></asp:TextBox>          
                                <label>Đến ngày</label>
                                <span class="helper-text" data-error="Vui lòng chọn ngày bắt đầu trước"></span>
                            </div>--%>
                            <div class="col s12 right-align">
                                <span class="search-action btn">Lọc kết quả</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="list-donate-money col s12 section">
                <div class="list-table card-panel">
                    <div class="filter">

                        <div class="clearfix"></div>
                    </div>
                    <div class="responsive-tb mt-2">
                        <table class="table responsive-table  bordered highlight  ">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Username</th>
                                    <th>Số tiền nạp</th>
                                    <th>Trạng thái</th>
                                    <th>Ngày tạo</th>
                                    <th>Người tạo</th>
                                    <th>Người duyệt</th>                                  
                                    <th>Thao tác</th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                            </tbody>
                        </table>
                    </div>
                    <div class="pagi-table float-right mt-2">
                        <%this.DisplayHtmlStringPaging1();%>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="bg-overlay"></div>
            <!-- Edit mode -->
            <div class="detail-fixed  col s12 m5 l5 xl4 section" id="donate-detail">
                <div class="rp-detail card-panel row">
                    <div class="col s12">
                        <div class="page-title">
                            <h5>Yêu cầu nạp tệ #<asp:Label runat="server" ID="lbID"></asp:Label></h5>
                            <a href="#!" class="close-editmode top-right valign-wrapper"><i
                                class="material-icons">close</i>Close</a>
                        </div>
                    </div>
                    <div class="col s12">
                        <div class="row pb-2 border-bottom-1 ">
                            <div class="input-field col s12">
                                <asp:TextBox runat="server" placeholder="" ID="lblUsername" type="text" class="validate" Enabled="false"></asp:TextBox>
                                <label for="rp_username">Username</label>
                            </div>
                            <div class="input-field col s12">
                                <asp:TextBox runat="server" placeholder="" ID="pWallet" TextMode="Number" class="validate" Enabled="false"></asp:TextBox>
                                <label for="rp_vnd">Số tiền nạp (VNĐ)</label>
                            </div>
                            <div class="input-field col s12">
                                <asp:TextBox runat="server" placeholder="" TextMode="MultiLine" ID="pContent" ReadOnly="true"
                                    class="materialize-textarea"></asp:TextBox>
                                <label for="rp_textarea">Nội dung</label>
                            </div>
                            <div class="input-field col s12">
                                <asp:ListBox runat="server" placeholder="" ID="EditddlStatus">
                                    <asp:ListItem Value="1" Text="Chờ duyệt"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Đã duyệt"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Hủy"></asp:ListItem>
                                </asp:ListBox>
                                <label>Trạng thái</label>
                            </div>
                        </div>
                        <div class="row section mt-2">
                            <div class="col s12">
                                <a href="javascript:;" onclick="btnSave()" class="btn">Cập nhật</a>
                                <a href="#" class="btn close-editmode">Trở về</a>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <!-- END : Edit mode -->
        </div>
    </div>
    <div id="printcontent" style="display: none">
    </div>
    <asp:HiddenField runat="server" ID="hdfIDHSW" />
    <asp:Button runat="server" ID="btnSaveEdit" OnClick="btnSave_Click" Style="display: none" UseSubmitBehavior="false" />
    <asp:HiddenField runat="server" ID="hdfMoney" />
    <asp:HiddenField runat="server" ID="hdfContent" />
    <asp:HiddenField runat="server" ID="hdfStatus" />
    <script type="text/javascript">
        function btnSave() {
            $('#<%=btnSaveEdit.ClientID%>').click();
        }
        function EditFunction(ID) {
            $.ajax({
                type: "POST",
                url: "/manager/RequestRechargeCYN.aspx/loadinfo",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = JSON.parse(msg.d);
                    if (data != null) {
                        $('#<%=lbID.ClientID%>').text(ID);
                        $('#<%=hdfIDHSW.ClientID%>').val(ID);
                        $('#<%=lblUsername.ClientID%>').val(data.Username);
                        $('#<%=pWallet.ClientID%>').val(data.Amount);
                        $('#<%=pContent.ClientID%>').val(data.Note);
                        $('#<%=EditddlStatus.ClientID%>').val(data.Status);

                        $('#<%=hdfMoney.ClientID%>').val(data.Amount);
                        $('#<%=hdfContent.ClientID%>').val(data.Note);
                        $('#<%=hdfStatus.ClientID%>').val(data.Status);
                        $('#<%=EditddlStatus.ClientID%>').prop('disabled', false);
                        if (data.Status != '1') {
                            console.log(data.Status);
                            $('#<%=EditddlStatus.ClientID%>').prop('disabled', true);
                        }
                        $('select').formSelect();
                    }
                    else
                        swal("Error", "Không thành công", "error");
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    swal("Error", "Fail updateInfoAcc", "error");
                }
            });
        }
        function myFunction() {
            if (event.which == 13 || event.keyCode == 13) {
                console.log($('#<%=search_name.ClientID%>').val());
                $('#<%=btnSearch.ClientID%>').click();
            }
        }
        $('.search-action').click(function () {
            console.log($('#<%=search_name.ClientID%>').val());
            $('#<%=btnSearch.ClientID%>').click();
        })
    </script>
</asp:Content>

