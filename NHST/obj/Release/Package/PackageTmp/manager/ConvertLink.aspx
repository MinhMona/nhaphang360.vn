<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConvertLink.aspx.cs" MasterPageFile="~/manager/adminMasterNew.Master" Inherits="NHST.manager.ConvertLink" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Chuyển đổi link từ app sang web</h4>
                </div>
            </div>
        </div>
        <div class="list-staff col s12 m12 l12 section">
            <div class="list-table card-panel">
                <div class="filter">
                    <div>
                        <asp:TextBox runat="server" type="text" ID="txtLink" placeholder="Nhập link sản phẩm" class="textarea"></asp:TextBox>
                        <span class="infor">Convert nhiều link, mỗi link cách nhau bởi dấu ","
                            <br />
                            VD: https://m.intl.taobao.com/detail/detail.html?spm=a1z5f.7632060.0.0&id=526430356503,https://m.intl.taobao.com/detail/detail.html?spm=a1z5f.7632060.0.0&id=526430356503 </span>
                    </div>

                    <div>
                        <a href="javascript:;" style="background-color: green; margin-top: 30px; margin-bottom: 30px" class="btn modal-trigger waves-effect" onclick="create()">THỰC HIỆN</a>
                    </div>

                    <span class="kq">
                        <asp:Literal runat="server" ID="ltrResult"></asp:Literal></span>
                </div>
            </div>
        </div>
    </div>   
    <asp:Button runat="server" ID="btnUpdate" Style="display: none" OnClick="btnSave_Click" />
    <script type="text/javascript">
        function create() {
                       
            $('#<%=btnUpdate.ClientID%>').click();
        }
    </script>
    <style>
        .filter .textarea {
            width: 100%;
            height: 200px;
            outline: 0 !important;
            padding-top: 10px;
            padding-left: 10px;
        }

        .kq {
            color: #0963ea;
            font-size: 16px;
        }

            .kq::before {
                content: 'Kết quả: ';
                color: black;
                font-size: 16px;
            }

        .infor {
            color: #F64302;
        }
    </style>
</asp:Content>
