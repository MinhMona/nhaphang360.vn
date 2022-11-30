<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UserMasterNew.Master" CodeBehind="ConvertLink.aspx.cs" Inherits="NHST.ConvertLink" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content runat="server" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="main">
        <div class="row">
            <div class="content-wrapper-before blue-grey lighten-5"></div>
            <div class="col s12">
                <div class="container">
                    <div class="all">
                        <div class="card-panel mt-3 no-shadow">
                            <div class="row">
                                <div class="col s12">
                                    <div class="page-title mt-2 center-align">
                                        <h4>Chuyển link app sang link web</h4>
                                    </div>
                                </div>

                                <div class="col s12 mt-2">
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
                        </div>
                    </div>
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
