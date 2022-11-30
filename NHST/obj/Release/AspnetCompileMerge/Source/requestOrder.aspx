<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="requestOrder.aspx.cs" Inherits="NHST.requestOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
       
        $(document).ready(function () {
            calladd();
        });
        function calladd()
        {
            var data_nhst = {
                'title_origin': '泰国潮牌性感交叉肩带大露背吊带仿真丝缎面背心上衣连衣裙女夏季',
                'title_translated': '泰国潮牌性感交叉肩带大露背吊带仿真丝缎面背心上衣连衣裙女夏季',
                'price_origin': '68',
                'price_promotion': '39',
                'property_translated': '均码;白色;',
                'property': '均码;白色;',
                'data_value': '20509:28383;1627207:28320;',
                'image_model': 'https%3A%2F%2Fgd1.alicdn.com%2Fbao%2Fuploaded%2Fi1%2F653659480%2FTB2vRf2iVXXXXX0XXXXXXXXXXXX_!!653659480.jpg_150x150.jpg',
                'image_origin': 'https%3A%2F%2Fgd1.alicdn.com%2Fbao%2Fuploaded%2Fi1%2F653659480%2FTB2vRf2iVXXXXX0XXXXXXXXXXXX_!!653659480.jpg_150x150.jpg',
                'shop_id': 'taobao_69287004',
                'shop_name': 'WONDER EYES独家订制女装',
                'seller_id': '653659480',
                'wangwang': 'iinhere',
                'quantity': '1',
                'stock': '265',
                'location_sale': '广东广州',
                'site': 'TAOBAO',
                'comment': '',
                'item_id': '528694290468',
                'link_origin': 'https://world.taobao.com/item/528694290468.htm?spm=a21bp.7806943.topsale_XX.5.R2WgMy',
                'outer_id': '',
                'error': '0',
                'weight': '0',
                'step': '1',
                'brand': 'test',
                'category_name': 'VíVí',
                'category_id': '13',
                'tool': 'Addon',
                'version': '4.11.88',
                'is_translate': 'false'
            };
            
            alert(JSON.stringify(data_nhst));
            $.ajax({
                url: "/WebService1.asmx/receiverequest",
                data: data_nhst,
                method: 'POST',
                contentType: 'application/x-www-form-urlencoded',
                xhrFields: {
                    withCredentials: true
                },
                headers: { 'X-Requested-With': 'XMLHttpRequest' },
                success: function (d) {
                    alert((new XMLSerializer()).serializeToString(d));
                    chrome.tabs.sendMessage(sender.tab.id, { action: request.callback, response: d }, function (response) {

                    });
                },
                error: function (event, jqXHR, ajaxSettings, thrownError) {
                    alert('[event:' + event + '], [jqXHR:' + jqXHR + '], [ajaxSettings:' + ajaxSettings + '], [thrownError:' + thrownError + '])');
                }
            });
        }
        function jsonCallback(json) {
            alert(json.volumeInfo.title);
            //$(".test").html(json.volumeInfo.title);
        }
    </script>
</asp:Content>
