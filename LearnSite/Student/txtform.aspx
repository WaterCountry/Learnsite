<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Scm.master" StylesheetTheme="Student" AutoEventWireup="true" CodeFile="txtform.aspx.cs" Inherits="Student_txtform" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cpcm" Runat="Server">
<div  id="showcontent">
<div class="left" style="width: 800px">
<br />
    <div   class="missiontitle">
     &nbsp;<asp:Label ID="LabelMtitle"  runat="server" ></asp:Label>
   &nbsp;</div><br />
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
    <script  type="text/javascript"> SyntaxHighlighter.all();  </script>
    <div  oncontextmenu="return false" ondragstart="return false" >
    <div  id="Mcontent"  class="coursecontent" runat="server">	
		</div>
        </div>
		<br />
		<br />
</div>
<div class="right">
<center>    
        &nbsp;<br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <input id="Btnform" type="button" value="提交填写"  onclick="SaveForm();" 
            style="border-width: 0px; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt; background-color: #3399FF; width: 80px; height: 24px;" /><br />
        <br />
        <br />
        <br />
        <br /> 
        <br />
        <div id="msg" style="color: #FF0000"></div>
        <br />
        <br />
        <br />
        <asp:HyperLink ID="Hlresult" runat="server" CssClass="txts20center" 
            Height="20px" SkinID="HyperLink" 
            Target="_blank" Width="80px">查看结果</asp:HyperLink>
        <br />
        <script type="text/javascript">
            function SaveForm() {
                var saveurl = "saveform.ashx?Mid=" + "<%=showMid()%>";
                var wordstr = $("div.coursecontent").html();
                $.ajax({
                    type: "post",
                    url: saveurl,
                    data:{Word:wordstr},
                    dataType: "html",
                    success: function (data) {
                                $('#msg').html(data.toString());                       
                    }
                });
            }
        </script>
        <br />
        <br />
        <br />
        <br />
    </center>
</div>   
    <br />
</div>
</asp:Content>

