<%@ Page Title="" Language="C#" MasterPageFile="~/Lessons/prescm.master" StylesheetTheme="Student" Validaterequest="false" AutoEventWireup="true" CodeFile="pretopicdiscuss.aspx.cs" Inherits="Lessons_pretopicdiscuss" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Ppcm" Runat="Server">
 <div id="student">
<div  id="topper"  style=" text-align: left; width: 600px;">
    <divstyle="text-align: left; width: 96%;">
        <asp:ImageButton ID="Btnclock" runat="server" ImageUrl="~/Images/clock.gif"  Enabled="False" />
        <strong>&nbsp;学案名称</strong>：<asp:Label ID="Labelcourse" runat="server" 
            Font-Size="11pt"></asp:Label>
    <br />
        <asp:CheckBox ID="TcloseCheck" runat="server" Visible="False" />
    <br />
        <strong>
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/topic.png" />
        &nbsp;讨论主题</strong>：<asp:Label ID="Labeltopic" runat="server" 
            Font-Size="11pt"></asp:Label>
    <br />
    </div><br />
    <div ID="Topics" runat="server" 
        style="border: 2px dotted #99CCFF; text-align: left; width: 570px; display:block;word-break:break-all;" >
        </div>
    <br />
    <div ID="TopicsResult" runat="server" 
        style="border: 2px dotted #CCFFCC; text-align: left; width: 570px; display:block;word-break:break-all;" >
        </div>
    <br />
    <div style="text-align: left; width: 580px;">
    <div>
    <div  class="topicleft">
        <strong>帖子列表</strong>：<asp:Label ID="Labelreplycount" runat="server"></asp:Label>
        </div>
        <div  class="topicright">
      <asp:imagebutton ID="ImageBtnFresh" runat="server" 
            ImageUrl="~/Images/refresh2.gif" ToolTip="刷新贴子" />
      <asp:HyperLink ID="HLbottom" runat="server" BorderStyle="None" 
             BorderWidth="0px" ImageUrl="~/Images/bottom.png" NavigateUrl="#bottom" 
            ToolTip="跳到底部"></asp:HyperLink>
            </div>
            </div>
            <br />
            <div>    
      </div>
      <div id="bottom"></div>
        <br />
<div  class="topicleft">
        <strong>讨论回复</strong>：<asp:Label ID="Labelreplycountbtm" runat="server"></asp:Label>
        </div>
<div  class="topicright">
        <asp:imagebutton ID="ImageBtnFreshtwo" runat="server" 
            ImageUrl="~/Images/refresh2.gif"  ToolTip="刷新贴子" />
     <asp:HyperLink ID="HLtop" runat="server" BorderStyle="None" BorderWidth="0px" 
            ImageUrl="~/Images/top.png" NavigateUrl="#topper" ToolTip="跳到顶部"></asp:HyperLink>
            </div>
      </div>
    <div style="width: 600px;">  
        <br />
    <textarea  name="textareaWord" style="width: 580px;" ></textarea> 
	 <script charset="utf-8" src="../kindeditor/kindeditor-min.js" type="text/javascript"></script>
	<script charset="utf-8" src="../kindeditor/lang/zh_CN.js" type="text/javascript"></script>
		<script>
		    var editor;
		    KindEditor.ready(function (K) {
		        editor = K.create('textarea[name="textareaWord"]', {
		            resizeType: 1,
		            pasteType: 0,					
					newlineTag:"br",
		            allowPreviewEmoticons: false,
		            allowImageUpload: false,
		            items: [
						'fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
						'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'emoticons'],
		            afterChange: function () {
		                K('.word_count').html(this.count('text'));
		            }
		        });
		    });
		</script>  
    <div style="width: 560px; text-align: center">
    您当前输入了 <span class="word_count">0</span> 个文字（最多为120汉字）
    <br /><br />
            <asp:Button ID="Btnword" runat="server" Text="发表讨论" 
             BorderStyle="None" CssClass="buttonimg"  Width="80px" />
    <br />
    <asp:Label ID="Labeldiscuss" runat="server"  SkinID="LabelMsgRed"></asp:Label>
            <br />
            </div>
    </div> 
    <br />
     </div>
     <br />
</div>
</asp:Content>

