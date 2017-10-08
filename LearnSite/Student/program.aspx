<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Scm.master" AutoEventWireup="true" StylesheetTheme="Student"  CodeFile="program.aspx.cs" Inherits="Student_program" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cpcm" Runat="Server">
<div  id="showcontent">
<div class="left" style="width: 800px">
<br />
    <div   class="missiontitle">
    <asp:Label ID="LabelMtitle"  runat="server" ></asp:Label><br />
   </div>
   <div class="courseother">
    <asp:Label ID="LabelSnum"  runat="server" Visible="False"></asp:Label>
            <asp:Label ID="LabelMid" runat="server" Visible="False"></asp:Label>            
            <asp:Label ID="LabelUploadType" runat="server" Visible="False"></asp:Label>
			<asp:Label ID="LabelMcid" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="LabelMsort"  runat="server" Visible="False"></asp:Label>
   </div>   
    <link href="../kindeditor/plugins/syntaxhighlighter/styles/shCore.css" rel="stylesheet" type="text/css" />
    <link href="../kindeditor/plugins/syntaxhighlighter/styles/shThemeRDark.css" rel="stylesheet"   type="text/css" />
    <script src="../kindeditor/plugins/syntaxhighlighter/scripts/shCore.js" type="text/javascript"></script>
    <script src="../kindeditor/plugins/syntaxhighlighter/scripts/shBrushCss.js" type="text/javascript"></script>
    <script src="../kindeditor/plugins/syntaxhighlighter/scripts/shBrushJScript.js" type="text/javascript"></script>
    <script src="../kindeditor/plugins/syntaxhighlighter/scripts/shBrushVb.js" type="text/javascript"></script>
    <script src="../kindeditor/plugins/syntaxhighlighter/scripts/shBrushCSharp.js" type="text/javascript"></script>
    <script src="../kindeditor/plugins/syntaxhighlighter/scripts/shBrushCpp.js" type="text/javascript"></script>
    <script src="../kindeditor/plugins/syntaxhighlighter/scripts/shBrushPython.js" type="text/javascript"></script>
    <script src="../kindeditor/plugins/syntaxhighlighter/scripts/shBrushPhp.js" type="text/javascript"></script>
    <script src="../kindeditor/plugins/syntaxhighlighter/scripts/shBrushXml.js" type="text/javascript"></script>
    <script  type="text/javascript">        SyntaxHighlighter.all();  </script>
<div   id="Mcontent"  class="coursecontent" runat="server">	
		</div>
		<br />
		<br />
</div>
<div class="right"><br />
<center>    
    <link href="../kindeditor/themes/me/me.css" rel="stylesheet" type="text/css" />
    <script charset="utf-8" src="../kindeditor/kindeditor-min.js" type="text/javascript"></script>
		<script charset="utf-8" src="../kindeditor/lang/zh_CN.js" type="text/javascript"></script>
        <div   class="missiontitle">&nbsp;<br /></div><br />
        <input type="button" class="sharedisk" id="share" value="我的网盘" onclick="showShare()" />
        <br />
        <br />
            <asp:HyperLink  ID ="VoteLink" runat="server" Target="_blank" 
                CssClass="txtszcenter" SkinID="HyperLinkPink">作品互评</asp:HyperLink>        
        <br />
        <br />
     
        <div>
<br />
            <asp:Image ID="Thumbnail" runat="server" Height="120px" Width="160px" />
            <asp:Label ID="Wtitle" runat="server" ></asp:Label>
        <br /> 
        <br />
            <br />
            <asp:Button ID="BtnScratch" runat="server" Font-Bold="True" 
                onclick="BtnScratch_Click" SkinID="buttonSkinPink" Text="开始编写" />
            <br />
            <br />
            <asp:Label ID="Labelscratch" runat="server" ForeColor="#0066FF"></asp:Label>
            <br />
            <br />
            <asp:Button ID="BtnBegin" runat="server" Font-Bold="True" 
                onclick="BtnBegin_Click" SkinID="buttonSkinPink" Text="开关指令" />
            <br />
    <asp:Label ID="Labelmsg" runat="server"  SkinID="LabelMsgRed"></asp:Label>
            <br />
            <br />
            <br />
    <br />
    </div>       
    <br />
    <br />
    </center>
</div>   
    <br />
        <link href="../Js/tinybox.css" rel="stylesheet" type="text/css" />
        <script src="../Js/tinybox.js" type="text/javascript"></script>
    <script type="text/javascript">
        function showShare() {
            var urlat = "../Student/groupshare.aspx";
            TINY.box.show({ iframe: urlat, boxid: 'frameless', width: 600, height: 400, fixed: false, maskopacity: 60, close: false })
        }
        function scratchShare(urlid) {
            var urlat = "../Student/codeshare.aspx?id=" + urlid;
            TINY.box.show({ iframe: urlat, boxid: 'frameless', width: 500, height: 480, fixed: false, maskopacity: 60, close: false })
        }
    </script>
</div>
</asp:Content>
