 //Upload image preview
 function chatFileUploaded(e) {
    var preview = e.parentNode.previousElementSibling;     
    var files = e.files;
    var fileArray = Array.from(files);
    while(preview.firstChild){
        preview.removeChild(preview.firstChild);
    }
    if(e.files[0].size > 1048576){
            alert('Dung lượng file tối đa là 1MB'); 
            return false;
    }
    function readAndChat(file) {
        // Make sure `file.name` matches our extensions criteria        
        var reader = new FileReader();
        if (/\.(jpe?g|png|gif|bmp|svg|webp)$/i.test(file.name)) {          
            reader.addEventListener(
                "load",
                function() {
                    var image = new Image();
                    image.height = 50;
                    image.title = file.name;
                    image.src = this.result;                        
                    image.classList = "materialboxed";
                    var wrap = document.createElement("div");
                    wrap.classList = "img-block";
                    wrap.appendChild(image);
                    var close = document.createElement("span");
                    close.classList = "material-icons red-text delete";
                    close.innerHTML = "clear";
                    close.onclick = deleteFile;
                    wrap.appendChild(close);
                    preview.appendChild(wrap);
                },
                false
            );           
        }else if(/\.(docx|doc|txt|pdf|zip|rar|7zip|xlsx|xls)$/i.test(file.name)){
            reader.addEventListener(
            "load",
            function() {
                var itemWrap = document.createElement('div');
                itemWrap.setAttribute('class','img-block file-type');
                var image = new Image();
                image.height = 50;
                image.style.objectFit = "cover";
                image.title = file.name;
                image.src = 'assets/images/icon/file.png';
                image.classList = "img-file";
                var fileName = document.createElement("a");
                fileName.href="#";
                fileName.classList = "name-file";
                fileName.innerText = file.name;
                itemWrap.appendChild(image);
                itemWrap.appendChild(fileName);
                var close = document.createElement("a");
                close.classList = "material-icons red-text delete";
                close.innerHTML = "clear";
                close.onclick = deleteFile;
                itemWrap.appendChild(close);
                preview.appendChild(itemWrap);
            },
            false
            );           
        }else{
            alert('Định dạng file không hợp được hỗ trợ.\nVui lòng chọn lại định dạng file khác !');
            return false;
        }
        reader.readAsDataURL(file);

        function deleteFile(){
            fileArray.splice(fileArray.indexOf(file));
            this.parentNode.parentNode.removeChild(this.parentNode);
            if(fileArray.length === 0){
                console.log(e);
                e.value = "";
            }
        };
        
    }

    if (files) {
 //       preview.style.display = "block";
        var promise = new Promise(function(resolve, reject) {
        resolve([].forEach.call(files, readAndChat));
        });
    }
}