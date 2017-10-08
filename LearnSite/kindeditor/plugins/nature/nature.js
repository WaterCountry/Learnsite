/*******************************************************************************
* KindEditor - WYSIWYG HTML Editor for Internet
* Copyright (C) 2006-2011 kindsoft.net
*
* @author Roddy <luolonghao@gmail.com>
* @site http://www.kindsoft.net/
* @licence http://www.kindsoft.net/license.php
*******************************************************************************/

KindEditor.plugin('nature', function(K) {
	var self = this, name = 'nature';
	self.clickToolbar(name, function() {
		var lang = self.lang(name + '.'),
			html = ['<div style="padding:10px 20px;">',
				'<div class="ke-dialog-row">',
				'标题：',
				'<input type="text" id="kenaturename" class="ke-input-text" style="width:240px;" name="naturename" value=""/>',				
				'</div>',
				'<textarea class="ke-textarea" style="width:580px;height:160px;"></textarea>',
				'</div>'].join(''),
			dialog = self.createDialog({
				name : name,
				width : 620,
				title : self.lang(name),
				body : html,
				yesBtn : {
					name : self.lang('yes'),
					click : function(e) {
						var type = K('.ke-nature-type', dialog.div).val(),
							nature = textarea.val();
						var tody = new Date();
						var haomiao = tody.getMilliseconds();
						var cdtitle=naturetitle.val();
							html = [
							'<div>'+'<span id="showhidetxt'+haomiao+'" class="ke-nature" >'+cdtitle+'</span>\r\n',
							'<script  type="text/javascript">\r\n',
							'$("#showhidetxt'+haomiao+'").click(function () { $("#cdarea'+haomiao+'").toggle(); });</script>\r\n',
							'<div id="cdarea' + haomiao + '" style="width:100%; display:none;">',
							'\r\n' + K.escape(nature) + '\r\n',
							'</div></div>\r\n</br>'
							].join('');

						if (K.trim(nature) === '') {
							alert(lang.pleaseInput);
							textarea[0].focus();
							return;
						}
						self.insertHtml(html).hideDialog().focus();
					}
				}
			}),
			naturetitle= K('[name="naturename"]', dialog.div);
			textarea = K('textarea', dialog.div);
		textarea[0].focus();
	});
});
