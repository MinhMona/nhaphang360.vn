<%@ Page Title="" Language="C#" MasterPageFile="~/ClientMaster.Master" AutoEventWireup="true" CodeBehind="nap-tien.aspx.cs" Inherits="NHST.nap_tien" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="all">
            <div class="main">
                <div class="sec form-sec">
                    <div class="sec-tt">
                        <h2 class="tt-txt">Nạp tiền</h2>
                        <p class="deco">
                            <img src="/App_Themes/NHST/images/title-deco.png" alt="">
                        </p>
                    </div>
                    <div class="primary-form custom-width">
                        <table class="customer-table mar-bot3 full-width font-size-16">
                            <tr>
                                <td>Số dư tài khoản
                                </td>
                                <td>
                                    <asp:Label ID="lblAccount" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div class="step-income">
                            <h1 class="font-size-16">Cách 1: Chuyển khoản qua ngân hàng</h1>
                            <table class="customer-table mar-top1">
                                <tr>
                                    <th width="10%">Ngân hàng</th>
                                    <th width="60%">Thông tin tài khoản</th>
                                </tr>
                                <tr>
                                    <td>
                                        <ul class="cardList clearfix">
                                            <li><i title="Ngân hàng TMCP Ngoại Thương Việt Nam" class="VCB"></i></li>
                                        </ul>
                                    </td>
                                    <td>
                                        <strong class="font-size-16">Ngân hàng TMCP Ngoại Thương Việt Nam</strong><br />
                                        <span class="label-NH">Số tài khoản: </span><i>0021000391641 </i>
                                        <br />
                                        <span class="label-NH">Chủ tài khoản: </span><i>Nguyễn Đức Duy</i><br />
                                        <span class="label-NH">Chi nhánh: </span><i>Chi nhánh Hà Nội</i><br />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <ul class="cardList clearfix">
                                            <li><i class="TCB" title="Ngân hàng thương mại cổ phần Kỹ Thương Việt Nam"></i></li>
                                        </ul>
                                    </td>
                                    <td>
                                        <strong class="font-size-16">Ngân hàng thương mại cổ phần Kỹ Thương Việt Nam</strong><br />
                                        <span class="label-NH">Số tài khoản: </span><i>19030928327997</i><br />
                                        <span class="label-NH">Chủ tài khoản: </span><i>Nguyễn Đức Duy</i><br />
                                        <span class="label-NH">Chi nhánh: </span><i>Chi nhánh Thăng Long</i><br />
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <ul class="cardList clearfix">
                                            <li><i title="Ngân hàng TMCP Công Thương Việt Nam" class="ICB"></i></li>
                                        </ul>
                                    </td>
                                    <td>
                                        <strong class="font-size-16">Ngân hàng TMCP Công Thương Việt Nam</strong><br />
                                        <span class="label-NH">Số tài khoản: </span><i>101003761228</i><br />
                                        <span class="label-NH">Chủ tài khoản: </span><i>Nguyễn Đức Duy</i><br />
                                        <span class="label-NH">Chi nhánh: </span><i>Chi nhánh Hoàn Kiếm</i><br />
                                    </td>
                                </tr>
                            </table>
                            <div class="row-1col mar-top2">
                                <label class="width-full font-size-18">
                                    <strong>Nội dung chuyển khoản: NAP Username "Số điện thoại của bạn (không bắt buộc, dùng để NHST liên hệ khi gặp sự cố)"</strong>
                                    <br />
                                    <br />
                                    <span class="example">Ví dụ: NAP
                                    <asp:Label ID="lblIDUser" runat="server"></asp:Label>
                                        0912834567</span>
                                </label>
                            </div>
                        </div>
                    <div class="step-income">
                        <h1 class="font-size-16 mar-top3">Cách 2: Chuyển khoản qua ATM hoặc các dịch vụ khác</h1>
                        <div class="row-1col mar-top2">
                            <label class="width-full font-size-18">
                                Quý khách hàng thực hiện chuyển khoản qua ATM hoặc các dịch vụ không có ghi chú, vui lòng liên hệ thông báo cho bộ phận DVKH
                            </label>
                        </div>
                    </div>
                    <div class="step-income">
                        <h1 class="font-size-16 mar-top3">Cách 3: Chuyển khoản qua ATM hoặc các dịch vụ khác</h1>
                        <div class="row-1col mar-top2">
                            <label class="width-full font-size-18">
                                Quý khách có thể nạp tiền trực tiếp tại văn phòng giao dịch của nhaphangsieutoc. Để đảm bảo an toàn về mặt tài chính cho Quý khách, 
                                chúng tôi khuyến cáo với các lần nạp có số tiền hơn 10 triệu đồng, Quý khách vui lòng sử dụng hình thức chuyển khoản.<br />
                                Xin cảm ơn!

                            </label>
                        </div>
                    </div>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
