<%@ page title="" language="C#" stylesheettheme="Student" validaterequest="false" autoeventwireup="true" inherits="Student_downwork, LearnSite" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>作品下载</title> 
    </head>
<body ondragstart="return false" onselectstart ="return false" >
     <form id="form1" runat="server">
    <div  class="studmasterhead"> 
            <center>
                <div class="placeauto">
<div id="student">
<div style="background-color: #E8F3FF; padding-bottom: 10px;"> 
    <br />
    〖<asp:Label ID="Labelmission" runat="server"></asp:Label>〗
        <strong>作品下载</strong>：<asp:Image ID ="ImageType" runat="server" />
<asp:HyperLink ID="HLfile" runat="server"  Visible="False" BorderStyle="None" 
        CssClass="txtszcenter" Height="18px" Target="_blank" Font-Underline="True">作品</asp:HyperLink>
        <asp:Label ID="Labelsize" runat="server"></asp:Label>
        <asp:Label ID="Labelmsg" runat="server" Font-Bold="False"></asp:Label>
    <asp:Image ID="Imagegood" runat="server"  ImageUrl="~/images/good16.png" Width="16px" />
    <asp:Label ID="Labelgood" runat="server"></asp:Label>
    <asp:Label ID="LabelWdate" runat="server"></asp:Label>
    <br />
    <br />
    作品评分：<asp:Label ID="LbWscore" runat="server"></asp:Label>
       &nbsp;作品加分：<asp:Label ID="LbWdscore" runat="server"></asp:Label>
       &nbsp;互评得分：<asp:Label ID="LbWfscore" runat="server"></asp:Label>&nbsp; 教师评语：<asp:Label ID="LbWself" runat="server" ForeColor="#3399FF"></asp:Label>
    <br />
    </div> 
    <div style=" text-align:center;">
    <asp:Literal ID="Literal1" runat="server"></asp:Literal> 
    </div>
    <div>
        <br />        
        <br />
        <asp:Label ID="Labelwid" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="Labeltype" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="Labelwurl" runat="server" Visible="False"></asp:Label>
    </div>

<br />
</div>
        </div>
    </center>
</div>

     </form>
</body>
</html>

