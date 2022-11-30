<%@ Page Title="" Language="C#" MasterPageFile="~/manager/adminMasterNew.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="Edit-Paget-Type.aspx.cs" Inherits="NHST.manager.Edit_Paget_Type" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="main" class="main-full">
        <div class="row">
            <div class="content-wrapper-before bg-dark-gradient"></div>
            <div class="page-title">
                <div class="card-panel">
                    <h4 class="title no-margin" style="display: inline-block;">Chỉnh sửa chuyên mục</h4>
                    <div class="clearfix"></div>
                </div>
            </div>
            <div class="new-post col s12 m12 l8 xl8 section">
                <div class="list-table card-panel">
                    <div>
                        <div class="row">
                            <div class="input-field col s12">
                                <asp:TextBox runat="server" id="txtName" type="text" class="validate" required value="Bảng giá"></asp:TextBox>
                                <label for="post_title"><span class="red-text">*</span> Tên chuyên mục: </label>
                                <span class="helper-text" data-error="Vui lòng nhập tên danh mục"></span>
                            </div>
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
                        <asp:HiddenField runat="server" ID="hdfDeltail"/>
                        <h5 class="center-align pt-2">Hỗ trợ SEO</h5>
                        <div class="row mt-3">                           
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" id="txtOGTitle" type="text" class="validate" data-type="text-only"></asp:TextBox>
                                <label for="txtOGTitle">OG Title</label>
                            </div>
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" id="txtOGDescription" type="text" class="validate" data-type="text-only"></asp:TextBox>
                                <label for="txtOGDescription">OG Description</label>
                            </div>
                            <div class="input-field file-field col s12 m12">
                                <span class="black-text">OG Image</span>
                                <div style="display: inline-block; margin-left: 15px;">
                                    <asp:FileUpload runat="server" ID="OGImage" class="upload-img" type="file" onchange="previewFiles(this);" title=""></asp:FileUpload>
                                    <button class="btn-upload">Upload</button>                                  
                                </div>
                                <div class="preview-img-default" >
                                      <asp:Image ID="OGImageBefore" style="max-height: 100px; max-width: 100px;" runat="server" />
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
                                     <asp:Image ID="OGFacebookIMGBefore" style="max-height: 100px; max-width: 100px;" runat="server" />
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
                                    <asp:Image ID="OGTwitterIMGBefore" style="max-height: 100px; max-width: 100px;" runat="server" />
                                </div>
                            </div>
                            <div class="input-field col s12 m6">
                                <asp:TextBox runat="server" id="txtMetaTitle" type="text" class="validate" data-type="text-only"></asp:TextBox>
                                <label for="add_meta-title">Meta Title</label>
                            </div>
                            <div class="input-field col s12 m12">
                                <asp:TextBox runat="server" id="txtMetaDescription" type="text" class="validate" data-type="text-only"></asp:TextBox>
                                <label for="add_meta-description">Meta Description</label>
                            </div>
                            <div class="input-field col s12 m12">
                                <asp:TextBox runat="server" placeholder="Cách nhau bởi dấu phẩy VD : muahang,trungquoc,vietnam"
                                    id="txtMetaKeyWord" type="text" class="validate" data-type="text-only"></asp:TextBox>
                                <label for="add_meta-keyword">Meta Keyword</label>
                            </div>

                        </div>
                        <div class="row mt-3">
                            <div class="col s12">
                                <a id="test" class="btn white-text"  onclick="myFunction()">Cập nhật</a>
                                <asp:Button style="display:none" runat="server" ID="btnUpdate" class="btn  white-text" Text="Cập nhật" OnClick="btnSave_Click"></asp:Button>
                              <%--  <a href="javascript:;" class="btn waves-effect" id="preview-btn">Xem trước</a>--%>
                              
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </div>
    <!-- END: Page Main-->
    <script src="/App_Themes/AdminNew45/assets/vendors/tinymce/tinymce.min.js" type="text/javascript"></script>
    <script>

        function myFunction() {
            var content = tinymce.editors[0].contentDocument.activeElement.innerHTML;
            $('#<%=hdfDeltail.ClientID%>').val(content);
            $('#<%=btnUpdate.ClientID%>').click();

        }
        function loadata() {
            var message = $('#<%=hdfDeltail.ClientID%>').val();
            tinymce.editors[0].contentDocument.activeElement.innerHTML = message;

            var a = $('#<%=hdfSideBar.ClientID%>').val();
            if (a == 0) {
                $('#<%=txtSideBar.ClientID%>').prop('checked', false);

            }
            else {
                $('#<%=txtSideBar.ClientID%>').prop('checked', true);

            }
        }
        $(window).load(function () {
            loadata();
        });
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