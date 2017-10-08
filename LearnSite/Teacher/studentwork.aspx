<%@ Page Language="C#" AutoEventWireup="true" CodeFile="studentwork.aspx.cs" Inherits="Teacher_studentwork" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center; font-family: 宋体, Arial, Helvetica, sans-serif; font-size: 9pt;">    
        <br />
        学号：<asp:Label ID="LabelSnum" runat="server"></asp:Label>
&nbsp;姓名：<asp:Label ID="LabelSname" runat="server"></asp:Label>
&nbsp;作品总分：<asp:Label ID="LabelWscore" runat="server"></asp:Label>
&nbsp;
        本学期作品列表&nbsp; 
        <asp:HyperLink ID="HlkCircle" runat="server" ForeColor="Blue" 
             Target="_blank">展播预览</asp:HyperLink>
        <br />
        <br />
        <div style="margin: auto; width: 98%;">
    <asp:GridView ID="GridViewworks" runat="server"  Width="100%"  
                AutoGenerateColumns="False" EnableModelValidation="True" 
            BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" 
            CellPadding="4" Font-Size="9pt" GridLines="Horizontal" 
                onrowdatabound="GridViewworks_RowDataBound">
        <Columns>
            <asp:BoundField DataField="Wgrade" HeaderText="年级" />
            <asp:BoundField DataField="Wclass" HeaderText="班级" />
            <asp:BoundField DataField="Wterm" HeaderText="学期" />
            <asp:BoundField DataField="Ctitle" HeaderText="学案" />
            <asp:BoundField DataField="Mtitle" HeaderText="活动" />
            <asp:BoundField DataField="Wscore" HeaderText="学分" />
            <asp:BoundField DataField="Wdscore" HeaderText="加分" />
            <asp:BoundField DataField="Wvote" HeaderText="鲜花"/>
            <asp:TemplateField HeaderText="查看">
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkWurl" runat="server"  Target="_blank" 
                      ToolTip='<%# Eval("Wurl") %>'  Text="下载"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLinkView" runat="server" NavigateUrl="" Text="预览" Target="_blank"></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="日期">
                <ItemTemplate>
                    <asp:Label ID="LabelWdate" runat="server" Text='<%# Bind("Wdate") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CheckBoxField DataField="Wcheck" HeaderText="评价" ReadOnly="True" />
        </Columns>
        <FooterStyle BackColor="White" ForeColor="#333333" />
        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />        
        <RowStyle BackColor="White" ForeColor="#333333" HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
    </asp:GridView>
    </div>
        <br />
        <br />
        <br />
     <asp:Button ID="Btnclose" runat="server"   Text="关闭" BackColor="WhiteSmoke" 
            BorderColor="#CCCCCC" BorderStyle="None" Font-Size="9pt" Height="20px" 
            Width="60px" />
        <br />
        <br />
        <asp:Label ID="Labelmsg" runat="server"></asp:Label>
        <br />
        <br />
    </div>
    </form>
</body>
</html>
