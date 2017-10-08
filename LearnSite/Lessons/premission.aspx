<%@ Page Title="" Language="C#" MasterPageFile="~/Lessons/prescm.master" AutoEventWireup="true"  StylesheetTheme="Student"  CodeFile="premission.aspx.cs" Inherits="Lessons_premission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Ppcm" Runat="Server">
<div  id="showcontent">
<div class="left" style="width: 780px">
<br />
<div   class="missiontitle">
     &nbsp;<asp:Label ID="LabelMtitle"  runat="server" ></asp:Label>
   &nbsp;</div><br /><br />
   <div class="courseother">
    日期：<asp:Label ID="LabelMdate"  runat="server" ></asp:Label>
			&nbsp;&nbsp;学号：<asp:Label ID="LabelSnum"  runat="server" ></asp:Label>
			&nbsp;&nbsp;作品类型：<asp:Image ID="ImageType" runat="server" />
            <asp:Label ID="LabelMfiletype" runat="server" ></asp:Label>
			&nbsp;&nbsp;作品提交：<asp:CheckBox ID="CkMupload" runat="server" Enabled="false" />
            &nbsp;&nbsp;小组合作：<asp:CheckBox ID="CkMgroup" runat="server" Enabled="false" />
   </div>   
<div   id="Mcontents"  class="coursecontent" runat="server">	
		</div>
		<br />
		<br />
</div>   
</div>
</asp:Content>

