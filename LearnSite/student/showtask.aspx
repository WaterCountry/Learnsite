<%@ page title="" language="C#" masterpagefile="~/student/Scm.master" autoeventwireup="true" stylesheettheme="Student" inherits="Student_showtask, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cpcm" Runat="Server">
<div  id="showcontent">
<br />
    <div   class="missiontitle">
    <asp:Label ID="LabelMtitle"  runat="server" ></asp:Label><br />
   </div>
   <div class="courseother">
    <asp:Label ID="LabelSnum"  runat="server" Visible="False"></asp:Label>
			<asp:CheckBox ID="CkMupload" runat="server" Enabled="false" Visible="False" />
            <asp:CheckBox ID="CkMgroup" runat="server" Enabled="false" Visible="False" />
            <asp:Label ID="LabelMid" runat="server" Visible="False"></asp:Label>            
            <asp:Label ID="LabelUploadType" runat="server" Visible="False"></asp:Label>
			<asp:Label ID="LabelMcid" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="LabelMsort"  runat="server" Visible="False"></asp:Label>
            <asp:Label ID="LabelLid"  runat="server" Visible="False"></asp:Label>
   </div>   

<div   id="Mcontent"  class="taskcontent" runat="server">	
</div>
		<br />
		<br />
<div class="sidebar"><br />
<center>    
    <script src="../Plupload/plupload.full.min.js" type="text/javascript"></script>
        <div ><br /></div><br />
        <input type="button" class="sharedisk" id="share" value="我的网盘" onclick="showShare()" />
        <br />
        <br />
            <asp:HyperLink  ID ="VoteLink" runat="server" Target="_blank" 
                CssClass="txtszcenter" SkinID="HyperLinkPink">作品互评</asp:HyperLink>        
        <br />
     <asp:Panel ID="Panelworks" runat="server" >
        <div>
<br />
            <asp:Image runat="server" ID="upFileType" Visible="False" /><br />
            <asp:HyperLink ID="oldUrl" runat="server" Height="16px" Visible="False" 
                Target="_blank"></asp:HyperLink>
                <br /><br />
            <asp:HyperLink ID="upFileUrl" runat="server" Height="16px" Visible="False" 
                Target="_blank"></asp:HyperLink>
        <br /> 
        <br />
            <br />
            <asp:Panel ID="Panelswfupload" runat="server">
            <div id="swfu_container" style="margin: 0px 10px;">
		    <div  id="container" style="text-align: center; margin: auto"> 
                <a id="pickfiles" href="javascript:;" style="background: url(../plupload/me.png) no-repeat ;width:100px;height: 23px;line-height: 23px;display: inline-block; color: #000; font-size: 10pt;">提交作品</a>
                <div id="filelist">
                    
                </div>
		    </div>
                 <script type="text/javascript">
                     var isup = false;
                     var mid = "<%=LabelMid.Text %>";
                     var lid = "<%=LabelLid.Text %>";
                     var num = "<%=LabelSnum.Text %>";
                     var urlstr = "uploadworkm.aspx?lid=" + lid;
                     var uploader = new plupload.Uploader({
                         runtimes: 'html5,html4',
                         browse_button: 'pickfiles', // you can pass an id...
                         container: document.getElementById('container'), // ... or DOM Element itself
                         url: urlstr,
                         multi_selection: false,
                         filters: {
                             max_file_size: '100mb',
                             mime_types: [
			                            { title: "work files", extensions: "<%=LabelUploadType.Text %>" }
		                            ]
                         },

                         init: {
                             PostInit: function () {
                                 document.getElementById('filelist').innerHTML = '';

                             },

                             FilesAdded: function (up, files) {
                                 plupload.each(files, function (file) {
                                     document.getElementById('filelist').innerHTML += '<div id="' + file.id + '">' + file.name + ' (' + plupload.formatSize(file.size) + ') <b></b></div>';
                                 });
                                 uploader.start();
                             },

                             UploadProgress: function (up, file) {
                                 document.getElementById(file.id).getElementsByTagName('b')[0].innerHTML = '<span>' + file.percent + "%</span>";
                                 if (file.percent == 100 && !isup) {
                                     isup = true;
                                     OfficeToPng();
                                     alert("作品已经提交成功！");
                                 }
                             },

                             UploadComplete: function (up, file) {
                                 location.reload();
                             },
                             Error: function (up, err) {
                                 document.getElementById('console').appendChild(document.createTextNode("\nError #" + err.code + ": " + err.message));
                             }
                         }
                     });

                     uploader.init();

                     //自动保存成绩
                     function OfficeToPng() {
                         console.log("文档转图片调用开始");                     
                         var formData = new FormData();
                         formData.append('mid', mid);
                         formData.append('num', num);
                         var saveurl = "spire.ashx";
                         $.ajax({
                             url: saveurl,
                             type: "POST",
                             cache: false,
                             data: formData,
                             dataType: "html",
                             processData: false,
                             contentType: false
                         }).done(function (res) {
                             console.log(res);
                         }).fail(function (res) {
                             console.log("保存失败");
                         });
                     }


	                </script>
	        </div>
            </asp:Panel>
            <br />
            <asp:Image ID="ImageType" runat="server" />
            <asp:Label ID="LabelMfiletype" runat="server"></asp:Label>
            格式<br />
    <asp:Label ID="Labelmsg" runat="server"  SkinID="LabelMsgRed"></asp:Label>
    </div>       
    </asp:Panel>
    <br /> 
    </center>
</div>   
    <br />
        <link href="../js/tinybox.css" rel="stylesheet" type="text/css" />
        <script src="../js/tinybox.js" type="text/javascript"></script>
    <script type="text/javascript">
        function jsCopy(contentid) {
            var e = document.getElementById(contentid); //对象是content 
            e.select(); //选择对象 
            document.execCommand("Copy"); //执行浏览器复制命令 
        }
        function showShare() {
            var urlat = "../student/groupshare.aspx";
            TINY.box.show({ iframe: urlat, boxid: 'frameless', width: 600, height: 400, fixed: false, maskopacity: 60, close: true })
        }   
    </script>
</div>
</asp:Content>

