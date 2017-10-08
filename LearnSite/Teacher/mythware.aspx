<%@ Page Title="" Language="C#" MasterPageFile="~/Teacher/Teach.master" StylesheetTheme="Teacher" AutoEventWireup="true" CodeFile="mythware.aspx.cs" Inherits="Teacher_mythware" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
 <div   class="placehold">   
     <div style="padding: 2px; margin: 0px">
         <br />
     极域班级模型ClassModel
     </div>
     <br />
     <div style="margin: auto; border: 1px solid #EBEBEB; padding: 2px; width: 680px; background-color: #F8FEFC;">
         最新模型文件夹列表<asp:ImageButton 
             ID="ImgBtnDown" runat="server" 
             ImageUrl="~/Images/down.gif" onclick="ImgBtnDown_Click" ToolTip="点击打包下载" 
             style="width: 16px" />
         <br />
                <asp:DataList ID="Dlfilelist" runat="server" 
                    RepeatColumns="3" RepeatDirection="Horizontal" CellPadding="3" CellSpacing="3" Width="100%" >
                    <ItemTemplate>
                        <div  style="text-align: left;">
                            <asp:Label ID="Labelfid" runat="server" Text='<%# Eval("fid") %>' ></asp:Label>&nbsp;
                            <asp:HyperLink ID="HLfname" runat="server" Target="_blank" Text='<%# Eval("fname") %>' ></asp:HyperLink>&nbsp;                            
                            <asp:Label ID="Labelfsize" runat="server" Text='<%# Eval("fsize") %>' ></asp:Label>
                            <asp:Label ID="Labelfread" runat="server" Text='<%#  Eval("fread") %>'  ToolTip="是否只读（T：只读 | F：可写）"  ForeColor="#00A279"></asp:Label>
                            <asp:Label ID="Labelurl" runat="server" Text='<%# Eval("furl") %>' Visible="false" ></asp:Label>                        
                        </div>
                    </ItemTemplate>
                </asp:DataList>
     </div>
     <br />
     <asp:Label ID="Labeldirhid" runat="server" Visible="False"></asp:Label>
     <asp:Label ID="Labeldir" runat="server" Visible="False"></asp:Label>
     <br />
     <br />
     <div>请选择你原有的班级模型xml或cls格式文件：<asp:FileUpload ID="FuClassModel" runat="server" 
             Font-Size="9pt" />
&nbsp;<br />
         <br />
         <asp:CheckBox ID="CkMachine" runat="server" Text="空余学生机是否预处理为主机名" 
             ToolTip="主机名与IP对应表有记录则有效" Checked="True" />
         <br />
         <br />
         <br />
         选择签到时间：<asp:DropDownList ID="DDLmonth" runat="server" 
             Font-Size="9pt">
             <asp:ListItem Value="1" Selected="True">1周内</asp:ListItem>
             <asp:ListItem Value="2">2周内</asp:ListItem>
             <asp:ListItem Value="3">3周内</asp:ListItem>
             <asp:ListItem Value="4">4周内</asp:ListItem>
             <asp:ListItem Value="5">5周内</asp:ListItem>
             <asp:ListItem Value="6">6周内</asp:ListItem>
         </asp:DropDownList>
         <br />
         <br />
         <br />
         <asp:HyperLink ID="Hlkroom" runat="server" ImageUrl="~/Images/zoom.gif" 
             NavigateUrl="~/Teacher/myseat.aspx" Target="_blank" ToolTip="机房视图预览">HyperLink</asp:HyperLink>
         电脑室名称：<asp:TextBox ID="TextBoxRoom" runat="server" 
             Width="60px"></asp:TextBox>
         <br />
         <br />
         <br />
         <br />
        <asp:Button ID="BtnBuild" runat="server" onclick="BtnBuild_Click" 
            SkinID="BtnLong" Text="生成任教班级模型" />
         <br />
         <br />
         <asp:Label ID="Labelmsg" runat="server" SkinID="LabelMsgRed" Width="98%"></asp:Label>
         <br />
         <br />
         <span style="color: #1480EB">说明：根据最近几个月内的签到表的姓名与IP对应，生成所教班级模型，可点击打包按钮下载。</span></div>
     <br />
     <br />
     
 </div>
</asp:Content>

