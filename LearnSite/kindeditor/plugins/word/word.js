KindEditor.plugin('word', function (K) {
    var editor = this, name = 'word';
    // 点击图标时执行
    editor.clickToolbar(name, function () {
        editor.insertHtml('<label class="textlabel"  style="background-color:#E6FFE6;" contenteditable="true">&nbsp;</label>');
    });
});