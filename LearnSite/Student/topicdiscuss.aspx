<%@ Page Title="" Language="C#" MasterPageFile="~/Student/Scm.master" StylesheetTheme="Student" Validaterequest="false" AutoEventWireup="true" CodeFile="topicdiscuss.aspx.cs" Inherits="Student_topicdiscuss" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cpcm" Runat="Server">
 <div id="student">
<div  id="topper"  style=" text-align: left; width: 800px;">
    <div style="text-align: left; width: 780px;">        
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/topic.png" />
       <asp:Label ID="Labeltopic" runat="server" 
            Font-Size="12pt" Font-Bold="True" Font-Names="宋体, Arial, Helvetica, sans-serif"></asp:Label>
        <asp:ImageButton ID="Btnclock" runat="server" ImageUrl="~/Images/clock.gif" 
            onclick="Btnclock_Click" Enabled="False" />
        <asp:CheckBox ID="TcloseCheck" runat="server" Visible="False" />
    <br />
    </div>
    <div ID="Topics" runat="server" 
        style="border: 2px dotted #99CCFF; text-align: left; width: 770px; display:block;font: 12px/150% Arial;padding: 10px 3px 3px 10px;" >
        </div>
    <br />
    <div ID="TopicsResult" runat="server" 
        style="border: 2px dotted #CCFFCC; text-align: left; width: 770px; display:block;font: 12px/150% Arial;padding: 10px 3px 3px 10px;" >
        </div>
    <br />
    <div style="text-align: left; width: 780px;overflow: hidden;">
    <div>
    <div  class="topicleft">
        <strong>帖子列表</strong>：<asp:Label ID="Labelreplycount" runat="server"></asp:Label>
        &nbsp;<asp:imagebutton ID="ImageBtngoodall" runat="server" 
            ImageUrl="~/Images/right.gif" onclick="ImageBtngoodall_Click" 
            ToolTip="给所有未评分的帖子加2分" Visible="False" />
        </div>
        <div  class="topicright">
      <asp:ImageButton ID="ImageBtnFresh" runat="server" 
            ImageUrl="~/Images/refresh2.gif" onclick="ImageBtnFresh_Click" ToolTip="刷新贴子" />
      <asp:HyperLink ID="HLbottom" runat="server" BorderStyle="None" 
             BorderWidth="0px" ImageUrl="~/Images/bottom.png" NavigateUrl="#bottom" 
            ToolTip="跳到底部"></asp:HyperLink>
            </div>
            </div>
            <br />
            <div>
    <asp:GridView ID="GVtopicDiscuss" runat="server" AutoGenerateColumns="False" 
        CellPadding="1" Width="100%" 
        onrowdatabound="GVtopicDiscuss_RowDataBound"  
        DataKeyNames="Rid"  PageSize="5" CellSpacing="1" 
            ShowHeader="False" GridLines="None" 
            onrowcommand="GVtopicDiscuss_RowCommand"  >
             <Columns>
                 <asp:TemplateField>
                     <ItemTemplate>   
                     <div style="border: 1px solid #F7F7F7; text-align: left;">
                     <div  class="topichead">
                     <div  class="topicleft">
                         <asp:Image ID="Imageflag" runat="server" ImageUrl="~/Images/topicnormal.png" />
                         <asp:Label ID="Labelfloor" runat="server"></asp:Label>楼&nbsp;                  
                         <asp:Label ID="Labelsname" runat="server"  Text='<%# Bind("Sname") %> '></asp:Label>
                         说：<asp:Label ID="Labeldate" runat="server" Text='<%# Bind("Rtime") %> '></asp:Label> &nbsp; &nbsp; &nbsp; 
                         学分：<asp:Label ID="Labelscore" runat="server" Text='<%# Bind("Rscore") %> ' ToolTip="学分" ForeColor="#333333"></asp:Label>
                         <asp:Image ID="Imageagree" runat="server" Visible="False" ImageUrl="~/Images/good16.png" />
                        <asp:CheckBox  ID="Ckedit" runat="server" Checked='<%# Bind("Redit") %> ' Visible="False" />
                        <asp:Label ID="Labelsnum" runat="server"  Text='<%# Bind("Rsnum") %> ' Visible="False"></asp:Label>
                     </div>
                         <div class="topicright">
                         <asp:ImageButton ID="ImageButtonEdit" runat="server" 
                             CausesValidation="false" CommandArgument='<%# Bind("Rid") %>'
                        CommandName="Reply" ImageUrl="~/Images/edno.gif" ></asp:ImageButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:ImageButton ID="ImageButtonGood" runat="server" 
                             CausesValidation="false" CommandArgument='<%# Bind("Rid") %>'
                        CommandName="Good" ImageUrl="~/Images/right.gif" ToolTip="加2分"></asp:ImageButton>
                        &nbsp;&nbsp; &nbsp;&nbsp;
                        <asp:ImageButton ID="ImageButtonless" runat="server" 
                             CausesValidation="false" CommandArgument='<%# Bind("Rid") %>'
                        CommandName="Less" ImageUrl="~/Images/ban.gif" ToolTip="减2分"></asp:ImageButton>
                        &nbsp;&nbsp; &nbsp;&nbsp;
                         <asp:ImageButton ID="ImageButtonDel" runat="server" CausesValidation="false" 
                             CommandArgument='<%# Bind("Rid") %>' CommandName="Del" 
                             ImageUrl="~/Images/delete.gif" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             &nbsp;赞(<asp:Label ID="Labelagree" runat="server" Text='<%# Bind("Ragree") %> '></asp:Label>)                                                
                         </div>
                         &nbsp;
                         </div>
                     <div>
                         <div class="topictext">
                         <%# HttpUtility.HtmlDecode( Eval("Rwords").ToString())%>
                         </div>
                         <div class="topicagree">
                         <asp:ImageButton ID="ImageButtonAgree" runat="server" 
                             CausesValidation="false" CommandArgument='<%# Bind("Rid") %>'
                        CommandName="Agree" ImageUrl="~/Images/good24.gif" ToolTip="点赞"></asp:ImageButton>
                         </div>
                     </div>
                         <br />                         
                        </div>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>             
             <HeaderStyle Font-Bold="False" />
      </asp:GridView>
      </div>
      <div id="bottom"></div>
        <br />
