<%@ Page Language="C#" MasterPageFile="~/UserMasterNew.Master" AutoEventWireup="true" CodeBehind="khieu-nai.aspx.cs" Inherits="NHST.khieu_nai" %>

<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="main">
        <div class="row">
            <div class="content-wrapper-before blue-grey lighten-5"></div>
            <div class="col s12">
                <div class="container">
                    <div class="all">
                        <div class="card-panel mt-3 no-shadow">
                            <div class="row">
                                <div class="col s12">
                                    <div class="page-title mt-2 center-align">
                                        <h4>Danh sách khiếu nại</h4>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col s12">
                                    <a href="taokhieunai.php" class="btn right mb-2" style="display: none" id="add-report">Gửi khiếu nại</a>
                                    <table class="table responsive-table   highlight bordered  centered">
                                        <thead>
                                            <tr>
                                                <th>Mã shop</th>
                                                <th class="tb-date">Tiền bồi thường</th>
                                                <th class="tb-date">Nội dung</th>
                                                <th class="tb-date">Trạng thái</th>
                                                <th class="tb-date">Ngày tạo</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <asp:Literal ID="ltr" runat="server"></asp:Literal>

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
                </div>
            </div>
        </div>
    </div>
</asp:Content>
