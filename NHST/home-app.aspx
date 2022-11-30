<%@ Page Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="home-app.aspx.cs" Inherits="NHST.home_app" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <main id="main-wrap">
        <div class="page-wrap">
            <asp:Panel ID="pnMobile" runat="server" Visible="false">
                <div class="page-body">
                    <div class="all">
                        <asp:Literal runat="server" ID="ltrhome"></asp:Literal>
                    </div>
                </div>
                <div class="page-bottom-toolbar">
                </div>
            </asp:Panel>
            <asp:Panel ID="pnShowNoti" runat="server" Visible="false">
                <div class="page-body">
                    <div class="heading-search">
                        <h4 class="page-title">Bạn vui lòng đăng xuất và đăng nhập lại!</h4>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </main>
    <div class="fixed-nav">
        <asp:Literal runat="server" ID="ltrZalo1"></asp:Literal>
    </div>
    <style>
        .group-buy > li > a {
            display: block;
            position: relative;
            padding: 0px;
            overflow: hidden;
        }

        .group-services > li > a {
            display: block;
            position: relative;
            padding: 0px;
            overflow: hidden;
        }

        .float_right a {
            background-color: #f37421;
        }
        .img-zalo{
            width: 40px;
        }
        .fixed-nav{
            display: flex;
            justify-content: space-around;
            background: white;
            padding: 5px 0;
            position: fixed;
            bottom: 0;
            width: 100%;            
        }
    </style>
    <script>
        jQuery(document).ready(function () {

            $('#xoahetsanpham-link').on('click', function (e) {
                e.preventDefault();
                $('#listsp_cont').find('.removeable-item').remove();
            });
            $('#themsanpham-link').on('click', function (e) {
                e.preventDefault();
                $('#listsp_cont').append(`<div class="page-feature sanpham-item removeable-item"><div class="frow"><p class="lb">Ảnh sản phẩm</p><label class="lb-uploadfile"><input type="file"><p class="ip-avt"><i class="fa fa-picture-o"></i> Choose File</p></label></div><div class="frow"><p class="lb">Link</p><input type="text" class="fcontrol"></div><div class="frow"><p class="lb">Tên</p><input type="text" class="fcontrol"></div><div class="frow flexrow"><div class="flex-item"><p class="lb">Màu sắc</p><select class="fcontrol"><option>Hồng</option></select></div><div class="flex-item"><p class="lb">Số lượng</p><select class="fcontrol"><option>01</option></select></div></div><div class="frow"><p class="lb">Yêu cầu thêm</p><textarea class="fcontrol" style="height: 65px"></textarea></div><div class="frow"><p class="right-txt"><a href="javascript:;" class="btn do-btn w170-btn remove-btn">Xoá sản phẩm</a></p></div></div>`);

            });

            $('#listsp_cont').on('click', '.remove-btn', function (e) {
                e.preventDefault();
                $(this).closest('.removeable-item').remove();;
            });


        });
    </script>
</asp:Content>
