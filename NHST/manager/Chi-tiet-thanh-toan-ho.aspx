<%@ Page Title="Chi tiết thanh toán hộ" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="Chi-tiet-thanh-toan-ho.aspx.cs" Inherits="NHST.manager.Chi_tiet_thanh_toan_ho" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Chi tiết yêu cầu</h4>
                    <div class="right-action">
<%--                        <a href="#" class="btn">In đơn hàng</a>
                        <a href="#" class="btn">In tem</a>--%>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="order-detail-wrap col s12 section">
                <div class="row">
                    <div class="col s12 m12 l4 sticky-wrap">
                        <div class="card-panel">
                            <div class="order-stick-detail">
                                <div class="order-stick order-owner">
                                    <table class="table   ">
                                        <tbody>
                                            <tr>
                                                <td class="tb-date">ID</td>
                                                <td><asp:Label runat="server" ID="lbID"></asp:Label></td>
                                            </tr>
                                              <tr>
                                                <td class="tb-date">Trạng thái</td>
                                                <td>
                                                    <asp:Literal runat="server" ID="ltrStatus"></asp:Literal>
                                                   <%-- <span class="badge green white-text darken-2">Đã thanh toán</span>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="tb-date">Tổng tiền</td>
                                                <td><asp:Label runat="server" ID="lbTongTien"></asp:Label></td>
                                            </tr>
                                            <%--<tr>
                                                <td class="tb-date">Đã trả</td>
                                                <td>12,000,000 VNĐ</td>
                                            </tr>
                                            <tr>
                                                <td class="tb-date">Còn lại</td>
                                                <td>0 VNĐ</td>
                                            </tr>--%>
                                        </tbody>
                                    </table>
                                </div>

                                <div class="action-btn center-align">
                                    <a href="javascript:;" class="btn mt-2"  onclick="update()">Cập nhật</a>
                                    <asp:Literal runat="server" ID="ltrBack"></asp:Literal>                                  
                                </div>
                            </div>
                        </div>



                    </div>
                    <div class="col s12 m12 l8">
                        <div class="card-panel">
                            <div class="section">
                                <div class="title-header bg-dark-gradient">
                                    <h6 class="white-text ">Chi tiết hóa đơn</h6>
                                </div>
                                <div class="child-fee">
                                    <div class="content-panel">
                                        <div class="row section">
                                            <div class="col s12">
                                                <div class="order-row">
                                                    <div class="left-fixed">
                                                        <p class="txt">Username:</p>
                                                    </div>
                                                    <div class="right-content">
                                                        <div class="row">
                                                            <div class="input-field col s12">
                                                                <asp:TextBox runat="server" ID="txtUserName" type="text" disabled></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="order-row top-justify">
                                                    <div class="left-fixed">
                                                        <p class="txt">Hóa đơn thanh toán hộ:</p>
                                                    </div>
                                                    <div class="right-content">
                                                        <div class="list-order">
                                                            <asp:Literal runat="server" ID="ltrList"></asp:Literal>                                                          
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="order-row">
                                                    <div class="left-fixed">
                                                        <p class="txt">Tỉ giá thật:</p>
                                                    </div>
                                                    <div class="right-content">
                                                        <div class="row">
                                                            <div class="input-field col s12 m6">
                                                                <asp:TextBox runat="server" ID="txtRealCurrency" placeholder="0" disabled></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="order-row">
                                                    <div class="left-fixed">
                                                        <p class="txt">Tổng tiền:</p>
                                                    </div>
                                                    <div class="right-content">
                                                        <div class="row">
                                                            <div class="input-field col s12 m6">
                                                                <asp:TextBox runat="server" ID="txtTotalPriceCNY" placeholder="0"  class=""></asp:TextBox>
                                                                <label>Tệ (¥)</label>
                                                            </div>
                                                            <div class="input-field col s12 m6">
                                                                <asp:TextBox runat="server" ID="txtTotalPriceVND" placeholder="0" ></asp:TextBox>
                                                                <label>Việt Nam Đồng (VNĐ)</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="order-row">
                                                    <div class="left-fixed">
                                                        <p class="txt">Giá Tệ - VND</p>
                                                    </div>
                                                    <div class="right-content">
                                                        <div class="row">
                                                            <div class="input-field col s12 m6">
                                                                <asp:TextBox runat="server" ID="txtPriceCNY" placeholder="0" ></asp:TextBox>
                                                                <label>Tệ (¥)</label>
                                                            </div>
                                                            <div class="input-field col s12 m6">
                                                                <asp:TextBox runat="server" ID="txtPriceVND" placeholder="0"></asp:TextBox>
                                                                <label>Việt Nam Đồng (VNĐ)</label>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="order-row">
                                                    <div class="left-fixed">
                                                        <p class="txt">Tổng tiền thật:</p>
                                                    </div>
                                                    <div class="right-content">
                                                        <div class="row">
                                                            <div class="input-field col s12 m6">
                                                                <asp:TextBox runat="server" ID="txtTotalRealPaid" placeholder="0"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="order-row">
                                                    <div class="left-fixed">
                                                        <p class="txt">Tổng tiền trả cuối:</p>
                                                    </div>
                                                    <div class="right-content">
                                                        <div class="row">
                                                            <div class="input-field col s12 m6">
                                                                <asp:TextBox runat="server" ID="txtTotalLastestPayment" placeholder="0"></asp:TextBox>

                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="order-row">
                                                    <div class="left-fixed">
                                                        <p class="txt">Số điện thoại:</p>
                                                    </div>
                                                    <div class="right-content">
                                                        <div class="row">
                                                            <div class="input-field col s12 m6">
                                                                <asp:TextBox runat="server" ID="txtPhoneNumber" placeholder="0"></asp:TextBox>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="order-row">
                                                    <div class="left-fixed">
                                                        <p class="txt">Ghi chú:</p>
                                                    </div>
                                                    <div class="right-content">
                                                        <div class="row">
                                                            <div class="input-field col s12">
                                                                <asp:TextBox TextMode="MultiLine" runat="server" ID="txtSummary"
                                                                    CssClass="materialize-textarea">Lorem ipsum dolor sit amet consectetur adipisicing elit. Neque praesentium consectetur optio ipsa laborum! Consequuntur sed voluptatum non eum fugit reiciendis, quia nobis culpa eligendi rerum! Repudiandae fugit corrupti quidem!</asp:TextBox>
                                                                <label for="textarea2">Nội dung</label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="order-row">
                                                    <div class="left-fixed">
                                                        <p class="txt">Chưa hoàn thiện</p>
                                                    </div>
                                                    <div class="right-content">
                                                        <div class="row">
                                                            <div class="center-checkbox col s12">
                                                                <label>
                                                                    <input type="checkbox" id="cbComplete" />
                                                                    <span></span>
                                                                </label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="order-row">
                                                    <div class="left-fixed">
                                                        <p class="txt">Trạng thái</p>
                                                    </div>
                                                    <div class="right-content">
                                                        <div class="row">
                                                            <div class="input-field col s12 m6">
                                                                <asp:ListBox runat="server" ID="ddlStatusDetail">
                                                                         <asp:ListItem Text="Đã hủy" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Chưa thanh toán" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="Đã xác nhận" Value="4"></asp:ListItem>
                                                                        <asp:ListItem Text="Đã thanh toán" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Hoàn thành" Value="3"></asp:ListItem>
                                                                </asp:ListBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                </div>
            </div>

        </div>
    </div>
    <asp:HiddenField ID="hdfList" runat="server" />
    <asp:HiddenField runat="server" ID="hdfCheckComplete" />
    <asp:Button Style="display: none" runat="server" ID="btnSave" OnClick="btnSave_Click"  UseSubmitBehavior="false"/>
    <asp:Button Style="display: none" runat="server" ID="btnBack" OnClick="btnBack_Click"  UseSubmitBehavior="false"/>
    <!-- END: Page Main-->
    <script>        
        function update() {
            var list = "";
            $(".itemyeuau").each(function () {
                var id = $(this).attr("data-id");
                var des1 = $(this).find(".txtDesc1").val();
                var des2 = $(this).find(".txtDesc2").val();
                list += id + "," + des1 + "," + des2 + "|";
            });
            if ($('#cbComplete').prop("checked") == true) {
                $("#<%=hdfCheckComplete.ClientID%>").val('true')
            }
            else {
                $("#<%=hdfCheckComplete.ClientID%>").val('false')
            } 
            $("#<%=hdfList.ClientID%>").val(list);
            $("#<%=btnSave.ClientID%>").click();
        }
        $(document).ready(function () {
            var ck = $("#<%=hdfCheckComplete.ClientID%>").val().toLowerCase();
            console.log(ck);
            if (ck == 'true')
            {
                $('#cbComplete').prop('checked', true);
            }
            $(window).scroll(function () {
                var id = $('.table-of-contents li a.active').attr('href');

                $('.scrollspy').each(function () {
                    var itemId = $(this).attr('id');
                    if (('#' + itemId) == id) {
                        $(this).parent().css({
                            'box-shadow': '0 8px 17px 2px rgba(0, 0, 0, 0.14), 0 3px 14px 2px rgba(0, 0, 0, 0.12), 0 5px 5px -3px rgba(0, 0, 0, 0.2)',

                        });
                        $('.scrollspy').not(this).parent().css({
                            'box-shadow': 'rgba(0, 0, 0, 0.14) 0px 2px 2px 0px, rgba(0, 0, 0, 0.12) 0px 3px 1px -2px, rgba(0, 0, 0, 0.2) 0px 1px 5px 0px',

                        });
                    }

                });

            });
        });
    </script>
</asp:Content>
