<%@ page title="" language="C#" masterpagefile="~/student/Scm.master" autoeventwireup="true" stylesheettheme="Student" inherits="Student_description, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cpcm" Runat="Server">
<div  id="showcontent">
<div class="centerdiv">
<br />
    <div   class="missiontitle">
    <asp:Label ID="LabelMtitle"  runat="server" ></asp:Label><br />
   </div>
   <div class="courseother">
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
<div   id="Mcontent"  class="descriptioncontent" runat="server">	
		</div>
    <asp:Button ID="Btnread" runat="server" onclick="Btnread_Click" Text="已阅读" 
        ToolTip="1分钟之后才能点击完成当前内容学习"  Enabled="False" BackColor="#3399FF" 
        BorderStyle="None" Height="24px" Width="80px"  />
		<br />
		<br />
</div>   
</div>
        <script type="text/javascript">
            var i = 20; //设定退出按钮几秒钟后有效
            function setbar() {
                i--;
                var btnid = "<%= Btnread.ClientID %>";
                if (i < 0) {
                    document.getElementById(btnid).disabled = false;
                }
                else {

                    document.getElementById(btnid).value = "已阅读("+i+")";
                }
                setTimeout("setbar()", 1000);
            }
            setbar(); 
          </script>
</asp:Content>

