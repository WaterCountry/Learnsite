<%@ page title="" language="C#" masterpagefile="~/teacher/Teach.master" stylesheettheme="Teacher" autoeventwireup="true" inherits="Teacher_pixelshow, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<div class="courseshow">
    <br />
        <div   class="missiontitle">
    <asp:Label ID="LabelMtitle"  runat="server" ></asp:Label>
   </div><br />
    <div class="courseother">
       日期：<asp:Label ID="LabelMdate"  runat="server" ></asp:Label>
			&nbsp;作品类型：<asp:Image ID="ImageType" runat="server" />
			<asp:Label ID="LabelMfiletype" runat="server" ></asp:Label>
            &nbsp;<asp:CheckBox ID="CheckPublish" runat="server" Text="是否发布" 
            Enabled="False" />
            &nbsp;<asp:HyperLink 
            ID="HLMgid" runat="server">评价标准</asp:HyperLink>
            <asp:ImageButton ID="BtnEdit" runat="server" ToolTip="点击修改" 
            ImageUrl="~/images/edit.gif" onclick="BtnEdit_Click" 
           style="width: 16px" />
   &nbsp;<asp:ImageButton ID="BtnReturnSmall" runat="server" ToolTip="返回" 
            ImageUrl="~/images/return.gif" onclick="BtnReturnSmall_Click" 
           style="width: 16px" />
   </div>
        <div   id="Mcontent"  class="coursecontent" runat="server">	
		</div>
		<br />
         <asp:LinkButton ID="LinkBtn" runat="server"  OnClick="LinkBtn_Click" SkinID="LinkBtn">返回学案</asp:LinkButton>
    <br />
		<br />

</div> 
    <br />
</asp:Content>



