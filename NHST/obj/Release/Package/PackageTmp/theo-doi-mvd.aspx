<%@ Page Title="" Language="C#" MasterPageFile="~/MinhAnMaster.Master" AutoEventWireup="true" CodeBehind="theo-doi-mvd.aspx.cs" Inherits="NHST.theo_doi_mvd" %>

<%--<%@ Register Src="~/UC/uc_Banner.ascx" TagName="Banner" TagPrefix="uc" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
       <%-- <uc:Banner ID="Banner1" runat="server" />--%>
        <section id="firm-services" class="services" style="padding: 30px 0;">
            <div class="all">
                <div class="breakcrum">
                    <a href="/" class="brc-home"><i class="fa fa-home"></i>Trang chủ</a>
                    <span class="brc-seperate"><i class="fa fa-arrow-right" style="font-size: 10px;"></i></span>
                    <a href="javascript:;">Theo dõi mã vận đơn</a>
                </div>
                <h4 class="sec__title center-txt">Theo dõi mã vận đơn</h4>
                <div class="primary-form custom-width">
                    <div class="order-tool clearfix">
                        <div class="tool-detail">
                            <div class="pane-shadow filter-form" id="filter-form">
                                <div class="grid-row">
                                    <div class="grid-col-100">
                                        <asp:TextBox runat="server" ID="tSearchName" CssClass="form-control txtsearchfield" placeholder="Nhập mã vận đơn"></asp:TextBox>
                                    </div>
                                    <div class="grid-col-100 center-txt">
                                        <asp:Button runat="server" ID="btnSearch" Text="TRA CỨU MÃ VẬN ĐƠN"
                                            CssClass="submit-form"
                                            OnClick="btnSearch_Click" UseSubmitBehavior="false"
                                            Style="display: inline-block; margin: 20px 0; border: none; color: #fff; cursor: pointer; float: none" />
                                    </div>
                                </div>
                                <style>
                                    .fr {
                                        float: right;
                                    }

                                    .fl {
                                        float: left;
                                    }

                                    .side {
                                        /*float: left;
                                        width: 100%;
                                        clear: both;*/
                                        padding-left: 1%;
                                    }

                                    .trk-info {
                                        font-size: 16px;
                                        margin-bottom: 20px;
                                    }

                                    .it {
                                        padding-bottom: 30px;
                                        position: relative;
                                    }

                                    .list {
                                        list-style: none;
                                    }

                                    li.it {
                                        list-style: none;
                                    }

                                    .it:before {
                                        content: '';
                                        border-left: 1px solid #e1e1e1;
                                        width: 1px;
                                        height: 76px;
                                        position: absolute;
                                        top: 0;
                                        left: 155px;
                                    }

                                    .trk-history .date-time {
                                        width: 140px;
                                        float: left;
                                    }

                                    .statuss {
                                        float: right;
                                        width: calc(100% - 140px);
                                        width: -webkit-calc(100% - 140px);
                                        position: relative;
                                        padding-left: 50px;
                                        font-size: 16px;
                                    }

                                        .statuss .ico {
                                            display: block;
                                            width: 30px;
                                            height: 30px;
                                            line-height: 30px;
                                            border: 1px solid #707070;
                                            background: #fff;
                                            text-align: center;
                                            border-radius: 50%;
                                            position: absolute;
                                            top: 0;
                                            left: 0;
                                            color: #707070;
                                        }

                                        .statuss.ok .ico {
                                            color: #8dc63f;
                                            border-color: #8dc63f;
                                        }

                                    .fa-check:before {
                                        content: "\f00c";
                                    }

                                    .clear:after, .all:after, .collapse .collapse-heading:after, .collapse .collapse-body:after, #header:after, #main-wrap:after, #footer:after, .quick-order-box .form-row:after {
                                        content: "";
                                        display: table;
                                        clear: both;
                                    }

                                    .m-color {
                                        color: #f36f21;
                                    }

                                    .tracking-btn:hover {
                                        background: #496872;
                                    }

                                    .submit-form {                                        
                                        line-height: 10px;
                                        padding: 10px;
                                        border-radius: 20px;
                                        background-color: #fa4659;
                                        color: #fff;
                                        z-index: 1;
                                        text-decoration: none;
                                        font-weight: bold;
                                        float: left;
                                        text-align: center;
                                        transition: .15s;
                                        margin-top: 5px;
                                        margin-right: 5px;
                                    }
                                </style>
                                <div class="grid-row" style="float: left; width: 75%; text-align: left; margin: 30px 0;">
                                    <aside class="side trk-info fr">
                                        <asp:Literal ID="ltrSmallpackageInfo" runat="server"></asp:Literal>
                                    </aside>
                                    <aside class="side trk-history fl">
                                        <ul class="list">
                                            <asp:Literal ID="ltrTrack" runat="server"></asp:Literal>
                                        </ul>
                                    </aside>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </main>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.txtsearchfield').on("keypress", function (e) {
                if (e.keyCode == 13) {
                    $("#<%=btnSearch.ClientID%>").click();
                }
            });
        });
    </script>
</asp:Content>
