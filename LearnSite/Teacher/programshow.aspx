<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master"  StylesheetTheme="Teacher" Validaterequest="false" AutoEventWireup="true" CodeFile="programshow.aspx.cs" Inherits="Teacher_programshow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div class="courseshow">
    <br />
        <div   class="missiontitle">
    <asp:Label ID="LabelMtitle"  runat="server" ></asp:Label>
   </div><br />
    <div class="courseother">
       日期：<asp:Label ID="LabelMdate"  runat="server" ></asp:Label>
			&nbsp;作品类型：<asp:Image ID="ImageType" runat="server" />
			<asp:Label ID="LabelMfiletype" runat="server" ></asp:Label>
            &nbsp;实例链接:<asp:HyperLink ID="Hlexample" runat="server"></asp:HyperLink>
        <asp:CheckBox ID="CheckPublish" runat="server" Text="是否发布" 
            Enabled="False" />&nbsp;
            <asp:CheckBox ID="CheckMicoWorld" runat="server" Text="微界面" 
            Enabled="False" />
            &nbsp;<asp:HyperLink 
            ID="HLMgid" runat="server">评价标准</asp:HyperLink>
            <asp:ImageButton ID="BtnEdit" runat="server" ToolTip="点击修改" 
            ImageUrl="~/Images/edit.gif" onclick="BtnEdit_Click" 
           style="width: 16px" />
   &nbsp;<asp:ImageButton ID="BtnReturnSmall" runat="server" ToolTip="返回" 
            ImageUrl="~/Images/return.gif" onclick="BtnReturnSmall_Click" 
           style="width: 16px" />
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
         <asp:LinkButton ID="LinkBtn" runat="server"  OnClick="LinkBtn_Click" SkinID="LinkBtn">返回学案</asp:LinkButton>
    <br />
		<br />

</div> 
    <br />
</asp:Content>

