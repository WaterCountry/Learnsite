<%@ page title="" language="C#" masterpagefile="~/teacher/Teach.master" stylesheettheme="Teacher" validaterequest="false" autoeventwireup="true" inherits="Teacher_pythonshow, LearnSite" %>

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
            &nbsp;<asp:Image ID="Imgauto" runat="server" /><asp:HyperLink ID="HLauto" runat="server">自动批改</asp:HyperLink>
            &nbsp;<asp:CheckBox ID="CheckPublish" runat="server" Text="发布" 
            Enabled="False" />        
            <asp:CheckBox ID="CheckBack" runat="server" Text="分步" Enabled="False"  />
            <asp:CheckBox ID="Checkhelp" runat="server" Text="绘图" Enabled="False"  />
            <asp:CheckBox ID="Checkblock" runat="server" Text="拼图" Enabled="False"  />
            <asp:CheckBox ID="Checkblockpy" runat="server" Text="积木" Enabled="False"  />
            &nbsp;<img src="../images/python.png" style="width: 18px; height: 18px" />
            <asp:HyperLink ID="HlExample" runat="server" Target="_blank">编程实例</asp:HyperLink>
            &nbsp;&nbsp; <asp:HyperLink 
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

