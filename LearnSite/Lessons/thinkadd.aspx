<%@ Page Title="" Language="C#" Validaterequest="false" AutoEventWireup="true" CodeFile="thinkadd.aspx.cs" Inherits="Lessons_thinkadd" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div >
    <div style="width: 780px; font-size: 9pt;text-align: left; margin:auto;">
        &nbsp;学案名称：<asp:TextBox 
            ID="Texttitle" runat="server" BorderStyle="Solid" Width="360px" Font-Size="9pt" 
            ReadOnly="True" BackColor="#FFFFE6" BorderWidth="1px" BorderColor="#E0E0E0"></asp:TextBox>
        </div>
    <div style="width: 780px; border-right: gainsboro 1px solid; border-top: gainsboro 1px solid; font-size: 9pt; border-bottom-width: 1px; border-bottom-color: gainsboro; border-left: gainsboro 1px solid; text-align: left; margin:auto;">
           <script charset="utf-8" src="../kindeditor/kindeditor-min.js"></script>
		<script charset="utf-8" src="../kindeditor/lang/zh_CN.js"></script>
		<script>
		    var editor;
            var cid= <%=myCid() %>;
            var ty="Course";
            var upjs= '../kindeditor/aspnet/upload_json.aspx?Cid='+cid+'&Ty='+ty;
            var fmjs='../kindeditor/aspnet/file_manager_json.aspx?Cid='+cid+'&Ty='+ty;
		    KindEditor.ready(function (K) {
		        editor = K.create('textarea[name="textareaItem"]', {
		            resizeType: 1,
		            newlineTag: "br",                    
				uploadJson : upjs,
				fileManagerJson : fmjs,
				allowFileManager : true,
                filterMode : false		            
		        });
		    });
		</script>
    <textarea  name="textareaItem" style="width:780px; height:350px;" ></textarea>
    </div>
     <div style="width: 660px;margin:auto; text-align: center;">
               <asp:Label ID="Labelmsg" runat="server" Font-Size="9pt" Width="300px"></asp:Label>
         <br />
              <asp:Button ID="Btnadd" runat="server" Font-Size="9pt" Height="20px" Text="添加"
                Width="89px" OnClick="Btnadd_Click" BackColor="WhiteSmoke" 
                   BorderColor="Gainsboro" BorderStyle="Solid" BorderWidth="1px"  />
         </div>           
        </div>
    </form>
</body>
</html>

