<%@ Page Title="Lịch sử nạp tiền" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="HistorySendWalletAccountant.aspx.cs" Inherits="NHST.manager.HistorySendWalletAccountant" %>
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
                    <h4 class="title no-margin" style="display: inline-block;">Lịch sử nạp tiền</h4>
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
                                <asp:ListBox runat="server" id="select_by">
                                    <asp:ListItem value="0" Selected="True">Tất cả</asp:ListItem>
                                    <asp:ListItem value="1">Chờ duyệt</asp:ListItem>
                                    <asp:ListItem value="2">Đã duyệt</asp:ListItem>
                                    <asp:ListItem value="3">Đã hủy</asp:ListItem>
                                </asp:ListBox>
                                <label for="select_by">Trạng thái</label>
                            </div>

                             <div class="input-field col s12">
                                <asp:ListBox runat="server" ID="ddlIsPayLoan">
                                    <asp:ListItem Value="-1" Selected="True">Tất cả</asp:ListItem>
                                    <asp:ListItem Value="0">Chưa thanh toán</asp:ListItem>
                                    <asp:ListItem Value="1">Đã thanh toán</asp:ListItem>
                                </asp:ListBox>
                                <label for="select_by">Thanh toán tiền vay</label>
                            </div>

                            <div class="input-field col s6 l6">
                                <asp:TextBox runat="server" ID="rFD" type="text" class="datepicker from-date"></asp:TextBox>
                                 <label>Từ ngày</label>
                            </div>
                            <div class="input-field col s6 l6">
                                <asp:TextBox runat="server" ID="rTD" type="text" class="datepicker to-date"></asp:TextBox>          
                                <label>Đến ngày</label>
                                <span class="helper-text" data-error="Vui lòng chọn ngày bắt đầu trước"></span>
                            </div>
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
                                    <th>Bank</th>
                                    <th>Trạng thái</th>
                                    <th>Ngày nạp</th>
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
                            <h5>Thông tin nạp tiền #<asp:Label runat="server" id="lbID"></asp:Label></h5>
                            <a href="#!" class="close-editmode top-right valign-wrapper"><i
                                class="material-icons">close</i>Close</a>
                        </div>
                    </div>
                    <div class="col s12">
                        <div class="row pb-2 border-bottom-1 ">
                            <div class="input-field col s12">
                                <asp:TextBox runat="server" placeholder="" id="lblUsername" type="text" class="validate" Enabled="false"></asp:TextBox>
                                <label for="rp_username">Username</label>
                            </div>
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" placeholder="" id="pWallet" type="text" class="validate" Enabled="false"></asp:TextBox>
                                <label for="rp_vnd">Số tiền nạp (VNĐ)</label>
                            </div>                            
                            <div class="input-field col s12">
                                <asp:TextBox runat="server" placeholder="" id="pContent"
                                    class="materialize-textarea"></asp:TextBox>
                                <label for="rp_textarea">Nội dung</label>
                            </div>
                            <div class="input-field col s12">
                                <asp:ListBox runat="server" placeholder="" ID="ddlStatus">
                                    <asp:ListItem Value="1" Text="Chờ duyệt"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Đã duyệt"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Hủy"></asp:ListItem>
                                </asp:ListBox>
                                <label>Trạng thái</label>
                            </div>

                              <div class="input-field col s12">
                                <div class="switch status-func">
                                    <span class="mr-2">Tiền vay: </span>
                                    <label>
                                        <asp:TextBox ID="txtIsLoan" runat="server" type="checkbox" Enabled="false" onclick="StatusIsLoan()"></asp:TextBox><span class="lever"></span>
                                    </label>
                                </div>
                            </div>

                            <div class="input-field col s12">
                                <div class="switch status-func">
                                    <span class="mr-2">Thanh toán tiền vay: </span>
                                    <label>
                                        <asp:TextBox ID="txtIsPayLoan" runat="server" type="checkbox" onclick="StatusIsPayLoan()"></asp:TextBox><span class="lever"></span>
                                    </label>

                                </div>
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
      <asp:HiddenField ID="hdfIsLoan" runat="server" Value="0" />
    <asp:HiddenField ID="hdfIsPayLoan" runat="server" Value="0" />
        <asp:Button runat="server" ID="btnSaveEdit" OnClick="btncreateuser_Click" style="display:none" UseSubmitBehavior="false"/>
        <script type="text/javascript">

            function btnSave() {
                $('#<%=btnSaveEdit.ClientID%>').click();
            }
            function EditFunction(ID) {
                $.ajax({
                    type: "POST",
                    url: "/manager/HistorySendWalletAccountant.aspx/loadinfo",
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
                            $('#<%=pContent.ClientID%>').val(data.TradeContent);
                            $('#<%=ddlStatus.ClientID%>').val(data.Status);
                            $('#<%=ddlStatus.ClientID%>').prop('disabled', false);
                            if (data.Status != '1') {
                                console.log(data.Status);
                                $('#<%=ddlStatus.ClientID%>').prop('disabled', true);
                            }

                                var IsLoan = data.IsLoan;
                        if (IsLoan == false) {
                            $('#<%=txtIsLoan.ClientID%>').prop('checked', true);
                            $('#<%=hdfIsLoan.ClientID%>').val('0');
                        }
                        else {
                            $('#<%=txtIsLoan.ClientID%>').prop('checked', false);
                            $('#<%=hdfIsPayLoan.ClientID%>').val('1');
                        }

                        var IsPayLoan = data.IsPayLoan;
                        if (IsPayLoan == false) {
                            $('#<%=txtIsPayLoan.ClientID%>').prop('checked', true);
                            $('#<%=hdfIsPayLoan.ClientID%>').val('0');
                        }
                        else {
                            $('#<%=txtIsPayLoan.ClientID%>').prop('checked', false);
                            $('#<%=hdfIsPayLoan.ClientID%>').val('1');
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

              function StatusIsLoan() {
            var a = $('#<%=hdfIsLoan.ClientID%>').val();
             if (a == '0') {

                 $('#<%=hdfIsLoan.ClientID%>').val('1');
            }
            else {

                $('#<%=hdfIsLoan.ClientID%>').val('0');
            }
        }

        function StatusIsPayLoan() {
            var a = $('#<%=hdfIsPayLoan.ClientID%>').val();
             if (a == '0') {

                 $('#<%=hdfIsPayLoan.ClientID%>').val('1');
            }
            else {

                $('#<%=hdfIsPayLoan.ClientID%>').val('0');
            }
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
            $(document).ready(function () {
                $('#search_name').autocomplete({
                    data: {
                        "Apple": null,
                        "Microsoft": null,
                        "Google": 'https://placehold.it/250x250',
                        "Asgard": null
                    },
                });

            });
            function printPhieuthu(ID) {
                $.ajax({
                    type: "POST",
                    url: "/manager/HistorySendWalletAccountant.aspx/GetData",
                    data: "{ID:'" + ID + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = msg.d;
                        var c = ConfigurationController.GetByTop1();
                        if (data != "null") {
                            var ret = JSON.parse(data);
                            var html = "";
                            html += "<div class=\"print-bill\">";
                            html += "   <div class=\"top\">";
                            html += "       <div class=\"left\">";
                            html += "           <span class=\"company-info\">NHẬP HÀNG 360</span>";
                            html += "          <span class=\"company-info\">Địa chỉ: " + c.Address + "</span>";
                            html += "       </div>";
                            html += "       <div class=\"right\">";
                            html += "           <span class=\"bill-num\">Mẫu số 01 - TT</span>";
                            html += "           <span class=\"bill-promulgate-date\">(Ban hành theo Thông tư số 133/2016/TT-BTC ngày 26/8/2016 của Bộ Tài chính)</span>";
                            html += "       </div>";
                            html += "   </div>";
                            html += "   <div class=\"bill-title\">";
                            html += "       <h1>PHIẾU THU</h1>";
                            html += "       <span class=\"bill-date\">" + ret.CreateDate + " </span>";
                            html += "   </div>";
                            html += "   <div class=\"bill-content\">";
                            html += "       <div class=\"bill-row\">";
                            html += "           <label class=\"row-name\">Họ và tên người nộp tiền: </label>";
                            html += "           <label class=\"row-info\">" + ret.FullName + "</label>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row\">";
                            html += "           <label class=\"row-name\">Địa chỉ: </label>";
                            html += "           <label class=\"row-info\">" + ret.Address + "</label>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row\">";
                            html += "           <label class=\"row-name\">Lý do nộp: </label>";
                            html += "           <label class=\"row-info\"></label>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row\">";
                            html += "           <label class=\"row-name\">Số tiền: </label>";
                            html += "           <label class=\"row-info\">" + ret.Money + " VND</label>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row\">";
                            html += "           <label class=\"row-name\">Bằng chữ: </label>";
                            html += "           <label class=\"row-info\"></label>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row\">";
                            html += "           <div class=\"row-col\">";
                            html += "               <label class=\"row-name\">Kèm theo: </label>";
                            html += "               <label class=\"row-info\"></label>";
                            html += "           </div>";
                            html += "           <div class=\"row-col\">";
                            html += "               <label class=\"row-name\">Chứng từ gốc: </label>";
                            html += "               <label class=\"row-info\"></label>";
                            html += "           </div>";
                            html += "       </div>";
                            html += "   </div>";
                            html += "   <div class=\"bill-footer\">";
                            html += "       <div class=\"bill-row-one\">";
                            html += "           <strong>Giám đốc</strong>";
                            html += "           <span class=\"note\">(Ký, họ tên, đóng dấu)</span>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row-one\">";
                            html += "           <strong>Kế toán trưởng</strong>";
                            html += "           <span class=\"note\">(Ký, họ tên)</span>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row-one\">";
                            html += "           <strong>Người nộp tiền</strong>";
                            html += "           <span class=\"note\">(Ký, họ tên)</span>";
                            html += "       </div>";
                            html += "       <div class=\"bill-row-one\">";
                            html += "           <strong>Thủ quỹ</strong>";
                            html += "           <span class=\"note\">(Ký, họ tên)</span>";
                            html += "       </div>";
                            html += "   </div>";
                            html += "</div>";
                            $("#printcontent").html(html);

                            printDiv('printcontent');
                        }
                    },
                    error: function (xmlhttprequest, textstatus, errorthrow) {
                    }
                });

            }
            function printDiv(divid) {
                var divToPrint = document.getElementById('' + divid + '');
                var newWin = window.open('', 'Print-Window');
                newWin.document.open();
                newWin.document.write('<html><head><link rel="stylesheet" href="/App_Themes/NewUI/css/custom.css" type="text/css"/></head><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
                newWin.document.close();
                setTimeout(function () { newWin.close(); }, 10);
            }
        </script>
</asp:Content>
