<%@ Page Language="C#" AutoEventWireup="true" CodeFile="search.aspx.cs" Inherits="Workshow_search" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>信息技术作品展示</title>
</head>
<body style="margin: auto; padding-top: 0px;">
    <form id="form1" runat="server">
    <div id="bk"  style="text-align: center; ">
    <div id="sh" 
            style="border: 1px solid #EFEFEF; margin: auto; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt; text-align: left; width: 800px;">   
        <div>
            <img alt="" src="image/workshow.gif" /></div>
        <div style="width: 798px; height: 6px; background-color: #EFEFEF; border: 1px solid #E3E3E3">
        </div>
        <div>
            <img alt="" src="image/tag.gif" />入学年度：<asp:RadioButtonList ID="Rblyear" 
                runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" 
                AutoPostBack="True" onselectedindexchanged="Rblyear_SelectedIndexChanged">
            </asp:RadioButtonList>
            <br />
            <img alt="" src="image/tag.gif" />年级选择：<asp:RadioButtonList ID="Rblgrade" 
                runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" 
                AutoPostBack="True" onselectedindexchanged="Rblgrade_SelectedIndexChanged">
            </asp:RadioButtonList>
            <br />
            <img alt="" src="image/tag.gif" />班级选择：<asp:RadioButtonList ID="Rblclass" 
                runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" 
                AutoPostBack="True" onselectedindexchanged="Rblclass_SelectedIndexChanged" 
                RepeatColumns="10">
            </asp:RadioButtonList>
            <br />
            <img alt="" src="image/tag.gif" />学期选择：<asp:RadioButtonList ID="Rblterm" 
                runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" 
                onselectedindexchanged="Rblterm_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem Selected="True" Value="1">第一学期</asp:ListItem>
                <asp:ListItem Value="2">第二学期</asp:ListItem>
            </asp:RadioButtonList>
            <br />
            <img alt="" src="image/tag.gif" />课节选择：<asp:RadioButtonList ID="Rblcourse" 
                runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" 
                AutoPostBack="True" 
                onselectedindexchanged="Rblcourse_SelectedIndexChanged" RepeatColumns="10" >
            </asp:RadioButtonList>
            <br />
            <img alt="" src="image/tag.gif" />活动选择：<asp:RadioButtonList ID="Rblmission" 
                runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" 
                AutoPostBack="True" 
                onselectedindexchanged="Rblmission_SelectedIndexChanged" 
                RepeatColumns="5" >
            </asp:RadioButtonList>
            <br />
            <img alt="" src="image/zoom.gif" />浏览方式：<asp:RadioButtonList ID="Rbldisplay" 
                runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" 
                onselectedindexchanged="Rbldisplay_SelectedIndexChanged" 
                AutoPostBack="True">
                <asp:ListItem Selected="True" Value="1">列表显示</asp:ListItem>
                <asp:ListItem Value="2">幻灯片式</asp:ListItem>
            </asp:RadioButtonList>
            &nbsp;
            <br />
        </div>
        <div style="padding: 2px; text-align: center">
            <asp:Label ID="LabelTitle" runat="server" Font-Bold="True" ForeColor="#0000CC"></asp:Label>
            <br />
        </div>
        <div id="divlist" runat="server" visible="false" style="border: 1px dashed #F3F3F3; padding: 2px; margin: auto; width: 760px;">
            <asp:DataList ID="DLworks" runat="server" CellPadding="3" RepeatColumns="12" 
                RepeatDirection="Horizontal" RepeatLayout="Flow" CellSpacing="3" 
                onitemcommand="DLworks_ItemCommand">
                <ItemStyle Height="20px" />
                <ItemTemplate>
                <div style="width: 62px; height: 20px">
                    <asp:LinkButton ID="lBtnWname" runat="server"  CommandArgument='<%# Eval("Wurl") %>' CommandName="S" 
                        Text='<%# Eval("Wname") %>' ></asp:LinkButton>
                        </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
        <div id="divview" runat="server" visible="false" style="margin: auto; text-align: center;">
            <asp:ImageButton ID="ImgBtnleft" runat="server" 
                ImageUrl="~/Workshow/image/left.gif" onclick="ImgBtnleft_Click" />
    <asp:DropDownList ID="DDLstore" runat="server" 
            Font-Bold="True" Width="80px" AutoPostBack="True" Font-Size="9pt" 
            onselectedindexchanged="DDLstore_SelectedIndexChanged">
        <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
            <asp:ImageButton ID="ImgBtnright" runat="server" 
                ImageUrl="~/Workshow/image/right.gif" onclick="ImgBtnright_Click" />
            <br />
            <asp:Label ID="lbcount" runat="server"></asp:Label>
        </div>        
    </div>
    <div style="padding: 2px; margin: auto; text-align: center; font-size: 9pt;">
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            <br />
            <br />
        </div>
    </div>
    </form>
</body>
</html>
