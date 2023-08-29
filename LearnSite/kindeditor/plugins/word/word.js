KindEditor.plugin('word', function (K) {
    var editor = this, name = 'word';
    // 点击图标时执行
    editor.clickToolbar(name, function () {
        editor.insertHtml('<label class="textlabel"  style="background-color:#FBF4D5;border: 1px dashed #ccc;display:inline-block;" contenteditable="true">&nbsp;</label>&nbsp;');
    });
});