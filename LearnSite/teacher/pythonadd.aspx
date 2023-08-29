<%@ page title="" language="C#" masterpagefile="~/teacher/Teach.master" stylesheettheme="Teacher" validaterequest="false" autoeventwireup="true" inherits="Teacher_pythonadd, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div  class="cplace">
<div  class="cleft">
        Python主题：<asp:TextBox ID="Texttitle" runat="server"  SkinID="TextBoxNormal" 
            Width="200px" ></asp:TextBox>
        <asp:CheckBox ID="CheckPublish" runat="server" Text="发布"  Checked="True" />
        <asp:CheckBox ID="CheckBack" runat="server" Text="分步" ToolTip="命令行和编辑器模式切换"  />
        <asp:CheckBox ID="Checkhelp" runat="server" Text="绘图" ToolTip="编程和绘图语句帮助切换"  />
        <asp:CheckBox ID="Checkblock" runat="server" Text="拼图" ToolTip="拼图编程模式"  />
        <asp:CheckBox ID="Checkblockpy" runat="server" Text="积木" ToolTip="积木编程模式（优先）"  />
        &nbsp;<img src="../images/python.png" style="width: 18px; height: 18px" />练习
        <asp:FileUpload ID="Fupload" runat="server" Font-Size="10pt" />
</div>
    <div  >
        <script charset="utf-8" src="../kindeditor/kindeditor-min.js"></script>
		<script charset="utf-8" src="../kindeditor/lang/zh_CN.js"></script>
		<script>
		    var editor;
            var cid= <%=myCid() %>;
            var ty="Course";
            var upjs= '../kindeditor/aspnet/upload_json.aspx?cid='+cid+'&ty='+ty;
            var fmjs='../kindeditor/aspnet/file_manager_json.aspx?cid='+cid+'&ty='+ty;
		    KindEditor.ready(function (K) {
		        editor = K.create('textarea[name="textareaItem"]', {
		            resizeType: 1,
		            newlineTag: "br", 
				uploadJson : upjs,
				fileManagerJson : fmjs,
				allowFileManager : true,
                filterMode : false,
					afterCreate : function() {
						this.loadPlugin('autoheight');
					}
		        });
		    });
		</script>
    <textarea  name="textareaItem" style="width: 830px; height:450px;" ></textarea>
    </div>
     <div  class="placehold">
               <asp:Label ID="Labelmsg" runat="server" ></asp:Label>
               <br />
               选择自定义评价标准：<asp:DropDownList ID="DDLMgid" runat="server" Font-Size="9pt"
            Width="160px" Font-Names="Arial">
        </asp:DropDownList>
               <br />
         <br />
              <asp:Button ID="Btnadd" runat="server"  Text="添加主题" OnClick="Btnadd_Click"  SkinID="BtnNormal" />&nbsp;&nbsp;&nbsp;
              <asp:Button ID="BtnCourse" runat="server"  Text="学案返回" OnClick="BtnCourse_Click"  SkinID="BtnNormal" /><br />
         <br />
         </div>
           
        </div>

</asp:Content>

