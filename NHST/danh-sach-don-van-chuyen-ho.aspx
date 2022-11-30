<%@ Page Language="C#" MasterPageFile="~/UserMasterNew.Master" AutoEventWireup="true" CodeBehind="danh-sach-don-van-chuyen-ho.aspx.cs" Inherits="NHST.danh_sach_don_van_chuyen_ho1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
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
                                        <h4>DANH SÁCH ĐƠN HÀNG VẬN CHUYỂN HỘ</h4>
                                    </div>
                                </div>
                                <div class="col s12 map-view" style="display:none">
                                    <div class="map-container">
                                        <div class="map-view-custom" id="map"></div>
                                    </div>
                                </div>
                                <div class="col s12">
                                    <div class="order-list-info">
                                        <div class="total-info mb-2">
                                            <div class="row section">
                                                <div class="col s12">
                                                    <a href="javascript:;" class="btn" id="filter-btn">Bộ lọc</a>
                                                    <div class="filter-wrap mb-2">
                                                        <div class="row">
                                                            <div class="input-field col s12 l6">
                                                                <asp:TextBox ID="txtOrderCode" placeholder="" runat="server"></asp:TextBox>
                                                                <%--  <input id="search_name" type="text" class="validate">--%>
                                                                <label for="search_name">
                                                                    <span>Nhập mã vận
                                                                    đơn</span></label>
                                                            </div>
                                                            <div class="input-field col s12 l6">
                                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Value="-1" Text="Tất cả"></asp:ListItem>
                                                                    <asp:ListItem Value="0" Text="Đơn hủy"></asp:ListItem>
                                                                    <asp:ListItem Value="1" Text="Chờ duyệt"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="Đã duyệt"></asp:ListItem>
                                                                    <asp:ListItem Value="3" Text="Đang xử lý"></asp:ListItem>
                                                                    <asp:ListItem Value="4" Text="Đã về kho TQ"></asp:ListItem>
                                                                    <asp:ListItem Value="5" Text="Đã về kho VN"></asp:ListItem>
                                                                    <asp:ListItem Value="6" Text="Khách đã thanh toán"></asp:ListItem>
                                                                    <asp:ListItem Value="7" Text="Đã hoàn thành"></asp:ListItem>
                                                                </asp:DropDownList>
                                                                <label for="status">Trạng thái</label>
                                                            </div>
                                                            <div class="input-field col s6 l6">
                                                                <asp:TextBox runat="server" placeholder="" CssClass="datetimepicker from-date" ID="FD"></asp:TextBox>
                                                                <label>Từ ngày</label>
                                                            </div>
                                                            <div class="input-field col s6 l6">
                                                                <asp:TextBox runat="server" placeholder="" ID="TD" CssClass="datetimepicker to-date"></asp:TextBox>
                                                                <label>Đến ngày</label>
                                                                <span class="helper-text"
                                                                    data-error="Vui lòng chọn ngày bắt đầu trước"></span>
                                                            </div>
                                                            <div class="col s12 right-align">
                                                                <asp:Button ID="btnSear" runat="server" CssClass="btn" OnClick="btnSear_Click" Text="LỌC TÌM KIẾM" />
                                                                <%-- <a class="btn ">Lọc kết quả</a>--%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="responsive-tb">
                                                        <table class="table   highlight bordered  centered bordered mt-2">
                                                            <thead>
                                                                <tr>
                                                                    <th>ID</th>
                                                                    <th>Số kiện</th>
                                                                    <th>Tổng tiền</th>
                                                                    <th>Tổng trọng lượng</th>
                                                                    <th class="tb-date">Ngày đặt</th>
                                                                    <th>Trạng thái</th>
                                                                    <th>Danh sách mã vận đơn</th>
                                                                    <th>Thao tác</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody>
                                                                <asp:Literal ID="ltr" runat="server" EnableViewState="false"></asp:Literal>

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
                </div>
            </div>
        </div>
    </div>
    <script src="https://maps.googleapis.com/maps/api/js?v=3&key=AIzaSyA-jM_HB6qmua59KRiq2eF6NgKEPr4SumU"></script>
    <!-- <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyA-jM_HB6qmua59KRiq2eF6NgKEPr4SumU&callback=initMap"
    async defer></script> -->
    <script src="/App_Themes/UserNew45/assets/js/markerwithlabel_packed.js"></script>
    <script>
        var map;
        var warehouses = [{
            name: 'Kho Hà Nội',
            lat: 21.027763,
            lng: 105.834160,
            package: [
                {
                    code: '33323212',
                    status: 'Đã về kho đích',
                    classColor: 'transported'
                },
                {
                    code: '65477214',
                    status: 'Đang vận chuyển',
                    classColor: 'being-transport'
                },
                {
                    code: '39594574',
                    status: 'Mới tạo',
                    classColor: 'new'
                }
            ]
        }, {
            name: 'Kho Nanning TQ',
            lat: 22.821930,
            lng: 108.318100,
            package: [
                {
                    code: '221141',
                    status: 'Đã về kho đích',
                    classColor: 'transported'
                },
                {
                    code: '7878984',
                    status: 'Đang vận chuyển',
                    classColor: 'being-transport'
                }
            ]
        }, {
            name: 'Kho Đà Nẵng',
            lat: 16.054407,
            lng: 108.202164,
            package: [
                {
                    code: '33323212',
                    status: 'Đã về kho đích',
                    classColor: 'transported'
                },
                {
                    code: '65477214',
                    status: 'Đang vận chuyển',
                    classColor: 'being-transport'
                },
                {
                    code: '39594574',
                    status: 'Mới tạo',
                    classColor: 'new'
                }, {
                    code: '65477214',
                    status: 'Đang vận chuyển',
                    classColor: 'being-transport'
                },
                {
                    code: '39594574',
                    status: 'Mới tạo',
                    classColor: 'new'
                }
            ]
        }];
        function initMap() {
            map = new google.maps.Map(document.getElementById('map'), {
                center: { lat: warehouses[0].lat, lng: warehouses[0].lng },
                zoom: 6,
                zoomControl: true
            });
            //Huong chuyen dong

            var lineSymbol = {
                path: google.maps.SymbolPath.CIRCLE,
                scale: 5,
                strokeColor: 'red'
            };


            setMarkerAll(map);
        }


        function animateCircle(line) {
            var count = 0;
            window.setInterval(function () {
                count = (count + 1) % 200;

                var icons = line.get('icons');
                icons[0].offset = (count / 2) + '%';
            }, 20);
                line.set('icons', icons);
        }
        function setMarkerAll(map) {
            for (var i = 0; i < warehouses.length; i++) {
                var data = warehouses[i];
                setMarkers(map, data, i)
            }
        }
        function setMarkers(map, data, i) {
            var contentString = this.buildHtmlInfoWindow(data);

            var infowindow = new google.maps.InfoWindow({
                maxWidth: 300
            });
            infowindow.setContent(contentString);
            // Adds markers to the map.
            // Define image icon
            var image = {
                url: '/App_Themes/UserNew45/assets/images/icon/warehouse.png',
                // This marker is 24 pixels wide by 24 pixels high.
                size: new google.maps.Size(24, 24),
                // The origin for this image is (0, 0).
                origin: new google.maps.Point(0, 0),
                // The anchor for this image is the base of the flagpole at (32, 0).
                anchor: new google.maps.Point(24, 5),
                labelOrigin: new google.maps.Point(0, -5)
            };
            // Shapes define the clickable region of the icon. The type defines an HTML
            // <area> element 'poly' which traces out a polygon as a series of X,Y points.
            // The final coordinate closes the poly by connecting to the first coordinate.
            var shape = {
                coords: [1, 1, 1, 20, 18, 20, 18, 1],
                type: 'poly'
            };

            var marker = new MarkerWithLabel({
                position: { lat: data.lat, lng: data.lng },
                map: map,
                animation: google.maps.Animation.DROP,
                icon: image,
                shape: shape,
                title: data.name,
                zIndex: i,
                labelContent: data.package.length.toString(),
                labelAnchor: new google.maps.Point(0, -5),
                labelClass: "label-count",
                labelInBackground: true
                // label: {
                // text: data.package.length.toString(),
                // color: "#eb3a44",
                // fontSize: "16px",
                // fontWeight: "bold"
                // }
            });
            marker.addListener('click', function () {
                infowindow.open(map, marker);
            });

            infowindow.addListener('domready', function () {
                var infoWindowContent = document.querySelectorAll('.gm-style-iw');
                infoWindowContent.forEach(function (element) {


                    element.addEventListener('mouseover', function () {

                        this.parentNode.parentNode.parentNode.style.zIndex = "99999";
                    });
                    element.addEventListener('mouseout', function () {
                        this.parentNode.parentNode.parentNode.style.zIndex = "1";
                    });
                });
            });
            // marker.addListener('mouseout', function() {
            //     infowindow.close(map, marker);
            // }); 

            google.maps.event.addDomListener(window, 'load', infowindow.open(map, marker));
        }


        function buildHtmlInfoWindow(data) {
            var listPackage = [];
            for (var i = 0; i < data.package.length; i++) {
                var package = '<div class="package">' +
                    'Mã <span class="bold code red-text">' + data.package[i].code + '</span>' +
                    '</div>';
                listPackage.push(package);
            }

            var joinString = listPackage.join(' ');
            var content = '<div class="content">' +
                '<p class="name-warehouse">' + data.name + '</p>' +
                '<p>Các mã vận đơn hiện có:</p>' +
                joinString +
                '</div>';

            return content;
        }
        google.maps.event.addDomListener(window, 'load', initMap);

    </script>
</asp:Content>
