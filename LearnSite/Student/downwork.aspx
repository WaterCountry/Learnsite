<%@ Page Title="" Language="C#" StylesheetTheme="Student" Validaterequest="false" AutoEventWireup="true" CodeFile="downwork.aspx.cs" Inherits="Student_downwork" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>作品下载</title> 
    </head>
<body ondragstart="return false" onselectstart ="return false" onselect="document.selection.empty()" 
oncopy="document.selection.empty()" onbeforecopy="return false" onmouseup="document.selection.empty()">
    <form id="form1" runat="server">
     <center >
    <div  class="studmasterhead"> 
      <div class="stu">
<div id="student">
<div class="left">
<div style="background-color: #E8F3FF; padding-bottom: 2px;"> 
    <br />
    〖<asp:Label ID="Labelmission" runat="server"></asp:Label>〗
        <strong>作品下载</strong>：<asp:Image ID ="ImageType" runat="server" />
<asp:HyperLink ID="HLfile" runat="server"  Visible="False" BorderStyle="None" 
        CssClass="txtszcenter" Height="18px" Target="_blank" Font-Underline="True">作品</asp:HyperLink>
        <asp:Label ID="Labelsize" runat="server"></asp:Label>
        <asp:Label ID="Labelmsg" runat="server" Font-Bold="False"></asp:Label>
    <asp:Image ID="Imagegood" runat="server"  ImageUrl="~/Images/good16.png" Width="16px" />
    <asp:Label ID="Labelgood" runat="server"></asp:Label>
    <asp:Label ID="LabelWdate" runat="server"></asp:Label>
    </div> 
    <br />
    <div>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal> 
    </div>
    <div>
        <br />
        <asp:Button ID="Buttonpreview" runat="server" onclick="Buttonpreview_Click" 
            SkinID="buttonSkin" Text="预览查看" />
        <br />
        <asp:Label ID="Labelwid" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="Labeltype" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="Labelwurl" runat="server" Visible="False"></asp:Label>
    </div>
</div>
<div class="right" >
    <div style=" text-align:left; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt;">
    <div style="background-color: #E8F3FF;  height:18px">
        <strong>&nbsp;自我评价：</strong>
        </div>
        <div>
                        <asp:DataList  ID="DataListSelf" runat="server" Font-Size="9pt" 
                            CellPadding="3" RepeatColumns="1" CaptionAlign="Left"  CellSpacing="3">
                        <ItemTemplate>
                        <div>
                            <asp:Image ID="Imageno" runat="server" ImageUrl="~/Images/item.png" />
                            <asp:Label ID="LbMitem" runat="server" Text='<%# Eval("Mitem") %>'></asp:Label>
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/smile16.gif" />
                        </div>
                        </ItemTemplate>
                    </asp:DataList> 
                    <br />
                    </div>
        <div style="background-color: #E8F3FF; height:18px">
       <strong> &nbsp;同学对你的作品评价:</strong>
       </div>
       <div>
                        <asp:DataList
                            ID="DataListGauge" runat="server" Font-Size="9pt" 
                            CellPadding="3" RepeatColumns="1" CaptionAlign="Left" 
                            onitemdatabound="DataListGauge_ItemDataBound"   CellSpacing="3">
                        <ItemTemplate>
                        <div>
                            <asp:Image ID="Imageno" runat="server" ImageUrl="~/Images/item.png" />
                            <asp:Label ID="LabelMid" runat="server" Text='<%# Eval("Mid") %>' Visible="false"></asp:Label>
                            <asp:Label ID="LbMitem" runat="server" Text='<%# Eval("Mitem") %>'></asp:Label>
                            <asp:Label ID="LabelCount" runat="server"></asp:Label>
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/smile16.gif" />
                        </div>
                        </ItemTemplate>
                    </asp:DataList>                       
       &nbsp;<asp:Image ID="Imagegoodtea0" runat="server"  ImageUrl="~/Images/item.png" />
                        &nbsp;互评得分：<asp:Label ID="LbWfscore" runat="server"></asp:Label>
       <asp:Image ID="Imagesmile0" runat="server" ImageUrl="~/Images/money.gif" />
                        <br />
       <br />
       </div>
       <div style="background-color: #E8F3FF; height:16px">
       <strong> &nbsp;老师对你的作品评价:</strong>
       </div>
       <div>
           <br />
       &nbsp;<asp:Image ID="Imagegoodtea" runat="server"  ImageUrl="~/Images/item.png" />
       &nbsp;作品打分：<asp:Label ID="LbWscore" runat="server"></asp:Label>
       <asp:Image ID="Imagesmile" runat="server" ImageUrl="~/Images/money.gif" />
           <br /><br />
       &nbsp;<asp:Image ID="Image1" runat="server"  ImageUrl="~/Images/item.png" />
       &nbsp;作品加分：<asp:Label ID="LbWdscore" runat="server"></asp:Label>
       <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/money.gif" />
           <br /><br />
       &nbsp;<asp:Image ID="Imagegoodtea1" runat="server"  ImageUrl="~/Images/item.png" />
       &nbsp;教师评语：<asp:Label ID="LbWself" runat="server" ForeColor="#3399FF"></asp:Label>
           <br />
       <br />
       </div>
    </div>

    </div>
<br />

</div>
</div>
</div>
</center>
 </form>
</body>
</html>

