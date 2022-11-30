<%@ Page Language="C#" MasterPageFile="~/UserMasterNew.Master" AutoEventWireup="true" CodeBehind="chi-tiet-don-hang-van-chuyen-ho.aspx.cs" Inherits="NHST.chi_tiet_don_hang_van_chuyen_ho1" %>

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
                                        <h4>Chi tiết đơn hàng ký gửi #0001</h4>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col s12 map-view">
                                <div class="map-container">
                                    <div class="map-view-custom" id="map"></div>
                                    <div class="map-view-info">
                                        <div class="view-info-wrap">
                                            <div class="info-top">
                                                <span class="bill">Mã đơn hàng: <span class="bold black-text code">445111235</span></span>
                                                <span class="status incoming">Đang vận chuyển</span>
                                            </div>
                                            <div class="info-bottom">
                                                <div class="from-to-location">
                                                    <asp:Literal runat="server" ID="ltrTQ"></asp:Literal>
                                                    <div class="icon">
                                                        <i class="material-icons">arrow_forward</i>
                                                    </div>
                                                    <asp:Literal runat="server" ID="ltrVN"></asp:Literal>
                                                </div>
                                                <div class="arrival-info" style="display: none">
                                                    <div class="arrival-note">
                                                        <p>Dự kiến tới nơi</p>
                                                        <span class="bold black-text">Jul 6th</span>
                                                    </div>
                                                    <div class="arrival-date">
                                                        <span class="time">10:30</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="col s12 order-detail-wrap mt-2">
                                <div class="card-panel">
                                    <div class="section history-list">
                                        <div class="title-header bg-dark-gradient">
                                            <h6 class="white-text ">Danh sách kiện hàng</h6>
                                        </div>
                                        <div class="child-fee">
                                            <div class="content-panel">
                                                <div class="responsive-tb">
                                                    <table class="table    highlight bordered  centered   ">
                                                        <thead>
                                                            <tr>
                                                                <th class="tb-date">Mã vận đơn</th>
                                                                <th>Loại hàng hóa</th>
                                                                <th>Số lượng</th>
                                                                <th>Số lượng thực tế</th>
                                                                <th>Cân nặng<br />
                                                                    Kg</th>
                                                                <th>KĐ</th>
                                                                <th>ĐG</th>
                                                                <th>BH</th>
                                                                <th>COD TQ (Tệ)</th>
                                                                <th class="tb-date">Ghi chú</th>
                                                                <th class="no-wrap">Trạng thái</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody class="list-product">
                                                            <asp:Literal ID="ltrListPackage" runat="server"></asp:Literal>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                                <div class="card-panel">
                                    <div class="section fee-order">
                                        <div class="title-header bg-dark-gradient">
                                            <h6 class="white-text ">Thông tin đơn hàng</h6>
                                        </div>
                                        <div class="child-fee">
                                            <div class="content-panel">
                                                <asp:Literal ID="ltrInfor" runat="server"></asp:Literal>

                                            </div>
                                        </div>

                                    </div>
                                    <hr />
                                    <div class="action-btn mt-2 mb-2 center-align">
                                        <asp:Literal ID="ltrBtn" runat="server"></asp:Literal>
                                        <%--  <a href="javascript:;" class="btn ">Thanh toán</a>--%>
                                        <a href="/danh-sach-don-van-chuyen-ho" class="btn ">Trở về</a>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField runat="server" ID="hdfLoadMap" />
    <asp:HiddenField ID="hdfProductList" runat="server" />
    <asp:Button ID="btnHuy" runat="server" Text="Hủy đơn" CssClass="btn btn-success btn-block pill-btn primary-btn main-btn hover custom-padding-display"
        OnClick="btnHuy_Click" Visible="false" />
    <asp:Button ID="btnPay" runat="server" Text="Thanh toán" CssClass="btn btn-success btn-block pill-btn primary-btn main-btn hover custom-padding-display"
        OnClick="btnPay_Click" Visible="false" />

    <script src="https://maps.googleapis.com/maps/api/js?v=3&key=AIzaSyA-jM_HB6qmua59KRiq2eF6NgKEPr4SumU"></script>
    <script src="/App_Themes/UserNew45/assets/js/markerwithlabel_packed.js"></script>
    <script type="text/javascript">

        function cancelOrder() {
            var c = confirm("Bạn muốn hủy đơn hàng này?");
            if (c == true) {
                $("#<%= btnHuy.ClientID%>").click();
            }
        }
        function payOrder() {
            var c = confirm("Bạn muốn thanh toán đơn hàng này?");
            if (c == true) {
                $("#<%= btnPay.ClientID%>").click();
            }
        }

        var array2 = $('#<%=hdfLoadMap.ClientID%>').val();
        console.log(array2);
        var data = JSON.parse(array2);
        console.log(data);


        var map;
        var warehouses = data;

        //var warehouses = [{
        //    name: 'Kho Hà Nội',
        //    lat: 21.027763,
        //    lng: 105.834160,
        //    package: [
        //        {
        //            code: '33323212',
        //            status: 'Đã về kho đích',
        //            classColor: 'transported'
        //        },
        //        {
        //            code: '65477214',
        //            status: 'Đang vận chuyển',
        //            classColor: 'being-transport'
        //        },
        //        {
        //            code: '39594574',
        //            status: 'Mới tạo',
        //            classColor: 'new'
        //        }
        //    ]
        //}, {
        //    name: 'Kho Nanning TQ',
        //    lat: 22.821930,
        //    lng: 108.318100,
        //    package: [
        //        {
        //            code: '221141',
        //            status: 'Đã về kho đích',
        //            classColor: 'transported'
        //        },
        //        {
        //            code: '7878984',
        //            status: 'Đang vận chuyển',
        //            classColor: 'being-transport'
        //        }
        //    ]
        //}, {
        //    name: 'Kho Đà Nẵng',
        //    lat: 16.054407,
        //    lng: 108.202164,
        //    package: [
        //        {
        //            code: '33323212',
        //            status: 'Đã về kho đích',
        //            classColor: 'transported'
        //        },
        //        {
        //            code: '65477214',
        //            status: 'Đang vận chuyển',
        //            classColor: 'being-transport'
        //        },
        //        {
        //            code: '39594574',
        //            status: 'Mới tạo',
        //            classColor: 'new'
        //        }, {
        //            code: '65477214',
        //            status: 'Đang vận chuyển',
        //            classColor: 'being-transport'
        //        },
        //        {
        //            code: '39594574',
        //            status: 'Mới tạo',
        //            classColor: 'new'
        //        }
        //    ]
        //}];
        function initMap() {


            map = new google.maps.Map(document.getElementById('map'), {
                center: { lat: parseFloat(warehouses[0].lat), lng: parseFloat(warehouses[0].lng) },
                zoom: 6,
                zoomControl: true
            });
            //Huong chuyen dong

            var lineSymbol = {
                path: google.maps.SymbolPath.CIRCLE,
                scale: 5,
                strokeColor: 'red'
            };

            // Create the polyline and add the symbol to it via the 'icons' property.


            var fromPos = document.getElementById('js-map-from');
            var toPos = document.getElementById('js-map-to');
            var fromToPosArr = [
                {
                    lat: parseFloat(fromPos.getAttribute('data-lat')),
                    lng: parseFloat(fromPos.getAttribute('data-lng')),
                },
                {
                    lat: parseFloat(toPos.getAttribute('data-lat')),
                    lng: parseFloat(toPos.getAttribute('data-lng'))
                }
            ];
            var transporting = new google.maps.Polyline({
                path: fromToPosArr,
                geodesic: true,
                strokeColor: "#0b444c",
                strokeWeight: 4,
                icons: [{
                    icon: lineSymbol,
                    offset: '100%'
                }],

                map: map
            });
            var line = new google.maps.Polyline({
                path: fromToPosArr,
                geodesic: true,
                strokeColor: "#FF0000",
                strokeOpacity: 0.3,
                strokeWeight: 4,
                map: map
            });
            animateCircle(transporting);

            setMarkerAll(map);
        }


        function animateCircle(line) {
            var count = 0;
            window.setInterval(function () {
                count = (count + 1) % 200;

                var icons = line.get('icons');
                icons[0].offset = (count / 2) + '%';
                line.set('icons', icons);
            }, 20);
        }
        function setMarkerAll(map) {
            for (var i = 0; i < warehouses.length; i++) {
                var data = warehouses[i];
                setMarkers(map, data, i)
            }
        }
        function setMarkers(map, data, i) {
            var contentString = this.buildHtmlInfoWindow(data);

            var lenght = 0;
            if (data.package != null)
                lenght = data.package.lenght;

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
                 position: { lat: parseFloat(data.lat), lng: parseFloat(data.lng) },
                map: map,
                animation: google.maps.Animation.DROP,
                icon: image,
                shape: shape,
                title: data.name,
                zIndex: i,
                labelContent: lenght.toString(),
                labelAnchor: new google.maps.Point(0, -5),
                labelClass: "label-count",
                // your desired CSS class
                labelInBackground: true
                // label: {
                // text: data.package.length.toString(),
                // color: "#eb3a44",
                // fontSize: "16px",
                // fontWeight: "bold"
                // }
            });
            marker.addListener('mouseover', function () {
                infowindow.open(map, marker);
            });
            marker.addListener('mouseout', function () {
                infowindow.close(map, marker);
            });
        }


        function buildHtmlInfoWindow(data) {
            var listPackage = [];

            if (data.package != null) {
                for (var i = 0; i < data.package.length; i++) {
                    var package = '<div class="package">' +
                        'Mã <span class="bold red-text code">' + data.package[i].code + '</span>'
                    '</div>';
                    listPackage.push(package);
                }
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
    <script>
        $(document).ready(function () {
            // Add message to chat   
            function enter_chat(source) {

                var message = $(this).prev().val();
                var d = new Date();
                var date = d.getDate();
                var month = d.getMonth();
                var year = d.getFullYear();
                var hour = d.getHours();
                var minute = d.getMinutes();

                function pad(number) {
                    return number < 10 ? '0' + number : number;
                }
                var dateTime = pad(date) + '/' + pad(month) + '/' + year + ' ' + pad(hour) + ':' + pad(minute);
                if (message != "") {
                    var html = '<div class="chat-text">' +
                        '<div class="date-time center-align">' + dateTime + '</div>' +
                        '<div class="text-content">' +
                        '<div class="content">' +
                        '<p>' + message + '</p>' +
                        '</div>' +
                        '</div>' +
                        '</div>'
                    var parents = $(this).parents('.chat-content-area');
                    parents.find(".chat:last-child .chat-body").append(html);
                    $(this).prev().val("");
                    parents.find(".chat-area").scrollTop(parents.find(".chat-area > .chats").height());
                }
            }

            $('#textarea2').val('');
            M.textareaAutoResize($('#textarea2'));
            $('#contact-chat .title-header').on('click', function () {
                $(this).parents('#contact-chat').toggleClass('hidden');
            });

            if ($(".customer-chat .chat-area").length > 0) {
                var ps_chat_area = new PerfectScrollbar('.customer-chat .chat-area', {
                    theme: "dark"
                });

            }
            $('.chat-footer .send').each(function () {
                $(this).on('click', enter_chat);
            });
            $('.chat-footer form.chat-input').each(function () {
                $(this).on('submit', enter_chat);
            });
        });





    </script>
</asp:Content>
