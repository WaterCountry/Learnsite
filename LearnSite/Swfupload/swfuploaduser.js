function uploadfun(furl,ftype,gurl) {
var swfu, twoswf;
swfu = new SWFUpload({
                    // Backend Settings
                    upload_url: furl,

                    // File Upload Settings
                    file_size_limit: "200 MB",
                    file_types: ftype,
                    file_types_description: "作品文件类型",
                    file_upload_limit: '1',
                    file_queue_limit: '1',
                    // Event Handler Settings
                    file_dialog_complete_handler: function (numFilesSelected, numFilesQueued) { if (numFilesQueued === 1) this.startUpload(); },
                    upload_success_handler: function (file, responseText) {
                        //alert("作品提交成功！作品名称为：" + file.name + "提交之后的作品名称为：" + responseText);
                        alert("作品提交成功！");
                        location.reload();
                    },

                    // Button settings
                    button_image_url: "../swfupload/image/100x22gray.png",
                    button_placeholder_id: "spanButtonPlaceholder",
                    button_width: 100,
                    button_height: 22,
                    button_text: '<span class="button">提交作品</span>',
                    button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt;text-align: center; width:100px;} ',
                    button_text_top_padding: 1,
                    button_text_left_padding: 5,

                    // Flash Settings
                    flash_url: "../swfupload/swfupload.swf", // Relative to this file

                    // Debug Settings
                    debug: false
                });
                twoswf = new SWFUpload({
                    // Backend Settings
                    upload_url: gurl,

                    // File Upload Settings
                    file_size_limit: "200 MB",
                    file_types: ftype,
                    file_types_description: "小组作品文件类型",

                    file_upload_limit: '1',
                    file_queue_limit: '1',

                    // Event Handler Settings
                    file_dialog_complete_handler: function (numFilesSelected, numFilesQueued) { if (numFilesQueued === 1) this.startUpload(); },
                    upload_success_handler: function (file, responseText) {
                        //alert("小组作品提交成功！作品名称为：" + file.name + "提交之后的作品名称为：" + responseText);
                        alert("小组作品提交成功！");
                        location.reload();
                    },

                    // Button settings
                    button_image_url: "../swfupload/image/100x22pink.png",
                    button_placeholder_id: "spanButtonPlaceholderTwo",
                    button_width: 100,
                    button_height: 22,
                    button_text: '<span class="button">小组合作</span>',
                    button_text_style: '.button { font-family: Helvetica, Arial, sans-serif; font-size: 14pt;text-align: center; width:100px;} ',
                    button_text_top_padding: 1,
                    button_text_left_padding: 5,

                    // Flash Settings
                    flash_url: "../swfupload/swfupload.swf", // Relative to this file

                    // Debug Settings
                    debug: false
});
}