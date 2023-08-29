<%@ page title="" language="C#" masterpagefile="~/teacher/Teach.master" stylesheettheme="Teacher" autoeventwireup="true" inherits="Teacher_topicshow, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<br />
<div  class="typeplace"> 

    <div class="courseshow">
    <br />
        <asp:ImageButton ID="Btnclock" runat="server" ImageUrl="~/images/clock.gif" 
            onclick="Btnclock_Click" />       
        <asp:Label ID="Labeltid"  runat="server" Visible="false" ></asp:Label>
    主题讨论名称：<asp:Label ID="LabelTtitle"  runat="server" ></asp:Label>
<br /><br />
   <div >
    &nbsp;&nbsp;日期：
			<asp:Label ID="LabelTdate"  runat="server" ></asp:Label>
			&nbsp;&nbsp;学案编号：
			<asp:Label ID="LabelMcid" runat="server"></asp:Label>
			&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="BtnEdit" runat="server" ToolTip="点击修改" 
            ImageUrl="~/images/edit.gif" onclick="BtnEdit_Click" 
           style="width: 16px" />
   </div>   
        <div   id="Tcontent"  class="coursecontent" runat="server">	
		</div>
		<br />
		<br />

</div> 
              <br />
              <asp:Button ID="Btnreturn" runat="server"  Text="返回" OnClick="Btnreturn_Click"  SkinID="BtnNormal" />
    <br />
 <br /> 
</div> 
</asp:Content>

