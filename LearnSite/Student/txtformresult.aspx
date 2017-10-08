<%@ Page Language="C#"  StylesheetTheme="Student" AutoEventWireup="true" CodeFile="txtformresult.aspx.cs" Inherits="Student_txtformresult" %>
<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>
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
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/inquiry.png" />
       <asp:Label ID="LbMtitle" runat="server" 
            Font-Size="12pt" Font-Bold="True" 
            Font-Names="宋体,Arial,Helvetica,sans-serif"></asp:Label>
    <br />
    </div>
    <div style="text-align: left; width: 950px;overflow: hidden;">
    <div>
    <div  class="topicleft">
        <strong>当前列表</strong>：<asp:Label ID="Labelreplycount" runat="server"></asp:Label>
        &nbsp;<anthem:ImageButton ID="ImageBtngoodall" runat="server" 
            ImageUrl="~/Images/right.gif" onclick="ImageBtngoodall_Click" 
            ToolTip="给所有未评分的表单加6分" Visible="False" />
        </div>
        <div  class="topicright">
      <anthem:ImageButton ID="ImageBtnFresh" runat="server" 
            ImageUrl="~/Images/refresh2.gif" onclick="ImageBtnFresh_Click"  />
      <asp:HyperLink ID="HLbottom" runat="server" BorderStyle="None" 
             BorderWidth="0px" ImageUrl="~/Images/bottom.png" NavigateUrl="#bottom" 
            ToolTip="跳到底部"></asp:HyperLink>
            </div>
            </div>
            <br />
            <div>
    <anthem:GridView ID="GVtxtform" runat="server" AutoGenerateColumns="False" 
        CellPadding="1" Width="100%" 
        onrowdatabound="GVtxtform_RowDataBound"  
        DataKeyNames="Rid"  PageSize="5" CellSpacing="1" 
            ShowHeader="False" GridLines="None" 
            onrowcommand="GVtxtform_RowCommand"  >
             <Columns>
                 <asp:TemplateField>
                     <ItemTemplate>   
                     <div style="border: 1px solid #F7F7F7; text-align: left;">
                     <div  class="topichead">
                     <div  class="topicleft">
                         <asp:Image ID="Imageflag" runat="server" ImageUrl="~/Images/topicnormal.png" />
                         <asp:Label ID="Labelfloor" runat="server"></asp:Label>楼&nbsp;                  
                         <asp:Label ID="Labelsname" runat="server"  Text='<%# Bind("Sname") %> ' Font-Bold="True"></asp:Label>
                         ：<asp:Label ID="Labeldate" runat="server" Text='<%# Bind("Rtime") %> '></asp:Label> &nbsp; &nbsp; &nbsp; 
                         学分：<asp:Label ID="Labelscore" runat="server" Text='<%# Bind("Rscore") %> ' ToolTip="学分" ForeColor="#333333"></asp:Label>
                         <asp:Image ID="Imageagree" runat="server" Visible="False" ImageUrl="~/Images/good16.png" />
                        <asp:Label ID="Labelsnum" runat="server"  Text='<%# Bind("Rsnum") %> ' Visible="False"></asp:Label>
                     </div>
                         <div class="topicright">
                        &nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:ImageButton ID="ImageButtonGood" runat="server" 
                             CausesValidation="false" CommandArgument='<%# Bind("Rid") %>'
                        CommandName="Good" ImageUrl="~/Images/right.gif" ToolTip="加2分"></asp:ImageButton>
                        &nbsp;&nbsp; &nbsp;&nbsp;
                        <asp:ImageButton ID="ImageButtonless" runat="server" 
                             CausesValidation="false" CommandArgument='<%# Bind("Rid") %>'
                        CommandName="Less" ImageUrl="~/Images/ban.gif" ToolTip="减2分"></asp:ImageButton>
                             &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;赞(<asp:Label ID="Labelagree" runat="server" Text='<%# Bind("Ragree") %> '></asp:Label>)                                                
                         </div>
                         &nbsp;
                         </div>
                     <div>
                         <div class="formtext">
                         <%# UnEdit(HttpUtility.HtmlDecode( Eval("Rwords").ToString()))%>
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
      </anthem:GridView>
      </div>
      <div id="bottom"></div>
        <br />
<div  class="topicright">
        <anthem:ImageButton ID="ImageBtnFreshtwo" runat="server" 
            ImageUrl="~/Images/refresh2.gif" onclick="ImageBtnFresh_Click"  />
     <asp:HyperLink ID="HLtop" runat="server" BorderStyle="None" BorderWidth="0px" 
            ImageUrl="~/Images/top.png" NavigateUrl="#topper" ToolTip="跳到顶部"></asp:HyperLink>
            </div>

      </div>
    <br />
    <div>    
        <asp:Label ID="Labelnostu" runat="server" ForeColor="#7D7D7D"></asp:Label>    
    </div>
     </div>
 </div>
 </center>
 </div>
    </form>
</body>
</html>