<div  class="topicleft">
        <strong>讨论回复</strong>：<asp:Label ID="Labelreplycountbtm" runat="server"></asp:Label>
        </div>
<div  class="topicright">
        <asp:ImageButton ID="ImageBtnFreshtwo" runat="server" 
            ImageUrl="~/Images/refresh2.gif" onclick="ImageBtnFresh_Click" 
            ToolTip="刷新贴子" />
     <asp:HyperLink ID="HLtop" runat="server" BorderStyle="None" BorderWidth="0px" 
            ImageUrl="~/Images/top.png" NavigateUrl="#topper" ToolTip="跳到顶部"></asp:HyperLink>
            </div>

      </div>
    <div style="width: 800px; overflow: hidden;">        
        <br />
    <textarea  name="textareaWord" style="width: 780px;" ></textarea> 
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
						'fontname', 'fontsize', '|', 'bold', 'italic', 'underline',
						'removeformat', '|', 'justifyleft', 'justifycenter', 'justifyright', 'emoticons'],
		            afterChange: function () {
		                K('.word_count').html(this.count('text'));
		            }
		        });
		    });
		</script>  
    <div style="width: 760px; text-align: center">
    您当前输入了 <span class="word_count">0</span> 个文字（不少于6个汉字，最多为300汉字）
    <br /><br />
            <asp:Button ID="Btnword" runat="server" Text="发表讨论" 
                onclick="Btnword_Click" BorderStyle="None" CssClass="buttonimg" 
            Width="80px" />
    <br />
    <asp:Label ID="Labeldiscuss" runat="server"  SkinID="LabelMsgRed"></asp:Label>
            <br />
            </div>
    <br />
    </div> 
    <br />
    <div>    
        <asp:Label ID="Labelnostu" runat="server" ForeColor="#7D7D7D"></asp:Label>    
    </div>
     </div>
 </div>
</asp:Content>

