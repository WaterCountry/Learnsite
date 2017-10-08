<%@ Page Language="C#" MasterPageFile="~/Student/Scm.master" StylesheetTheme="Student" Validaterequest="false" AutoEventWireup="true" CodeFile="summaryedit.aspx.cs" Inherits="Student_summaryedit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cpcm" Runat="Server">
<div  id="showcontent">
        <center>
            <table style="border: 1px double #CADEFD;font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt; text-align: left;">
                <tr>
                    <td colspan="3" style="width: 660px">
                        学案名称：<asp:Label ID="Label1" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="border: 1px double #CADEFD; width: 660px;height: 28px;">
                        总结内容：<br />
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
				allowFileManager : true		            
		        });
		    });
		</script>
    <textarea  name="textareaItem" style="width: 680px; height:300px;" ><%=contentstr %></textarea> 
                        <br />
                    </td>
                </tr>
                <tr>                    
                    <td style="width: 220px; height: 16px;">
                        </td>
                    <td style="width: 220px; height: 16px;">
                        </td>
                    <td style="width: 220px; height: 16px;">
                        撰写日期：<asp:Label ID="Label6" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td  colspan="3" style="width: 660px;text-align: center">
                        <asp:Button ID="ButtonEdit" runat="server" onclick="ButtonEdit_Click" 
                            Text="添加总结" SkinID="buttonSkinPink" Enabled="False" />
                    </td>
                </tr>
            </table>
            </center>
</div>
</asp:Content>
