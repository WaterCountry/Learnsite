<%@ page language="C#" autoeventwireup="true" inherits="Teacher_consolepreview, LearnSite" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>Python交互解释器WEB-IDLE</title>
</head>

<script src="../code/skulpt.min.js" type="text/javascript"></script>
<script src="../code/skulpt-stdlib.js" type="text/javascript"></script>
<script src="../code/build/src/ace.js" type="text/javascript"></script>
<script src="../code/build/src/ext-language_tools.js" type="text/javascript"></script>
<link rel="stylesheet" type="text/css" href="../code/ipython.css"/>
<script type="text/javascript">
    var ide;
    window.addEventListener("load", function (event) {
        var editor = document.getElementById("editor");
        ide = new ipythonExample(editor);
    })
    //<div class="container ace-gruvbox ace_editor"> 会增加一个文本框
    //ace.js 修改12px/normal为28px
</script>

<body>
<div class="container ">
    <div id="editor" class="ace-gruvbox ace_editor" > </div>
</div>
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="hidenjson" runat="server" />
        <asp:HiddenField ID="hidennid" runat="server" />
        <asp:HiddenField ID="hidencid" runat="server" />
        <asp:HiddenField ID="hidenlid" runat="server" />
    </div>    
<a id="btnreturn" href="#" class="button" >返回学案</a>
    </form>
<script src="../code/jquery.min.js" type="text/javascript"></script>
<script src="../code/ipython.js?ver=20211015"  type="text/javascript"></script>
</body>
</html>
