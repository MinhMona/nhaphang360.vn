<%@ Page Title="Thống kê đơn hàng" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="Report-Orders.aspx.cs" Inherits="NHST.manager.Report_Orders" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Thống kê đơn hàng</h4>
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
                                                <asp:TextBox runat="server" ID="rdatefrom" type="text" CssClass="datetimepicker from-date"></asp:TextBox>
                                                <label>Từ ngày</label>
                                            </div>
                                            <div class="input-field col s6 m4 l5">
                                                <asp:TextBox runat="server" ID="rdateto" type="text" CssClass="datetimepicker to-date"></asp:TextBox>
                                                <label>Đến ngày</label>                                             
                                            </div>
                                            <div class="input-field col s12 m4 l2">
                                                <a href="javascript:;" class="btn btnFilter">Xem thống kê</a>
                                            </div>
                                            <asp:Button ID="btnFilter" runat="server" CssClass="btn primary-btn" Text="Xem" OnClick="btnFilter_Click" Style="display:none"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row section">
                                <div class="col s12 m4">
                                    <div class="card report-all-wrap">
                                        <div class="card-move-up waves-effect waves-block waves-light">
                                            <div class="move-up white darken-4">
                                                <div>
                                                    <span class="chart-title black-text">Đơn hàng<span id="name-class"></span></span>
                                                    <div class="chart-revenue text-darken-2 grey-text" style="background-color: #e8e8e8 !important;">
                                                        <p class="chart-revenue-per mb-1">Tổng tất cả</p>
                                                        <p class="chart-revenue-total"><asp:Label runat="server" ID="lbTongTien"></asp:Label> VNĐ</p>
                                                    </div>
                                                </div>
                                                <div class="revenue-line-chart-wrapper">
                                                    <canvas id="order-line-chart" height="180"></canvas>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col s12 m8">
                                    <div class="card report-all-wrap">
                                        <div class="card-move-up waves-effect waves-block waves-light">
                                            <div class="move-up white darken-4">
                                                <div class="revenue-line-chart-wrapper">
                                                    <canvas id="line-chart-order" height="103"></canvas>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row section ">
                           <div class="input-field col s12 m4 l2">
                                                <a href="javascript:;" class="btn btnExcel">Xuất Excel</a>
                                            </div>
                        <div class="col s12">
                            <div class="responsive-tb">
                                <table class="table highlight tb-border  ad-rp-checkout   ">
                                    <thead>
                                        <tr class="teal darken-4">
                                            <th class="white-text">Mã đơn hàng</th>
                                            <th class="white-text">Tên shop</th>
                                            <th class="white-text">Tổng tiền</th>
                                            <th class="white-text">Tổng tiền thật</th>
                                            <th class="white-text">Đặt cọc</th>
                                            <th class="white-text">Còn lại</th>
                                            <th class="white-text">Trạng thái</th>
                                            <th class="white-text">Ngày tạo</th>
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
    <asp:HiddenField runat="server" ID="hdfChartStatus"/>
    <asp:HiddenField runat="server" ID="hdfDataChartTotal" />
    <asp:Button runat="server" ID="btnExport" OnClick="btnExport_Click"  style="display:none"/>
    <telerik:RadScriptBlock runat="server">
        <script type="text/javascript">
            $('.btnFilter').click(function () {

                $('#<%=btnFilter.ClientID%>').click();
            });  
            $('.btnExcel').click(function () {

                $('#<%=btnExport.ClientID%>').click();
            });  
            function OnDateSelected(sender, eventArgs) {
                var date1 = sender.get_selectedDate();
                date1.setDate(date1.getDate() + 31);
                var datepicker = $find("<%= rdateto.ClientID %>");
                datepicker.set_maxDate(date1);
            }
            window.onload = function () {
                function createColor(ctx, start, stop) {
                    var gradientColor = ctx.createLinearGradient(0, 0, 0, 600);
                    gradientColor.addColorStop(0, start);
                    gradientColor.addColorStop(1, stop);
                    return gradientColor;
                }
                var ctx = document.getElementById("order-line-chart").getContext('2d');
                var donoughtDataChart = {
                    labels: [],
                    datasets: []
                };
                var donoughtData = $('#<%=hdfDataChartTotal.ClientID%>').val();
                if (donoughtData != null || donoughtData != "") {
                    var donoughtData2 = JSON.parse($('#<%=hdfDataChartTotal.ClientID%>').val());
                    donoughtDataChart.labels = JSON.parse(donoughtData2.labels)
                    donoughtDataChart.datasets = JSON.parse(donoughtData2.datasets);               

                }   
                var donoughtChart = new Chart(ctx, {
                    type: 'doughnut',
                    data: donoughtDataChart,
                    options: {
                        plugins: {
                            datalabels: {
                                formatter: (value, ctx) => {
                                    let datasets = ctx.chart.data.datasets;
                                    if (datasets.indexOf(ctx.dataset) === datasets.length - 1) {
                                        let sum = datasets[0].data.reduce((a, b) => a + b, 0);
                                        let percentage = Math.round((value / sum) * 100) + '%';
                                        return percentage;
                                    } else {
                                        return percentage;
                                    }
                                },
                                fontSize: 12
                            }
                        },
                        legend: {
                            display: true,
                            position: 'right',
                            labels: {
                                fontSize: 12,
                                padding: 20,

                            }
                        },
                        title: {
                            display: false,
                            text: 'Đơn hàng',
                            fontSize: 28,
                        },
                        tooltips: {
                            titleFontSize: 0,
                            callbacks: {
                                label: function (tooltipItem, data) {
                                    console.log(data);
                                    console.log(tooltipItem)
                                    if (parseInt(data.datasets[0].data[tooltipItem.index]) >= 1000) {
                                        return data.labels[tooltipItem.index] + ': ' + data.datasets[0].data[tooltipItem.index].toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + ' VNĐ';
                                    } else {
                                        return data.labels[tooltipItem.index] + ': ' + data.datasets[0].data[tooltipItem.index] + ' VNĐ';
                                    }

                                }
                            }
                        }

                    }//end of options

                });
                /*
                       Success rate convert by branch - Line Chart
                    */

                var rateConvertChartCTX = $("#line-chart-order");

                var rateConvertChartOption = {
                    plugins: {
                        datalabels: {
                            color: "#000",
                            clamp: false,
                            anchor: "end",
                            align: "top",
                            offset: 1,
                            formatter: (value, ctx) => {
                                return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")
                            },
                            fontSize: 14
                        }
                    },
                    responsive: true,
                    // maintainAspectRatio: false,
                    legend: {
                        display: false
                    },
                    hover: {
                        mode: "label"
                    },
                    scales: {
                        xAxes: [
                            {
                                display: true,
                                gridLines: {
                                    display: false,
                                    color: 'rgba(0,0,0,.5)'
                                },
                                ticks: {
                                    beginAtZero: true,
                                    fontColor: "#000",
                                    autoSkip: false
                                }
                            }
                        ],
                        yAxes: [
                            {
                                display: true,
                                fontColor: "#000",
                                gridLines: {
                                    display: false,
                                    color: 'rgba(0,0,0,.5)'
                                },
                                ticks: {
                                    beginAtZero: true,
                                    fontColor: "#000",

                                }
                            }
                        ]
                    },
                    tooltips: {
                        titleFontSize: 0,
                        mode: 'index',
                        intersect: false,
                        callbacks: {
                            beforeLabel: function (tooltipItem, data) {
                                return 'Tỉ lệ chuyển hóa thành công';
                            },
                            label: function (tooltipItems, data) {
                                console.log(tooltipItems);
                                return tooltipItems.xLabel + ': ' + tooltipItems.yLabel + ' đơn';
                            }

                        }
                    }
                };

                //var rateConvertChartData = {
                //    labels: ['Chờ đặt cọc', 'Hủy đơn hàng', 'Đã đặt cọc', 'Đã mua hàng', 'Tại kho TQ', 'Tại kho VN', 'Khách đã thanh toán', 'Đã hoàn thành'],
                //    datasets: [
                //        {
                //            label: "Số lượng",
                //            data: [65, 45, 50, 30, 63, 65, 45, 50],
                //            fill: false,
                //            lineTension: 0,
                //            borderColor: "#37474f",
                //            pointBorderColor: "#fff",
                //            pointBackgroundColor: "#009688",
                //            pointHighlightFill: "#000",
                //            pointHoverBackgroundColor: "#000",
                //            borderWidth: 4,
                //            pointBorderWidth: 1,
                //            pointHoverBorderWidth: 4,
                //            pointRadius: 4
                //        }
                //    ]
                //};

                var rateConvertChartData = {
                    labels: [],
                    datasets: []
                };

                var dataCountry = $('#<%=hdfChartStatus.ClientID%>').val();
                if (dataCountry != null || dataCountry != "") {
                    var dataCountry2 = JSON.parse($('#<%=hdfChartStatus.ClientID%>').val());
                    rateConvertChartData.labels = JSON.parse(dataCountry2.labels)
                    rateConvertChartData.datasets = JSON.parse(dataCountry2.datasets);  

                }
                var rateConvertChartConfig = {
                    type: "line",
                    options: rateConvertChartOption,
                    data: rateConvertChartData
                };
                var rateConvertChart = new Chart(rateConvertChartCTX, rateConvertChartConfig);
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>