KindEditor.plugin('text', function (K) {
    var editor = this, name = 'text';
    // 点击图标时执行
    editor.clickToolbar(name, function () {
        editor.insertHtml('<div style="background-color:#FBF4D5;border: 1px dashed #ccc; height:auto; " class="textbox" contenteditable="true"> &nbsp;</div>');
    });
});