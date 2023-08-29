<%@ page language="C#" stylesheettheme="Student" autoeventwireup="true" inherits="Student_txtformresult, LearnSite" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body oncontextmenu="return false" ondragstart="return false" onselectstart ="return false" onselect="document.selection.empty()" oncopy="document.selection.empty()" onbeforecopy="return false" onmouseup="document.selection.empty()">
    <form id="form1" runat="server">
          <div  >
          <center>
           <div id="student">
<div  id="topper"  style=" text-align: left; width: 960px;">
    <div style="text-align: center; ">
    <br />        
        <anthem:Image ID="Image2" runat="server" ImageUrl="~/images/inquiry.png" />
       <anthem:Label ID="LbMtitle" runat="server" 
            Font-Size="12pt" Font-Bold="True" 
            Font-Names="宋体,Arial,Helvetica,sans-serif"></anthem:Label>
    <br />
    </div>
    <div style="text-align: left; width: 950px;overflow: hidden;">
    <div>
    <div  class="topicleft">
        <strong>当前列表</strong>：<anthem:Label ID="Labelreplycount" runat="server"></anthem:Label>
        &nbsp;<anthem:ImageButton ID="ImageBtngoodall" runat="server" 
            ImageUrl="~/images/right.gif" onclick="ImageBtngoodall_Click" 
            ToolTip="给所有未评分的表单加6分" Visible="False" />
        </div>
        <div  class="topicright">
      <anthem:ImageButton ID="ImageBtnFresh" runat="server" 
            ImageUrl="~/images/refresh2.gif" onclick="ImageBtnFresh_Click"  />
      <anthem:HyperLink ID="HLbottom" runat="server" BorderStyle="None" 
             BorderWidth="0px" ImageUrl="~/images/bottom.png" NavigateUrl="#bottom" 
            ToolTip="跳到底部"></anthem:HyperLink>
            </div>
            </div>
            <br />
            <div>
    <anthem:GridView ID="GVtxtform" runat="server" AutoGenerateColumns="False" 
        CellPadding="1" Width="100%" 
        onrowdatabound="GVtxtform_RowDataBound"  
        DataKeyNames="rid"  PageSize="5" CellSpacing="1" 
            ShowHeader="False" GridLines="None" 
            onrowcommand="GVtxtform_RowCommand"  >
             <Columns>
                 <asp:TemplateField>
                     <ItemTemplate>   
                     <div style="border: 1px solid #F7F7F7; text-align: left;">
                     <div  class="topichead">
                     <div  class="topicleft">
                         <anthem:Image ID="Imageflag" runat="server" ImageUrl="~/images/topicnormal.png" />
                         <anthem:Label ID="Labelfloor" runat="server"></anthem:Label>楼&nbsp;                  
                         <anthem:Label ID="Labelsname" runat="server"  Text='<%# Bind("Sname") %> ' Font-Bold="True"></anthem:Label>
                         ：<anthem:Label ID="Labeldate" runat="server" Text='<%# Bind("Rtime") %> '></anthem:Label> &nbsp; &nbsp; &nbsp; 
                         学分：<anthem:Label ID="Labelscore" runat="server" Text='<%# Bind("Rscore") %> ' ToolTip="学分" ForeColor="#333333"></anthem:Label>
                         <anthem:Image ID="Imageagree" runat="server" Visible="False" ImageUrl="~/images/good16.png" />
                        <anthem:Label ID="Labelsnum" runat="server"  Text='<%# Bind("Rsnum") %> ' Visible="False"></anthem:Label>
                     </div>
                         <div class="topicright">
                        &nbsp;&nbsp;&nbsp;&nbsp;
                         <anthem:ImageButton ID="ImageButtonGood" runat="server" 
                             CausesValidation="false" CommandArgument='<%# Bind("rid") %>'
                        CommandName="Good" ImageUrl="~/images/right.gif" ToolTip="加2分"></anthem:ImageButton>
                        &nbsp;&nbsp; &nbsp;&nbsp;
                        <anthem:ImageButton ID="ImageButtonless" runat="server" 
                             CausesValidation="false" CommandArgument='<%# Bind("rid") %>'
                        CommandName="Less" ImageUrl="~/images/ban.gif" ToolTip="减2分"></anthem:ImageButton>
                             &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;赞(<anthem:Label ID="Labelagree" runat="server" Text='<%# Bind("Ragree") %> '></anthem:Label>)                                                
                         </div>
                         &nbsp;
                         </div>
                     <div>
                         <div class="formtext">
                         <%# UnEdit(HttpUtility.HtmlDecode( Eval("Rwords").ToString()))%>
                         </div>
                         <div class="topicagree">
                         <anthem:ImageButton ID="ImageButtonAgree" runat="server" 
                             CausesValidation="false" CommandArgument='<%# Bind("rid") %>'
                        CommandName="Agree" ImageUrl="~/images/good24.gif" ToolTip="点赞"></anthem:ImageButton>
                         </div>
                     </div>
                         <br />                         
                        </div>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>             
             <HeaderStyle Font-Bold="False" />
      </anthem:GridView>
      </div>
      <div id="bottom"></div>
        <br />
<div  class="topicright">
        <anthem:ImageButton ID="ImageBtnFreshtwo" runat="server" 
            ImageUrl="~/images/refresh2.gif" onclick="ImageBtnFresh_Click"  />
     <anthem:HyperLink ID="HLtop" runat="server" BorderStyle="None" BorderWidth="0px" 
            ImageUrl="~/images/top.png" NavigateUrl="#topper" ToolTip="跳到顶部"></anthem:HyperLink>
            </div>

      </div>
    <br />
    <div>    
        <anthem:Label ID="Labelnostu" runat="server" ForeColor="#7D7D7D"></anthem:Label>    
    </div>
     </div>
 </div>
 </center>
 </div>
    </form>
</body>
</html>
