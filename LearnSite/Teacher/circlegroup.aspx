<%@ Page Language="C#" AutoEventWireup="true" StylesheetTheme="Teacher"  CodeFile="circlegroup.aspx.cs" Inherits="Teacher_circlegroup" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>小组作品展示</title>
</head>
<body style="margin: 0 auto;">
    <form id="form1" runat="server">
    <div style="font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt">
    <div style="border-width: 1px; border-color: #EFEFEF; padding: 2px; text-align: center; background-color: #EEFCE4; border-bottom-style: solid;">
    <asp:Label  ID="LabeMtitle" runat="server" Font-Bold="True" Font-Size="10pt"></asp:Label>
    </div>
    <div style="text-align: right">
    <asp:LinkButton ID="LinkBtnying" runat="server" onclick="LinkBtnying_Click" 
            ToolTip="让查看过作品隐藏" Font-Underline="False" >隐</asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="LinkBtnxi" runat="server" onclick="LinkBtnxi_Click" 
            ToolTip="让查看过作品显示" Font-Underline="False" >显</asp:LinkButton>
        &nbsp;
        </div>
        <div style="margin: 0px; padding: 2px;">
        <asp:DataList ID="DataListGroup" runat="server" RepeatLayout="Flow" 
                onitemcommand="DataListGroup_ItemCommand" 
                onitemdatabound="DataListGroup_ItemDataBound" Width="100%" CellPadding="0">
            <ItemTemplate>
            <div style="padding: 2px; border-bottom-style: dashed; border-width: 1px; border-color: #CCCCCC">
                <div>
                      <asp:Label ID="LabelSid" runat="server" Text='<%# Eval("Sid") %>' Visible="false" ></asp:Label>
                      <asp:Label ID="LabelSnum" runat="server" Text='<%# Eval("Snum") %>' Visible="false" ></asp:Label>
                      <asp:Image ID="ImageFlag" runat="server" ImageUrl="~/Images/gflag.gif" />
                      <asp:Label ID="LabelLeader" runat="server" Text='<%# Eval("Sname") %>' Font-Bold="True"></asp:Label>
                    &nbsp;<asp:Label ID="LabelGstus" runat="server" Width="60%"></asp:Label>
                    &nbsp;<asp:DropDownList ID="DDLGscores" runat="server" AutoPostBack="True"  
                          Font-Size="9pt" onselectedindexchanged="DDLGscores_SelectedIndexChanged">
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
                    </asp:DropDownList>&nbsp;学分
                    &nbsp;<asp:LinkButton ID="LinkBtnView" runat="server" CausesValidation="false" 
                        CommandName="V" Text="作品查看" Font-Underline="False"></asp:LinkButton>
                </div>
                <div style="margin: auto; text-align: center">
                    <asp:Literal ID="LiteralView" runat="server"></asp:Literal>
                </div>
              </div>
            </ItemTemplate>        
        </asp:DataList>
        </div>
        <br />
    </div>
    </form>
</body>
</html>
