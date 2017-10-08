/*******************************************************************************
* KindEditor - WYSIWYG HTML Editor for Internet
* Copyright (C) 2006-2011 kindsoft.net
*
* @author Roddy <luolonghao@gmail.com>
* @site http://www.kindsoft.net/
* @licence http://www.kindsoft.net/license.php
*******************************************************************************/

// google code prettify: http://google-code-prettify.googlecode.com/
// http://google-code-prettify.googlecode.com/

KindEditor.plugin('code', function(K) {
	var self = this, name = 'code';
	self.clickToolbar(name, function() {
		var lang = self.lang(name + '.'),
			html = ['<div style="padding:10px 20px;">',
				'<div class="ke-dialog-row">',
				'标题：',
				'<input type="text" id="keCodename" class="ke-input-text" style="width:240px;" name="Codename" value=""/>&nbsp;&nbsp;',
				'<select class="ke-code-type">',
				'<option value="cpp">C/C++</option>',
				'<option value="vb">VB</option>',
				'<option value="c">Arduino</option>',
				'<option value="py">Python</option>',
				'<option value="c#">C#</option>',
				'<option value="php">PHP</option>',
				'<option value="js">JavaScript</option>',
				'<option value="html">HTML</option>',
				'<option value="css">CSS</option>',
				'<option value="xml">XML</option>',
				'<option value="rb">Ruby</option>',
				'<option value="java">Java</option>',
				'<option value="bsh">Shell</option>',
				'<option value="">Other</option>',
				'</select>',
				'&nbsp;&nbsp;&nbsp;',
				'<input type="checkbox" id="keAutostart" name="autostart" value="" />',
				'隐藏代码',								
				'</div>',
				'<textarea class="ke-textarea" style="width:480px;height:360px;"></textarea>',
				'</div>'].join(''),
			dialog = self.createDialog({
				name : name,
				width : 540,
				title : self.lang(name),
				body : html,
				yesBtn : {
					name : self.lang('yes'),
					click : function(e) {
						var type = K('.ke-code-type', dialog.div).val(),
							code = textarea.val();
						var autostart =autostartBox[0].checked ? 'none' : 'block';
						var tody = new Date();
						var haomiao = tody.getMilliseconds();
						var cdtitle=codetitle.val();
							html = [
							'<div>'+cdtitle+'<img id="showhide'+haomiao+'" src="../kindeditor/themes/default/codeshow.gif" alt="显示/隐藏代码" />\r\n',
							'<script  type="text/javascript">$(document).ready(function () {\r\n',
							'$("#showhide'+haomiao+'").click(function () { $("#cdarea'+haomiao+'").toggle(); });});</script>\r\n',
							'<div id="cdarea' + haomiao + '" style="width:100%;background-color:#666666;display:' + autostart + ';">',
							'\r\n<pre class="brush:' + type + ';">\r\n' + K.escape(code) + '</pre>\r\n',
							'</div></div>\r\n</br>'
							].join('');

						if (K.trim(code) === '') {
							alert(lang.pleaseInput);
							textarea[0].focus();
							return;
						}
						self.insertHtml(html).hideDialog().focus();
					}
				}
			}),
			codetitle= K('[name="Codename"]', dialog.div);
			textarea = K('textarea', dialog.div);
			autostartBox = K('[name="autostart"]', dialog.div);
		textarea[0].focus();
	});
});
