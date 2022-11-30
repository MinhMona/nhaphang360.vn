<%@ Page Title="Thống kê số dư" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="Report-user-wallet.aspx.cs" Inherits="NHST.manager.Report_user_wallet" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Thống kê số dư</h4>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="col s12">
                <div class="card-panel">

                    <div class="order-list-info">
                        <div class="total-info">
                            <div class="row section">
                                <div class="col s12">
                                    <div class="filter">
                                        <div class="row">
                                            <div class="input-field col s12 m6 l4">

                                                <asp:DropDownList ID="ddlFilter" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="0" Text="Tất cả"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="User có số dư tài khoản"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="User không có số dư tài khoản"></asp:ListItem>
                                                </asp:DropDownList>
                                                <label>Loại user</label>

                                            </div>
                                            <div class="input-field col s12 m6 l4">
                                                <a href="javascript:;" class="btn btnFilter">Lọc kết quả</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row section">
                                <div class="col s12 m6">
                                    <div class="chart-wrap">
                                        <h5>Tổng số dư: <span id="totalsd"><asp:Literal runat="server" ID="lbTotalWallet"></asp:Literal> VNĐ</span></h5>
                                        <canvas id="donate-chart" height="150"></canvas>
                                    </div>

                                </div>
                            </div>
                            <div class="row section">
                                <div class="col s12">
                                    <div class="responsive-tb">
                                        <table class="table highlight tb-border  ad-rp-checkout   ">
                                            <thead>
                                                <tr class="teal darken-4">
                                                    <th class="white-text">ID</th>
                                                    <th class="white-text">Username</th>
                                                    <th class="white-text">Số dư</th>
                                                    <th class="white-text">Quyền hạn</th>
                                                    <th class="white-text">Trạng thái</th>
                                                    <th class="white-text">NV Kinh doanh</th>
                                                    <th class="white-text">NV Đặt hàng</th>
                                                    <th class="white-text">Ngày tạo</th>
                                                    <th class="white-text">Action</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Literal runat="server" ID="ltr"></asp:Literal>
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
                </div>
            </div>
        </div>
    </div>
     <asp:Button ID="btnFilter" style="display:none" runat="server" CssClass="btn btn-success" Text="Xem" OnClick="btnFilter_Click"></asp:Button>
    <asp:HiddenField runat="server" ID="hdfDataChart" />
    <!-- END: Page Main-->

    <script>
        $('.btnFilter').click(function () {

            $('#<%=btnFilter.ClientID%>').click();
        });  
        window.onload = function () {

            var countryRevenueChartCTX = $("#donate-chart");

            var countryRevenueChartOption = {
                responsive: true,
                // maintainAspectRatio: false,
                legend: {
                    display: false
                },
                title: {
                    display: true,
                    text: 'Biểu đồ số lượng user theo số dư',
                    fontSize: '16'
                },
                hover: {
                    mode: "label"
                },
                scales: {
                    xAxes: [
                        {
                            display: true,
                            scaleLabel: {
                                display: true,
                                labelString: 'Số dư'
                            },
                            gridLines: {
                                display: false
                            },
                            ticks: {
                                fontColor: "#000"

                            }
                        }
                    ],
                    yAxes: [
                        {
                            display: true,
                            fontColor: "#000",
                            scaleLabel: {
                                display: true,
                                labelString: 'User'
                            },
                            gridLines: {
                                display: false
                            },
                            ticks: {
                                beginAtZero: true,
                                fontColor: "#000",
                                callback: function (value, index, values) {
                                    if (parseInt(value) >= 1000) {
                                        return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                                    } else {
                                        return value;
                                    }
                                }
                            }

                        }
                    ]
                },
                tooltips: {
                    titleFontSize: 0,
                    callbacks: {
                        label: function (tooltipItem, data) {
                            console.log(data);
                            if (parseInt(tooltipItem.yLabel) >= 1000) {
                                return tooltipItem.xLabel + ': ' + tooltipItem.yLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + ' User';
                            } else {
                                return tooltipItem.xLabel + ': ' + tooltipItem.yLabel + ' User';
                            }

                        }
                    }
                }
            };

            var countryRevenueChartData = {
                labels: ["Lớn hơn 0", "Bằng 0", "1 triệu - 5 triệu", "5 triệu - 10 triệu"],
                datasets: [
                    {
                        label: "Số dư",
                        data: [98, 32, 12, 34],
                        backgroundColor: ["rgba(211, 47, 47, 0.5)", 'rgba(25, 118, 210,.5)', 'rgba(245, 124, 0,.5)', 'rgba(95,186,125,0.9)']
                    }
                ]
            };

            var countryRevenueChartData = {
                labels: [],
                datasets: []
            };
            var dataCountry = $('#<%=hdfDataChart.ClientID%>').val();
            if (dataCountry != null || dataCountry != "") {
                var dataCountry2 = JSON.parse($('#<%=hdfDataChart.ClientID%>').val());
                countryRevenueChartData.labels = JSON.parse(dataCountry2.labels)
                countryRevenueChartData.datasets = JSON.parse(dataCountry2.datasets);
                console.log(countryRevenueChartData);
            }

            var countryRevenueChartConfig = {
                type: "bar",
                options: countryRevenueChartOption,
                data: countryRevenueChartData
            };
            var countryRevenueChart = new Chart(countryRevenueChartCTX, countryRevenueChartConfig);
        };
    </script>
</asp:Content>

