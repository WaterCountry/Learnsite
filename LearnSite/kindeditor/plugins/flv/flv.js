/*******************************************************************************
* KindEditor - WYSIWYG HTML Editor for Internet
* Copyright (C) 2006-2011 kindsoft.net
*
* @author Roddy <luolonghao@gmail.com>
* @site http://www.kindsoft.net/
* @licence http://www.kindsoft.net/license.php
*******************************************************************************/

KindEditor.plugin('flv', function (K) {
    var self = this, name = 'flv', lang = self.lang(name + '.'),
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
            //密码观看
                '<div class="ke-dialog-row">',
                '<label style="width:60px;">密码</label>',
                '<input type="text" id="kePassword" class="ke-input-text ke-input-number" name="password" value="" maxlength="3" />',
                '<label style="width:20px;"></label>',
                '<input type="checkbox" id="keAutostart" name="autostart" value="" checked="checked" />隐藏',
                '</div>',
            //mp4,flv格式
				'<div class="ke-dialog-row">',
				'注明: 最佳格式为mp4（视频编码AVC H264）<br/>',
                '建议：最佳录制工具Camtasia Studio 最合适尺寸为800x600或1024x768',
				'</div>',
				'</div>'
			].join('');
            var dialog = self.createDialog({
                name: name,
                width: 450,
                height: 260,
                title: self.lang(name),
                body: html,
                yesBtn: {
                    name: self.lang('yes'),
                    click: function (e) {
                        var url = K.trim(urlBox.val());
                        if (url == 'http://' || K.invalidUrl(url)) {
                            alert(self.lang('invalidUrl'));
                            urlBox[0].focus();
                            return;
                        }
                        var tody = new Date();
                        var haomiao = tody.getMilliseconds();
                        var divplayer = "player" + haomiao;
                        var srcjs = "../kindeditor/plugins/video/video.js";
                        var autohide = autostartBox[0].checked ? 'none' : 'block';
                        var password = passwordBox.val();
                        var lock = false;
                        if (autostartBox[0].checked && password != "") { lock = true; }
                        var flvicon = "../kindeditor/themes/default/flvshow.gif";
                        if (lock) flvicon = "../kindeditor/themes/default/flvlock.gif";

                        var html = [
						'<link rel="stylesheet" href="../kindeditor/plugins/video/video-js.css" />',
					    '<img  id="showhide' + haomiao + '" src="' + flvicon + '" alt="显示/隐藏视频" />\r\n',
                        '<div id="divarea' + haomiao + '" style="display:' + autohide + ';" >\r\n',
						'<script  type="text/javascript">$(document).ready(function () {\r\n',
                        'var setpwd' + haomiao + '="' + password + '";\r\n',
                        'var lockkey=' + lock + ';\r\n',
						'$("#showhide' + haomiao + '").click(function () {\r\n',
						'if(lockkey){',
                        'var getpwd=prompt("请输入密码","");\r\n',
                        'if(setpwd' + haomiao + '==getpwd) {$("#divarea' + haomiao + '").toggle();lockkey=false}\r\n',
                        '}else{$("#divarea' + haomiao + '").toggle();}',
                        ' });});',
                        '</script>\r\n',
                        '<script type="text/javascript" src="' + srcjs + '"></script>\r\n',
                        '<div id="' + divplayer + '" >',
						' <video  class="video-js vjs-big-play-centered" preload="auto" controls="" data-setup="{}" ><source type="video/mp4" src="' + url + '"></video>',
						'</div>',
                        '</div><br>\r\n'
						].join('');
                        //alert(html);
                        self.insertHtml(html).hideDialog().focus();
                    }
                }
            }),
			div = dialog.div,
			urlBox = K('[name="url"]', div),
			viewServerBtn = K('[name="viewServer"]', div),
			autostartBox = K('[name="autostart"]', div);
            passwordBox = K('[name="password"]', div);
            urlBox.val('http://');

            if (allowFlashUpload) {
                var uploadbutton = K.uploadbutton({
                    button: K('.ke-upload-button', div)[0],
                    fieldName: 'imgFile',
                    extraParams: extraParams,
                    url: K.addParam(uploadJson, 'dir=flv'),
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
                            dirName: 'flv',
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
