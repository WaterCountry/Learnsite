<%@ Page Language="C#" AutoEventWireup="true" CodeFile="codeproject.aspx.cs" Inherits="Student_codeproject" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
        <script src="../Js/jquery-1.8.2.min.js" type="text/javascript"></script>
        <script src="../Statics/swfobject.js" type="text/javascript"></script>
        <script type="text/javascript">
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

        function handleParameters() {}
        
        var flashvars = {
            extensionDevMode: true,
            noneMode: true,
            microworldMode: <%=Microworld %>,
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
        function hidebutton(){
            $('#barbtn').hide();//隐藏    
        }
        function showbutton(){    
             $('#barbtn').show();//显示    
        }
        </script>
    <style type="text/css">
		html, body { height:100%; overflow:hidden; text-align: center;}
        body{margin: 0;}
    </style>
</head>
<body>
 <div id="scratch">
     <p><a href="http://www.adobe.com/go/getflashplayer">Get Adobe Flash player</a></p>
 </div>
        <script src="../Statics/extensions/scratch_ext.js" type="text/javascript"></script>
        <script src="../Statics/extensions/scratch_plugin.js" type="text/javascript"></script>
        <script src="../Statics/extensions/scratch_nmh.js" type="text/javascript"></script>
        <script src="../Statics/extensions/scratch_proxies.js" type="text/javascript"></script>
</body>
</html>
