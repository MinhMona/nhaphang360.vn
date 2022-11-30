<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="NHST.Admin.OrderDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Import Namespace="NHST.Controllers" %>
<%@ Import Namespace="NHST.Models" %>
<%@ Import Namespace="NHST.Bussiness" %>
<%@ Import Namespace="MB.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="mainTit" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="main" runat="server">
    <link rel="stylesheet" href="/App_Themes/NHST/css/style.css" media="all">
    <link rel="stylesheet" href="/App_Themes/NHST/css/responsive.css" media="all">
    <link href="/App_Themes/NewUI/js/select2/select2.css" rel="stylesheet" />
    <link rel="stylesheet" href="/App_Themes/NHST/css/style-custom.css" media="all">
    <asp:Panel ID="pEdit" runat="server">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-white">
                    <div class="panel-heading">
                        <h3 class="panel-title semi-text text-uppercase" style="padding: 30px;">Chi tiết đơn hàng</h3>
                    </div>
                    <div class="panel-body">
                        <div class="table-responsive">
                            <main id="main-wrap">
                                <div class="sec order-detail-sec">
                                    <div class="all">
                                        <div class="main">
                                            <div class="order-panels mar-bot2">
                                                 <a href="javascript:;" onclick="printDiv()" class="btn pill-btn primary-btn admin-btn">In đơn hàng</a>
                                            </div>
                                            <div class="order-panels">
                                                <asp:Panel ID="pnadminmanager" runat="server" Visible="false" CssClass="full-width">
                                                    <div class="order-panel">
                                                        <div class="title">Nhân viên xử lý</div>
                                                        <div class="cont">
                                                            <dl>
                                                                <dt>Nhân viên saler</dt>
                                                                <dd>
                                                                    <asp:DropDownList ID="ddlSaler" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                                        DataTextField="Username" DataValueField="ID">
                                                                    </asp:DropDownList></dd>
                                                                <dt>Nhân viên đặt hàng</dt>
                                                                <dd>
                                                                    <asp:DropDownList ID="ddlDatHang" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                                        DataTextField="Username" DataValueField="ID">
                                                                    </asp:DropDownList></dd>
                                                                <dt>Nhân viên kho TQ</dt>
                                                                <dd>
                                                                    <asp:DropDownList ID="ddlKhoTQ" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                                        DataTextField="Username" DataValueField="ID">
                                                                    </asp:DropDownList></dd>
                                                                <dt>Nhân viên kho VN</dt>
                                                                <dd>
                                                                    <asp:DropDownList ID="ddlKhoVN" runat="server" CssClass="form-control" AppendDataBoundItems="true"
                                                                        DataTextField="Username" DataValueField="ID">
                                                                    </asp:DropDownList></dd>
                                                                <dd>
                                                                    <asp:Button ID="btnStaffUpdate" runat="server" CssClass="btn pill-btn primary-btn admin-btn" Text="CẬP NHẬT" OnClick="btnStaffUpdate_Click" /></dd>
                                                            </dl>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                            <div class="order-panels print6" style="display:none;">
                                                <asp:Literal ID="ltr_OrderCode" runat="server"></asp:Literal>
                                            </div>
                                            <div class="order-panels">
                                                <asp:Literal ID="ltr_OrderFee_UserInfo1" runat="server"></asp:Literal>
                                            </div>
                                            <div class="order-panels">
                                                <asp:Literal ID="ltr_OrderFee_UserInfo" runat="server"></asp:Literal>
                                            </div>
                                            <div class="order-panels">
                                                <div class="order-panel">
                                                    <div class="title">Đánh giá đơn hàng</div>
                                                    <ul class="list-comment">
                                                        <asp:Literal ID="ltr_comment" runat="server"></asp:Literal>
                                                        <%--<li class="item">
                                                        <div class="item-left">
                                                            <span class="avata circle">
                                                                <img src="/App_Themes/NHST/images/user-icon.png" width="100%" />
                                                            </span>
                                                        </div>
                                                        <div class="item-right">
                                                            <strong class="item-username">Phương Nguyễn</strong>
                                                            <span class="item-date">22/12/2015 13:55</span>
                                                            <p class="item-comment">
                                                                Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. 
                                                            </p>
                                                        </div>
                                                    </li>
                                                    <li class="item">
                                                        <span class="item-left avata circle">
                                                            <img src="/App_Themes/NHST/images/icon.png" width="100%" />
                                                        </span>
                                                        <div class="item-right">
                                                            <strong class="item-username">Phương Nguyễn</strong>
                                                            <span class="item-date">22/12/2015 13:55</span>
                                                            <p class="item-comment">
                                                                Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. 
                                                            </p>
                                                        </div>
                                                    </li>--%>
                                                    </ul>
                                                    <asp:Panel ID="pn_sendcomment" runat="server">
                                                        <div class="bottom comment-bottom">
                                                            <div class="comment-input">
                                                                <div class="comment-input-left">
                                                                    <asp:Literal ID="ltr_currentUserImg" runat="server"></asp:Literal>
                                                                </div>
                                                                <div class="comment-input-right">
                                                                    <asp:TextBox ID="txtComment" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rq" runat="server" ValidationGroup="n" ControlToValidate="txtComment" ForeColor="Red" ErrorMessage="Không để trống nội dung."></asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                            <div class="comment-input">
                                                                <asp:DropDownList ID="ddlTypeComment" runat="server" CssClass="form-control full-width">
                                                                    <asp:ListItem Value="0" Text="Chọn khu vực"></asp:ListItem>
                                                                    <asp:ListItem Value="1" Text="Khách hàng"></asp:ListItem>
                                                                    <asp:ListItem Value="2" Text="Nội bộ"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                            <asp:Button ID="btnSend" runat="server" Text="Gửi đánh giá" ValidationGroup="n" CssClass="btn pill-btn primary-btn admin-btn" OnClick="btnSend_Click" />

                                                        </div>
                                                    </asp:Panel>
                                                </div>
                                                <asp:Literal ID="ltr_AddressReceive" runat="server"></asp:Literal>
                                            </div>
                                              <div class="order-panel print3" style="display:none;">
                                                <div class="title">Danh sách sản phẩm</div>
                                                <div class="cont" style="overflow-x:scroll">                                                    
                                                    <table class="tb-product">
                                                        <tr>
                                                            <th class="pro">ID</th>
                                                            <th class="pro">Sản phẩm</th>
                                                            <th class="pro">Thuộc tính</th>
                                                            <th class="qty">Số lượng</th>
                                                            <th class="price">Đơn giá</th>
                                                            <th class="price">Giá sản phẩm CNY</th>                                                          
                                                            <th class="price">Ghi chú riêng sản phẩm</th>
                                                            <th class="price">Trạng thái</th>                                                            
                                                        </tr>
                                                        <asp:Literal ID="ltrProductPrint" runat="server"></asp:Literal>
                                                        <%--<asp:Repeater ID="rpt" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="pro">
                                                                        <%#Eval("ID") %>
                                                                    </td>
                                                                    <td class="pro">
                                                                        <div class="thumb-product">
                                                                            <div class="pd-img">
                                                                                <a href="<%#Eval("link_origin") %>" target="_blank">
                                                                                    <img src="<%#Eval("image_origin") %>" alt="">
                                                                                </a>
                                                                            </div>
                                                                            <div class="info">
                                                                                <a href="<%#Eval("link_origin") %>" target="_blank"><%#Eval("brand") %>
                                                                                </a>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td class="pro">
                                                                        <%#Eval("property") %>
                                                                    </td>
                                                                    <td class="qty"><%#Eval("quantity") %>
                                                                    </td>
                                                                    <td class="price">
                                                                        <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("price_origin"))*Convert.ToDouble(Eval("CurrentCNYVN"))) %> đ</p>
                                                                    </td>
                                                                    <td class="price">
                                                                        <p class=""><i class="fa fa-yen"></i><%#string.Format("{0:#.##}", Convert.ToDouble(Eval("price_origin"))) %></p>
                                                                    </td>
                                                                    <td class="price">
                                                                        <p class=""><i class="fa fa-yen"></i><%#string.Format("{0:#.##}", Convert.ToDouble(Eval("RealPrice"))) %></p>
                                                                    </td>                                                                    
                                                                    <td class="price">                                                                        
                                                                        <p class="">
                                                                            <%# PJUtils.CheckRoleShowRosePrice() == 0?
                                                                                        "<i class=\"fa fa-yen\"></i>"+string.Format("{0:#.##}",Convert.ToDouble(Eval("price_origin")) - Convert.ToDouble(Eval("RealPrice")))+"":"xxx" %>
                                                                        </p>
                                                                    </td>
                                                                    <td class="price">
                                                                        <a class="btn btn-info btn-sm" href='/Admin/ProductEdit.aspx?id=<%#Eval("ID") %>'>Sửa</a>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>--%>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="order-panel ">
                                                <div class="title">Danh sách sản phẩm</div>
                                                <div class="cont">                                                    
                                                    <table class="tb-product">
                                                        <tr>
                                                            <th class="pro">ID</th>
                                                            <th class="pro">Sản phẩm</th>
                                                            <th class="pro">Thuộc tính</th>
                                                            <th class="qty">Số lượng</th>
                                                            <th class="price">Đơn giá</th>
                                                            <th class="price">Giá sản phẩm CNY</th>
                                                            <th class="price">Giá mua thực tế</th>
                                                            <th class="price">Hoa hồng đặt hàng</th>
                                                            <th class="price">Ghi chú riêng sản phẩm</th>
                                                            <th class="price">Trạng thái</th>
                                                            <th class="tool"></th>
                                                        </tr>
                                                        <asp:Literal ID="ltrProducts" runat="server"></asp:Literal>
                                                        <%--<asp:Repeater ID="rpt" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="pro">
                                                                        <%#Eval("ID") %>
                                                                    </td>
                                                                    <td class="pro">
                                                                        <div class="thumb-product">
                                                                            <div class="pd-img">
                                                                                <a href="<%#Eval("link_origin") %>" target="_blank">
                                                                                    <img src="<%#Eval("image_origin") %>" alt="">
                                                                                </a>
                                                                            </div>
                                                                            <div class="info">
                                                                                <a href="<%#Eval("link_origin") %>" target="_blank"><%#Eval("brand") %>
                                                                                </a>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td class="pro">
                                                                        <%#Eval("property") %>
                                                                    </td>
                                                                    <td class="qty"><%#Eval("quantity") %>
                                                                    </td>
                                                                    <td class="price">
                                                                        <p class=""><%#string.Format("{0:N0}", Convert.ToDouble(Eval("price_origin"))*Convert.ToDouble(Eval("CurrentCNYVN"))) %> đ</p>
                                                                    </td>
                                                                    <td class="price">
                                                                        <p class=""><i class="fa fa-yen"></i><%#string.Format("{0:#.##}", Convert.ToDouble(Eval("price_origin"))) %></p>
                                                                    </td>
                                                                    <td class="price">
                                                                        <p class=""><i class="fa fa-yen"></i><%#string.Format("{0:#.##}", Convert.ToDouble(Eval("RealPrice"))) %></p>
                                                                    </td>                                                                    
                                                                    <td class="price">                                                                        
                                                                        <p class="">
                                                                            <%# PJUtils.CheckRoleShowRosePrice() == 0?
                                                                                        "<i class=\"fa fa-yen\"></i>"+string.Format("{0:#.##}",Convert.ToDouble(Eval("price_origin")) - Convert.ToDouble(Eval("RealPrice")))+"":"xxx" %>
                                                                        </p>
                                                                    </td>
                                                                    <td class="price">
                                                                        <a class="btn btn-info btn-sm" href='/Admin/ProductEdit.aspx?id=<%#Eval("ID") %>'>Sửa</a>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>--%>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="order-panels">
                                                <div class="order-panel">
                                                    <div class="title">Chi phí của đơn hàng</div>
                                                    <div class="cont">
                                                        <dl>
                                                            <dt class="full-width"><strong class="title-fee">Phí cố định</strong></dt>
                                                            <dt>Tiền hàng trên web</dt>
                                                            <dd>
                                                                <asp:Label ID="lblTotalMoney" runat="server"></asp:Label></dd>
                                                            <dt>Phí ship Trung Quốc</dt>
                                                            <dd>
                                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                    ID="pCNShipFeeNDT" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="CountShipFee('ndt')"
                                                                    NumberFormat-GroupSizes="3" Width="10%" placeholder="Tiền ship Trung Quốc CNY" Value="0">
                                                                </telerik:RadNumericTextBox>
                                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                    ID="pCNShipFee" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup="CountShipFee('vnd')"
                                                                    NumberFormat-GroupSizes="3" Width="100%" Value="0">
                                                                </telerik:RadNumericTextBox>
                                                                <span class="currency">vnđ</span></dd>
                                                            <dt>Phí mua hàng
                                                                <br />
                                                                (CK:
                                                                <asp:Label ID="lblCKFeebuypro" runat="server"></asp:Label>
                                                                %)</dt>
                                                            <dd>
                                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                    ID="pBuyNDT" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup="CountFeeBuyPro()"
                                                                    NumberFormat-GroupSizes="3" Width="10%" placeholder="Phí mua hàng CNY">
                                                                </telerik:RadNumericTextBox>
                                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                    ID="pBuy" MinValue="0" NumberFormat-DecimalDigits="0"
                                                                    NumberFormat-GroupSizes="3" Width="100%" Value="0">
                                                                </telerik:RadNumericTextBox>
                                                                <span class="currency">vnđ</span></dd>

                                                            <dt>Tổng cân nặng</dt>
                                                            <dd>
                                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                    ID="txtOrderWeight" MinValue="0" NumberFormat-DecimalDigits="2"
                                                                    NumberFormat-GroupSizes="3" Width="100%" Value="0">
                                                                </telerik:RadNumericTextBox>
                                                                <%--<asp:Label ID="lblPacked" runat="server" Text="0"></asp:Label>--%>
                                                                <%--<asp:TextBox ID="txtOrderWeight" Enabled="false" runat="server" CssClass="form-control width-notfull" style="float:left;width:81%;margin-right:5px"></asp:TextBox>--%>
                                                                <span class="currency">KG</span>
                                                            </dd>
                                                            <dt>Phí vận chuyển TQ-VN
                                                                <br />
                                                                (CK
                                                                <asp:Label ID="lblCKFeeWeight" runat="server"></asp:Label>% : 
                                                                <asp:Label ID="lblCKFeeweightPrice" runat="server"></asp:Label>)</dt>
                                                            <dd>
                                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                    ID="pWeightNDT" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="CountFeeWeight('kg')"
                                                                    NumberFormat-GroupSizes="3" Width="10%" placeholder="Phí cân nặng CNY" Value="0">
                                                                </telerik:RadNumericTextBox>
                                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                    ID="pWeight" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup=""
                                                                    NumberFormat-GroupSizes="3" Width="100%" Value="0">
                                                                </telerik:RadNumericTextBox>
                                                                <span class="currency">vnđ</span></dd>

                                                            <dt>Trạng thái đơn hàng</dt>
                                                            <dd>
                                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                                    <%--<asp:ListItem Value="0">Chưa đặt cọc</asp:ListItem>
                                                                    <asp:ListItem Value="1">Hủy đơn hàng</asp:ListItem>
                                                                    <asp:ListItem Value="2">Đã đặt cọc</asp:ListItem>
                                                                    <asp:ListItem Value="3">Chờ duyệt đơn</asp:ListItem>
                                                                    <asp:ListItem Value="4">Đã duyệt đơn</asp:ListItem>
                                                                    <asp:ListItem Value="5">Đã đặt hàng</asp:ListItem>
                                                                    <asp:ListItem Value="6">Đã nhận hàng tại TQ</asp:ListItem>
                                                                    <asp:ListItem Value="7">Đã nhận hàng tại VN</asp:ListItem>
                                                                    <asp:ListItem Value="8">Chờ thanh toán</asp:ListItem>
                                                                    <asp:ListItem Value="9">Đã xong</asp:ListItem>--%>
                                                                </asp:DropDownList></dd>
                                                            <dd class="ordercodes width100">

                                                                <div class="ordercode">
                                                                    <div class="item-element">
                                                                        <span>Mã Vận đơn 1</span>
                                                                        <asp:TextBox ID="txtOrdertransactionCode" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                    <div class="item-element">
                                                                        <span>Cân nặng</span>
                                                                        <asp:TextBox ID="txtOrdertransactionCodeWeight" runat="server" CssClass="form-control" Enabled="false" onkeyup="gettotalweight()"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <asp:Literal ID="ltraddordercode" runat="server"></asp:Literal>

                                                                <div id="oc2" class="ordercode" style="display: none;">
                                                                    <div class="item-element">
                                                                        <span>Mã Vận đơn 2</span>
                                                                        <asp:TextBox ID="txtOrdertransactionCode2" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                    <div class="item-element">
                                                                        <span>Cân nặng</span>
                                                                        <asp:TextBox ID="txtOrdertransactionCodeWeight2" runat="server" CssClass="form-control" Enabled="false" onkeyup="gettotalweight()"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div id="oc3" class="ordercode" style="display: none;">
                                                                    <div class="item-element">
                                                                        <span>Mã Vận đơn 3</span>
                                                                        <asp:TextBox ID="txtOrdertransactionCode3" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                    <div class="item-element">
                                                                        <span>Cân nặng</span>
                                                                        <asp:TextBox ID="txtOrdertransactionCodeWeight3" runat="server" CssClass="form-control" Enabled="false" onkeyup="gettotalweight()"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div id="oc4" class="ordercode" style="display: none;">
                                                                    <div class="item-element">
                                                                        <span>Mã Vận đơn 4</span>
                                                                        <asp:TextBox ID="txtOrdertransactionCode4" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                    <div class="item-element">
                                                                        <span>Cân nặng</span>
                                                                        <asp:TextBox ID="txtOrdertransactionCodeWeight4" runat="server" CssClass="form-control" Enabled="false" onkeyup="gettotalweight()"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                                <div id="oc5" class="ordercode" style="display: none;">
                                                                    <div class="item-element">
                                                                        <span>Mã Vận đơn 5</span>
                                                                        <asp:TextBox ID="txtOrdertransactionCode5" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                    </div>
                                                                    <div class="item-element">
                                                                        <span>Cân nặng</span>
                                                                        <asp:TextBox ID="txtOrdertransactionCodeWeight5" runat="server" CssClass="form-control" Enabled="false" onkeyup="gettotalweight()"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                            </dd>

                                                            <dt>Nhận hàng tại</dt>
                                                            <dd>
                                                                <asp:DropDownList ID="ddlReceivePlace" runat="server" CssClass="form-control" onchange="getplace()" Enabled="false">
                                                                    <asp:ListItem Value="Kho Hà Nội" Text="Kho Hà Nội"></asp:ListItem>
                                                                    <asp:ListItem Value="Kho Hồ Chí Minh" Text="Kho Hồ Chí Minh"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </dd>

                                                            <dt class="full-width"><strong class="title-fee">Phí tùy chọn</strong></dt>
                                                            <dt>
                                                                <asp:CheckBox ID="chkCheck" runat="server" Enabled="false" />
                                                                Phí kiểm đếm
                                                                
                                                            </dt>
                                                            <dd>
                                                                <%--<asp:Label ID="lblCheck" runat="server" Text="0"></asp:Label>--%>
                                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                    ID="pCheckNDT" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="CountCheckPro('ndt')"
                                                                    NumberFormat-GroupSizes="3" Width="10%" placeholder="Phí kiểm đếm CNY" Value="0">
                                                                </telerik:RadNumericTextBox>
                                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                    ID="pCheck" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup="CountCheckPro('vnd')"
                                                                    NumberFormat-GroupSizes="3" Width="100%" Value="0">
                                                                </telerik:RadNumericTextBox>
                                                                <span class="currency">vnđ</span></dd>
                                                            <dt>
                                                                <asp:CheckBox ID="chkPackage" runat="server" Enabled="false" />
                                                                Phí đóng gỗ
                                                            </dt>
                                                            <dd>
                                                                <%--<asp:Label ID="lblPacked" runat="server" Text="0"></asp:Label>--%>
                                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                    ID="pPackedNDT" MinValue="0" NumberFormat-DecimalDigits="2" onkeyup="CountFeePackage('ndt')"
                                                                    NumberFormat-GroupSizes="3" Width="10%" placeholder="Phí đóng gỗ CNY" Value="0">
                                                                </telerik:RadNumericTextBox>
                                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                    ID="pPacked" MinValue="0" NumberFormat-DecimalDigits="0" onkeyup="CountFeePackage('vnd')"
                                                                    NumberFormat-GroupSizes="3" Width="100%" Value="0">
                                                                </telerik:RadNumericTextBox>
                                                                <span class="currency">vnđ</span></dd>
                                                            <dt>
                                                                <asp:CheckBox ID="chkShiphome" runat="server" Enabled="false" />
                                                                Phí ship giao hàng tận nhà
                                                            </dt>
                                                            <dd>
                                                                <%--<asp:Label ID="lblShipHome" runat="server" Text="0"></asp:Label>--%>
                                                                <%--<telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                    ID="pShipHomeNDT" MinValue="0" NumberFormat-DecimalDigits="0"
                                                                    NumberFormat-GroupSizes="3" Width="10%" placeholder="Phí ship giao tận nhà CNY">
                                                                </telerik:RadNumericTextBox>--%>
                                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                    ID="pShipHome" MinValue="0" NumberFormat-DecimalDigits="0"
                                                                    NumberFormat-GroupSizes="3" Width="100%" Value="0">
                                                                </telerik:RadNumericTextBox>
                                                                <span class="currency">vnđ</span></dd>
                                                            <dt>
                                                                <asp:CheckBox ID="chkIsFast" runat="server" Enabled="false" />
                                                                Phí đơn hàng hỏa tốc</dt>
                                                            <dd>
                                                                <asp:Label ID="lblFastPrice" runat="server"></asp:Label>
                                                                vnđ</dd>

                                                            <dt class="full-width"><strong class="title-fee">Thanh toán</strong></dt>
                                                            <dt>Số Tiền phải cọc</dt>
                                                            <dd>
                                                                <%-- <asp:Label ID="lblAmountDeposit" runat="server"></asp:Label>--%>
                                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                    ID="pAmountDeposit" MinValue="0" NumberFormat-DecimalDigits="0"
                                                                    NumberFormat-GroupSizes="3" Width="100%" Enabled="false">
                                                                </telerik:RadNumericTextBox>
                                                                <span class="currency">vnd</span></dd>
                                                            <dt>Số tiền đã trả</dt>
                                                            <dd>
                                                                <%--<telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                    ID="pDepositNDT" MinValue="0" NumberFormat-DecimalDigits="0"
                                                                    NumberFormat-GroupSizes="3" Width="10%" placeholder="Tiền đặt cọc CNY">
                                                                </telerik:RadNumericTextBox>--%>
                                                                <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                    ID="pDeposit" MinValue="0" NumberFormat-DecimalDigits="0"
                                                                    NumberFormat-GroupSizes="3" Width="100%">
                                                                </telerik:RadNumericTextBox>
                                                                <span class="currency">vnd</span></dd>
                                                            <dt>
                                                                <asp:Button ID="btnUpdate" runat="server" CssClass="btn pill-btn primary-btn admin-btn full-width" Text="CẬP NHẬT" OnClick="btnUpdate_Click" />
                                                            </dt>
                                                            <dd>
                                                                <asp:Button ID="btnThanhtoan" runat="server" CssClass="btn pill-btn primary-btn admin-btn full-width" Text="THANH TOÁN" OnClick="btnThanhtoan_Click" Visible="false" />
                                                            </dd>
                                                            <dd></dd>
                                                        </dl>
                                                    </div>
                                                </div>
                                                <div class="order-panel bg-red-nhst print4">
                                                    <div class="title">Tổng tiền hàng khách cần thanh toán</div>
                                                    <div class="cont">
                                                        <dl>
                                                            <dt>Tiền hàng</dt>
                                                            <dd>
                                                                <asp:Label ID="lblMoneyNotFee" runat="server"></asp:Label>
                                                                vnđ</dd>
                                                            <dt>Ship nội địa</dt>
                                                            <dd>
                                                                <asp:Label ID="lblShipTQ" runat="server" Text="0"></asp:Label>
                                                                vnđ</dd>
                                                            <dt>Phí mua hàng</dt>
                                                            <dd>
                                                                <asp:Label ID="lblFeeBuyProduct" runat="server" Text="0"></asp:Label>
                                                                vnđ</dd>
                                                            <dt>Phí tùy chọn</dt>
                                                            <dd>
                                                                <asp:Label ID="lblAllFee" runat="server" Text="0"></asp:Label>
                                                                vnđ</dd>
                                                            <dt>Phí vận chuyển TQ - VN</dt>
                                                            <dd>
                                                                <asp:Label ID="lblFeeTQVN" runat="server" Text="0"></asp:Label>
                                                                vnđ</dd>
                                                            <%-- <telerik:RadNumericTextBox runat="server" CssClass="form-control width-notfull" Skin="MetroTouch"
                                                                    ID="pShipTQToVN" MinValue="0" NumberFormat-DecimalDigits="0"
                                                                    NumberFormat-GroupSizes="3" Width="100%">
                                                                </telerik:RadNumericTextBox> vnđ</dd>--%>
                                                            <dt>Tổng chi phí</dt>
                                                            <dd>
                                                                <asp:Label ID="lblAllTotal" runat="server" Text="0"></asp:Label>
                                                                vnđ</dd>
                                                            <dt>Đã thanh toán</dt>
                                                            <dd>
                                                                <asp:Label ID="lblDeposit" runat="server" Text="0"></asp:Label>
                                                                vnđ</dd>
                                                            <dt>Cần thanh toán</dt>
                                                            <dd>
                                                                <asp:Label ID="lblLeftPay" runat="server" Text="0"></asp:Label>
                                                                vnđ</dd>
                                                        </dl>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--<asp:Panel ID="pn" runat="server" Visible="false">
                                                <div class="order-panels">
                                                    <a class="btn pill-btn primary-btn" href="javascript:;" onclick="cancelOrder()">Hủy đơn hàng</a>
                                                    <asp:Button ID="btn_cancel" runat="server" CssClass="btn pill-btn primary-btn" Text="Hủy đơn hàng" Style="display: none;" OnClick="btn_cancel_Click" />
                                                </div>
                                            </asp:Panel>--%>
                                            <div class="order-panel print5">
                                                <div class="title">Lịch sử thanh toán </div>
                                                <div class="cont">
                                                    <table class="tb-product">
                                                        <tr>
                                                            <th class="pro">Ngày thanh toán</th>
                                                            <th class="pro">Loại thanh toán</th>
                                                            <th class="pro">Hình thức thanh toán</th>
                                                            <th class="qty">Số tiền</th>
                                                        </tr>
                                                        <asp:Repeater ID="rptPayment" runat="server">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td class="pro">
                                                                        <%#Eval("CreatedDate","{0:dd/MM/yyyy}") %>
                                                                    </td>
                                                                    <td class="pro">
                                                                        <%# PJUtils.ShowStatusPayHistory( Eval("Status").ToString().ToInt()) %>
                                                                    </td>
                                                                    <td class="pro">
                                                                        <%#Eval("Type").ToString() == "1"?"Trực tiếp":"Ví điện tử" %>
                                                                    </td>
                                                                    <td class="qty"><%#Eval("Amount","{0:N0}") %> VNĐ
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                        <asp:Literal ID="ltrpa" runat="server"></asp:Literal>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </main>
                        </div>
                    </div>
                </div>
                <!-- Modal -->
            </div>
        </div>

        <div id="printcontent" class="printdetail" style="display: none;">
        </div>

    </asp:Panel>
    <asp:HiddenField ID="hdfcurrent" runat="server" />
    <asp:HiddenField ID="hdfFeeBuyProDiscount" runat="server" />
    <asp:HiddenField ID="hdfFeeWeightDiscount" runat="server" />
    <asp:HiddenField ID="hdfFeeweightPriceDiscount" runat="server" />
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="gr">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                    <telerik:AjaxUpdatedControl ControlID="pEdit" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnUpdate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pEdit" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSend">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="pEdit" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="btnSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="gr" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <style>
        .sec {
            padding: 20px 0;
        }

        .main {
            width: 91%;
        }

        dl dd {
            display: block;
            padding-left: 0px;
            float: left;
            width: 50%;
            margin-bottom: 15px;
        }

        select.form-control {
            line-height: 25px;
        }

        .riSingle {
            width: 40% !important;
        }

        .admin-btn {
            float: right;
            clear: both;
            margin-top: 10px;
            line-height: 30px;
        }

        .order-panel dl dt {
            width: 40%;
            clear: both;
        }

        .ordercodes {
            width: 100%;
        }

        .ordercode {
            float: left;
            width: 100%;
            clear: both;
        }

            .ordercode .item-element {
                float: left;
                width: 50%;
                padding: 0 10px;
            }

        .addordercode {
            padding: 0 10px;
            margin: 20px 0;
            background: url('/App_Themes/NewUI/images/icon-plus.png') center left no-repeat;
        }

            .addordercode a {
                padding-left: 30px;
            }

        .title-fee {
            float: left;
            width: 100%;
            border-bottom: solid 1px #ccc;
            font-size: 20px;
            margin: 20px 0;
            color: #000;
        }

        .bg-red-nhst {
            background: #e84545;
            color: #fff;
        }

            .bg-red-nhst dt, .bg-red-nhst .title {
                color: #fff;
            }

        .order-panel .title {
            border-bottom: solid 1px #fff;
        }
    </style>
    <asp:HiddenField ID="hdforderamount" runat="server" />
    <asp:HiddenField ID="hdfoc2" runat="server" />
    <asp:HiddenField ID="hdfoc3" runat="server" />
    <asp:HiddenField ID="hdfoc4" runat="server" />
    <asp:HiddenField ID="hdfoc5" runat="server" />
    <asp:HiddenField ID="hdfReceivePlace" runat="server" />
    <asp:HiddenField ID="hdfFeeTQVNHN" runat="server" />
    <asp:HiddenField ID="hdfFeeTQVNHCM" runat="server" />

    <telerik:RadScriptBlock ID="rsc" runat="server">
        <script type="text/javascript">
            function printDiv() {
                var html = "";

                $('link').each(function () { // find all <link tags that have
                    if ($(this).attr('rel').indexOf('stylesheet') != -1) { // rel="stylesheet"
                        html += '<link rel="stylesheet" href="' + $(this).attr("href") + '" />';
                    }
                });
                html += '<body onload="window.focus(); window.print()">' + $("#printcontent").html() + '</body>';
                var w = window.open("", "print");
                if (w) { w.document.write(html); w.document.close() }
            }
            $(document).ready(function () {
                $(".print6").clone().appendTo(".printdetail").show();
                $(".print1").clone().appendTo(".printdetail");
                $(".print2").clone().appendTo(".printdetail");
                $(".print3").clone().appendTo(".printdetail").show();
                $(".print4").clone().appendTo(".printdetail");
                $(".print5").clone().appendTo(".printdetail");
                

                var oc2 = $("#<%= hdfoc2.ClientID%>").val();
                var oc3 = $("#<%= hdfoc3.ClientID%>").val();
                var oc4 = $("#<%= hdfoc4.ClientID%>").val();
                var oc5 = $("#<%= hdfoc5.ClientID%>").val();

                if (oc2 == "1") {
                    $("#oc2").show();
                }
                if (oc3 == "1") {
                    $("#oc3").show();
                }
                if (oc4 == "1") {
                    $("#oc4").show();
                }
                if (oc5 == "1") {
                    $("#oc5").show();
                }
            });
            function isEmpty(str) {
                return !str.replace(/^\s+/g, '').length; // boolean (`true` if field is empty)
            }
            var currency = $("#<%=hdfcurrent.ClientID%>").val();
            function CountShipFee(type) {
                var shipfeendt = $("#<%= pCNShipFeeNDT.ClientID%>").val();
                var shipfeevnd = $("#<%= pCNShipFee.ClientID%>").val();
                if (type == "vnd") {
                    if (isEmpty(shipfeevnd) != true) {
                        var ndt = shipfeevnd / currency;
                        $("#<%= pCNShipFeeNDT.ClientID%>").val(ndt);
                    }
                    else {
                        $("#<%= pCNShipFee.ClientID%>").val(0);
                        $("#<%= pCNShipFeeNDT.ClientID%>").val(0);
                    }
                }
                else {
                    if (!isEmpty(shipfeendt)) {
                        var vnd = shipfeendt * currency;
                        $("#<%= pCNShipFee.ClientID%>").val(vnd);
                    }
                    else {
                        $("#<%= pCNShipFee.ClientID%>").val(0);
                        $("#<%= pCNShipFeeNDT.ClientID%>").val(0);
                    }
                }
            }

            function CountCheckPro(type) {
                var pCheckNDT = $("#<%= pCheckNDT.ClientID%>").val();
                var pCheckVND = $("#<%= pCheck.ClientID%>").val();
                if (type == "vnd") {
                    if (!isEmpty(pCheckVND)) {
                        var ndt = pCheckVND / currency;
                        $("#<%= pCheckNDT.ClientID%>").val(ndt);
                    }
                    else {
                        $("#<%= pCheckNDT.ClientID%>").val(0);
                        $("#<%= pCheck.ClientID%>").val(0);
                    }
                }
                else {
                    if (!isEmpty(pCheckNDT)) {
                        var vnd = pCheckNDT * currency;
                        $("#<%= pCheck.ClientID%>").val(vnd);
                    }
                    else {
                        $("#<%= pCheckNDT.ClientID%>").val(0);
                        $("#<%= pCheck.ClientID%>").val(0);
                    }
                }
            }
            function CountFeePackage(type) {
                var pPackedNDT = $("#<%= pPackedNDT.ClientID%>").val();
                var pPackedVND = $("#<%= pPacked.ClientID%>").val();
                if (type == "vnd") {
                    if (!isEmpty(pPackedVND)) {
                        var ndt = pPackedVND / currency;
                        $("#<%= pPackedNDT.ClientID%>").val(ndt);
                    }
                    else {
                        $("#<%= pPackedNDT.ClientID%>").val(0);
                        $("#<%= pPacked.ClientID%>").val(0);
                    }
                }
                else {
                    if (!isEmpty(pPackedNDT)) {
                        var vnd = pPackedNDT * currency;
                        $("#<%= pPacked.ClientID%>").val(vnd);
                    }
                    else {
                        $("#<%= pPackedNDT.ClientID%>").val(0);
                        $("#<%= pPacked.ClientID%>").val(0);
                    }
                }
            }
            function CountFeeBuyPro() {
                var pBuyNotDC = $("#<%= pBuyNDT.ClientID%>").val();
                var pBuyDC = $("#<%= pBuy.ClientID%>").val();

                var discountper = $("#<%= hdfFeeBuyProDiscount.ClientID%>").val();
                var subfee = pBuyNotDC * discountper / 100;
                var vnd = (pBuyNotDC - subfee) * currency;
                $("#<%= pBuy.ClientID%>").val(vnd);
            }
            function addordercode() {
                var count = 0;
                count = parseInt($("#<%=hdforderamount.ClientID %>").val());

                if (count == 5) {
                    return;
                }
                else {
                    count = count + 1;
                    $("#<%=hdforderamount.ClientID %>").val(count);
                    var occ = "oc" + count;
                    $("#" + occ + "").show();
                }
            }
            function CountFeeWeight(type) {
                var pWeightNDT = $("#<%= pWeightNDT.ClientID%>").val();
                var pWeightVND = $("#<%= pWeight.ClientID%>").val();
                var discountper = $("#<%= hdfFeeWeightDiscount.ClientID%>").val();

                var receiveplace = $("#<%= hdfReceivePlace.ClientID%>").val();
                var hnfee = $("#<%= hdfFeeTQVNHN.ClientID%>").val();
                var hcmfee = $("#<%= hdfFeeTQVNHCM.ClientID%>").val();
                var countfeearea = "";
                if (receiveplace == "1") {
                    countfeearea = hnfee;
                }
                else {
                    countfeearea = hcmfee;
                }
                var totalweight = parseFloat(pWeightNDT);
                if (totalweight > 0) {
                    //var leftweight = totalweight - 1;
                    var leftweight = totalweight;
                    var totalfeeweight = 0;
                    //var firstfeeweight = 100000;
                    var firstfeeweight = 0;
                    if (type == "kg") {
                        var steps = countfeearea.split('|');
                        if (steps.length > 0) {
                            for (var i = 0; i < steps.length - 1; i++) {
                                var step = steps[i];
                                var itemstep = step.split(',');
                                var wf = parseFloat(itemstep[1]);
                                var wt = parseFloat(itemstep[2]);
                                var amount = parseFloat(itemstep[3]);
                                if (totalweight >= wf && totalweight <= wt) {
                                    totalfeeweight = firstfeeweight + (leftweight * amount);
                                }
                            }
                        }
                        var vnd = totalfeeweight;
                        var subfee = vnd * discountper / 100;
                        vnd = vnd - subfee;
                        $("#<%= lblCKFeeweightPrice.ClientID%>").text(parseInt(subfee));
                        $("#<%= hdfFeeweightPriceDiscount.ClientID%>").val(parseInt(subfee));
                        $("#<%= pWeight.ClientID%>").val(vnd);
                    }
                }
                else {
                    $("#<%= lblCKFeeweightPrice.ClientID%>").text(parseInt(0));
                    $("#<%= hdfFeeweightPriceDiscount.ClientID%>").val(parseInt(0));
                    $("#<%= pWeight.ClientID%>").val(0);
                }
            }
            function gettotalweight() {
                //txtOrderWeight, txtOrdertransactionCodeWeight
                var ocw = $("#<%= txtOrdertransactionCodeWeight.ClientID%>").val();
                var ocw2 = $("#<%= txtOrdertransactionCodeWeight2.ClientID%>").val();
                var ocw3 = $("#<%= txtOrdertransactionCodeWeight3.ClientID%>").val();
                var ocw4 = $("#<%= txtOrdertransactionCodeWeight4.ClientID%>").val();
                var ocw5 = $("#<%= txtOrdertransactionCodeWeight5.ClientID%>").val();
                var receiveplace = $("#<%= hdfReceivePlace.ClientID%>").val();
                var hnfee = $("#<%= hdfFeeTQVNHN.ClientID%>").val();
                var hcmfee = $("#<%= hdfFeeTQVNHCM.ClientID%>").val();
                var countfeearea = "";
                if (receiveplace == "1") {
                    countfeearea = hnfee;
                }
                else {
                    countfeearea = hcmfee;
                }
                //alert(countfeearea);

                if (isEmpty(ocw)) {
                    ocw = 0;
                }
                if (isEmpty(ocw2)) {
                    ocw2 = 0;
                }
                if (isEmpty(ocw3)) {
                    ocw3 = 0;
                }
                if (isEmpty(ocw4)) {
                    ocw4 = 0;
                }
                if (isEmpty(ocw5)) {
                    ocw5 = 0;
                }
                var totalweight = parseFloat(ocw) + parseFloat(ocw2) + parseFloat(ocw3) + parseFloat(ocw4) + parseFloat(ocw5);
                var currency = $("#<%=hdfcurrent.ClientID%>").val();
                //var firstfeeweight = 100000;
                var firstfeeweight = 0;
                var firstfeepacked = 20;

                var leftweight = totalweight;
                //var leftweight = totalweight - 1;


                var totalfeeweight = 0;

                var steps = countfeearea.split('|');
                if (steps.length > 0) {
                    for (var i = 0; i < steps.length - 1; i++) {
                        var step = steps[i];
                        var itemstep = step.split(',');
                        var wf = parseFloat(itemstep[1]);
                        var wt = parseFloat(itemstep[2]);
                        var amount = parseFloat(itemstep[3]);
                        if (totalweight >= wf && totalweight <= wt) {
                            totalfeeweight = firstfeeweight + (leftweight * amount);
                        }
                    }
                }

                var feepackedndt = leftweight * 1 + 20;
                var feepackedvnd = feepackedndt * currency;

                var pweightndt = totalfeeweight / currency;

                //$("#<%= pPackedNDT.ClientID %>").val(feepackedndt);
                //$("#<%= pPacked.ClientID %>").val(feepackedvnd);
                //$("#<%= pWeight.ClientID %>").val(totalfeeweight);
                $("#<%= pWeightNDT.ClientID %>").val(totalweight);

                $("#<%= txtOrderWeight.ClientID %>").val(totalweight);
                CountFeeWeight("kg");
            }
            function getplace() {
                var value = $("#<%= ddlReceivePlace.ClientID%>").val();
                if (value == "Kho Hà Nội") {
                    $("#<%= hdfReceivePlace.ClientID%>").val(1);
                }
                else {
                    $("#<%= hdfReceivePlace.ClientID%>").val(2);
                }
                gettotalweight();
                CountFeeWeight('kg');
            }
        </script>
    </telerik:RadScriptBlock>
</asp:Content>
