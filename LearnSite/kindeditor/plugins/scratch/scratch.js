/*******************************************************************************
* KindEditor - WYSIWYG HTML Editor for Internet
* Copyright (C) 2006-2011 kindsoft.net
*
* @author Roddy <luolonghao@gmail.com>
* @site http://www.kindsoft.net/
* @licence http://www.kindsoft.net/license.php
*******************************************************************************/

KindEditor.plugin('scratch', function (K) {
    var self = this, name = 'scratch', lang = self.lang(name + '.'),
		allowFlashUpload = K.undef(self.allowFlashUpload, true),
		allowFileManager = K.undef(self.allowFileManager, false),
		formatUploadUrl = K.undef(self.formatUploadUrl, true),
		extraParams = K.undef(self.extraFileUploadParams, {}),
		uploadJson = K.undef(self.uploadJson, self.basePath + 'php/upload_json.php');
    self.plugin.flv = {
        edit: function () {
            var html = [
				'<div style="padding:20px;">',
            //url
				'<div class="ke-dialog-row">',
				'<label for="keUrl" style="width:60px;">' + lang.url + '</label>',
				'<input class="ke-input-text" type="text" id="keUrl" name="url" value="" style="width:160px;" /> &nbsp;',
				'<input type="button" class="ke-upload-button" value="' + lang.upload + '" /> &nbsp;',
				'<span class="ke-button-common ke-button-outer">',
				'<input type="button" class="ke-button-common ke-button" name="viewServer" value="' + lang.viewServer + '" />',
				'</span>',
				'</div>',
            //width
				'<div class="ke-dialog-row">',
				'<label for="keWidth" style="width:60px;">' + lang.width + '</label>',
				'<input type="text" id="keWidth" class="ke-input-text ke-input-number" name="width" value="482" maxlength="4" />',
				'</div>',
            //height
				'<div class="ke-dialog-row">',
				'<label for="keHeight" style="width:60px;">' + lang.height + '</label>',
				'<input type="text" id="keHeight" class="ke-input-text ke-input-number" name="height" value="400" maxlength="4" />',
				'</div>',
				'</div>'
			].join('');
            var dialog = self.createDialog({
                name: name,
                width: 450,
                height: 230,
                title: self.lang(name),
                body: html,
                yesBtn: {
                    name: self.lang('yes'),
                    click: function (e) {
                        var url = K.trim(urlBox.val()),
							width = widthBox.val(),
							height = heightBox.val();
                        if (url == 'http://' || K.invalidUrl(url)) {
                            alert(self.lang('invalidUrl'));
                            urlBox[0].focus();
                            return;
                        }
                        if (!/^\d*$/.test(width)) {
                            alert(self.lang('invalidWidth'));
                            widthBox[0].focus();
                            return;
                        }
                        if (!/^\d*$/.test(height)) {
                            alert(self.lang('invalidHeight'));
                            heightBox[0].focus();
                            return;
                        }
                        var tody = new Date();
                        var haomiao = tody.getMilliseconds();
                        var scratchplayer = "PlayerOnly" + haomiao;
                        var srcjs = "../Plugins/scratch/swfobject.js";
                        var html = [
						'<br/><div style="text-align: center">\r\n',
                        '<div id="' + scratchplayer + '" style="margin: auto;background:#666666;width:482px;height:400px">Scratch<br/>\r\n',
                        '<script type="text/javascript" src="' + srcjs + '"></script>\r\n',
                        '<script type="text/javascript">\r\n',
                        'var fwidth = 482;\r\n',
                        ' var fheight = 400;\r\n ',
                         'installPlayer("../Plugins/scratch/Scratch.swf", "' + scratchplayer + '");\r\n',
                        'function installPlayer(swfName, swfID) {\r\n',
                         'var flashvars = {\r\n',
                         'project: "' + url + '?version=3"};\r\n',
                         ' var params = {\r\n',
                        'allowScriptAccess:"always",\r\n',
                        'allowFullScreen: true\r\n',
                        '};\r\n',
                        'var attributes = {};\r\n',
                         'swfobject.embedSWF(swfName, swfID, fwidth, fheight, "10.0", false, flashvars, params, attributes);\r\n',
                        '}\r\n',
                        '</script>\r\n',
                        '</div></div><br/><br/>\r\n'
						].join('');
                        //alert(html);
                        self.insertHtml(html).hideDialog().focus();
                    }
                }
            }),
			div = dialog.div,
			urlBox = K('[name="url"]', div),
			viewServerBtn = K('[name="viewServer"]', div),
			widthBox = K('[name="width"]', div),
			heightBox = K('[name="height"]', div),
			autostartBox = K('[name="autostart"]', div);
            urlBox.val('http://');

            if (allowFlashUpload) {
                var uploadbutton = K.uploadbutton({
                    button: K('.ke-upload-button', div)[0],
                    fieldName: 'imgFile',
                    extraParams: extraParams,
                    url: K.addParam(uploadJson, 'dir=scratch'),
                    afterUpload: function (data) {
                        dialog.hideLoading();
                        if (data.error === 0) {
                            var url = data.url;
                            if (formatUploadUrl) {
                                url = K.formatUrl(url, 'absolute');
                            }
                            urlBox.val(url);
                            if (self.afterUpload) {
                                self.afterUpload.call(self, url);
                            }
                            alert(self.lang('uploadSuccess'));
                        } else {
                            alert(data.message);
                        }
                    },
                    afterError: function (html) {
                        dialog.hideLoading();
                        self.errorDialog(html);
                    }
                });
                uploadbutton.fileBox.change(function (e) {
                    dialog.showLoading(self.lang('uploadLoading'));
                    uploadbutton.submit();
                });
            } else {
                K('.ke-upload-button', div).hide();
            }

            if (allowFileManager) {
                viewServerBtn.click(function (e) {
                    self.loadPlugin('filemanager', function () {
                        self.plugin.filemanagerDialog({
                            viewType: 'LIST',
                            dirName: 'scratch',
                            clickFn: function (url, title) {
                                if (self.dialogs.length > 1) {
                                    K('[name="url"]', div).val(url);
                                    self.hideDialog();
                                }
                            }
                        });
                    });
                });
            } else {
                viewServerBtn.hide();
            }

        },
        'delete': function () {
            self.plugin.getSelectedFlv().remove();
        }
    };
    self.clickToolbar(name, self.plugin.flv.edit);
});
