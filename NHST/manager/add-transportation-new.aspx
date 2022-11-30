<%@ Page Language="C#"  MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="add-transportation-new.aspx.cs" Inherits="NHST.manager.add_transportation_new" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Thêm mới đơn ký gửi cho khách</h4>
                </div>
            </div>
            <div class="list-staff col s12 section">
                <div class="list-table">
                    <div class="row">
                        <div class="col s12 m3">
                            <div class="card-panel">
                                <h6 class="black-text">Thông tin</h6>
                                <hr class="mb-5" />
                                <div class="row">
                                    <div class="input-field col s12">
                                        <div class="col s6">
                                            <select id="ddlBigpack">
                                            </select>
                                            <%-- <asp:DropDownList runat="server" ID="ddlBigpackage" DataTextField="PackageCode" DataValueField="ID"></asp:DropDownList>--%>
                                            <label for="package_code">Mã bao hàng</label>
                                        </div>
                                        <div class="col s6">
                                            <a href="#addBadge" class="btn modal-trigger waves-effect">Tạo mới bao lớn</a>
                                        </div>
                                    </div>
                                    <div class="input-field col s12">
                                        <asp:TextBox runat="server" CssClass="username" placeholder="" ID="txtUsername"></asp:TextBox>
                                        <label for="package_weight">Username khách hàng</label>
                                    </div>
                                        <div class="input-field col s12">
                                        <asp:DropDownList runat="server" ID="ddlWareHouseFrom" DataTextField="WareHouseName" DataValueField="ID"></asp:DropDownList>
                                        <label for="package_m3">Kho TQ</label>
                                    </div>
                                    <div class="input-field col s12">
                                        <asp:DropDownList runat="server" ID="ddlWareHouseTo" DataTextField="WareHouseName" DataValueField="ID"></asp:DropDownList>
                                        <label for="package_mvc">Kho VN</label>
                                    </div>
                                    <div class="input-field col s12">
                                        <asp:DropDownList runat="server" ID="ddlShippingType" DataTextField="ShippingTypeName" DataValueField="ID"></asp:DropDownList>
                                        <label for="package_code">Phương thức vận chuyển</label>
                                    </div>
                                    <div class="input-field col s12">
                                        <asp:TextBox runat="server" ID="txtNote" TextMode="MultiLine"></asp:TextBox>
                                        <label for="package_mvc">Ghi chú</label>
                                    </div>
                                    <div class="input-field col s12">
                                        <a class="btn" href="javascript:;" onclick="CreateTrans()">Tạo mới</a>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="col s12 m9">
                            <div class="card-panel">
                                <h6 class="black-text">Danh sách mã vận đơn</h6>
                                <hr class="mb-5" />
                                <div class="row">
                                    <div class="col s12 m12 l6">
                                        <div class="search-name input-field no-margin full-width">
                                            <input placeholder="Nhập mã vận đơn" id="barcode-input" type="text"
                                                class="barcode">
                                            <div class="bg-barcode"></div>
                                            <span class="material-icons search-action">search</span>
                                        </div>
                                    </div>
                                    <div class="col s12 mt-2">
                                        <div class="list-package hide">
                                            <div class="package-item">
                                                <div class="responsive-tb">
                                                    <table class="table  centered bordered ">
                                                        <thead>
                                                            <tr class="teal darken-4">
                                                                <th>Mã vận đơn</th>
                                                                <th>Cân nặng (kg)</th>
                                                                <th>Kích thước</th>
                                                                <th>Bao lớn</th>
                                                                <th>Ghi chú</th>
                                                                <th>Hình ảnh</th>
                                                                <%-- <th>Action</th>--%>
                                                            </tr>
                                                        </thead>
                                                        <tbody class="listpackage">
                                                            <%--  <tr>
                                                                <td><span>120332454</span></td>
                                                                <td>
                                                                    <div class="input-field">
                                                                        <select>
                                                                            <option value="" disabled selected>Chọn</option>
                                                                            <option value="1">Bao lớn 1 hoang lien anh</option>
                                                                            <option value="2">Bao 2</option>
                                                                            <option value="3">Vân Liên 3</option>
                                                                        </select>
                                                                    </div>
                                                                </td>

                                                                <td class="size">
                                                                    <input type="text" value="4"></td>
                                                                <td class="size">
                                                                    <p>
                                                                        <span class="lb">d:</span>
                                                                        <input type="text" value="0">
                                                                    </p>
                                                                    <p class="operator">
                                                                        <span class="lb">r:</span>
                                                                        <input type="text" value="0">
                                                                    </p>
                                                                    <p class="operator">
                                                                        <span class="lb">c:</span>
                                                                        <input type="text" value="0">
                                                                    </p>
                                                                </td>
                                                                <td>
                                                                    <textarea name="" class="tb-textarea"
                                                                        rows="5">Quần đùi màu xanh 4x4 size M Quần đùi màu xanh 4x4 size MM Quần đùi màu xanh 4x4 size M</textarea>

                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <input class="upload-img" type="file"
                                                                            onchange="previewFiles(this)" multiple title="">
                                                                        <button class="btn-upload">Upload</button>
                                                                    </div>
                                                                    <div class="preview-img">
                                                                        <div class="img-block">
                                                                            <img class="img materialboxed"
                                                                                data-caption="Một cái quần đùi màu xanh"
                                                                                src="assets/images/gallery/post-2.png">
                                                                            <span
                                                                                class="material-icons red-text delete">clear</span>
                                                                        </div>
                                                                    </div>

                                                                </td>
                                                                <td>
                                                                    <div class="action-table">
                                                                        <a href="mavandon-chitiet.php"
                                                                            class="tooltipped edit-mode" data-position="top"
                                                                            data-tooltip="Cập nhật"><i
                                                                                class="material-icons">edit</i></a>

                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><span>120332454</span></td>
                                                                <td>
                                                                    <div class="input-field">
                                                                        <select>
                                                                            <option value="" disabled selected>Chọn</option>
                                                                            <option value="1">Bao lớn 1 hoang lien anh</option>
                                                                            <option value="2">Bao 2</option>
                                                                            <option value="3">Vân Liên 3</option>
                                                                        </select>
                                                                    </div>
                                                                </td>
                                                                <td class="size">
                                                                    <input type="text" value="4"></td>
                                                                <td class="size">
                                                                    <p>
                                                                        <span class="lb">d:</span>
                                                                        <input type="text" value="0">
                                                                    </p>
                                                                    <p class="operator">
                                                                        <span class="lb">r:</span>
                                                                        <input type="text" value="0">
                                                                    </p>
                                                                    <p class="operator">
                                                                        <span class="lb">c:</span>
                                                                        <input type="text" value="0">
                                                                    </p>
                                                                </td>
                                                                <td>
                                                                    <textarea name="" class="tb-textarea"
                                                                        rows="5">Quần đùi màu xanh 4x4 size M Quần đùi màu xanh 4x4 size MM Quần đùi màu xanh 4x4 size M</textarea>

                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <input class="upload-img" type="file"
                                                                            onchange="previewFiles(this)" multiple title="">
                                                                        <button class="btn-upload">Upload</button>
                                                                    </div>
                                                                    <div class="preview-img">
                                                                        <div class="img-block">
                                                                            <img class="img materialboxed"
                                                                                data-caption="Một cái quần đùi màu xanh"
                                                                                src="assets/images/gallery/post-2.png">
                                                                            <span
                                                                                class="material-icons red-text delete">clear</span>
                                                                        </div>
                                                                    </div>

                                                                </td>
                                                                <td>
                                                                    <div class="action-table">
                                                                        <a href="mavandon-chitiet.php"
                                                                            class="tooltipped edit-mode" data-position="top"
                                                                            data-tooltip="Cập nhật"><i
                                                                                class="material-icons">edit</i></a>

                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td><span>120332454</span></td>
                                                                <td>
                                                                    <div class="input-field">
                                                                        <select>
                                                                            <option value="" disabled selected>Chọn</option>
                                                                            <option value="1">Bao lớn 1 hoang lien anh</option>
                                                                            <option value="2">Bao 2</option>
                                                                            <option value="3">Vân Liên 3</option>
                                                                        </select>
                                                                    </div>
                                                                </td>
                                                                <td class="size">
                                                                    <input type="text" value="4"></td>
                                                                <td class="size">
                                                                    <p>
                                                                        <span class="lb">d:</span>
                                                                        <input type="text" value="0">
                                                                    </p>
                                                                    <p class="operator">
                                                                        <span class="lb">r:</span>
                                                                        <input type="text" value="0">
                                                                    </p>
                                                                    <p class="operator">
                                                                        <span class="lb">c:</span>
                                                                        <input type="text" value="0">
                                                                    </p>
                                                                </td>
                                                                <td>
                                                                    <textarea name="" class="tb-textarea"
                                                                        rows="5">Quần đùi màu xanh 4x4 size M Quần đùi màu xanh 4x4 size MM Quần đùi màu xanh 4x4 size M</textarea>

                                                                </td>
                                                                <td>
                                                                    <div>
                                                                        <input class="upload-img" type="file"
                                                                            onchange="previewFiles(this)" multiple title="">
                                                                        <button class="btn-upload">Upload</button>
                                                                    </div>
                                                                    <div class="preview-img">
                                                                        <div class="img-block">
                                                                            <img class="img materialboxed"
                                                                                data-caption="Một cái quần đùi màu xanh"
                                                                                src="assets/images/gallery/post-2.png">
                                                                            <span
                                                                                class="material-icons red-text delete">clear</span>
                                                                        </div>
                                                                    </div>

                                                                </td>
                                                                <td>
                                                                    <div class="action-table">
                                                                        <a href="mavandon-chitiet.php"
                                                                            class="tooltipped edit-mode" data-position="top"
                                                                            data-tooltip="Cập nhật"><i
                                                                                class="material-icons">edit</i></a>
                                                                    </div>
                                                                </td>
                                                            </tr>--%>
                                                        </tbody>
                                                    </table>
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
    <!-- END: Page Main-->

    <div id="addBadge" class="modal modal-big add-package">
        <div class="modal-hd">
            <span class="right"><i class="material-icons modal-close right-align">clear</i></span>
            <h4 class="no-margin center-align">Thêm bao lớn mới</h4>
        </div>
        <div class="modal-bd">
            <div class="row">

                <div class="input-field col s12 m4">
                    <asp:TextBox runat="server" ID="txtPackageCode" CssClass="validate" placeholder="Mã bao hàng"></asp:TextBox>
                    <span class="error-info-show">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPackageCode" Display="Dynamic"
                            ErrorMessage="Không để trống" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </span>
                    <label for="kg_weight">Mã bao hàng</label>
                </div>

                <div class="input-field col s12 m4">
                    <telerik:RadNumericTextBox runat="server" CssClass="validate" Skin="MetroTouch"
                        ID="pWeight" MinValue="0" NumberFormat-DecimalDigits="2"
                        NumberFormat-GroupSizes="3" Width="100%" placeholder="Cân (kg)" Value="0">
                    </telerik:RadNumericTextBox>
                    <label for="kg_weight" class="active">Cân nặng (kg)</label>
                </div>
                <div class="input-field col s12 m4">
                    <telerik:RadNumericTextBox runat="server" CssClass="validate" Skin="MetroTouch"
                        ID="pVolume" MinValue="0" NumberFormat-DecimalDigits="2"
                        NumberFormat-GroupSizes="3" Width="100%" placeholder="Khối (m3)" Value="0">
                    </telerik:RadNumericTextBox>
                    <label for="m2_weigth" class="active">Khối (m<sup>3</sup>)</label>

                </div>
            </div>
        </div>
        <div class="modal-ft">
            <div class="ft-wrap center-align">
                <%-- <asp:Button ID="btncreateuser" runat="server" Text="Tạo mới" Style="display: none" CssClass="modal-action btn modal-close waves-effect waves-green mr-2"
                    OnClick="btncreateuser_Click" />--%>
                <a href="javascript:;" onclick="AddBigPackage()" class="modal-action btn modal-close waves-effect waves-green mr-2 submit-button">Thêm</a>
                <a href="javascript:;" class="modal-action btn orange darken-2 modal-close waves-effect waves-green ml-2">Hủy</a>
            </div>

        </div>
    </div>

    <asp:HiddenField runat="server" ID="hdfValue" />
    <asp:Button runat="server" ID="btnCreate" UseSubmitBehavior="false" OnClick="btnCreate_Click" Style="display: none" />

    <script type="text/javascript">
        $(document).ready(function () {
            $('#barcode-input').focus();
            $('#barcode-input').keydown(function (e) {
                if (e.key === 'Enter') {
                    //getCodeNew
                    CheckCode($(this));
                    e.preventDefault();
                    return false;
                }
            });

            $('.username').keyup(function (e) {
                var user = $("#<%=txtUsername.ClientID%>").val();
                document.title = "Thêm đơn ký gửi cho " + user;
            })


            loadBigPackage(0);

            //Enter ben trong tr update
            $('.listpackage').on('keypress', 'input,select', function (e) {
                if (e.keyCode == 13) {
                    var $element = $(e.target);
                    $element.closest('.package-row').find('.updatebutton').click();
                    $('#barcode-input').focus().select();
                    return false;
                }
            });

            $('body').on('keyup', function (e) {
                if (e.ctrlKey && e.keyCode == 66) {
                    //Hotkey Tao bao moi    Crtl + B
                    $('#addBadge').modal('open');
                    $("#<%=txtPackageCode.ClientID%>").val('').focus();
                }
            });

            $('.modal-big').on('keypress', function (e) {
                if (e.keyCode == 13) {
                    $(e.target).closest('.modal').find('.submit-button').click(); //Click submit button tren modal
                    $(e.target).closest('.modal').modal('close');
                    $('#barcode-input').focus();
                }
            })
        });

        function loadBigPackage(value) {
            $.ajax({
                type: "POST",
                url: "/manager/TQWareHouse.aspx/GetBigPackage",
                //data: "{a:'1'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = JSON.parse(msg.d);
                    var html = " <option value=\"0\">Chọn bao lớn</option>";

                    if (data != null) {
                        for (var i = 0; i < data.length; i++) {
                            var item = data[i];
                            html += " <option value=\"" + item.ID + "\">" + item.PackageCode + "</option>";
                        }
                    }
                    $("#ddlBigpack").html(html);
                    $("#ddlBigpack").val(value);
                    $('select').formSelect();
                }
            })
        }

        function AddBigPackage() {
            var packageCode = $("#<%=txtPackageCode.ClientID%>").val();
            var weight = $("#<%=pWeight.ClientID%>").val();
            var Volume = $("#<%=pVolume.ClientID%>").val();
            if (!isEmpty(packageCode)) {
                $.ajax({
                    type: "POST",
                    url: "/manager/TQWareHouse.aspx/AddBigPackage",
                    data: "{PackageCode:'" + packageCode + "',Weight:'" + weight + "',Volume:'" + Volume + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        var data = msg.d;
                        if (data != null) {
                            if (data != "existCode") {
                                loadBigPackage(parseInt(data));
                            }
                            else {
                                alert('Mã bao hàng đã tồn tài.');
                            }
                        }
                        else {
                            alert('Có lỗi trong quá trình xử lý.');
                        }
                    }
                })
            }
            else {
                alert('Không được để trống mã bao hàng!');
            }
        }

        function isEmpty(str) {
            return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
        }

        function CheckCode(obj) {
            var bc = obj.val();
            if (isEmpty(bc)) {
                alert('Vui lòng không để trống barcode.');
            }
            else {
                $.ajax({
                    type: "POST",
                    url: "/manager/add-transportation.aspx/CheckCode",
                    data: "{barcode:'" + bc + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d != "none") {
                            var data = JSON.parse(msg.d);

                            var check = false;
                            $(".package").each(function () {
                                if ($(this).attr("data-barcode") == data.Barcode) {
                                    check = true;
                                }
                            })
                            if (check == false) {
                                var html = "";
                                html += "<tr id=\"" + data.Barcode + "\" data-barcode=\"" + data.Barcode + "\" class=\"package\">";
                                html += "<td><span>" + data.Barcode + "</span></td>";

                                html += "<td class=\"size\">";
                                html += "<input class=\"packageWeightUpdate\" type=\"text\" value=\"0\"></td>";



                                html += "<td class=\"size\">";
                                html += "<p><span class=\"lb\">d:</span><input class=\"lengthsize\" type=\"text\" value=\"0\"></p>";
                                html += "<p class=\"operator\"><span class=\"lb\">r:</span><input class=\"widthsize\" type=\"text\" value=\"0\"></p>";
                                html += "<p class=\"operator\"><span class=\"lb\">c:</span><input class=\"heightsize\" type=\"text\" value=\"0\"></p>";
                                html += "</td>";
                                html += "<td>";
                                html += "<div class=\"input-field\">";
                                html += "<select class=\"package-bigpackageID\">";
                                html += "<option value=\"0\" selected>Chọn bao lớn</option>";
                                var selectedbigpack = parseFloat($("#ddlBigpack option:selected").val());
                                var getBs = data.ListBig;
                                for (var k = 0; k < getBs.length; k++) {
                                    var b = getBs[k];
                                    var idbig = parseFloat(b.ID);
                                    if (selectedbigpack == idbig) {
                                        html += "<option value=\"" + idbig + "\" selected>" + b.PackageCode + "</option>";
                                    }
                                    else {
                                        html += "<option value=\"" + idbig + "\">" + b.PackageCode + "</option>";
                                    }
                                }
                                html += "</select>";
                                html += "</div>";
                                html += "</td>";
                                html += "<td><textarea name=\"\" class=\"tb-textarea note\" rows=\"5\"></textarea></td>";
                                html += "<td><div><input class=\"upload-img\" type=\"file\" onchange=\"previewFiles(this)\" multiple title=\"\"><button class=\"btn-upload\">Upload</button></div>";
                                html += "<div class=\"preview-img " + data.Barcode + "\">";
                                html += "</div>";
                                html += "</div>";

                                html += "</td>";
                                //html += "<td><div class=\"action-table\"><a href=\"mavandon-chitiet.php\" class=\"tooltipped edit-mode\" data-position=\"top\" data-tooltip=\"Cập nhật\"><i class=\"material-icons\">edit</i></a></div></td>";
                                html += "</tr>";

                                $(".listpackage").prepend(html);
                                $("#barcode-input").val('');
                                $('select').formSelect();
                                $(".list-package").removeClass('hide');
                                Weightfocus(data.Barcode);
                            }
                            else {
                                alert('Mã vận đơn này đã được hiển thị');
                            }
                        }
                        else {
                            alert('Mã vận đơn này đã tồn tại trong hệ thống');
                        }
                    }

                })
            }
        }

        function Weightfocus(id) {
            $('#' + id).find('.packageWeightUpdate').focus().select();
        }

        function CreateTrans() {
            var username = $("#<%=txtUsername.ClientID%>").val();
         <%--   var khotq = $("#<%=ddlWareHouseFrom.ClientID%>").val();--%>
            var khovn = $("#<%=ddlWareHouseTo.ClientID%>").val();
            var sp = $("#<%=ddlShippingType.ClientID%>").val();
            //if (khotq == 0) {
            //    alert('Chọn kho TQ');
            //    return;
            //}
            if (khovn == 0) {
                alert('Chọn kho VN');
                return;
            }
            if (sp == 0) {
                alert('Chọn phương thức VC');
                return;
            }
            if (!isEmpty(username)) {
                if ($(".package").length > 0) {
                    var html = '';
                    $(".package").each(function () {
                        var barcode = $(this).attr("data-barcode");
                        var weight = $(this).find(".packageWeightUpdate").val();
                        var d = $(this).find(".lengthsize").val();
                        var r = $(this).find(".widthsize").val();
                        var c = $(this).find(".heightsize").val();
                        var bigpackage = $(this).find(".package-bigpackageID").val();
                        var note = $(this).find(".note").val();
                        var base64 = "";
                        $(".preview-img." + barcode + " img").each(function () {
                            base64 += $(this).attr('src') + "[";
                        })

                        html += barcode + '|' + weight + '|' + d + '|' + r + '|' + c + '|' + bigpackage + '|' + note + '|' + base64 + '*';
                    })
                    $("#<%=hdfValue.ClientID%>").val(html);
                    $("#<%=btnCreate.ClientID%>").click()
                }
                else {
                    alert('Chưa có mã vận đơn nào!');
                }
            }
            else {
                alert('Vui lòng nhập username khách hàng');
            }

        }

    </script>

</asp:Content>
