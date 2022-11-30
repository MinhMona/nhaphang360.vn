<%@ Page Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="report-vch.aspx.cs" Inherits="NHST.manager.report_vch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Thống kê cước vận chuyển hộ</h4>
                    <div class="right-action">
                        <a href="#" class="btn" id="filter-btn">Bộ lọc</a>
                    </div>
                    <div class="clearfix"></div>
                    <div class="filter-wrap">
                        <div class="row mt-2 pt-2">
                             <div class="input-field col s6">
                                <asp:TextBox runat="server" type="text" ID="search_name" placeholder="" class=""></asp:TextBox>
                                <label>Username</label>
                            </div>
                              <div class="input-field col s6">
                                <asp:TextBox runat="server" type="text" ID="search_mvd" placeholder="" class=""></asp:TextBox>
                                <label>Mã vận đơn</label>
                            </div>
                            <div class="input-field col s12 l6">
                                <asp:ListBox runat="server" ID="ddlstatus">
                                    <asp:ListItem Value="-1" Selected="true" Text="Tất cả"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Chưa thanh toán"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Đã thanh toán"></asp:ListItem>
                                </asp:ListBox>
                                <label for="status">Trạng thái</label>
                            </div>

                            <div class="input-field col s6 l3">
                                <asp:TextBox ID="rFD" runat="server" Type="text" class="datetimepicker from-date"></asp:TextBox>
                                <label>Từ ngày</label>
                            </div>
                            <div class="input-field col s6 l3">
                                <asp:TextBox runat="server" Type="text" ID="rTD" class="datetimepicker to-date"></asp:TextBox>
                                <label>Đến ngày</label>
                                <span class="helper-text" data-error="Vui lòng chọn ngày bắt đầu trước"></span>
                            </div>

                            <div class="col s12 right-align">
                                <asp:Button runat="server" ID="filter" OnClick="btnSearch_Click" class="btnSort btn" Text="Lọc kết quả"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="list-order-cus col s12 section">
                <div class="list-table card-panel">
                    <div class="table-info row center-align-xs" style="display: none">
                        <div class="checkout col s12 m6">
                            <p class="black-text">
                                <span class="lbl">Tổng cân nặng:</span><span
                                    class="black-text font-weight-700"><asp:Literal runat="server" ID="lblWeightAll"></asp:Literal></span>
                            </p>
                            <p class="black-text">
                                <span class="lbl">Tổng tiền đã thanh toán:</span><span
                                    class="black-text font-weight-700"><asp:Literal runat="server" ID="lblPriceAllVND"></asp:Literal></span>
                            </p>
                            <p class="black-text">
                                <span class="lbl">Tổng tiền chưa thanh toán: </span><span
                                    class="black-text font-weight-700">
                                    <asp:Literal runat="server" ID="lblPriceNotPay"></asp:Literal></span>
                            </p>
                        </div>
                    </div>
                    <div class="responsive-tb mt-3">
                        <table class="table bordered highlight   ">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Tên tài khoản</th>
                                    <th>Ngày YCXK</th>
                                    <th>Ngày XK</th>
                                    <th>Tổng số kiện</th>
                                    <th>Tổng số kg</th>
                                    <th>Tổng cước</th>
                                    <th>HTVC</th>
                                    <th>Trạng thái thanh toán</th>
                                    <th>Trạng thái xuất kho</th>
                                    <th>Ghi chú</th>
                                    <th>Thao tác</th>
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
    </div>
    <asp:HiddenField runat="server" ID="hdfID" />
    <asp:Button ID="btnAllOutstock" runat="server" UseSubmitBehavior="false" OnClick="btnAllOutstock_Click" Style="display: none" />
    <asp:Button runat="server" ID="btnPayByWallet" Style="display: none" OnClick="btnPayByWallet_Click" UseSubmitBehavior="false" />
    <asp:Button runat="server" ID="btnPay" OnClick="btnPay_Click" UseSubmitBehavior="false" Style="display: none" />
    <asp:Button runat="server" ID="btnDelete" OnClick="btnDelete_Click" UseSubmitBehavior="false" Style="display: none" />
    <asp:HiddenField ID="hdfListPID" runat="server" />
    <!-- END: Page Main-->

    <!-- BEGIN: Modal edit services -->
    <div id="modalEditServices" class="modal modal-fixed-footer">
        <div class="modal-hd">
            <span class="right"><i class="material-icons modal-close right-align">clear</i></span>
            <h4 class="no-margin center-align">Chỉnh sửa tổng tiền</h4>
        </div>
        <div class="modal-bd">
            <div class="row">
                <div class="input-field col s12 m12">
                    <asp:TextBox runat="server" ID="EditServicesTotalPriceVND"  placeholder="" type="text" class="validate" data-type="currency"></asp:TextBox>
                    <label class="active" for="edit__step-name">Tổng tiền</label>
                </div>
            </div>
        </div>
        <div class="modal-ft">
            <div class="ft-wrap center-align">
                <a id="btnUpService" onclick="btnUpService()" class="modal-action btn modal-close waves-effect waves-green mr-2">Cập nhật</a>
                <a href="#!" class="modal-action btn orange darken-2 modal-close waves-effect waves-green ml-2">Hủy</a>
            </div>

        </div>
    </div>
    <asp:Button ID="buttonUpServices" runat="server" OnClick="btnUpServices_Click" Style="display: none" />
      <asp:Button Style="display: none" UseSubmitBehavior="false" ID="btnSearch" runat="server" OnClick="btnSearch_Click" />
    <asp:HiddenField ID="hdfEditServicesID" runat="server" Value="0" />
    <asp:HiddenField ID="hdfEditServicesStatus" runat="server" Value="0" />
    <!-- END: Modal edit services -->
       <script type="text/javascript">
           function btnUpService() {
               $('#<%=buttonUpServices.ClientID%>').click();
           }
           function EditServices(ID) {
               $.ajax({
                   type: "POST",
                   url: "/manager/report-vch.aspx/loadinfoServices",
                   data: '{ID: "' + ID + '"}',
                   contentType: "application/json; charset=utf-8",
                   dataType: "json",
                   success: function (msg) {
                       var data = JSON.parse(msg.d);
                       if (data != null) {
                           $('#<%=EditServicesTotalPriceVND.ClientID%>').val(data.TotalPriceVND.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
                       
                        $('#<%=hdfEditServicesID.ClientID%>').val(data.ID);
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
           function ServicesStatusFunction() {
               var a = $('#<%=hdfEditServicesStatus.ClientID%>').val();
            if (a == '0') {

                $('#<%=hdfEditServicesStatus.ClientID%>').val('1');
            }
            else {

                $('#<%=hdfEditServicesStatus.ClientID%>').val('0');
            }
           }
          
           
           function Delete(obj, ID) {
               var c = confirm('Bạn muốn hủy mã thống kê này?');
               if (c) {
                   $("#<%=hdfID.ClientID%>").val(ID);
                $("#<%=btnDelete.ClientID%>").click();
            }
         }

           
    </script>






    <script type="text/javascript">
        function updateNote(obj, ID) {
            var staffNote = obj.parent().find(".txtNote").val();
            $.ajax({
                type: "POST",
                url: "/manager/Report-VCH.aspx/UpdateStaffNote",
                data: "{ID:'" + ID + "',staffNote:'" + staffNote + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var ret = msg.d;
                    if (ret == "ok") {
                        obj.parent().find(".update-info").show();
                    }
                    else {
                        obj.parent().find(".update-info").hide();
                    }
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    //alert('lỗi checkend');
                }
            });
        }

        function xuatkhotatcakien(obj, ID) {
            var c = confirm('Bạn muốn in phiếu');
            if (c) {
                $("#<%=hdfID.ClientID%>").val(ID);
                $("#<%=btnAllOutstock.ClientID%>").click();
            }
        }

        function PayByWallet(obj, ID) {
            var c = confirm('Bạn muốn thanh toán bằng ví?');
            if (c) {
                $("#<%=hdfID.ClientID%>").val(ID);
                $("#<%=btnPayByWallet.ClientID%>").click();
            }
        }

        function myFunction() {
        <%--    if (event.which == 13 || event.keyCode == 13) {
                console.log($('#<%=search_name.ClientID%>').val());--%>
             $('#<%=btnSearch.ClientID%>').click();
            //}
        }
        $('.search-action').click(function () {
            console.log('dkm ngon');
            console.log($('#<%=search_name.ClientID%>').val());
            $('#<%=btnSearch.ClientID%>').click();
        })

        function Pay(obj, ID) {
            var c = confirm('Bạn muốn thanh toán trực tiếp?');
            if (c) {
                $("#<%=hdfID.ClientID%>").val(ID);
                $("#<%=btnPay.ClientID%>").click();
            }
        }

        function VoucherSourcetoPrint(source) {
            var r = "<html><head><link rel=\"stylesheet\" href=\"/App_Themes/AdminNew/css/style.css\" type=\"text/css\"/><link rel=\"stylesheet\" href=\"/App_Themes/AdminNew/css/style-p.css\" type=\"text/css\"/><script>function step1(){\n" +
                "setTimeout('step2()', 10);}\n" +
                "function step2(){window.print();window.close()}\n" +
                "</scri" + "pt></head><body onload='step1()'>\n" +
                "" + source + "</body></html>";
            return r;
        }
        function VoucherPrint(source) {
            Pagelink = "about:blank";
            var pwa = window.open(Pagelink, "_new");
            pwa.document.open();
            pwa.document.write(VoucherSourcetoPrint(source));
            pwa.document.close();
        }
    </script>
    <style>
        .modal.modal-fixed-footer
        {
            height: auto !important;
        }
        .table .action-table{
            flex-flow: column;
        }
        .btn {
                white-space: nowrap;
        }
    </style>
</asp:Content>
