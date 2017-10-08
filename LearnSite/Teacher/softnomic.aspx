<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="Teacher"  CodeFile="softnomic.aspx.cs" Inherits="Teacher_softnomic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>自学园作品评价与展示</title>
</head>
<body>
    <form id="form1" runat="server">
    <div  style="top: 2px">
    <center>
     <div style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt">    
    资源分类：<asp:DropDownList ID="DDLCategory" runat="server" 
             Width="300px" AutoPostBack="True" 
             onselectedindexchanged="DDLCategory_SelectedIndexChanged">
        </asp:DropDownList>
         <br />
         <br />
         资源标题：<asp:DropDownList ID="DDLsoft" runat="server" Width="300px" 
             AutoPostBack="True" onselectedindexchanged="DDLsoft_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <br /></div>
    <div style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt">    
        <asp:Button ID="Btnflash" runat="server" Text="刷新" onclick="Btnflash_Click" 
            SkinID="BtnSmall" Width="40px" />
        &nbsp;
        <asp:Button ID="Btnrestart" runat="server" Text="重新" onclick="Btnrestart_Click" 
            SkinID="BtnSmall" Width="40px" />
        &nbsp;
        <asp:Button ID="Btnstop" runat="server" Text="继续" onclick="Btnstop_Click" 
            SkinID="BtnSmall" Width="40px" />
        &nbsp;<asp:ImageButton ID="ImgBtnLeft" runat="server" ImageUrl="~/Images/left.png" 
            onclick="ImgBtnLeft_Click" Width="16px" />
    <asp:DropDownList ID="DDLstore" runat="server" 
            Font-Bold="True" Width="100px" AutoPostBack="True" Font-Size="12pt" 
            onselectedindexchanged="DDLstore_SelectedIndexChanged">
        <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
        <asp:ImageButton ID="ImgBtnright" runat="server" 
            ImageUrl="~/Images/right.png" onclick="ImgBtnright_Click" />
            <asp:Label ID="Labelnum" runat="server" Font-Names="Arial" Font-Size="9pt"></asp:Label>
         <br />
         <asp:Label ID="lbcurindex" runat="server" Text="0" Visible="False"></asp:Label>
         <br />
         <div>
        教师评语：<asp:TextBox ID="TextBoxWself" runat="server" Width="350px" 
                BorderColor="Silver" BorderStyle="Dashed" BorderWidth="1px" 
                 BackColor="#FFF9E1"></asp:TextBox> 
        <asp:RadioButtonList ID="RBLselect" runat="server"   RepeatDirection="Horizontal" Visible="True" 
            Font-Size="9pt" AutoPostBack="True" onselectedindexchanged="RBLselect_SelectedIndexChanged" RepeatLayout="Flow" 
              CellPadding="3" CellSpacing="3">
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
                                 &nbsp;<asp:CheckBox ID="CkFlash" runat="server" 
                 oncheckedchanged="CkFlash_CheckedChanged" Text="FlashLoop" 
                 ToolTip="Flash播放循环设置" AutoPostBack="True" />
        &nbsp;&nbsp;&nbsp; <asp:Button ID="Btndel" runat="server" Text="删除" onclick="Btndel_Click" 
            SkinID="BtnSmall" ToolTip="删除该作品，不可恢复！" Width="40px" />
        </div>   
        </div>        
        <div style=" font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt; margin: 2px; " >
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </div>
        <br />
        <asp:ImageButton ID="ImgBtn" runat="server" ImageUrl="~/Images/refresh.gif" 
            onclick="ImgBtn_Click" ToolTip="循环展播专用刷新" />
        </center>
        <script type ="text/javascript" >
            function myrefresh() {
                var stxt = document.getElementById("<%= Btnstop.ClientID %>").value;
                if (stxt == "暂停") {
                    document.getElementById("<%= ImgBtn.ClientID %>").click();
                }
            }
            setTimeout("myrefresh()", 8000); //指定8秒刷新一次作品         
        </script>

    </div>
    </form>
</body>
</html>
