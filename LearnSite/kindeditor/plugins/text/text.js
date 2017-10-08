KindEditor.plugin('text', function (K) {
    var editor = this, name = 'text';
    // 点击图标时执行
    editor.clickToolbar(name, function () {
        editor.insertHtml('<div class="textbox" style="background-color:#E6FFE6;" contenteditable="true">&nbsp;</div>');
    });
});