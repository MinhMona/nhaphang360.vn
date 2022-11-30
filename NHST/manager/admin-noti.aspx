<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="admin-noti.aspx.cs" Inherits="NHST.manager.admin_noti" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
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
            <div class="list-staff col s12 section">    
                <div class="list-table card-panel">
                    <div class="filter">
                        <div class="row">
                            <div class="input-field col s6 m4 l5">
                                <asp:TextBox runat="server" ID="txtFromDate" type="text" class="datetimepicker from-date"></asp:TextBox>
                                <label>Từ ngày</label>
                            </div>
                            <div class="input-field col s6 m4 l5">
                              <asp:TextBox runat="server" type="text" ID="txtToDate" class="datetimepicker to-date"></asp:TextBox>
                                <label>Đến ngày</label>
                                <span class="helper-text" data-error="Vui lòng chọn ngày bắt đầu trước"></span>
                            </div>
                            <div class="input-field col s12 m4 l2">
                                <a href="javascript:;" class="btn btnSearch">Xem</a>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>

                    <table class="table responsive-table bordered highlight">
                        <thead>
                            <tr>
                                <th>Ngày</th>
                                <th>Mã đơn hàng</th>
                                <th>Nội dung</th>              
                                <th>Trạng thái</th>  
                            </tr>
                        </thead>
                        <tbody>
                           <asp:Literal runat="server" ID="ltr"></asp:Literal>            
    
                        </tbody>
                    </table>
                    <div class="pagi-table float-right mt-2">
                          <%this.DisplayHtmlStringPaging1();%>      
                    </div>
                    <div class="clearfix"></div>    
                </div>
            </div>
        </div>
    </div>    
    <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" />
    <!-- END: Page Main-->
    <script>
        $('.btnSearch').click(function () {
            $('#<%=btnSearch.ClientID%>').click();
        });
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
    </script>
    <%-- <main id="main-wrap">
        <div class="all">
            <div class="main">
                <div class="sec form-sec">
                    <div class="sec-tt">
                        <h2 class="tt-txt">Thông báo của bạn</h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                        </p>
                    </div>
                    
                </div>
            </div>
        </div>
    </main>--%>
</asp:Content>

