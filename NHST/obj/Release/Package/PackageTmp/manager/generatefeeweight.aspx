<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMaster.Master" AutoEventWireup="true" CodeBehind="generatefeeweight.aspx.cs" Inherits="NHST.manager.generatefeeweight" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel runat="server" ID="Parent">
        <main id="main-wrap">
            <div class="grid-row">
                <div class="grid-col" id="main-col-wrap">
                    <div class="feat-row grid-row">
                        <div class="grid-col-50 grid-row-center">
                            <article class="pane-primary">
                                <div class="heading">
                                    <h3 class="lb">Generate phí vận chuyển</h3>
                                </div>
                                <div class="cont">
                                    <div class="inner">
                                        <div class="form-row marbot2">
                                            <asp:Literal ID="ltrList" runat="server"></asp:Literal>
                                        </div>
                                        <div class="form-row marbot1">
                                            Vận chuyển hộ
                                        </div>
                                        <div class="form-row marbot2">
                                            <asp:CheckBox runat="server" ID="isHidden" Checked="false" />
                                        </div>
                                        <div class="form-row no-margin center-txt">
                                            <asp:Button runat="server" ID="btnSave" Text="Generate" CssClass="btn primary-btn" 
                                                ValidationGroup="n" OnClick="btnSave_Click" />
                                        </div>
                                    </div>
                                </div>
                            </article>
                        </div>
                    </div>
                </div>
            </div>
        </main>
    </asp:Panel>
</asp:Content>
