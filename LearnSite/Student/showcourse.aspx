<%@ page title="" language="C#" masterpagefile="~/student/Scm.master" stylesheettheme="Student" validaterequest="false" autoeventwireup="true" inherits="Student_showcourse, LearnSite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Cpcm" Runat="Server">
<div id="showcontent">
    <br />  	
   <div   class="missiontitle">
    <asp:Label ID="LabelCtitle" runat="server"  CssClass="coursetitle"></asp:Label><br />
   </div>
    <div class="courseother">	
    </div>
    <div  id="Ccontent" class="coursecontent" runat ="server">   
    </div>
    <br/>
    <br />        
</div>
</div>
</asp:Content>

