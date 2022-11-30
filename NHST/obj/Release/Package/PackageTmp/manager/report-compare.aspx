<%@ Page Title="Thống kê đơn hàng" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="report-compare.aspx.cs" Inherits="NHST.manager.report_compare" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/1.0.2/Chart.js" type="text/javascript"> </script>--%>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.2/Chart.min.js" type="text/javascript"> </script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.2/Chart.bundle.min.js" type="text/javascript"> </script>
    <style type="text/css">
        /* Chart.js */
        @-webkit-keyframes chartjs-render-animation {
            from {
                opacity: 0.99
            }

            to {
                opacity: 1
            }
        }

        @keyframes chartjs-render-animation {
            from {
                opacity: 0.99
            }

            to {
                opacity: 1
            }
        }

        .chartjs-render-monitor {
            -webkit-animation: chartjs-render-animation 0.001s;
            animation: chartjs-render-animation 0.001s;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">

        <h1 class="page-title">Thống kê đơn hàng</h1>
        <div class="cont900" data-css='{"margin-bottom": "20px"}'>
            <div class="pane-shadow filter-form" id="filter-form">
                <div class="grid-row">
                    <div class="grid-col-100">
                        <div class="lb">Trạng thái đơn hàng</div>
                        <asp:DropDownList ID="ddlFilter" runat="server" CssClass="form-control">
                            <asp:ListItem Value="1" Text="Mua hàng hộ"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Thanh toán hộ"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Vận chuyển hộ"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="grid-col-100 center-txt">
                        <a class="btn primary-btn" onclick="GetChar()">Xem</a>
                        <asp:Button ID="btnFilter" runat="server" CssClass="btn primary-btn" Text="Xem" Style="display: none" OnClick="btnFilter_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>

        <div id="canvas-holder" style="width: 100%">
            <div class="chartjs-size-monitor">
                <div class="chartjs-size-monitor-expand">
                    <div></div>
                </div>
                <div class="chartjs-size-monitor-shrink">
                    <div></div>
                </div>
            </div>
            <canvas id="chart-area" width="772" height="386" class="chartjs-render-monitor"></canvas>
        </div>


        <canvas id="myChart" width="200" height="200"></canvas>

        <script>

            $(document).ready(function () {
                GetChar();
            });

            function GetChar() {
                var t = $("#<%=ddlFilter.ClientID%>").val();
                 $('#chart-area').replaceWith('<canvas id="chart-area" width="100" height="100"></canvas>');
                //alert(t);
                //var data = 1;
                $.ajax({
                    type: "POST",
                    url: "/manager/report-compare.aspx/GetTotal",
                    data: "{data:'" + t + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {

                        var aData = JSON.parse(r.d);
                        if (aData.length > 0) {
                            var obj1 = [];
                            var obj2 = [];
                            var obj3 = [];

                            $.each(aData, function (inx, val) {
                                obj1.push(val.value);
                                obj2.push(val.color);
                                obj3.push(val.label);
                            });

                            var config = {
                                type: 'pie',
                                data: {
                                    datasets: [{
                                        data: obj1,
                                        backgroundColor: obj2,
                                        label: 'Dataset 1'
                                    }],
                                    labels: obj3
                                },
                                options: {
                                    responsive: true
                                }
                            };

                           



                            window.onload = function () {
                                var ctx = document.getElementById('chart-area').getContext('2d');
                                window.myPie = new Chart(ctx, config);
                            };
                        }
                        else {
                            alert("Chưa có đơn hàng");
                        }
                    }
                });
            }

           <%-- $(document).ready(function () {
                GetChar();
            });


            function GetChar() {
                var t = $("#<%=ddlFilter.ClientID%>").val();

                $.ajax({
                    type: "POST",
                    url: "/manager/report-compare.aspx/GetTotal",
                    data: "{data:'" + t + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess_,
                    error: OnErrorCall_
                });

                function OnSuccess_(response) {
                    var aData = JSON.parse(response.d);
                    var arr = [];
                    $.each(aData, function (inx, val) {
                        var obj = {};
                        obj.color = val.color;
                        obj.value = val.value;
                        obj.label = val.label;
                        arr.push(obj);
                    });
                    //var ctx = $("#myChart").get(0).getContext("2d");

                    var ctx = document.getElementById('myChart').getContext('2d');
                    window.myPie = new Chart(ctx, arr);

                    //var myPieChart = new Chart(ctx, {
                    //    type: 'pie',
                    //    data: arr
                    //});

                   // var myPieChart = new Chart(ctx).Pie(arr);
                }

                function OnErrorCall_(response) { }
            }--%>



</script>
    </main>

    <%-- </asp:Panel>--%>
    <%-- <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnFilter">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>--%>
    <telerik:RadScriptBlock runat="server">
    </telerik:RadScriptBlock>
    <script type="text/javascript">

        //var data = "testlname";
        //$.ajax({
        //    type: "POST",
        //    url: "/manager/report-compare.aspx/ChartjsTempData",
        //    data: "{data:'" + data + "'}",
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    success: function (r) {
        //        var da = r.d;
        //        var dataStrArr = da.split(",")
        //        var dataIntArr = [];
        //        dataStrArr.forEach(function (data, index, arr) {
        //            dataIntArr.push(+data);
        //        });

        //        new Chart(document.getElementById("bar-chart"), {
        //            type: 'bar',
        //            data: {
        //                labels: ["Africa", "Asia", "Europe", "Latin America", "North America"],
        //                datasets: [
        //                    {
        //                        label: "Population (millions)",
        //                        backgroundColor: ["#3e95cd", "#8e5ea2", "#3cba9f", "#e8c3b9", "#c45850"],
        //                        data: dataIntArr
        //                    }
        //                ]
        //            },
        //            options: {
        //                legend: { display: false },
        //                title: {
        //                    display: true,
        //                    text: 'Predicted world population (millions) in 2050'
        //                }
        //            }
        //        });
        //    }
        //});

        // pie chart data
        //var pieData = [
        //    {
        //        value: 20,
        //        color: "#878BB6"
        //    },
        //    {
        //        value: 40,
        //        color: "#4ACAB4"
        //    },
        //    {
        //        value: 10,
        //        color: "#FF8153"
        //    },
        //    {
        //        value: 30,
        //        color: "#FFEA88"
        //    }
        //];
        //// pie chart options
        //var pieOptions = {
        //    segmentShowStroke: false,
        //    animateScale: true
        //}
        //// get pie chart canvas
        //var countries = document.getElementById("countries").getContext("2d");
        //var myPieChart = new Chart(countries, {
        //    type: 'pie',
        //    data: pieData,
        //    options: pieOptions
        //});

        //var countries = document.getElementById("countries").getContext("2d");
        // draw pie chart
        //new Chart(countries).Pie(pieData, pieOptions);


        //$(document).ready(function () {
        //    var data = "123123";
        //    $.ajax({
        //        type: "POST",
        //        url: "/manager/report-compare.aspx/get",
        //        data: "{data:'" + data + "'}",
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (r) {
        //            var aData = r.d;
        //            var arr = [];
        //            $.each(aData, function (inx, val) {
        //                var obj = {};
        //                obj.color = val.color;
        //                obj.value = val.value;
        //                obj.label = val.label;
        //                arr.push(obj);
        //            });
        //            var ctx = $("#myChart").get(0).getContext("2d");
        //            var myPieChart = new Chart(ctx).Pie(arr);
        //        }
        //    });
        //});
    </script>
</asp:Content>
