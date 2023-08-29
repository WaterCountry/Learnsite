<%@ page language="C#" autoeventwireup="true" stylesheettheme="Teacher" inherits="Teacher_circleshow, LearnSite" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>学生文档作品自动展示</title>
    <script src="../js/jquery.min.js" type="text/javascript"></script>
</head>
<body>
    <center>
	<br>
    <div>
    <form id="form1" runat="server">
    <div style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 11pt">
    <asp:Label  ID="LabeMtitle" runat="server" Font-Bold="True"></asp:Label>&nbsp;
        <asp:Button ID="Btnflash" runat="server" Text="刷新" onclick="Btnflash_Click" 
            SkinID="BtnSmall" Width="40px" />
        &nbsp;
        <asp:Button ID="Btnrestart" runat="server" Text="重新" onclick="Btnrestart_Click" 
            SkinID="BtnSmall" Width="40px" />
        &nbsp;
        <asp:Button ID="Btnstop" runat="server" Text="继续" onclick="Btnstop_Click" 
            SkinID="BtnSmall" Width="40px" />
        &nbsp;<asp:ImageButton ID="ImgBtnLeft" runat="server" ImageUrl="~/images/left.png" 
            onclick="ImgBtnLeft_Click" Width="16px" />
    <asp:DropDownList ID="DDLstore" runat="server" 
            Font-Bold="True" Width="100px" AutoPostBack="True" Font-Size="12pt" 
            onselectedindexchanged="DDLstore_SelectedIndexChanged" >
        <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
        <asp:ImageButton ID="ImgBtnright" runat="server" 
            ImageUrl="~/images/right.png" onclick="ImgBtnright_Click" />
            <asp:Label ID="Labelnum" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
         <br />
         <div id="stuname" style=" display:none; font-size:xx-large;">
         <asp:Label ID="Labelname" runat="server" ></asp:Label>
         </div>
         <asp:Label ID="lbcurindex" runat="server" Text="0" Visible="False"></asp:Label>
         <br />

         <div >        
             <asp:ImageButton ID="ImgBtnTextbox" runat="server" CommandName="v" 
                 ImageUrl="~/images/peer_review.png" onclick="ImgBtnTextbox_Click" />
             教师评语：<asp:TextBox ID="TextBoxWself" 
                 runat="server" BorderColor="Silver" BorderStyle="Dashed" BorderWidth="1px" 
                 BackColor="#FDF5E3" ></asp:TextBox> 
             <asp:Image ID="Image2" runat="server" ImageUrl="~/images/token.png" />
             加分：<asp:TextBox ID="TextBoxWdsocre" runat="server" MaxLength="2" Width="40px" 
                 BackColor="#FDF5E3" SkinID="TextBoxNum" Height="19px">0</asp:TextBox>
             <asp:RadioButtonList 
                 ID="RBLselect" runat="server"   RepeatDirection="Horizontal" Visible="True" 
            Font-Size="16pt" AutoPostBack="True" 
                 onselectedindexchanged="RBLselect_SelectedIndexChanged" RepeatLayout="Flow" 
              CellPadding="0" CellSpacing="18" Width="240px" >
                                    <Items>
                                    <asp:ListItem>G</asp:ListItem>
                                    <asp:ListItem>A</asp:ListItem>
                                    <asp:ListItem>B</asp:ListItem>
                                    <asp:ListItem>C</asp:ListItem>
                                    <asp:ListItem>D</asp:ListItem>
                                    <asp:ListItem>E</asp:ListItem>
                                    <asp:ListItem>O</asp:ListItem>
                                    </Items>
                                </asp:RadioButtonList>
        <asp:ImageButton ID="ImgBtn" runat="server" ImageUrl="~/images/refresh.gif" 
            onclick="ImgBtn_Click" ToolTip="循环展播专用刷新" />
             &nbsp;<asp:ImageButton ID="BtnCheck" runat="server" onclick="BtnCheck_Click" 
              ImageUrl="~/images/python.png" ToolTip="将自动得分Python作品设置为已评" />
          &nbsp; 
               <img id="showname" src="../images/help.png"  alt="显示姓名"/>&nbsp;
                  <asp:DropDownList ID="DDLname" runat="server"  AutoPostBack="True" 
                 Width="60px" onselectedindexchanged="DDLname_SelectedIndexChanged">
                <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
                 &nbsp;
                 <asp:CheckBox ID="CkselectG" runat="server" Text="筛Ｇ评" 
                 ToolTip="推荐作品筛选" AutoPostBack="True" 
                 oncheckedchanged="CkselectG_CheckedChanged" />&nbsp;
            <asp:CheckBox ID="CheckselectA" runat="server" Text="筛A评" 
                 ToolTip="优秀作品筛选" AutoPostBack="True" 
                 oncheckedchanged="CheckselectA_CheckedChanged"  /> &nbsp;
            <asp:CheckBox ID="CheckBoxW" runat="server" Text="筛未评" 
                 ToolTip="未评作品筛选" AutoPostBack="True" 
                 oncheckedchanged="CheckBoxW_CheckedChanged"  />  
            &nbsp;&nbsp;
             <asp:ImageButton ID="ImageBtnDel" runat="server" ImageUrl="~/images/delete.gif" 
                 onclick="ImageBtnDel_Click" ToolTip="删除作品" style="width: 12px" />
                         </div>

        </div>  
          
        <div style="height:80vh" >
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
        <div>
        <br />
        <asp:HyperLink ID="Hlcode" runat="server" Font-Size="11pt" Target="_blank" 
            Visible="False" CssClass="HyperlinkNormal" >查看脚本</asp:HyperLink> 
        </div>
    </form> 
        <script type ="text/javascript" >
            function myrefresh() {
                var stxt = document.getElementById("<%= Btnstop.ClientID %>").value;
                if (stxt == "暂停") {
                    document.getElementById("<%= ImgBtn.ClientID %>").click();
                }
            }
            setTimeout("myrefresh()", 8000); //指定8秒刷新一次作品
            
            $("#showname").click(function () {
                $("#stuname").slideToggle();
            });

        </script>

    </div> 
    </center>
</body>
</html>
