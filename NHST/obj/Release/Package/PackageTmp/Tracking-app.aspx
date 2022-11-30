<%@ Page Title="" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="Tracking-app.aspx.cs" Inherits="NHST.Tracking_app" %>

<asp:Content runat="server" ID="head" ContentPlaceHolderID="head"></asp:Content>

<asp:Content runat="server" ID="main" ContentPlaceHolderID="ContentPlaceHolder1">
    <main id="main-wrap">
    <div class="page-wrap">
        <div class="page-body">
            <div class="heading-search">
                <div class="all">
                    <div class="frow">
                       <%-- <input type="text" class="fcontrol" placeholder="Mã đơn hàng">--%>
                         <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control" placeholder="Nhập mã vận đơn"></asp:TextBox>
                    </div>
                      <asp:Button runat="server" ID="btnSearch" Text="Tra cứu mã vận đơn" CssClass="btn primary-btn fw-btn"
                                OnClick="btnSearch_Click" />
                   <%-- <a href="#" class="btn primary-btn fw-btn">Tracking</a>--%>
                </div>
            </div>
            <div class="search-result-list">
                <div class="all">
                    <p class="result-title"><span class="gray-txt"><asp:Literal runat="server" ID="ltrTitle"></asp:Literal></span></p>
                    <div class="order-group offset15">
                       <asp:Literal runat="server" ID="ltrTrack"></asp:Literal>

                      
                      <%--  <div class="smr">
                            <div class="tracking-wrap">
                                <div class="tk-heading">
                                    <div class="tk-left">
                                        <p class="tk-title">Ngày</p>
                                    </div>
                                    <div class="tk-right">
                                        <p class="tk-title">Trạng thái</p>
                                    </div>
                                </div>

                                <div class="tk-row">
                                    <div class="tk-left">
                                        08-08-2018<br>
                                        09:51
                                    </div>
                                    <div class="tk-right">
                                        Đã về kho Trung Quốc<br>
                                        Người nhận: <span class="hl-txt">Shockdie127 <i class="hl-txt fa fa-caret-down"></i></span>
                                    </div>
                                </div>
                                <div class="tk-row current">
                                    <div class="tk-left">
                                        08-08-2018<br>
                                        09:51
                                    </div>
                                    <div class="tk-right">
                                        Đã về kho Trung Quốc <i class="hl-txt fa fa-caret-down"></i><br>
                                        Người nhận: <span class="hl-txt">Shockdie127</span>
                                    </div>
                                </div>
                                <div class="tk-row pass">
                                    <div class="tk-left">
                                        08-08-2018<br>
                                        09:51
                                    </div>
                                    <div class="tk-right">
                                        Đã về kho Trung Quốc <i class="hl-txt fa fa-caret-down"></i><br>
                                        Người nhận: <span class="hl-txt">Shockdie127</span>
                                    </div>
                                </div>
                                <div class="tk-row pass">
                                    <div class="tk-left">
                                        08-08-2018<br>
                                        09:51
                                    </div>
                                    <div class="tk-right">
                                        Đã về kho Trung Quốc <i class="hl-txt fa fa-caret-down"></i><br>
                                        Người nhận: <span class="hl-txt">Shockdie127</span>
                                    </div>
                                </div>
                            </div>
                        </div>--%>

                    </div>
                </div>
            </div>
        </div>
        <div class="page-bottom-toolbar">
            
        </div>
    </div>
</main>   
</asp:Content>
