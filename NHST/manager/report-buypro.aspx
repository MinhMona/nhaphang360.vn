<%@ Page Title="Thống kê lợi nhuận mua hộ" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="report-buypro.aspx.cs" Inherits="NHST.manager.report_buypro" %>

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
                    <h4 class="title no-margin" style="display: inline-block;">Thống kê lợi nhuận mua hộ</h4>
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
                                                <asp:TextBox runat="server" ID="txtdatefrom" type="text" class="datetimepicker from-date"></asp:TextBox>
                                                <label>Từ ngày</label>
                                            </div>
                                            <div class="input-field col s6 m4 l5">
                                                <asp:TextBox runat="server" ID="txtdateto" type="text" class="datetimepicker to-date"></asp:TextBox>
                                                <label>Đến ngày</label>
                                                <span class="helper-text"
                                                    data-error="Vui lòng chọn ngày bắt đầu trước"></span>
                                            </div>
                                            <div class="input-field col s12 m4 l2">
                                                <a href="javascript:;" class="btn" id="btnFilter">Xem thống kê</a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row section" style="display:none">
                                <div class="col s12 m6">
                                    <div class="chart-wrap">
                                        <canvas id="donate-chart" height="150"></canvas>
                                    </div>
                                </div>
                            </div>
                            <div class="row section ">
                                <div class="input-field col s12 m4 l2">
                                    <a href="javascript:;" style="background-color: green" class="btn" id="btnExport">Xuất thống kê</a>
                                </div>
                                <div class="col s12">
                                    <div class="responsive-tb">
                                        <table class="table highlight tb-border ad-rp-checkout">
                                            <thead>
                                                <tr class="teal darken-4">
                                                    <th class="white-text">Mã đơn hàng</th>
                                                    <th class="white-text">Username</th>
                                                    <th class="white-text">Tổng tiền</th>
                                                    <th class="white-text">Tiền mặc cả</th>
                                                    <th class="white-text">Tiền lời</th>
                                                    <th class="white-text">Tiền mua thật</th>
                                                    <th class="white-text">Tiền hàng </br> trên Web</th>
                                                    <th class="white-text">Ship TQ</th>
                                                    <th class="white-text">Phí vận chuyển</th>
                                                    <th class="white-text">Phí mua hàng</th>
                                                    <th class="white-text">Phí giao hàng</th>
                                                    <th class="white-text">Phí kiểm đếm</th>
                                                    <th class="white-text">Phí đóng gỗ</th>
                                                    <th class="white-text">Phí bảo hiểm</th>
                                                    <th class="white-text">Trạng thái</th>
                                                    <th class="white-text">Ngày lên đơn</th>
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
    <!-- END: Page Main-->
    <asp:Button runat="server" ID="buttonFilter" OnClick="btnFilter_Click" Style="display: none" UseSubmitBehavior="false" />
    <asp:Button runat="server" ID="buttonExport" OnClick="btnExport_Click" Style="display: none" UseSubmitBehavior="false" />
    <asp:HiddenField runat="server" ID="hdfDataChart" />
    <script>
        $('#btnFilter').click(function () {
            $(<%=buttonFilter.ClientID%>).click();
        });
        $('#btnExport').click(function () {
            $(<%=buttonExport.ClientID%>).click();
        });
        var countryRevenueChartCTX = $("#donate-chart");
        var ctx = document.getElementById('donate-chart').getContext('2d');

        var orange_yellow_gradient = ctx.createLinearGradient(0, 0, 0, 600);
        orange_yellow_gradient.addColorStop(0.2, 'rgba(255, 196, 11, 1)');
        orange_yellow_gradient.addColorStop(1, 'rgba(255, 228, 51, 1)');

        var red_orange_gradient = ctx.createLinearGradient(0, 0, 0, 600);
        red_orange_gradient.addColorStop(0.2, 'rgba(224, 7, 10, 1)');
        red_orange_gradient.addColorStop(1, 'rgba(252, 244, 212, 1)');

        var green_cyan_gradient = ctx.createLinearGradient(0, 0, 0, 600);
        green_cyan_gradient.addColorStop(0.2, 'rgba(204, 245, 191, 1)');
        green_cyan_gradient.addColorStop(1, 'rgba(7, 230, 226, 1)');

        var purple_cyan_gradient = ctx.createLinearGradient(0, 0, 0, 600);
        purple_cyan_gradient.addColorStop(0.2, 'rgba(123, 31, 162, 1)');
        purple_cyan_gradient.addColorStop(1, 'rgba(181, 131, 202, 1)');

        var blue_cyan_gradient = ctx.createLinearGradient(0, 0, 0, 600);
        blue_cyan_gradient.addColorStop(0.2, 'rgba(25, 128, 210, 1)');
        blue_cyan_gradient.addColorStop(1, 'rgba(149, 191, 224, 1)');


        var pink_cyan_gradient = ctx.createLinearGradient(0, 0, 0, 600);
        pink_cyan_gradient.addColorStop(0.2, 'rgba(236, 11, 94, 1)');
        pink_cyan_gradient.addColorStop(1, 'rgba(228, 130, 166, 1)');

        var orange_cyan_gradient = ctx.createLinearGradient(0, 0, 0, 600);
        orange_cyan_gradient.addColorStop(0.2, 'rgba(246, 67, 2, 1)');
        orange_cyan_gradient.addColorStop(1, 'rgba(234, 162, 135, 1)');

        var greenb_cyan_gradient = ctx.createLinearGradient(0, 0, 0, 600);
        greenb_cyan_gradient.addColorStop(0.2, 'rgba(46, 241, 10, 1)');
        greenb_cyan_gradient.addColorStop(1, 'rgba(152, 234, 137, 1)');

        var blueb_cyan_gradient = ctx.createLinearGradient(0, 0, 0, 600);
        blueb_cyan_gradient.addColorStop(0.2, 'rgba(128, 176, 234, 1)');
        blueb_cyan_gradient.addColorStop(1, 'rgba(180, 204, 234, 1)');

        var yellowb_cyan_gradient = ctx.createLinearGradient(0, 0, 0, 600);
        yellowb_cyan_gradient.addColorStop(0.2, 'rgba(208, 204, 90, 1)');
        yellowb_cyan_gradient.addColorStop(1, 'rgba(187, 185, 123, 1)');

        var redb_cyan_gradient = ctx.createLinearGradient(0, 0, 0, 600);
        redb_cyan_gradient.addColorStop(0.2, 'rgba(208, 204, 90, 1)');
        redb_cyan_gradient.addColorStop(1, 'rgba(187, 185, 123, 1)');


        var hover_gradient = ctx.createLinearGradient(0, 0, 0, 600);
        hover_gradient.addColorStop(0.6, '#021925');
        hover_gradient.addColorStop(0.3, '#0b444c');

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
                text: ['Thống kê lợi nhuận mua hàng hộ'],
                fontSize: '16'
            },
            hover: {
                mode: "label"
            },
            scales: {
                xAxes: [
                    {
                        display: true,
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
            countryRevenueChartData.datasets[0].backgroundColor[0] = green_cyan_gradient;
            countryRevenueChartData.datasets[0].backgroundColor[1] = red_orange_gradient;
            countryRevenueChartData.datasets[0].backgroundColor[2] = orange_yellow_gradient;
            countryRevenueChartData.datasets[0].backgroundColor[3] = purple_cyan_gradient;
            countryRevenueChartData.datasets[0].backgroundColor[4] = blue_cyan_gradient;
            countryRevenueChartData.datasets[0].backgroundColor[5] = pink_cyan_gradient;
            countryRevenueChartData.datasets[0].backgroundColor[6] = orange_cyan_gradient;
            countryRevenueChartData.datasets[0].backgroundColor[7] = greenb_cyan_gradient;
            countryRevenueChartData.datasets[0].backgroundColor[8] = blueb_cyan_gradient;
            countryRevenueChartData.datasets[0].backgroundColor[9] = yellowb_cyan_gradient;
            countryRevenueChartData.datasets[0].backgroundColor[10] = redb_cyan_gradient;
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
        window.onload = function () {
            var countryRevenueChart = new Chart(countryRevenueChartCTX, countryRevenueChartConfig);
        };
    </script>
</asp:Content>

