<%@ Page Language="C#" AutoEventWireup="true" CodeFile="codeshare.aspx.cs" Inherits="Student_codeshare" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <style type="text/css">
        body{margin: 0;text-align: center;}
    </style>
</head>
<body>
<div style="text-align: center;">
<script src="../Js/jquery-1.8.2.min.js" type="text/javascript"></script>
<script src="../Statics/swfobject.js" type="text/javascript"></script>
<script type="text/javascript">
    var fwidth = 480; // flash view width
    var fheight = 400; // flash view height      
    // （scratch目录不能为根目录，否则读不出）

    var Scratch = Scratch || {};
    Scratch.FlashApp = Scratch.FlashApp || {};
    var editorId = "scratch";

    function handleEmbedStatus(e) {
        var scratch = $(document.getElementById(editorId));
        Scratch.FlashApp.ASobj = scratch[0];
        Scratch.FlashApp.$ASobj = $(Scratch.FlashApp.ASobj);
    }

    // enables the SWF to log errors
    function JSthrowError(e) {
        if (window.onerror) window.onerror(e, 'swf', 0);
        else console.error(e);
    }

    function JSeditorReady() {
        return true;
    }

    function handleParameters() { }

    var flashvars = {
        extensionDevMode: true,
        microworldMode: '<%=Microworld %>',
        viewMode: true,
        project: '',
        autostart: 'false',
        pm: '<%=Id %>',
        projectTitle: '<%=Titles %>',
        projectOwner: '<%=Owner %>'
    };
    var params = {
        allowFullscreen: "true",
        allowScriptAccess: "always"
    };

    var swfFile = "../Statics/MinMake.swf";
    swfobject.embedSWF(swfFile, "scratch", fwidth, fheight, "10.0.0", "../Statics/expressInstall.swf", flashvars, params, null, handleEmbedStatus);
</script>
 <div id="scratch" style="margin: auto; text-align: center">
        <p><a href="http://www.adobe.com/go/getflashplayer">Get Adobe Flash player</a></p>
</div>
<div><asp:Label ID="LabelTitle" runat="server" Text="分享" Font-Size="9pt"></asp:Label></div>
<div style="width: 170px;MARGIN-RIGHT: auto; MARGIN-LEFT: auto;">
<div class="bdsharebuttonbox">
<a href="#" class="bds_more" data-cmd="more"></a>
<a href="#" class="bds_qzone" data-cmd="qzone" title="分享到QQ空间"></a>
<a href="#" class="bds_tsina" data-cmd="tsina" title="分享到新浪微博"></a>
<a href="#" class="bds_tqq" data-cmd="tqq" title="分享到腾讯微博"></a>
<a href="#" class="bds_weixin" data-cmd="weixin" title="分享到微信"></a>
<a href="#" class="bds_sqq" data-cmd="sqq" title="分享到QQ好友"></a>
<a href="#" class="bds_tieba" data-cmd="tieba" title="分享到百度贴吧"></a>
<div>
</div>
<script type="text/javascript">    window._bd_share_config = { "common": { "bdSnsKey": {}, "bdText": "<%=Owner %>", "bdMini": "2", "bdMiniList": false, "bdPic": "<%=Pic %>", "bdStyle": "0", "bdSize": "16" }, "share": {}, "image": { "viewList": ["qzone", "tsina", "tqq", "weixin", "sqq", "tieba"], "viewText": "分享到：", "viewSize": "16"} }; with (document) 0[(getElementsByTagName('head')[0] || body).appendChild(createElement('script')).src = 'http://bdimg.share.baidu.com/static/api/js/share.js?v=89860593.js?cdnversion=' + ~(-new Date() / 36e5)];</script>
    </div>
</div>
</div>
        <script src="../Statics/extensions/scratch_ext.js" type="text/javascript"></script>
        <script src="../Statics/extensions/scratch_plugin.js" type="text/javascript"></script>
        <script src="../Statics/extensions/scratch_nmh.js" type="text/javascript"></script>
        <script src="../Statics/extensions/scratch_proxies.js" type="text/javascript"></script>
</body>
</html>

