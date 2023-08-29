<%@ page title="" language="C#" masterpagefile="~/teacher/Teach.master" validaterequest="false" autoeventwireup="true" inherits="Teacher_consoleadd, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
 <div  class="cplace">
    <div  class="cleft">
        &nbsp;测评名称：<asp:TextBox ID="Texttitle" runat="server"  SkinID="TextBoxNormal" 
            Width="240px" ></asp:TextBox>
        &nbsp;<asp:CheckBox ID="Publish" runat="server" Text="是否发布" />
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
		        editor = K.create('textarea[name="ctl00$Content$mcontent"]', {
		            resizeType: 1,
		            newlineTag: "br",                    
				uploadJson : upjs,
				fileManagerJson : fmjs,
				allowFileManager : true,
                filterMode : false		            
		        });
		    });
		</script>
    <textarea  id ="mcontent" runat ="server" style="width: 780px; height:500px;" ></textarea> 
    </div>
     <div  class="placehold">
               <asp:Label ID="Labelmsg" runat="server" ></asp:Label>
         <br />
              <asp:Button ID="Btnadd" runat="server"  Text="添加测评" 
                   SkinID="BtnNormal" onclick="Btnadd_Click" />&nbsp;&nbsp;&nbsp;
              <asp:Button ID="BtnCourse" runat="server"  Text="返回"   SkinID="BtnNormal" 
                   onclick="BtnCourse_Click" /><br />
         <br />
         </div>
           
        </div>
</asp:Content>

