<%@ page title="" language="C#" masterpagefile="~/teacher/Teach.master" stylesheettheme="Teacher" validaterequest="false" autoeventwireup="true" inherits="Teacher_missionadd, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div  class="cplace">
    <div  class="cleft">
        活动名称：<asp:TextBox ID="Texttitle" runat="server"  SkinID="TextBoxNormal"  Width="200px" ></asp:TextBox>
        作品类型<asp:DropDownList ID="DDLmfiletype" runat="server"  Width="60px" Font-Names="Arial">
        </asp:DropDownList>
        <asp:CheckBox ID="CheckUpload" runat="server" Text="是否提交" Checked="True" />
        &nbsp;<asp:CheckBox ID="CheckPublish" runat="server" Text="是否发布"  Checked="True" />
        &nbsp;<asp:CheckBox ID="CheckGroup" runat="server" Text="小组合作" />
        <asp:CheckBox ID="CheckRemote" runat="server" Text="远程图片" ToolTip="自动下载远程图片，有时失效！" />
        <asp:CheckBox ID="CheckMicoWorld" runat="server" Text="上次作品"  Checked="False" 
            ToolTip="显示上一节课作品，适合项目学习"  />
        </div>
    <div  >
        <script charset="utf-8" src="../kindeditor/kindeditor-min.js"></script>
		<script charset="utf-8" src="../kindeditor/lang/zh_CN.js"></script>
		<script>
		    var editor;
            var cid= <%=myCid() %>;
            var ty="Course";
            var upjs= '../kindeditor/aspnet/upload_json.aspx?cid='+cid+'&Ty='+ty;
            var fmjs='../kindeditor/aspnet/file_manager_json.aspx?cid='+cid+'&Ty='+ty;
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
    <textarea  name="textareaItem" style="width: 830px; height:550px;" ></textarea>
    </div>
     <div  class="placehold">
               <asp:Label ID="Labelmsg" runat="server" ></asp:Label>
               <br />
               选择自定义评价标准：<asp:DropDownList ID="DDLMgid" runat="server" Font-Size="9pt"
            Width="160px" Font-Names="Arial">
        </asp:DropDownList>
               <br />
         <br />
              <asp:Button ID="Btnadd" runat="server"  Text="添加活动" OnClick="Btnadd_Click"  SkinID="BtnNormal" />&nbsp;&nbsp;&nbsp;
              <asp:Button ID="BtnCourse" runat="server"  Text="学案返回" OnClick="BtnCourse_Click"  SkinID="BtnNormal" /><br />
         <br />
         </div>
           
        </div>

</asp:Content>

