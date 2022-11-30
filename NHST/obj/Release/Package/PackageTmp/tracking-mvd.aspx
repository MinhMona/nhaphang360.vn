<%@ Page Language="C#" MasterPageFile="~/UserMasterNew.Master" AutoEventWireup="true" CodeBehind="tracking-mvd.aspx.cs" Inherits="NHST.tracking_mvd1" %>

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
                                        <h4>Tracking mã vận đơn</h4>

                                    </div>
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col s12">
                                    <div class="tracking-input">
                                        <div class="search-name input-field">
                                            <asp:TextBox runat="server" ID="tSearchName" CssClass="validate autocomplete txtsearchfield" placeholder="Nhập mã vận đơn"></asp:TextBox>
                                            <a class="material-icons search-action" onclick="Tracking()">search</a>
                                        </div>
                                    </div>
                                </div>
                                <div class="col s12">
                                    <asp:Literal runat="server" ID="ltrTracking"></asp:Literal>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Button runat="server" ID="btnSearch" Text="Tìm" CssClass="btn primary-btn" Style="display: none"
        OnClick="btnSearch_Click" UseSubmitBehavior="false" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('.txtsearchfield').on("keypress", function (e) {
                if (e.keyCode == 13) {
                    Tracking();
                }
            });
        });

        function Tracking() {
            $("#<%=btnSearch.ClientID%>").click();
        }
    </script>
</asp:Content>
