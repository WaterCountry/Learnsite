<%@ Page Title="" Language="C#" Validaterequest="false" AutoEventWireup="true" CodeFile="thinkedit.aspx.cs" Inherits="Lessons_thinkedit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<div style="text-align: center;">
    <div style="width: 780px; font-size: 9pt;text-align: left; margin:auto;">
        &nbsp;学案名称：<asp:TextBox ID="Texttitle" runat="server" BorderColor="#E7E7E7" BorderStyle="Solid" BackColor="#FFFFE6"
                BorderWidth="1px" Width="360px" Font-Size="9pt" ReadOnly="True"></asp:TextBox>
        </div>
    <div style="width: 780px; text-align: left; margin:auto;">
           <script charset="utf-8" src="../kindeditor/kindeditor-min.js"></script>
		<script charset="utf-8" src="../kindeditor/lang/zh_CN.js"></script>
		<script>
		    var editor;
            var cid= <%=myCid() %>;
            var ty="Course";
            var upjs= '../kindeditor/aspnet/upload_json.aspx?Cid='+cid+'&Ty='+ty;
            var fmjs='../kindeditor/aspnet/file_manager_json.aspx?Cid='+cid+'&Ty='+ty;
		    KindEditor.ready(function (K) {
		        editor = K.create('textarea[name="mcontent"]', {
		            resizeType: 1,
		            newlineTag: "br",                     
				uploadJson : upjs,
				fileManagerJson : fmjs,
				allowFileManager : true,
                filterMode : false		            
		        });
		    });
		</script>
    <textarea  id ="mcontent" runat ="server" style="width: 780px; height:350px;" ></textarea>  
    </div>
     <div style="width: 660px;margin:auto;">
               <asp:Label ID="Labelmsg" runat="server" Font-Size="9pt" Width="300px"></asp:Label>
         <br />
              <asp:Button ID="BtnEdit" runat="server" Font-Size="9pt" Height="20px" Text="修改"
                Width="89px" OnClick="BtnEdit_Click" BackColor="WhiteSmoke" 
                   BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px"  />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Button ID="BtnCourse" runat="server" Font-Size="9pt" Height="20px" Text="返回"
                Width="89px" OnClick="BtnCourse_Click" BackColor="WhiteSmoke" 
                   BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px"  />
         </div>           
        </div>
    </form>
</body>
</html>

