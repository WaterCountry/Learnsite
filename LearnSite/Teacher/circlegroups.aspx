<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="Teacher"  CodeFile="circlegroups.aspx.cs" Inherits="Teacher_circlegroups" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>小组作品展示</title>
</head>
<body style="margin: 0 auto;">
    <form id="form1" runat="server">
    <div style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt">
    <div style="border-width: 1px; border-color: #EFEFEF; padding: 2px; text-align: center; background-color: #EEFCE4; border-bottom-style: solid;">
    <asp:Label  ID="LabeMtitle" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label>
    </div>
    <div style="padding: 2px; text-align: center">
        <asp:DataList ID="DLgroups" runat="server" RepeatDirection="Horizontal" 
            RepeatLayout="Flow" CellPadding="3" RepeatColumns="6" 
            onitemdatabound="DLgroups_ItemDataBound" CellSpacing="3" 
            onitemcommand="DLgroups_ItemCommand">
            <ItemTemplate>                               
                <asp:LinkButton ID="LbSgtitle" runat="server"  Text='<%# Eval("Sgtitle") %>'  CommandName="S" SkinID="LinkBtn"></asp:LinkButton>
                <asp:Label ID="LabelSid" runat="server" Text='<%# Eval("Sid") %>' Visible="false" ></asp:Label>                
                <asp:Label ID="LabelSname" runat="server" Text='<%# Eval("Sname") %>' Visible="false" ></asp:Label>
                <asp:Label ID="LabelSnum" runat="server" Text='<%# Eval("Snum") %>' Visible="false" ></asp:Label>
            </ItemTemplate>
        </asp:DataList>
        <br />
        <asp:Label ID="Labelgid" runat="server" Visible="False"></asp:Label>
        <asp:ImageButton ID="ImgBtnrefresh" runat="server" ImageUrl="~/Images/b.gif" 
            onclick="ImgBtnrefresh_Click" />
        <asp:Label ID="Labelpos" runat="server" Text="0" Visible="False"></asp:Label>
        <asp:Label ID="Labellastpos" runat="server" Text="0" Visible="False"></asp:Label>
                 <br />
                <img alt="组长" src="../Images/gflag.gif" /><asp:Label 
            ID="LabelSgtitle" runat="server" 
            Font-Bold="True" ForeColor="#0066FF" ></asp:Label>
&nbsp;组长：<asp:Label 
            ID="LabelLeader" runat="server" 
            Font-Bold="False" ForeColor="#0066FF" ></asp:Label>
&nbsp;成员：<asp:Label ID="Labelmember" runat="server" ForeColor="#3399FF" ></asp:Label>
&nbsp;学分
        <asp:DropDownList ID="DDLGscores" runat="server" AutoPostBack="True"  
                          Font-Size="9pt" 
            onselectedindexchanged="DDLGscores_SelectedIndexChanged" Width="40px" 
            Font-Bold="False" Font-Names="Arial" ForeColor="#336600">
                          <asp:ListItem Value="20">A+</asp:ListItem>
                          <asp:ListItem Value="19">A</asp:ListItem>
                          <asp:ListItem Value="18">A-</asp:ListItem>
                          <asp:ListItem Value="17">B+</asp:ListItem>
                          <asp:ListItem Value="16">B</asp:ListItem>
                          <asp:ListItem Value="15">B-</asp:ListItem>
                          <asp:ListItem Value="14">C+</asp:ListItem>
                          <asp:ListItem Value="13">C</asp:ListItem>
                          <asp:ListItem Value="12">C-</asp:ListItem>
                          <asp:ListItem Value="0">0</asp:ListItem>
                    </asp:DropDownList>
        <br />
        <br />
        <asp:ImageButton ID="ImgBtnLeft" runat="server" ImageUrl="~/Images/left.png" 
            onclick="ImgBtnLeft_Click" Width="16px" />
            &nbsp;<asp:Button ID="BtnCicle" runat="server" onclick="BtnCicle_Click" 
            SkinID="BtnSmall" Text="播放" />
            &nbsp;<asp:ImageButton ID="ImgBtnright" runat="server" 
            ImageUrl="~/Images/right.png" onclick="ImgBtnright_Click" />

        <script type ="text/javascript" >
            function myrefresh() {
                var stxt = document.getElementById("<%= BtnCicle.ClientID %>").value;
                if (stxt == "暂停") {
                    document.getElementById("<%= ImgBtnrefresh.ClientID %>").click();
                }
            }
            setTimeout("myrefresh()", 8000); //指定8秒刷新一次作品         
        </script>
        <br />
        <div style="padding: 2px;margin: auto; text-align: center;">
                    <asp:Literal ID="LiteralView" runat="server"></asp:Literal>
        </div>
        </div>
        <br />
    </div>
    </form>
</body>
</html>