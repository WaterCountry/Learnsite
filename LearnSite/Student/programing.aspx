<%@ page language="C#" autoeventwireup="true" inherits="Student_programing, LearnSite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../js/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="../Statics/swfobject.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.onbeforeunload = function () { return "您确定要关闭页面吗？记得先保存作品!"; }
        var fwidth = "100%";
        var fheight = "100%";
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
            canDown:true,     //设定菜单显示或隐藏下载项目功能
            extensionDevMode: true,
            microworldMode: false,
            viewMode: false,
            project: '<%=Filename %>',
            autostart: 'false',
            pm: '<%=Id %>',
            projectTitle: '<%=Titles %>',
            projectOwner: '<%=Owner %>'
        };
        var params = {
            menu: "false",
            scale: "noScale",
            allowFullscreen: "true",
            allowScriptAccess: "always",
            bgcolor: "",
            wmode: "transparent" //direct can cause issues with FP settings & webcam
        };

        var swfFile = "../Statics/MinMake.swf";
        swfobject.embedSWF(swfFile,"scratch", fwidth, fheight, "10.0.0","../Statics/expressInstall.swf",flashvars, params, null, handleEmbedStatus);
    </script>
    <style type="text/css">
        html, body { height:100%; overflow:hidden;}
        body{margin: 0;}
    </style>
</head>
<body>
    <div style="text-align: right; position: absolute; right: 50px; top: 3px; font-size: 11pt;
        z-index: 2;">
        <div id="barbtn">
        <span id="uploading" style="color:#fff; font-weight:bold;"></span>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <img id="bill" src="../images/bill.png" alt="学习单" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <button id="savebtn">
                立即保存</button>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <button id="returnbtn">
                返回学案</button>
        </div>
    </div>
    <div id="scratch">
    <object classid="clsid27CDB6E-AE6D-11cf-96B8-444553540000">
     <embed width="360" height="360" type="application/x-shockwave-flash"></embed>
    </object>
    </div>
    <div id="mcontext" style="display: none; background: #fff; overflow-y: auto; overflow-x: hidden;
        position: absolute; width: 500px; max-height: 710px; min-height: 220px; z-index: 999;
        left: 0px; top: 0px; padding: 2px;">
        <div style="height: 16px; text-align: right; right: 50px;">
            <img id="zoom" src="../images/zoom.gif" alt="放大缩小" /></div>
        <%=Mcontents %>
    </div>
    <script type="text/javascript">
        function hidebutton() {
            $('#barbtn').hide(); //隐藏    
        }
        function showbutton() {
            $('#barbtn').show(); //显示    
        }
        function showsave() {
            $('#savebtn').show(); //显示立即保存按钮
        }
        function hidesave() {
            $('#savebtn').hide(); //隐藏立即保存按钮
        }
        function showreturn() {
            $('#uploading').html("");
            $('#returnbtn').show(); //显示返回按钮
        }
        $("#mcontext").dblclick(function () {
            $("#mcontext").slideToggle();
        });
        $("#bill").click(function () {
            $("#mcontext").slideToggle();
        });
        $("#savebtn").click(function () {
            var obj = swfobject.getObjectById('scratch');
            obj.SaveNow();
            $('#savebtn').hide(); //显示立即保存按钮
            $('#returnbtn').hide(); //隐藏返回按钮
            $('#uploading').html("正在上传作品中……，请稍等！");
        });
        $("#returnbtn").click(function () {
            self.location = '<%=Fpage %>';
        });
        $("#zoom").toggle(function () {
            $("#mcontext").width(810);
        }, function () {
            $("#mcontext").width(500);
        });
    </script>
        <script src="../Statics/extensions/scratch_ext.js" type="text/javascript"></script>
        <script src="../Statics/extensions/scratch_plugin.js" type="text/javascript"></script>
        <script src="../Statics/extensions/scratch_nmh.js" type="text/javascript"></script>
        <script src="../Statics/extensions/scratch_proxies.js" type="text/javascript"></script>
</body>
</html>
