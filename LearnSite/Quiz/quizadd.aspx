<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" Validaterequest="false"  AutoEventWireup="true" CodeFile="quizadd.aspx.cs" Inherits="Quiz_quizadd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div   class="placehold">
    <div  class="quizleft">
        &nbsp;
        选择题型：<asp:DropDownList ID="DDLqtype" runat="server" AutoPostBack="True" 
            Font-Size="9pt" onselectedindexchanged="DDLqtype_SelectedIndexChanged">
            <asp:ListItem Selected="True" Value="0">单选题</asp:ListItem>
            <asp:ListItem Value="1">多选题</asp:ListItem>
            <asp:ListItem Value="2">判断题</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;
        学案类型：<asp:DropDownList ID="DDLclass" runat="server" Width="100px" 
            Font-Size="9pt">
            </asp:DropDownList>
            &nbsp;
        
        分值：<asp:DropDownList ID="DDLqscore" runat="server" 
                Font-Size="9pt">
            <asp:ListItem Value="1">1分</asp:ListItem>
            <asp:ListItem Selected="True" Value="2">2分</asp:ListItem>
            <asp:ListItem Value="3">3分</asp:ListItem>
            <asp:ListItem Value="4">4分</asp:ListItem>
            <asp:ListItem Value="5">5分</asp:ListItem>
            <asp:ListItem Value="6">6分</asp:ListItem>
            </asp:DropDownList>
    </div>
    <div   class="quizdiv" >
           <script charset="utf-8" src="../kindeditor/kindeditor-min.js"></script>
		<script charset="utf-8" src="../kindeditor/lang/zh_CN.js"></script>
		<script>
		    var editor;
            var cid='-1';
            var ty="Quiz";
            var upjs= '../kindeditor/aspnet/upload_json.aspx?Cid='+cid+'&Ty='+ty;
            var fmjs='../kindeditor/aspnet/file_manager_json.aspx?Cid='+cid+'&Ty='+ty;
		    KindEditor.ready(function (K) {
		        editor = K.create('textarea[name="textareaItem"]', {
		            resizeType: 1,
		            newlineTag: "br",                    
				uploadJson : upjs,
				fileManagerJson : fmjs,
				allowFileManager : true		            
		        });
		    });
		</script>
    <textarea  name="textareaItem" style="width: 700px; height:200px;" ></textarea>
<br />              
    </div>
    <div class="quizleft">
    <div class="quizleft2">
        <br />
               答案：</div>
    <div class="quizright2">
    <asp:RadioButtonList ID="RBLselect" runat="server" 
            RepeatDirection="Horizontal" Visible="True" Height="35px">
        <asp:ListItem Selected="True">A</asp:ListItem>
        <asp:ListItem>B</asp:ListItem>
        <asp:ListItem>C</asp:ListItem>
        <asp:ListItem>D</asp:ListItem>
        </asp:RadioButtonList>
        <asp:CheckBoxList ID="CBLselect" runat="server" RepeatDirection="Horizontal" 
            Visible="False">
            <asp:ListItem>A</asp:ListItem>
            <asp:ListItem>B</asp:ListItem>
            <asp:ListItem>C</asp:ListItem>
            <asp:ListItem>D</asp:ListItem>
        </asp:CheckBoxList>
        <asp:RadioButtonList ID="RBLjudge" runat="server" RepeatDirection="Horizontal" 
            Visible="False" Height="35px">
            <asp:ListItem Selected="True">对</asp:ListItem>
            <asp:ListItem>错</asp:ListItem>
        </asp:RadioButtonList>
        </div>
    </div> <br />
        <div class="quizleft">
            <div class="quizleft2">
                <br />
                分析：</div>
            <div class="quizright2">
                     <asp:TextBox ID="TextBoxqanalyze" runat="server" Width="500px"  
                         SkinID="TextBoxNormal" Height="20px" ></asp:TextBox>
            </div>
    </div>
     <div  class="quizdiv">
              &nbsp;<br /><br />
              <asp:Button ID="Btnadd" runat="server"  Text="添加" OnClick="Btnadd_Click"  
                  SkinID="BtnNormal" />
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Button ID="Btnreturn" runat="server"  Text="返回" OnClick="Btnreturn_Click"  
                  SkinID="BtnNormal" />
               <br />
              <br />
               <asp:Label ID="Labelmsg" runat="server" ></asp:Label>
               <br />
               <br />         
         </div>
         <br />           
        </div>
</asp:Content>

