<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" AutoEventWireup="true" CodeBehind="shippingtypelist.aspx.cs" Inherits="NHST.manager.shippingtypelist" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">DANH SÁCH LOẠI VẬN CHUYỂN</h4>
                    <div class="right-action">
                        <a href="#modalAdd" class="tooltipped modal-trigger btn" data-position="top" data-tooltip="Thêm kho đến">Thêm loại vận chuyển</a>
                    </div>
                </div>
            </div>
            <div class="list-staff col s12 section">
                <div class="list-table card-panel">
                    <div class="filter">
                        <div class="search-name input-field">
                            <asp:TextBox ID="search_name" type="text" onkeypress="myFunction()" runat="server" />
                            <label for="search_name"><span>Tên loại</span></label>
                            <span class="material-icons search-action">search</span>
                           <asp:Button Style="display: none" UseSubmitBehavior="false" ID="btnSearch" runat="server" OnClick="btnSearch_Click"/>
                        </div>
                    </div>
                    <div class="responsive-tb">
                        <table class="table highlight bordered  ">
                            <thead>
                                <tr>
                                    <th>ID</th>
                                    <th>Tên loại vận chuyển</th>
                                    <th>Trạng thái</th>
                                    <th>Ngày tạo</th>
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
    <div id="modalAdd" class="modal modal-fixed-footer" style="height:auto">
        <div class="modal-hd">
            <span class="right"><i class="material-icons modal-close right-align">clear</i></span>
            <h4 class="no-margin center-align">THÊM LOẠI VẬN CHUYỂN</h4>
        </div>
        <div class="modal-bd">
            <div class="row">
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txtAddShipName" type="text" class="validate" data-type="text-only"></asp:TextBox>
                    <label for="edit_from_warehouse">Tên loại</label>
                </div>       
                  <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txtAddShipDescrtiption" placeholder="" class="validate" data-type="text-only" ></asp:TextBox>
                    <label for="edit_from_warehouse">Mô tả</label>
                </div>
                <div class="input-field col s12 m6">
                    <div class="switch status-func">
                        <span class="mr-2">Ẩn: </span>
                        <label> 
                            <asp:TextBox ID="AddStatus" runat="server" type="checkbox" onclick="AddFunction()"></asp:TextBox>
                            <span class="lever"></span>                        
                        </label>
                    </div>
                    <asp:HiddenField runat="server" ID="hdfAddIsHidden" Value="0"/>
                </div>
        </div>
    </div>
    <div class="modal-ft" >
        <div class="ft-wrap center-align">
           <asp:Button ID="btnCreate" OnClick="btnCreate_Click" runat="server"  UseSubmitBehavior="false" class="white-text btn  mr-2" Text="Thêm"></asp:Button>
            <a href="#!" class="modal-action btn orange darken-2 modal-close waves-effect waves-green ml-2">Hủy</a>
        </div>
    </div>
    </div>
    <div id="modalEditFee" class="modal modal-fixed-footer" style="height:auto">
        <div class="modal-hd">
            <span class="right"><i class="material-icons modal-close right-align">clear</i></span>
            <h4 class="no-margin center-align">CHỈNH SỬA LOẠI VẬN CHUYỂN</h4>
        </div>
        <div class="modal-bd">
            <div class="row">
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txtEditShipName" placeholder="" class="validate" data-type="text-only" ></asp:TextBox>
                    <label for="edit_from_warehouse">Tên loại</label>
                </div>  
                <div class="input-field col s12 m6">
                    <asp:TextBox runat="server" ID="txtEditShipDescription" placeholder="" class="validate" data-type="text-only" ></asp:TextBox>
                    <label for="edit_from_warehouse">Mô tả</label>
                </div> 
                <div class="input-field col s12 m6">
                    <div class="switch status-func">
                        <span class="mr-2">Ẩn: </span>
                        <label>                          
                            <asp:TextBox ID="EditStatus" runat="server" type="checkbox" onclick="EditStatusFunction()"></asp:TextBox>
                            <span class="lever"></span>                          
                        </label>
                    </div>
                    <asp:HiddenField runat="server" ID="hdfEditStatus" Value="0"/>
                </div>
        </div>
    </div>
    <div class="modal-ft">
        <div class="ft-wrap center-align">
            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" UseSubmitBehavior="false" class="white-text btn  mr-2" Text="Cập nhật"></asp:Button>
            <a href="#!" class="modal-action btn orange darken-2 modal-close waves-effect waves-green ml-2">Hủy</a>
        </div>

    </div>
    </div>
    <asp:HiddenField ID="hdfID" runat="server" />
    <script>
        function EditStatusFunction() {

            var a = $('#<%=hdfEditStatus.ClientID%>').val();
            if (a == '0')
            {

                $('#<%=hdfEditStatus.ClientID%>').val('1');
            }
            else
            {

                $('#<%=hdfEditStatus.ClientID%>').val('0');
            }
            console.log($('#<%=hdfEditStatus.ClientID%>').val());
        }
        function AddFunction()
        {
            var a = $('#<%=hdfAddIsHidden.ClientID%>').val();
            if (a == '0')
            {
                $('#<%=hdfAddIsHidden.ClientID%>').val('1');
            }
            else
            {
                $('#<%=hdfAddIsHidden.ClientID%>').val('0');
            }
            console.log($('#<%=hdfAddIsHidden.ClientID%>').val());
        }
        function EditFunction(ID) {
            $.ajax({
                type: "POST",
                url: "/manager/shippingtypelist.aspx/loadinfo",
                data: '{ID: "' + ID + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    var data = JSON.parse(msg.d);
                    if (data != null) {
                        $('#<%=hdfID.ClientID%>').val(ID);
                        $('#<%=txtEditShipName.ClientID%>').val(data.ShippingTypeName);        
                        $('#<%=txtEditShipDescription.ClientID%>').val(data.ShippintTypeDescription);                                          

                        var a = data.IsHidden;
                        if (a == true) {
                            $('#<%=EditStatus.ClientID%>').prop('checked', true);
                            $('#<%=hdfEditStatus.ClientID%>').val('1');
                        }
                        else {
                            $('#<%=EditStatus.ClientID%>').prop('checked', false);
                            $('#<%=hdfEditStatus.ClientID%>').val('0');
                        }
                        $('select').formSelect();
                    }
                    else
                        swal("Error", "Không thành công", "error");
                },
                error: function (xmlhttprequest, textstatus, errorthrow) {
                    swal("Error", "Fail updateInfoAcc", "error");
                }
            });
        }
        function myFunction() {
            if (event.which == 13 || event.keyCode == 13) {
                console.log($('#<%=search_name.ClientID%>').val());
                 $('#<%=btnSearch.ClientID%>').click();
            }
        }
        $('.search-action').click(function () {
                   <%-- console.log($('#<%=search_name.ClientID%>').val());--%>
            $('#<%=btnSearch.ClientID%>').click();
        })
    </script>
</asp:Content>
