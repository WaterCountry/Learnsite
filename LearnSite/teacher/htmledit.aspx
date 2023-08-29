<%@ page title="" language="C#" masterpagefile="~/teacher/Teach.master" validaterequest="false" autoeventwireup="true" inherits="teacher_htmledit, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div  class="cplace">
    <div  class="cleft">
        网页主题：<asp:TextBox ID="Texttitle" runat="server"  SkinID="TextBoxNormal" 
            Width="220px" ></asp:TextBox>
        <asp:CheckBox ID="CheckPublish" runat="server" Text="是否发布"  Checked="True" />
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
                filterMode : false,
					afterCreate : function() {
						this.loadPlugin('autoheight');
					}
		        });
		    });
		</script>
    <textarea  id ="mcontent" runat ="server" style="width: 830px; height:450px;" ></textarea> 
    </div>
     <div  class="placehold">
               <asp:Label ID="Labelmsg" runat="server" ></asp:Label>
               <br />
               选择自定义评价标准：<asp:DropDownList ID="DDLMgid" runat="server" Font-Size="9pt"
            Width="160px" Font-Names="Arial">
        </asp:DropDownList>
               <br />
         <br />
              <asp:Button ID="Btnedit" runat="server"  Text="修改主题" OnClick="Btnedit_Click"  SkinID="BtnNormal" />&nbsp;&nbsp;&nbsp;
              <asp:Button ID="BtnCourse" runat="server"  Text="学案返回" OnClick="BtnCourse_Click"  SkinID="BtnNormal" /><br />
         <br />
         </div>
           
        </div>
</asp:Content>

