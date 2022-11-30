<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="AddPage.aspx.cs" Inherits="NHST.manager.AddPage" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Chỉnh sửa bài viết</h4>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="new-post col s12 m12 l8 xl8 section">
                <div class="list-table card-panel">
                    <div>
                        <div class="row">
                            <div class="input-field col s12">
                                <asp:TextBox runat="server" ID="txtTitle" placeholder="" type="text" class="validate" required></asp:TextBox>
                                <label for="txtTitle"><span class="red-text">*</span> Tiêu đề: </label>
                                <span class="helper-text" data-error="Vui lòng nhập tiêu đề bài viết"></span>
                            </div>
                            <div class="input-field col s12">
                                <asp:ListBox runat="server" name="" ID="ddlPageType"></asp:ListBox>
                                <label>Chuyên mục:</label>
                            </div>
                            <div class="input-field col s12">
                                <asp:TextBox runat="server" ID="txtShortDescription" placeholder="" type="text" TextMode="MultiLine" Width="100%" Height="150px" class="validate" data-type="text-only"></asp:TextBox>
                                <label for="add_og-description">Mô tả ngắn</label>
                            </div>
                            <div class="input-field col s12">
                                <span class="black-text">Ảnh đại diện</span>
                                <div style="display: inline-block; margin-left: 15px;">
                                    <asp:FileUpload ID="UpIMG" runat="server" class="upload-img" type="file" onchange="previewFiles(this);" title="" />
                                    <button class="btn-upload" type="button">Upload</button>

                                </div>
                                <div class="preview-img-avatar">                                  

                                </div>
                            </div>
                            <div class="input-field col s12 m6">
                                <div class="switch status-func">
                                    <span class="mr-2">Trạng thái: </span>
                                    <label>
                                        Ẩn
                                          <asp:TextBox runat="server" onclick="CheckStatus()" ID="txtStatus" type="checkbox"></asp:TextBox>
                                        <span class="lever"></span>
                                    </label>
                                </div>
                            </div>
                            <asp:HiddenField runat="server" ID="hdfStatus" Value="0" />
                            <div class="input-field col s12 m6" style="display:none">
                                <div class="switch status-func">
                                    <span class="mr-2">SideBar: </span>
                                    <label>
                                        Ẩn
                                          <asp:TextBox runat="server" onclick="CheckSideBar()" ID="txtSideBar" type="checkbox"></asp:TextBox>
                                        <span class="lever"></span>
                                    </label>
                                </div>
                            </div>
                            <asp:HiddenField runat="server" ID="hdfSideBar" Value="0" />
                           
                        </div>
                        <textarea class="content-editor" id="editor"></textarea>
                        <asp:HiddenField runat="server" ID="hdfDetail" />
                        <h5 class="center-align pt-2">Hỗ trợ SEO</h5>
                        <div class="row mt-3">
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" placeholder=""  ID="txtOGTitle" type="text" class="validate" data-type="text-only"></asp:TextBox>
                                <label for="add_og-title">OG Title</label>
                            </div>
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" ID="txtOGDescription" placeholder=""  type="text" class="validate" data-type="text-only"></asp:TextBox>
                                <label for="add_og-description">OG Description</label>
                            </div>
                            <div class="input-field file-field col s12 m12">
                                <span class="black-text">OG Image</span>
                                <div style="display: inline-block; margin-left: 15px;">
                                    <asp:FileUpload ID="OGIMG" runat="server" class="upload-img" type="file" onchange="previewFiles(this);" title="" />
                                    <button class="btn-upload" type="button">Upload</button>
                                </div>
                                <div class="preview-img-default">
                                  
                                </div>
                            </div>
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" ID="OGFacebookTitle" placeholder=""  type="text" class="validate" data-type="text-only"></asp:TextBox>
                                <label for="add_og-title">OG Facebook Title</label>
                            </div>
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" ID="OGFacebookDescription" placeholder=""  type="text" class="validate" data-type="text-only"></asp:TextBox>
                                <label for="add_og-description">OG Facebook Description</label>
                            </div>
                            <div class="input-field file-field col s12 m12">
                                <span class="black-text">OG Facebook Image</span>
                                <div style="display: inline-block; margin-left: 15px;">
                                    <asp:FileUpload ID="OGFacebookIMG" runat="server" class="upload-img" type="file" onchange="previewFiles(this);" title="" />
                                    <button class="btn-upload" type="button">Upload</button>
                                </div>
                                <div class="preview-img-facebook">
                                    
                                </div>
                            </div>
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" ID="OGTwitterTitle" placeholder=""  type="text" class="validate" data-type="text-only"></asp:TextBox>
                                <label for="add_og-title">OG Twitter Title</label>
                            </div>
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" ID="OGTwitterDescription" placeholder=""  type="text" class="validate" data-type="text-only"></asp:TextBox>
                                <label for="add_og-description">OG Twitter Description</label>
                            </div>
                            <div class="input-field file-field col s12 m12">
                                <span class="black-text">OG Twitter Image</span>
                                <div style="display: inline-block; margin-left: 15px;">
                                    <asp:FileUpload ID="OGTwitterIMG" runat="server" class="upload-img" type="file" onchange="previewFiles(this);" title="" />
                                    <button class="btn-upload" type="button">Upload</button>
                                </div>
                                <div class="preview-img-twitter">
                                  
                                </div>
                            </div>
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" ID="txtMetaTitle"  placeholder="" type="text" class="validate" data-type="text-only"></asp:TextBox>
                                <label for="add_meta-title">Meta Title</label>
                            </div>
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" ID="txtMetaDescription" placeholder=""  type="text" class="validate" data-type="text-only"></asp:TextBox>
                                <label for="add_meta-description">Meta Description</label>
                            </div>
                            <div class="input-field col s12 m12">
                                <asp:TextBox runat="server" placeholder="Cách nhau bởi dấu phẩy VD : muahang,trungquoc,vietnam"
                                    ID="txtMetaKeyWord" type="text" class="validate" data-type="text-only"></asp:TextBox>
                                <label for="add_meta-keyword">Meta Keyword</label>
                            </div>

                        </div>
                        <div class="row mt-3">
                            <div class="col s12">
                                <a onclick="updateFunction()" class="btn waves-effect white-text mr-2">Tạo</a>          
                            </div>
                        </div>
                </div>
            </div>
        </div>
    </div>
     </div>
    <asp:Button runat="server" ID="btnUpdate" Style="display: none" OnClick="btnSave_Click" />
    <!-- END: Page Main-->
    <script src="/App_Themes/AdminNew45/assets/vendors/tinymce/tinymce.min.js" type="text/javascript"></script>
    <script>
        function updateFunction() {
            var content = tinymce.editors[0].contentDocument.activeElement.innerHTML;
            $('#<%=hdfDetail.ClientID%>').val(content);
            $('#<%=btnUpdate.ClientID%>').click();
        }
        function CheckStatus() {
            var a = $('#<%=hdfStatus.ClientID%>').val();
            if (a == '0') {

                $('#<%=hdfStatus.ClientID%>').val('1');
            }
            else {

                $('#<%=hdfStatus.ClientID%>').val('0');
            }
            console.log($('#<%=hdfStatus.ClientID%>').val());
        }
        function CheckSideBar() {
            var a = $('#<%=hdfSideBar.ClientID%>').val();
            if (a == '0') {
                $('#<%=hdfSideBar.ClientID%>').val('1');
            }
            else {

                $('#<%=hdfSideBar.ClientID%>').val('0');
            }
            console.log($('#<%=hdfSideBar.ClientID%>').val());
        }
        function myFunction() {
            var message = $('#<%=hdfDetail.ClientID%>').val();
            tinymce.editors[0].contentDocument.activeElement.innerHTML = message;
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
                selector: '.content-editor',
                plugin: "autosave",
                menubar: false,
                oninit: "setPlainText",
                plugins: "paste imagetools media link table",
                paste_as_text: true,
                inline: false,
                relative_urls: false,
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
        });
    </script>

</asp:Content>