<%@ page title="" language="C#" masterpagefile="~/student/Scm.master" autoeventwireup="true" inherits="Student_console, LearnSite" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Cpcm" Runat="Server">
			<asp:Label ID="LabelCid" runat="server" Visible="False"></asp:Label>
			<asp:Label ID="LabelLid" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="LabelNid" runat="server" Visible="False"></asp:Label> 
    <div  id="showcontent">
<div class="left" >
<br />
    <div   class="missiontitle">
    <asp:Label ID="LabelMtitle"  runat="server" ></asp:Label><br />
   </div>
   <div class="courseother">
   </div>   
<div   id="Mcontent"  class="coursecontent" runat="server">	
		</div>
		<br />
		<br />
</div>
<div class="right"><br />
<center>    
    <link href="../kindeditor/themes/me/me.css" rel="stylesheet" type="text/css" />
    <script charset="utf-8" src="../kindeditor/kindeditor-min.js" type="text/javascript"></script>
		<script charset="utf-8" src="../kindeditor/lang/zh_CN.js" type="text/javascript"></script>
        <div   class="missiontitle">
           <br /></div>
    <div>
        <br />
            <asp:GridView ID="GVSolve" runat="server" EnableModelValidation="True" 
            AutoGenerateColumns="False" onrowdatabound="GVSolve_RowDataBound" 
            Font-Size="11pt">
                <Columns>
                    <asp:BoundField HeaderText="题目" />
                    <asp:TemplateField HeaderText="得分">
                        <ItemTemplate>
                            <asp:Label ID="Labelscore" runat="server" Text='<%# Bind("Vscore") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="Labelflag" runat="server" ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
        </asp:GridView>
            <br />
            <asp:Button ID="BtnIdle" runat="server" Font-Bold="True" 
                 SkinID="buttonSkinPink" Text="开始测评" onclick="BtnIdle_Click" />
            <br /><br />
                    <asp:ImageButton ID="Btnclock" runat="server" ImageUrl="~/images/clock.gif" 
            onclick="Btnclock_Click"  />
            <br />
            <asp:Image ID="Imagepass" runat="server" ImageUrl="~/images/pass.png"  />
            <br />
            <br />
            <br />
            <asp:HyperLink ID="Hlsolve" runat="server" Target="_blank" Font-Size="11pt" 
            CssClass="btncopy" Width="120px" >班级测评报告</asp:HyperLink>
            <br />
            <br />
    </div>       
    <br />
    <br />
    </center>
</div>   
    <br />

</div>
</asp:Content>

