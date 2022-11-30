<%@ Page Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="add-package-customer.aspx.cs" Inherits="NHST.manager.add_package_customer" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link rel="stylesheet" type="text/css" href="/App_Themes/AdminNew45/assets/js/lightgallery/css/lightgallery.min.css">
    <script src="/App_Themes/AdminNew45/assets/js/lightgallery/js/lightgallery-all.min.js" type="text/javascript"></script>
    <style>
        #mainordercode-list {
            float: left;
            list-style: none;
            margin-top: -3px;
            padding: 0;
            /* width: 190px;*/
            z-index: 9999;
            position: absolute;
        }

            #mainordercode-list li {
                padding: 10px;
                background: #f0f0f0;
                border-bottom: #bbb9b9 1px solid;
            }

                #mainordercode-list li:hover {
                    background: #ece3d2;
                    cursor: pointer;
                }

        li > img {
            width: 4%;
        }
    </style>
    <script src="https://code.jquery.com/jquery-2.1.1.min.js" type="text/javascript"></script>
    <style>
        .waves-button-input {
            color: #fff;
        }

        /*.isvekho {
            background-color: #26661a !important;
        }*/

        .select-bao {
            width: 700px;
        }

        .isvekho {
            background: #2154b0;
        }

        .package-item .table .isvekho td {
            color: #fff;
        }

        .isvekho td .tb-block .black-text {
            color: #fff !important;
        }

        .table thead tr th {
            min-width: 100px;
            color: #fff;
        }

        table.centered thead tr th {
            border: 1px solid #000;
            padding: 10px 5px;
        }

        table.centered tbody tr td {
            border: 1px solid #000;
            padding: unset;
        }

        /*#addPackage table tr td img {
            width: 5%;
        }*/

        [type="checkbox"] + span:not(.lever) {
            padding-left: 20px;
        }

        .select2-selection.select2-selection--single {
            height: 40px;
        }

        .search-name.input-field.no-margin > .select-wrapper {
            display: none;
        }

        .select-wrapper-hide {
            padding: 0 !important;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Gán kiện ký gửi cho khách</h4>
                </div>
            </div>
            <div class="list-staff col s12 section">
                <div class="list-table card-panel">
                    <div class="filter">
                        <div class="search-name input-field no-margin">
                            <asp:DropDownList ID="ddlBigpack" runat="server" CssClass="select2"
                                DataValueField="ID" DataTextField="Username">
                            </asp:DropDownList>
                        </div>
                        <div class="search-name input-field no-margin">
                            <input placeholder="Nhập mã vận đơn hoặc mã bao hàng" id="barcode-input" type="text"
                                class="barcode">
                            <div class="bg-barcode"></div>
                        </div>
                        <div class="labelled" style="display: none">
                            <div class="lb-block">
                                <span class="block orange lighten-4"></span>
                                <span>Kiện hàng chưa về TQ</span>
                            </div>
                            <div class="lb-block">
                                <span class="block green lighten-4"></span>
                                <span>Kiện hàng đã về TQ</span>
                            </div>
                            <div class="lb-block">
                                <span class="block cyan lighten-4"></span>
                                <span>Kiện hàng thất lạc</span>
                            </div>
                            <div class="lb-block">
                                <span class="block red lighten-4"></span>
                                <span>Kiện hàng đã hủy</span>
                            </div>
                            <div class="lb-block">
                                <span class="block grey lighten-4"></span>
                                <span>Kiện hàng đã cập nhật</span>
                            </div>
                        </div>
                    </div>
                    <div class="list-package" id="listpackage">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- END: Page Main-->
    <!-- BEGIN: Modal Add Bao Lớn -->
    <!-- Modal Structure -->

    <!-- END: Modal Add Bao Lớn -->

    <!-- BEGIN: Modal Add Mã kiện -->
    <!-- Modal Structure -->

    <!-- Check Product -->
    <div id="checkProduct" class="modal modal-small add-package">
        <div class="modal-hd">
            <span class="right"><i class="material-icons modal-close right-align">clear</i></span>
            <h4 class="no-margin center-align" id="mainOrderID"></h4>
        </div>
        <div class="modal-bd">
            <div class="row">
                <div class="input-field col s12 m12 hide" id="tblProduct">
                    <table class="table table-inside centered table-warehouse">
                        <thead>
                            <tr class="teal darken-4">
                                <th>Sản phẩm</th>
                                <th>Hình ảnh</th>
                                <th>Số lượng</th>
                            </tr>
                        </thead>
                        <tbody id="dataProduct">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <%--<div class="modal-ft">
            <div class="ft-wrap center-align">
                <a href="javascript:;" onclick="CheckAddTempCode()" class="modal-action btn modal-close waves-effect waves-green mr-2 submit-button">Thêm</a>
                <a href="javascript:;" class="modal-action btn orange darken-2 modal-close waves-effect waves-green ml-2">Hủy</a>
            </div>

        </div>--%>
    </div>

    <script type="text/javascript">

        $(document).ready(function () {
            $('.view-gallery').on('click', function () {
                var $this = $(this);
                var arr = [];
                var listImg = $this.parent().find('.list-img  img');
                console.log(listImg);
                $(listImg).each(function (i, element) {
                    var $src = $(element).attr('src');
                    arr[i] = {
                        src: $src,
                        thumb: $(element).attr('data-thumb') || $src
                    }
                });
                $this.lightGallery({
                    dynamic: true,
                    dynamicEl: arr,
                    download: false,
                    actualSize: false,
                    fullScreen: false,
                    autoplay: false,
                    share: false,
                    hideBarsDelay: 3000,
                });
            });

            $('.table-warehouse').on('click', '.select-trigger', function () {
                var content = $(this).parent().find('.dropdown-content');
                var dropDownTop = $(this).offset().top + $(this).outerHeight();
                content.css('top', dropDownTop + 'px');
                content.css('left', $(this).offset().left + 'px');
            });

            $('.bg-barcode').on('click', function () {
                alert('BarCode Open !');
            });
            $('body').on('click', '.img-block .delete', function (e) {
                $(this).parents('.img-block').remove();
            });

            $('body').on('change', 'input.tooltipped', function () {
                $(this).attr('data-tooltip', $(this).val());
            });





            $('.modal-small').on('keypress', function (e) {
                if (e.keyCode == 13) {
                    $(e.target).closest('.modal').find('.submit-button').click(); //Click submit button tren modal
                    //$(e.target).closest('.modal').modal('close');
                }
                else if (e.keyCode == 27) {
                    $(e.target).closest('.modal').modal('close');
                    $('#barcode-input').val('').focus();
                }
            })

            $('.modal-big').on('keypress', function (e) {
                if (e.keyCode == 13) {
                    $(e.target).closest('.modal').find('.submit-button').click(); //Click submit button tren modal
                    $(e.target).closest('.modal').modal('close');
                    $('#barcode-input').focus();
                }
            })

            //Enter ben trong tr update
            $('.list-package').on('keypress', 'input,select', function (e) {
                if (e.keyCode == 13) {
                    var $element = $(e.target);
                    $element.closest('.package-row').find('.updatebutton').click();
                    $('#barcode-input').focus().select();
                    return false;
                }
            });


            //F2  ben trong tr print barcode
            $('.list-package').on('keyup', 'input,select', function (e) {
                if (e.keyCode == 113) {
                    var $element = $(e.target);
                    $element.closest('.package-row').find('.printbarcode').click();
                    //$('#barcode-input').focus().select();
                }
            });
        });

        $(document).ready(function () {
            $('#barcode-input').focus();
            $('#barcode-input').keydown(function (e) {
                if (e.key === 'Enter') {
                    //getCodeNew
                    getCode($(this));
                    e.preventDefault();
                    return false;
                }
            });
            loadBigPackage(0);

            //$('#smmainordercode').keyup(function (e) {
            //    setTimeout(function (event) {
            //        var value = $('#smmainordercode').val();
            //        $.ajax({
            //            type: "POST",
            //            url: "/manager/add-package-customer.aspx/GetOrder",
            //            data: "{MainOrderCode:'" + value + "'}",
            //            contentType: "application/json; charset=utf-8",
            //            dataType: "json",
            //            success: function (msg) {
            //                if (msg.d != null) {
            //                    var data = JSON.parse(msg.d);
            //                    console.log(data);
            //                }
            //            }
            //        })
            //    }, 2000);

            //})


            function getCode(obj) {
                var bc = obj.val();
                if (isEmpty(bc)) {
                    alert('Vui lòng nhập mã bao hàng / mã vận đơn');
                }
                else {
                    $.ajax({
                        type: "POST",
                        url: "/manager/add-package-customer.aspx/GetListPackage",
                        data: "{barcode:'" + bc + "'}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (msg) {
                            if (msg.d != "none") {
                                var bi = JSON.parse(msg.d);
                                if (bi != "none") {
                                    var biType = bi.BigpackageType;
                                    var listsmallpackage = bi.Smallpackages;

                                    if (biType == 1) {
                                        if (listsmallpackage.length > 0) {
                                            for (var i = 0; i < listsmallpackage.length; i++) {
                                                //document.getElementById('audio2').play();
                                                var data = listsmallpackage[i];
                                                var UID = data.UID;
                                                var Username = data.Username;
                                                var Phone = data.Phone;
                                                var packageID = data.ID;
                                                var isExist = false;
                                                if ($(".package-row").length > 0) {
                                                    $(".package-row").each(function () {
                                                        var dt_packageID = $(this).attr("data-packageID");
                                                        if (packageID == dt_packageID) {
                                                            isExist = true;
                                                        }
                                                    });
                                                }

                                                var check = false;
                                                $(".package-wrap").each(function () {
                                                    if ($(this).attr("data-uid") == UID) {
                                                        check = true;
                                                    }
                                                })
                                                var pID = UID;
                                                if (data.OrderTypeInt != 2) {
                                                    if (isExist == false) {
                                                        var idpack = "bc-" + data.BarCode + "-" + packageID;
                                                        if (check == false) {
                                                            var html = '';
                                                            html += "<div class=\"package-wrap accent-2\" id=\"" + pID + "\" data-uid=\"" + UID + "\">";
                                                            html += "<div class=\"row\">";
                                                            html += "<div class=\"col s12\">";
                                                            html += "<div class=\"list-package\">";
                                                            html += "<div class=\"package-item pb-2\">";
                                                            html += "<div class=\"wrap-top-action\">";
                                                            html += "<div class=\"owner\">";
                                                            html += "<span>" + Username + "</span> | <span>" + Phone + "</span>";
                                                            html += "</div>";
                                                            html += "<div class=\"action-all\">";
                                                            html += "<a href=\"javascript:;\" onclick=\"RemoveAllByUID(" + UID + ",'" + Username + "')\" style=\"margin-right:5px;\" class=\"btn\">Ẩn tất cả</a>";
                                                            html += "<a href=\"javascript:;\" onclick=\"UpdateAllByUID()\" class=\"btn\">Cập nhật tất cả</a>";
                                                            html += "</div>";
                                                            html += "</div>";
                                                            html += "<div class=\"responsive-tb\">";
                                                            html += "<table class=\"table table-inside centered table-warehouse\">";
                                                            html += "<thead>";
                                                            html += "<tr class=\"teal darken-4\">";
                                                            html += "<th style=\"min-width: 50px;\">Order ID</th>";
                                                            html += "<th style=\"min-width: 50px;\">Loại ĐH</th>";
                                                            html += "<th style=\"min-width: 100px;\">Đơn hàng</th>";
                                                            html += "<th>Mã vận đơn</th>";
                                                            
                                                            
                                                            html += "<th style=\"min-width: auto;\">Cân nặng<br />(kg)</th>";
                                                            html += "<th class=\"size-th\">Kích thước</th>";
                                                            
                                                            
                                                            html += "<th style=\"width: 100px;\">Ghi chú</th>";
                                                            html += "<th style=\"width: 100px;\">Khách ghi chú</th>";
                                                            html += "<th style=\"min-width: 50px;\">Hình ảnh</th>";
                                                            html += "<th>Trạng thái</th>";
                                                            html += "<th style=\"min-width: 80px;\">Action</th>";
                                                            html += "</tr>";
                                                            html += "</thead>";
                                                            html += "<tbody class=\"orderid" + UID + " dh" + data.MainorderID + "\" data-orderid=\"" + data.MainorderID + "\"  data-ordertype=\"" + data.OrderTypeInt + "\">";
                                                            html += "<tr><td rowspan=\"1000\" class=\"grey lighten-2\"><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></td></tr>";

                                                            if (data.Status == 0) {
                                                                html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                            }
                                                            else if (data.Status == 1) {
                                                                html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                            }
                                                            else if (data.Status == 2) {
                                                                html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                            }
                                                            else {
                                                                html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                            }

                                                            //html += "<td rowspan=\"50\" class=\"grey lighten-2\"><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a>";
                                                            //html += "</td>";
                                                            html += "<td><span>" + data.OrderType + "</span></td>";
                                                            html += "<td>";
                                                            html += "<div class=\"tb-block\">";
                                                            html += "<p class=\"black-text\">KD</p>";
                                                            if (data.Kiemdem == "Có") {
                                                                html += "<p><a href=\"javascript:;\" onclick=\"checkProduct(" + data.MainorderID + ");\"><i class=\"material-icons green-text\">check_circle</i></a></p>";
                                                            }
                                                            else {
                                                                html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                            }
                                                            html += "</div>";
                                                            html += "<div class=\"tb-block\">";
                                                            html += "<p class=\"black-text\">ĐG</p>";
                                                            if (data.Donggo == "Có") {
                                                                html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                            }
                                                            else {
                                                                html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                            }
                                                            html += "</div>";
                                                            html += "<div class=\"tb-block\">";
                                                            html += "<p class=\"black-text\">BH</p>";
                                                            if (data.Baohiem == "Có") {
                                                                html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                            }
                                                            else {
                                                                html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                            }
                                                            html += "</div>";
                                                            html += "</td>";
                                                            html += "<td><span>" + data.BarCode + "</span></td>";
                                                           
                                                            
                                                            html += "<td><span>" + data.Weight + "</span></td>";
                                                            html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                            html += "<span class=\"packageWeightUpdate\" type=\"text\" value=\"" + data.Weight + "\"> " + data.Weight + "</span></td>";
                                                            html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                            html += "<p><span class=\"lb\">d:</span><input class=\"lengthsize\" type=\"text\" value=\"" + data.dai + "\"></p>";
                                                            html += "<p class=\"operator\"><span class=\"lb\">r:</span><input class=\"widthsize\" type=\"text\" value=\"" + data.rong + "\"></p>";
                                                            html += "<p class=\"operator\"><span class=\"lb\">c:</span><input class=\"heightsize\" type=\"text\" value=\"" + data.cao + "\"></p>";

                                                            html += "</td>";

                                                           
                                                           
                                                            html += "<td>";
                                                            html += "<textarea name=\"\" class=\"tb-textarea packagedescription\" rows=\"5\">" + data.Description + "</textarea>";
                                                            html += "</td>";
                                                            html += "<td>";
                                                            html += "<textarea name=\"\" class=\"tb-textarea packagecustdescription\" rows=\"5\">" + data.Khachghichu + "</textarea>";
                                                            html += "</td>";
                                                            html += "<td>";
                                                            html += "<div style=\"pointer-events: none;\">";
                                                            html += "<input class=\"upload-img\" type=\"file\" onchange=\"previewFiles(this)\" multiple title=\"\">";
                                                            html += "<button class=\"btn-upload\">Upload</button>";
                                                            html += "</div>";
                                                            html += "<div class=\"preview-img " + packageID + "\">";
                                                            if (data.IMG != null) {
                                                                var IMG = data.IMG.split('|');
                                                                for (var j = 0; j < IMG.length - 1; j++) {
                                                                    html += "<div class=\"img-block\">";
                                                                    html += "<img class=\"img materialboxed\" title=\"\" data-caption=\"\" src=\"" + IMG[j] + "\">";
                                                                    html += "<span class=\"material-icons red-text delete\">clear</span>";
                                                                    html += "</div>";
                                                                }
                                                            }
                                                            html += "</div>";
                                                            html += "</td>";
                                                            html += "<td>";
                                                            html += "<div class=\"input-field\" style=\"pointer-events: none;\">";
                                                            html += "<select class=\"package-status-select\">";
                                                            html += "<option value=\"\" disabled>Trạng thái</option>";
                                                            var status = data.Status;
                                                            if (status == 0) {
                                                                html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                            }
                                                            else if (status == 1) {
                                                                html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                            }
                                                            else if (status == 2) {
                                                                html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                            }
                                                            else if (status == 3) {
                                                                html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                            }
                                                            else {
                                                                html += "<option value=\"4\" selected>Đã giao cho khách</option>";
                                                            }

                                                            html += "</select>";
                                                            html += "</div>";
                                                            html += "</td>";
                                                            html += "<td>";
                                                            html += "<div class=\" action-table\">";
                                                            html += "<a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "',$(this)," + packageID + ")\" class=\"tooltipped updatebutton\" data-position=\"top\" data-tooltip=\"Cập nhật thay đổi\"><i class=\"material-icons\">sync</i></a>";




                                                            html += "<a href=\"javascript:;\" onclick=\"huyxuatkho($(this)," + pID + ", " + data.MainorderID + ")\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Ẩn đi\"><i class=\"material-icons\">visibility_off</i></a>";
                                                            html += "</div>";
                                                            html += "</td>";
                                                            html += "</tr>";
                                                            html += "</tbody>";
                                                            html += "</table>";
                                                            html += "</div>";
                                                            html += "</div>";
                                                            html += "</div>";
                                                            html += "</div>";
                                                            html += "</div>";
                                                            html += "</div>";

                                                            $("#listpackage").prepend(html);
                                                            // moveOnTopPackage(packageID);
                                                        }
                                                        else {
                                                            var MainID = $(".orderid" + UID + "").attr('data-orderid');
                                                            if (MainID == data.MainorderID) {
                                                                var otype = $(".orderid" + UID + "").attr('data-ordertype');
                                                                if (otype == data.OrderTypeInt) {
                                                                    var html = '';
                                                                    // html += "<tr><td rowspan=\"1000\" class=\"grey lighten-2\"><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></td></tr>";
                                                                    if (data.Status == 0) {
                                                                        html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                    }
                                                                    else if (data.Status == 1) {
                                                                        html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                    }
                                                                    else if (data.Status == 2) {
                                                                        html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                    }
                                                                    else {
                                                                        html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                    }
                                                                    //html += "<td rowspan=\"50\" class=\"grey lighten-2\"><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a>";
                                                                    html += "</td>";
                                                                    html += "<td><span>" + data.OrderType + "</span></td>";
                                                                    html += "<td>";
                                                                    html += "<div class=\"tb-block\">";
                                                                    html += "<p class=\"black-text\">KD</p>";
                                                                    if (data.Kiemdem == "Có") {
                                                                        html += "<p><a href=\"javascript:;\" onclick=\"checkProduct(" + data.MainorderID + ");\"><i class=\"material-icons green-text\">check_circle</i></a></p>";
                                                                    }
                                                                    else {
                                                                        html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                    }
                                                                    html += "</div>";
                                                                    html += "<div class=\"tb-block\">";
                                                                    html += "<p class=\"black-text\">ĐG</p>";
                                                                    if (data.Donggo == "Có") {
                                                                        html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                    }
                                                                    else {
                                                                        html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                    }
                                                                    html += "</div>";
                                                                    html += "<div class=\"tb-block\">";
                                                                    html += "<p class=\"black-text\">BH</p>";
                                                                    if (data.Baohiem == "Có") {
                                                                        html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                    }
                                                                    else {
                                                                        html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                    }
                                                                    html += "</div>";
                                                                    html += "</td>";
                                                                    html += "<td><span>" + data.BarCode + "</span></td>";
                                                                   
                                                                    
                                                                    html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                                    html += "<span class=\"packageWeightUpdate\" type=\"text\" value=\"" + data.Weight + "\"> " + data.Weight + "</span></td>";
                                                                    html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                                    html += "<p><span class=\"lb\">d:</span><input class=\"lengthsize\" type=\"text\" value=\"" + data.dai + "\"></p>";
                                                                    html += "<p class=\"operator\"><span class=\"lb\">r:</span><input class=\"widthsize\" type=\"text\" value=\"" + data.rong + "\"></p>";
                                                                    html += "<p class=\"operator\"><span class=\"lb\">c:</span><input class=\"heightsize\" type=\"text\" value=\"" + data.cao + "\"></p>";
                                                                    html += "</td>";

                                                                   
                                                                  
                                                                    html += "<td>";
                                                                    html += "<textarea name=\"\" class=\"tb-textarea packagedescription\" rows=\"5\">" + data.Description + "</textarea>";
                                                                    html += "</td>";
                                                                    html += "<td>";
                                                                    html += "<textarea name=\"\" class=\"tb-textarea packagecustdescription\" rows=\"5\">" + data.Khachghichu + "</textarea>";
                                                                    html += "</td>";
                                                                    html += "<td>";
                                                                    html += "<div style=\"pointer-events: none;\">";
                                                                    html += "<input class=\"upload-img\" type=\"file\" onchange=\"previewFiles(this)\" multiple title=\"\">";
                                                                    html += "<button class=\"btn-upload\">Upload</button>";
                                                                    html += "</div>";
                                                                    html += "<div class=\"preview-img " + packageID + "\">";
                                                                    if (data.IMG != null) {
                                                                        var IMG = data.IMG.split('|');
                                                                        for (var j = 0; j < IMG.length - 1; j++) {
                                                                            html += "<div class=\"img-block\">";
                                                                            html += "<img class=\"img materialboxed\" title=\"\" data-caption=\"\" src=\"" + IMG[j] + "\">";
                                                                            html += "<span class=\"material-icons red-text delete\">clear</span>";
                                                                            html += "</div>";
                                                                        }
                                                                    }
                                                                    html += "</div>";
                                                                    html += "</td>";
                                                                    html += "<td>";
                                                                    html += "<div class=\"input-field\" style=\"pointer-events: none;\">";
                                                                    html += "<select class=\"package-status-select\">";
                                                                    html += "<option value=\"\" disabled>Trạng thái</option>";
                                                                    var status = data.Status;

                                                                    if (status == 0) {
                                                                        html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                                    }
                                                                    else if (status == 1) {
                                                                        html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                                    }
                                                                    else if (status == 2) {
                                                                        html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                                    }
                                                                    else if (status == 3) {
                                                                        html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                                    }
                                                                    else {
                                                                        html += "<option value=\"4\" selected>Đã giao cho khách</option>";
                                                                    }


                                                                    html += "</select>";
                                                                    html += "</div>";
                                                                    html += "</td>";
                                                                    html += "<td>";
                                                                    html += "<div class=\" action-table\">";
                                                                    html += "<a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "',$(this)," + packageID + ")\" class=\"tooltipped updatebutton\" data-position=\"top\" data-tooltip=\"Cập nhật thay đổi\"><i class=\"material-icons\">sync</i></a>";




                                                                    html += "<a href=\"javascript:;\" onclick=\"huyxuatkho($(this)," + pID + ", " + data.MainorderID + ")\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Ẩn đi\"><i class=\"material-icons\">visibility_off</i></a>";
                                                                    html += "</div>";
                                                                    html += "</td>";
                                                                    html += "</tr>";

                                                                    $("." + data.MainorderID + "").parent().append(html);
                                                                    moveOnTopPackage(packageID);
                                                                }
                                                                else {
                                                                    var html = '';
                                                                    html += "<tbody class=\"orderid" + UID + " dh" + data.MainorderID + "\" data-orderid=\"" + data.MainorderID + "\"  data-ordertype=\"" + data.OrderTypeInt + "\">";
                                                                    html += "<tr><td rowspan=\"1000\" class=\"grey lighten-2\"><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></td></tr>";
                                                                    if (data.Status == 0) {
                                                                        html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                    }
                                                                    else if (data.Status == 1) {
                                                                        html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                    }
                                                                    else if (data.Status == 2) {
                                                                        html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                    }
                                                                    else {
                                                                        html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                    }
                                                                    //html += "<td rowspan=\"50\" class=\"grey lighten-2\"><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a>";
                                                                    //html += "</td>";
                                                                    html += "<td><span>" + data.OrderType + "</span></td>";
                                                                    html += "<td>";
                                                                    html += "<div class=\"tb-block\">";
                                                                    html += "<p class=\"black-text\">KD</p>";
                                                                    if (data.Kiemdem == "Có") {
                                                                        html += "<p><a href=\"javascript:;\" onclick=\"checkProduct(" + data.MainorderID + ");\"><i class=\"material-icons green-text\">check_circle</i></a></p>";
                                                                    }
                                                                    else {
                                                                        html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                    }
                                                                    html += "</div>";
                                                                    html += "<div class=\"tb-block\">";
                                                                    html += "<p class=\"black-text\">ĐG</p>";
                                                                    if (data.Donggo == "Có") {
                                                                        html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                    }
                                                                    else {
                                                                        html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                    }
                                                                    html += "</div>";
                                                                    html += "<div class=\"tb-block\">";
                                                                    html += "<p class=\"black-text\">BH</p>";
                                                                    if (data.Baohiem == "Có") {
                                                                        html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                    }
                                                                    else {
                                                                        html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                    }
                                                                    html += "</div>";
                                                                    html += "</td>";
                                                                    html += "<td><span>" + data.BarCode + "</span></td>";
                                                                   
                                                                    
                                                                    html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                                    html += "<span class=\"packageWeightUpdate\" type=\"text\" value=\"" + data.Weight + "\"> " + data.Weight + "</span></td>";
                                                                    html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                                    html += "<p><span class=\"lb\">d:</span><input class=\"lengthsize\" type=\"text\" value=\"" + data.dai + "\"></p>";
                                                                    html += "<p class=\"operator\"><span class=\"lb\">r:</span><input class=\"widthsize\" type=\"text\" value=\"" + data.rong + "\"></p>";
                                                                    html += "<p class=\"operator\"><span class=\"lb\">c:</span><input class=\"heightsize\" type=\"text\" value=\"" + data.cao + "\"></p>";
                                                                    html += "</td>";

                                                                   
                                                                   
                                                                    html += "<td>";
                                                                    html += "<textarea name=\"\" class=\"tb-textarea packagedescription\" rows=\"5\">" + data.Description + "</textarea>";
                                                                    html += "</td>";
                                                                    html += "<td>";
                                                                    html += "<textarea name=\"\" class=\"tb-textarea packagecustdescription\" rows=\"5\">" + data.Khachghichu + "</textarea>";
                                                                    html += "</td>";
                                                                    html += "<td>";
                                                                    html += "<div style=\"pointer-events: none;\">";
                                                                    html += "<input class=\"upload-img\" type=\"file\" onchange=\"previewFiles(this)\" multiple title=\"\">";
                                                                    html += "<button class=\"btn-upload\">Upload</button>";
                                                                    html += "</div>";
                                                                    html += "<div class=\"preview-img " + packageID + "\">";
                                                                    if (data.IMG != null) {
                                                                        var IMG = data.IMG.split('|');
                                                                        for (var j = 0; j < IMG.length - 1; j++) {
                                                                            html += "<div class=\"img-block\">";
                                                                            html += "<img class=\"img materialboxed\" title=\"\" data-caption=\"\" src=\"" + IMG[j] + "\">";
                                                                            html += "<span class=\"material-icons red-text delete\">clear</span>";
                                                                            html += "</div>";
                                                                        }
                                                                    }
                                                                    html += "</div>";
                                                                    html += "</td>";
                                                                    html += "<td>";
                                                                    html += "<div class=\"input-field\" style=\"pointer-events: none;\">";
                                                                    html += "<select class=\"package-status-select\">";
                                                                    html += "<option value=\"\" disabled>Trạng thái</option>";
                                                                    var status = data.Status;

                                                                    if (status == 0) {
                                                                        html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                                    }
                                                                    else if (status == 1) {
                                                                        html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                                    }
                                                                    else if (status == 2) {
                                                                        html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                                    }
                                                                    else if (status == 3) {
                                                                        html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                                    }
                                                                    else {
                                                                        html += "<option value=\"4\" selected>Đã giao cho khách</option>";
                                                                    }


                                                                    html += "</select>";
                                                                    html += "</div>";
                                                                    html += "</td>";
                                                                    html += "<td>";
                                                                    html += "<div class=\" action-table\">";
                                                                    html += "<a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "',$(this)," + packageID + ")\" class=\"tooltipped updatebutton\" data-position=\"top\" data-tooltip=\"Cập nhật thay đổi\"><i class=\"material-icons\">sync</i></a>";




                                                                    html += "<a href=\"javascript:;\" onclick=\"huyxuatkho($(this)," + pID + ", " + data.MainorderID + ")\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Ẩn đi\"><i class=\"material-icons\">visibility_off</i></a>";
                                                                    html += "</div>";
                                                                    html += "</td>";
                                                                    html += "</tr>";
                                                                    html += "</tbody>";

                                                                    $(".orderid" + UID + "").parent().prepend(html);
                                                                    // moveOnTopPackage(packageID);
                                                                }
                                                            }
                                                            else {
                                                                var html = '';
                                                                html += "<tbody class=\"orderid" + UID + " dh" + data.MainorderID + "\" data-orderid=\"" + data.MainorderID + "\"  data-ordertype=\"" + data.OrderTypeInt + "\">";
                                                                html += "<tr><td rowspan=\"1000\" class=\"grey lighten-2\"><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></td></tr>";
                                                                if (data.Status == 0) {
                                                                    html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                else if (data.Status == 1) {
                                                                    html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                else if (data.Status == 2) {
                                                                    html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                else {
                                                                    html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                //html += "<td rowspan=\"50\" class=\"grey lighten-2\"><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a>";
                                                                //html += "</td>";
                                                                html += "<td><span>" + data.OrderType + "</span></td>";
                                                                html += "<td>";
                                                                html += "<div class=\"tb-block\">";
                                                                html += "<p class=\"black-text\">KD</p>";
                                                                if (data.Kiemdem == "Có") {
                                                                    html += "<p><a href=\"javascript:;\" onclick=\"checkProduct(" + data.MainorderID + ");\"><i class=\"material-icons green-text\">check_circle</i></a></p>";
                                                                }
                                                                else {
                                                                    html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                }
                                                                html += "</div>";
                                                                html += "<div class=\"tb-block\">";
                                                                html += "<p class=\"black-text\">ĐG</p>";
                                                                if (data.Donggo == "Có") {
                                                                    html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                }
                                                                else {
                                                                    html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                }
                                                                html += "</div>";
                                                                html += "<div class=\"tb-block\">";
                                                                html += "<p class=\"black-text\">BH</p>";
                                                                if (data.Baohiem == "Có") {
                                                                    html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                }
                                                                else {
                                                                    html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                }
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "<td><span>" + data.BarCode + "</span></td>";
                                                               
                                                                
                                                                html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                                html += "<span class=\"packageWeightUpdate\" type=\"text\" value=\"" + data.Weight + "\"> " + data.Weight + "</span></td>";
                                                                html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                                html += "<p><span class=\"lb\">d:</span><input class=\"lengthsize\" type=\"text\" value=\"" + data.dai + "\"></p>";
                                                                html += "<p class=\"operator\"><span class=\"lb\">r:</span><input class=\"widthsize\" type=\"text\" value=\"" + data.rong + "\"></p>";
                                                                html += "<p class=\"operator\"><span class=\"lb\">c:</span><input class=\"heightsize\" type=\"text\" value=\"" + data.cao + "\"></p>";
                                                                html += "</td>";

                                                               
                                                              
                                                                html += "<td>";
                                                                html += "<textarea name=\"\" class=\"tb-textarea packagedescription\" rows=\"5\">" + data.Description + "</textarea>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<textarea name=\"\" class=\"tb-textarea packagecustdescription\" rows=\"5\">" + data.Khachghichu + "</textarea>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<div style=\"pointer-events: none;\">";
                                                                html += "<input class=\"upload-img\" type=\"file\" onchange=\"previewFiles(this)\" multiple title=\"\">";
                                                                html += "<button class=\"btn-upload\">Upload</button>";
                                                                html += "</div>";
                                                                html += "<div class=\"preview-img " + packageID + "\">";
                                                                if (data.IMG != null) {
                                                                    var IMG = data.IMG.split('|');
                                                                    for (var j = 0; j < IMG.length - 1; j++) {
                                                                        html += "<div class=\"img-block\">";
                                                                        html += "<img class=\"img materialboxed\" title=\"\" data-caption=\"\" src=\"" + IMG[j] + "\">";
                                                                        html += "<span class=\"material-icons red-text delete\">clear</span>";
                                                                        html += "</div>";
                                                                    }
                                                                }
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<div class=\"input-field\" style=\"pointer-events: none;\">";
                                                                html += "<select class=\"package-status-select\">";
                                                                html += "<option value=\"\" disabled>Trạng thái</option>";
                                                                var status = data.Status;
                                                                if (status == 0) {
                                                                    html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                                }
                                                                else if (status == 1) {
                                                                    html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                                }
                                                                else if (status == 2) {
                                                                    html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                                }
                                                                else if (status == 3) {
                                                                    html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                                }
                                                                else {
                                                                    html += "<option value=\"4\" selected>Đã giao cho khách</option>";
                                                                }

                                                                html += "</select>";
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<div class=\" action-table\">";
                                                                html += "<a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "',$(this)," + packageID + ")\" class=\"tooltipped updatebutton\" data-position=\"top\" data-tooltip=\"Cập nhật thay đổi\"><i class=\"material-icons\">sync</i></a>";




                                                                html += "<a href=\"javascript:;\" onclick=\"huyxuatkho($(this)," + pID + ", " + data.MainorderID + ")\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Ẩn đi\"><i class=\"material-icons\">visibility_off</i></a>";
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "</tr>";
                                                                html += "</tbody>";

                                                                $(".orderid" + UID + "").parent().prepend(html);
                                                                moveOnTopPackage(packageID);
                                                            }
                                                        }
                                                        $('select').formSelect();
                                                        obj.val("");
                                                    }
                                                }
                                                else if (data.OrderTypeInt != 1) {
                                                    if (isExist == false) {
                                                        var idpack = "bc-" + data.BarCode + "-" + packageID;
                                                        if (check == false) {
                                                            var html = '';
                                                            html += "<div class=\"package-wrap accent-2\" id=\"" + pID + "\" data-uid=\"" + UID + "\">";
                                                            html += "<div class=\"row\">";
                                                            html += "<div class=\"col s12\">";
                                                            html += "<div class=\"list-package\">";
                                                            html += "<div class=\"package-item pb-2\">";
                                                            html += "<div class=\"wrap-top-action\">";
                                                            html += "<div class=\"owner\">";
                                                            html += "<span>" + Username + "</span> | <span>" + Phone + "</span>";
                                                            html += "</div>";
                                                            html += "<div class=\"action-all\">";
                                                            html += "<a href=\"javascript:;\" onclick=\"RemoveAllByUID(" + UID + ",'" + Username + "')\" style=\"margin-right:5px;\" class=\"btn\">Ẩn tất cả</a>";
                                                            html += "<a href=\"javascript:;\" onclick=\"UpdateAllByUID()\" class=\"btn\">Cập nhật tất cả</a>";
                                                            html += "</div>";
                                                            html += "</div>";
                                                            html += "<div class=\"responsive-tb\">";
                                                            html += "<table class=\"table table-inside centered table-warehouse\">";
                                                            html += "<thead>";
                                                            html += "<tr class=\"teal darken-4\">";
                                                            html += "<th style=\"min-width: 50px;\">Order ID</th>";
                                                            html += "<th style=\"min-width: 50px;\">Loại ĐH</th>";
                                                            html += "<th style=\"min-width: 100px;\">Đơn hàng</th>";
                                                            html += "<th>Mã vận đơn</th>";
                                                            
                                                            
                                                            html += "<th style=\"min-width: auto;\">Cân nặng<br />(kg)</th>";
                                                            html += "<th class=\"size-th\">Kích thước</th>";
                                                            html += "<th style=\"min-width: 100px\">Cước vật tư</th>";
                                                            html += "<th>Phụ phí ĐB</th>";
                                                            html += "<th style=\"width: 100px;\">Ghi chú</th>";
                                                            html += "<th style=\"width: 100px;\">Khách ghi chú</th>";
                                                            html += "<th style=\"min-width: 50px;\">Hình ảnh</th>";
                                                            html += "<th>Trạng thái</th>";
                                                            html += "<th style=\"min-width: 80px;\">Action</th>";
                                                            html += "</tr>";
                                                            html += "</thead>";
                                                            html += "<tbody class=\"orderid" + UID + " dh" + data.MainorderID + "\" data-orderid=\"" + data.MainorderID + "\"  data-ordertype=\"" + data.OrderTypeInt + "\">";
                                                            html += "<tr><td rowspan=\"1000\" class=\"grey lighten-2\"><a  target=\"_blank\">" + data.TransportationID + "</a></td></tr>";

                                                            if (data.Status == 0) {
                                                                html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                            }
                                                            else if (data.Status == 1) {
                                                                html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                            }
                                                            else if (data.Status == 2) {
                                                                html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                            }
                                                            else {
                                                                html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                            }

                                                            //html += "<td rowspan=\"50\" class=\"grey lighten-2\"><a  target=\"_blank\">" + data.TransportationID + "</a>";
                                                            //html += "</td>";
                                                            html += "<td><span>" + data.OrderType + "</span></td>";
                                                            html += "<td>";
                                                            html += "<div class=\"tb-block\">";
                                                            html += "<p class=\"black-text\">KD</p>";
                                                            if (data.Kiemdem == "Có") {
                                                                html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                            }
                                                            else {
                                                                html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                            }
                                                            html += "</div>";
                                                            html += "<div class=\"tb-block\">";
                                                            html += "<p class=\"black-text\">ĐG</p>";
                                                            if (data.Donggo == "Có") {
                                                                html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                            }
                                                            else {
                                                                html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                            }
                                                            html += "</div>";
                                                            html += "<div class=\"tb-block\">";
                                                            html += "<p class=\"black-text\">BH</p>";
                                                            if (data.Baohiem == "Có") {
                                                                html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                            }
                                                            else {
                                                                html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                            }
                                                            html += "</div>";
                                                            html += "</td>";
                                                            html += "<td><span>" + data.BarCode + "</span></td>";
                                                           
                                                            
                                                            html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                            html += "<span class=\"packageWeightUpdate\" type=\"text\" value=\"" + data.Weight + "\"> " + data.Weight + "</span></td>";
                                                            html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                            html += "<p><span class=\"lb\">d:</span><input class=\"lengthsize\" type=\"text\" value=\"" + data.dai + "\"></p>";
                                                            html += "<p class=\"operator\"><span class=\"lb\">r:</span><input class=\"widthsize\" type=\"text\" value=\"" + data.rong + "\"></p>";
                                                            html += "<p class=\"operator\"><span class=\"lb\">c:</span><input class=\"heightsize\" type=\"text\" value=\"" + data.cao + "\"></p>";
                                                            html += "</td>";

                                                            //html += "<td><input type=\"text\" value=\"" + data.Loaisanpham + "\" class=\"packageproducttype\"></td>";

                                                            html += "<td class=\"\">";
                                                            html += "<p class=\"operator\"><span class=\"lb\">CNY:</span><input class=\"packageadditionfeeCYN\" oninput=\"getPriceFee('packageadditionfeeCYN',$(this),'cyn')\" type=\"number\" value=\"" + data.CuocvattuCYN + "\"></p>";
                                                            html += "<p class=\"operator\"><span class=\"lb\">VND:</span><input class=\"packageadditionfeeVND\" oninput=\"getPriceFee('packageadditionfeeVND',$(this),'vnd')\" type=\"number\" value=\"" + data.CuocvattuVND + "\"></p>";
                                                            html += "</td>";


                                                            //html += "<td>";
                                                            //html += "<div>";
                                                            //                                                            html += "<span type=\"text\" value=\"" + data.NVKiemdem + "\" class=\"packagedStaffNoteCheck\">"+data.NVKiemdem+"</span>";
                                                            //html += "</div>";
                                                            //html += "</td>";

                                                            html += "<td class=\"\">";
                                                            html += "<p class=\"operator\"><span class=\"lb\">CNY:</span><input class=\"packageSensorCYN\" oninput=\"getPriceFee('packageSensorCYN',$(this),'cyn')\" type=\"number\" value=\"" + data.HangDBCYN + "\"></p>";
                                                            html += "<p class=\"operator\"><span class=\"lb\">VND:</span><input class=\"packageSensorVND\" oninput=\"getPriceFee('packageSensorVND',$(this),'vnd')\" type=\"number\" value=\"" + data.HangDBVND + "\"></p>";
                                                            html += "</td>";

                                                            html += "<td>";
                                                            html += "<textarea name=\"\" class=\"tb-textarea packagedescription\" rows=\"5\">" + data.Description + "</textarea>";
                                                            html += "</td>";
                                                            html += "<td>";
                                                            html += "<textarea name=\"\" class=\"tb-textarea packagecustdescription\" rows=\"5\">" + data.Khachghichu + "</textarea>";
                                                            html += "</td>";
                                                            html += "<td>";
                                                            html += "<div style=\"pointer-events: none;\">";
                                                            html += "<input class=\"upload-img\" type=\"file\" onchange=\"previewFiles(this)\" multiple title=\"\">";
                                                            html += "<button class=\"btn-upload\">Upload</button>";
                                                            html += "</div>";
                                                            html += "<div class=\"preview-img " + packageID + "\">";
                                                            if (data.IMG != null) {
                                                                var IMG = data.IMG.split('|');
                                                                for (var j = 0; j < IMG.length - 1; j++) {
                                                                    html += "<div class=\"img-block\">";
                                                                    html += "<img class=\"img materialboxed\" title=\"\" data-caption=\"\" src=\"" + IMG[j] + "\">";
                                                                    html += "<span class=\"material-icons red-text delete\">clear</span>";
                                                                    html += "</div>";
                                                                }
                                                            }
                                                            html += "</div>";
                                                            html += "</td>";
                                                            html += "<td>";
                                                            html += "<div class=\"input-field\" style=\"pointer-events: none;\">";
                                                            html += "<select class=\"package-status-select\">";
                                                            html += "<option value=\"\" disabled>Trạng thái</option>";
                                                            var status = data.Status;
                                                            if (status == 0) {
                                                                html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                            }
                                                            else if (status == 1) {
                                                                html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                            }
                                                            else if (status == 2) {
                                                                html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                            }
                                                            else if (status == 3) {
                                                                html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                            }
                                                            else {
                                                                html += "<option value=\"4\" selected>Đã giao cho khách</option>";
                                                            }

                                                            html += "</select>";
                                                            html += "</div>";
                                                            html += "</td>";
                                                            html += "<td>";
                                                            html += "<div class=\" action-table\">";
                                                            html += "<a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "',$(this)," + packageID + ")\" class=\"tooltipped updatebutton\" data-position=\"top\" data-tooltip=\"Cập nhật thay đổi\"><i class=\"material-icons\">sync</i></a>";



                                                            html += "<a href=\"javascript:;\" onclick=\"huyxuatkho($(this)," + pID + ", " + data.MainorderID + ")\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Ẩn đi\"><i class=\"material-icons\">visibility_off</i></a>";
                                                            html += "</div>";
                                                            html += "</td>";
                                                            html += "</tr>";
                                                            html += "</tbody>";
                                                            html += "</table>";
                                                            html += "</div>";
                                                            html += "</div>";
                                                            html += "</div>";
                                                            html += "</div>";
                                                            html += "</div>";
                                                            html += "</div>";

                                                            $("#listpackage").prepend(html);
                                                            // moveOnTopPackage(packageID);
                                                        }
                                                        else {
                                                            var MainID = $(".orderid" + UID + "").attr('data-orderid');
                                                            if (MainID == data.MainorderID) {
                                                                var otype = $(".orderid" + UID + "").attr('data-ordertype');
                                                                if (otype == data.OrderTypeInt) {
                                                                    var html = '';
                                                                    // html += "<tr><td rowspan=\"1000\" class=\"grey lighten-2\"><a  target=\"_blank\">" + data.TransportationID + "</a></td></tr>";
                                                                    if (data.Status == 0) {
                                                                        html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                    }
                                                                    else if (data.Status == 1) {
                                                                        html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                    }
                                                                    else if (data.Status == 2) {
                                                                        html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                    }
                                                                    else {
                                                                        html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                    }
                                                                    //html += "<td rowspan=\"50\" class=\"grey lighten-2\"><a  target=\"_blank\">" + data.TransportationID + "</a>";
                                                                    html += "</td>";
                                                                    html += "<td><span>" + data.OrderType + "</span></td>";
                                                                    html += "<td>";
                                                                    html += "<div class=\"tb-block\">";
                                                                    html += "<p class=\"black-text\">KD</p>";
                                                                    if (data.Kiemdem == "Có") {
                                                                        html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                    }
                                                                    else {
                                                                        html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                    }
                                                                    html += "</div>";
                                                                    html += "<div class=\"tb-block\">";
                                                                    html += "<p class=\"black-text\">ĐG</p>";
                                                                    if (data.Donggo == "Có") {
                                                                        html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                    }
                                                                    else {
                                                                        html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                    }
                                                                    html += "</div>";
                                                                    html += "<div class=\"tb-block\">";
                                                                    html += "<p class=\"black-text\">BH</p>";
                                                                    if (data.Baohiem == "Có") {
                                                                        html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                    }
                                                                    else {
                                                                        html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                    }
                                                                    html += "</div>";
                                                                    html += "</td>";
                                                                    html += "<td><span>" + data.BarCode + "</span></td>";
                                                                   
                                                                    
                                                                    html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                                    html += "<span class=\"packageWeightUpdate\" type=\"text\" value=\"" + data.Weight + "\"> " + data.Weight + "</span></td>";
                                                                    html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                                    html += "<p><span class=\"lb\">d:</span><input class=\"lengthsize\" type=\"text\" value=\"" + data.dai + "\"></p>";
                                                                    html += "<p class=\"operator\"><span class=\"lb\">r:</span><input class=\"widthsize\" type=\"text\" value=\"" + data.rong + "\"></p>";
                                                                    html += "<p class=\"operator\"><span class=\"lb\">c:</span><input class=\"heightsize\" type=\"text\" value=\"" + data.cao + "\"></p>";
                                                                    html += "</td>";

                                                                    //html += "<td><input type=\"text\" value=\"" + data.Loaisanpham + "\" class=\"packageproducttype\"></td>";
                                                                    html += "<td class=\"\">";
                                                                    html += "<p class=\"operator\"><span class=\"lb\">CNY:</span><input class=\"packageadditionfeeCYN\" oninput=\"getPriceFee('packageadditionfeeCYN',$(this),'cyn')\" type=\"number\" value=\"" + data.CuocvattuCYN + "\"></p>";
                                                                    html += "<p class=\"operator\"><span class=\"lb\">VND:</span><input class=\"packageadditionfeeVND\" oninput=\"getPriceFee('packageadditionfeeVND',$(this),'vnd')\" type=\"number\" value=\"" + data.CuocvattuVND + "\"></p>";
                                                                    html += "</td>";

                                                                    //html += "<td>";
                                                                    //html += "<div>";
                                                                    //                                                            html += "<span type=\"text\" value=\"" + data.NVKiemdem + "\" class=\"packagedStaffNoteCheck\">"+data.NVKiemdem+"</span>";
                                                                    //html += "</div>";
                                                                    //html += "</td>";

                                                                    html += "<td class=\"\">";
                                                                    html += "<p class=\"operator\"><span class=\"lb\">CNY:</span><input class=\"packageSensorCYN\" oninput=\"getPriceFee('packageSensorCYN',$(this),'cyn')\" type=\"number\" value=\"" + data.HangDBCYN + "\"></p>";
                                                                    html += "<p class=\"operator\"><span class=\"lb\">VND:</span><input class=\"packageSensorVND\" oninput=\"getPriceFee('packageSensorVND',$(this),'vnd')\" type=\"number\" value=\"" + data.HangDBVND + "\"></p>";
                                                                    html += "</td>";

                                                                    html += "<td>";
                                                                    html += "<textarea name=\"\" class=\"tb-textarea packagedescription\" rows=\"5\">" + data.Description + "</textarea>";
                                                                    html += "</td>";
                                                                    html += "<td>";
                                                                    html += "<textarea name=\"\" class=\"tb-textarea packagecustdescription\" rows=\"5\">" + data.Khachghichu + "</textarea>";
                                                                    html += "</td>";
                                                                    html += "<td>";
                                                                    html += "<div style=\"pointer-events: none;\">";
                                                                    html += "<input class=\"upload-img\" type=\"file\" onchange=\"previewFiles(this)\" multiple title=\"\">";
                                                                    html += "<button class=\"btn-upload\">Upload</button>";
                                                                    html += "</div>";
                                                                    html += "<div class=\"preview-img " + packageID + "\">";
                                                                    if (data.IMG != null) {
                                                                        var IMG = data.IMG.split('|');
                                                                        for (var j = 0; j < IMG.length - 1; j++) {
                                                                            html += "<div class=\"img-block\">";
                                                                            html += "<img class=\"img materialboxed\" title=\"\" data-caption=\"\" src=\"" + IMG[j] + "\">";
                                                                            html += "<span class=\"material-icons red-text delete\">clear</span>";
                                                                            html += "</div>";
                                                                        }
                                                                    }
                                                                    html += "</div>";
                                                                    html += "</td>";
                                                                    html += "<td>";
                                                                    html += "<div class=\"input-field\" style=\"pointer-events: none;\">";
                                                                    html += "<select class=\"package-status-select\">";
                                                                    html += "<option value=\"\" disabled>Trạng thái</option>";
                                                                    var status = data.Status;

                                                                    if (status == 0) {
                                                                        html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                                    }
                                                                    else if (status == 1) {
                                                                        html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                                    }
                                                                    else if (status == 2) {
                                                                        html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                                    }
                                                                    else if (status == 3) {
                                                                        html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                                    }
                                                                    else {
                                                                        html += "<option value=\"4\" selected>Đã giao cho khách</option>";
                                                                    }


                                                                    html += "</select>";
                                                                    html += "</div>";
                                                                    html += "</td>";
                                                                    html += "<td>";
                                                                    html += "<div class=\" action-table\">";
                                                                    html += "<a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "',$(this)," + packageID + ")\" class=\"tooltipped updatebutton\" data-position=\"top\" data-tooltip=\"Cập nhật thay đổi\"><i class=\"material-icons\">sync</i></a>";



                                                                    html += "<a href=\"javascript:;\" onclick=\"huyxuatkho($(this)," + pID + ", " + data.MainorderID + ")\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Ẩn đi\"><i class=\"material-icons\">visibility_off</i></a>";
                                                                    html += "</div>";
                                                                    html += "</td>";
                                                                    html += "</tr>";

                                                                    $("." + data.MainorderID + "").parent().append(html);
                                                                    moveOnTopPackage(packageID);
                                                                }
                                                                else {
                                                                    var html = '';
                                                                    html += "<tbody class=\"orderid" + UID + " dh" + data.MainorderID + "\" data-orderid=\"" + data.MainorderID + "\"  data-ordertype=\"" + data.OrderTypeInt + "\">";
                                                                    html += "<tr><td rowspan=\"1000\" class=\"grey lighten-2\"><a  target=\"_blank\">" + data.TransportationID + "</a></td></tr>";
                                                                    if (data.Status == 0) {
                                                                        html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                    }
                                                                    else if (data.Status == 1) {
                                                                        html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                    }
                                                                    else if (data.Status == 2) {
                                                                        html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                    }
                                                                    else {
                                                                        html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                    }
                                                                    //html += "<td rowspan=\"50\" class=\"grey lighten-2\"><a  target=\"_blank\">" + data.TransportationID + "</a>";
                                                                    //html += "</td>";
                                                                    html += "<td><span>" + data.OrderType + "</span></td>";
                                                                    html += "<td>";
                                                                    html += "<div class=\"tb-block\">";
                                                                    html += "<p class=\"black-text\">KD</p>";
                                                                    if (data.Kiemdem == "Có") {
                                                                        html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                    }
                                                                    else {
                                                                        html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                    }
                                                                    html += "</div>";
                                                                    html += "<div class=\"tb-block\">";
                                                                    html += "<p class=\"black-text\">ĐG</p>";
                                                                    if (data.Donggo == "Có") {
                                                                        html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                    }
                                                                    else {
                                                                        html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                    }
                                                                    html += "</div>";
                                                                    html += "<div class=\"tb-block\">";
                                                                    html += "<p class=\"black-text\">BH</p>";
                                                                    if (data.Baohiem == "Có") {
                                                                        html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                    }
                                                                    else {
                                                                        html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                    }
                                                                    html += "</div>";
                                                                    html += "</td>";
                                                                    html += "<td><span>" + data.BarCode + "</span></td>";
                                                                   
                                                                    
                                                                    html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                                    html += "<span class=\"packageWeightUpdate\" type=\"text\" value=\"" + data.Weight + "\"> " + data.Weight + "</span></td>";
                                                                    html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                                    html += "<p><span class=\"lb\">d:</span><input class=\"lengthsize\" type=\"text\" value=\"" + data.dai + "\"></p>";
                                                                    html += "<p class=\"operator\"><span class=\"lb\">r:</span><input class=\"widthsize\" type=\"text\" value=\"" + data.rong + "\"></p>";
                                                                    html += "<p class=\"operator\"><span class=\"lb\">c:</span><input class=\"heightsize\" type=\"text\" value=\"" + data.cao + "\"></p>";
                                                                    html += "</td>";

                                                                    //html += "<td><input type=\"text\" value=\"" + data.Loaisanpham + "\" class=\"packageproducttype\"></td>";
                                                                    html += "<td class=\"\">";
                                                                    html += "<p class=\"operator\"><span class=\"lb\">CNY:</span><input class=\"packageadditionfeeCYN\" oninput=\"getPriceFee('packageadditionfeeCYN',$(this),'cyn')\" type=\"number\" value=\"" + data.CuocvattuCYN + "\"></p>";
                                                                    html += "<p class=\"operator\"><span class=\"lb\">VND:</span><input class=\"packageadditionfeeVND\" oninput=\"getPriceFee('packageadditionfeeVND',$(this),'vnd')\" type=\"number\" value=\"" + data.CuocvattuVND + "\"></p>";
                                                                    html += "</td>";

                                                                    //html += "<td>";
                                                                    //html += "<div>";
                                                                    //                                                            html += "<span type=\"text\" value=\"" + data.NVKiemdem + "\" class=\"packagedStaffNoteCheck\">"+data.NVKiemdem+"</span>";
                                                                    //html += "</div>";
                                                                    //html += "</td>";

                                                                    html += "<td class=\"\">";
                                                                    html += "<p class=\"operator\"><span class=\"lb\">CNY:</span><input class=\"packageSensorCYN\" oninput=\"getPriceFee('packageSensorCYN',$(this),'cyn')\" type=\"number\" value=\"" + data.HangDBCYN + "\"></p>";
                                                                    html += "<p class=\"operator\"><span class=\"lb\">VND:</span><input class=\"packageSensorVND\" oninput=\"getPriceFee('packageSensorVND',$(this),'vnd')\" type=\"number\" value=\"" + data.HangDBVND + "\"></p>";
                                                                    html += "</td>";


                                                                    html += "<td>";
                                                                    html += "<textarea name=\"\" class=\"tb-textarea packagedescription\" rows=\"5\">" + data.Description + "</textarea>";
                                                                    html += "</td>";
                                                                    html += "<td>";
                                                                    html += "<textarea name=\"\" class=\"tb-textarea packagecustdescription\" rows=\"5\">" + data.Khachghichu + "</textarea>";
                                                                    html += "</td>";
                                                                    html += "<td>";
                                                                    html += "<div style=\"pointer-events: none;\">";
                                                                    html += "<input class=\"upload-img\" type=\"file\" onchange=\"previewFiles(this)\" multiple title=\"\">";
                                                                    html += "<button class=\"btn-upload\">Upload</button>";
                                                                    html += "</div>";
                                                                    html += "<div class=\"preview-img " + packageID + "\">";
                                                                    if (data.IMG != null) {
                                                                        var IMG = data.IMG.split('|');
                                                                        for (var j = 0; j < IMG.length - 1; j++) {
                                                                            html += "<div class=\"img-block\">";
                                                                            html += "<img class=\"img materialboxed\" title=\"\" data-caption=\"\" src=\"" + IMG[j] + "\">";
                                                                            html += "<span class=\"material-icons red-text delete\">clear</span>";
                                                                            html += "</div>";
                                                                        }
                                                                    }
                                                                    html += "</div>";
                                                                    html += "</td>";
                                                                    html += "<td>";
                                                                    html += "<div class=\"input-field\" style=\"pointer-events: none;\">";
                                                                    html += "<select class=\"package-status-select\">";
                                                                    html += "<option value=\"\" disabled>Trạng thái</option>";
                                                                    var status = data.Status;

                                                                    if (status == 0) {
                                                                        html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                                    }
                                                                    else if (status == 1) {
                                                                        html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                                    }
                                                                    else if (status == 2) {
                                                                        html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                                    }
                                                                    else if (status == 3) {
                                                                        html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                                    }
                                                                    else {
                                                                        html += "<option value=\"4\" selected>Đã giao cho khách</option>";
                                                                    }


                                                                    html += "</select>";
                                                                    html += "</div>";
                                                                    html += "</td>";
                                                                    html += "<td>";
                                                                    html += "<div class=\" action-table\">";
                                                                    html += "<a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "',$(this)," + packageID + ")\" class=\"tooltipped updatebutton\" data-position=\"top\" data-tooltip=\"Cập nhật thay đổi\"><i class=\"material-icons\">sync</i></a>";



                                                                    html += "<a href=\"javascript:;\" onclick=\"huyxuatkho($(this)," + pID + ", " + data.MainorderID + ")\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Ẩn đi\"><i class=\"material-icons\">visibility_off</i></a>";
                                                                    html += "</div>";
                                                                    html += "</td>";
                                                                    html += "</tr>";
                                                                    html += "</tbody>";

                                                                    $(".orderid" + UID + "").parent().prepend(html);
                                                                    // moveOnTopPackage(packageID);
                                                                }
                                                            }
                                                            else {
                                                                var html = '';
                                                                html += "<tbody class=\"orderid" + UID + " dh" + data.MainorderID + "\" data-orderid=\"" + data.MainorderID + "\"  data-ordertype=\"" + data.OrderTypeInt + "\">";
                                                                html += "<tr><td rowspan=\"1000\" class=\"grey lighten-2\"><a  target=\"_blank\">" + data.TransportationID + "</a></td></tr>";
                                                                if (data.Status == 0) {
                                                                    html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                else if (data.Status == 1) {
                                                                    html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                else if (data.Status == 2) {
                                                                    html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                else {
                                                                    html += "<tr class=\"package-row  slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                //html += "<td rowspan=\"50\" class=\"grey lighten-2\"><a  target=\"_blank\">" + data.TransportationID + "</a>";
                                                                //html += "</td>";
                                                                html += "<td><span>" + data.OrderType + "</span></td>";
                                                                html += "<td>";
                                                                html += "<div class=\"tb-block\">";
                                                                html += "<p class=\"black-text\">KD</p>";
                                                                if (data.Kiemdem == "Có") {
                                                                    html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                }
                                                                else {
                                                                    html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                }
                                                                html += "</div>";
                                                                html += "<div class=\"tb-block\">";
                                                                html += "<p class=\"black-text\">ĐG</p>";
                                                                if (data.Donggo == "Có") {
                                                                    html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                }
                                                                else {
                                                                    html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                }
                                                                html += "</div>";
                                                                html += "<div class=\"tb-block\">";
                                                                html += "<p class=\"black-text\">BH</p>";
                                                                if (data.Baohiem == "Có") {
                                                                    html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                }
                                                                else {
                                                                    html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                }
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "<td><span>" + data.BarCode + "</span></td>";
                                                               
                                                                
                                                                html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                                html += "<span class=\"packageWeightUpdate\" type=\"text\" value=\"" + data.Weight + "\"> " + data.Weight + "</span></td>";
                                                                html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                                html += "<p><span class=\"lb\">d:</span><input class=\"lengthsize\" type=\"text\" value=\"" + data.dai + "\"></p>";
                                                                html += "<p class=\"operator\"><span class=\"lb\">r:</span><input class=\"widthsize\" type=\"text\" value=\"" + data.rong + "\"></p>";
                                                                html += "<p class=\"operator\"><span class=\"lb\">c:</span><input class=\"heightsize\" type=\"text\" value=\"" + data.cao + "\"></p>";
                                                                html += "</td>";

                                                                //html += "<td><input type=\"text\" value=\"" + data.Loaisanpham + "\" class=\"packageproducttype\"></td>";
                                                                html += "<td class=\"\">";
                                                                html += "<p class=\"operator\"><span class=\"lb\">CNY:</span><input class=\"packageadditionfeeCYN\" oninput=\"getPriceFee('packageadditionfeeCYN',$(this),'cyn')\" type=\"number\" value=\"" + data.CuocvattuCYN + "\"></p>";
                                                                html += "<p class=\"operator\"><span class=\"lb\">VND:</span><input class=\"packageadditionfeeVND\" oninput=\"getPriceFee('packageadditionfeeVND',$(this),'vnd')\" type=\"number\" value=\"" + data.CuocvattuVND + "\"></p>";
                                                                html += "</td>";


                                                                //html += "<td>";
                                                                //html += "<div>";
                                                                //                                                            html += "<span type=\"text\" value=\"" + data.NVKiemdem + "\" class=\"packagedStaffNoteCheck\">"+data.NVKiemdem+"</span>";
                                                                //html += "</div>";
                                                                //html += "</td>";

                                                                html += "<td class=\"\">";
                                                                html += "<p class=\"operator\"><span class=\"lb\">CNY:</span><input class=\"packageSensorCYN\" oninput=\"getPriceFee('packageSensorCYN',$(this),'cyn')\" type=\"number\" value=\"" + data.HangDBCYN + "\"></p>";
                                                                html += "<p class=\"operator\"><span class=\"lb\">VND:</span><input class=\"packageSensorVND\" oninput=\"getPriceFee('packageSensorVND',$(this),'vnd')\" type=\"number\" value=\"" + data.HangDBVND + "\"></p>";
                                                                html += "</td>";

                                                                html += "<td>";
                                                                html += "<textarea name=\"\" class=\"tb-textarea packagedescription\" rows=\"5\">" + data.Description + "</textarea>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<textarea name=\"\" class=\"tb-textarea packagecustdescription\" rows=\"5\">" + data.Khachghichu + "</textarea>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<div style=\"pointer-events: none;\">";
                                                                html += "<input class=\"upload-img\" type=\"file\" onchange=\"previewFiles(this)\" multiple title=\"\">";
                                                                html += "<button class=\"btn-upload\">Upload</button>";
                                                                html += "</div>";
                                                                html += "<div class=\"preview-img " + packageID + "\">";
                                                                if (data.IMG != null) {
                                                                    var IMG = data.IMG.split('|');
                                                                    for (var j = 0; j < IMG.length - 1; j++) {
                                                                        html += "<div class=\"img-block\">";
                                                                        html += "<img class=\"img materialboxed\" title=\"\" data-caption=\"\" src=\"" + IMG[j] + "\">";
                                                                        html += "<span class=\"material-icons red-text delete\">clear</span>";
                                                                        html += "</div>";
                                                                    }
                                                                }
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<div class=\"input-field\" style=\"pointer-events: none;\">";
                                                                html += "<select class=\"package-status-select\">";
                                                                html += "<option value=\"\" disabled>Trạng thái</option>";
                                                                var status = data.Status;
                                                                if (status == 0) {
                                                                    html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                                }
                                                                else if (status == 1) {
                                                                    html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                                }
                                                                else if (status == 2) {
                                                                    html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                                }
                                                                else if (status == 3) {
                                                                    html += "               <option value=\"3\" selected>Đã về kho đích</option>";
                                                                }
                                                                else {
                                                                    html += "<option value=\"4\" selected>Đã giao cho khách</option>";
                                                                }

                                                                html += "</select>";
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<div class=\" action-table\">";
                                                                html += "<a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "',$(this)," + packageID + ")\" class=\"tooltipped updatebutton\" data-position=\"top\" data-tooltip=\"Cập nhật thay đổi\"><i class=\"material-icons\">sync</i></a>";



                                                                html += "<a href=\"javascript:;\" onclick=\"huyxuatkho($(this)," + pID + ", " + data.MainorderID + ")\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Ẩn đi\"><i class=\"material-icons\">visibility_off</i></a>";
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "</tr>";
                                                                html += "</tbody>";

                                                                $(".orderid" + UID + "").parent().prepend(html);
                                                                moveOnTopPackage(packageID);
                                                            }
                                                        }
                                                        $('select').formSelect();
                                                        obj.val("");
                                                    }
                                                }

                                                $("#findcode").addClass('hide');
                                            }
                                        }
                                        else {
                                            $(".notexist").html(bc);
                                            $("#findcode").removeClass('hide');
                                        }
                                    }
                                    else {
                                        for (var i = 0; i < listsmallpackage.length; i++) {
                                            //document.getElementById('audio2').play();
                                            var data = listsmallpackage[i];
                                            var UID = data.UID;
                                            var Username = data.Username;
                                            var Phone = data.Phone;
                                            var packageID = data.ID;
                                            var isExist = false;
                                            if ($(".package-row").length > 0) {
                                                $(".package-row").each(function () {
                                                    var dt_packageID = $(this).attr("data-packageID");
                                                    if (packageID == dt_packageID) {
                                                        isExist = true;
                                                    }
                                                });
                                            }

                                            var check = false;
                                            $(".package-wrap").each(function () {
                                                if ($(this).attr("data-uid") == UID) {
                                                    check = true;
                                                }
                                            })

                                            var pID = UID;
                                            if (data.OrderTypeInt != 2) {
                                                if (isExist == false) {
                                                    var idpack = "bc-" + data.BarCode + "-" + packageID;
                                                    if (check == false) {
                                                        var html = '';
                                                        html += "<div class=\"package-wrap accent-2\" id=\"" + pID + "\" data-uid=\"" + UID + "\">";
                                                        html += "<div class=\"row\">";
                                                        html += "<div class=\"col s12\">";
                                                        html += "<div class=\"list-package\">";
                                                        html += "<div class=\"package-item pb-2\">";
                                                        html += "<div class=\"wrap-top-action\">";
                                                        html += "<div class=\"owner\">";
                                                        html += "<span>" + Username + "</span> | <span>" + Phone + "</span>";
                                                        html += "</div>";
                                                        html += "<div class=\"action-all\">";
                                                        html += "<a href=\"javascript:;\" onclick=\"RemoveAllByUID(" + UID + ",'" + Username + "')\" style=\"margin-right:5px;\" class=\"btn\">Ẩn tất cả</a>";
                                                        html += "<a href=\"javascript:;\" onclick=\"UpdateAllByUID()\" class=\"btn\">Cập nhật tất cả</a>";
                                                        html += "</div>";
                                                        html += "</div>";
                                                        html += "<div class=\"responsive-tb\">";
                                                        html += "<table class=\"table table-inside centered table-warehouse\">";
                                                        html += "<thead>";
                                                        html += "<tr class=\"teal darken-4\">";
                                                        html += "<th style=\"min-width: 50px;\">Order ID</th>";
                                                        html += "<th style=\"min-width: 50px;\">Loại ĐH</th>";
                                                        html += "<th style=\"min-width: 100px;\">Đơn hàng</th>";
                                                        html += "<th>Mã vận đơn</th>";
                                                        
                                                        
                                                        html += "<th style=\"min-width: auto;\">Cân nặng<br />(kg)</th>";
                                                        html += "<th class=\"size-th\">Kích thước</th>";

                                                        
                                                        
                                                        html += "<th style=\"width: 100px;\">Ghi chú</th>";
                                                        html += "<th style=\"width: 100px;\">Khách ghi chú</th>";
                                                        html += "<th style=\"min-width: 50px;\">Hình ảnh</th>";
                                                        html += "<th>Trạng thái</th>";
                                                        html += "<th style=\"min-width: 80px;\">Action</th>";
                                                        html += "</tr>";
                                                        html += "</thead>";
                                                        html += "<tbody class=\"orderid" + UID + " dh" + data.MainorderID + "\" data-orderid=\"" + data.MainorderID + "\"  data-ordertype=\"" + data.OrderTypeInt + "\">";
                                                        html += "<tr><td rowspan=\"1000\" class=\"grey lighten-2\"><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></td></tr>";

                                                        if (data.Status == 0) {
                                                            html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                        }
                                                        else if (data.Status == 1) {
                                                            html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                        }
                                                        else if (data.Status == 2) {
                                                            html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                        }
                                                        else {
                                                            html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                        }


                                                        //html += "<td rowspan=\"50\" class=\"grey lighten-2\"><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a>";
                                                        //html += "</td>";
                                                        html += "<td><span>" + data.OrderType + "</span></td>";
                                                        html += "<td>";
                                                        html += "<div class=\"tb-block\">";
                                                        html += "<p class=\"black-text\">KD</p>";
                                                        if (data.Kiemdem == "Có") {
                                                            html += "<p><a href=\"javascript:;\" onclick=\"checkProduct(" + data.MainorderID + ");\"><i class=\"material-icons green-text\">check_circle</i></a></p>";
                                                        }
                                                        else {
                                                            html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                        }
                                                        html += "</div>";
                                                        html += "<div class=\"tb-block\">";
                                                        html += "<p class=\"black-text\">ĐG</p>";
                                                        if (data.Donggo == "Có") {
                                                            html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                        }
                                                        else {
                                                            html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                        }
                                                        html += "</div>";
                                                        html += "<div class=\"tb-block\">";
                                                        html += "<p class=\"black-text\">BH</p>";
                                                        if (data.Baohiem == "Có") {
                                                            html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                        }
                                                        else {
                                                            html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                        }
                                                        html += "</div>";
                                                        html += "</td>";
                                                        html += "<td><span>" + data.BarCode + "</span></td>";
                                                       
                                                        
                                                        html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                        html += "<span class=\"packageWeightUpdate\" type=\"text\" value=\"" + data.Weight + "\"> " + data.Weight + "</span></td>";
                                                        html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                        html += "<p><span class=\"lb\">d:</span><input class=\"lengthsize\" type=\"text\" value=\"" + data.dai + "\"></p>";
                                                        html += "<p class=\"operator\"><span class=\"lb\">r:</span><input class=\"widthsize\" type=\"text\" value=\"" + data.rong + "\"></p>";
                                                        html += "<p class=\"operator\"><span class=\"lb\">c:</span><input class=\"heightsize\" type=\"text\" value=\"" + data.cao + "\"></p>";
                                                        html += "</td>";

                                                       
                                                       
                                                        html += "<td>";
                                                        html += "<textarea name=\"\" class=\"tb-textarea packagedescription\" rows=\"5\">" + data.Description + "</textarea>";
                                                        html += "</td>";
                                                        html += "<td>";
                                                        html += "<textarea name=\"\" class=\"tb-textarea packagecustdescription\" rows=\"5\">" + data.Khachghichu + "</textarea>";
                                                        html += "</td>";
                                                        html += "<td>";
                                                        html += "<div style=\"pointer-events: none;\">";
                                                        html += "<input class=\"upload-img\" type=\"file\" onchange=\"previewFiles(this)\" multiple title=\"\">";
                                                        html += "<button class=\"btn-upload\">Upload</button>";
                                                        html += "</div>";
                                                        html += "<div class=\"preview-img " + packageID + "\">";
                                                        if (data.IMG != null) {
                                                            var IMG = data.IMG.split('|');
                                                            for (var j = 0; j < IMG.length - 1; j++) {
                                                                html += "<div class=\"img-block\">";
                                                                html += "<img class=\"img materialboxed\" title=\"\" data-caption=\"\" src=\"" + IMG[j] + "\">";
                                                                html += "<span class=\"material-icons red-text delete\">clear</span>";
                                                                html += "</div>";
                                                            }
                                                        }
                                                        html += "</div>";
                                                        html += "</td>";
                                                        html += "<td>";
                                                        html += "<div class=\"input-field\" style=\"pointer-events: none;\">";
                                                        html += "<select class=\"package-status-select\">";
                                                        html += "<option value=\"\" disabled>Trạng thái</option>";
                                                        var status = data.Status;
                                                        if (status < 3) {
                                                            if (status == 0) {
                                                                html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                            }
                                                            else if (status == 1) {
                                                                html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                                html += "<option value=\"0\">Hủy kiện</option>";
                                                            }
                                                            else if (status == 2) {
                                                                html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                            }
                                                        }
                                                        else {
                                                            if (status == 0) {
                                                                html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                            }
                                                            else if (status == 1) {
                                                                html += "<option value=\"1\" selected>Mới đặt - chưa về kho TQ</option>";
                                                            }
                                                            else if (status == 2) {
                                                                html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                            }
                                                            else if (status == 3) {
                                                                html += "<option value=\"3\" selected>Đã về kho đích</option>";
                                                            }
                                                            else {
                                                                html += "<option value=\"4\" selected>Đã giao cho khách</option>";
                                                            }
                                                        }

                                                        html += "</select>";
                                                        html += "</div>";
                                                        html += "</td>";
                                                        html += "<td>";
                                                        html += "<div class=\" action-table\">";
                                                        html += "<a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "',$(this)," + packageID + ")\" class=\"tooltipped updatebutton\" data-position=\"top\" data-tooltip=\"Cập nhật thay đổi\"><i class=\"material-icons\">sync</i></a>";




                                                        html += "<a href=\"javascript:;\" onclick=\"huyxuatkho($(this)," + pID + ", " + data.MainorderID + ")\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Ẩn đi\"><i class=\"material-icons\">visibility_off</i></a>";
                                                        html += "</div>";
                                                        html += "</td>";
                                                        html += "</tr>";
                                                        html += "</tbody>";
                                                        html += "</table>";
                                                        html += "</div>";
                                                        html += "</div>";
                                                        html += "</div>";
                                                        html += "</div>";
                                                        html += "</div>";
                                                        html += "</div>";

                                                        $("#listpackage").prepend(html);
                                                        moveOnTopPackage(packageID);
                                                    }
                                                    else {
                                                        var MainID = $(".orderid" + UID + "").attr('data-orderid');
                                                        if (MainID == data.MainorderID) {
                                                            var otype = $(".orderid" + UID + "").attr('data-ordertype');
                                                            if (otype == data.OrderTypeInt) {
                                                                var html = '';
                                                                // html += "<tr><td rowspan=\"1000\" class=\"grey lighten-2\"><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></td></tr>";
                                                                if (data.Status == 0) {
                                                                    html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                else if (data.Status == 1) {
                                                                    html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                else if (data.Status == 2) {
                                                                    html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                else {
                                                                    html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                //html += "<td rowspan=\"50\" class=\"grey lighten-2\"><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a>";
                                                                html += "</td>";
                                                                html += "<td><span>" + data.OrderType + "</span></td>";
                                                                html += "<td>";
                                                                html += "<div class=\"tb-block\">";
                                                                html += "<p class=\"black-text\">KD</p>";
                                                                if (data.Kiemdem == "Có") {
                                                                    html += "<p><a href=\"javascript:;\" onclick=\"checkProduct(" + data.MainorderID + ");\"><i class=\"material-icons green-text\">check_circle</i></a></p>";
                                                                }
                                                                else {
                                                                    html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                }
                                                                html += "</div>";
                                                                html += "<div class=\"tb-block\">";
                                                                html += "<p class=\"black-text\">ĐG</p>";
                                                                if (data.Donggo == "Có") {
                                                                    html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                }
                                                                else {
                                                                    html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                }
                                                                html += "</div>";
                                                                html += "<div class=\"tb-block\">";
                                                                html += "<p class=\"black-text\">BH</p>";
                                                                if (data.Baohiem == "Có") {
                                                                    html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                }
                                                                else {
                                                                    html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                }
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "<td><span>" + data.BarCode + "</span></td>";
                                                               
                                                                
                                                                html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                                html += "<span class=\"packageWeightUpdate\" type=\"text\" value=\"" + data.Weight + "\"> " + data.Weight + "</span></td>";
                                                                html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                                html += "<p><span class=\"lb\">d:</span><input class=\"lengthsize\" type=\"text\" value=\"" + data.dai + "\"></p>";
                                                                html += "<p class=\"operator\"><span class=\"lb\">r:</span><input class=\"widthsize\" type=\"text\" value=\"" + data.rong + "\"></p>";
                                                                html += "<p class=\"operator\"><span class=\"lb\">c:</span><input class=\"heightsize\" type=\"text\" value=\"" + data.cao + "\"></p>";
                                                                html += "</td>";

                                                               
                                                                
                                                                html += "<td>";
                                                                html += "<textarea name=\"\" class=\"tb-textarea packagedescription\" rows=\"5\">" + data.Description + "</textarea>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<textarea name=\"\" class=\"tb-textarea packagecustdescription\" rows=\"5\">" + data.Khachghichu + "</textarea>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<div style=\"pointer-events: none;\">";
                                                                html += "<input class=\"upload-img\" type=\"file\" onchange=\"previewFiles(this)\" multiple title=\"\">";
                                                                html += "<button class=\"btn-upload\">Upload</button>";
                                                                html += "</div>";
                                                                html += "<div class=\"preview-img " + packageID + "\">";
                                                                if (data.IMG != null) {
                                                                    var IMG = data.IMG.split('|');
                                                                    for (var j = 0; j < IMG.length - 1; j++) {
                                                                        html += "<div class=\"img-block\">";
                                                                        html += "<img class=\"img materialboxed\" title=\"\" data-caption=\"\" src=\"" + IMG[j] + "\">";
                                                                        html += "<span class=\"material-icons red-text delete\">clear</span>";
                                                                        html += "</div>";
                                                                    }
                                                                }
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<div class=\"input-field\" style=\"pointer-events: none;\">";
                                                                html += "<select class=\"package-status-select\">";
                                                                html += "<option value=\"\" disabled>Trạng thái</option>";
                                                                var status = data.Status;
                                                                if (status < 3) {
                                                                    if (status == 0) {
                                                                        html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                                    }
                                                                    else if (status == 1) {
                                                                        html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                                        html += "<option value=\"0\">Hủy kiện</option>";
                                                                    }
                                                                    else if (status == 2) {
                                                                        html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                                    }
                                                                }
                                                                else {
                                                                    if (status == 0) {
                                                                        html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                                    }
                                                                    else if (status == 1) {
                                                                        html += "<option value=\"1\" selected>Mới đặt - chưa về kho TQ</option>";
                                                                    }
                                                                    else if (status == 2) {
                                                                        html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                                    }
                                                                    else if (status == 3) {
                                                                        html += "<option value=\"3\" selected>Đã về kho đích</option>";
                                                                    }
                                                                    else {
                                                                        html += "<option value=\"4\" selected>Đã giao cho khách</option>";
                                                                    }
                                                                }

                                                                html += "</select>";
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<div class=\" action-table\">";
                                                                html += "<a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "',$(this)," + packageID + ")\" class=\"tooltipped updatebutton\" data-position=\"top\" data-tooltip=\"Cập nhật thay đổi\"><i class=\"material-icons\">sync</i></a>";




                                                                html += "<a href=\"javascript:;\" onclick=\"huyxuatkho($(this)," + pID + ", " + data.MainorderID + ")\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Ẩn đi\"><i class=\"material-icons\">visibility_off</i></a>";
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "</tr>";

                                                                $("." + data.MainorderID + "").parent().append(html);
                                                                moveOnTopPackage(packageID);
                                                            }
                                                            else {
                                                                var html = '';
                                                                html += "<tbody class=\"orderid" + UID + " dh" + data.MainorderID + "\" data-orderid=\"" + data.MainorderID + "\"  data-ordertype=\"" + data.OrderTypeInt + "\">";
                                                                html += "<tr><td rowspan=\"1000\" class=\"grey lighten-2\"><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></td></tr>";
                                                                if (data.Status == 0) {
                                                                    html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                else if (data.Status == 1) {
                                                                    html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                else if (data.Status == 2) {
                                                                    html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                else {
                                                                    html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                //html += "<td rowspan=\"50\" class=\"grey lighten-2\"><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a>";
                                                                //html += "</td>";
                                                                html += "<td><span>" + data.OrderType + "</span></td>";
                                                                html += "<td>";
                                                                html += "<div class=\"tb-block\">";
                                                                html += "<p class=\"black-text\">KD</p>";
                                                                if (data.Kiemdem == "Có") {
                                                                    html += "<p><a href=\"javascript:;\" onclick=\"checkProduct(" + data.MainorderID + ");\"><i class=\"material-icons green-text\">check_circle</i></a></p>";
                                                                }
                                                                else {
                                                                    html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                }
                                                                html += "</div>";
                                                                html += "<div class=\"tb-block\">";
                                                                html += "<p class=\"black-text\">ĐG</p>";
                                                                if (data.Donggo == "Có") {
                                                                    html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                }
                                                                else {
                                                                    html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                }
                                                                html += "</div>";
                                                                html += "<div class=\"tb-block\">";
                                                                html += "<p class=\"black-text\">BH</p>";
                                                                if (data.Baohiem == "Có") {
                                                                    html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                }
                                                                else {
                                                                    html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                }
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "<td><span>" + data.BarCode + "</span></td>";
                                                               
                                                                
                                                                html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                                html += "<span class=\"packageWeightUpdate\" type=\"text\" value=\"" + data.Weight + "\"> " + data.Weight + "</span></td>";
                                                                html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                                html += "<p><span class=\"lb\">d:</span><input class=\"lengthsize\" type=\"text\" value=\"" + data.dai + "\"></p>";
                                                                html += "<p class=\"operator\"><span class=\"lb\">r:</span><input class=\"widthsize\" type=\"text\" value=\"" + data.rong + "\"></p>";
                                                                html += "<p class=\"operator\"><span class=\"lb\">c:</span><input class=\"heightsize\" type=\"text\" value=\"" + data.cao + "\"></p>";
                                                                html += "</td>";

                                                               
                                                               
                                                                html += "<td>";
                                                                html += "<textarea name=\"\" class=\"tb-textarea packagedescription\" rows=\"5\">" + data.Description + "</textarea>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<textarea name=\"\" class=\"tb-textarea packagecustdescription\" rows=\"5\">" + data.Khachghichu + "</textarea>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<div style=\"pointer-events: none;\">";
                                                                html += "<input class=\"upload-img\" type=\"file\" onchange=\"previewFiles(this)\" multiple title=\"\">";
                                                                html += "<button class=\"btn-upload\">Upload</button>";
                                                                html += "</div>";
                                                                html += "<div class=\"preview-img " + packageID + "\">";
                                                                if (data.IMG != null) {
                                                                    var IMG = data.IMG.split('|');
                                                                    for (var j = 0; j < IMG.length - 1; j++) {
                                                                        html += "<div class=\"img-block\">";
                                                                        html += "<img class=\"img materialboxed\" title=\"\" data-caption=\"\" src=\"" + IMG[j] + "\">";
                                                                        html += "<span class=\"material-icons red-text delete\">clear</span>";
                                                                        html += "</div>";
                                                                    }
                                                                }
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<div class=\"input-field\" style=\"pointer-events: none;\">";
                                                                html += "<select class=\"package-status-select\">";
                                                                html += "<option value=\"\" disabled>Trạng thái</option>";
                                                                var status = data.Status;
                                                                if (status < 3) {
                                                                    if (status == 0) {
                                                                        html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                                    }
                                                                    else if (status == 1) {
                                                                        html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                                        html += "<option value=\"0\">Hủy kiện</option>";
                                                                    }
                                                                    else if (status == 2) {
                                                                        html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                                    }
                                                                }
                                                                else {
                                                                    if (status == 0) {
                                                                        html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                                    }
                                                                    else if (status == 1) {
                                                                        html += "<option value=\"1\" selected>Mới đặt - chưa về kho TQ</option>";
                                                                    }
                                                                    else if (status == 2) {
                                                                        html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                                    }
                                                                    else if (status == 3) {
                                                                        html += "<option value=\"3\" selected>Đã về kho đích</option>";
                                                                    }
                                                                    else {
                                                                        html += "<option value=\"4\" selected>Đã giao cho khách</option>";
                                                                    }
                                                                }

                                                                html += "</select>";
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<div class=\" action-table\">";
                                                                html += "<a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "',$(this)," + packageID + ")\" class=\"tooltipped updatebutton\" data-position=\"top\" data-tooltip=\"Cập nhật thay đổi\"><i class=\"material-icons\">sync</i></a>";




                                                                html += "<a href=\"javascript:;\" onclick=\"huyxuatkho($(this)," + pID + ", " + data.MainorderID + ")\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Ẩn đi\"><i class=\"material-icons\">visibility_off</i></a>";
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "</tr>";
                                                                html += "</tbody>";

                                                                $(".orderid" + UID + "").parent().prepend(html);
                                                                moveOnTopPackage(packageID);
                                                            }
                                                        }
                                                        else {
                                                            var html = '';
                                                            html += "<tbody class=\"orderid" + UID + " dh" + data.MainorderID + "\" data-orderid=\"" + data.MainorderID + "\"  data-ordertype=\"" + data.OrderTypeInt + "\">";
                                                            html += "<tr><td rowspan=\"1000\" class=\"grey lighten-2\"><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a></td></tr>";
                                                            if (data.Status == 0) {
                                                                html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                            }
                                                            else if (data.Status == 1) {
                                                                html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                            }
                                                            else if (data.Status == 2) {
                                                                html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                            }
                                                            else {
                                                                html += "<tr class=\"package-row greisvekhoen slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                            }
                                                            //html += "<td rowspan=\"50\" class=\"grey lighten-2\"><a href=\"/manager/OrderDetail.aspx?id=" + data.MainorderID + "\" target=\"_blank\">" + data.MainorderID + "</a>";
                                                            //html += "</td>";
                                                            html += "<td><span>" + data.OrderType + "</span></td>";
                                                            html += "<td>";
                                                            html += "<div class=\"tb-block\">";
                                                            html += "<p class=\"black-text\">KD</p>";
                                                            if (data.Kiemdem == "Có") {
                                                                html += "<p><a href=\"javascript:;\" onclick=\"checkProduct(" + data.MainorderID + ");\"><i class=\"material-icons green-text\">check_circle</i></a></p>";
                                                            }
                                                            else {
                                                                html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                            }
                                                            html += "</div>";
                                                            html += "<div class=\"tb-block\">";
                                                            html += "<p class=\"black-text\">ĐG</p>";
                                                            if (data.Donggo == "Có") {
                                                                html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                            }
                                                            else {
                                                                html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                            }
                                                            html += "</div>";
                                                            html += "<div class=\"tb-block\">";
                                                            html += "<p class=\"black-text\">BH</p>";
                                                            if (data.Baohiem == "Có") {
                                                                html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                            }
                                                            else {
                                                                html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                            }
                                                            html += "</div>";
                                                            html += "</td>";
                                                            html += "<td><span>" + data.BarCode + "</span></td>";
                                                           
                                                            
                                                            html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                            html += "<span class=\"packageWeightUpdate\" type=\"text\" value=\"" + data.Weight + "\"> " + data.Weight + "</span></td>";
                                                            html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                            html += "<p><span class=\"lb\">d:</span><input class=\"lengthsize\" type=\"text\" value=\"" + data.dai + "\"></p>";
                                                            html += "<p class=\"operator\"><span class=\"lb\">r:</span><input class=\"widthsize\" type=\"text\" value=\"" + data.rong + "\"></p>";
                                                            html += "<p class=\"operator\"><span class=\"lb\">c:</span><input class=\"heightsize\" type=\"text\" value=\"" + data.cao + "\"></p>";
                                                            html += "</td>";

                                                           
                                                           
                                                            html += "<td>";
                                                            html += "<textarea name=\"\" class=\"tb-textarea packagedescription\" rows=\"5\">" + data.Description + "</textarea>";
                                                            html += "</td>";
                                                            html += "<td>";
                                                            html += "<textarea name=\"\" class=\"tb-textarea packagecustdescription\" rows=\"5\">" + data.Khachghichu + "</textarea>";
                                                            html += "</td>";
                                                            html += "<td>";
                                                            html += "<div style=\"pointer-events: none;\">";
                                                            html += "<input class=\"upload-img\" type=\"file\" onchange=\"previewFiles(this)\" multiple title=\"\">";
                                                            html += "<button class=\"btn-upload\">Upload</button>";
                                                            html += "</div>";
                                                            html += "<div class=\"preview-img " + packageID + "\">";
                                                            if (data.IMG != null) {
                                                                var IMG = data.IMG.split('|');
                                                                for (var j = 0; j < IMG.length - 1; j++) {
                                                                    html += "<div class=\"img-block\">";
                                                                    html += "<img class=\"img materialboxed\" title=\"\" data-caption=\"\" src=\"" + IMG[j] + "\">";
                                                                    html += "<span class=\"material-icons red-text delete\">clear</span>";
                                                                    html += "</div>";
                                                                }
                                                            }
                                                            html += "</div>";
                                                            html += "</td>";
                                                            html += "<td>";
                                                            html += "<div class=\"input-field\" style=\"pointer-events: none;\">";
                                                            html += "<select class=\"package-status-select\">";
                                                            html += "<option value=\"\" disabled>Trạng thái</option>";
                                                            var status = data.Status;
                                                            if (status < 3) {
                                                                if (status == 0) {
                                                                    html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                                }
                                                                else if (status == 1) {
                                                                    html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                                    html += "<option value=\"0\">Hủy kiện</option>";
                                                                }
                                                                else if (status == 2) {
                                                                    html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                                }
                                                            }
                                                            else {
                                                                if (status == 0) {
                                                                    html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                                }
                                                                else if (status == 1) {
                                                                    html += "<option value=\"1\" selected>Mới đặt - chưa về kho TQ</option>";
                                                                }
                                                                else if (status == 2) {
                                                                    html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                                }
                                                                else if (status == 3) {
                                                                    html += "<option value=\"3\" selected>Đã về kho đích</option>";
                                                                }
                                                                else {
                                                                    html += "<option value=\"4\" selected>Đã giao cho khách</option>";
                                                                }
                                                            }

                                                            html += "</select>";
                                                            html += "</div>";
                                                            html += "</td>";
                                                            html += "<td>";
                                                            html += "<div class=\" action-table\">";
                                                            html += "<a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "',$(this)," + packageID + ")\" class=\"tooltipped updatebutton\" data-position=\"top\" data-tooltip=\"Cập nhật thay đổi\"><i class=\"material-icons\">sync</i></a>";




                                                            html += "<a href=\"javascript:;\" onclick=\"huyxuatkho($(this)," + pID + ", " + data.MainorderID + ")\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Ẩn đi\"><i class=\"material-icons\">visibility_off</i></a>";
                                                            html += "</div>";
                                                            html += "</td>";
                                                            html += "</tr>";
                                                            html += "</tbody>";

                                                            $(".orderid" + UID + "").parent().prepend(html);
                                                            moveOnTopPackage(packageID);
                                                        }
                                                    }
                                                    $('select').formSelect();
                                                    obj.val("");
                                                }
                                                else {
                                                    obj.val("");
                                                    var r = confirm('Mã này đã scan rồi!');
                                                    if (r) {
                                                        AddPackageSame(bc);
                                                    }

                                                    //addLoading();
                                                    //$(".package-row").each(function () {
                                                    //    var ordercodetrans = $(this).attr("data-barcode");
                                                    //    if (ordercodetrans == bc) {
                                                    //        var packageID = $(this).attr("data-packageID");
                                                    //        var idpack = "bc-" + bc + "-" + packageID;
                                                    //        updateWeight(bc, $(this).find(".updatebutton"), packageID);
                                                    //    }
                                                    //})
                                                }
                                            }
                                            else if (data.OrderTypeInt != 1) {
                                                if (isExist == false) {
                                                    var idpack = "bc-" + data.BarCode + "-" + packageID;
                                                    if (check == false) {
                                                        var html = '';
                                                        html += "<div class=\"package-wrap accent-2\" id=\"" + pID + "\" data-uid=\"" + UID + "\">";
                                                        html += "<div class=\"row\">";
                                                        html += "<div class=\"col s12\">";
                                                        html += "<div class=\"list-package\">";
                                                        html += "<div class=\"package-item pb-2\">";
                                                        html += "<div class=\"wrap-top-action\">";
                                                        html += "<div class=\"owner\">";
                                                        html += "<span>" + Username + "</span> | <span>" + Phone + "</span>";
                                                        html += "</div>";
                                                        html += "<div class=\"action-all\">";
                                                        html += "<a href=\"javascript:;\" onclick=\"RemoveAllByUID(" + UID + ",'" + Username + "')\" style=\"margin-right:5px;\" class=\"btn\">Ẩn tất cả</a>";
                                                        html += "<a href=\"javascript:;\" onclick=\"UpdateAllByUID()\" class=\"btn\">Cập nhật tất cả</a>";
                                                        html += "</div>";
                                                        html += "</div>";
                                                        html += "<div class=\"responsive-tb\">";
                                                        html += "<table class=\"table table-inside centered table-warehouse\">";
                                                        html += "<thead>";
                                                        html += "<tr class=\"teal darken-4\">";
                                                        html += "<th style=\"min-width: 50px;\">Order ID</th>";
                                                        html += "<th style=\"min-width: 50px;\">Loại ĐH</th>";
                                                        html += "<th style=\"min-width: 100px;\">Đơn hàng</th>";
                                                        html += "<th>Mã vận đơn</th>";
                                                        
                                                        
                                                        html += "<th style=\"min-width: auto;\">Cân nặng<br />(kg)</th>";
                                                        html += "<th class=\"size-th\">Kích thước</th>";

                                                        html += "<th style=\"min-width: 100px\">Cước vật tư</th>";
                                                        html += "<th>Phụ phí ĐB</th>";
                                                        html += "<th style=\"width: 100px;\">Ghi chú</th>";
                                                        html += "<th style=\"width: 100px;\">Khách ghi chú</th>";
                                                        html += "<th style=\"min-width: 50px;\">Hình ảnh</th>";
                                                        html += "<th>Trạng thái</th>";
                                                        html += "<th style=\"min-width: 80px;\">Action</th>";
                                                        html += "</tr>";
                                                        html += "</thead>";
                                                        html += "<tbody class=\"orderid" + UID + " dh" + data.MainorderID + "\" data-orderid=\"" + data.MainorderID + "\"  data-ordertype=\"" + data.OrderTypeInt + "\">";
                                                        html += "<tr><td rowspan=\"1000\" class=\"grey lighten-2\"><a  target=\"_blank\">" + data.TransportationID + "</a></td></tr>";

                                                        if (data.Status == 0) {
                                                            html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                        }
                                                        else if (data.Status == 1) {
                                                            html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                        }
                                                        else if (data.Status == 2) {
                                                            html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                        }
                                                        else {
                                                            html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                        }


                                                        //html += "<td rowspan=\"50\" class=\"grey lighten-2\"><a  target=\"_blank\">" + data.TransportationID + "</a>";
                                                        //html += "</td>";
                                                        html += "<td><span>" + data.OrderType + "</span></td>";
                                                        html += "<td>";
                                                        html += "<div class=\"tb-block\">";
                                                        html += "<p class=\"black-text\">KD</p>";
                                                        if (data.Kiemdem == "Có") {
                                                            html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                        }
                                                        else {
                                                            html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                        }
                                                        html += "</div>";
                                                        html += "<div class=\"tb-block\">";
                                                        html += "<p class=\"black-text\">ĐG</p>";
                                                        if (data.Donggo == "Có") {
                                                            html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                        }
                                                        else {
                                                            html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                        }
                                                        html += "</div>";
                                                        html += "<div class=\"tb-block\">";
                                                        html += "<p class=\"black-text\">BH</p>";
                                                        if (data.Baohiem == "Có") {
                                                            html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                        }
                                                        else {
                                                            html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                        }
                                                        html += "</div>";
                                                        html += "</td>";
                                                        html += "<td><span>" + data.BarCode + "</span></td>";
                                                       
                                                        
                                                        html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                        html += "<span class=\"packageWeightUpdate\" type=\"text\" value=\"" + data.Weight + "\"> " + data.Weight + "</span></td>";
                                                        html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                        html += "<p><span class=\"lb\">d:</span><input class=\"lengthsize\" type=\"text\" value=\"" + data.dai + "\"></p>";
                                                        html += "<p class=\"operator\"><span class=\"lb\">r:</span><input class=\"widthsize\" type=\"text\" value=\"" + data.rong + "\"></p>";
                                                        html += "<p class=\"operator\"><span class=\"lb\">c:</span><input class=\"heightsize\" type=\"text\" value=\"" + data.cao + "\"></p>";
                                                        html += "</td>";

                                                        //                                                                    html += "<td><span type=\"text\" value=\"" + data.Loaisanpham + "\" class=\"packageproducttype\">"+ data.Loaisanpham+" </span></td>";
                                                        html += "<td class=\"\">";
                                                        html += "<p class=\"operator\"><span class=\"lb\">CNY:</span><input class=\"packageadditionfeeCYN\" oninput=\"getPriceFee('packageadditionfeeCYN',$(this),'cyn')\" type=\"number\" value=\"" + data.CuocvattuCYN + "\"></p>";
                                                        html += "<p class=\"operator\"><span class=\"lb\">VND:</span><input class=\"packageadditionfeeVND\" oninput=\"getPriceFee('packageadditionfeeVND',$(this),'vnd')\" type=\"number\" value=\"" + data.CuocvattuVND + "\"></p>";
                                                        html += "</td>";

                                                        //html += "<td>";
                                                        //html += "<div>";
                                                        //                                                            html += "<span type=\"text\" value=\"" + data.NVKiemdem + "\" class=\"packagedStaffNoteCheck\">"+data.NVKiemdem+"</span>";
                                                        //html += "</div>";
                                                        //html += "</td>";


                                                        html += "<td class=\"\">";
                                                        html += "<p class=\"operator\"><span class=\"lb\">CNY:</span><input class=\"packageSensorCYN\" oninput=\"getPriceFee('packageSensorCYN',$(this),'cyn')\" type=\"number\" value=\"" + data.HangDBCYN + "\"></p>";
                                                        html += "<p class=\"operator\"><span class=\"lb\">VND:</span><input class=\"packageSensorVND\" oninput=\"getPriceFee('packageSensorVND',$(this),'vnd')\" type=\"number\" value=\"" + data.HangDBVND + "\"></p>";
                                                        html += "</td>";

                                                        html += "<td>";
                                                        html += "<textarea name=\"\" class=\"tb-textarea packagedescription\" rows=\"5\">" + data.Description + "</textarea>";
                                                        html += "</td>";
                                                        html += "<td>";
                                                        html += "<textarea name=\"\" class=\"tb-textarea packagecustdescription\" rows=\"5\">" + data.Khachghichu + "</textarea>";
                                                        html += "</td>";
                                                        html += "<td>";
                                                        html += "<div style=\"pointer-events: none;\">";
                                                        html += "<input class=\"upload-img\" type=\"file\" onchange=\"previewFiles(this)\" multiple title=\"\">";
                                                        html += "<button class=\"btn-upload\">Upload</button>";
                                                        html += "</div>";
                                                        html += "<div class=\"preview-img " + packageID + "\">";
                                                        if (data.IMG != null) {
                                                            var IMG = data.IMG.split('|');
                                                            for (var j = 0; j < IMG.length - 1; j++) {
                                                                html += "<div class=\"img-block\">";
                                                                html += "<img class=\"img materialboxed\" title=\"\" data-caption=\"\" src=\"" + IMG[j] + "\">";
                                                                html += "<span class=\"material-icons red-text delete\">clear</span>";
                                                                html += "</div>";
                                                            }
                                                        }
                                                        html += "</div>";
                                                        html += "</td>";
                                                        html += "<td>";
                                                        html += "<div class=\"input-field\" style=\"pointer-events: none;\">";
                                                        html += "<select class=\"package-status-select\">";
                                                        html += "<option value=\"\" disabled>Trạng thái</option>";
                                                        var status = data.Status;
                                                        if (status < 3) {
                                                            if (status == 0) {
                                                                html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                            }
                                                            else if (status == 1) {
                                                                html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                                html += "<option value=\"0\">Hủy kiện</option>";
                                                            }
                                                            else if (status == 2) {
                                                                html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                            }
                                                        }
                                                        else {
                                                            if (status == 0) {
                                                                html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                            }
                                                            else if (status == 1) {
                                                                html += "<option value=\"1\" selected>Mới đặt - chưa về kho TQ</option>";
                                                            }
                                                            else if (status == 2) {
                                                                html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                            }
                                                            else if (status == 3) {
                                                                html += "<option value=\"3\" selected>Đã về kho đích</option>";
                                                            }
                                                            else {
                                                                html += "<option value=\"4\" selected>Đã giao cho khách</option>";
                                                            }
                                                        }

                                                        html += "</select>";
                                                        html += "</div>";
                                                        html += "</td>";
                                                        html += "<td>";
                                                        html += "<div class=\" action-table\">";
                                                        html += "<a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "',$(this)," + packageID + ")\" class=\"tooltipped updatebutton\" data-position=\"top\" data-tooltip=\"Cập nhật thay đổi\"><i class=\"material-icons\">sync</i></a>";



                                                        html += "<a href=\"javascript:;\" onclick=\"huyxuatkho($(this)," + pID + ", " + data.MainorderID + ")\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Ẩn đi\"><i class=\"material-icons\">visibility_off</i></a>";
                                                        html += "</div>";
                                                        html += "</td>";
                                                        html += "</tr>";
                                                        html += "</tbody>";
                                                        html += "</table>";
                                                        html += "</div>";
                                                        html += "</div>";
                                                        html += "</div>";
                                                        html += "</div>";
                                                        html += "</div>";
                                                        html += "</div>";

                                                        $("#listpackage").prepend(html);
                                                        moveOnTopPackage(packageID);
                                                    }
                                                    else {
                                                        var MainID = $(".orderid" + UID + "").attr('data-orderid');
                                                        if (MainID == data.MainorderID) {
                                                            var otype = $(".orderid" + UID + "").attr('data-ordertype');
                                                            if (otype == data.OrderTypeInt) {
                                                                var html = '';
                                                                // html += "<tr><td rowspan=\"1000\" class=\"grey lighten-2\"><a  target=\"_blank\">" + data.TransportationID + "</a></td></tr>";
                                                                if (data.Status == 0) {
                                                                    html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                else if (data.Status == 1) {
                                                                    html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                else if (data.Status == 2) {
                                                                    html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                else {
                                                                    html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                //html += "<td rowspan=\"50\" class=\"grey lighten-2\"><a  target=\"_blank\">" + data.TransportationID + "</a>";
                                                                html += "</td>";
                                                                html += "<td><span>" + data.OrderType + "</span></td>";
                                                                html += "<td>";
                                                                html += "<div class=\"tb-block\">";
                                                                html += "<p class=\"black-text\">KD</p>";
                                                                if (data.Kiemdem == "Có") {
                                                                    html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                }
                                                                else {
                                                                    html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                }
                                                                html += "</div>";
                                                                html += "<div class=\"tb-block\">";
                                                                html += "<p class=\"black-text\">ĐG</p>";
                                                                if (data.Donggo == "Có") {
                                                                    html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                }
                                                                else {
                                                                    html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                }
                                                                html += "</div>";
                                                                html += "<div class=\"tb-block\">";
                                                                html += "<p class=\"black-text\">BH</p>";
                                                                if (data.Baohiem == "Có") {
                                                                    html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                }
                                                                else {
                                                                    html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                }
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "<td><span>" + data.BarCode + "</span></td>";
                                                               
                                                                
                                                                html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                                html += "<span class=\"packageWeightUpdate\" type=\"text\" value=\"" + data.Weight + "\"> " + data.Weight + "</span></td>";
                                                                html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                                html += "<p><span class=\"lb\">d:</span><input class=\"lengthsize\" type=\"text\" value=\"" + data.dai + "\"></p>";
                                                                html += "<p class=\"operator\"><span class=\"lb\">r:</span><input class=\"widthsize\" type=\"text\" value=\"" + data.rong + "\"></p>";
                                                                html += "<p class=\"operator\"><span class=\"lb\">c:</span><input class=\"heightsize\" type=\"text\" value=\"" + data.cao + "\"></p>";
                                                                html += "</td>";

                                                                //html += "<td><input type=\"text\" value=\"" + data.Loaisanpham + "\" class=\"packageproducttype\"></td>";
                                                                html += "<td class=\"\">";
                                                                html += "<p class=\"operator\"><span class=\"lb\">CNY:</span><input class=\"packageadditionfeeCYN\" oninput=\"getPriceFee('packageadditionfeeCYN',$(this),'cyn')\" type=\"number\" value=\"" + data.CuocvattuCYN + "\"></p>";
                                                                html += "<p class=\"operator\"><span class=\"lb\">VND:</span><input class=\"packageadditionfeeVND\" oninput=\"getPriceFee('packageadditionfeeVND',$(this),'vnd')\" type=\"number\" value=\"" + data.CuocvattuVND + "\"></p>";
                                                                html += "</td>";


                                                                //html += "<td>";
                                                                //html += "<div>";
                                                                //                                                            html += "<span type=\"text\" value=\"" + data.NVKiemdem + "\" class=\"packagedStaffNoteCheck\">"+data.NVKiemdem+"</span>";
                                                                //html += "</div>";
                                                                //html += "</td>";


                                                                html += "<td class=\"\">";
                                                                html += "<p class=\"operator\"><span class=\"lb\">CNY:</span><input class=\"packageSensorCYN\" oninput=\"getPriceFee('packageSensorCYN',$(this),'cyn')\" type=\"number\" value=\"" + data.HangDBCYN + "\"></p>";
                                                                html += "<p class=\"operator\"><span class=\"lb\">VND:</span><input class=\"packageSensorVND\" oninput=\"getPriceFee('packageSensorVND',$(this),'vnd')\" type=\"number\" value=\"" + data.HangDBVND + "\"></p>";
                                                                html += "</td>";


                                                                html += "<td>";
                                                                html += "<textarea name=\"\" class=\"tb-textarea packagedescription\" rows=\"5\">" + data.Description + "</textarea>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<textarea name=\"\" class=\"tb-textarea packagecustdescription\" rows=\"5\">" + data.Khachghichu + "</textarea>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<div style=\"pointer-events: none;\">";
                                                                html += "<input class=\"upload-img\" type=\"file\" onchange=\"previewFiles(this)\" multiple title=\"\">";
                                                                html += "<button class=\"btn-upload\">Upload</button>";
                                                                html += "</div>";
                                                                html += "<div class=\"preview-img " + packageID + "\">";
                                                                if (data.IMG != null) {
                                                                    var IMG = data.IMG.split('|');
                                                                    for (var j = 0; j < IMG.length - 1; j++) {
                                                                        html += "<div class=\"img-block\">";
                                                                        html += "<img class=\"img materialboxed\" title=\"\" data-caption=\"\" src=\"" + IMG[j] + "\">";
                                                                        html += "<span class=\"material-icons red-text delete\">clear</span>";
                                                                        html += "</div>";
                                                                    }
                                                                }
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<div class=\"input-field\" style=\"pointer-events: none;\">";
                                                                html += "<select class=\"package-status-select\">";
                                                                html += "<option value=\"\" disabled>Trạng thái</option>";
                                                                var status = data.Status;
                                                                if (status < 3) {
                                                                    if (status == 0) {
                                                                        html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                                    }
                                                                    else if (status == 1) {
                                                                        html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                                        html += "<option value=\"0\">Hủy kiện</option>";
                                                                    }
                                                                    else if (status == 2) {
                                                                        html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                                    }
                                                                }
                                                                else {
                                                                    if (status == 0) {
                                                                        html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                                    }
                                                                    else if (status == 1) {
                                                                        html += "<option value=\"1\" selected>Mới đặt - chưa về kho TQ</option>";
                                                                    }
                                                                    else if (status == 2) {
                                                                        html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                                    }
                                                                    else if (status == 3) {
                                                                        html += "<option value=\"3\" selected>Đã về kho đích</option>";
                                                                    }
                                                                    else {
                                                                        html += "<option value=\"4\" selected>Đã giao cho khách</option>";
                                                                    }
                                                                }

                                                                html += "</select>";
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<div class=\" action-table\">";
                                                                html += "<a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "',$(this)," + packageID + ")\" class=\"tooltipped updatebutton\" data-position=\"top\" data-tooltip=\"Cập nhật thay đổi\"><i class=\"material-icons\">sync</i></a>";



                                                                html += "<a href=\"javascript:;\" onclick=\"huyxuatkho($(this)," + pID + ", " + data.MainorderID + ")\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Ẩn đi\"><i class=\"material-icons\">visibility_off</i></a>";
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "</tr>";

                                                                $("." + data.MainorderID + "").parent().append(html);
                                                                moveOnTopPackage(packageID);
                                                            }
                                                            else {
                                                                var html = '';
                                                                html += "<tbody class=\"orderid" + UID + " dh" + data.MainorderID + "\" data-orderid=\"" + data.MainorderID + "\"  data-ordertype=\"" + data.OrderTypeInt + "\">";
                                                                html += "<tr><td rowspan=\"1000\" class=\"grey lighten-2\"><a  target=\"_blank\">" + data.TransportationID + "</a></td></tr>";
                                                                if (data.Status == 0) {
                                                                    html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                else if (data.Status == 1) {
                                                                    html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                else if (data.Status == 2) {
                                                                    html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                else {
                                                                    html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                                }
                                                                //html += "<td rowspan=\"50\" class=\"grey lighten-2\"><a  target=\"_blank\">" + data.TransportationID + "</a>";
                                                                //html += "</td>";
                                                                html += "<td><span>" + data.OrderType + "</span></td>";
                                                                html += "<td>";
                                                                html += "<div class=\"tb-block\">";
                                                                html += "<p class=\"black-text\">KD</p>";
                                                                if (data.Kiemdem == "Có") {
                                                                    html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                }
                                                                else {
                                                                    html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                }
                                                                html += "</div>";
                                                                html += "<div class=\"tb-block\">";
                                                                html += "<p class=\"black-text\">ĐG</p>";
                                                                if (data.Donggo == "Có") {
                                                                    html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                }
                                                                else {
                                                                    html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                }
                                                                html += "</div>";
                                                                html += "<div class=\"tb-block\">";
                                                                html += "<p class=\"black-text\">BH</p>";
                                                                if (data.Baohiem == "Có") {
                                                                    html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                                }
                                                                else {
                                                                    html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                                }
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "<td><span>" + data.BarCode + "</span></td>";
                                                               
                                                                
                                                                html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                                html += "<span class=\"packageWeightUpdate\" type=\"text\" value=\"" + data.Weight + "\"> " + data.Weight + "</span></td>";
                                                                html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                                html += "<p><span class=\"lb\">d:</span><input class=\"lengthsize\" type=\"text\" value=\"" + data.dai + "\"></p>";
                                                                html += "<p class=\"operator\"><span class=\"lb\">r:</span><input class=\"widthsize\" type=\"text\" value=\"" + data.rong + "\"></p>";
                                                                html += "<p class=\"operator\"><span class=\"lb\">c:</span><input class=\"heightsize\" type=\"text\" value=\"" + data.cao + "\"></p>";
                                                                html += "</td>";

                                                                //html += "<td><input type=\"text\" value=\"" + data.Loaisanpham + "\" class=\"packageproducttype\"></td>";
                                                                html += "<td class=\"\">";
                                                                html += "<p class=\"operator\"><span class=\"lb\">CNY:</span><input class=\"packageadditionfeeCYN\" oninput=\"getPriceFee('packageadditionfeeCYN',$(this),'cyn')\" type=\"number\" value=\"" + data.CuocvattuCYN + "\"></p>";
                                                                html += "<p class=\"operator\"><span class=\"lb\">VND:</span><input class=\"packageadditionfeeVND\" oninput=\"getPriceFee('packageadditionfeeVND',$(this),'vnd')\" type=\"number\" value=\"" + data.CuocvattuVND + "\"></p>";
                                                                html += "</td>";


                                                                //html += "<td>";
                                                                //html += "<div>";
                                                                //                                                            html += "<span type=\"text\" value=\"" + data.NVKiemdem + "\" class=\"packagedStaffNoteCheck\">"+data.NVKiemdem+"</span>";
                                                                //html += "</div>";
                                                                //html += "</td>";

                                                                html += "<td class=\"\">";
                                                                html += "<p class=\"operator\"><span class=\"lb\">CNY:</span><input class=\"packageSensorCYN\" oninput=\"getPriceFee('packageSensorCYN',$(this),'cyn')\" type=\"number\" value=\"" + data.HangDBCYN + "\"></p>";
                                                                html += "<p class=\"operator\"><span class=\"lb\">VND:</span><input class=\"packageSensorVND\" oninput=\"getPriceFee('packageSensorVND',$(this),'vnd')\" type=\"number\" value=\"" + data.HangDBVND + "\"></p>";
                                                                html += "</td>";

                                                                html += "<td>";
                                                                html += "<textarea name=\"\" class=\"tb-textarea packagedescription\" rows=\"5\">" + data.Description + "</textarea>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<textarea name=\"\" class=\"tb-textarea packagecustdescription\" rows=\"5\">" + data.Khachghichu + "</textarea>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<div style=\"pointer-events: none;\">";
                                                                html += "<input class=\"upload-img\" type=\"file\" onchange=\"previewFiles(this)\" multiple title=\"\">";
                                                                html += "<button class=\"btn-upload\">Upload</button>";
                                                                html += "</div>";
                                                                html += "<div class=\"preview-img " + packageID + "\">";
                                                                if (data.IMG != null) {
                                                                    var IMG = data.IMG.split('|');
                                                                    for (var j = 0; j < IMG.length - 1; j++) {
                                                                        html += "<div class=\"img-block\">";
                                                                        html += "<img class=\"img materialboxed\" title=\"\" data-caption=\"\" src=\"" + IMG[j] + "\">";
                                                                        html += "<span class=\"material-icons red-text delete\">clear</span>";
                                                                        html += "</div>";
                                                                    }
                                                                }
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<div class=\"input-field\" style=\"pointer-events: none;\">";
                                                                html += "<select class=\"package-status-select\">";
                                                                html += "<option value=\"\" disabled>Trạng thái</option>";
                                                                var status = data.Status;
                                                                if (status < 3) {
                                                                    if (status == 0) {
                                                                        html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                                    }
                                                                    else if (status == 1) {
                                                                        html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                                        html += "<option value=\"0\">Hủy kiện</option>";
                                                                    }
                                                                    else if (status == 2) {
                                                                        html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                                    }
                                                                }
                                                                else {
                                                                    if (status == 0) {
                                                                        html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                                    }
                                                                    else if (status == 1) {
                                                                        html += "<option value=\"1\" selected>Mới đặt - chưa về kho TQ</option>";
                                                                    }
                                                                    else if (status == 2) {
                                                                        html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                                    }
                                                                    else if (status == 3) {
                                                                        html += "<option value=\"3\" selected>Đã về kho đích</option>";
                                                                    }
                                                                    else {
                                                                        html += "<option value=\"4\" selected>Đã giao cho khách</option>";
                                                                    }
                                                                }

                                                                html += "</select>";
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "<td>";
                                                                html += "<div class=\" action-table\">";
                                                                html += "<a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "',$(this)," + packageID + ")\" class=\"tooltipped updatebutton\" data-position=\"top\" data-tooltip=\"Cập nhật thay đổi\"><i class=\"material-icons\">sync</i></a>";



                                                                html += "<a href=\"javascript:;\" onclick=\"huyxuatkho($(this)," + pID + ", " + data.MainorderID + ")\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Ẩn đi\"><i class=\"material-icons\">visibility_off</i></a>";
                                                                html += "</div>";
                                                                html += "</td>";
                                                                html += "</tr>";
                                                                html += "</tbody>";

                                                                $(".orderid" + UID + "").parent().prepend(html);
                                                                moveOnTopPackage(packageID);
                                                            }
                                                        }
                                                        else {
                                                            var html = '';
                                                            html += "<tbody class=\"orderid" + UID + " dh" + data.MainorderID + "\" data-orderid=\"" + data.MainorderID + "\"  data-ordertype=\"" + data.OrderTypeInt + "\">";
                                                            html += "<tr><td rowspan=\"1000\" class=\"grey lighten-2\"><a  target=\"_blank\">" + data.TransportationID + "</a></td></tr>";
                                                            if (data.Status == 0) {
                                                                html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                            }
                                                            else if (data.Status == 1) {
                                                                html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                            }
                                                            else if (data.Status == 2) {
                                                                html += "<tr class=\"package-row isvekho slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                            }
                                                            else {
                                                                html += "<tr class=\"package-row greisvekhoen slide-up lighten-4 order-id  " + data.MainorderID + " small" + UID + "\" data-packageID=\"" + packageID + "\" id=\"" + packageID + "\" data-barcode=" + data.BarCode + ">";
                                                            }
                                                            //html += "<td rowspan=\"50\" class=\"grey lighten-2\"><a  target=\"_blank\">" + data.TransportationID + "</a>";
                                                            //html += "</td>";
                                                            html += "<td><span>" + data.OrderType + "</span></td>";
                                                            html += "<td>";
                                                            html += "<div class=\"tb-block\">";
                                                            html += "<p class=\"black-text\">KD</p>";
                                                            if (data.Kiemdem == "Có") {
                                                                html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                            }
                                                            else {
                                                                html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                            }
                                                            html += "</div>";
                                                            html += "<div class=\"tb-block\">";
                                                            html += "<p class=\"black-text\">ĐG</p>";
                                                            if (data.Donggo == "Có") {
                                                                html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                            }
                                                            else {
                                                                html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                            }
                                                            html += "</div>";
                                                            html += "<div class=\"tb-block\">";
                                                            html += "<p class=\"black-text\">BH</p>";
                                                            if (data.Baohiem == "Có") {
                                                                html += "<p><i class=\"material-icons green-text\">check_circle</i></p>";
                                                            }
                                                            else {
                                                                html += "<p><i class=\"material-icons grey-text\">check_circle</i></p>";
                                                            }
                                                            html += "</div>";
                                                            html += "</td>";
                                                            html += "<td><span>" + data.BarCode + "</span></td>";
                                                           
                                                            
                                                            html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                            html += "<span class=\"packageWeightUpdate\" type=\"text\" value=\"" + data.Weight + "\"> " + data.Weight + "</span></td>";
                                                            html += "<td class=\"size\" style=\"pointer-events: none;\">";
                                                            html += "<p><span class=\"lb\">d:</span><input class=\"lengthsize\" type=\"text\" value=\"" + data.dai + "\"></p>";
                                                            html += "<p class=\"operator\"><span class=\"lb\">r:</span><input class=\"widthsize\" type=\"text\" value=\"" + data.rong + "\"></p>";
                                                            html += "<p class=\"operator\"><span class=\"lb\">c:</span><input class=\"heightsize\" type=\"text\" value=\"" + data.cao + "\"></p>";
                                                            html += "</td>";

                                                            //html += "<td><input type=\"text\" value=\"" + data.Loaisanpham + "\" class=\"packageproducttype\"></td>";
                                                            html += "<td class=\"\">";
                                                            html += "<p class=\"operator\"><span class=\"lb\">CNY:</span><input class=\"packageadditionfeeCYN\" oninput=\"getPriceFee('packageadditionfeeCYN',$(this),'cyn')\" type=\"number\" value=\"" + data.CuocvattuCYN + "\"></p>";
                                                            html += "<p class=\"operator\"><span class=\"lb\">VND:</span><input class=\"packageadditionfeeVND\" oninput=\"getPriceFee('packageadditionfeeVND',$(this),'vnd')\" type=\"number\" value=\"" + data.CuocvattuVND + "\"></p>";
                                                            html += "</td>";


                                                            //html += "<td>";
                                                            //html += "<div>";
                                                            //                                                            html += "<span type=\"text\" value=\"" + data.NVKiemdem + "\" class=\"packagedStaffNoteCheck\">"+data.NVKiemdem+"</span>";
                                                            //html += "</div>";
                                                            //html += "</td>";

                                                            html += "<td class=\"\">";
                                                            html += "<p class=\"operator\"><span class=\"lb\">CNY:</span><input class=\"packageSensorCYN\" oninput=\"getPriceFee('packageSensorCYN',$(this),'cyn')\" type=\"number\" value=\"" + data.HangDBCYN + "\"></p>";
                                                            html += "<p class=\"operator\"><span class=\"lb\">VND:</span><input class=\"packageSensorVND\" oninput=\"getPriceFee('packageSensorVND',$(this),'vnd')\" type=\"number\" value=\"" + data.HangDBVND + "\"></p>";
                                                            html += "</td>";

                                                            html += "<td>";
                                                            html += "<textarea name=\"\" class=\"tb-textarea packagedescription\" rows=\"5\">" + data.Description + "</textarea>";
                                                            html += "</td>";
                                                            html += "<td>";
                                                            html += "<textarea name=\"\" class=\"tb-textarea packagecustdescription\" rows=\"5\">" + data.Khachghichu + "</textarea>";
                                                            html += "</td>";
                                                            html += "<td>";
                                                            html += "<div style=\"pointer-events: none;\">";
                                                            html += "<input class=\"upload-img\" type=\"file\" onchange=\"previewFiles(this)\" multiple title=\"\">";
                                                            html += "<button class=\"btn-upload\">Upload</button>";
                                                            html += "</div>";
                                                            html += "<div class=\"preview-img " + packageID + "\">";
                                                            if (data.IMG != null) {
                                                                var IMG = data.IMG.split('|');
                                                                for (var j = 0; j < IMG.length - 1; j++) {
                                                                    html += "<div class=\"img-block\">";
                                                                    html += "<img class=\"img materialboxed\" title=\"\" data-caption=\"\" src=\"" + IMG[j] + "\">";
                                                                    html += "<span class=\"material-icons red-text delete\">clear</span>";
                                                                    html += "</div>";
                                                                }
                                                            }
                                                            html += "</div>";
                                                            html += "</td>";
                                                            html += "<td>";
                                                            html += "<div class=\"input-field\" style=\"pointer-events: none;\">";
                                                            html += "<select class=\"package-status-select\">";
                                                            html += "<option value=\"\" disabled>Trạng thái</option>";
                                                            var status = data.Status;
                                                            if (status < 3) {
                                                                if (status == 0) {
                                                                    html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                                }
                                                                else if (status == 1) {
                                                                    html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                                    html += "<option value=\"0\">Hủy kiện</option>";
                                                                }
                                                                else if (status == 2) {
                                                                    html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                                }
                                                            }
                                                            else {
                                                                if (status == 0) {
                                                                    html += "<option value=\"0\" selected>Hủy kiện</option>";
                                                                }
                                                                else if (status == 1) {
                                                                    html += "<option value=\"1\" selected>Mới đặt - chưa về kho TQ</option>";
                                                                }
                                                                else if (status == 2) {
                                                                    html += "<option value=\"2\" selected>Đã về kho TQ</option>";
                                                                }
                                                                else if (status == 3) {
                                                                    html += "<option value=\"3\" selected>Đã về kho đích</option>";
                                                                }
                                                                else {
                                                                    html += "<option value=\"4\" selected>Đã giao cho khách</option>";
                                                                }
                                                            }

                                                            html += "</select>";
                                                            html += "</div>";
                                                            html += "</td>";
                                                            html += "<td>";
                                                            html += "<div class=\" action-table\">";
                                                            html += "<a href=\"javascript:;\" onclick=\"updateWeight('" + data.BarCode + "',$(this)," + packageID + ")\" class=\"tooltipped updatebutton\" data-position=\"top\" data-tooltip=\"Cập nhật thay đổi\"><i class=\"material-icons\">sync</i></a>";
                                                            html += "<a href=\"javascript:;\" onclick=\"huyxuatkho($(this)," + pID + ", " + data.MainorderID + ")\" class=\"tooltipped\" data-position=\"top\" data-tooltip=\"Ẩn đi\"><i class=\"material-icons\">visibility_off</i></a>";
                                                            html += "</div>";
                                                            html += "</td>";
                                                            html += "</tr>";
                                                            html += "</tbody>";

                                                            $(".orderid" + UID + "").parent().prepend(html);
                                                            moveOnTopPackage(packageID);
                                                        }
                                                    }
                                                    $('select').formSelect();
                                                    obj.val("");
                                                }
                                                else {
                                                    obj.val("");
                                                    //addLoading();
                                                    //$(".package-row").each(function () {
                                                    //    var ordercodetrans = $(this).attr("data-barcode");
                                                    //    if (ordercodetrans == bc) {
                                                    //        var packageID = $(this).attr("data-packageID");
                                                    //        var idpack = "bc-" + bc + "-" + packageID;
                                                    //        updateWeight(bc, $(this).find(".updatebutton"), packageID);
                                                    //    }
                                                    //})
                                                }
                                            }
                                        }
                                        obj.val("");
                                    }

                                    $('.tooltipped')
                                        .tooltip({
                                            trigger: 'manual'
                                        })
                                        .tooltip('show');
                                }
                            }
                            else {
                                //document.getElementById('audio').play();
                                alert('Không tìm thấy thông tin');
                                obj.val("");
                            }
                        },
                        error: function (xmlhttprequest, textstatus, errorthrow) {
                            //alert('lỗi checkend');
                        }
                    });
                }
            }









        });

        function OpenModal(obj) {
            $('#addPackage').modal('open');
            $("#smOrderTransactionCode").focus();
            $("#smOrderTransactionCode").val(obj);
            $("#smMainOrderID").val('');
            $("#smmainordercode").val('');
            $("#smNote").val('');
        }

        function loadBigPackage(value) {
            $.ajax({
                type: "POST",
                url: "/manager/add-package-customer.aspx/GetBigPackage",
                //data: "{a:'1'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = JSON.parse(msg.d);
                    var html = " <option value=\"0\">Chọn khách hàng</option>";

                    if (data != null) {
                        for (var i = 0; i < data.length; i++) {
                            var item = data[i];
                            html += " <option value=\"" + item.ID + "\">" + item.Username + "</option>";
                        }
                    }
                    $("#ddlBigpack").html(html);
                    $("#ddlBigpack").val(value);
                    $('select').formSelect();
                }
            })
        }

        function isEmpty(str) {
            return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
        }



        //function addLoading() {
        //    $(".page-inner").append("<div class='loading_bg'></div>");
        //    var height = $(".page-inner").height();
        //    $(".loading_bg").css("height", height + "px");
        //}
        //function removeLoading() {
        //    $(".loading_bg").remove();
        //}





        $('#add-package').on('click', function () {
            $('#addPackage').show();
        });

        function updateWeight(barcode, obj, packageID) {
            var dai = obj.parent().parent().parent().find(".lengthsize").val();
            var rong = obj.parent().parent().parent().find(".widthsize").val();
            var cao = obj.parent().parent().parent().find(".heightsize").val();

            var quantity = obj.parent().parent().parent().find(".packageWeightUpdate").val();
            var status = obj.parent().parent().parent().find(".package-status-select").val();
            var bigpackage = obj.parent().parent().find(".package-bigpackage-select").val();
            var note = obj.parent().parent().parent().find(".packagedescription").val();

            var staffnote = obj.parent().parent().parent().find(".packagedStaffNoteCheck").val();
            var custdescription = obj.parent().parent().parent().find(".packagecustdescription").val();
            var producttype = obj.parent().parent().parent().find(".packageproducttype").val();

            var packageadditionfeeCYN = obj.parent().parent().parent().find(".packageadditionfeeCYN").val();
            var packageadditionfeeVND = obj.parent().parent().parent().find(".packageadditionfeeVND").val();
            var packageSensorCYN = obj.parent().parent().parent().find(".packageSensorCYN").val();
            var packageSensorVND = obj.parent().parent().parent().find(".packageSensorVND").val();

            var username = $("#<%=ddlBigpack.ClientID%>").val();

            var base64 = "";
            $(".preview-img." + packageID + " img").each(function () {
                base64 += $(this).attr('src') + "|";
            })
            addLoading();
            $.ajax({
                type: "POST",
                url: "/manager/add-package-customer.aspx/UpdateQuantityNew",
                //data: "{barcode:'" + barcode + "',quantity:'" + quantity + "',status:'" + status + "',BigPackageID:'0',packageID:'" + packageID + "',dai:'" + dai + "',rong:'" + rong + "',cao:'" + cao + "'}",
                data: "{barcode:'" + barcode + "',quantity:'" + quantity + "',status:'" + status
                + "',BigPackageID:'0',packageID:'" + packageID + "',dai:'" + dai + "',rong:'" + rong + "',Username:'" + username
                + "',cao:'" + cao + "',base64:'" + base64 + "',note:'" + note + "', nvkiemdem:'" + staffnote + "',khachghichu:'" + custdescription
                + "',loaisanpham:'" + producttype + "',packageadditionfeeCYN:'" + packageadditionfeeCYN
                + "',packageadditionfeeVND:'" + packageadditionfeeVND
                + "',packageSensorCYN:'" + packageSensorCYN
                + "',packageSensorVND:'" + packageSensorVND + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var ret = msg.d;
                    if (ret != "none") {
                        if (ret == "existsmallpackage") {
                            alert('Không tồn tại mã vận đơn trong hệ thống.');
                        }
                        else if (ret == "nottroinoi") {
                            alert('Không thể gán cho khách vì kiện đã có chủ .');
                        }
                        else if (ret == "nhapusername") {
                            alert('Vui lòng nhập Username khách hàng .');
                        }
                        else if (ret == "notusername") {
                            alert('Không tồn tại Username trong hệ thống .');
                        }
                        else {
                            var PackageAll = JSON.parse(ret);
                            var data = PackageAll.listPackageGet[0];
                            var barcode = data.BarCode;
                            $("#dataMDH").html('');
                            $("#tblMDH").addClass('hide');
                            alert('Gán kiện thành công.');
                        }
                    }
                    removeLoading();
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    alert(errorthrow);
                    removeLoading();
                }
            });

        }

        function huyxuatkho(obj, uid, mainorderid) {
            var r = confirm("Bạn muốn tắt package này?");
            if (r == true) {
               <%--   var id = barcode + "|";
              var listbarcode = $("#<%=hdfListBarcode.ClientID%>").val();
                listbarcode = listbarcode.replace(id, "");
                $("#<%=hdfListBarcode.ClientID%>").val(listbarcode);--%>
                obj.parent().parent().parent().remove();

                if ($(".dh" + mainorderid + " tr").length == 1) {
                    $(".dh" + mainorderid + "").remove();
                }

                if ($(".small" + uid + "").length == 0) {
                    $("#" + uid + "").remove();
                }

                //if ($(".package-item").length == 0) {
                //    $("#outall-package").hide();
                //    $("#capnhatall").hide();
                //}
                //countOrder();
            } else {

            }
        }

        function moveOnTopPackage(id) {
            var last = $('#' + id).closest('.package-wrap');
            var parent = last.parent();
            var first = parent.children().first();
            if ($('.package-wrap').length > 1) {
                first.before(last);
                $('#' + id).find('.packageWeightUpdate').focus().select();

            } else {
                $('#' + id).find('.packageWeightUpdate').focus().select();
            }
        }

        function RemoveAllByUID(UID, username) {
            var r = confirm("Bạn muốn tắt tất cả kiện của " + username + "?");
            if (r == true) {
                $("#" + UID + "").remove();
            }
        }

        //function UpdateAllByUID() {
        //    var barcode = $(this).attr("data-barcode");
        //    var packageID = $(this).attr("data-packageID");
        //    updateWeight(barcode, $(this).find(".updatebutton"), packageID);
        //}

        function UpdateAllByUID() {
            var c = confirm('Vui lòng xác nhận lại Username khách !!!');
            if (c == true) {
                $(".package-row").length > 0
                {
                    $(".package-row").each(function () {
                        var barcode = $(this).attr("data-barcode");
                        var packageID = $(this).attr("data-packageID");
                        updateWeight(barcode, $(this).find(".updatebutton"), packageID);
                    })
                }
              }
           
        }






        $(document).ready(function () {
            $('.select2').select2();
        });


        function DeletePackage(packageID, obj, uid, mainorderid) {
            var r = confirm("Bạn muốn tắt package này?");
            if (r == true) {
                $.ajax({
                    type: "POST",
                    url: "/manager/add-package-customer.aspx/Delete",
                    data: "{PackageID:'" + packageID + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d == "ok") {
                            obj.parent().parent().parent().remove();

                            if ($(".dh" + mainorderid + " tr").length == 1) {
                                $(".dh" + mainorderid + "").remove();
                            }

                            if ($(".small" + uid + "").length == 0) {
                                $("#" + uid + "").remove();
                            }
                        }
                        else {

                        }
                    }
                })
            } else {

            }
        }

    </script>
</asp:Content>
