<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="printPhieu.aspx.cs" Inherits="NHST.printPhieu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="/App_Themes/vcdqg/css/style-P.css" />
</head>
<body>
    <form id="form1" runat="server">

        <div class="print-bill">
            <div class="top">
                <div class="left">
                    <span class="company-info">NHẬP HÀNG 360</span>
                    <span class="company-info">Địa chỉ: 165 Thái Hà, Đống Đa, Hà Nội</span>
                </div>
                <div class="right">
                    <span class="bill-num">Mẫu số 01 - TT</span>
                    <span class="bill-promulgate-date">(Ban hành theo Thông tư số 133/2016/TT-BTC ngày 26/8/2016 của Bộ Tài chính)			
                    </span>
                </div>
            </div>
            <div class="bill-title">
                <h1>PHIẾU THU</h1>
                <span class="bill-date">Ngày 30 tháng 11 năm 2017</span>
            </div>
            <div class="bill-content">
                <%--<div class="bill-row">
                    <label class="row-name" style="width: 11%;">Họ và tên người nộp tiền: </label>
                    <label class="row-info" style="width: 89%;">Nguyễn Văn A</label>
                </div>
                <div class="bill-row">
                    <label class="row-name" style="width: 3.5%;">Địa chỉ: </label>
                    <label class="row-info" style="width: 96.5%;">151 Hồ Bá Kiện, Phường 15, Quận 10, Tp.HCM</label>
                </div>
                <div class="bill-row">
                    <label class="row-name" style="width: 5%;">Lý do nộp: </label>
                    <label class="row-info" style="width: 95%;">Chuyển tiền vào tài khoản ví</label>
                </div>
                <div class="bill-row">
                    <label class="row-name" style="width: 3.5%;">Số tiền: </label>
                    <label class="row-info" style="width: 96.5%;">2.000.000</label>
                </div>
                <div class="bill-row">
                    <label class="row-name" style="width: 5%;">Bằng chữ: </label>
                    <label class="row-info" style="width: 95%;">Hai triệu đồng chẵn</label>
                </div>
                <div class="bill-row">
                    <div class="row-col">
                        <label class="row-name" style="width: 10%;">Kèm theo: </label>
                        <label class="row-info" style="width: 88%; height: 18px;"></label>
                    </div>
                    <div class="row-col">
                        <label class="row-name" style="width: 13%;">Chứng từ gốc: </label>
                        <label class="row-info" style="width: 87%; height: 18px;"></label>
                    </div>
                </div>--%>
                <div class="bill-row">
                    <label class="row-name">Họ và tên người nộp tiền: </label>
                    <label class="row-info">Nguyễn Văn A</label>
                </div>
                <div class="bill-row">
                    <label class="row-name">Địa chỉ: </label>
                    <label class="row-info">151 Hồ Bá Kiện, Phường 15, Quận 10, Tp.HCM</label>
                </div>
                <div class="bill-row">
                    <label class="row-name">Lý do nộp: </label>
                    <label class="row-info">Chuyển tiền vào tài khoản ví</label>
                </div>
                <div class="bill-row">
                    <label class="row-name">Số tiền: </label>
                    <label class="row-info">2.000.000</label>
                </div>
                <div class="bill-row">
                    <label class="row-name">Bằng chữ: </label>
                    <label class="row-info">Hai triệu đồng chẵn</label>
                </div>
                <div class="bill-row">
                    <div class="row-col">
                        <label class="row-name">Kèm theo: </label>
                        <label class="row-info"></label>
                    </div>
                    <div class="row-col">
                        <label class="row-name">Chứng từ gốc: </label>
                        <label class="row-info"></label>
                    </div>
                </div>
            </div>
            <div class="bill-footer">
                <div class="bill-row-one">
                    <strong>Giám đốc</strong>
                    <span class="note">(Ký, họ tên, đóng dấu)</span>
                </div>
                <div class="bill-row-one">
                    <strong>Kế toán trưởng</strong>
                    <span class="note">(Ký, họ tên)</span>
                </div>
                <div class="bill-row-one">
                    <strong>Người nộp tiền</strong>
                    <span class="note">(Ký, họ tên)</span>
                </div>
                <div class="bill-row-one">
                    <strong>Thủ quỹ</strong>
                    <span class="note">(Ký, họ tên)</span>
                </div>
            </div>
        </div>
        <div class="print-bill">
            <div class="top">
                <div class="left">
                    <span class="company-info">NHẬP HÀNG 360</span>
                    <span class="company-info">Địa chỉ: T6/08 Phạm Văn Đồng Hà Nội</span>
                </div>
                <div class="right">
                    <span class="bill-num">Mẫu số 01 - TT</span>
                    <span class="bill-promulgate-date">(Ban hành theo Thông tư số 133/2016/TT-BTC ngày 26/8/2016 của Bộ Tài chính)			
                    </span>
                </div>
            </div>
            <div class="bill-title">
                <h1>PHIẾU CHI</h1>
                <span class="bill-date">Ngày 01 tháng 12 năm 2017</span>
            </div>
            <div class="bill-content">
                <div class="bill-row">
                    <label class="row-name">Họ và tên người nhận tiền: </label>
                    <label class="row-info">Nguyễn Văn A</label>
                </div>
                <div class="bill-row">
                    <label class="row-name">Địa chỉ: </label>
                    <label class="row-info">151 Hồ Bá Kiện, Phường 15, Quận 10, Tp.HCM</label>
                </div>
                <div class="bill-row">
                    <label class="row-name">Lý do chi: </label>
                    <label class="row-info">Rút tiền khỏi tài khoản ví</label>
                </div>
                <div class="bill-row">
                    <label class="row-name">Số tiền: </label>
                    <label class="row-info">2.000.000</label>
                </div>
                <div class="bill-row">
                    <label class="row-name">Bằng chữ: </label>
                    <label class="row-info">Hai triệu đồng chẵn</label>
                </div>
                <div class="bill-row">
                    <div class="row-col">
                        <label class="row-name">Kèm theo: </label>
                        <label class="row-info"></label>
                    </div>
                    <div class="row-col">
                        <label class="row-name">Chứng từ gốc: </label>
                        <label class="row-info"></label>
                    </div>
                </div>
            </div>
            <div class="bill-footer">
                <div class="bill-row-all right">
                    Ngày 01 tháng 12 năm 2017
                </div>
            </div>
            <div class="bill-footer">
                <div class="bill-row-one">
                    <strong>Giám đốc</strong>
                    <span class="note">(Ký, họ tên, đóng dấu)</span>
                </div>
                <div class="bill-row-one">
                    <strong>Kế toán trưởng</strong>
                    <span class="note">(Ký, họ tên)</span>
                </div>
                <div class="bill-row-one">
                    <strong>Người nộp tiền</strong>
                    <span class="note">(Ký, họ tên)</span>
                </div>
                <div class="bill-row-one">
                    <strong>Thủ quỹ</strong>
                    <span class="note">(Ký, họ tên)</span>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
