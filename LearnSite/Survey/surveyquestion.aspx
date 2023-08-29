﻿<%@ page title="" language="C#" masterpagefile="~/teacher/Teach.master" stylesheettheme="Teacher" validaterequest="false" autoeventwireup="true" inherits="Survey_surveyquestion, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
 <div  class="cplace">
    <div  class="cleft">
        &nbsp;调查试题描述：&nbsp;<asp:CheckBox ID="QBlack" runat="server" Text="是否填空" />
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
    <textarea  id ="mcontent" runat ="server" style="width: 760px; height:200px;" ></textarea> 
    </div>
     <div  class="placehold">
               <asp:Label ID="Labelmsg" runat="server" ></asp:Label>
         <br />
              <asp:Button ID="Btnadd" runat="server"  Text="添加试题" OnClick="Btnadd_Click"  
                   SkinID="BtnNormal" />&nbsp;&nbsp;&nbsp;
              <asp:Button ID="BtnSurvey" runat="server"  Text="返回" 
                   OnClick="BtnSurvey_Click"  SkinID="BtnNormal" /><br />
         <br />
         </div>
           
        </div>
</asp:Content>