<%@ Page Title="Thống kê tiền nạp - rút" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="Report-recharge.aspx.cs" Inherits="NHST.manager.Report_recharge" %>

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
                    <h4 class="title no-margin" style="display: inline-block;">Thống kê tiền nạp - rút</h4>
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
                                            <div class="input-field col s6 m2 l2">
                                                <asp:TextBox ID="search_name" name="txtSearchName" type="text" onkeypress="myFunction()" runat="server" />
                                                <label for="search_name"><span>Username</span></label>
                                            </div>
                                            <div class="input-field col s6 m2 l2">
                                                <asp:ListBox ID="ddlBank" runat="server" name="bank" class=""></asp:ListBox>
                                                <label>Bank</label>
                                            </div>
                                            <div class="input-field col s6 m2 l3">
                                                <asp:TextBox runat="server" ID="rdatefrom" type="text" class="datetimepicker from-date" placeholder=""></asp:TextBox>
                                                <label>Từ ngày</label>
                                            </div>
                                            <div class="input-field col s6 m2 l3">
                                                <asp:TextBox runat="server" ID="rdateto" type="text" class="datetimepicker from-date" placeholder=""></asp:TextBox>
                                                <label>Đến ngày</label>
                                                <span class="helper-text" data-error="Vui lòng chọn ngày bắt đầu trước"></span>
                                            </div>
                                            <div class="input-field col s12 m2 l2">
                                                <a href="javascript:;" class="btn btnFilter">Lọc kết quả</a>
                                            </div>
                                            <asp:Button ID="btnFilter" runat="server" CssClass="btn primary-btn" Text="Xem" OnClick="btnFilter_Click" Style="display: none"></asp:Button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row section">
                                <div class="col s12 m6">
                                    <div class="chart-wrap">
                                        <canvas id="donate-chart" height="150"></canvas>
                                    </div>
                                </div>
                            </div>
                            <div class="row section">
                                <div class="col s12">
                                    <h5 class="black-text font-weight-700">Danh sách nạp tiền</h5>
                                    <div class="responsive-tb">
                                        <div class="input-field1">
                                            <a href="javascript:;" class="btn" id="btnExport1">Xuất thống kê</a>
                                        </div>
                                        <table class="table highlight tb-border ad-rp-checkout">
                                            <thead>
                                                <tr class="teal darken-4">
                                                    <th class="white-text">ID</th>
                                                    <th class="white-text">Username</th>
                                                    <th class="white-text">Số tiền</th>
                                                    <th class="white-text">Ngân hàng</th>
                                                    <th class="white-text">Trạng thái</th>
                                                    <th class="white-text">Ngày tạo</th>
                                                    <th class="white-text">Người tạo</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="pagi-table float-right mt-2">
                                        <%this.DisplayHtmlStringPagingA();%>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>
                            </div>
                            <div class="row section">
                                <div class="col s12">
                                    <h5 class="black-text font-weight-700">Danh sách rút tiền</h5>
                                    <div class="responsive-tb">
                                         <div class="input-field1">
                                            <a href="javascript:;" class="btn" id="btnExport2">Xuất thống kê</a>
                                        </div>
                                        <table class="table highlight tb-border ad-rp-checkout">
                                            <thead>
                                                <tr class="teal darken-4">
                                                    <th class="white-text">ID</th>
                                                    <th class="white-text">Username</th>
                                                    <th class="white-text">Số tiền</th>                             
                                                    <th class="white-text">Trạng thái</th>
                                                    <th class="white-text">Ngày tạo</th>      
                                                    <th class="white-text">Người duyệt</th>      
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Literal ID="ltrWithDraw" runat="server" EnableViewState="false"></asp:Literal>
                                            </tbody>
                                        </table>
                                    </div>
                                    <div class="pagi-table float-right mt-2">
                                        <%this.DisplayHtmlStringPagingB();%>
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
    <asp:HiddenField  runat="server" ID="hdfDataChart"/> 
       <asp:Button runat="server" ID="buttonExport1" OnClick="btnExport1_Click" Style="display: none" UseSubmitBehavior="false" />
   <asp:Button runat="server" ID="buttonExport2" OnClick="btnExport2_Click" Style="display: none" UseSubmitBehavior="false" />
    <!-- END: Page Main-->

    <script>

        $('.btnFilter').click(function () {

            $('#<%=btnFilter.ClientID%>').click();
        });
        $('#btnExport1').click(function () {
            $(<%=buttonExport1.ClientID%>).click();
        });
        $('#btnExport2').click(function () {
            $(<%=buttonExport2.ClientID%>).click();
        });
        window.onload = function () {
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
                    text: ['Biểu đồ thống kê tiền nạp '],
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
                countryRevenueChartData.datasets[0].backgroundColor[0] = green_cyan_gradient;
                countryRevenueChartData.datasets[0].backgroundColor[1] = red_orange_gradient;
                countryRevenueChartData.datasets[0].backgroundColor[2] = orange_yellow_gradient;
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
     <style>
        .input-field1 {
            position: absolute;
            top: -6px;
            left: 200px;
        }
        .black-text {
            margin-bottom: 30px;
        }
        .row .col.s12 {
            position: relative;
        }
    </style>
</asp:Content>