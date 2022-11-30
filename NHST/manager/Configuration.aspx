<%@ Page Title="Cấu hình hệ thống" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="Configuration.aspx.cs" Inherits="NHST.manager.Configuration" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">CẤU HÌNH HỆ THỐNG NHẬP HÀNG 360</h4>

                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="order-detail-wrap col s12 section setting">
                <div class="row">
                    <div class="col s12 m12 l4 sticky-wrap">
                        <div class="hide-on-small-only card-panel">
                            <ul class="table-of-contents ">
                                <li><a href="#general-sec">Cấu hình chung</a></li>
                                <li><a href="#social-sec">Cấu hình tài khoản MXH</a></li>
                                <li><a href="#value-sec">Cấu hình tỉ giá - hoa hồng</a></li>
                                <li><a href="#noti-sec">Cấu hình thông báo</a></li>
                                <%--<li><a href="#footer-sec">Cấu hình Footer</a></li>--%>
                               <%-- <li><a href="#seo-sec">Cấu hình SEO trang chủ và mặc định</a></li>--%>
                            </ul>
                        </div>
                        <div class="right-action">
                            <a class="btn" onclick="myNewFunction()">Cập nhật</a>
                            <asp:Button runat="server" ID="btnUpdate" class="btn" Text="Cập nhật" Style="display: none" OnClick="btnUpdate_Click"></asp:Button>
                        </div>

                    </div>
                    <div class="col s12 m12 l8">
                        <div class="card-panel">
                            <div id="general-sec" class="section scrollspy fee-order">
                                <div class="title-header bg-dark-gradient">
                                    <h6 class="white-text ">Cấu hình chung</h6>
                                </div>
                                <div class="child-fee">
                                    <div class="content-panel">
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Tên website</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtWebName"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                         <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Logo</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <div style="display: inline-block; margin-left: 15px; padding-bottom: 5px">
                                                            <asp:FileUpload ID="imgLogo" runat="server" class="upload-img" type="file" onchange="previewFiles(this);" title="" />
                                                            <button class="btn-upload" type="button">Upload</button>
                                                        </div>
                                                        <div class="preview-img-default">
                                                            <asp:Image ID="imgLogoBefore" Style="max-height: 100px; max-width: 100px;" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Tên công ty viết tắt</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtCompanyShortName"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Tên công ty ngắn</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtCompanyName"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Tên công ty dài</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtCompanyLongName"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Mã số thuế</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtTaxCode"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Địa chỉ 1</p>
                                            </div>
                                            <div class="right-content" style="width: min-content;">
                                                <div class="tiny-editor"></div>
                                            </div>
                                            <asp:HiddenField runat="server" ID="hdfAddress1" />

                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Địa chỉ 2</p>
                                            </div>
                                            <div class="right-content" style="width: min-content;">
                                                <div class="tiny-editor"></div>
                                            </div>
                                            <asp:HiddenField runat="server" ID="hdfAddress2" />

                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Về chúng tôi</p>
                                            </div>
                                            <div class="right-content" style="width: min-content;">
                                                <div class="tiny-editor"></div>
                                            </div>
                                            <asp:HiddenField runat="server" ID="hdfAbout" />
                                            <%-- <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" id="txtAbout"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>--%>
                                        </div>

                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Giờ hoạt động</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtWorkingTime"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Email liên lạc</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="email" ID="txtEmailContact"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Email hỗ trợ</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="email" ID="txtEmailSupport"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Hotline</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtHotline"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Hotline hỗ trợ</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtHotlineSupport"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Hotline phản hồi</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtHotlineFeedback"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card-panel">
                            <div id="social-sec" class="section scrollspy fee-total">
                                <div class="title-header bg-dark-gradient">
                                    <h6 class="white-text ">Cấu hình tài khoản mạng xã hội</h6>
                                </div>
                                <div class="conten-panel pt-2">
                                    <div class="row">
                                        <div class="input-field col s12 m6" style="display:none">
                                            <asp:TextBox runat="server" placeholder="#" type="text" ID="txtFacebook" value=""></asp:TextBox>
                                            <label>Facebook</label>
                                        </div>
                                        <div class="input-field col s12 m12">
                                            <asp:TextBox runat="server" placeholder="#" type="text" ID="txtTwitter" value=""></asp:TextBox>
                                            <label>Messenger-App</label>
                                        </div>
                                        <div class="input-field col s12 m6" style="display:none">
                                            <asp:TextBox runat="server" placeholder="#" type="text" ID="txtGooglePlus" value=""></asp:TextBox>
                                            <label>Google+</label>
                                        </div>
                                        <div class="input-field col s12 m6"style="display:none">
                                            <asp:TextBox runat="server" placeholder="#" type="text" ID="txtPinterest" value=""></asp:TextBox>
                                            <label>Pinterest</label>
                                        </div>
                                        <div class="input-field col s12 m6" style="display:none">
                                            <asp:TextBox runat="server" placeholder="#" type="text" ID="txtInstagram" value=""></asp:TextBox>
                                            <label>Instagram</label>
                                        </div>
                                        <div class="input-field col s12 m6" style="display:none">
                                            <asp:TextBox runat="server" placeholder="#" type="text" ID="txtSkype" value=""></asp:TextBox>
                                            <label>Skype</label>
                                        </div>
                                        <div class="input-field col s12 m6" style="display:none">
                                            <asp:TextBox runat="server" placeholder="#" type="text" ID="txtLinkYoutube" value=""></asp:TextBox>
                                            <label>Link Youtube</label>
                                        </div>
                                        <div class="input-field col s12 m12">
                                            <asp:TextBox runat="server" placeholder="#" type="text" ID="txtLinkZalo" value=""></asp:TextBox>
                                            <label>Link Zalo1</label>
                                        </div>
                                         <div class="input-field col s12 m12" style="display:none">
                                            <asp:TextBox runat="server" placeholder="#" type="text" ID="txtLinkZalo2" value=""></asp:TextBox>
                                            <label>Link Zalo2</label>
                                        </div>
                                         <div class="input-field col s12 m12">
                                            <asp:TextBox runat="server" placeholder="#" type="text" ID="txtLinkZalo3" value=""></asp:TextBox>
                                            <label>Link Zalo3</label>
                                        </div>
                                         <div class="input-field col s12 m12" style="display:none">
                                            <asp:TextBox runat="server" placeholder="#" type="text" ID="txtLinkZalo4" value=""></asp:TextBox>
                                            <label>Link Zalo4</label>
                                        </div>
                                           <div class="input-field col s12 m12" style="display:none">
                                            <asp:TextBox runat="server" placeholder="#" type="text" ID="txtLinkZalo5" value=""></asp:TextBox>
                                            <label>Link Zalo5</label>
                                        </div>
                                        <div class="input-field col s12 m6" style="display:none">
                                            <asp:TextBox runat="server" placeholder="#" type="text" ID="txtLinkWeChat" value=""></asp:TextBox>
                                            <label>Link Wechat</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card-panel">
                            <div id="value-sec" class="section scrollspy fee-order">
                                <div class="title-header bg-dark-gradient">
                                    <h6 class="white-text ">Cấu hình tỉ giá và hoa hồng</h6>
                                </div>
                                <div class="child-fee">
                                    <div class="content-panel">
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Tỷ giá TQ - VN</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="number" ID="txtCNY_VN"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                         <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Tỷ giá thật tính lợi nhuận</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="number" ID="txtCurrencyReal"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-row" style="display:none;">
                                            <div class="left-fixed">
                                                <p class="txt">Giá tiền mặc định thanh toán hộ</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtPriceDefault"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Phần trăm nhân viên sale trong 3 tháng đầu</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="number" ID="txtPercentIn3M"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Phần trăm nhân viên sale sau 3 tháng</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="number" ID="txtPercentAfter3M"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Phần trăm nhân viên đặt hàng</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="number" ID="txtPercentStaffOrder"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Phần trăm tiền bảo hiểm đơn hàng</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="number" ID="txtInsurancePercent"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="order-row" style="display:none;">
                                            <div class="left-fixed">
                                                <p class="txt">Tỷ giá đại lý</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="number" ID="rAgentCurrency"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="order-row" style="display:none">
                                            <div class="left-fixed">
                                                <p class="txt">Giá tiền 1 lần xuất kho (VCH)</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="number" ID="rPriceCheckOutWareDefault"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Số lượng link trong 1 đơn</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="number" ID="txtNumberLinkOfOrder"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card-panel">
                            <div id="noti-sec" class="section scrollspy fee-order">
                                <div class="title-header bg-dark-gradient">
                                    <h6 class="white-text ">Cấu hình thông báo</h6>
                                </div>
                                <div class="child-fee">
                                    <div class="content-panel">
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Tiêu đề thông báo Popup</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtTitleNotificationPopup"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Email liên hệ Popup </p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtTitleNotificattion"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Nội dung thông báo Popup</p>
                                            </div>
                                            <div class="right-content" style="width: min-content;">
                                                <div class="tiny-editor"></div>
                                            </div>
                                            <asp:HiddenField runat="server" ID="hdfContentNotificationPopup" />

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="card-panel" style="display:none;">
                            <div id="footer-sec" class="section scrollspy fee-order">
                                <div class="title-header bg-dark-gradient">
                                    <h6 class="white-text ">Cấu hình footer</h6>
                                </div>
                                <div class="child-fee">
                                    <div class="content-panel">
                                        <div class="tiny-editor"></div>
                                    </div>
                                </div>
                                <asp:HiddenField runat="server" ID="hdfFooterConfig" />
                            </div>
                        </div>

                        <div class="card-panel" style="display:none;">
                            <div id="seo-sec" class="section scrollspy fee-order">
                                <div class="title-header bg-dark-gradient">
                                    <h6 class="white-text ">Cấu hình SEO trang chủ và mặc định</h6>
                                </div>
                                <div class="child-fee">
                                    <div class="content-panel">
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Title</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtTitle"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Meta keyword</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtMetaKeyWord"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Meta description</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtMetaDescription"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">OG Title</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtOGTitle"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">OG Description</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtOGDescription"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">OG Image</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <div style="display: inline-block; margin-left: 15px; padding-bottom: 5px">
                                                            <asp:FileUpload ID="OGImage" runat="server" class="upload-img" type="file" onchange="previewFiles(this);" title="" />
                                                            <button class="btn-upload" type="button">Upload</button>
                                                        </div>
                                                        <div class="preview-img-default">
                                                            <asp:Image ID="OGImageBefore" Style="max-height: 100px; max-width: 100px;" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="order-row" style="display: none">
                                            <div class="left-fixed">
                                                <p class="txt">OG Facebook Title</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtOGFBTitle"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-row" style="display: none">
                                            <div class="left-fixed">
                                                <p class="txt">OG Facebook Description</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtOGFBDescription"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-row" style="display: none">
                                            <div class="left-fixed">
                                                <p class="txt">OG Facebook Image</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <div style="display: inline-block; margin-left: 15px; padding-bottom: 5px">
                                                            <asp:FileUpload ID="OGFbImage" runat="server" class="upload-img" type="file" onchange="previewFiles(this);" title="" />
                                                            <button class="btn-upload" type="button">Upload</button>
                                                        </div>
                                                        <div class="preview-img-default">
                                                            <asp:Image ID="OGFbImageBefore" Style="max-height: 100px; max-width: 100px;" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">OG Twitter Title</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtTwitterTitle"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">OG Twitter Description</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" placeholder="" type="text" ID="txtTwitterDescription"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">OG Twitter Image</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <div style="display: inline-block; margin-left: 15px; padding-bottom: 5px">
                                                            <asp:FileUpload ID="OGTwitterImage" runat="server" class="upload-img" type="file" onchange="previewFiles(this);" title="" />
                                                            <button class="btn-upload" type="button">Upload</button>
                                                        </div>
                                                        <div class="preview-img-default">
                                                            <asp:Image ID="OGTwitterImageBefore" Style="max-height: 100px; max-width: 100px;" runat="server" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Google Analytics (Đặt nội dung trong thẻ script)</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" ID="txtGoogleAna" TextMode="MultiLine"
                                                            Width="100%" Height="200px"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">WebMaster Tools  (Đặt nội dung trong thẻ script)</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" ID="txtWebMaster" TextMode="MultiLine"
                                                            Width="100%" Height="200px"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Header Script Code  (Đặt nội dung trong thẻ script)</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" ID="txtHeaderScript" TextMode="MultiLine"
                                                            Width="100%" Height="200px"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="order-row">
                                            <div class="left-fixed">
                                                <p class="txt">Footer Script  (Đặt nội dung trong thẻ script)</p>
                                            </div>
                                            <div class="right-content">
                                                <div class="row">
                                                    <div class="input-field col s12">
                                                        <asp:TextBox runat="server" ID="txtFooterScript" TextMode="MultiLine"
                                                            Width="100%" Height="200px"></asp:TextBox>
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
    </div>

    <!-- END: Page Main-->
    <script src="/App_Themes/AdminNew45/assets/vendors/tinymce/tinymce.min.js"></script>
    <script>
        function myNewFunction() {
            var content1 = tinymce.editors[0].contentDocument.activeElement.innerHTML;
            $('#<%=hdfAddress1.ClientID%>').val(content1);


            var content2 = tinymce.editors[1].contentDocument.activeElement.innerHTML;
            $('#<%=hdfAddress2.ClientID%>').val(content2);


            var content3 = tinymce.editors[2].contentDocument.activeElement.innerHTML;
            $('#<%=hdfAbout.ClientID%>').val(content3);


            var content4 = tinymce.editors[3].contentDocument.activeElement.innerHTML;
            $('#<%=hdfContentNotificationPopup.ClientID%>').val(content4);


            var content5 = tinymce.editors[4].contentDocument.activeElement.innerHTML;


            var content5 = tinymce.editors[4].contentDocument.activeElement.innerHTML;

            var content6 = tinymce.editors[4].contentDocument.activeElement.innerText;
            //alert(content6);

            $('#<%=hdfFooterConfig.ClientID%>').val(content5);
          <%--  var message5 = $('#<%=hdfFooterConfig.ClientID%>').val(); --%>         
            $('#<%=btnUpdate.ClientID%>').click();

        }
        function myFunction() {
            var message1 = $('#<%=hdfAddress1.ClientID%>').val();
            tinymce.editors[0].contentDocument.activeElement.innerHTML = message1;

            var message2 = $('#<%=hdfAddress2.ClientID%>').val();
            tinymce.editors[1].contentDocument.activeElement.innerHTML = message2;

            var message3 = $('#<%=hdfAbout.ClientID%>').val();
            tinymce.editors[2].contentDocument.activeElement.innerHTML = message3;

            var message4 = $('#<%=hdfContentNotificationPopup.ClientID%>').val();
            tinymce.editors[3].contentDocument.activeElement.innerHTML = message4;

            var message5 = $('#<%=hdfFooterConfig.ClientID%>').val();
            tinymce.editors[4].contentDocument.activeElement.innerHTML = message5;

        }
        $(window).load(function () {
            myFunction();
        });
        function uploadImage() {
            var editor = tinymce.activeEditor;
            // create input element, call modal dialog w
            var fileInput = document.createElement('input');
            fileInput.setAttribute('type', 'file');
            fileInput.setAttribute('accept', 'image/png, image/gif, image/jpeg, image/bmp, image/x-icon');
            // if file is submitted run our key code
            fileInput.addEventListener('change', () => {
                if (fileInput.files != null && fileInput.files[0] != null) {
                    // create instance of FileReader()
                    var formData = new FormData();
                    formData.append("FileUpload", fileInput.files[0]);
                    $.ajax({
                        async: false,
                        type: 'POST',
                        url: '/HandlerCS.ashx',
                        data: formData,
                        dataType: 'json',
                        contentType: false,
                        processData: false,
                        success: function (file) {
                            var url = "";
                            var real = "";
                            if (file.length > 0) {
                                file.forEach(function (data, index) {
                                    url += data.name;
                                    real += data.realname;
                                });
                                console.log(url);
                                console.log(real);
                                tinymce.activeEditor.insertContent('<img src="' + url + '"/>');
                            }
                        },
                        error: function (error) {
                            console.log('error upload file audio');
                        }
                    });

                }
            });
            fileInput.click();
        }
        $(document).ready(function () {
            tinymce.init({
                selector: '.tiny-editor',
                plugin: "autosave",
                menubar: false,
                oninit: "setPlainText",
                plugins: "paste imagetools code",
                paste_as_text: true,
                inline: false,
                menubar: 'file edit insert view format table tools help imagetools',
                toolbar: 'formatselect | bold italic strikethrough forecolor backcolor | link | alignleft aligncenter alignright alignjustify  | numlist bullist outdent indent  | table UploadImage link media | removeformat',
                content_css: [
                    '//fonts.googleapis.com/css?family=Lato:300,300i,400,400i'
                ],
                height: 450,
                images_dataimg_filter: function (img) {
                    return img.hasAttribute('internal-blob');
                },
                setup: (editor) => {
                    editor.ui.registry.addButton('UploadImage', {
                        text: 'Image',
                        icon: 'image',
                        onAction: uploadImage
                    });
                }
            });
            $('#preview-btn').on('click', function () {
                tinyMCE.activeEditor.execCommand('mcePreview');
            });

            $(window).scroll(function () {
                var id = $('.table-of-contents li a.active').attr('href');
                $('.scrollspy').each(function () {
                    var itemId = $(this).attr('id');
                    if (('#' + itemId) == id) {
                        $(this).parent().css({
                            'box-shadow': '0 8px 17px 2px rgba(0, 0, 0, 0.14), 0 3px 14px 2px rgba(0, 0, 0, 0.12), 0 5px 5px -3px rgba(0, 0, 0, 0.2)',
                            'border': '1px solid #000'
                        });
                        $('.scrollspy').not(this).parent().css({
                            'box-shadow': 'rgba(0, 0, 0, 0.14) 0px 2px 2px 0px, rgba(0, 0, 0, 0.12) 0px 3px 1px -2px, rgba(0, 0, 0, 0.2) 0px 1px 5px 0px',
                            'border': '0'
                        });
                    }

                });

            });
        });
    </script>
</asp:Content>
