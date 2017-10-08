<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" Validaterequest="false" AutoEventWireup="true" CodeFile="courseedit.aspx.cs" Inherits="Teacher_courseedit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
    <div  class="cplace">
    <div class="cleft">
        &nbsp;学案名称：<asp:TextBox ID="Texttitle" runat="server"  Width="436px"  
            SkinID="TextBoxNormal"></asp:TextBox>
        </div>
    <div  class="cleft">
        &nbsp;学案分类：<asp:DropDownList ID="DDLclass" runat="server" Width="100px" 
            Font-Size="9pt">
            </asp:DropDownList></div>
    <div  class="cleft">
        &nbsp;授课年级：<asp:DropDownList ID="DDLcobj" runat="server" 
            Font-Size="9pt" Width="40px">
                </asp:DropDownList>        
         &nbsp; &nbsp; 第<asp:DropDownList ID="DDLCterm" runat="server" 
            Font-Names="Arial" Font-Size="8pt" Width="40px">
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem Selected="True">2</asp:ListItem>
        </asp:DropDownList>
        学期&nbsp;&nbsp; 第<asp:DropDownList ID="DDLCks" runat="server" Font-Size="8pt"
            Width="40px" Font-Names="Arial">       
        </asp:DropDownList>课节&nbsp;&nbsp;<asp:CheckBox ID="CheckPublish" runat="server" Text="是否发布"  Checked="True" />
        &nbsp;
        </div>
    <div >
    <script charset="utf-8" src="../kindeditor/kindeditor-min.js"></script>
		<script charset="utf-8" src="../kindeditor/lang/zh_CN.js"></script>
		<script>
		    var editor;
            var cid= <%=myCid() %>;
            var ty="Course";
            var upjs= '../kindeditor/aspnet/upload_json.aspx?Cid='+cid+'&Ty='+ty;
            var fmjs='../kindeditor/aspnet/file_manager_json.aspx?Cid='+cid+'&Ty='+ty;
		    KindEditor.ready(function (K) {
		        editor = K.create('textarea[name="ctl00$Content$mcontent"]', {
		            resizeType: 1,
		            newlineTag: "br",  
                    cssPath : ['../kindeditor/plugins/code/prettify.css'],
				uploadJson : upjs,
				fileManagerJson : fmjs,
				allowFileManager : true,
                filterMode : false
		        });
		    });
		</script>
    <textarea  id ="mcontent" runat ="server" style="width: 780px; height:400px; left:10px;" ></textarea>  
    </div>
     <div  class="placehold">
               <asp:Label ID="Labelmsg" runat="server" ></asp:Label>
         <br />
              <asp:Button ID="Btnedit" runat="server"  Text="确定" onclick="Btnedit_Click"  SkinID="BtnNormal"  />
               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Button ID="Btnreturn" runat="server"  Text="返回" onclick="Btnreturn_Click"  
                   SkinID="BtnNormal"  />
               <br />
               <br />
         </div>
           
        </div>

</asp:Content>

