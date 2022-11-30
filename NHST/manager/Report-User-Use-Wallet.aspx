<%@ Page Title="Thống kê giao dịch" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="Report-User-Use-Wallet.aspx.cs" Inherits="NHST.manager.Report_User_Use_Wallet" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
                    <h4 class="title no-margin" style="display: inline-block;">Thống kê giao dịch</h4>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="col s12">
                <div class="card-panel">

                    <div class="order-list-info">
                        <div class="total-info">
                            <div class="row section">
                                <div class="col s12">
                                    <div class="filter filter-fix">
                                        <div class="row">
                                            <div class="input-field col s6 m4 l5">
                                                <asp:TextBox runat="server" ID="rdatefrom" type="text" class="datepicker from-date"></asp:TextBox>
                                                <label>Từ ngày</label>
                                            </div>
                                            <div class="input-field col s6 m4 l5">
                                                <asp:TextBox runat="server" ID="rdateto" type="text" class="datepicker to-date"></asp:TextBox>
                                                <label>Đến ngày</label>
                                                <span class="helper-text"
                                                    data-error="Vui lòng chọn ngày bắt đầu trước"></span>
                                            </div>
                                            <div class="input-field col s12 m4 l2">
                                                <a href="javascript:;" class="btn btnFilter">Xem thống kê</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row section">
                                <div class="col s12 m12">
                                    <div class="chart-wrap">
                                        <p id="js-date-filter">Thống kê :
                                            <asp:Literal runat="server" ID="ltrDateToDate"></asp:Literal></p>
                                        <h6>Tổng số tiền giao dịch: <span id="totalgd">
                                            <asp:Label runat="server" ID="lbTotalPrice"></asp:Label></span> VNĐ</h6>
                                        <canvas id="donate-chart" height="100"></canvas>
                                    </div>

                                </div>
                            </div>
                            <div class="row section">
                                <div class="col s12">
                                    <div class="responsive-tb">
                                        <table class="table highlight  tb-border   ">
                                            <thead>
                                                <tr class="teal darken-4">
                                                    <th class="white-text">Ngày giờ</th>
                                                    <th class="white-text tb-date">Nội dung</th>
                                                    <th class="white-text">Số tiền</th>
                                                    <th class="white-text tb-date">Loại giao dịch</th>
                                                    <th class="white-text">Số dư</th>
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
    <asp:Button ID="btnFilter" runat="server" CssClass="btn primary-btn" Text="Xem" Style="display: none" OnClick="btnFilter_Click"></asp:Button>
    <asp:HiddenField runat="server" ID="hdfDataChart" />
    <!-- END: Page Main-->
    <script>
        $('.btnFilter').click(function () {
            $('#<%=btnFilter.ClientID%>').click();
    });
        window.onload = function () {
            function createColor(ctx, start, stop) {
                var gradientColor = ctx.createLinearGradient(0, 0, 0, 600);
                gradientColor.addColorStop(0, start);
                gradientColor.addColorStop(1, stop);
                return gradientColor;
            }

            var ctx = document.getElementById('donate-chart').getContext('2d');

            var color = {
                'datcoc': createColor(ctx, 'rgba(255, 196, 11, 1)', 'rgba(255, 228, 51, 1)'),
                'chuyentien': createColor(ctx, 'rgba(224, 7, 10, 1)', 'rgba(252, 244, 212, 1)'),
                'ruttien': createColor(ctx, 'rgba(204, 245, 191, 1)', 'rgba(7, 230, 226, 1)'),
                'thanhtoan': createColor(ctx, 'rgba(11, 22, 191, 1)', 'rgba(142, 230, 111, 1)')
            }

            var countryRevenueChartCTX = $("#donate-chart");

            var hover_gradient = createColor(ctx, '#021925', '#0b444c');
            var countryRevenueChartOption = {
                responsive: true,
                // maintainAspectRatio: false,
                plugins: {
                    datalabels: {
                        color: "#000",
                        anchor: "end",
                        align: "top",
                        offset: 1,
                        formatter: (value, ctx) => {
                            return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")
                        },
                        fontSize: 14
                    }
                },
                legend: {
                    display: false
                },
                title: {
                    display: true,
                    text: ['Biểu đồ thống kê giao dịch '],
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
                                labelString: 'Loại giao dịch'
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
                                labelString: 'VNĐ'
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
                            console.log(tooltipItem)
                            if (parseInt(tooltipItem.yLabel) >= 1000) {
                                return data.labels[tooltipItem.index] + ': ' + tooltipItem.yLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + ' VNĐ';
                            } else {
                                return data.labels[tooltipItem.index] + ': ' + tooltipItem.yLabel + ' VNĐ';
                            }

                        }
                    }
                }
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
                countryRevenueChartData.datasets[0].hoverBackgroundColor = hover_gradient;
                countryRevenueChartData.datasets[0].hoverBorderWidth = 2;
                countryRevenueChartData.datasets[0].hoverBorderColor = hover_gradient;
                console.log(countryRevenueChartData);

            }
            var countryRevenueChartConfig = {
                type: "bar",
                options: countryRevenueChartOption,
                data: countryRevenueChartData
            };
            function updateTotalRevenue(data) {
                const reducer = (accumulator, currentValue) => accumulator + currentValue;
                var totalData = data.reduce(reducer).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
                return totalData + ' VNĐ';
            };
            var countryRevenueChart = new Chart(countryRevenueChartCTX, countryRevenueChartConfig);
        };
    </script>
</asp:Content>
