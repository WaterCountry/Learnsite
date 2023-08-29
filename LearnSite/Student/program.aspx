<%@ page title="" language="C#" masterpagefile="~/student/Scm.master" autoeventwireup="true" stylesheettheme="Student" inherits="Student_program, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cpcm" Runat="Server">
<div  id="showcontent">
<div class="left" >
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
            <asp:CheckBox ID="CheckBack" runat="server"  Visible="False" />
            <asp:CheckBox ID="CheckBlock" runat="server"  Visible="False" />
            <asp:CheckBox ID="CheckBlockpy" runat="server"  Visible="False" />
			<asp:Label ID="LabelLid" runat="server" Visible="False"></asp:Label>
   </div>   
<div   id="Mcontent"  class="coursecontent" runat="server">	
		</div>
		<br />
		<br />
</div>
<div class="right">
<center>    
    <link href="../kindeditor/themes/me/me.css" rel="stylesheet" type="text/css" />
    <script charset="utf-8" src="../kindeditor/kindeditor-min.js" type="text/javascript"></script>
		<script charset="utf-8" src="../kindeditor/lang/zh_CN.js" type="text/javascript"></script>
        <input type="button" class="sharedisk" id="share" value="我的网盘" onclick="showShare()" />
        <br />
        <br />
            <asp:HyperLink  ID ="VoteLink" runat="server" Target="_blank" 
                CssClass="txtszcenter" SkinID="HyperLinkPink">作品互评</asp:HyperLink>        
        <br />
        <br />
     
        <div>
            <asp:Image ID="Thumbnail" runat="server"  style=" width:160px; max-height:240px;"/>
            <div id="pixelsmall" runat="server" ></div>
            <asp:Label ID="Wtitle" runat="server" ></asp:Label>
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
            <br />
            <asp:Button ID="ButtonClear" runat="server" Font-Bold="True" 
                 SkinID="buttonSkinPink" Text="清除提交" ToolTip="清除模拟学生提交的本项作品" 
                onclick="ButtonClear_Click" />
            <br />
            <br />
    <asp:Label ID="Labelmsg" runat="server"  SkinID="LabelMsgRed"></asp:Label>
            <br />
            <asp:Image ID="ImagePass" CssClass="ImagePass" runat="server" ImageUrl="~/images/sucessed.png" 
                Visible="False"  />
            <br />
            <br />
    <br />
    </div>       
    <br />
    <br />
    </center>
</div>   
    <br />
        <link href="../js/tinybox.css" rel="stylesheet" type="text/css" />
        <script src="../js/tinybox.js" type="text/javascript"></script>
    <script type="text/javascript">
        function showShare() {
            var urlat = "../student/groupshare.aspx";
            TINY.box.show({ iframe: urlat, boxid: 'frameless', width: 600, height: 400, fixed: false, maskopacity: 60, close: false })
        }
    </script>
</div>
</asp:Content>
