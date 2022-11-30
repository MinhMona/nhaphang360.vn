<%@ Page Title="Cấu hình phí thanh toán hộ" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="PriceChangeList.aspx.cs" Inherits="NHST.manager.PriceChangeList" %>

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
                    <h4 class="title no-margin" style="display: inline-block;">CẤU HÌNH PHÍ THANH TOÁN HỘ</h4>
                   <%-- <div class="right-action">
                        <a href="/manager/AddPriceChange.aspx" class="btn waves-effect">Thêm mới</a>
                    </div>--%>
                </div>
            </div>
            <div class="col s12 m12 section">
                <div class="list-table card-panel">
                    <div class="filter">
                        <div class="search-name input-field">
                            <div class="grid-row">
                                <div class="grid-col-25">
                                    <div class="lb" style="font-weight:bold;font-size:20px; color:#004d40">Giá tiền mặc định: <asp:Label ID="lblPriceDefault" runat="server"></asp:Label> VNĐ</div>
                                </div>
                                 <div class="grid-col-25">
                                    
                                </div>
                               
                            </div>

                        </div>
                    </div>
                    <div class="responsive-tb">
                        <table class="table bordered centered highlight  ">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Giá tệ từ</th>
                                    <th>Giá tệ đến</th>
                                    <th>VIP 0</th>
                                    <th>VIP 1</th>
                                    <th>VIP 2</th>
                                    <th>VIP 3</th>
                                    <th>VIP 4</th>
                                    <th>VIP 5</th>
                                    <th>VIP 6</th>
                                    <th>VIP 7</th>
                                    <th>VIP 8</th>
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
                </div>
            </div>

        </div>

    </div>
    <!-- END: Page Main-->


    <!-- BEGIN: Modal Add NV -->
    <!-- Modal Structure -->
    <div id="modalEditFee" class="modal modal-fixed-footer">
        <div class="modal-hd">
            <span class="right"><i class="material-icons modal-close right-align">clear</i></span>
            <h4 class="no-margin center-align">CẤU HÌNH PHÍ THANH TOÁN HỘ#<asp:Label runat="server" ID="lbID"></asp:Label></h4>
        </div>
        <div class="modal-bd">
            <div class="row">
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" id="txtPriceFrom" type="text" class="validate" data-type="text-only" value="0"></asp:TextBox>
                    <label for="edit_from_warehouse">Giá tệ từ</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" id="txtPriceTo" type="text" class="validate" data-type="text-only" value="3,000,000"></asp:TextBox>
                    <label for="edit_to_warehouse">Giá tệ đến</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" id="txtVip0" type="text" class="validate" data-type="text-only" value="0"></asp:TextBox>
                    <label for="edit_vip0">Vip 0</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" id="txtVip1" type="text" class="validate" data-type="text-only" value="0"></asp:TextBox>
                    <label for="edit_vip1">Vip 1</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" id="txtVip2" type="text" class="validate" data-type="text-only" value="0"></asp:TextBox>
                    <label for="edit_vip2">Vip 2</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" id="txtVip3" type="text" class="validate" data-type="text-only" value="0"></asp:TextBox>
                    <label for="edit_vip3">Vip 3</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" id="txtVip4" type="text" class="validate" data-type="text-only" value="0"></asp:TextBox>
                    <label for="edit_vip4">Vip 4</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" id="txtVip5" type="text" class="validate" data-type="text-only" value="0"></asp:TextBox>
                    <label for="edit_vip5">Vip 5</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" id="txtVip6" type="text" class="validate" data-type="text-only" value="0"></asp:TextBox>
                    <label for="edit_vip6">Vip 6</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" id="txtVip7" type="text" class="validate" data-type="text-only" value="0"></asp:TextBox>
                    <label for="edit_vip7">Vip 7</label>
                </div>
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" id="txtVip8" type="text" class="validate" data-type="text-only" value="0"></asp:TextBox>
                    <label for="edit_vip8">Vip 8</label>
                </div>

            </div>
        </div>
        <div class="modal-ft">
            <div class="ft-wrap center-align">
                    <asp:Button runat="server" ID="btnSave" UseSubmitBehavior="false" OnClick="btnSave_Click" class="btn white-text mr-2" Text="Cập nhật"></asp:Button>

                <a href="#!" class="modal-action btn orange darken-2 modal-close waves-effect waves-green ml-2">Hủy</a>
            </div>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="hdfID" />
    <script> 
        function EditFunction(ID) {
            $.ajax({
                type: "POST",
                url: "/manager/pricechangeList.aspx/loadinfo",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = JSON.parse(msg.d);
                    if (data != null) {
                        $('#<%=hdfID.ClientID%>').val(ID);
                        $('#<%=lbID.ClientID%>').text(ID);
                        $('#<%=txtPriceFrom.ClientID%>').val(data.PriceFromCYN);
                        $('#<%=txtPriceTo.ClientID%>').val(data.PriceToCYN);
                        $('#<%=txtVip0.ClientID%>').val(data.Vip0);
                        $('#<%=txtVip1.ClientID%>').val(data.Vip1);
                        $('#<%=txtVip2.ClientID%>').val(data.Vip2);
                        $('#<%=txtVip3.ClientID%>').val(data.Vip3);
                        $('#<%=txtVip4.ClientID%>').val(data.Vip4);
                        $('#<%=txtVip5.ClientID%>').val(data.Vip5);
                        $('#<%=txtVip6.ClientID%>').val(data.Vip6);
                        $('#<%=txtVip7.ClientID%>').val(data.Vip7);
                        $('#<%=txtVip8.ClientID%>').val(data.Vip8);
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
    </script>
</asp:Content>